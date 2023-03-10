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
using ATCer.DataCenter.Enums;

namespace ATCer.Application.DataCenter.Domains.MetData
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class CW2A_v1
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }


    public class CW2AKTSTR
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

    public class CW2AMPSSTR
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

///
//
    public class DATA
    {   
        ///<summary>
        /// 气象一数据
        ///</summary>
        public Tuple<MetDataStatus, string?>? CW2A{get;set;}
        public WSINS WSINS { get; set; }
        public WDINS WDINS { get; set; }
        public WS2A WS2A { get; set; }
        public WS2M WS2M { get; set; }
        public WS2X WS2X { get; set; }
        public WD2A WD2A { get; set; }
        public WD2M WD2M { get; set; }
        public WD2X WD2X { get; set; }
        public WS10A WS10A { get; set; }
        public WS10M WS10M { get; set; }
        public WS10X WS10X { get; set; }
        public WD10A WD10A { get; set; }
        public WD10M WD10M { get; set; }
        public WD10X WD10X { get; set; }
        public HW2A HW2A { get; set; }
        //public CW2A CW2A { get; set; }
        public CW2AKTSTR CW2A_KT_STR { get; set; }
        public CW2AMPSSTR CW2A_MPS_STR { get; set; }
    }

    public class HW2A
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

    public class Root
    {
        public Value Value { get; set; }
    }

    public class Value
    {
        public string TYPE { get; set; }
        public string LOC { get; set; }
        public DATA DATA { get; set; }
        public int TIME { get; set; }
    }

    public class WD10A
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

    public class WD10M
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

    public class WD10X
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

    public class WD2A
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

    public class WD2M
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

    public class WD2X
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

    public class WDINS
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

    public class WS10A
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

    public class WS10M
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

    public class WS10X
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

    public class WS2A
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

    public class WS2M
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

    public class WS2X
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }

    public class WSINS
    {
        public string D_TYPE { get; set; }
        public string STATUS { get; set; }
        public string VALUE { get; set; }
    }
}
