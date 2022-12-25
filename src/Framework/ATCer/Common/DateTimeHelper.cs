// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;

namespace ATCer.Common
{
    /// <summary>
    /// 日期工具类
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// 格式化日期差值为
        /// dd:HH:mm:ss
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns></returns>
        public static string FormatDateTimeDiff(DateTimeOffset begin, DateTimeOffset end) 
        {
            TimeSpan ts = (end - begin);
            string sdt2 = string.Format("{0}:{1}:{2}:{3}", ts.Days.ToString().PadLeft(2, '0'), ts.Hours.ToString().PadLeft(2, '0'), ts.Minutes.ToString().PadLeft(2, '0'), ts.Seconds.ToString().PadLeft(2, '0'));
            return sdt2;
        }
    }
}
