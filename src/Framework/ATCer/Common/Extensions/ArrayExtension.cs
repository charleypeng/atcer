// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.Collections.Generic;

namespace ATCer.Common
{
    public static class ArrayExtension
    {
        /// <summary>
        /// 数组转为get请求参数集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, object>> ConvertToQueryParameters<T>(this T[] datas, string name)
        {
            List<KeyValuePair<string, object>> paramas = new List<KeyValuePair<string, object>>();
            if (datas != null)
            {
                foreach (T id in datas)
                {
                    paramas.Add(new KeyValuePair<string, object>(name, id));
                }
            }
            return paramas;
        }
    }
}
