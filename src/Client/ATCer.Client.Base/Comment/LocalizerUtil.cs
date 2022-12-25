﻿// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.Client.Base
{
    /// <summary>
    /// 静态本地化器
    /// </summary>
    public class LocalizerUtil
    {
        /// <summary>
        /// 本地化器
        /// </summary>
        public static IClientLocalizer Localizer;

        /// <summary>
        /// 合并多个
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public static string Combination(params string[] names)
        {
            if (Localizer != null)
            {
               return Localizer.Combination(names);
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取本地化结果
        /// </summary>
        /// <param name="name"></param>
        /// <param name="toLower"></param>
        /// <returns></returns>
        public static string GetValue(string name,bool toLower=false)
        {
            if (Localizer != null && !string.IsNullOrEmpty(name))
            {
                if (toLower)
                {
                    return Localizer[name].ToLower();
                }
                return Localizer[name];
            }
            return string.Empty;
        }
    }
}
