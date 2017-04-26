using System.Collections.Generic;

namespace GooglePublicDns
{
    public class GooglePublicDnsResponse
    {
        public int Status { get; set; }

        // ReSharper disable once InconsistentNaming
        public bool TC { get; set; }

        // ReSharper disable once InconsistentNaming
        public bool RD { get; set; }

        // ReSharper disable once InconsistentNaming
        public bool RA { get; set; }

        // ReSharper disable once InconsistentNaming
        public bool AD { get; set; }

        // ReSharper disable once InconsistentNaming
        public bool CD { get; set; }

        public List<Question> Question { get; set; }

        public List<Answer> Answer { get; set; }

        public string Comment { get; set; }

        public string RawJson { get; set; }
    }
}
