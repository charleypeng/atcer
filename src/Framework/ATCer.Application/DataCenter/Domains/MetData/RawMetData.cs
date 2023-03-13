// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.DataCenter.Domains
{
    /// <summary>
    /// 
    /// </summary>
    public class RawMetData
    {
        /// <summary>
        /// 
        /// </summary>
        public string? TYPE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? LOC { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<List<string?>?>? DATA { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? TIME { get; set; }
    }
}
