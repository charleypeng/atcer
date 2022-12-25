// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using ATCer.VerifyCode.Dtos;
using ATCer.VerifyCode.Services;
using System.Threading.Tasks;

namespace ATCer.VerifyCode.Client.Services
{
    [ScopedService]
    public class ImageVerifyCodeService : IImageVerifyCodeService
    {
        private readonly static string controller = "image-verify-code";
        private readonly IApiCaller apiCaller;
        public ImageVerifyCodeService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<ImageVerifyCodeOutput> Create(ImageVerifyCodeInput input)
        {
            return await apiCaller.PostAsync<VerifyCodeInput, ImageVerifyCodeOutput>($"{controller}", input);
        }

        public async Task<bool> Remove(string key)
        {
            return await apiCaller.DeleteAsync<bool>($"{controller}/{key}");
        }

        public async Task<bool> Verify(ImageVerifyCodeCheckInput verifyCodeInput)
        {
            return await apiCaller.PostAsync<ImageVerifyCodeCheckInput, bool>($"{controller}/verify", verifyCodeInput);
        }
    }
}
