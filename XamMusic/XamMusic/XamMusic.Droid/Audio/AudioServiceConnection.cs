using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamMusic.Droid.Audio
{
    public class AudioServiceConnection : Java.Lang.Object, IServiceConnection
    {
        private MainActivity _activity;

        public AudioServiceConnection(MainActivity activity)
        {
            _activity = activity;
        }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            if (service != null)
            {
                var binder = (AudioServiceBinder)service;
                MainActivity.Binder = binder;
                _activity.IsBound = true;
            }
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            _activity.IsBound = false;
        }
    }
}