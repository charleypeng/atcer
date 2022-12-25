// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows;
using ATCer.Client.WPF.Extensions;
using Microsoft.Extensions.Options;
using System.Net.Http;
using AntDesign.ProLayout;
using ATCer.Client.Core;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authorization;
using ATCer.NotificationSystem.Client.Core;

namespace ATCer.Client.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApiSettings>(Configuration.GetSection(nameof(ApiSettings)));
            services.PostConfigure<ApiSettings>(setting =>
            {
                ConfigureApiSettings(services, setting);
            });
            #region httpclient
            services.AddScoped(sp => {
                IOptions<ApiSettings> settings = sp.GetService<IOptions<ApiSettings>>();
                return new HttpClient { BaseAddress = new Uri(settings.Value.BaseAddres) };
            });
            #endregion

            #region ant design
            services.AddAntDesign();
            services.Configure<ProSettings>(Configuration.GetSection("ProSettings"));
            #endregion

            #region 认证、授权
            services.AddScoped<IAuthenticationStateManager, AuthenticationStateManager>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            services.AddAuthorizationCore(option =>
            {
                option.AddPolicy(AuthConstant.DefaultAuthenticatedPolicy, a => a.RequireAuthenticatedUser());
                option.AddPolicy(AuthConstant.ClientUIResourcePolicy, a => a.Requirements.Add(new ClientUIAuthorizationRequirement()));
                option.DefaultPolicy = option.GetPolicy(AuthConstant.DefaultAuthenticatedPolicy);
            });
            services.AddScoped<IAuthorizationHandler, ClientUIResourceAuthorizationHandler>();
            services.Configure<AuthSettings>(Configuration.GetSection("AuthSettings"));
            #endregion

            #region 本地化
            services.AddLocalization(option =>
            {
                option.ResourcesPath = "Resources";
            });
            services.AddCulture<App>();
            #endregion

            services.AddModuleLoader(Configuration);

            #region services
            services.AddServicesWithAttributeOfTypeFromModuleContextWPF(new[] { typeof(App).Assembly });
            services.AddScoped<IJsTool, JsTool>();
            services.AddScoped(typeof(HttpClientManager));
            services.AddScoped(typeof(SystemNotificationSender));
            #endregion

            #region  Mapster 配置
            services.AddTypeAdapterConfigs();
            #endregion

            #region  SignalR
            services.AddTransient<ISignalRClientBuilder, SignalRClientBuilder>();
            services.AddScoped<ISignalRClientManager, SignalRClientManager>();
            #endregion

            services.AddATCerComponents();
            services.AddWPFServices();
            services.AddTransient(_=>new MainWindow(services));
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static void ConfigureApiSettings(IServiceCollection services, ApiSettings apiSettings)
        {
            string host = apiSettings.Host;
            string port = apiSettings.Port;
            string uploadUrl = apiSettings.UploadPath;
            string basePath = apiSettings.BasePath;
            Uri baseUri = new Uri("https://localhost:5001");
            if (string.IsNullOrEmpty(host))
            {
                host = baseUri.Host;
            }
            if (string.IsNullOrEmpty(port))
            {
                port = baseUri.Port.ToString();
            }
            if (host.IndexOf("http://") < 0 && host.IndexOf("https://") < 0)
            {
                host = baseUri.Scheme + "://" + host;
            }
            apiSettings.Host = host;
            apiSettings.Port = port;
            apiSettings.BasePath = basePath;
            apiSettings.UploadPath = uploadUrl;
        }
    }
}

