using System.Collections.Generic;
using System.IO;
using System.Linq;
using MKVdefaultTrackSwitcher;

namespace WindowsFormsApp1
{
    class MkvWriter
    {
        private List<MKVfile> LsMkVfilePaths { get; }

        public MkvWriter(List<MKVfile> lsMKVfilePaths)
        {
            this.LsMkVfilePaths = lsMKVfilePaths;
        }

        public bool ApplyDefaultTracks(int trackAudio, int trackSubtitle)
        {
            // Remove read-only flag from all files
            foreach (var x in LsMkVfilePaths)
            {
                FileAttributes attrs = File.GetAttributes(x.filepath);
                if (attrs.HasFlag(FileAttributes.ReadOnly))
                    File.SetAttributes(x.filepath, attrs & ~FileAttributes.ReadOnly);

                string commandargs = "";
                x.streams.Where(i => i.codec_type != "attachment").ToList().ForEach(t =>
                {
                    string flagtrack = (t.index + 1).ToString(); //mkvpropedit track index is 1 higher than FFMPEG
                    string flagdefault = LsMkVfilePaths.First().streams.Any(p => 
                        (p.index == trackAudio || p.index == trackSubtitle) &&
                        p.codec_type == t.codec_type &&
                        p.tags.language == t.tags.language &&
                        p.tags.title == t.tags.title) ? "1" : "0";

                    commandargs += $" --edit track:@{flagtrack} --set flag-default={flagdefault} --set flag-forced=0";
                });

                string mkveditres = Helper.ExecuteEXE("mkvpropedit", $"\"{x.filepath}\"{commandargs}");
                if (!mkveditres.EndsWith("Done."))
                {
                    return false;
                }
            }

            return true;
        }

    }
}
