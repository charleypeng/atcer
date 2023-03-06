using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATCer
{
    /// <summary>
    /// General Extensions for ATCer
    /// </summary>
    public static class GeneralHelper
    {
        /// <summary>
        /// To ensure whether the given list is null or empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list == null || list?.Count() == 0;
        }

        /// <summary>
        /// To assume a certain string reversely contains in a certain string 
        /// </summary>
        /// <param name="str1">basic string</param>
        /// <param name="str2">the given string</param>
        /// <returns></returns>
        public static bool ReverseContain(this string str1, string str2)
        {
            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
                return false;

            return str2.Contains(str1);
        }

        /// <summary>
        /// To check whether the given string is null or empty white space
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmptyString(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Safely toupper a string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SafeToUpper(this string str)
        {
            return str?.Trim().ToUpper();
        }

        /// <summary>
        /// Safely toupper a string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SafeToLower(this string str)
        {
            return str?.Trim().ToLower();
        }

        /// <summary>
        /// Add Non-Null object into a list
        /// </summary>
        /// <param name="lst">non-empty list</param>
        /// <param name="item">item object</param>
        /// <typeparam name="T">given object type</typeparam>
        public static void AddNonNullObject<T>(this IList<T> lst, T item)
        {
            if(item != null)
            {
                lst?.Add(item);
            }
        }

        /// <summary>
        /// Safely parse a datetime with less exception
        /// </summary>
        /// <param name="str">the given date time string</param>
        /// <param name="reMoveSeconds">true remove the seconds</param>
        /// <returns></returns>
        public static DateTime SafeParseDateTime(this string str, bool reMoveSeconds = false)
        {
            if (str.IsEmptyString())
                throw new Exception("given datetime is not in correct form");

            var dt = DateTime.Parse(str);

            if (reMoveSeconds)
            {
                dt = dt.AddSeconds(-dt.Second);
            }

            return dt;
        }

        /// <summary>
        /// To confirm the characters are all ASCSII  numbers
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumber(this string str)
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