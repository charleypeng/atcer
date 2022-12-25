// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.LTFATCenter.Dtos;
using ATCer.LTFATCenter.Services;

namespace ATCer.LTFATCenter.Client.Services
{
    [ScopedService]
    public class FlightPlanService : ClientServiceBase<FlightPlanDto, long>, IFlightPlanService
    {
        public FlightPlanService(IApiCaller apiCaller) : base(apiCaller, "flight-plan")
        {

        }

        public Task<bool> UpdatePlansByDate(DateTime begin, DateTime end)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePlansOfToday()
        {
            throw new NotImplementedException();
        }
    }
}
