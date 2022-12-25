// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using ATCer.UserCenter.Dtos;
using ATCer.UserCenter.Impl.Domains;
using ATCer.UserCenter.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATCer.UserCenter.Impl.Services
{
    /// <summary>
    /// 客户端与接口关系服务
    /// </summary>
    [ApiDescriptionSettings("UserCenterServices")]
    public class ClientFunctionService : IClientFunctionService, IDynamicApiController
    {

        private readonly IRepository<ClientFunction> repository;
        /// <summary>
        /// 客户端与接口关系服务
        /// </summary>
        /// <param name="repository"></param>
        public ClientFunctionService(IRepository<ClientFunction> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// 添加客户端与接口关系
        /// </summary>
        /// <param name="clientFunctionDtos"></param>
        /// <returns></returns>
        public async Task<bool> Add(List<ClientFunctionDto> clientFunctionDtos)
        {
            await repository.InsertAsync(clientFunctionDtos.Select(x => x.Adapt<ClientFunction>()));
            return true;
        }

        /// <summary>
        /// 删除客户端与接口关系
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public async Task<bool> Delete([FromRoute] Guid clientId, [FromRoute] Guid functionId)
        {
            List<ClientFunction> entitys = await repository.Where(x => x.ClientId.Equals(clientId) && x.FunctionId.Equals(functionId)).ToListAsync();

            if (entitys == null || entitys.Count == 0)
            {
                return false;
            }

            await repository.DeleteAsync(entitys);

            return true;
        }
    }
}
