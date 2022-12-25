// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authentication.Dtos;
using ATCer.Authorization.Dtos;
using System.Threading.Tasks;

namespace ATCer.Authorization.Core
{
    /// <summary>
    /// 身份权限服务
    /// </summary>
    public interface IIdentityPermissionService
    {

        /// <summary>
        /// 检测是否有该功能点的使用权限
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="api"></param>
        /// <returns></returns>
        Task<bool> Check(Identity identity, ApiEndpoint api);

        /// <summary>
        /// 获取身份的编号
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        object GetIdentityId(Identity identity);

        /// <summary>
        /// 检测loginId是否可用
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        Task<bool> CheckLoginIdUsable(string loginId);
    }
}
