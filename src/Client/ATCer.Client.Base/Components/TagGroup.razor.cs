// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using AntDesign;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATCer.Client.Components
{
    public partial class TagGroup : AntInputComponentBase<List<string>>
    {

        

        private string _tagInputValue = string.Empty;
        private bool _tagInputVisible = false;

        protected override Task OnInitializedAsync()
        {
            this.UpdateClassMap();
            return base.OnInitializedAsync();
        }

        protected override void OnParametersSet()
        {
            this.UpdateClassMap();
            base.OnParametersSet();
        }

        private void UpdateClassMap()
        {
            string prefix = "ant-tag";
            this.ClassMapper.Clear().Add(prefix)
                .If($"{prefix}-has-color", () => false)
                ;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="tag"></param>
        private void OnCloseTagClick(string tag) 
        {
            this.CurrentValue.Remove(tag);
        }
        /// <summary>
        /// tag输入框失去焦点
        /// </summary>
        private Task HandleTagInputValue()
        {
            if (string.IsNullOrEmpty(_tagInputValue)) return Task.CompletedTask;

            if (!this.CurrentValue.Any(x => x.Equals(_tagInputValue)))
            {
                this.CurrentValue.Add(_tagInputValue);
            }
            this._tagInputValue = string.Empty;
            this._tagInputVisible = false;
            return Task.CompletedTask;
        }
        /// <summary>
        /// 点击添加标签
        /// </summary>
        /// <returns></returns>
        private Task OnTagAddClick()
        {
            _tagInputVisible = !_tagInputVisible;
            return Task.CompletedTask;
        }
    }
}
