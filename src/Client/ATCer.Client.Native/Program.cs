using System;
using System.Net.Http;
using ATCer.Client.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Photino.Blazor;
using Microsoft.Extensions.Logging;
using AntDesign.ProLayout;
using ATCer.Client.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using System.IO;
using ATCer.NotificationSystem.Client.Core;

namespace ATCer.Client.Native
{
    class Program
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public static IConfiguration Configuration { get; private set; }
        [STAThread]
        static void Main(string[] args)
        {
            var cbuilder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = cbuilder.Build();

            var builder = PhotinoBlazorAppBuilder.CreateDefault(args);

            
            builder.Services
                .AddLogging();
            //config my services
            ConfigureServices(builder.Services);
            // register root component and selector
            builder.RootComponents.Add<App>("app");

            var app = builder.Build();

            // customize window
            app.MainWindow
                .SetIconFile("favicon.ico")
                .SetTitle("Photino Blazor Sample");

            AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
            {
                app.MainWindow.ShowMessage("Fatal exception", error.ExceptionObject.ToString());
            };

            app.Run();

        }

        private static void ConfigureServices(IServiceCollection services)
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
            services.AddServicesWithAttributeOfTypeFromModuleContextNative(new[] { typeof(App).Assembly });
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
            services.AddNativeServices();

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
