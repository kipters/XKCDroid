using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using XKCDroid;

namespace It.Rvs.XKCDroid.Widget
{
    public class AutoResizeImageView : ImageView
    {
        private float _aspectRatio;
        private const float DefaultAspectRatio = 1.6f;
        private bool _autoCalculateAspectRatio;

        public AutoResizeImageView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public AutoResizeImageView(Context context) : base(context) { }

        public AutoResizeImageView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            LoadAttributes(context, attrs);
        }

        public AutoResizeImageView(Context context, IAttributeSet attrs, int defStyleAttr) : this(context, attrs) { }

        public AutoResizeImageView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : this(context, attrs) { }

        private void LoadAttributes(Context context, IAttributeSet attrs)
        {
            var a = context.ObtainStyledAttributes(attrs, Resource.Styleable.AutoResizeView, 0, 0);

            _aspectRatio = a.GetFloat(Resource.Styleable.AutoResizeView_aspectRatio, DefaultAspectRatio);
            if (_aspectRatio <= 0)
                _aspectRatio = DefaultAspectRatio;
            a.Recycle();
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);

            if (MeasuredWidth <= 0 || MeasuredHeight <= 0)
                return;

            var aR = _autoCalculateAspectRatio ? (float)MeasuredWidth/MeasuredHeight : _aspectRatio;
            SetMeasuredDimension(MeasuredWidth, (int) (MeasuredWidth / aR));
        }
    }
}