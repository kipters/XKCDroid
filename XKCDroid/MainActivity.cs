using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using XKCDroid.Adapter;
using XKCDroid.Utils;

namespace XKCDroid
{
    [Activity(MainLauncher = true, Label = "@string/app_name", Icon = "@mipmap/ic_launcher", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        private RecyclerView _recyclerView;
        private XkcdRvAdapter _adapter;
        private OverscrollManager _layoutManager;
        private int _latest;
        private int _running;
        private readonly object _lockObject = new object();

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);
            _adapter = new XkcdRvAdapter(this);
            _recyclerView = FindViewById<RecyclerView>(Resource.Id.rvList);
            _layoutManager = new OverscrollManager(this);

            _layoutManager.LoadTriggered += (sender, args) => LoadData();

            _recyclerView.SetLayoutManager(_layoutManager);
            _recyclerView.SetAdapter(_adapter);

            var data = await NetworkUtils.GetXkcdDataAsync(this);
            _adapter.Add(data);
            _latest = data.Number - 1;
            LoadData();
        }

        private async void LoadData()
        {
            if (IsRunning || _latest <= 0)
                return;

            SetRunning(10);

            for (var i = 0; i < 10; i++)
            {
                if (_latest <= 0)
                    continue;

                var data = await NetworkUtils.GetXkcdDataAsync(this, --_latest);
                _adapter.Add(data);
                DecRunning();
            }
        }

        private bool IsRunning
        {
            get
            {
                lock (_lockObject)
                {
                    return _running > 0;
                }
            }
        }

        private void SetRunning(int running)
        {
            lock (_lockObject)
            {
                _running = running;
            }
        }

        private void DecRunning()
        {
            lock (_lockObject)
            {
                _running--;
            }
        }
    }
}

