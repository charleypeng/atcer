// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DynamicApiController;
using ATCer.Attributes;
using ATCer.VerifyCode.Core;
using ATCer.VerifyCode.Dtos;
using ATCer.VerifyCode.Enums;
using ATCer.VerifyCode.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ATCer.ImageVerifyCode.Services
{
    /// <summary>
    /// 图片验证码服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class ImageVerifyCodeService : IImageVerifyCodeService, IDynamicApiController
    {
        private readonly IVerifyCode verifyCodeService;
        /// <summary>
        /// 验证码服务
        /// </summary>
        /// <param name="verifyCodeServiceProvider"></param>
        public ImageVerifyCodeService(Func<VerifyCodeTypeEnum, IVerifyCode> verifyCodeServiceProvider)
        {
            this.verifyCodeService = verifyCodeServiceProvider(VerifyCodeTypeEnum.Image);
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="input">类型</param>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        public async Task<ImageVerifyCodeOutput> Create(ImageVerifyCodeInput input)
        {
            ImageVerifyCodeOutput imageVerifyCode =(ImageVerifyCodeOutput) await verifyCodeService.Create(input);
            return imageVerifyCode;
        }
        /// <summary>
        /// 移除验证码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        public async Task<bool> Remove(string key)
        {
            return await verifyCodeService.Remove(key);
        }

        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        public async Task<bool> Verify(ImageVerifyCodeCheckInput input)
        {
            return await verifyCodeService.Verify(input.VerifyCode, input.VerifyCodeKey);
        }
    }
}
