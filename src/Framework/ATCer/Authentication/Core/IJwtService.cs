// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authentication.Dtos;
using System.Threading.Tasks;

namespace ATCer.Authentication.Core
{
    /// <summary>
    /// jwt
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// 创建token
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        Task<JsonWebToken> CreateToken(Identity identity);
        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="orlRefreshToken"></param>
        /// <returns></returns>
        Task<JsonWebToken> RefreshToken(string orlRefreshToken);
        /// <summary>
        /// 移除当前用户的刷新token
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        Task<bool> RemoveRefreshToken(Identity identity);
        /// <summary>
        /// 解密JWT
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        Task<object> DecryptJwt(string token);
    }
}