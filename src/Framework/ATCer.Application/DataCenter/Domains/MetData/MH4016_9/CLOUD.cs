// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.DataCenter.Domains.MH4016_9
{
    /// <summary>
    /// 云
    /// </summary>
    public class CLOUD: BaseMetDomain
    {
        /// <summary>
        /// 是否晴空
        /// <para>1=晴空</para>
        /// <para>0=非晴空</para>
        /// </summary>
        public MetTuple? ISSKYCLEAR { get; set; }
        /// <summary>
        /// 是否探测到垂直能见度
        /// <para>0=没有 1=有</para>
        /// </summary>
        public MetTuple? ISVERVIS { get; set; }
        /// <summary>
        /// 垂直能见度
        /// <para>单位：m</para>
        /// </summary>
        public MetTuple? VERVIS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MetTuple? CLOUDBASE { get; set; }
        public MetTuple? CH1INS { get; set; }
        public MetTuple? CH2INS { get; set; }
        public MetTuple? CH3INS { get; set; }
        public MetTuple? AMOUNT1 { get; set; }
        public MetTuple? CH1 { get; set; }
        public MetTuple? AMOUNT2 { get; set; }
        public MetTuple? CH2 { get; set; }
        public MetTuple? AMOUNT3 { get; set; }
        public MetTuple? CH3 { get; set; }
        public MetTuple? AMOUNT4 { get; set; }
        public MetTuple? CH4 { get; set; }
    }
}
