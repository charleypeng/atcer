// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.SysJob.Dtos;

/// <summary>
/// 系统作业信息
/// </summary>
public class SysJobDetailDto : Base.BaseDto<int>
{
    /// <summary>
    /// 作业Id
    /// </summary>
    [Required(ErrorMessage = "作业Id不能为空"), MinLength(2, ErrorMessage = "作业Id不能少于2个字符"), MaxLength(64)]
    public virtual string JobId { get; set; }

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
    /// 触发器集合
    /// </summary>
    public List<SysJobTriggerDto> JobTriggers { get; set; }
}