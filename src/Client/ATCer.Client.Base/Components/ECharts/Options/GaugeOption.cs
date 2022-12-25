// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.Client.Components
{
    public class Anchor
    {
        public bool show;
    }

    public class AxisLabel
    {
        public int distance;
        public string color;
        public int fontSize;
        public bool? show;
    }

    public class AxisLine
    {
        public LineStyle lineStyle;
        public bool? show;
    }

    public class AxisTick
    {
        public int distance;
        public int splitNumber;
        public LineStyle lineStyle;
        public bool? show;
    }

    public class Detail
    {
        public bool valueAnimation;
        public string width;
        public int lineHeight;
        public int borderRadius;
        public object[] offsetCenter;
        public int fontSize;
        public string fontWeight;
        public string formatter;
        public string color;
        public bool? show;
    }

    public class ItemStyle
    {
        public string color;
    }

    public class LineStyle
    {
        public int width;
        public string color;
    }

    public class Pointer
    {
        public bool show;
    }

    public class Progress
    {
        public bool show;
        public double width;
    }

    public class GaugeOption
    {
        public List<Series> series;
    }

    public class Series
    {
        public string type;
        public string[] center;
        public double startAngle;
        public double endAngle;
        public int min;
        public int max;
        public int splitNumber;
        public ItemStyle itemStyle;
        public Progress progress;
        public Pointer pointer;
        public AxisLine axisLine;
        public AxisTick axisTick;
        public SplitLine splitLine;
        public AxisLabel axisLabel;
        public Anchor anchor;
        public string title;
        public Detail detail;
        public object data;
    }

    public class SplitLine
    {
        public int distance;
        public int length;
        public LineStyle lineStyle;
        public bool? show;
    }
}
