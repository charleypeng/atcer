using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Mapster;
using Furion.TaskScheduler;
using Furion;
using ATCer.Cache;
using Furion.DatabaseAccessor;
using ATCer.Common;
using ATCer.LTFATCenter.Domains.GZFips;
using Microsoft.EntityFrameworkCore;
using ATCer.LTFATCenter.Domains;

namespace ATCer.LTFATCenter.Services
{
    /// <summary>
    /// Fips后台同步
    /// </summary>
    public class FipsSyncService : BackgroundService, IFipsSyncService
    {
        private readonly ILogger<FipsSyncService> _logger;
        private readonly ICache _cache;
        private readonly string _localAirport;
        /// <summary>
        /// FIPS Sync Service Init
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="cache"></param>
        public FipsSyncService(ILogger<FipsSyncService> logger,
                               ICache cache)
        {
            _logger = logger;
            _cache = cache;
            _localAirport = App.Configuration["ServerSettings:Local"];
        }

        /// <summary>
        /// Execute the sync
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken = default)
        {
            _logger.LogInformation(
                "FipsSync Background Service Now Begins.");
            while (!stoppingToken.IsCancellationRequested)
            {
                // 间隔执行任务
                await SpareTime.DoAsync(10000, async() =>
                {
                    await DoWork(stoppingToken);
                    //_logger.LogInformation("Fips sync service running at: {time}", DateTimeOffset.Now);
                }, stoppingToken);
            }
            
        }

        private long counter;
        /// <summary>
        /// Do Fips sync work
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        private async Task DoWork(CancellationToken stoppingToken = default)
        {

            _logger.LogInformation(
                $"Fips Background Service Hosted Service is working @: {DateTimeOffset.Now}, count:{counter}");
           

            //await _cache.SetAsync(CacheSchems.FlightsOfToday, );
            counter++;
        }

        /// <summary>
        /// Stop fips sync work
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public override async Task StopAsync(CancellationToken stoppingToken = default)
        {
            _logger.LogInformation(
                "Fips Background Service Hosted Service is stopping...");

            await base.StopAsync(stoppingToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task StartAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation(
                "Fips Background Service Hosted Service is starting...");
            return base.StartAsync(cancellationToken);
        }
    }
}