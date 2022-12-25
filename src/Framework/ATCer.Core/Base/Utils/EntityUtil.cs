// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer
{
    /// <summary>
    /// Entity Utilities
    /// </summary>
    public static class EntityUtil
    {
        /// <summary>
        /// 获取主键值
        /// </summary>
        public static object GetEntityMainKey<TEntity>(TEntity entity)
        {
            if (entity == null)
                return null;
            var key = entity.GetType()
                            .GetProperties()
                            .FirstOrDefault(p => p.CustomAttributes.Any(attr => attr.AttributeType == typeof(KeyAttribute)));
            return key == null ? null : key.GetValue(entity);
        }

        /// <summary>
        /// 获取主键值
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static object ToEntityMainKey<TEntity>(this TEntity entity)
        {
            return GetEntityMainKey(entity);
        }
    }
}
