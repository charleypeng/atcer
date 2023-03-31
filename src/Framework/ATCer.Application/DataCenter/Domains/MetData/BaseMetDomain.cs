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
    public class BaseMetDomain<Tkey>:ATCElasticEntity<Tkey>
    {
        /// <summary>
        /// 数据原ID
        /// </summary>
        public string? SourceId { get; set; }
        
        /// <summary>
        /// 传感器位置
        /// </summary>
        public string? Location { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BaseMetDomain : BaseMetDomain<string>
    {
    }
}
