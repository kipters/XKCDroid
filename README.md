# XKCDroid

Direct C# port of [XKCDroid](https://drive.google.com/open?id=0By14p6EVys8nZHd4Q2l2STUyRmc) sample by Rocco Versace.

I basically ported the code line by line, adapting it to C# conventions where it made sense.
The only exception is `NetworkUtils.cs`, which was rewritten using the Task Parallel Library.

The app shows how to use RecyclerView and the ViewHolder pattern showing [XKCD](http://xkcd.com) comic strips,
incrementally loading them in groups of ten.