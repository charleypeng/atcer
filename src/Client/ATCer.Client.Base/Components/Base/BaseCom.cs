namespace ATCer.Client.Components
{
    public abstract class BaseCom:ComponentBase, IBaseCom
    {
        public bool IsFirstRender { get; set; }
        /// <summary>
        /// 是否繁忙
        /// </summary>
        [Parameter]
        public bool IsBusy { get; set; }
        /// <summary>
        /// 宽
        /// </summary>
        [Parameter]
        public int Width { get; set; } = 200;
        /// <summary>
        /// 长
        /// </summary>
        [Parameter]
        public int Height { get; set; } = 50;
        /// <summary>
        /// 显示骨架
        /// </summary>
        [Parameter]
        public bool ShowSkeleton { get; set; } = true;
    }
}
