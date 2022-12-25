// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.HRCenter.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATCer.HRCenter.Resources;

namespace ATCer.HRCenter.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class ATCDepartmentNode
    {
        public ATCDepartment Department { get; set; }

        public string DepartmentName
        {
            get { return ATCDepartmentRes.ResourceManager.GetString(Department.ToString()); }
        }
    }
}
