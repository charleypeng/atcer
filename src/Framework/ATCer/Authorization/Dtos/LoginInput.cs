// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authentication.Enums;
using ATCer.Enums;
using ATCer.VerifyCode.Dtos;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ATCer.Authorization.Dtos
{
    /// <summary>
    /// 登录输入参数
    /// </summary>
    public class LoginInput: ImageVerifyCodeCheckInput
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空。")]
        [MinLength(5, ErrorMessage = "长度不能小于5位。")]
        [MaxLength(32, ErrorMessage = "长度不能大于32位。")]
        [DisplayName("用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空。")]
        [MinLength(5, ErrorMessage = "长度不能小于5位。")]
        [MaxLength(32, ErrorMessage = "长度不能大于32位。")]
        [DisplayName("密码")]
        public string Password { get; set; }

        /// <summary>
        /// 登录类型
        /// </summary>
        /// 
        public LoginType LoginType { get; set; } = LoginType.AccountPassword;
        /// <summary>
        /// 登录客户端类型
        /// </summary>
        public LoginClientType LoginClientType { get; set; } = LoginClientType.Browser;

    }
}