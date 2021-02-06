﻿#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region SkyTextBox

    public class SkyTextBox : Control
    {
        private readonly TextBox txtbox = new TextBox();

        #region " Control Help - Properties & Flicker Control "

        private bool _passmask = false;
        public bool UseSystemPasswordChar
        {
            get => _passmask;
            set
            {
                txtbox.UseSystemPasswordChar = UseSystemPasswordChar;
                _passmask = value;
                Invalidate();
            }
        }

        private int _maxchars = 32767;
        public int MaxLength
        {
            get => _maxchars;
            set
            {
                _maxchars = value;
                txtbox.MaxLength = MaxLength;
                Invalidate();
            }
        }

        private HorizontalAlignment _align;
        public HorizontalAlignment TextAlignment
        {
            get => _align;
            set
            {
                _align = value;
                Invalidate();
            }
        }

        private bool _multiline = false;
        public bool MultiLine
        {
            get => _multiline;
            set
            {
                _multiline = value;
                Invalidate();
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            txtbox.BackColor = BackColor;
            Invalidate();
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            txtbox.ForeColor = ForeColor;
            Invalidate();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            txtbox.Font = Font;
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            txtbox.Focus();
        }

        public void TextChngTxtBox(object system, EventArgs e)
        {
            Text = txtbox.Text;
        }

        public void TextChng(object system, EventArgs e)
        {
            txtbox.Text = Text;
        }

        public void NewTextBox()
        {
            {
                txtbox.Multiline = false;
                txtbox.BackColor = UnknownBackColor;
                txtbox.ForeColor = ForeColor;
                txtbox.Text = string.Empty;
                txtbox.TextAlign = HorizontalAlignment.Center;
                txtbox.BorderStyle = BorderStyle.None;
                txtbox.Location = new Point(5, 4);
                txtbox.Font = new("Trebuchet MS", 8.25f, FontStyle.Bold);
                txtbox.Size = new Size(Width - 10, Height - 11);
                txtbox.UseSystemPasswordChar = UseSystemPasswordChar;
            }

        }
        #endregion

        #region Variables
        private SmoothingMode _SmoothingType = SmoothingMode.HighQuality;
        private Color _BorderColorA = Color.FromArgb(220, 220, 220);
        private Color _BorderColorB = Color.FromArgb(228, 228, 228);
        private Color _BorderColorC = Color.FromArgb(191, 191, 191);
        private Color _BorderColorD = Color.FromArgb(254, 254, 254);
        private Color _BaseColor = Color.Transparent;
        private Color _UnknownBackColor = Color.FromArgb(43, 43, 43);
        #endregion

        #region Settings
        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        public Color BorderColorA
        {
            get => _BorderColorA;
            set => _BorderColorA = value;
        }

        public Color BorderColorB
        {
            get => _BorderColorB;
            set => _BorderColorB = value;
        }

        public Color BorderColorC
        {
            get => _BorderColorC;
            set => _BorderColorC = value;
        }

        public Color BorderColorD
        {
            get => _BorderColorD;
            set => _BorderColorD = value;
        }

        public Color BaseColor
        {
            get => _BaseColor;
            set => _BaseColor = value;
        }

        public Color UnknownBackColor
        {
            get => _UnknownBackColor;
            set => _UnknownBackColor = value;
        }
        #endregion

        public SkyTextBox() : base()
        {
            NewTextBox();
            Controls.Add(txtbox);

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            Text = "";
            BackColor = Color.FromArgb(233, 233, 233);
            ForeColor = Color.FromArgb(27, 94, 137);
            Font = new("Verdana", 6.75f, FontStyle.Bold);
            Size = new Size(75, 35);
            DoubleBuffered = true;
            txtbox.TextChanged += new EventHandler(TextChngTxtBox);
            base.TextChanged += new EventHandler(TextChng);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            G.SmoothingMode = SmoothingType;

            Height = txtbox.Height + 10;
            {
                txtbox.Width = Width - 10;
                txtbox.TextAlign = TextAlignment;
                txtbox.UseSystemPasswordChar = UseSystemPasswordChar;
            }

            G.Clear(BaseColor);

            LinearGradientBrush innerBorderBrush = new(new Rectangle(1, 1, Width - 3, Height - 3), BorderColorA, BorderColorB, 90);
            Pen innerBorderPen = new(innerBorderBrush);
            G.DrawRectangle(innerBorderPen, new Rectangle(1, 1, Width - 3, Height - 3));
            G.DrawLine(new(BorderColorC), new Point(1, 1), new Point(Width - 3, 1));

            G.DrawRectangle(new(BorderColorD), new Rectangle(0, 0, Width - 1, Height - 1));
            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}