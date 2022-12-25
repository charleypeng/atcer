// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.LTFATCenter.Dtos;
using ATCer.LTFATCenter.Services;
using ATCer.LTFATCenter.Enums;
using ATCer.Common;
using Microsoft.AspNetCore.Components;
using AntDesign.Charts;
using Title = AntDesign.Charts.Title;
using ATCer.EventBus;
using ATCer.MessageQueue.Dtos;
using ATCer.MessageQueue.Events;
using Microsoft.JSInterop;

namespace ATCer.LTFATCenter.Client.Pages
{
    public partial class FlightPlanAdmin : ListTableBase<FlightPlanDto, long, FlightPlanEdit>
    {
        MECharts Mychart;
        MECharts Mychart2;
       // [Inject] private IFileReader fileReader { get; set; }
        object optSurface;//=> SurfaceOption.Option;
        [Inject]
        private IJSRuntime js { get; set; }
        private bool isLoading { get; set; }
        [Inject]
        private IDashboardService _dashboardService { get; set; }
        IChartComponent chart1;
        IChartCom chartWow;
        IChartCom bt2;
        public bool isBusy { get; set; } = true;
        private IList<object> data3 { get; set; }
        [Inject]
        private IEventBus eventBus { get; set; }
        List<object> data = new List<object> {
        new  { data= "NORTH", name= 3 },
        new  { data= "SOTH", name= 4 },
        new  { data= "1993", name= 3.5 },
        new  { data= "1994", name= 5 },
        new  { data= "1995", name= 4.9 },
        new  { data= "1996", name= 6 },
        new  { data= "1997", name= 7 },
        new  { data= "1998", name= 9 },
        new  { data= "1999", name= 13 },
        };
        object[] data2 = new object[] {264,
                                      417,
                                      438,
                                      887,
                                      309,
                                      397,
                                      550,
                                      575,
                                      563,
                                      430,
                                      525,
                                      592,
                                      492,
                                      467,
                                      513,
                                      546,
                                      983,
                                      340,
                                      539,
                                      243,
                                      226,
                                      192, };
        AntDesign.Charts.LineConfig config = new AntDesign.Charts.LineConfig()
        {
            Title = new Title()
            {
                Visible = true,
                Text = "曲线折线图",
            },
            Description = new AntDesign.Charts.Description()
            {
                Visible = true,
                Text = "用平滑的曲线代替折线。",
            },
            Padding = "auto",
            ForceFit = true,
            XField = "year",
            YField = "value",
            Smooth = true,
        };
        static int counter = 1;

        public bool DarkMode { get; set; }
        protected override async Task OnInitializedAsync()
        {
            isBusy = true;
            data3 = await _dashboardService.GetSIDStats();
            if (data3 == null)
                data3 = new List<object>();
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(data3));
            //subscribe
            eventBus.Subscribe<MQEventInfo<MQData>>(EventCallBack);

            //optSurface = await fileReader.ReadAsJsonAsync<object>("wwwroot/test.json");
            isBusy = false;
        }


        private async Task EventCallBack(MQEventInfo<MQData> e)
        {
            MQData data = e.Data;
            if (data.MQTopic == "TESTDATA")
            {
                Console.WriteLine(data.Data);
            }
        }

        private async Task OnChartAdd()
        {
            var dt = new { Name = $"{1999 + counter}", Data = 13 + counter };
            data3.Add(dt);
            data.Add(new { name = $"{1999 + counter}", data = 13 + counter });
            await chart1.ChangeData(data, true);
            Console.WriteLine("添加了1次" + $"{dt.ToString()}");
            counter++;
            bt2?.RefreshData();
        }
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public readonly static TableFilter<FlightStatus>[] FunctionFlightStatusFilters = EnumHelper.EnumToList<FlightStatus>().Select(x => { return new TableFilter<FlightStatus>() { Text = x.ToString(), Value = x }; }).ToArray();
        public readonly static TableFilter<PlanStatus>[] FunctionPlanStatusFilters = EnumHelper.EnumToList<PlanStatus>().Select(x => { return new TableFilter<PlanStatus>() { Text = x.ToString(), Value = x }; }).ToArray();
        public readonly static TableFilter<WakeTurbulence>[] FunctionWakeTurbulenceFilters = EnumHelper.EnumToList<WakeTurbulence>().Select(x => { return new TableFilter<WakeTurbulence>() { Text = x.ToString(), Value = x }; }).ToArray();

        private static double loft = 0;
        private GaugeOption go = new GaugeOption
        {
            series = new List<Series>
            {
                new Series
                {
                    title = "gauge",
                    center = new string[]{"50%","60%" },
                    startAngle = 200,
                    endAngle = -20,
                    min = 0,
                    max = 60,
                    splitNumber = 12,
                    itemStyle = new ItemStyle{ color = "#FFAB91"},
                    progress = new ATCer.Client.Components.Progress{show = true, width=30},
                    pointer = new Pointer{show = true},
                    axisLine = new AxisLine{lineStyle = new ATCer.Client.Components.LineStyle{width=30}},
                    axisTick = new AxisTick{distance = -45, splitNumber = 5, lineStyle = new ATCer.Client.Components.LineStyle{width=2, color = "#999"}},
                    splitLine = new SplitLine{distance = -52, length = 14, lineStyle = new ATCer.Client.Components.LineStyle{width=3,color="#999"}},
                    axisLabel = new AxisLabel{distance = -20, color="#999", fontSize=20},
                    anchor = new ATCer.Client.Components.Anchor{show = true},
                    detail = new Detail{valueAnimation = true, width="60%",lineHeight=40,borderRadius=8, offsetCenter=new object[]{0,"-15%" }, fontSize=60, fontWeight="bolder",formatter=$"{loft}°C",color="auto" },
                    data = new {value = loft }
                }
            }
        };
    }
}
