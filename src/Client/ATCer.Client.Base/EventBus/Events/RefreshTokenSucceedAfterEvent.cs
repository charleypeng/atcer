// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authorization.Dtos;
using ATCer.EventBus;

namespace ATCer.Client.Base.EventBus.Events
{
    /// <summary>
    /// 刷新token成功后
    /// </summary>
    public class RefreshTokenSucceedAfterEvent:EventBase
    {
        /// <summary>
        /// 登录token
        /// </summary>
        public TokenOutput Token { get; set; }
    }
}
