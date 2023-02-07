// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.SysJob.Dtos;

/// <summary>
/// 
/// </summary>
public class JobOutput
{
    /// <summary>
    /// 作业信息
    /// </summary>
    public SysJobDetailDto JobDetail { get; set; }

    /// <summary>
    /// 触发器集合
    /// </summary>
    public List<SysJobTriggerDto> JobTriggers { get; set; }
}