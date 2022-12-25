// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using AntDesign.Charts;
using System.ComponentModel;

namespace ATCer.Client.Components
{
    /// <summary>
    /// 图表控件基类
    /// </summary>
    public abstract class BaseChartCom: BaseCom, INotifyPropertyChanged, IChartCom
    {
        private object data;
        /// <summary>
        /// 数据
        /// </summary>
        [Parameter]
        public virtual object Data
        {
            get { return data; }
            set { data = value; RaisePropertyChanged(nameof(Data)); }
        }
        /// <summary>
        /// 全部数据更改 
        /// </summary>
        [Parameter]
        public virtual bool AllChange { get; set; } = false;
        /// <summary>
        /// 标题
        /// </summary>
        [Parameter]
        public virtual string Title { get; set; } = string.Empty;
        /// <summary>
        /// 标题可见
        /// </summary>
        [Parameter]
        public bool TitleVisible { get; set; } = true;
        /// <summary>
        /// 描述
        /// </summary>
        [Parameter]
        public string Description { get; set; }
        /// <summary>
        /// 描述可见
        /// </summary>
        [Parameter]
        public bool DescriptionVisible { get; set; } = true;
        /// <summary>
        /// 图表控件
        /// </summary>
        public IChartComponent chart;
        /// <summary>
        /// 数据变化
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 网络变化
        /// </summary>
        public event NetworkStatusChangedEventHandler NetWorkStatusChanged;
        /// <summary>
        /// To raise a property changed event by the given property name
        /// </summary>
        /// <param name="propertyName"></param>
        public virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// To raise a property changed event by the given property name 
        /// </summary>
        /// <param name="propertyName"></param>
        public virtual void RaisePropertyChanged(object obj)
        {
            var strName = nameof(obj);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(strName));
        }
        public virtual void RaiseNetworkStatusChanged(object obj, NetworkStatus status)
        {
            NetWorkStatusChanged?.Invoke(this, status);
        }
        /// <summary>
        /// Init
        /// </summary>
        public BaseChartCom()
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BaseCom_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(Data))
            {
                await RefreshData();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            if(data != null)
                await RefreshData();

            await base.OnInitializedAsync();
        }
        /// <summary>
        /// 手动更新数据
        /// </summary>
        public virtual async Task RefreshData()
        {
            if (data == null || chart == null)
                return;

            await chart.ChangeData(data, AllChange);
            var str = System.Text.Json.JsonSerializer.Serialize(data);
            Console.WriteLine($"已更新数据:{str}");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            this.PropertyChanged += BaseCom_PropertyChanged;
        }
    }
}
