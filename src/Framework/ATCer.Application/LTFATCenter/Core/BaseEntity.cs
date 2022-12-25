// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Furion.DatabaseAccessor;

namespace ATCer.LTFATCenter.Core
{
    /// <summary>
    /// 2项基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TDBContextLocator1"></typeparam>
    public abstract class BaseEntity<TKey, TDBContextLocator1>: PrivateBaseEntity<TKey>
                                                                where TDBContextLocator1 : class, IDbContextLocator
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Tkey"></typeparam>
    public abstract class PrivateBaseEntity<Tkey>: IPrivateEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual Tkey Id { get; set; }
    }

    public abstract class PrivateBaseEntity<TKey, TName>:IPrivateEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual TKey FlightPlanID { get; set; }
    }
}
