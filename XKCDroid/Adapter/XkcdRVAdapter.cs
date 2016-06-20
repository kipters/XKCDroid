using System.Collections.Generic;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using XKCDroid.Model;

namespace XKCDroid.Adapter
{
    public class XkcdRvAdapter : RecyclerView.Adapter
    {
        private readonly List<XkcdJson> _list;
        private readonly Activity _context;

        public XkcdRvAdapter(Activity context)
        {
            _list = new List<XkcdJson>();
            _context = context;
        }

        public void Add(XkcdJson json)
        {
            _list.Add(json);
            NotifyDataSetChanged();
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) => 
            (holder as XkcdViewHolder)?.Fill(_list[position]);

        public override int ItemCount => _list.Count;

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.xkcd_item, parent, false);
            return new XkcdViewHolder(_context, v);
        }
    }
}