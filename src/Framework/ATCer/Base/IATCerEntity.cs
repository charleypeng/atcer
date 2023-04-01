// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authentication.Enums;

namespace ATCer.Base;

/// <summary>
/// Entity基类
/// </summary>
public interface IATCerEntity
{
    /// <summary>
    /// 是否锁定
    /// </summary>
    bool IsLocked { get; set; }
    /// <summary>
    /// 是否删除
    /// </summary>
    bool IsDeleted { get; set; }
}