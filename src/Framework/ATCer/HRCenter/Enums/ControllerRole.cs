// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.HRCenter.Enums
{
    /// <summary>
    /// 管制职位
    /// </summary>
    public enum ControllerRole
    {
        /// <summary>
        /// 管制员
        /// </summary>
        [Description("管制员")]
        Controller = 0,
        /// <summary>
        /// 学员
        /// </summary>
        [Description("学员")]
        Student = 1,
        /// <summary>
        /// 教员
        /// </summary>
        [Description("教员")]
        Caoch = 2,
        /// <summary>
        /// 带班
        /// </summary>
        [Description("领班")]
        Supervisor = 3,
    }

    public static class ControllerConf
    {
        static ControllerConf()
        {
            RoleMultiplierDict = new Dictionary<ControllerRole, double>();
            RoleMultiplierDict.Add(ControllerRole.Controller, 1);
            RoleMultiplierDict.Add(ControllerRole.Student, 1);
            RoleMultiplierDict.Add(ControllerRole.Supervisor, 1.5);
            RoleMultiplierDict.Add(ControllerRole.Caoch, 1.5);
        }
        public static Dictionary<ControllerRole, double> RoleMultiplierDict { get; }

    }
}
