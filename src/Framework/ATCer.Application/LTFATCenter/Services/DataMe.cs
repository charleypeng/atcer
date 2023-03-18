// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Cache;
using ATCer.DataCenter.Enums;
using ATCer.DataCenter.Models.MH4016_9;
using ATCer.ElasticSearch;
using ATCer.ElasticSearch.Services;
using Microsoft.AspNetCore.Authorization;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.Application.LTFATCenter.Services
{
    [AllowAnonymous]
    public class DataMe:BaseElasticService<TestWalala,TestWalala,string>, IScoped, ICapSubscribe
    {
        private readonly ICapPublisher _publisher;
        /// <summary>
        /// 初始化
        /// </summary>
        public DataMe(ILogger<DataMe> logger,
                                IATCerEsClient elasticClient,
                                ICapPublisher publisher,
                                ICache cache) : base(elasticClient, cache, logger, "testdata2")
        {
            _publisher = publisher;
        }

        public async Task InsertMany()
        {
            var ds = new TestWalala()
            {
                Id = "1",
                CreatedTime = DateTimeOffset.Now,
                IsDeleted = false,
                Position = "36R",
                Type = "wdd",
                MyProperty = new List<Testhey>
                {
                    new Testhey { Name = "34hjhf", Status = MetDataStatus.Invalid , Value = 123},
                    new Testhey { Name = "press", Status = MetDataStatus.Manual, Value = 234 },
                    new Testhey { Name = "pw", Status = MetDataStatus.Manual, },
                    new Testhey { Name = "null", Status = MetDataStatus.Normal, Value = 123 }
                }
            };

            var ds2 = new TestWalala()
            {
                Id = "2",
                CreatedTime = DateTimeOffset.Now,
                IsDeleted = false,
                Position = "36R",
                Type = "press",
                MyProperty = new List<Testhey>
                {
                    new Testhey { Name = "34hjhf", Status = MetDataStatus.Invalid, Value= 2343 },
                    new Testhey { Name = "press", Status = MetDataStatus.Manual, Value= 123 },
                    new Testhey { Name = "pw", Status = MetDataStatus.Manual },
                    new Testhey { Name = "null", Status = MetDataStatus.Normal }
                }
            };

            await this.Insert(ds);
            await this.Insert(ds2);
        }
    }
}
