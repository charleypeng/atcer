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

namespace ATCer.HRCenter.Dtos
{
    public class ImportFromExcelOutput
    {
        public int OriginTotalCount { get; set; }
        public int ProcessedTotalCount { get; set; }
        public bool Succeed { get; set; } = false;
        public object Errors { get; set; }
    }
}
