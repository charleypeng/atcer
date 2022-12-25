// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.Client.Components
{
    public class RacingBarOption
    {
        public object Option => new
        {
            xAxis = new { max = "dataMax" },
            max = "dataMax",
            yAxis = new
            {
                type = "category",
                data = new[] { "A", "B", "C", "D", "E" },
                inverse = true,
                animationDuration = 300,
                animationDurationUpdate = 300,
                max = 2
            },
            series = new[]
             {
                 new
                 {
                    realtimeSort = true,
                    name = "X",
                    type = "bar",
                    data = new[]{12221,3554,8788,8668,7887 },
                    label = new
                    {
                        show = true,
                        position = "right",
                        valueAnimation = true
                    }
                 }
              },
            legend = new
            {
                show = true
            },
            animationDuration = 0,
            animationDurationUpdate = 3000,
            animationEasing = "linear",
            animationEasingUpdate = "linear"
        };
    }
}
