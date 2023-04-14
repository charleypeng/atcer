using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? list)
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
        public static void AddNonNullObject<T>(this IList<T>? lst, T? item)
        {
            if (lst == null)
                lst = new List<T>();
            if(item != null)
            {
                lst?.Add(item);
            }
        }

        /// <summary>
        /// Add Non-Null object into a list
        /// </summary>
        /// <param name="lst">non-empty list</param>
        /// <param name="item">item object</param>
        /// <typeparam name="T">given object type</typeparam>
        public static void AddNonNullListObject<T>(this IList<IEnumerable<T>> lst, IEnumerable<T> item)
        {
            if (!item.IsNullOrEmpty())
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
        
        /// <summary>
        /// assume float or int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsFloatOrInt(this string value) {
            int intValue;
            float floatValue;
            return Int32.TryParse(value, out intValue) || float.TryParse(value, out floatValue);
        }

        public static T CloneObject<T>(this object source)
        {
            T result = Activator.CreateInstance<T>();
            return result;
        }

        public static void AddUnique<T>(this IList<T>? lst, T? value)
        {
            if (lst == null)
                return;
            if (value == null)
                return;

            if (!lst.Contains(value))
                lst.Add(value);
        }

        /// <summary>
        /// returns a boolean where collection1 equals collection2 no matter the order
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comparer1">integer list</param>
        /// <param name="comparer2">integer list</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool SameAs<T>(this IEnumerable<T>? comparer1, IEnumerable<T>? comparer2)
        {
            var myObject = Activator.CreateInstance<T>();

            var myLong = myObject as long?;

            if (myLong == null)
                throw new Exception($"the given type '{typeof(T)}' should be integer");

            if (comparer1 == null || comparer2 == null)
                return false;

            if (comparer1 == null && comparer2 == null)
                return true;

            if(comparer1?.Count() != comparer2?.Count())
                return false;

            var c1 = comparer1?.OrderBy(x=>x).ToList();
            var c2 = comparer2?.OrderBy(x=>x).ToList();

            bool isSame = false;
            for (var i = 0; i < c1?.Count(); i++)
            {
                isSame = c1[i]!.Equals(c2![i]);

                if (!isSame)
                    break;
            }

            return isSame;
        }

        /// <summary>
        /// returns a boolean where the given compare list contains the given list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comparer1"></param>
        /// <param name="comparer2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool NoOrderContains<T>(this IEnumerable<IEnumerable<T>?>? comparer1, IEnumerable<T>? comparer2)
        {
            if(comparer1 == null)
                throw new ArgumentNullException(nameof(comparer1));

            if(comparer2 == null)
                throw new ArgumentNullException(nameof(comparer2));

            bool isSame = false;
            foreach (var item in comparer1)
            {
                if (item == null) continue;

                isSame = item.SameAs(comparer2);

                if (isSame) break;
            }
            return isSame;
        }
    }
    
}