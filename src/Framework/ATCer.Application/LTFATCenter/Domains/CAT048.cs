// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//
//  2021  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Nest;

namespace ATCer.LTFATCenter.Domains
{
    public class BDS60
    {
        public double MagneticHeading { get; set; }
        public int IndicatedAirspeed { get; set; }
        public double MachNumber { get; set; }
        public int BarometricAltitudeRate { get; set; }
        public int InertialAltitudeRate { get; set; }
    }

    public class BDS40
    {
        public double MCPorFCUSelectAltitude_m { get; set; }
        public double FMSSelectAltitude_m { get; set; }
        public double BarometricPressureSet_mb { get; set; }
    }

    public class BDS50
    {
        public double RollAngle { get; set; }
        public double TrueTrackAngle { get; set; }
        public int GroundSpeed { get; set; }
        public int TrackAngleRate { get; set; }
    }

    public class ModeSMBData
    {
        public BDS60 BDS60 { get; set; }
        public BDS40 BDS40 { get; set; }
        public BDS50 BDS50 { get; set; }
        public string BDS30 { get; set; }
        public string BDS { get; set; }
    }

    public class VelocityInPolar
    {
        public double GroundSpeed_kt { get; set; }
        public double Heading_degree { get; set; }
    }

    public class CAT048:ElasticSearch.BaseElasticEntity<string>
    {
        public string SAC_SIC { get; set; }
        public string SIM { get; set; }
        public string TST { get; set; }
        public string Mode3ACode { get; set; }
        public string FlightLevel_m { get; set; }
        public string AircraftAdress { get; set; }
        public string CallSign { get; set; }
        public ModeSMBData ModeSMBData { get; set; }
        public VelocityInPolar VelocityInPolar { get; set; }
    }
}
