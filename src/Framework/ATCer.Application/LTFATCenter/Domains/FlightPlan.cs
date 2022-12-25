// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace ATCer.LTFATCenter.Domains
{
    public class FlightPlan : GardenerEntityBase<long>, IEntity<MasterDbContextLocator, ATCerSlaveDbContextLocator, SugarTestLocator>
    {
        [MaxLength(32)]
        public string GUFI { get; set; }
        [MaxLength(8)]
        public string Callsign { get; set; }
        [MaxLength(5)]
        public string SsrCode { get; set; }
        [MaxLength(1)]
        public string FlightRules { get; set; }
        [MaxLength(3)]
        public string FlightType { get; set; }
        public int AircraftNumber { get; set; }
        [MaxLength(4)]
        public string AircraftType { get; set; }
        public WakeTurbulence WTC { get; set; }
        [MaxLength(50)]
        public string AidEquipment { get; set; }
        [MaxLength(50)]
        public string SurEquipment { get; set; }
        [MaxLength(4)]
        public string ADEP { get; set; }
        [MaxLength(4)]
        public string ADES { get; set; }
        [MaxLength(4)]
        public string ADAR { get; set; }
        [MaxLength(4)]
        public string ALTN1 { get; set; }
        [MaxLength(4)]
        public string ALTN2 { get; set; }
        [MaxLength(4)]
        public string TotalEET { get; set; }
        [MaxLength(800)]
        public string FiledRoute { get; set; }
        [MaxLength(1000)]
        public string OtherInfo { get; set; }
        public DateTimeOffset EOBT { get; set; }
        public DateTimeOffset? ETOT { get; set; }
        public DateTimeOffset? ELDT { get; set; }
        public DateTimeOffset? EIBT { get; set; }
        public DateTimeOffset? AOBT { get; set; }
        public DateTimeOffset? ATOT { get; set; }
        public DateTimeOffset? ALDT { get; set; }
        public DateTimeOffset? AIBT { get; set; }
        public DateTimeOffset? SOBT { get; set; }
        public DateTimeOffset? SIBT { get; set; }
        public DateTimeOffset? IOBT { get; set; }
        [MaxLength(1000)]
        public string ApprovedRoute { get; set; }
        [MaxLength(200)]
        public string AirwayCodes { get; set; }
        [MaxLength(20)]
        public string RegMarking { get; set; }
        [MaxLength(8)]
        public string ApprovedArcType { get; set; }
        [MaxLength(3)]
        public string Jurisdiction { get; set; }
        public PlanStatus PrePlanState { get; set; }
        public PlanStatus PlanState { get; set; }
        [MaxLength(1000)]
        public string Remarks { get; set; }
        public bool FiledFlag { get; set; }
        public bool AbolishFlag { get; set; }
        public bool DeleteFlag { get; set; }
        [MaxLength(20)]
        public string ProcessUser { get; set; }
        [MaxLength(3)]
        public string CreateMode { get; set; }
        [MaxLength(20)]
        public string ReferSourceID { get; set; }
        [MaxLength(200)]
        public string ControlArea { get; set; }
        [MaxLength(3)]
        public string UpdateMode { get; set; }
    }
}
