// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using ATCer.Authentication.Enums;
using System.ComponentModel;

namespace ATCer.Base
{
    /// <summary>
    /// 基类
    /// </summary>
    public abstract class ATCerEntityBase<TKey, TDbContextLocator1, TDbContextLocator2> :
        Entity<TKey, TDbContextLocator1, TDbContextLocator2>,IATCerEntity 
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
    {
        /// <summary>
        /// 是否锁定
        /// </summary>
        [DisplayName("是否锁定")]
        public bool IsLocked { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建者编号
        /// </summary>
        [DisplayName("创建者编号")]
        public string? CreatorId { get; set; }

        /// <summary>
        /// 创建者身份类型
        /// </summary>
        [DisplayName("创建者身份类型")]
        public IdentityType CreatorIdentityType { get; set; }
    }
    /// <summary>
    /// 基类
    /// </summary>
    public abstract class ATCerEntityBase<TKey, TDbContextLocator1> :
        ATCerEntityBase<TKey, TDbContextLocator1, MasterDbContextLocator> where TDbContextLocator1 : class, IDbContextLocator
    {}
    /// <summary>
    /// 基类
    /// </summary>
    public abstract class ATCerEntityBase<TKey> :
        ATCerEntityBase<TKey, MasterDbContextLocator>
    {}
    /// <summary>
    /// 基类
    /// </summary>
    public abstract class ATCerEntityBase :
        ATCerEntityBase<int>
    { }
}
