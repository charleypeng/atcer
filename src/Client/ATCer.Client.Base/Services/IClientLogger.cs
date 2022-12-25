// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;

namespace ATCer.Client.Base
{
    public interface IClientLogger
    {
        void Debug(string msg, int? code = null, Exception ex = null);
        void Fatal(string msg, int? code = null, Exception ex = null, bool sendNotify = true);
        void Error(string msg, int? code = null, Exception ex = null, bool sendNotify = true);
        void Info(string msg, int? code = null, Exception ex = null, bool sendNotify = false);
        void Warn(string msg, int? code = null, Exception ex = null, bool sendNotify = true);
    }
}
