// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------


namespace ATCer.Client.Services
{
    public abstract class BaseChartService : ClientServiceBase<object>, IChartService
    {
        public BaseChartService(IApiCaller apiCaller, string controller) : base(apiCaller, controller) { }

        public Task<object> GetData()
        {
            throw new NotImplementedException();
        }

        public Task<object> GetData(DateTime begin, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
