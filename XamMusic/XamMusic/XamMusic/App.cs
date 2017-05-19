using MediaPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamMusic.Controls;
using XamMusic.Interfaces;
using XamMusic.Models;
using XamMusic.ViewModels;
using XamMusic.Views;

namespace XamMusic
{
    public class App : Application
    {
        public App()
        {
            // initialize MusicStateViewModel to load current playlist as app loads
            var vm = MusicStateViewModel.Instance;
            var q = QueuePopup.Instance;
            var s = SliderControl.Instance;
            //var p = NowPlayingPopup.Instance;

            MainPage = new RootPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
