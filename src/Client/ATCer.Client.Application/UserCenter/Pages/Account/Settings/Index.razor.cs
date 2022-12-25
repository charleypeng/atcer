// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using AntDesign;
using ATCer;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace ATCer.Client.UserCenter.Pages.Account.Settings
{
    public partial class Index
    {
        [Inject]
        private UserOptions userOptions { get; set; }

        private ElementReference reference;
        private bool shouldRender = true;

        private readonly Dictionary<string, string> _menuMap = new Dictionary<string, string>
        {
            {"base", "基础设置"},
            {"theme", "界面设置"},
            {"security", "安全设置"},
            {"apptoken", "用户令牌"},
            {"notification", "消息设置"},
        };

        private string _selectKey = "base";

        private void SelectKey(MenuItem item)
        {
            _selectKey = item.Key;
        }

        private void SelectTabKey(string key)
        {
            _selectKey = key;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}
