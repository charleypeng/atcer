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

namespace ATCer
{
    /// <summary>
    /// 色彩空间
    /// </summary>
    public struct ColorSpace
    {
        /// <summary>
        /// 离港程序颜色
        /// </summary>
        public struct SidColor
        {
            /// <summary>
            /// 北向
            /// </summary>
            public const string North = "#009966";
            /// <summary>
            /// 南向
            /// </summary>
            public const string South = "#FFFF99";
            /// <summary>
            /// 西向
            /// </summary>
            public const string West = "#FF9900";
            /// <summary>
            /// 东向
            /// </summary>
            public const string East = "#0099CC";
        }
    }
}
