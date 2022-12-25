// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace ATCer.UserCenter.Dtos
{
    /// <summary>
    /// 更新头像
    /// </summary>
    public class UserUpdateAvatarInput
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 头像
        /// </summary>
        [MaxLength(100, ErrorMessage = "最大长度不能大于{1}")]
        public string Avatar { get; set; }
    }
}
