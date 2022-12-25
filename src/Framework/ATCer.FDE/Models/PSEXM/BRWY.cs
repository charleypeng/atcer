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
    /// 机场跑道状态信息
    /// </summary>
    public class BRWY : BaseFDE
    {
        public string AIRPORT { get; set; }
        public IList<string> RUNWAY { get; set; }
    }
}
