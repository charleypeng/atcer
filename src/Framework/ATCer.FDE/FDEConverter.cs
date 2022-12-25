// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static ATCer.FDE.FDECommon;

namespace ATCer.FDE
{
    
    /// <summary>
    /// 飞行数据交换转换工具
    /// </summary>
    public class FDEConverter
    {
        /// <summary>
        /// 反序列化数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        [Obsolete("Please use Deserialize<T>")]
        public static T Deserialize_V0_1<T>(string value) where T : class, new()
        {
            var values = FDEReader.GetFDEData(value, NewLine);
            //create a dictionary to store values
            var lst = new List<Tuple<string, object>>();

            //if not valid string data them return empty list
            if (values == null)
                return default(T);

            Type typeFromHandle = typeof(T);
            object obj = Activator.CreateInstance(typeFromHandle);
            PropertyInfo[] properties = typeFromHandle.GetProperties();
            PropertyInfo[] array2 = properties;
            //traversal all values and add them into dictionary
            for (int i = 0; i < values.Count(); i++)
            {
                var str1 = values[i];

                if (!str1.StartsWith("-BEGIN"))
                {
                    var array1 = str1.Split(' ', 2);
                    if (array1.Length == 2)
                        lst.Add(new Tuple<string, object>(array1[0].Replace("-", ""), array1[1]));
                }
                else
                {
                    for (int j = i; j < 0; j++)
                    {

                    }
                }
            }
            return default(T);
        }
        /// <summary>
        /// 反序列化数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string value) where T:class,new()
        {
            var array = FDEReader.GetFDEData(value);
            try
            {
                Type typeFromHandle = typeof(T);
                object obj = Activator.CreateInstance(typeFromHandle);
                PropertyInfo[] properties = typeFromHandle.GetProperties();
                PropertyInfo[] array2 = properties;
                foreach (PropertyInfo propertyInfo in array2)
                {
                    string name = propertyInfo.PropertyType.Name;
                    var dt = typeof(DateTime?);
                    if (name == "String" || name == "Int32" || propertyInfo.PropertyType == typeof(DateTime?) || propertyInfo.PropertyType == typeof(DateTime))
                    {
                        string[] array3 = array;
                        // trasversal all data array
                        foreach (string text in array3)
                        {
                            if (text.IndexOf(propertyInfo.Name) == -1)
                            {
                                continue;
                            }
                            string pattern = $"(?<={propertyInfo.Name})(.+\\S)";
                            Match match = new Regex(pattern).Match(text);
                            if (!match.Success)
                            {
                                continue;
                            }
                            string text2 = name;
                            string text3 = text2;
                            int result;

                            if (propertyInfo.PropertyType == typeof(DateTime?) || text3 == "DateTime")
                            {
                                //Todo:根据MH4029.3中日期的格式进行设置
                                DateTime date;
                                if (DateTime.TryParse(match.Value.Trim(), out date))
                                {
                                    propertyInfo.SetValue(obj, date);
                                }
                            }
                            if (!(text3 == "Int32"))
                            {
                                if (text3 == "String")
                                {
                                    propertyInfo.SetValue(obj, match.Value.Trim());
                                }
                            }
                            else if (int.TryParse(match.Value.Trim(), out result))
                            {
                                propertyInfo.SetValue(obj, result);
                            }
                        }
                    }
                    else
                    {
                        if (!propertyInfo.PropertyType.IsGenericType)
                        {
                            continue;
                        }
                        string name2 = propertyInfo.Name;
                        string pattern2 = string.Format("(?<={0})(.+\\S)", "BEGIN");
                        string pattern3 = string.Format("(?<={0})(.+\\S)", "END");
                        Match match2 = new Regex(pattern2).Match(value);
                        Match match3 = new Regex(pattern3).Match(value);
                        if (!match2.Success || !match3.Success || !(match2.Value.Trim() == match3.Value.Trim()) || !(match3.Value.Trim() == name2))
                        {
                            continue;
                        }
                        int num = value.IndexOf(name2) + name2.Length;
                        int num2 = value.LastIndexOf("END");
                        string text4 = value.Substring(num, num2 - num - 1).Replace("\r\n", "").Replace("\n", "");
                        int num3 = text4.IndexOf("-");
                        int num4 = text4.IndexOf("-", num3 + "-".Length);
                        string text5 = text4.Substring(num3, num4 + 1).Trim();
                        string[] array4 = text4.Split(new string[1] { text5 }, int.MaxValue, StringSplitOptions.RemoveEmptyEntries);
                        IList list = Activator.CreateInstance(propertyInfo.PropertyType) as IList;
                        string[] array5 = array4;
                        foreach (string input in array5)
                        {
                            Type type = propertyInfo.PropertyType.GenericTypeArguments.FirstOrDefault();
                            object obj2 = Activator.CreateInstance(type);
                            PropertyInfo[] properties2 = type.GetProperties();
                            foreach (PropertyInfo propertyInfo2 in properties2)
                            {
                                string name3 = propertyInfo2.Name;
                                if (name3 == "RMK")
                                {
                                    propertyInfo2.SetValue(obj2, text5.Replace("-", "").Trim());
                                    continue;
                                }
                                string pattern4 = "(?<=" + name3 + " )([^\\-]+)";
                                Match match4 = new Regex(pattern4).Match(input);
                                if (!match4.Success)
                                {
                                    continue;
                                }
                                string name4 = propertyInfo2.PropertyType.Name;
                                string text6 = name4;
                                int result2;
                                var trimStr = match4.Value.Trim();

                                if (!(text6 == "Int32"))
                                {
                                    if (text6 == "String")
                                    {
                                        propertyInfo2.SetValue(obj2, trimStr);
                                    }
                                    else if (text6 == "DateTime" || propertyInfo2.PropertyType == typeof(DateTime?))
                                    {
                                        DateTime date;

                                        if(DateTime.TryParse(trimStr,null, DateTimeStyles.AssumeUniversal, out date))
                                        {
                                            propertyInfo2.SetValue(obj2, date.ToUniversalTime());
                                        }
                                        else if(DateTime.TryParseExact(trimStr, "yyyyMMddHHmmss",null, DateTimeStyles.AssumeUniversal, out date))
                                        {
                                            propertyInfo2.SetValue(obj2, date.ToUniversalTime());
                                        }
                                    }
                                }
                                else if (int.TryParse(trimStr, out result2))
                                {
                                    propertyInfo2.SetValue(obj2, result2);
                                }
                            }
                            list.Add(obj2);
                        }
                        propertyInfo.SetValue(obj, list);
                    }
                }
                return obj as T;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 序列化数据
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Serialize(object value)
        {
            string empty = string.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(Prefix);
            Type type = value.GetType();
            PropertyInfo[] properties = type.GetProperties();
            PropertyInfo[] array = properties;
            foreach (PropertyInfo propertyInfo in array)
            {
                if (propertyInfo.GetMethod.IsVirtual)
                {
                    continue;
                }
                if (propertyInfo.PropertyType.IsGenericType)
                {
                    stringBuilder.Append("-BEGIN " + propertyInfo.Name);
                    IEnumerable<object> enumerable = propertyInfo.GetValue(value) as IEnumerable<object>;
                    foreach (object item in enumerable)
                    {
                        PropertyInfo[] properties2 = item.GetType().GetProperties();
                        PropertyInfo[] array2 = properties2;
                        foreach (PropertyInfo propertyInfo2 in array2)
                        {
                            if (propertyInfo2.Name == "RMK")
                            {
                                stringBuilder.AppendLine().Append(string.Format("{0}{1}{2}", "-", propertyInfo2.GetValue(item), " "));
                                continue;
                            }
                            stringBuilder.Append(string.Format("{0}{1} {2}{3}", "-", propertyInfo2.Name, propertyInfo2.GetValue(item), " "));
                        }
                    }
                    stringBuilder.AppendLine().AppendLine("-END " + propertyInfo.Name);
                }
                else
                {
                    stringBuilder.AppendLine("-" + propertyInfo.Name + " " + propertyInfo.GetValue(value));
                }
            }
            stringBuilder.Append("NNNN");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// To confirm the characters are all ASCSII  numbers
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static bool isNum(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] < '0' || str[i] > '9')
                    return false;
            }
            return true;
        }
    }
}
