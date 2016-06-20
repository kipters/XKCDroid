using System;
using Android.Content;
using Android.Runtime;
using Android.Support.V7.Widget;

namespace XKCDroid
{
    public class OverscrollManager : LinearLayoutManager
    {
        public OverscrollManager(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public OverscrollManager(Context context) : base(context)
        {
        }

        public event EventHandler LoadTriggered;

        public override int ScrollVerticallyBy(int dx, RecyclerView.Recycler recycler, RecyclerView.State state)
        {
            var scrollRange = base.ScrollVerticallyBy(dx, recycler, state);
            var overScroll = dx - scrollRange;
            if (overScroll > 0)
                LoadTriggered?.Invoke(this, EventArgs.Empty);

            return scrollRange;
        }
    }
}