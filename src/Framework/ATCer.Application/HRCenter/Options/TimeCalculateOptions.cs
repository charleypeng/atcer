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

namespace ATCer.HRCenter.Options
{
    /// <summary>
    /// 小时费计算选项
    /// </summary>
    public class TimeCalculateOptions
    {
        public TimeSpan DaySpan { get; set; } = new TimeSpan(8, 0, 0);
        public TimeSpan NightSpan { get; set; } = new TimeSpan(22, 0, 0);
    }
}
