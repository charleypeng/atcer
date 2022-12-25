// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.HRCenter.Enums
{
    /// <summary>
    /// ICAO等级
    /// </summary>
    public enum ICAOLevel
    {
        /// <summary>
        /// 3级
        /// </summary>
        [Description("3级")]
        Level3 = 3,
        /// <summary>
        /// 4级
        /// </summary>
        [Description("4级")]
        Level4 = 4,
        /// <summary>
        /// 5级
        /// </summary>
        [Description("5级")]
        Level5 = 5,
        /// <summary>
        /// 6级
        /// </summary>
        [Description("6级")]
        Level6 = 6
    }
}
