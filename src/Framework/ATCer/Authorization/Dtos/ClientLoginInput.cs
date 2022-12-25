// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ATCer.Authorization.Dtos
{
    /// <summary>
    /// 客户端登录输入
    /// </summary>
    public class ClientLoginInput
    {
        /// <summary>
        /// 客户端编号
        /// </summary>
        [DisplayName("客户端编号")]
        [Required]
        public Guid ClientId { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        [DisplayName("时间戳")]
        [Required]
        public long Timespan { get; set; }

        /// <summary>
        /// 加密的值
        /// </summary>
        [Required]
        [DisplayName("加密的值")]
        public string EncryptionValue { get; set; }
    }
}
