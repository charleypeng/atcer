// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight 2021  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.Common
{
    /// <summary>
    /// 时间类
    /// </summary>
    public class TimeConfig
    {
        /// <summary>
        /// 今日
        /// </summary>
        public DateTime DateOfToday { get; private set; }
        /// <summary>
        /// 昨日
        /// </summary>
        public DateTime Yesterday { get; private set; }
        /// <summary>
        /// 今日截止时间
        /// </summary>
        public DateTime EndTimeOfToday { get; private set; }
        /// <summary>
        /// 明日
        /// </summary>
        public DateTime Tomorrow { get; private set; }
        /// <summary>
        /// 明日截至时间
        /// </summary>
        public DateTime EndTimeOfTommorow { get; private set; }
        /// <summary>
        /// 今日开始时间
        /// </summary>
        public DateTime BeginTimeOfToday { get; private set; }
        /// <summary>
        /// 带参数使用
        /// </summary>
        /// <param name="dateTime"></param>
        public TimeConfig(DateTime dateTime)
        {
            DateOfToday = dateTime.Date;
            init();
        }
        /// <summary>
        /// 无参数使用
        /// </summary>
        public TimeConfig()
        {
            DateOfToday = DateTime.Now.Date;
            init();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void init()
        {
            Yesterday = DateOfToday.AddDays(-1);
            EndTimeOfToday = DateOfToday.AddHours(23).AddMinutes(59).AddSeconds(59);
            Tomorrow = DateOfToday.AddDays(1);
            BeginTimeOfToday = DateOfToday.AddHours(00).AddMinutes(00).AddSeconds(00);
            EndTimeOfTommorow = Tomorrow.AddHours(23).AddMinutes(59).AddSeconds(59);
        }

    }
}
