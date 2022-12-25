// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.Client.Components
{
    /// <summary>
    /// 可以随机颜色
    /// </summary>
    [Obsolete("请用TagPro代替")]
    public partial class TagRandomColor : AntDomComponentBase
    {

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public object Text { get; set; }

        private string color = "";

        private string[] colors = { "magenta", "pink", "red", "volcano", "orange", "green", "cyan", "blue", "lime", "geekblue", "purple" };

        protected override void OnInitialized()
        {
            int code = Math.Abs(Text.ToString().GetHashCode());
            int colorIndex = (code % 1000) % colors.Length;
            color = colors[colorIndex];
        }

    }
}
