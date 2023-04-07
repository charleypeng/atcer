// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.HRCenter.Enums;

/// <summary>
/// 管制职位
/// </summary>
public enum ControllerRole:byte
{
    /// <summary>
    /// 管制员
    /// </summary>
    [Description("管制员")]
    [Display(Name = "管制员")]
    Controller = 0,
    /// <summary>
    /// 学员
    /// </summary>
    [Description("学员")]
    [Display(Name = "学员")]
    Student = 1,
    /// <summary>
    /// 教员
    /// </summary>
    [Description("教员")]
    [Display(Name = "教员")]
    Caoch = 2,
    /// <summary>
    /// 带班
    /// </summary>
    [Description("领班")]
    [Display(Name = "领班")]
    Supervisor = 3,
}

/// <summary>
/// 管制员角色
/// </summary>
public static class ControllerConf
{
    static ControllerConf()
    {
        RoleMultiplierDict = new Dictionary<ControllerRole, double>
        {
            { ControllerRole.Controller, 1 },
            { ControllerRole.Student, 1 },
            { ControllerRole.Supervisor, 1.5 },
            { ControllerRole.Caoch, 1.5 }
        };
    }

    /// <summary>
    /// 角色系数字典
    /// </summary>
    public static Dictionary<ControllerRole, double> RoleMultiplierDict { get; }

    public static ControllerRole ToConrollerRole(this string roleName)
    {
        switch(roleName)
        {
            case "管制员":
                return ControllerRole.Controller;
            case "学员":
                return ControllerRole.Student;
            case "教员":
                return ControllerRole.Caoch;
            case "领班":
                return ControllerRole.Supervisor;
            default:
                return ControllerRole.Controller;
        }
    }
}