// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.ElasticSearch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.Application.LTFATCenter.Domains
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Item
    {
        public I010 I010 { get; set; }
        public I140 I140 { get; set; }
        public I020 I020 { get; set; }
        public I040 I040 { get; set; }
        public I070 I070 { get; set; }
        public I090 I090 { get; set; }
        public I220 I220 { get; set; }
        public I240 I240 { get; set; }
        public I250 I250 { get; set; }
        public I161 I161 { get; set; }
        public I042 I042 { get; set; }
        public I200 I200 { get; set; }
        public I170 I170 { get; set; }
        public I230 I230 { get; set; }
    }

    public class BDS60
    {
        public double MH { get; set; }
        public int IA { get; set; }
        public double MN { get; set; }
        public int BAR { get; set; }
        public int IAR { get; set; }
    }

    public class I010
    {
        public string SacSic { get; set; }
    }

    public class I020
    {
        public int TYP { get; set; }
        public int SIM { get; set; }
        public int RDP { get; set; }
        public int SPI { get; set; }
        public int RAB { get; set; }
        public int FX { get; set; }
    }

    public class I040
    {
        public double RHO { get; set; }
        public double THETA { get; set; }
    }

    public class I042
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class I070
    {
        public int V { get; set; }
        public int G { get; set; }
        public int L { get; set; }
        public int spare { get; set; }
        public string Mode3A { get; set; }
    }

    public class I090
    {
        public int V { get; set; }
        public int G { get; set; }
        public double FL_m { get; set; }
    }

    public class I140
    {
        public double ToD { get; set; }
    }

    public class I161
    {
        public int Tn { get; set; }
    }

    public class I170
    {
        public int CNF { get; set; }
        public int RAD { get; set; }
        public int DOU { get; set; }
        public int MAH { get; set; }
        public int CDM { get; set; }
        public int FX { get; set; }
    }

    public class I200
    {
        public double CGS { get; set; }
        public double CHdg { get; set; }
    }

    public class I220
    {
        public string ACAddr { get; set; }
    }

    public class I230
    {
        public int COM { get; set; }
        public int STAT { get; set; }
        public int SI { get; set; }
        public int spare { get; set; }
        public int ModSMssc { get; set; }
        public int ARC { get; set; }
        public int AIC { get; set; }
        public int B1A { get; set; }
        public int B1B { get; set; }
    }

    public class I240
    {
        public string TId { get; set; }
    }

    public class I250
    {
        public REP1 REP1 { get; set; }
        public REP2 REP2 { get; set; }
    }

    public class MBdata
    {
        public BDS60 BDS60 { get; set; }
        public string BDS10 { get; set; }
    }

    public class REP1
    {
        public MBdata MBdata { get; set; }
    }

    public class REP2
    {
        public MBdata MBdata { get; set; }
    }

    public class CATItem1:BaseElasticEntity<string>
    {
        [JsonProperty("1")]
        public Item Item { get; set; }
    }


}
