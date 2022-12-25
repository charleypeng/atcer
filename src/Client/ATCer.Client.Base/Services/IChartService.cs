// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer
{
    public interface IChartService
    {
        Task<object> GetData();
        Task<object> GetData(DateTime begin, DateTime end);
    }
}
