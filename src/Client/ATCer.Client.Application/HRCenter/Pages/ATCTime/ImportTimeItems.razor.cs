// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.ComponentModel;
using AntDesign.ProLayout;
using ATCer.HRCenter.Dtos;
using ATCer.HRCenter.Services;
using ATCer.Attachment.Dtos;
using ATCer.Attachment.Enums;
using ATCer.Base;
using ATCer.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace ATCer.HRCenter.Client.Pages.ATCTime
{

    public partial class ImportTimeItems: INotifyPropertyChanged, IReuseTabsPage
    {
        // time items property
        private IEnumerable<TimeItemDto> _timeItems;
        private IEnumerable<TimeItemDto> timeItems 
        { 
            get { return _timeItems; } 
            set { _timeItems = value; RaisePropertyChanged(nameof(timeItems)); }
        }
        //page list setup
        protected ITable _table;
        private int pageIndex = 1;
        private int pageSize = 10;
        private int totalPages = 0;
        //upload button loading
        private bool loading = false;
        //table loading indicator
        private bool isTableLoading = false;
        //import now button enabled
        private bool importNowDisabled = true;
        //headers
        private Dictionary<string, string> headers;
        /// <summary>
        /// 上传附件附带参数
        /// </summary>
        private Dictionary<string, object> uploadAttachmentInput = new Dictionary<string, object>()
        {
            { "BusinessType", AttachmentBusinessType.TimeItem}
        };
        //time items count
        private int totalCount = 0;
        //time item changed event
        public event PropertyChangedEventHandler PropertyChanged;
        private bool firstRenderAfter = false;
        [Inject]
        private ITimeItemService timeItemService { get; set; }
        [Inject]
        MessageService messagerService { get; set; }
        [Inject]
        IOptions<ApiSettings> apiSettings { get; set; }
        [Inject]
        IAuthenticationStateManager authenticationStateManager { get; set; }
        [Inject]
        protected ConfirmService confirmService { get; set; }
        [Inject]
        private IClientLocalizer localizer { get; set; }

        /// <summary>
        /// 上传地址
        /// </summary>
        public string UploadUrl
        {
            get
            {
                return apiSettings.Value.BaseAddres + apiSettings.Value.UploadPath;
            }
        }

        protected override async Task OnInitializedAsync()
        {
            isTableLoading = true;
            _timeItems = new List<TimeItemDto>();
            uploadAttachmentInput.Add("BusinessId", null);
            headers = await authenticationStateManager.GetCurrentTokenHeaders();
            await base.OnInitializedAsync();
        }

        private void ImportTimeItems_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(timeItems))
            {
                totalCount = timeItems.Count();

                importNowDisabled = totalCount > 0 ? false : true;
            }   
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                this.firstRenderAfter = true;
                await ReLoadTable(true);
                this.PropertyChanged += ImportTimeItems_PropertyChanged;
                await InvokeAsync(StateHasChanged);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        //handle file extension before upload
        private bool BeforeUpload(UploadFileItem file)
        {
            var typeOk =  file.Ext.ToLower() == ".xlsx";
            if (!typeOk)
            {
                messagerService.Error(localizer["仅支持.xlsx文件上传"]);
            }
            var sizeOk = file.Size / 1024 < 50000;
            if (!sizeOk)
            {
                messagerService.Error("文件必须小于50M！");
            }
            return typeOk && sizeOk;
            //var result = confirmService.YesNo("询问", "这将删除之前导入的文件，确定吗？").Result;

            //return result == ConfirmResult.Yes ? true : false;
        }

        //handle file change from input
        async Task HandleChange(UploadInfo fileinfo)
        {
            isTableLoading = true;
            loading = fileinfo.File.State == UploadState.Uploading;

            try
            {
                if (fileinfo.File.State == UploadState.Success)
                {
                    ApiResult<UploadAttachmentOutput> apiResult = fileinfo.File.GetResponse<ApiResult<UploadAttachmentOutput>>
                        (new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (apiResult.Succeeded)
                    {
                        timeItems = new List<TimeItemDto>();
                        //更新到数据库
                        var state = await timeItemService.ImportViaId(apiResult.Data.Id);

                        if (state.Succeed)
                        {
                            await messagerService.Success(localizer["添加小时成功"]);
                            await ReLoadTable(true);
                        }
                        else
                        {
                            await messagerService.Success(localizer["添加小时失败"]);
                        }
                    }
                    else
                    {
                        await messagerService.Error($"{apiResult.Errors} [{apiResult.StatusCode}]");
                        await messagerService.Error(localizer["上传失败"]);
                    }
                }
                else if (fileinfo.File.State == UploadState.Fail)
                {
                    await messagerService.Error(localizer["上传失败"]);
                }
            }
            catch
            {

            }
            finally
            {
                isTableLoading = false;
            }
        }

        protected virtual MyPageRequest GetPageRequest()
        {
            MyPageRequest pageRequest = _table?.GetPageRequest() ?? new MyPageRequest();
            return pageRequest;
        }

        //load the last time imported time items
        private async Task LoadLastImported()
        {
            await ReLoadTable(true);
        }

        //reload the page list table

        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <param name="firstPage">是否从首页加载</param>
        /// <returns></returns>
        protected virtual async Task ReLoadTable(bool firstPage)
        {
            if (firstPage)
            {
                pageIndex = 1;
            }
            MyPageRequest pageRequest = GetPageRequest();
            await ReLoadTable(pageRequest.PageIndex,pageRequest.PageSize);
        }

        private async Task ReLoadTable()
        {
            await ReLoadTable(false);
        }
        private async Task ReLoadTable(int pageIndex = 1, int pageSize = 10)
        {
            isTableLoading = true;
            var pr = GetPageRequest();
            var pageListResult = await timeItemService.GetImported(pr.PageIndex, pr.PageSize);
            
            if (pageListResult != null)
            {
                timeItems = pageListResult.Items;
                totalCount = pageListResult.TotalCount;
                totalPages = pageListResult.TotalPages;
            }
            else
            {
                await messagerService.Error(localizer.Combination("加载", "失败"));
            }
            isTableLoading = false;
            if (!pageListResult.Items.IsNullOrEmpty())
                importNowDisabled = false;
        }
        //import time items
        private async Task ImportNow()
        {
            if(timeItems.Count() == 0)
            {
                await messagerService.Info(localizer["没有可以导入的数据"]);
                return;
            }
            var result = await confirmService.YesNo(localizer["询问"], localizer["确定导入所有数据？"]);
            if (result == ConfirmResult.No)
                return;

            var isSuccess = await timeItemService.ImportNow();
            if(isSuccess)
            {
                await messagerService.Success(localizer["导入成功"]);
                timeItems = new List<TimeItemDto>();
            }
            else
            {
                await messagerService.Error(localizer["导入失败"]);
            }
        }
        //delete imported time items
        private async Task DeleteNow()
        {
            if (timeItems.Count() == 0)
            {
                await messagerService.Info(localizer["没有可以删除的数据"]);
                return;
            }
            var result = await confirmService.YesNo(localizer["询问"], localizer["确定删除之前导入的所有数据？"]);
            if (result == ConfirmResult.No)
                return;

            var isSuccess = await timeItemService.DeleteRecentImported();
            if (isSuccess)
            {
                await messagerService.Success(localizer["删除成功"]);
                timeItems = new List<TimeItemDto>();
            }
            else
            {
                await messagerService.Error(localizer["删除失败"]);
            }
        }

        private async Task OnChange()
        {
            if(firstRenderAfter)
            {
                await ReLoadTable();
            }
        }
        //to raise a property change of a time item
        private void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// 获取页面title
        /// </summary>
        /// <returns></returns>
        public RenderFragment GetPageTitle()
        {
            return GetPageTitleValue().ToRenderFragment();
        }

        /// <summary>
        /// 获取页面title
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 根据页面路由path获取对应菜单名字作为title
        /// </remarks>
        public virtual string GetPageTitleValue()
        {
            string title = "";
            RouteAttribute routeAttribute = this.GetType().GetAttribute<RouteAttribute>(true);
            if (routeAttribute != null)
            {
                //根据路由去匹配菜单的名称
                MenuDataItem menu = ClientMenuCache.Get(routeAttribute.Template);
                title = menu?.Name;
            }
            return title;
        }
    }
}
