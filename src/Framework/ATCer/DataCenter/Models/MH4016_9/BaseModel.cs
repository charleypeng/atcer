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

namespace ATCer.DataCenter.Models.MH4016_9
{
    public class BaseModel
    {
        public DateTime TIME { get; set; }
    }

    public class DataItem
    {
        public string PorpertyName { get; set; }
        public int MyProperty { get; set; }
    }
}
