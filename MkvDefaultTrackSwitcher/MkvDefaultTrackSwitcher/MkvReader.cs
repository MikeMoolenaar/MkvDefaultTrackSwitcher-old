using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace MKVdefaultTrackSwitcher
{
    class MkvReader
    {
        public List<Stream> lsAudioStreams;
        public List<Stream> lsSubtitleStreams;
        public List<MKVfile> lsMkvfile;
        public List<Stream> streams => lsAudioStreams.Concat(lsSubtitleStreams).ToList();

        public MkvReader(List<string> lsMKVfilePaths)
        {
            lsMkvfile = new List<MKVfile>();
            foreach (string mkvfile in lsMKVfilePaths)
            {
                string mkvinfo = Helper.ExecuteEXE("ffprobe", string.Format("-v quiet -print_format json -show_streams \"{0}\"", mkvfile));
                var result = JsonConvert.DeserializeObject<MKVstreamlist>(mkvinfo);
                lsMkvfile.Add(new MKVfile() { streams = result.streams, filepath = mkvfile });
            }

            lsAudioStreams = lsMkvfile.First().streams.Where(x => x.codec_type == "audio").ToList();
            lsSubtitleStreams = lsMkvfile.First().streams.Where(x => x.codec_type == "subtitle").ToList();
        }

        
    }
}
