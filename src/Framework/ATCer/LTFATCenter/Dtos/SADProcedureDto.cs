// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.LTFATCenter.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.LTFATCenter.Dtos
{
    public class SADProcedureDto
    {
        public int Id { get; set; }
        public ProcedureMethod Method { get; set; }
        [MaxLength(10)]
        public string Name { get; set; }
        public SADPDirection Direction { get; set; }
        [MaxLength(4)]
        public string PrefixCode { get; set; }
        [MaxLength(50)]
        public string Remarks { get; set; }
    }
}
