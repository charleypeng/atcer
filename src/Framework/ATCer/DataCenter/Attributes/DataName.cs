// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.DataCenter;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class DataName : Attribute
{
    public DataName(string name)
    {
        DisplayName = name;
    }

    /// <summary>
    /// 显示的数据名称
    /// </summary>
    public string DisplayName { get; private set; }
}
