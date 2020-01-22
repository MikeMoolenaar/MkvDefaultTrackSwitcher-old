using System.Collections.Generic;

namespace MKVdefaultTrackSwitcher
{
    public class Disposition
    {
        public int @default { get; set; }
        public int forced { get; set; }
    }

    public class Tags
    {
        public string language { get; set; }
        public string title { get; set; }
    }

    public class Stream
    {
        public int index { get; set; }
        public string codec_name { get; set; }
        public string codec_type { get; set; }
        public Tags tags { get; set; }
        public Disposition disposition { get; set; }

        public override string ToString()
        {
            return this.tags is null ? $"(Unknown language or title) Stream {index}" : $"({this.tags.language}) {this.tags.title}";
        }
    }

    public class MKVstreamlist
    {
        public List<Stream> streams { get; set; }
    }

    public class MKVfile
    {
        public string filepath;
        public List<Stream> streams;
    }
}
