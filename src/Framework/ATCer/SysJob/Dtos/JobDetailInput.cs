// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace ATCer.SysJob.Dtos;

/// <summary>
/// 
/// </summary>
public class JobDetailInput
{
    /// <summary>
    /// 作业Id
    /// </summary>
    public string JobId { get; set; }
}

/// <summary>
/// 
/// </summary>
public class PageJobInput : Base.MyPagedList
{
    /// <summary>
    /// 作业Id
    /// </summary>
    public string JobId { get; set; }

    /// <summary>
    /// 描述信息
    /// </summary>
    public string Description { get; set; }
}

/// <summary>
/// 
/// </summary>
public class AddJobDetailInput : SysJobDetailDto
{
    /// <summary>
    /// 作业Id
    /// </summary>
    [Required(ErrorMessage = "作业Id不能为空"), MinLength(2, ErrorMessage = "作业Id不能少于2个字符")]
    public override string JobId { get; set; }
}

/// <summary>
/// 
/// </summary>
public class UpdateJobDetailInput : AddJobDetailInput
{
}

/// <summary>
/// 
/// </summary>
public class DeleteJobDetailInput : JobDetailInput
{
}
