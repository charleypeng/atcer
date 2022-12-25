// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.HRCenter.Dtos;

namespace ATCer.HRCenter.Services
{
    /// <summary>
    /// 执勤小时统计服务
    /// </summary>
    public interface IWorkStats
    {
        /// <summary>
        /// 执勤小时综合统计
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<WorkTimeConfDto> GetWorkTimeStats(DateTime? begin, DateTime? end);
        /// <summary>
        /// 用户工作统计
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<WorkTimeConfDto> GetUserWorkTimeStats(int userId);
        /// <summary>
        /// 排行榜
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<object> GetRank(DateTime? begin, DateTime? end);
    }
}
