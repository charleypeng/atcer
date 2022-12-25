// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ATCer.Base
{
    /// <summary>
    /// 定义过滤表达式功能
    /// </summary>
    public interface IDynamicFilterService
    {
        /// <summary>
        /// 获取指定查询条件组的查询表达式
        /// </summary>
        /// <typeparam name="T">表达式实体类型</typeparam>
        /// <param name="groups">查询条件组，如果为null，则直接返回 true 表达式</param>
        /// <returns>查询表达式</returns>
        Expression<Func<T, bool>> GetExpression<T>(List<FilterGroup> groups);
    }
}
