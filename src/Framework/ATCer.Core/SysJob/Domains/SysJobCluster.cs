// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.SysJob.Enums;
using System.ComponentModel.DataAnnotations;

namespace ATCer.SysJob.Domains;

/// <summary>
/// 系统作业集群表
/// </summary>
public class SysJobCluster : ATCerEntityBase
{
    /// <summary>
    /// 作业集群Id
    /// </summary>
    [Required, MaxLength(64)]
    public virtual string ClusterId { get; set; }

    /// <summary>
    /// 描述信息
    /// </summary>
    [MaxLength(128)]
    public string Description { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public ClusterStatus Status { get; set; }
}