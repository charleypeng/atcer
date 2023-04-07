// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.HRCenter.Enums;

/// <summary>
/// 管制员等级
/// </summary>
public enum ATCLevel:byte
{
    /// <summary>
    /// 特级
    /// </summary>
    [Description("特级")]
    Teji = 0,
    /// <summary>
    /// 一级
    /// </summary>
    [Description("一级")]
    YiJi = 1,
    /// <summary>
    /// 二级
    /// </summary>
    [Description("二级")]
    ErJi = 2,
    /// <summary>
    /// 三级
    /// </summary>
    [Description("三级")]
    SanJi = 3,
    /// <summary>
    /// 四级
    /// </summary>
    [Description("四级")]
    SiJi = 4,
    /// <summary>
    /// 五级
    /// </summary>
    [Description("五级")]
    WuJi = 5,
}
