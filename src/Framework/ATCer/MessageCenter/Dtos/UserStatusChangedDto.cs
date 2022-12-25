// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.MessageQueue.Dtos;
using ATCer.MessageQueue.Enums;
// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------


namespace ATCer.MessageCenter.Dtos
{
    /// <summary>
    /// 用户状态更改Dto
    /// </summary>
    public class UserStatusChangedDto : MQDataBase
    {
        /// <summary>
        /// 在线状态
        /// </summary>
        public UserOnlineStatus OnlineStatus { get; set; }
    }
}
