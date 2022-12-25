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
    /// 数据交换报文基类
    /// </summary>
    public abstract class BaseFDE
    {
        /// <summary>
        /// 数据类型
        /// </summary>
        public string TITLE { get; set; }
        public string SOURCE { get; set; }
    }
}
