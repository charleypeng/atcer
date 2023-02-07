// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.SysJob.Dtos;

/// <summary>
/// 
/// </summary>
public class JobTriggerInput
{
    /// <summary>
    /// 作业Id
    /// </summary>
    public string JobId { get; set; }

    /// <summary>
    /// 触发器Id
    /// </summary>
    public string TriggerId { get; set; }
}

/// <summary>
/// 
/// </summary>
public class AddJobTriggerInput : SysJobTriggerDto
{
    /// <summary>
    /// 作业Id
    /// </summary>
    [Required(ErrorMessage = "作业Id不能为空"), MinLength(2, ErrorMessage = "作业Id不能少于2个字符")]
    public override string JobId { get; set; }

    /// <summary>
    /// 触发器Id
    /// </summary>
    [Required(ErrorMessage = "触发器Id不能为空"), MinLength(2, ErrorMessage = "触发器Id不能少于2个字符")]
    public override string TriggerId { get; set; }
}

/// <summary>
/// 
/// </summary>
public class UpdateJobTriggerInput : AddJobTriggerInput
{
}

/// <summary>
/// 
/// </summary>
public class DeleteJobTriggerInput : JobTriggerInput
{
}