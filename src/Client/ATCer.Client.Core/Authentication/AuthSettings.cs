// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.Client.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthSettings
    {
        /// <summary>
        /// token刷新间隔（单位：秒）
        /// token 过期时间是API 配置 JWTSettings.ExpiredTime
        /// </summary>
        public int RefreshTokenCheckInterval { get; set; } = 240;

        /// <summary>
        /// token刷新过期时间阈值（单位：秒）
        /// </summary>
        public int RefreshTokenTimeThreshold { get; set; } = 70;
    }
}
