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
    /// 二次代码分配、回收信息
    /// </summary>
    public class BSSR:BaseFDE
    {
        /// <summary>
        /// 二次代码
        /// </summary>
        /// <value></value>
        public string SSRCODE { get; set; }
        public string SSROPER { get; set; }
        public DateTime? OPERTIME { get; set; }
        public string ARCID { get; set; }
        public string ADEP { get; set; }
        public string ADES { get; set; }
        public DateTime? EOBD { get; set; }
        public DateTime? EOBT { get; set; }
    }
}
