﻿#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor
{
    #region LostButton

    public class LostButton : ControlLostBase
    {
        private Image _image = null;
        public Image Image
        {
            get { return _image; }
            set { _image = value; Invalidate(); }
        }

        private Color _hovercolor = ThemeLost.AccentBrush.Color;
        public Color HoverColor
        {
            get { return _hovercolor; }
            set { _hovercolor = value; Invalidate(); }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(MouseOver ? new SolidBrush(_hovercolor) : new SolidBrush(BackColor), ClientRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_image != null)
                e.Graphics.DrawImage(_image,
                    Width / 2 - _image.Width / 2, Height / 2 - _image.Height / 2,
                    _image.Width, _image.Height);

            SizeF textSize = e.Graphics.MeasureString(Text, ThemeLost.BodyFont);
            e.Graphics.DrawString(Text, ThemeLost.BodyFont, new SolidBrush(ForeColor),
                Width / 2 - textSize.Width / 2, Height / 2 - textSize.Height / 2);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            MouseOver = false;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            MouseOver = true;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            HasShadow = true;
            Parent.Invalidate(ShadeRect(ThemeLost.ShadowSize), false);
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            HasShadow = false;
            Parent.Invalidate(ShadeRect(ThemeLost.ShadowSize), false);
            Invalidate();
        }
    }

    #endregion
}