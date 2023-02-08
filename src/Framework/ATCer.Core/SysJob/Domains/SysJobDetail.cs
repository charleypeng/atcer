// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace ATCer.SysJob.Domains;

/// <summary>
/// 系统作业信息
/// </summary>
#nullable disable
[Comment("系统作业表")]
public class SysJobDetail : ATCerEntityBase, IEntityTypeBuilder<SysJobDetail>
{
    /// <summary>
    /// 作业Id
    /// </summary>
    [Required, MaxLength(64)]
    public string JobId { get; set; }

    /// <summary>
    /// 组名称
    /// </summary>
    [MaxLength(128)]
    public string GroupName { get; set; } = "default";

    /// <summary>
    /// 作业类型FullName
    /// </summary>
    [MaxLength(128)]
    public string JobType { get; set; }

    /// <summary>
    /// 程序集Name
    /// </summary>
    [MaxLength(128)]
    public string AssemblyName { get; set; }

    /// <summary>
    /// 描述信息
    /// </summary>
    [MaxLength(128)]
    public string Description { get; set; }

    /// <summary>
    /// 是否并行执行
    /// </summary>
    public bool Concurrent { get; set; } = true;

    /// <summary>
    /// 是否扫描特性触发器
    /// </summary>
    public bool IncludeAnnotations { get; set; } = false;

    /// <summary>
    /// 额外数据
    /// </summary>
    public string Properties { get; set; } = "{}";

    /// <summary>
    /// 脚本代码
    /// </summary>
    public string ScriptCode { get; set; }
    /// <summary>
    /// Trigger Id
    /// </summary>
    public int SysJobTriggerId { get; set; }
    /// <summary>
    /// Triggers
    /// </summary>
    public List<SysJobTrigger> SysJobTriggers { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entityBuilder"></param>
    /// <param name="dbContext"></param>
    /// <param name="dbContextLocator"></param>
    public void Configure(EntityTypeBuilder<SysJobDetail> entityBuilder, DbContext dbContext, Type dbContextLocator)
    {
        
    }
}