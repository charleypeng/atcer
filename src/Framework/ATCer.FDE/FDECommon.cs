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

namespace ATCer.FDE
{
    /// <summary>
    /// 数据交换通用配置
    /// </summary>
    public static class FDECommon
    {
        /// <summary>
        /// 报文头
        /// </summary>
        public const string Prefix = "ZCZC";
        /// <summary>
        /// 报文尾
        /// </summary>
        public const string Suffix = "NNNN";
        /// <summary>
        /// 开始标记
        /// </summary>
        public const string Begin = "BEGIN";
        /// <summary>
        /// 结束标记
        /// </summary>
        public const string End = "END";
        /// <summary>
        /// 分隔符
        /// </summary>
        public const string Split = "-";
        /// <summary>
        /// 分隔空格
        /// </summary>
        public const string Space = " ";
        /// <summary>
        /// 通配模板
        /// </summary>
        public const string RegTemplate = "(?<={0})(.+\\S)";
        /// <summary>
        /// 新行
        /// </summary>
        public static  string NewLine = Environment.NewLine;
    }
}
