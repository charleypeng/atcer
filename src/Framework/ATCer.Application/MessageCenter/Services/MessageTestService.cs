// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATCer.MessageCenter.Domains;
using ATCer.MessageCenter.Dtos;
using Furion.DatabaseAccessor;
using Microsoft.AspNetCore.Authorization;
using ATCer.Cache;
using ATCer;
namespace ATCer.MessageCenter.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageTestService : ServiceBase<HubClient, HubClientDto, int>
    {
        private readonly IRepository<HubClient> _hubClientRepo;
        private readonly ICache _cache;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="cache"></param>
        public MessageTestService(IRepository<HubClient> repository,
                                  ICache cache) : base(repository)
        {
            _hubClientRepo = repository;
            _cache = cache;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task InsertNewUser()
        {
            var u = new HubClientDto();
            u.Remark = Guid.NewGuid().ToString();
            await this.Insert(u);
            await _cache.SetAsync("lol", u);
        }
    }
}
