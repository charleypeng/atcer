// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion;
using ATCer.ImageVerifyCode.Core;
using ATCer.ImageVerifyCode.Services;
using ATCer.VerifyCode.CacheStore;
using ATCer.VerifyCode.Core;
using ATCer.VerifyCode.Core.Settings;
using ATCer.VerifyCode.DbStore;
using ATCer.VerifyCode.Enums;
using ATCer.VerifyCode.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 验证码
    /// </summary>
    public static class VerifyCodeExtensions
    {
        /// <summary>
        /// 添加验证码服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="enableAutoVerification">是否启用自动验证</param>
        /// <returns></returns>
        public static IServiceCollection AddVerifyCode<TVerifyCodeStoreService>(this IServiceCollection services, bool enableAutoVerification = true) where TVerifyCodeStoreService : class, IVerifyCodeStoreService
        {
            if (enableAutoVerification)
            {
                services.Configure<MvcOptions>(options =>
                {
                    //自动验证
                    options.Filters.Add<VerifyCodeAutoVerificationFilter>();
                });
            }
            //图片验证码
            services.AddScoped<ImageVerifyCode>();
            services.AddScoped<IImageVerifyCodeService, ImageVerifyCodeService>();
            //图片验证码配置
            services.AddConfigurableOptions<ImageVerifyCodeOptions>();
             //邮件验证码
            services.AddScoped<EmailVerifyCode>();
            services.AddScoped<IEmailVerifyCodeService, EmailVerifyCodeService>();
            //邮件验证码配置
            services.AddConfigurableOptions<EmailVerifyCodeOptions>();
            //验证码存储实现
            services.AddScoped<IVerifyCodeStoreService, TVerifyCodeStoreService>();
            //验证码服务提供器
            services.AddScoped(serviceProvider => {
                Func<VerifyCodeTypeEnum, IVerifyCode> accesor = key =>
                {
                    if (VerifyCodeTypeEnum.Image.Equals(key))
                        return serviceProvider.GetService<ImageVerifyCode>();
                    else if (VerifyCodeTypeEnum.Email.Equals(key))
                        return serviceProvider.GetService<EmailVerifyCode>();
                    else
                        throw new ArgumentException($"不支持的验证码类型: {key}");
                };
                return accesor;

            });
            return services;
        }

        /// <summary>
        /// 添加验证码服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="enableAutoVerification">是否启用自动验证</param>
        /// <returns></returns>
        public static IServiceCollection AddVerifyCode(this IServiceCollection services, bool enableAutoVerification = true)
        {
            string storeMode = App.Configuration["VerifyCodeStoreSetting"];
            if ("Cache".Equals(storeMode))
            { 
                services.AddVerifyCode<VerifyCodeCacheStoreService>();
            }else if ("DB".Equals(storeMode))
            {
                services.AddVerifyCode<VerifyCodeDbStoreService>();
            }
            return services;
        }
    }
}
