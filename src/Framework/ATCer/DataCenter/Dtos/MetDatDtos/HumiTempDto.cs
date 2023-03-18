// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.DataCenter.Dtos.MetDatDtos
{
    public class HumiTempDto:BaseMetDto
    {
        public MetTuple TAINS { get; set; }
        public MetTuple TA10M { get; set; }
        public MetTuple TA10X { get; set; }
        public MetTuple TA1hA { get; set; }
        public MetTuple TA1hM { get; set; }
        public MetTuple TA1hX { get; set; }
        public MetTuple TDINS { get; set; }
        public MetTuple RHINS { get; set; }
        public MetTuple VPINS { get; set; }
    }
}
