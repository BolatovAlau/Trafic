using System.Collections.Generic;

namespace TraficLight.BusinessLogic.Entities
{
    public class Sequence
    {
        public string Id { get; set; }
        public string Start { get; set; } // List<NumInfo>
        public int StartNum { get; set; }
        public int FirstMissing { get; set; }
        public int SecondMissing { get; set; }
        public bool IsNotFirst { get; set; }
        public bool Broken { get; set; }
        public byte CurentDeep { get; set; }
    }

    public class Result
    {
        public List<NumInfo> NumInfos { get; set; }
        public bool NoResult { get; set; }
        public int Start { get; set; }
        public int FirstMissing { get; set; }
        public int SecondMissing { get; set; }
    }

    public class NumInfo
    {
        public int Start { get; set; }
        public int FirstMissing { get; set; }
        public int SecondMissing { get; set; }
    }
}
