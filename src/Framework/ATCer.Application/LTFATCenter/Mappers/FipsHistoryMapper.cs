// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.LTFATCenter.Domains.GZFips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.LTFATCenter.Impl.Mappers
{
    /// <summary>
    /// 历史飞行计划映射规则
    /// </summary>
    public class FipsHistoryMapper : IRegister
    {
        /// <summary>
        /// Register
        /// </summary>
        /// <param name="config"></param>
        public void Register(TypeAdapterConfig config)
        {
            //for fips history
            config.ForType<GZFlightPlanHist, FlightPlan>()
                  .Map(src => src.PlanState, des => des.FlightState.ToPlanStatus())
                  .Map(src => src.WTC, des => des.WTC.ToWakeTurbulence())
                  .Map(src => src.PrePlanState, des => des.PreFlightState.ToPlanStatus())
                  .Map(src => src.Id, des => des.FlightPlanID)
                  .Map(src => src.UpdatedTime, des => des.UpdateTime)
                  .Map(src => src.CreatedTime, des => des.CreateTime);

            config.ForType<GZFlightArrInfoHist, FlightArrInfo>()
                 .Map(src => src.SurfaceState, des => des.SurfaceState.ToFlightStatus())
                 .Map(src => src.PreSurState, des => des.PreSurState.ToFlightStatus())
                 .Map(src => src.Id, des => des.FlightPlanID);

            config.ForType<GZFlightDepInfoHist, FlightDepInfo>()
                  .Map(src => src.SurfaceState, des => des.SurfaceState.ToFlightStatus())
                  .Map(src => src.PreSurState, des => des.PreSurState.ToFlightStatus())
                  .Map(src => src.Id, des => des.FlightPlanID);
        }
    }
}
