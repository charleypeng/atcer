// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.Authorization.Dtos
{
    /// <summary>
    /// token刷新返回结果
    /// </summary>
    public class TokenOutput
    {
        /// <summary>
        /// 获取或设置 用于业务身份认证的AccessToken
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 获取或设置 AccessToken有效期(时间戳精确到秒)
        /// </summary>
        public long AccessTokenExpires { get; set; }
        /// <summary>
        /// 获取或设置 用于刷新AccessToken的RefreshToken
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// 获取或设置 RefreshToken有效期(时间戳精确到秒)
        /// </summary>
        public long RefreshTokenExpires { get; set; }
    }
}
