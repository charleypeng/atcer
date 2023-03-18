// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.DataCenter.Domains.MH4016_9
{
    /// <summary>
    /// 主导能见度
    /// </summary>
    public class PV:BaseMetDomain
    {
        public MetTuple? RawVIS { get; set; }
        public MetTuple? VIS { get; set; }
        public MetTuple? VIS1A { get; set; }
        public MetTuple? VIS2A { get; set; }
        public MetTuple? VIS10A { get; set; }
        public MetTuple? VIS10M { get; set; }
        public MetTuple? VIS10X { get; set; }
        public MetTuple? SENSOR_VIS10M { get; set; }
        public MetTuple<string>? SENSOR_VIS10M_DIR { get; set; }
        public MetTuple? SENSOR_VIS10X { get; set; }
        public MetTuple<string>? SENSOR_VIS10X_DIR { get; set; }
    }
}
