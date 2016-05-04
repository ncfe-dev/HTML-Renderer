using TheArtOfDev.HtmlRenderer.Adapters;
using TheArtOfDev.HtmlRenderer.Adapters.Entities;

namespace TheArtOfDev.HtmlRenderer.Core.Dom
{
    // kind of symbolic link for some CssBox.
    // allows to paint the same box several times 
    internal class CssBoxCopy : CssBox
    {
        private CssBox _originalBox;

        public CssBoxCopy(CssBox originalBox) : base(originalBox.ParentBox, originalBox.HtmlTag)
        {
            _originalBox = originalBox;
            this.Display = _originalBox.Display;
        }

        protected override void PerformLayoutImp(RGraphics g)
        {
            Size = _originalBox.Size;
        }

        // offsets clip region to paint original box in different location
        protected override void PaintImp(RGraphics g)
        {
            var offset = HtmlContainer.ScrollOffset;
            HtmlContainer.ScrollOffset = new RPoint(offset.X + this.Location.X - _originalBox.Location.X , offset.Y + this.Location.Y - _originalBox.Location.Y);

            _originalBox.Paint(g);

            HtmlContainer.ScrollOffset = new RPoint(offset.X, offset.Y);
        }
    }
}
