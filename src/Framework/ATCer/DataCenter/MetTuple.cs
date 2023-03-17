// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2023  版权所有
// -----------------------------------------------------------------------------

using ATCer.DataCenter.Enums;

namespace ATCer.DataCenter;

/// <summary>
/// 气象数据基类
/// </summary>
public class MetTuple<T>
{
    /// <summary>
    /// 数据状态
    /// <remarks>默认状态为Normal</remarks>
    /// </summary>
    public MetDataStatus Status { get; set; } = MetDataStatus.Normal;
    /// <summary>
    /// 数据值
    /// </summary>
    public T? Value { get; set; }
    /// <summary>
    /// 数据显示名称
    /// </summary>
    public string? DisplayName { get; set; }
}

/// <summary>
/// 气象数据基类
/// <remarks>Float作为基础类</remarks>
/// </summary>
public class MetTuple : MetTuple<float?>
{
}