﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using ATCer.LTFATCenter.Domains;
using Furion;
using Elasticsearch.Net;
using System.Security.Cryptography.X509Certificates;
using ATCer.Services;
using ATCer.LTFATCenter.Domains.Elastic;
using ATCer.Application.LTFATCenter.Domains;
using ATCer.DataCenter.Models.MH4016_9;
using ATCer.DataCenter.Domains;
using ATCer.LTFATCenter.Impl.Services;
using ATCer.LTFATCenter.Services;
using Confluent.Kafka;
using ATCer.ElasticSearch;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// ElasticSearch扩展类
    /// </summary>
    public static class LTFATServiceExtension
    {
        /// <summary>
        /// 添加ElasticSearch服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddLTFATService(
            this IServiceCollection services)
        {
            var configuration = App.Configuration;
            var url = configuration["elasticsearch:url"];
            var defaultIndex = configuration["elasticsearch:index"];
            var userName = configuration["elasticsearch:username"];
            var passWord = configuration["elasticsearch:password"];

            var settings = new ConnectionSettings(new Uri(url))
                .ServerCertificateValidationCallback((obj, cert, chain, sslPolicyErrors) => true)
                .BasicAuthentication(userName, passWord)
                .DefaultIndex(defaultIndex);

            var client = new ATCerEsClient(settings);

            services.AddSingleton<IATCerEsClient>(client);



            services.AddScoped<IFipsService, FipsService>();
            services.AddScoped<IFlightPlanService, FlightPlanService>();
            services.AddScoped<ISyncService, FlightPlanSync>();
            services.AddScoped<IDashboardService, DataCenterService>();
            return services;
        }

        private static void AddDefaultMappings(ConnectionSettings settings)
        {
            //settings
            //    .DefaultMappingFor<ESFlightPlan>(m => m
            //    .IdProperty("Id")
            //        //.Ignore(p => p.OtherInfo)
            //    );
            //settings
            //    .DefaultMappingFor<Record>(m => m
            //    .IdProperty("Id")
            //    );
            settings
                .DefaultMappingFor<MyMetData>(m => m
                .IdProperty("Id")
                );
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            //var createIndexResponse = client.Indices.Create(indexName,
            //    index => index.Map<ESFlightPlan>(x => x.AutoMap())
            //);

            //if (createIndexResponse.Acknowledged)
            //    Console.WriteLine("ElasticSearch index created");

            var createIndexResponse2 = client.Indices.Create(indexName,
                index => index.Map<Metadata>(x => x.AutoMap())
            );

            if (createIndexResponse2.Acknowledged)
                Console.WriteLine("ElasticSearch index created");
        }
    }
}
