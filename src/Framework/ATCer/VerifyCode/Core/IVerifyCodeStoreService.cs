// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.VerifyCode.Enums;
using System;
using System.Threading.Tasks;

namespace ATCer.VerifyCode.Core
{
    /// <summary>
    /// 图片验证码存储
    /// </summary>
    public interface IVerifyCodeStoreService
    {
        /// <summary>
        /// 保存校验码
        /// </summary>
        /// <param name="verifyCodeType"></param>
        /// <param name="key"></param>
        /// <param name="code"></param>
        /// <param name="expire"></param>
        Task Add(VerifyCodeTypeEnum verifyCodeType, string key, string code, TimeSpan expire);

        /// <summary>
        /// 获取校验码
        /// </summary>
        /// <param name="verifyCodeType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> GetCode(VerifyCodeTypeEnum verifyCodeType, string key);

        /// <summary>
        /// 移除校验码
        /// </summary>
        /// <param name="verifyCodeType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task Remove(VerifyCodeTypeEnum verifyCodeType, string key);
    }
}
