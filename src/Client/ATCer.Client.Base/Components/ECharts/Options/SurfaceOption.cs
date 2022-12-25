// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.Client.Components
{
    public class SurfaceOption
    {
        public static object Option => new
        {
            tooltip = new { },
            backgroundColor = "#fff",
            visualMap = new
            {
                show = false,
                dimension = 2,
                min = -1,
                max = 1,
                inRange = new
                {
                    color = new[]
                    {
                        "#313695",
                        "#4575b4",
                        "#74add1",
                        "#abd9e9",
                        "#e0f3f8",
                        "#ffffbf",
                        "#fee090",
                        "#fdae61",
                        "#f46d43",
                        "#d73027",
                        "#a50026"
                    }
                }
            },
            xAxis3D = new
            {
                type = "value"
            },
            yAxis3D = new
            {
                type = "value"
            },
            zAxis3D = new
            {
                type = "value"
            },
            grid3D = new
            {
                viewControl = new
                {
                    // projection: "orthographic"
                }
            },
            series = new[]
            {
                new
                {
                    type = "surface",
                    wireframe = new
                    {
                        // show: false
                    },
                    equation = new
                    {
                        x = new
                        {
                            step = 0.05
                        },
                        y = new
                        {
                            step = 0.05
                        },
                        z = rock(),
                    }
                }
            }
        };

        public static object  rock()
        {
            if (Math.Abs(0.05) < 0.1 && Math.Abs(0.05) < 0.1)
            {
                return "-";
            }
            var d = Math.Sin(0.05 * Math.PI) * Math.Sin(0.05 * Math.PI);
            return d;
        }
    }
}
