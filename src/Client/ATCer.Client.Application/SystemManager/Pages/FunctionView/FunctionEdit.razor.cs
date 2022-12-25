// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using ATCer.SystemManager.Dtos;
using System.ComponentModel.DataAnnotations;
using MyHttpMethod = ATCer.Enums.MyHttpMethod;

namespace ATCer.SystemManager.Client.Pages.FunctionView
{
    public partial class FunctionEdit: EditOperationDialogBase<FunctionDto,Guid>
    {
        [Required(ErrorMessage ="不能为空")]
        private string _currentEditModelHttpMethodType
        {
            get
            {
                return _editModel.Method.ToString();
            }
            set
            {
                _editModel.Method = (MyHttpMethod)Enum.Parse(typeof(MyHttpMethod), value);
            }
        }
        
       
    }
}
