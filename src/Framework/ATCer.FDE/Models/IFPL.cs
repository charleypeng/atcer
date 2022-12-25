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
    /// 飞行计划数据
    /// </summary>
    public class IFPL:BaseFDE
    {
        public string FILTIM { get; set; }
        public string IFPLID { get; set; }
        public string SOURCE { get; set; }
        public string ARCID { get; set; }
        public string ADEP { get; set; }
        public string ADES { get; set; }
        public string EOBD { get; set; }
        public string EOBT { get; set; }
        public string SSRCODE { get; set; }
        public string CEQPT { get; set; }
        public List<PT> RTEPTS { get; set; }
    }
}
