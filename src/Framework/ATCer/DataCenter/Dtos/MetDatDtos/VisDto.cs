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
    public class VisDto:BaseMetDto
    {
        public MetTuple? RawRVR { get; set; }
        public MetTuple? RVR { get; set; }
        public MetTuple? RVR1A { get; set; }
        public MetTuple? RVR1A10M { get; set; }
        public MetTuple? RVR1A10X { get; set; }
        public MetTuple? RVR2A { get; set; }
        public MetTuple? RVR10A { get; set; }
        public MetTuple? RVR10M { get; set; }
        public MetTuple? RVR10X { get; set; }
        public MetTuple? RVR10T { get; set; }
        public MetTuple? RawRVR_METAR { get; set; }
        public MetTuple? RVR_METAR { get; set; }
        public MetTuple? RVR1A_METAR { get; set; }
        public MetTuple? RVR1A10M_METAR { get; set; }
        public MetTuple? RVR1A10X_METAR { get; set; }
        public MetTuple? RVR2A_METAR { get; set; }
        public MetTuple? RVR10A_METAR { get; set; }
        public MetTuple? RVR10M_METAR { get; set; }
        public MetTuple? RVR10X_METAR { get; set; }
        public MetTuple<string>? RVR10T_METAR { get; set; }
        public MetTuple? RawMOR { get; set; }
        public MetTuple? MOR { get; set; }
        public MetTuple? MOR1A { get; set; }
        public MetTuple? MOR2A { get; set; }
        public MetTuple? MOR10A { get; set; }
        public MetTuple? MOR10M { get; set; }
        public MetTuple? MOR10X { get; set; }
        public MetTuple? VIS1K { get; set; }
        public MetTuple? RawVIS { get; set; }
        /// <summary>
        /// 瞬时能见度
        /// </summary>
        /// <remarks>由于类名称重复，在原定义中名称为VIS</remarks>
        [DataName("VIS")]
        public MetTuple? VIS_Instant { get; set; }
        public MetTuple? VIS1A { get; set; }
        public MetTuple? VIS2A { get; set; }
        public MetTuple? VIS10A { get; set; }
        public MetTuple? VIS10M { get; set; }
        public MetTuple? VIS10X { get; set; }
        public MetTuple? BL { get; set; }
        public MetTuple? BL1A { get; set; }
        public MetTuple? BL2A { get; set; }
        public MetTuple? LIGHTS { get; set; }
        public MetTuple? EDGELIGHTS { get; set; }
        public MetTuple? CENTERLIGHTS { get; set; }
    }
}
