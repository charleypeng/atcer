// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace ATCer.Client.Base
{
    public interface IClientNotifier
    {
        Task Error(string description, Exception ex = null);
        Task Error(string msg,string description, Exception ex = null);
        Task Info(string description);
        Task Info(string msg, string description);
        Task Success(string description);
        Task Success(string msg, string description);
        Task Warn(string description, Exception ex = null);
        Task Warn(string msg, string description, Exception ex = null);
    }
}