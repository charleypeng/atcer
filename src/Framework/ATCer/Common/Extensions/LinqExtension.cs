﻿// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATCer.Common
{
    public static class LinqExtension
    {
        /// <summary>
        /// 使用异步遍历处理数据
        /// </summary>
        /// <typeparam name="T">需要遍历的基类</typeparam>
        /// <param name="list">集合</param>
        /// <param name="func">Lambda表达式</param>
        /// <returns> </returns>
        public static async Task ForEachAsync<T>(this IEnumerable<T> list, Func<T, Task> func)
        {
            foreach (T value in list)
            {
                await func(value);
            }
        }
    }
}
