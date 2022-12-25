// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;

namespace ATCer.VerifyCode.Core
{
    /// <summary>
    /// 验证码自动验证
    /// 但是要保证参数要继承<see cref="ATCer.VerifyCode.Dtos.VerifyCodeCheckInput"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class VerifyCodeAutoVerificationAttribute : Attribute
    {

    }
}
