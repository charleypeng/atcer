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

namespace ATCer.HRCenter.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class ATCTimeStats
    {
        [DisplayName("用户名")]
        public string UserName { get; set; }
        public string MyProperty { get; set; }
        public double TotalHours { get; set; }
        public List<ATCTimeRing> Ring { get; set; } = new List<ATCTimeRing>();
        public double Load { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ATCTimeRing
    {
        public string Index { get; set; }
        public int Value { get; set; }
    }
}
