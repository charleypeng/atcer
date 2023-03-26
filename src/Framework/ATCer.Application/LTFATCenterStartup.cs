// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using ATCer.LTFATCenter.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using ATCer.DataRecorder;
using System.Text;
using ATCer.LTFATCenter.Services;
using ATCer.MessageQueue.Core;
using ATCer.MessageQueue.Dtos;
using SimpleUdp;

namespace ATCer.Core
{
    /// <summary>
    /// LTFAT启动类
    /// </summary>
    [AppStartup(600)]
    public class LTFATCenterStartup : AppStartup
    {
        private string migrationAssemblyName = App.Configuration["FipsDbSettings:MigrationAssemblyName"]!;
        private string fipsDbProvider = App.Configuration["FipsDbSettings:DbProvider"]!;
        private string masterDbProvider = App.Configuration["DefaultDbSettings:DbProvider"]!;
        private ILTFATDataService? dataService;
        private ICapPublisher? publisher;
        private IMQService? mqService;
        /// <summary>
        /// Config service
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            //业务库
            services.AddDatabaseAccessor(options =>
            {
                //注入数据库上下文
                options.AddDbPool<FipsDbContext, FipsDbContextLocator>(fipsDbProvider);
                options.AddDbPool<ATCerSlaveDbContext, ATCerSlaveDbContextLocator>(masterDbProvider);
                options.AddDbPool<FipsHistoryDbContext, FipsHistoryDbContextLocator>(fipsDbProvider);
                options.AddDbPool<SugarTest, SugarTestLocator>(masterDbProvider);
            }, migrationAssemblyName);

            services.AddLTFATService();
            //services.AddLTFATBackgroundService();
            services.AddDataRecorder(opt =>
            {
                opt.Recorders = new List<RecorderOptions>
                {
                    new RecorderOptions { Encoding = DataEncodings.UTF8, Ip=null,Port=1234, RecorderName=nameof(TestRecorder),EndpointType= EndpointType.Broadcast},
                    new RecorderOptions { Encoding = DataEncodings.UTF8, 
                        Ip="239.119.119.119",Port=40169, 
                        RecorderName = nameof(TestRecorder2), 
                        EndpointType = EndpointType.Multicast , 
                        JobAction = (a)=>
                        {
                            Console.WriteLine(Encoding.UTF8.GetString(a.Data));
                        } 
                    }
                };
            });
            //.AddRecorder<Services.TestRecorder2>(opt =>
            //{
            //    opt.Encoding = DataEncodings.UTF8;
            //    opt.Ip = "127.0.0.1";
            //    opt.Port = 12334;
            //});
        }

        /// <summary>
        /// Config
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            //allow mapster to configure globally
            TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);
            //切换postgresql时在dbsettings中设置为true
            //var sp = App.GetService<IRecorder>();
            //app.UseRecorder(sp);
            //UDPListener.StartListener();
            //var tr = App.GetService<IRecorder>();
            //tr.Start();
            var tr = App.GetService<ITestRecorder>();
            ITestRecorder2 tr2 = App.GetService<ITestRecorder2>();
            //tr.StartWithRetry(5);
            tr.DataReceived += tr_DataReceived!;

            tr2.StartWithRetry(5);
            Console.WriteLine(tr.RecordData.Ip + ":" + tr.RecordData.Port);
            //tr.Start();
            tr2.DataReceived += tr2_DataReceived!;
            publisher = App.GetService<ICapPublisher>();
            dataService = App.GetService<ILTFATDataService>();
            mqService = App.GetService<IMQService>();

            var dt = App.GetService<DataTest>();
            //dt.Start();
        }

        private async void tr_DataReceived(object sender, Datagram e)
        {
            var str = Encoding.ASCII.GetString(e.Data);
            MQRecord data = new MQRecord { Data = str, MQTopic = "TESTDATA" };
            MQData mq = new MQData { MQTopic = "TESTDATA", Data = str };
            await mqService?.SendToAllWithMQ<MQData>("SendMQ", mq)!;
            Console.WriteLine($"!#WARNING#:msg is send in {data.IssueTime}");
        }

        private async void tr2_DataReceived(object sender, Datagram e)
        {
            var str = Encoding.UTF8.GetString(e.Data);
            var data = new Record { Cat = "4029.3", Content = str };
            Console.WriteLine(str);
            //await publisher?.PublishAsync("cat048test", data)!;
        }
    }
}
