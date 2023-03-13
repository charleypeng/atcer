﻿// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.DataCenter.Enums
{
    public enum MetDatType
    {
        /// <summary>
        /// 长整型
        /// </summary>
        I = 0,
        /// <summary>
        /// 浮点型
        /// </summary>
        R = 1,
        /// <summary>
        /// 字符串型
        /// </summary>
        S = 2
    }
    public struct MetDataTypeString
    {
        public const string DInteger = "I";
        public const string DFloat = "R";
        public const string DString = "S";
    }
}
