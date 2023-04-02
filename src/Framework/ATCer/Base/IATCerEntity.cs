// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.Base;

/// <summary>
/// Entity基类
/// </summary>
public interface IATCerEntity : IBaseEntity
{
    /// <summary>
    /// 是否锁定
    /// </summary>
    bool IsLocked { get; set; }
}