using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using XKCDroid;

namespace It.Rvs.XKCDroid.Widget
{
    public class OutlineTextView : TextView
    {
        private const int DefaultOutlineSize = 6;
        private int _outlineSize = DefaultOutlineSize;
        private Color _outlineColor = new Color(0, 0, 0, 0xFF);
        private int _alpha = 255;
        private int _previousAlpha = 255;

        public OutlineTextView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }

        public OutlineTextView(Context context) : base(context)
        {
            _outlineColor = GetInverseColor(TextColors.DefaultColor);
            _outlineSize = DefaultOutlineSize;
            Init();
        }

        public OutlineTextView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            LoadAttributes(context, attrs);
        }

        public OutlineTextView(Context context, IAttributeSet attrs, int defStyleAttr) : this(context, attrs) { }

        public OutlineTextView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : this(context, attrs) { }

        private void LoadAttributes(Context context, IAttributeSet attrs)
        {
            var a = context.ObtainStyledAttributes(attrs, Resource.Styleable.OutlineTextView, 0, 0);

            _outlineSize = a.GetInt(Resource.Styleable.OutlineTextView_outlineSize, DefaultOutlineSize);
            if (_outlineSize < 0)
                _outlineSize = DefaultOutlineSize;

            _outlineColor = a.GetColor(Resource.Styleable.OutlineTextView_outlineColor, -1);
            if (_outlineColor == -1)
                _outlineColor = GetInverseColor(TextColors.DefaultColor);

            a.Recycle();
        }

        public Color OutlineColor { set { _outlineColor = value; } }

        private Color GetInverseColor(int color)
        {
            var red = Color.GetRedComponent(color);
            var green = Color.GetGreenComponent(color);
            var blue = Color.GetBlueComponent(color);
            var alpha = Color.GetAlphaComponent(color);
            return Color.Argb(alpha, 255 - red, 255 - green, 255 - blue);
        }

        private void Init() => SetPadding(PaddingLeft + _outlineSize, PaddingTop, PaddingRight + _outlineSize, PaddingBottom);

        protected override bool OnSetAlpha(int alpha)
        {
            _alpha = alpha;
            return base.OnSetAlpha(alpha);
        }

        public override void Draw(Canvas canvas)
        {
            Paint.Color = _outlineColor;
            Paint.SetStyle(Android.Graphics.Paint.Style.Stroke);
            Paint.StrokeWidth = _outlineSize;
            _previousAlpha = Paint.Alpha;
            canvas.Save();

            canvas.Translate(CompoundPaddingLeft + _outlineSize, CompoundPaddingTop);
            Layout.Draw(canvas);

            canvas.Restore();
            Paint.Alpha = _previousAlpha;
            Paint.Color = Color.Black;
            Paint.SetStyle(Android.Graphics.Paint.Style.Fill);

            canvas.Save();
            canvas.Translate(_outlineSize, 0);

            base.Draw(canvas);
            canvas.Restore();
        }
    }
}