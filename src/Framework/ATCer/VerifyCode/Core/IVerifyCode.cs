// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.VerifyCode.Dtos;
using System.Threading.Tasks;

namespace ATCer.VerifyCode.Core
{
    /// <summary>
    /// 验证码基础服务
    /// </summary>
    public interface IVerifyCode
    {
        /// <summary>
        /// 创建校验码
        /// </summary>
        /// <param name="input">验证码类型</param>
        /// <returns></returns>
        public Task<VerifyCodeOutput> Create(VerifyCodeInput input);
        /// <summary>
        /// 验证校验码
        /// </summary>
        /// <returns></returns>
        Task<bool> Verify(string key, string code);

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> Remove(string key);
    }
}
