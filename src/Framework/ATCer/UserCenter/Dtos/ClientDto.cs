// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ATCer.UserCenter.Dtos
{
    /// <summary>
    /// 客户端信息
    /// </summary>
    public class ClientDto:BaseDto<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [Required(ErrorMessage = "不能为空"), MaxLength(30,ErrorMessage = "最大长度不能大于{1}")]
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(500, ErrorMessage = "最大长度不能大于{1}"), Required(ErrorMessage = "不能为空")]
        [DisplayName("备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [DisplayName("联系人")]
        [MaxLength(20, ErrorMessage = "最大长度不能大于{1}")]
        public string Contacts { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [DisplayName("电话")]
        [MaxLength(20, ErrorMessage = "最大长度不能大于{1}")]
        public string Tel { get; set; }

        /// <summary>
        /// 私钥
        /// </summary>
        [Required(ErrorMessage = "不能为空"), StringLength(64, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("私钥")]
        public string SecretKey { get; set; }
        
        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("邮箱")]
        public string Email { get; set; }
    }
}
