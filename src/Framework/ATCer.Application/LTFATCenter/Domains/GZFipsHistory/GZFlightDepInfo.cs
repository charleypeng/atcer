using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ATCer.LTFATCenter.Domains.GZFips
{
    // <summary>
    // 用于跟中南FIPS交互的Model
    // </summary>
    [Table("FlightArrInfoHistory")]
    public class GZFlightArrInfoHist : IEntity<FipsHistoryDbContextLocator>
    {
        [Key]
        public int FAID { get; set; }
        public int FlightPlanID { get; set; }
        public string Callsign { get; set; }
        public string ADEP { get; set; }
        public string ADES { get; set; }
        public DateTime EOBT { get; set; }
        public DateTime? TCHT { get; set; }
        public string PreSurState { get; set; }
        public string SurfaceState { get; set; }
        public DateTime? StateTime { get; set; }
        public string STAR { get; set; }
        public string Runway { get; set; }
        public string Taxiway { get; set; }
        public string Stand { get; set; }
    }
}
