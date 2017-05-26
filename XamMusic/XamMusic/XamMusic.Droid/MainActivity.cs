using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using XamMusic.Droid.Audio;
using FFImageLoading.Forms.Droid;
using Android.Graphics;

namespace XamMusic.Droid
{
    [Activity(Label = "XamMusic", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleInstance, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IJavaObject
    {
        public static MainActivity Instance;

        public bool IsBound = false;
        public Intent AudioServiceIntent;
        public static AudioServiceBinder Binder;

        private AudioServiceConnection _connection;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            // Set Status Bar Color
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Window.SetStatusBarColor(Color.Black);

            // FFImageLoading
            CachedImageRenderer.Init();

            Instance = this;
            AudioServiceIntent = new Intent(Droid.Audio.AudioService.ActionStart);
            ComponentName name = StartService(AudioServiceIntent);
            _connection = new AudioServiceConnection(this);
            bool binded = BindService(AudioServiceIntent, _connection, Bind.AutoCreate);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        protected override void OnDestroy()
        {
            StartService(new Intent(Droid.Audio.AudioService.ActionTryKill));
            base.OnDestroy();
        }
    }
}

