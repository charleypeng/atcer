// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2023  版权所有
// -----------------------------------------------------------------------------

namespace ATCer.ElasticSearch.Interfaces;

/// <summary>
/// ElasticSearch index locator
/// </summary>
public interface IESIndex
{
    /// <summary>
    /// Index name of this elastic entity
    /// </summary>
    public string IndexName { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public Func<bool>? Mapping { get; set; }
}