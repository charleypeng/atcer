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
    public class WindDto:BaseMetDto
    {
        public MetTuple? WSINS { get; set; }
        public MetTuple? WDINS { get; set; }
        public MetTuple? WS2A { get; set; }
        public MetTuple? WS2M { get; set; }
        public MetTuple? WS2X { get; set; }
        public MetTuple? WD2A { get; set; }
        public MetTuple? WD2M { get; set; }
        public MetTuple? WD2X { get; set; }
        public MetTuple? WS10A { get; set; }
        public MetTuple? WS10M { get; set; }
        public MetTuple? WS10X { get; set; }
        public MetTuple? WD10A { get; set; }
        public MetTuple? WD10M { get; set; }
        public MetTuple? WD10X { get; set; }
        public MetTuple? HW2A { get; set; }
        public MetTuple? CW2A { get; set; }
        /// <summary>
        /// 侧风风速
        /// <para>
        /// 单位：kn
        /// </para>
        /// </summary>
        public MetTuple<string>? CW2A_KT_STR { get; set; }
        /// <summary>
        /// 测风风速
        /// <para>单位：mps</para>
        /// </summary>
        public MetTuple<string>? CW2A_MPS_STR { get; set; }
    }
}
