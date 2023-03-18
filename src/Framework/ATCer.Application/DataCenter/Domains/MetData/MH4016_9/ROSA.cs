// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.DataCenter.Domains.MH4016_9
{
    /// <summary>
    /// 道面传感器
    /// </summary>
    public class ROSA:BaseMetDomain
    {
        public MetTuple? STEMP { get; set; }
        public MetTuple? GTEMP { get; set; }
        public MetTuple? SURFSTA { get; set; }
        public MetTuple<string>? ALARM_STATUS { get; set; }
        public MetTuple<string>? RAIN_STATUS { get; set; }
        public MetTuple<string>? SURFACE_STATUS { get; set; }
    }
}
