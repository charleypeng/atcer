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
    public class PW:BaseMetDomain
    {
        /// <summary>
        /// 天气代码
        /// </summary>
        /// <remarks>
        /// 电码格式的机场例行天气报告
        /// <para>原名称为PW</para>
        /// </remarks>
        [DataName("PW")]
        public MetTuple<string>? PW_Instant { get; set; }
        /// <summary>
        /// 近时天气代码
        /// </summary>
        /// <remarks>资料中显示为 S 但数据为 I</remarks>
        public MetTuple<string>? RW { get; set; }
        public MetTuple<string>? WXNWS { get; set; }
        public MetTuple? WMOINS { get; set; }
        public MetTuple? WMO15A { get; set; }
        public MetTuple? WMO60A { get; set; }
        public MetTuple? PRW1A { get; set; }
        public MetTuple? PRWS { get; set; }
        public MetTuple? PRSS { get; set; }
        public MetTuple? TBINS { get; set; }
    }
}
