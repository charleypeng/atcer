// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.MessageCenter.Dtos;

namespace ATCer.MessageCenter.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserStatusNotifyService:ATCer.Base.IServiceBase<UserStatusNotifyDto, long>
    {
        /// <summary>
        /// 删除所有状态
        /// </summary>
        /// <returns></returns>
        Task ClearAllStatus();
    }
}
