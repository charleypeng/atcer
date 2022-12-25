// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using ATCer.LTFATCenter.Dtos.GZFips;
using ATCer.LTFATCenter.Domains;

namespace ATCer.LTFATCenter.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class MapsterExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddFlightPlanAdapter(this IServiceCollection services)
        {
            //TypeAdapterConfig<GZFlightPlan, FlightPlan>
            //    .NewConfig()
            //    .BeforeMapping((src, des) =>
            //    {
            //        var data = Enums.FlightStatus.Unknown;
            //        var result = Enum.TryParse(src.FlightState, out data);
            //        if (result)
            //        {
            //            des.FlightState = data;
            //        }
            //        else
            //        {
            //            des.FlightState = Enums.FlightStatus.Unknown;
            //        }
            //    })
            //      .Map(src => src.Id, des => des.FlightPlanID);
            return services;
        }
    }
}
