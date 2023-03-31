// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.DataCenter.Domains.RadarData;

/// <summary>
/// 雷达数据基类
/// </summary>
/// <typeparam name="TKey"></typeparam>
public abstract class BaseRadarDomain<TKey> : ATCElasticEntity<TKey>
{
    /// <summary>
    /// 源数据Id
    /// </summary>
    [Nest.Keyword]
    public string? SourceId { get; set; }
}
