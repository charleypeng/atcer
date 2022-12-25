// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using ATCer.Authentication.Domains;
using ATCer.Authentication.Dtos;
using ATCer.Base;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ATCer.Authentication.Services
{
    /// <summary>
    /// 用户登录TOKEN服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class LoginTokenService : ServiceBase<LoginToken, LoginTokenDto, Guid>, ILoginTokenService
    {
        private readonly IRepository<LoginToken> _loginTokenRepository;
        /// <summary>
        /// 用户登录TOKEN服务
        /// </summary>
        /// <param name="repository"></param>
        public LoginTokenService(IRepository<LoginToken> repository) : base(repository)
        {
            _loginTokenRepository = repository;
        }
        
        /// <summary>
        /// 搜索
        /// </summary>
        /// <remarks>
        /// 搜索数据
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<MyPagedList<LoginTokenDto>> Search(MyPageRequest request)
        {
            IDynamicFilterService filterService = App.GetService<IDynamicFilterService>();
            Expression<Func<LoginToken, bool>> expression = filterService.GetExpression<LoginToken>(request.FilterGroups);

            IQueryable<LoginToken> queryable = _loginTokenRepository.AsQueryable(false)
                .Where(u => u.IsDeleted == false)
                .Where(expression);
            return await queryable
                .OrderConditions(request.OrderConditions.ToArray())
                .Select(x => x.Adapt<LoginTokenDto>())
                .ToPageAsync(request.PageIndex, request.PageSize);
        }
    }
}
