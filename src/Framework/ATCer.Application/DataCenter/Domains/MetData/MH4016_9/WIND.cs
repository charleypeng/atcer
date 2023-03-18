// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.DataCenter.Domains.MH4016_9
{
    /// <summary>
    /// 风速和风向
    /// </summary>
    public class WIND:BaseMetDomain
    {
        public MetTuple? WSINS { get; set; }
        public MetTuple? WDINS { get; set; }
        public MetTuple? WS2A { get; set; }
        public MetTuple? WS2M { get; set; }
        public MetTuple? WS2X { get; set; }
        public MetTuple? WD2A { get; set; }
        public MetTuple? WD2M { get; set; }
        public MetTuple? WD2X { get; set; }
        public MetTuple? WS10A { get; set; }
        public MetTuple? WS10M { get; set; }
        public MetTuple? WS10X { get; set; }
        public MetTuple? WD10A { get; set; }
        public MetTuple? WD10M { get; set; }
        public MetTuple? WD10X { get; set; }
        public MetTuple? HW2A { get; set; }
        public MetTuple? CW2A { get; set; }
        /// <summary>
        /// 侧风风速
        /// <para>
        /// 单位：kn
        /// </para>
        /// </summary>
        public MetTuple<string>? CW2A_KT_STR { get; set; }
        /// <summary>
        /// 测风风速
        /// <para>单位：mps</para>
        /// </summary>
        public MetTuple<string>? CW2A_MPS_STR { get; set; }
    }
}
