﻿// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;

namespace ATCer.Attributes
{
    /// <summary>
    /// Enum 转换集合时忽略
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class IgnoreOnConvertToMapAttribute : Attribute
    {
    }
}
