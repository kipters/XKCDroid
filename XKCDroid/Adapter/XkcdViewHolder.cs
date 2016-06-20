using Android.App;
using Android.Content;
using Android.Net;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using XKCDroid.Model;
using XKCDroid.Utils;

namespace XKCDroid.Adapter
{
    public class XkcdViewHolder : RecyclerView.ViewHolder, View.IOnClickListener
    {
        private readonly Activity _context;
        private View _rootView;
        private ImageView _ivEntry;
        private TextView _tvTitle;
        private TextView _tvSubtitle;
        private TextView _tvAlt;

        private string _image;

        public XkcdViewHolder(Activity context, View itemView) : base(itemView)
        {
            _context = context;
            Init(itemView);
        }

        private void Init(View itemView)
        {
            _rootView = itemView;
            _rootView.SetOnClickListener(this);
            _ivEntry = itemView.FindViewById<ImageView>(Resource.Id.ivEntry);
            _tvTitle = itemView.FindViewById<TextView>(Resource.Id.tvTitle);
            _tvSubtitle = itemView.FindViewById<TextView>(Resource.Id.tvSubtitle);
            _tvAlt = itemView.FindViewById<TextView>(Resource.Id.tvAlt);
        }

        public async void Fill(XkcdJson json)
        {
            _ivEntry.SetImageBitmap(null);
            _tvTitle.Text = json.SafeTitle;
            _tvAlt.Text = json.AlternativeText;
            _tvSubtitle.Text = $"{json.Number} - {json.Day}/{json.Month}/{json.Year}";
            _image = json.ImageUrl;
            var image = await NetworkUtils.GetXkcdImageAsync(_context, _image);
            _ivEntry.SetImageBitmap(image);
        }

        public void OnClick(View v)
        {
            var file = NetworkUtils.GetCacheFile(_context, _image);

            if (!file.CanRead() || !file.IsFile)
                return;

            var intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(Uri.Parse($"file://{file.AbsolutePath}"), "image/*");
            _context.StartActivity(intent);
        }
    }
}