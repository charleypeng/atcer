//// -----------------------------------------------------------------------------
//// ATCer 全平台综合性空中交通管理系统
////  作者：彭磊  
////  CopyRight(C) 2022  版权所有 
//// -----------------------------------------------------------------------------

//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;
//using ATCer.Base;
//using Furion.DatabaseAccessor;

//namespace ATCer.Base
//{
//    /// <summary>
//    /// 基类
//    /// </summary>
//    /// <typeparam name="TKey"></typeparam>
//    /// <typeparam name="TDbContextLocator1"></typeparam>
//    /// <typeparam name="TDbContextLocator2"></typeparam>
//    public abstract class ATCerBaseEntity<TKey, TDbContextLocator1, TDbContextLocator2> :
//        GardenerEntityBase<TKey, TDbContextLocator1, TDbContextLocator2>, IBaseEntity
//        where TDbContextLocator1 : class, IDbContextLocator
//        where TDbContextLocator2 : class, IDbContextLocator
//    {
//        /// <summary>
//        /// 主键
//        /// </summary>
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public override TKey Id { get; set; }
//    }

//    /// <summary>
//    /// 基类
//    /// </summary>
//    /// <typeparam name="TKey"></typeparam>
//    /// <typeparam name="TDbContextLocator1"></typeparam>
//    public abstract class ATCerEntityBase<TKey, TDbContextLocator1> :
//        GardenerEntityBase<TKey, TDbContextLocator1, MasterDbContextLocator>
//        where TDbContextLocator1: class, IDbContextLocator
//    {

//    }

//    /// <summary>
//    /// 基类
//    /// </summary>
//    /// <typeparam name="TKey"></typeparam>
//    public abstract class ATCerEntityBase<TKey> :
//        GardenerEntityBase<TKey, MasterDbContextLocator>
//    {

//    }

//    /// <summary>
//    /// 基类
//    /// </summary>
//    public abstract class ATCerEntityBase : ATCerEntityBase<int, MasterDbContextLocator>
//    {

//    }
//}
