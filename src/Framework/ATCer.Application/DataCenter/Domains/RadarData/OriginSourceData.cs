﻿// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.DataCenter.Domains.RadarData;

/// <summary>
/// 存储原始雷达数据
/// </summary>
public class OriginSourceData : BaseRadarDomain
{
    /// <summary>
    /// 雷达数据内容
    /// </summary>
    [Nest.Keyword(Index = false)]
    public string? RadarContent { get; set; }
}
