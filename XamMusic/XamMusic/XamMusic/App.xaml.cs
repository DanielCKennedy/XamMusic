using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamMusic.Controls;
using XamMusic.ViewModels;
using XamMusic.Views;

namespace XamMusic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // initialize MusicStateViewModel to load current playlist as app loads
            var a = MusicStateViewModel.Instance;
            var b = QueuePopup.Instance;
            var c = SliderControl.Instance;

            MainPage = new RootPage();
        }
    }
}