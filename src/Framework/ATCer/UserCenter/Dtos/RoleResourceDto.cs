// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.SystemManager.Dtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace ATCer.UserCenter.Dtos
{
    /// <summary>
    /// 角色资源关系
    /// </summary>
    public class RoleResourceDto
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Required]
        public int RoleId { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public RoleDto Role { get; set; }
        /// <summary>
        /// 权限Id
        /// </summary>
        [Required]
        public Guid ResourceId { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public ResourceDto Resource { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;
    }
}
