﻿// -----------------------------------------------------------------------------
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
    public class PT
    {
        public string PTID { get; set; }
        public string FL { get; set; }
        public DateTime? ETO { get; set; }
        public string ISPASS { get; set; }
    }
}
