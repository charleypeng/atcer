// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.DataCenter.Domains.MH4016_9
{
    /// <summary>
    /// 天气现象
    /// </summary>
    public class PW
    {
        public RW RW { get; set; }
        public WXNWS WXNWS { get; set; }
        public WMOINS WMOINS { get; set; }
        public WMO15A WMO15A { get; set; }
        public WMO60A WMO60A { get; set; }
        public PRW1A PRW1A { get; set; }
        public PRWS PRWS { get; set; }
        public PRSS PRSS { get; set; }
        public TBINS TBINS { get; set; }
    }

    public class PRSS
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

    public class PRW1A
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

    public class PRWS
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

    public class RW
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

    public class TBINS
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

    public class WMO15A
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

    public class WMO60A
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

    public class WMOINS
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

    public class WXNWS
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }
}
