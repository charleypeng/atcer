// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using AntDesign.ProLayout;
using System;
using System.Collections.Generic;

namespace ATCer.Client.Base
{
    /// <summary>
    /// 客户端菜单缓存
    /// </summary>
    public static class ClientMenuCache
    {
        /// <summary>
        /// 以path为key的字典
        /// </summary>
        private static Dictionary<string, MenuDataItem> pathMap = new Dictionary<string, MenuDataItem>();

        /// <summary>
        /// 添加到缓存中
        /// </summary>
        /// <param name="menu"></param>
        public static void Add(MenuDataItem menu)
        {
            if (string.IsNullOrEmpty(menu.Path))
            {
                return;
            }
            UriBuilder uriBuilder = new UriBuilder($"http://www.gardener.com{menu.Path}");
            if (!pathMap.ContainsKey(uriBuilder.Path))
            {
                pathMap.Add(uriBuilder.Path, menu);
            }
        }

        /// <summary>
        /// 根据path获取菜单信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static MenuDataItem Get(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            if (pathMap.ContainsKey(path))
            {
                return pathMap[path];
            }
            return null;
        }
    }
}
