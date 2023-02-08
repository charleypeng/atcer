// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using Furion.Schedule;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ATCer.SysJob.Domains;

/// <summary>
/// 系统作业触发器表
/// </summary>
#nullable disable
[Comment("系统作业触发器表")]
public class SysJobTrigger : IEntity, IEntityTypeBuilder<SysJobTrigger>
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// 触发器Id
    /// </summary>
    public string TriggerId { get; set; }

    /// <summary>
    /// 作业Id
    /// </summary>
    [Required, MaxLength(64)]
    public virtual string JobId { get; set; }

    /// <summary>
    /// 触发器类型FullName
    /// </summary>
    [MaxLength(128)]
    public string TriggerType { get; set; }

    /// <summary>
    /// 程序集Name
    /// </summary>
    [MaxLength(128)]
    public string AssemblyName { get; set; } = "Furion";

    /// <summary>
    /// 参数
    /// </summary>
    [MaxLength(128)]
    public string Args { get; set; }

    /// <summary>
    /// 描述信息
    /// </summary>
    [MaxLength(128)]
    public string Description { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public TriggerStatus Status { get; set; } = TriggerStatus.Ready;

    /// <summary>
    /// 起始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 最近运行时间
    /// </summary>
    public DateTime? LastRunTime { get; set; }

    /// <summary>
    /// 下一次运行时间
    /// </summary>
    public DateTime? NextRunTime { get; set; }

    /// <summary>
    /// 触发次数
    /// </summary>
    public long NumberOfRuns { get; set; }

    /// <summary>
    /// 最大触发次数（0:不限制，n:N次）
    /// </summary>
    public long MaxNumberOfRuns { get; set; }

    /// <summary>
    /// 出错次数
    /// </summary>
    public long NumberOfErrors { get; set; }

    /// <summary>
    /// 最大出错次数（0:不限制，n:N次）
    /// </summary>
    public long MaxNumberOfErrors { get; set; }

    /// <summary>
    /// 重试次数
    /// </summary>
    public int NumRetries { get; set; }

    /// <summary>
    /// 重试间隔时间（ms）
    /// </summary>
    public int RetryTimeout { get; set; } = 1000;

    /// <summary>
    /// 是否立即启动
    /// </summary>
    public bool StartNow { get; set; } = true;

    /// <summary>
    /// 是否启动时执行一次
    /// </summary>
    public bool RunOnStart { get; set; } = false;

    /// <summary>
    /// 是否在启动时重置最大触发次数等于一次的作业
    /// </summary>
    public bool ResetOnlyOnce { get; set; } = true;

    /// <summary>
    /// 
    /// </summary>
    public DateTimeOffset? UpdatedTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int SysJobDetailId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public SysJobDetail SysJobDetail { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entityBuilder"></param>
    /// <param name="dbContext"></param>
    /// <param name="dbContextLocator"></param>
    public void Configure(EntityTypeBuilder<SysJobTrigger> entityBuilder, DbContext dbContext, Type dbContextLocator)
    {
        entityBuilder.HasKey(x => x.Id);

        entityBuilder.HasOne(x => x.SysJobDetail)
                     .WithMany(x => x.SysJobTriggers)
                     .HasForeignKey(x => x.SysJobDetailId)
                     .OnDelete(DeleteBehavior.Cascade);
    }
}