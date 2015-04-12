namespace MonoUI.Core.Views
{
    public struct Alignment
    {
        public Alignment(HorizontalAlignment horizontal, VerticalAlignment vertical)
            : this()
        {
            Vertical = vertical;
            Horizontal = horizontal;
        }

        public VerticalAlignment Vertical { get; private set; }
        public HorizontalAlignment Horizontal { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}", Horizontal, Vertical);
        }
    }
}