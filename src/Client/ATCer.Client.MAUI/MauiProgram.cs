// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ATCer.Client.Base;
using ATCer.Client.Core;
using ATCer.Client.MAUI.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using AntDesign.ProLayout;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Reflection;
using ATCer.NotificationSystem.Client.Core;

namespace ATCer.Client.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
           
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            #region 加载 Appsettings
            var a = typeof(App).GetTypeInfo().Assembly;
            var name = a.GetName().Name;
            using var stream = FileSystem.OpenAppPackageFileAsync($"appsettings.json").Result;
            var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();
            builder.Configuration.AddConfiguration(config);
            #endregion

            #region api settings
            builder.AddApiSetting();
            #endregion

            #region httpclient
            builder.Services.AddScoped(sp => {
                IOptions<ApiSettings> settings = sp.GetService<IOptions<ApiSettings>>();
                return new HttpClient { BaseAddress = new Uri(settings.Value.BaseAddres) };
            });
            #endregion

            #region log
            builder.Logging.AddConfiguration(
                builder.Configuration.GetSection("Logging")
            );
            #endregion

            #region ant design
            builder.Services.AddAntDesign();
            builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));
            #endregion

            #region 认证、授权
            builder.Services.AddScoped<IAuthenticationStateManager, AuthenticationStateManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            builder.Services.AddAuthorizationCore(option =>
            {
                option.AddPolicy(AuthConstant.DefaultAuthenticatedPolicy, a => a.RequireAuthenticatedUser());
                option.AddPolicy(AuthConstant.ClientUIResourcePolicy, a => a.Requirements.Add(new ClientUIAuthorizationRequirement()));
                option.DefaultPolicy = option.GetPolicy(AuthConstant.DefaultAuthenticatedPolicy);
            });
            builder.Services.AddScoped<IAuthorizationHandler, ClientUIResourceAuthorizationHandler>();
            builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));
            #endregion

            #region 本地化
            builder.Services.AddLocalization();
            builder.Services.AddCulture<Main>();
            #endregion

            #region module
            builder.AddModuleLoader();
            #endregion

            #region services

            builder.Services.AddServicesWithAttributeOfTypeFromModuleContextMAUI(new[] { typeof(App).Assembly });

            builder.Services.AddScoped(typeof(HttpClientManager));
            builder.Services.AddScoped(typeof(SystemNotificationSender));
            #endregion

            #region  Mapster 配置
            builder.Services.AddTypeAdapterConfigs();
            #endregion

            #region  SignalR
            builder.AddSignalRClientManager();
            #endregion

            builder.Services.AddMauiServices();
            builder.Services.AddATCerComponents();
            builder.Services.AddMauiBlazorWebView();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif
            return builder.Build();
        }
    }
}