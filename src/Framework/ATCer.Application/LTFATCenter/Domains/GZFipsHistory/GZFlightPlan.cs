using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using ATCer.ElasticSearch;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATCer.LTFATCenter.Domains.GZFips
{
    /// <summary>
    /// 管综FIPS
    /// </summary>
    [Table("FlightPlanHistory")]
    public class GZFlightPlanHist : IEntity<FipsHistoryDbContextLocator>
    {
        [Key]
        public int FlightPlanID { get; set; }
        [MaxLength(32)]
        public string GUFI { get; set; }
        [MaxLength(8)]
        public string Callsign { get; set; }
        [MaxLength(5)]
        [Column(TypeName = "char")]
        public string SsrCode { get; set; }
        [MaxLength(1)]
        [Column(TypeName = "char")]
        public string FlightRules { get; set; }
        [MaxLength(3)]
        [Column(TypeName = "char")]
        public string FlightType { get; set; }
        public int AircraftNumber { get; set; }
        [MaxLength(4)]
        public string AircraftType { get; set; }
        [MaxLength(1)]
        [Column(TypeName = "char")]
        public string WTC { get; set; }
        [MaxLength(50)]
        public string AidEquipment { get; set; }
        [MaxLength(50)]
        public string SurEquipment { get; set; }
        [MaxLength(4)]
        [Column(TypeName = "char")]
        public string ADEP { get; set; }
        [MaxLength(4)]
        [Column(TypeName = "char")]
        public string ADES { get; set; }
        [MaxLength(4)]
        [Column(TypeName = "char")]
        public string ADAR { get; set; }
        [MaxLength(4)]
        [Column(TypeName = "char")]
        public string ALTN1 { get; set; }
        [MaxLength(4)]
        [Column(TypeName = "char")]
        public string ALTN2 { get; set; }
        [MaxLength(4)]
        [Column(TypeName = "char")]
        public string TotalEET { get; set; }
        [MaxLength(800)]
        public string FiledRoute { get; set; }
        [MaxLength(1000)]
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
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        public string ApprovedRoute { get; set; }
        [MaxLength(200)]
        public string AirwayCodes { get; set; }
        [MaxLength(20)]
        public string RegMarking { get; set; }
        [MaxLength(8)]
        public string ApprovedArcType { get; set; }
        [MaxLength(3)]
        [Column(TypeName = "char")]
        public string Jurisdiction { get; set; }
        [MaxLength(3)]
        [Column(TypeName = "char")]
        public string PreFlightState { get; set; }
        [MaxLength(3)]
        [Column(TypeName = "char")]
        public string FlightState { get; set; }
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        public string Remarks { get; set; }
        public bool FiledFlag { get; set; }
        public bool AbolishFlag { get; set; }
        public bool DeleteFlag { get; set; }
        [MaxLength(20)]
        [Column(TypeName = "nvarchar(1000)")]
        public string ProcessUser { get; set; }
        public DateTime CreateTime { get; set; }
        [MaxLength(3)]
        [Column(TypeName = "char")]
        public string CreateMode { get; set; }
        [MaxLength(20)]
        public string ReferSourceID { get; set; }
        [MaxLength(200)]
        public string ControlArea { get; set; }
        public DateTime? UpdateTime { get; set; }
        [MaxLength(3)]
        public string UpdateMode { get; set; }
    }
}
