using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ATCer.LTFATCenter.Domains.GZFips
{
    [Table("FlightDepInfoHistory")]
    public class GZFlightDepInfoHist: IEntity<FipsHistoryDbContextLocator>
    {
        [Key]
        public int FDID { get; set; }
        public int FlightPlanID { get; set; }
        public string Callsign { get; set; }
        public string ADEP { get; set; }
        public string ADES { get; set; }
        public DateTime EOBT { get; set; }
        public DateTime? COBT { get; set; }
        public DateTime? CTOT { get; set; }
        public DateTime? TOBT { get; set; }
        public DateTime? TTOT { get; set; }
        public DateTime? ASAT { get; set; }
        public DateTime? SlotTime { get; set; }
        public DateTime? TAKT { get; set; }
        public string PreSurState { get; set; }
        public string SurfaceState { get; set; }
        public DateTime? StateTime { get; set; }
        public string Stand { get; set; }
        public string Taxiway { get; set; }
        public string Runway { get; set; }
        public string SID { get; set; }
        public bool? IsFirstTrip { get; set; }
    }
}
