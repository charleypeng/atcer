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

namespace ATCer.HRCenter
{
    public static class ImportItemExtension
    {
        public static ATCDepartment ToDepartment(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new Exception("can not transfer into department");
            }
            var departmentValue = str;

            if (departmentValue.Contains("ZT") || departmentValue.Contains("TWR"))
            {
                return ATCDepartment.TWR;
            }
            else if (departmentValue.Contains("AC") || departmentValue.Contains("ZRZX"))
            {
                return ATCDepartment.ACC;
            }

            else if (departmentValue.Contains("AP"))
            {
                return ATCDepartment.APP;
            }
            else
            {
                throw new Exception($"未知的物理席位地址 {str}");
            }
        }
    }
}
