// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ATCer.NotificationSystem.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class JwtUserIdProvider : IUserIdProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.GetHttpContext().User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
