// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.LTFATCenter.Enums
{
    /// <summary>
    /// 程序方向
    /// </summary>
    public enum SADPDirection
    {
        /// <summary>
        /// 北
        /// </summary>
        [Description("北")]
        NORTH = 0,
        /// <summary>
        /// 东
        /// </summary>
        [Description("东")]
        EAST = 1,
        /// <summary>
        /// 南
        /// </summary>
        [Description("南")]
        SOUTH = 2,
        /// <summary>
        /// 西
        /// </summary>
        [Description("西")]
        WEST = 3
    }
}
