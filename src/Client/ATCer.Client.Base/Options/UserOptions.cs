// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;

namespace ATCer
{
    public partial class UserOptions : ObservableModel
    {
        private bool _darkMode = false;
        public bool DarkMode {get{ return _darkMode; } set { _darkMode = value; RaisePropertyChanged(nameof(DarkMode)); } }
        public Theme  Theme { get; set; }
        public string RightContentSize { get; set; } = "large";
    }
}
