// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ATCer.LTFATCenter.Dtos.GZFips
{
    public class GZFlightPlanDto
    {
        public int FlightPlanID { get; set; }
        public string GUFI { get; set; }
        public string Callsign { get; set; }
        public string SsrCode { get; set; }
        public string FlightRules { get; set; }
        public string FlightType { get; set; }
        public int AircraftNumber { get; set; }
        public string AircraftType { get; set; }
        public string WTC { get; set; }
        public string AidEquipment { get; set; }
        public string SurEquipment { get; set; }
        public string ADEP { get; set; }
        public string ADES { get; set; }
        public string ADAR { get; set; }
        public string ALTN1 { get; set; }
        public string ALTN2 { get; set; }
        public string TotalEET { get; set; }
        public string FiledRoute { get; set; }
        public string OtherInfo { get; set; }
        public DateTime EOBT { get; set; }
        public DateTime? ETOT { get; set; } 
        public DateTime? ELDT { get; set; }
        public DateTime? EIBT { get; set; }
        public DateTime? AOBT { get; set; }
        public DateTime? ATOT { get; set; }
        public DateTime? ALDT { get; set; }
        public DateTime? AIBT { get; set; }
        public DateTime? SOBT { get; set; }
        public DateTime? SIBT { get; set; }
        public DateTime? IOBT { get; set; }
        public string ApprovedRoute { get; set; }
        public string AirwayCodes { get; set; }
        public string RegMarking { get; set; }
        public string ApprovedArcType { get; set; }
        public string Jurisdiction { get; set; }
        public string PreFlightState { get; set; }
        public string FlightState { get; set; }
        public string Remarks { get; set; }
        public bool FiledFlag { get; set; }
        public bool AbolishFlag { get; set; }
        public bool DeleteFlag { get; set; }
        public string ProcessUser { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateMode { get; set; }
        public string ReferSourceID { get; set; }
        public string ControlArea { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string UpdateMode { get; set; }
    }
}
