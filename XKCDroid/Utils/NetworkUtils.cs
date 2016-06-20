using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Newtonsoft.Json;
using XKCDroid.Model;
using File = Java.IO.File;

namespace XKCDroid.Utils
{
    public static class NetworkUtils
    {
        private const string XkcdLatestUrl = "http://xkcd.com/info.0.json";
        private const string XkcdBaseUrl = "http://xkcd.com/{0}/info.0.json";

        public static async Task<Bitmap> GetXkcdImageAsync(Activity context, string url)
        {
            var fl = GetCacheFile(context, url);
            var bitmap = GetFromCache(fl);

            if (bitmap == null)
            {
                bitmap = await GetXkcdImageAsync(url);
                if (MarshmallowPermissions.RequestWriteStorage(context))
                    await SaveToCacheAsync(bitmap, fl);
            }

            return bitmap;
        }

        private static async Task SaveToCacheAsync(Bitmap bitmap, File fl)
        {
            if (bitmap == null)
                return;

            FileStream fs = null;

            try
            {
                fs = System.IO.File.OpenWrite(fl.AbsolutePath);
                await bitmap.CompressAsync(Bitmap.CompressFormat.Jpeg, 100, fs);
            }
            catch (Exception)
            {
                fl.Delete();
            }
            finally
            {
                fs?.Flush();
                fs?.Close();
            }
        }

        private static async Task<Bitmap> GetXkcdImageAsync(string url)
        {
            try
            {
                var wc = new WebClient();
                var data = await wc.DownloadDataTaskAsync(url);
                var bitmap = await BitmapFactory.DecodeByteArrayAsync(data, 0, data.Length);
                return bitmap;
            }
            catch
            {
                return null;
            }
        }

        private static Bitmap GetFromCache(File fl)
        {
            try
            {
                var bitmap = fl.CanRead()
                    ? BitmapFactory.DecodeFile(fl.AbsolutePath)
                    : null;

                return bitmap;
            }
            catch (Exception)
            {
                fl.Delete();
                return null;
            }
        }

        public static File GetCacheFile(Activity context, string url) => 
            new File(context.ExternalCacheDir, url.Substring(url.LastIndexOf('/') + 1));

        public static Task<XkcdJson> GetXkcdDataAsync(Context context) => GetXkcdDataAsync(context, -1);

        public static async Task<XkcdJson> GetXkcdDataAsync(Context context, int id)
        {
            try
            {
                var wc = new WebClient();
                var url = id > 0
                    ? string.Format(XkcdBaseUrl, id)
                    : XkcdLatestUrl;

                var data = await wc.DownloadStringTaskAsync(url);
                return JsonConvert.DeserializeObject<XkcdJson>(data);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}