// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authentication.Core;
using ATCer.Authentication.Dtos;
using ATCer.Authentication.Enums;
using ATCer.Cache;
using ATCer.Common;
using ATCer.SystemManager.Dtos;
using ATCer.UserCenter.Dtos;
using ATCer.UserCenter.Impl.Domains;
using ATCer.UserCenter.Services;
using Microsoft.AspNetCore.Http;

namespace ATCer.UserCenter.Impl.Services
{
    /// <summary>
    /// AppToken服务
    /// </summary>
    [ApiDescriptionSettings("UserCenterServices")]
    public class AppTokenService : ServiceBase<AppToken, AppTokenDto, string>, IAppTokenServce, IScoped
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtService _jwtBearerService;
        private readonly IRepository<ClientFunction> _clientFunctionRespository;
        private readonly IRepository<User> _userRepository;
        private readonly ICache _cache;
        private readonly ILogger<AppTokenService> _logger;

        /// <summary>
        /// init
        /// </summary>
        /// <param name="jwtBearerService"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="repository"></param>
        /// <param name="userRepository"></param>
        /// <param name="cache"></param>
        /// <param name="logger"></param>
        /// <param name="clientFunctionRespository"></param>
        public AppTokenService(IJwtService jwtBearerService, 
                               IHttpContextAccessor httpContextAccessor, 
                               IRepository<AppToken> repository, 
                               IRepository<User> userRepository,
                               ICache cache,
                               ILogger<AppTokenService> logger,
                               IRepository<ClientFunction> clientFunctionRespository) : base(repository)
        {
            _jwtBearerService = jwtBearerService;
            _httpContextAccessor = httpContextAccessor;
            _clientFunctionRespository = clientFunctionRespository;
            _userRepository = userRepository;
            _logger = logger;
            _cache = cache;
        }

        public Task<List<FunctionDto>> GetFunctions(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取认证信息
        /// </summary>
        /// <param name="appToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [NonAction]
        public async Task<Identity> GetIdentity(string appToken)
        {
            var data = await _cache.GetAsync<Identity>(appToken);
            var token = await _repository.Where(x => x.Token == appToken && 
                            x.IsDeleted == false && 
                            x.IsLocked == false && 
                            x.ExpireAt >= DateTimeOffset.Now).FirstOrDefaultAsync();
            
            return data == null || token == null ? null : data;
        }

        /// <summary>
        /// 获取JWT
        /// </summary>
        /// <param name="appToken"></param>
        /// <returns></returns>
        [NonAction]
        public async Task<string> GetJwtToken(string appToken)
        {
            var identity = await GetIdentity(appToken);
            if (identity != null)
            {
                var jwt = await _jwtBearerService.CreateToken(identity);
                return jwt == null ? string.Empty : jwt.AccessToken;
            }

            return string.Empty;
        }


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<AppTokenDto> Insert(AppTokenDto input)
        {
            var token = await base.Insert(input);
            
            if(token != null)
            {
                //get user
                var user = await _userRepository.Where(x => x.IsLocked == false &&
                    x.IsDeleted == false && 
                    x.Id == token.UserId).FirstOrDefaultAsync();
                if (user != null)
                {
                    var identity = new Identity
                    {
                        Id = user.Id.ToString(),
                        GivenName = user.NickName,
                        Name = user.UserName,
                        IdentityType = IdentityType.AppToken,
                        LoginClientType = LoginClientType.App,
                        LoginId = token.Id
                    };
                    //注意主Key为token
                    await _cache.SetAsync(token.Token, identity);
                }
                return token;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<bool> Update(AppTokenDto input)
        {
            var result = await base.Update(input);
            if(result)
            {
                //get user
                var user = await _userRepository.Where(x => x.IsLocked == false && x.IsDeleted == false & x.Id == input.UserId).FirstOrDefaultAsync();
                if (user != null)
                {
                    var identity = new Identity
                    {
                        Id = user.Id.ToString(),
                        GivenName = user.NickName,
                        Name = user.UserName,
                        IdentityType = IdentityType.AppToken,
                        LoginClientType = LoginClientType.App,
                        LoginId = input.Id
                    };
                    //注意主Key为token
                    await _cache.SetAsync(input.Token, identity);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<bool> FakeDelete(string id)
        {
            var result = await base.FakeDelete(id);
            if (result)
            {
                //also should remove from cache
                var token = await _repository.FindAsync(id);
                if(token != null)
                {
                    await _cache.RemoveAsync(token.Token);
                }
                
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 生成Token
        /// </summary>
        /// <returns></returns>
        public Task<string> GenerateToken()
        {
            return Task.FromResult("atk_"+Guid.NewGuid().ToString("N"));
        }

        /// <summary>
        /// 添加至缓存
        /// <para>请在启动项中添加</para>
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public async Task ImportToCache()
        {
            var appTokens = await _repository.AsQueryable(false).ToListAsync();
            if (appTokens == null)
                return;

            await appTokens.ForEachAsync(async token =>
            {
                //get user
                var user = await _userRepository.Where(x=>x.IsLocked == false && 
                                                          x.IsDeleted == false &&
                                                          x.Id == token.UserId).FirstOrDefaultAsync();
                if(user != null)
                {
                    var identity = new Identity
                    {
                        Id = user.Id.ToString(),
                        GivenName = user.NickName,
                        Name = user.UserName,
                        IdentityType = IdentityType.AppToken,
                        LoginClientType = LoginClientType.App,
                        LoginId = token.Id
                    };
                    //注意主Key为token
                    await _cache.SetAsync(token.Token, identity);
                }
            });
            _logger.LogInformation($"{appTokens?.Count} 条AppToken已加入缓存");
        }
    }
}
