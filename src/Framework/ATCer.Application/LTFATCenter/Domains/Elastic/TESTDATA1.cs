//// -----------------------------------------------------------------------------
//// ATCer 全平台综合性空中交通管理系统
////  作者：彭磊  
////  CopyRight(C) 2023  版权所有 
//// -----------------------------------------------------------------------------

//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ATCer.Application.LTFATCenter.Domains.Elastic
//{
//    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
//    public class Item
//    {
//        public I010 I010 { get; set; }
//        public I020 I020 { get; set; }
//        public I161 I161 { get; set; }
//        public I040 I040 { get; set; }
//        public I200 I200 { get; set; }
//        public I070 I070 { get; set; }
//        public I090 I090 { get; set; }
//        public I170 I170 { get; set; }
//    }

//    public class I010
//    {
//        public string SacSic { get; set; }
//    }

//    public class I020
//    {
//        public int TYP { get; set; }
//        public int SIM { get; set; }
//        public int SSRPSR { get; set; }
//        public int ANT { get; set; }
//        public int SPI { get; set; }
//        public int RAB { get; set; }
//        public int FX { get; set; }
//    }

//    public class I040
//    {
//        public double RHO { get; set; }
//        public double THETA { get; set; }
//    }

//    public class I070
//    {
//        public int V { get; set; }
//        public int G { get; set; }
//        public int L { get; set; }
//        public int spare { get; set; }
//        public string Mode3A { get; set; }
//    }

//    public class I090
//    {
//        public int V { get; set; }
//        public int G { get; set; }
//        public double ModeC { get; set; }
//    }

//    public class I161
//    {
//        public int tpn { get; set; }
//    }

//    public class I170
//    {
//        public int CON { get; set; }
//        public int RAD { get; set; }
//        public int MAN { get; set; }
//        public int DOU { get; set; }
//        public int RDPC { get; set; }
//        public int spare { get; set; }
//        public int GHO { get; set; }
//        public int FX { get; set; }
//    }

//    public class I200
//    {
//        public double CalcGS { get; set; }
//        public double CalcHdg { get; set; }
//    }

//    public class Ca
//    {
//        [JsonProperty("1")]
//        public Item Item { get; set; }
//    }
//    internal class TESTDATA1
//    {
//    }
//}
