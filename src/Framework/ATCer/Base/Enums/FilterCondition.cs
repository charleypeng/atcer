// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Attributes;
using System.ComponentModel;

namespace ATCer.Base
{
    /// <summary>
    /// 过滤条件
    /// </summary>
    public enum FilterCondition
    {

        /// <summary>
        /// 并且
        /// </summary>
        [Code("and")]
        [Description("并且")]
        And = 1,

        /// <summary>
        /// 或者
        /// </summary>
        [Code("or")]
        [Description("或者")]
        Or = 2
    }
}
