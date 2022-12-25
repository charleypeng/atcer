// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Base.Enums;
using ATCer.Client.Base;
using ATCer.Common;
using ATCer.SystemManager.Dtos;
using ATCer.SystemManager.Services;
using Microsoft.AspNetCore.Components;

namespace ATCer.SystemManager.Client.Pages.ResourceView
{
    public partial class ResourceEdit : EditOperationDialogBase<ResourceDto,Guid>
    {
        [Inject]
        IResourceService resourceService { get; set; }
        /// <summary>
        /// 资源父级
        /// </summary>
        public string ParentId 
        {
            get {
                return _editModel.ParentId?.ToString();
            }
            set 
            {
                if (!string.IsNullOrEmpty(value))
                { 
                    _editModel.ParentId = Guid.Parse(value);
                }
            }
        
        }
        /// <summary>
        /// 父级资源
        /// </summary>
        private List<ResourceDto> resources=new List<ResourceDto>();
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<ResourceType, string> resourceTypes = new Dictionary<ResourceType, string>();
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();


            _isLoading = true;
            resources = await resourceService.GetTree();

            if (this.Options.Type.Equals(DrawerInputType.Add))
            {
                _editModel.Id = Guid.NewGuid();

                if (!Guid.Empty.Equals(this.Options.Id))
                {
                    _editModel.ParentId = this.Options.Id;

                    ResourceDto parent = await resourceService.Get(this.Options.Id);

                    _editModel.Type = parent.Type;

                }
            }

            if (this.Options.Type.Equals(DrawerInputType.Select))
            {
                resourceTypes.Add(_editModel.Type, _editModel.Type.GetEnumDescription());
            }
            else
            {
                foreach (var gitem in EnumHelper.EnumToDictionary<ResourceType>())
                {
                    if ((int)gitem.Key >= (int)_editModel.Type)
                    {
                        resourceTypes.Add(gitem.Key, gitem.Value);
                    }
                }
            }
            _isLoading = false;
        }
    }
}
