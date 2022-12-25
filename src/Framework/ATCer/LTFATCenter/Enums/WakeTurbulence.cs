/* This project is part of the CBTSoft, which is a private license under the legal of laws.
 * Any copy or use may cause legal problem.
 * Author: Peng Lei
 * Created Date:2019-1-20 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ATCer.LTFATCenter.Enums
{
    /// <summary>
    /// 尾流等级
    /// </summary>
    public enum WakeTurbulence
    {

        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UNKNOWN,
        /// <summary>
        /// 轻型
        /// </summary>
        [Description("轻型")]
        L,
        ///<summary>
        /// 中型
        ///</summary>
        [Description("中型")]
        M,
        ///<summary>
        /// 重型
        ///</summary>
        [Description("重型")]
        H,
        ///<summary>
        /// 超重型
        ///</summary>
        [Description("极重型")]
        S,
        ///<summary>
        /// 超重型
        ///</summary>
        [Description("超重型")]
        J
    }

    /// <summary>
    /// 
    /// </summary>
    public static class WakeTurbulenceExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static WakeTurbulence ToWakeTurbulence(this string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return WakeTurbulence.UNKNOWN;
            try
            {
                WakeTurbulence p = WakeTurbulence.UNKNOWN;
                var s = Enum.TryParse(status.ToUpper(), out p);
                if (s)
                {
                    return p;
                }
                else
                {
                    return WakeTurbulence.UNKNOWN;
                }
            }
            catch
            {
                return WakeTurbulence.UNKNOWN;
            }
        }
    }
}
