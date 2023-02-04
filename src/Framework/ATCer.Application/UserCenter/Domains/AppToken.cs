// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using ATCer.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace ATCer.UserCenter.Impl.Domains
{
    /// <summary>
    /// AppToken实体
    /// </summary>
    [Index(nameof(AppName), IsUnique = true)]
    public class AppToken:ATCerEntityBase<string>
    {
        /// <summary>
        /// App名称
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string? AppName { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        [Required]
        public string? Token { get; set; }
        /// <summary>
        /// 关联的用户ID
        /// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// 过期时间(默认30天)
        /// <para>如果为Max则不过期</para>
        /// </summary>
        public DateTimeOffset ExpireAt { get; set; }
    }
}
