// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Base;

namespace ATCer.DataCenter.Dtos.RadarDataDtos;

public class BaseRadarDto<TKey> : BaseDto<TKey>
{
    public string? SourceId { get; set; }
}