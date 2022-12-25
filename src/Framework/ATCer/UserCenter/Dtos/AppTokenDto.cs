// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.UserCenter.Dtos
{
    /// <summary>
    /// APPToken dto
    /// </summary>
    public class AppTokenDto:BaseDto<string>
    {
        /// <summary>
        /// App名称
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string AppName { get; set; } = NameGenerator.GenerateName(10);
        /// <summary>
        /// 密钥
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 关联的用户ID
        /// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// 过期时间(默认30天)
        /// <para>如果为空则不过期</para>
        /// </summary>
        public DateTimeOffset? ExpireAt { get; set; }
    }
}
