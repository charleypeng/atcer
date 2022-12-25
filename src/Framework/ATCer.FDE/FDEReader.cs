// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ATCer.FDE.FDECommon;

namespace ATCer.FDE
{
    internal class STRObj
    {
        string Type { get; set; }
        string Content { get; set; }
    }
    /// <summary>
    /// 数据交换读取类
    /// </summary>
    internal class FDEReader
    {
        /// <summary>
        /// 判断是否为合法的报文
        /// </summary>
        /// <param name="data"></param>
        /// <param name="spliter"></param>
        /// <returns></returns>
        internal static string[] GetFDEData(string data, string spliter = Split)
        {
            if(string.IsNullOrWhiteSpace(data))
                return null;
            // remove '\n'  in case of linux failure
            data = data.Replace("\n", NewLine);
            if (data.StartsWith(Prefix))
            {
                data = data.Replace(Prefix, string.Empty).Replace(Suffix, string.Empty);

                return data.Trim().Split(spliter, int.MaxValue, StringSplitOptions.RemoveEmptyEntries);
            }
            else if (data.StartsWith('"') && data.EndsWith('"'))
            {
                data.Replace(data.First().ToString(), string.Empty);
                return data.Split(spliter, int.MaxValue, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringData"></param>
        /// <returns></returns>
        /// <exception cref="NotValidFDEException"></exception>
        internal static List<Tuple<string,object>> GetFDEDataDict(string stringData)
        {
            var values = GetFDEData(stringData, NewLine);
            //create a dictionary to store values
            var lst = new List<Tuple<string, object>>();
            
            //if not valid string data them return empty list
            if(values == null)
                return lst;

            //traversal all values and add them into dictionary
            for (int i = 0; i < values.Count(); i++)
            {
                var str1 = values[i];

                if(!str1.StartsWith("-BEGIN"))
                {
                    var array1 = str1.Split(' ', 2);
                    if(array1.Length == 2)
                        lst.Add(new Tuple<string, object>(array1[0].Replace("-",""), array1[1]));
                }
                else
                {
                    for (int j = i; j <1 ; j++)
                    {

                    }
                }
            }
            return lst;
        }

        //internal static List<STRObj> GetSTRObjs(string data)
        //{
        //    if (string.IsNullOrWhiteSpace(data))
        //        return new List<STRObj>();

        //    var beginIndex = data.IndexOf(FDECommon.Begin);
        //}

        internal static bool IsFDEData(string data)
        {
            return data.StartsWith(FDECommon.Prefix) && data.EndsWith(FDECommon.Suffix);
        }

        /// <summary>
        /// 判断是否为合法的报文
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal static string[] GetFDEData(byte[] data)
        {
            var str = Encoding.ASCII.GetString(data);
            return GetFDEData(str);
        }
        /// <summary>
        /// 判断是否为合法的报文
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal static Task<string[]> GetFDEDataAsync(string data)
        {
            return Task.FromResult(GetFDEData(data));
        }
        /// <summary>
        /// 判断是否为合法的报文
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal static Task<string[]> GetFDEDataAsync(byte[] data)
        {
            var str = Encoding.ASCII.GetString(data);
            return Task.FromResult(GetFDEData(str));
        }
    }
}
