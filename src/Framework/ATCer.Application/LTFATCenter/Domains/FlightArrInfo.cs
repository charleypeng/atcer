// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.LTFATCenter.Domains
{
    public class FlightArrInfo:ATCerEntityBase<long>
    {
        public int FlightPlanID { get; set; }
        public string Callsign { get; set; }
        public string ADEP { get; set; }
        public string ADES { get; set; }
        public DateTimeOffset EOBT { get; set; }
        public DateTimeOffset? TCHT { get; set; }
        public FlightStatus PreSurState { get; set; }
        public FlightStatus SurfaceState { get; set; }
        public DateTimeOffset? StateTime { get; set; }
        public string STAR { get; set; }
        public string Runway { get; set; }
        public string Taxiway { get; set; }
        public string Stand { get; set; }
    }
}
