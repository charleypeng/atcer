// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.EventBus;
using System.Net;

namespace ATCer.Client.Base
{
    /// <summary>
    /// 授权失败的api调用
    /// </summary>
    public class UnauthorizedApiCallEvent : EventBase
    {
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
