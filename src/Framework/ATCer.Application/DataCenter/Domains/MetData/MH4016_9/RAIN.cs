// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.DataCenter.Domains.MH4016_9
{
    /// <summary>
    /// 降水
    /// </summary>
    public class RAIN:BaseMetDomain
    {
        /// <summary>
        /// 降水传感器
        /// </summary>
        /// <remarks>
        /// <para>0:没有探测到降水</para> 
        /// <para>1:探测到降水</para>
        /// </remarks>
        public MetTuple? RAINON { get; set; }
        public MetTuple? DURATION_1H { get; set; }
        public MetTuple? AMOUNT_INS { get; set; }
        public MetTuple? SUM_INS { get; set; }
        public MetTuple? SUM_1H { get; set; }
    }
}
