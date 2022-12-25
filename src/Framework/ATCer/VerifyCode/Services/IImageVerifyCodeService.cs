// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.VerifyCode.Dtos;
using System.Threading.Tasks;

namespace ATCer.VerifyCode.Services
{
    /// <summary>
    /// 图片验证码服务
    /// </summary>
    public interface IImageVerifyCodeService
    {
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<ImageVerifyCodeOutput> Create(ImageVerifyCodeInput input);

        /// <summary>
        /// 移除验证码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> Remove(string key);

        /// <summary>
        /// 验证图片验证码
        /// </summary>
        /// <param name="verifyCodeInput"></param>
        /// <returns></returns>
        Task<bool> Verify(ImageVerifyCodeCheckInput verifyCodeInput);
    }
}
