// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using AntDesign.Charts;

namespace ATCer.Client.Components
{
    public partial class SADCom
    {
        [Inject]
        protected MQServiceTransciever transciever { get; set; }
        [Parameter]
        public LineConfig Config { get; set; }

        public SADCom()
        {
            Config = new LineConfig()
            {
                Title = new AntDesign.Charts.Title()
                {
                    Visible = TitleVisible,
                    Text = Title,
                },
                Description = new Description()
                {
                    Visible = DescriptionVisible,
                    Text = "用平滑的曲线代替折线。",
                },
                Padding = "auto",
                ForceFit = true,
                XField = "name",
                YField = "data",
                Smooth = true,
                Animation = true,
            };
        }
    }
}
