// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using DotNetCore.CAP.Dashboard.NodeDiscovery;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Savorboard.CAP.InMemoryMessageQueue;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ATCer.MessageQueue
{
    /// <summary>
    /// Add message queue extension
    /// </summary>
    public static class AddMQServiceExtention
    {
        /// <summary>
        ///  add message queue
        /// </summary>
        /// <param name="services"></param>
        public static void AddMQService(this IServiceCollection services)
        {
            //add CAP authorization
            //services
            //    .AddAuthorization()
            //    .AddAuthentication(options =>
            //    {
            //        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            //    })
            //    .AddCookie()
            //    .AddOpenIdConnect(options =>
            //    {
            //        options.Authority = "http://192.168.2.126:8090/admin/oauthcallback.php";
            //        options.ClientId = "GLtGncQtkPMjFBMxnDM12YN1dAy7wwrmNqYQTXY4RgGcgrJNVXNw0iS4jv6C41QQ";
            //        options.ClientSecret = "YpnFvskThVn3BX78FvEHgnlsSCvBckaXcUfV174JwPIcl8WJfxf3pP49XCAtB9Ol";
            //        options.ResponseType = "code";
            //        options.UsePkce = true;
            //        options.RequireHttpsMetadata = false;
            //        options.Scope.Clear();
            //        options.Scope.Add("openid");
            //        options.Scope.Add("profile");
            //    });
            //get CAPNode options
            using var serviceProvider = services.BuildServiceProvider();
            var mqOptions = serviceProvider.GetService<IOptions<MessageQueueOptions>>().Value;
            var discoveryOptions = mqOptions.CAPNode.Adapt<DiscoveryOptions>();
            //get assemblies
            var assemblies = App.Assemblies.ToArray();
            //add cap service
            services.AddCap(opt =>
            {
                //set database
                opt.SetDbProivder(mqOptions);
                //set mq server
                opt.SetMqProivder(mqOptions);
                //add dashboard
                opt.UseDashboard(opt =>
                {
                    //opt.UseChallengeOnAuth = true;
                    //opt.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                });
                //add consul
                if (discoveryOptions != null)
                {
                    opt.UseDiscovery(x =>
                    {
                        x.DiscoveryServerHostName = discoveryOptions.DiscoveryServerHostName;
                        x.DiscoveryServerPort = discoveryOptions.DiscoveryServerPort;
                        x.CurrentNodeHostName = discoveryOptions.CurrentNodeHostName;
                        x.CurrentNodePort = discoveryOptions.CurrentNodePort;
                        x.MatchPath = discoveryOptions.MatchPath;
                        x.Scheme = discoveryOptions.Scheme;
                        x.NodeId = discoveryOptions.NodeId;
                        x.NodeName = discoveryOptions.NodeName;
                        x.CustomTags = discoveryOptions.CustomTags;
                    });
                }
            })
            .AddSubscriberAssembly(assemblies)
            .AddSubscribeFilter<MQFilter>(); 
        }

        /// <summary>
        /// set db provider
        /// </summary>
        /// <param name="opt"></param>
        /// <param name="mqOptions"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetDbProivder(this CapOptions opt, MessageQueueOptions mqOptions)
        {
            
            //set db provider
            if(mqOptions == null)
                throw new ArgumentNullException(nameof(mqOptions));

            switch (mqOptions.DbProvider)
            {
                case DbProviders.SqlServer:
                    if (mqOptions.SqlServerOptions == null)
                        throw new ArgumentNullException(nameof(mqOptions.SqlServerOptions));
                    opt.UseSqlServer(mqOptions.SqlServerOptions.ConnectionString);
                    break;
                case DbProviders.PostreSql:
                    if (mqOptions.PostgreSql == null)
                        throw new ArgumentNullException(nameof(mqOptions.PostgreSql));
                    opt.UsePostgreSql(mqOptions.PostgreSql.ConnectionString);
                    break;
                case DbProviders.InMemory:
                    opt.UseInMemoryStorage();
                    break;
                case DbProviders.MySql:
                    if (mqOptions.MySql == null)
                        throw new ArgumentNullException(nameof(mqOptions.MySql));
                    opt.UseSqlServer(mqOptions.MySql.ConnectionString);
                    break;
                default:
                    opt.UseInMemoryStorage();
                    break;
            }
        }

        /// <summary>
        /// set mq provider
        /// </summary>
        /// <param name="opt"></param>
        /// <param name="mqOptions"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetMqProivder(this CapOptions opt, MessageQueueOptions mqOptions)
        {

            //set db provider
            if (mqOptions == null)
                throw new ArgumentNullException(nameof(mqOptions));

            switch (mqOptions.MQProvider)
            {
                case MQProviders.RabitMQ:
                    if (mqOptions.RabitMQ == null)
                        throw new ArgumentNullException(nameof(mqOptions.RabitMQ));
                    opt.UseRabbitMQ(x =>
                    {
                        x.HostName = mqOptions.RabitMQ.HostName;
                        x.UserName = mqOptions.RabitMQ.UserName;
                        x.Password = mqOptions.RabitMQ.Password;
                        x.Port = mqOptions.RabitMQ.Port;
                    });
                    break;
                case MQProviders.Kalfka:
                    if (mqOptions.Kalfka == null)
                        throw new ArgumentNullException(nameof(mqOptions.Kalfka));
                    opt.UseKafka(x => 
                    {
                        x.Servers = mqOptions.Kalfka.Servers;
                        x.ConnectionPoolSize = mqOptions.Kalfka.ConnectionPoolSize;
                    });
                    break;
                default:
                    opt.UseInMemoryMessageQueue();
                    break;
            }
        }
    }
}
