using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamMusic.ViewModels;

namespace XamMusic.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NowPlayingPopup : PopupPage
    {
        private static NowPlayingPopup _instance;
        public static NowPlayingPopup Instance
        {
            get
            {
                System.Diagnostics.Debug.WriteLine("NowPlayingPopup.Instance");
                if (_instance == null)
                {
                    _instance = new NowPlayingPopup();
                }
                return _instance;//new NowPlayingPopup();//_instance;
            }
        }

        private NowPlayingPopup()
        {
            System.Diagnostics.Debug.WriteLine("NowPlayingPopup()");
            this.BindingContext = MusicStateViewModel.Instance;
            InitializeComponent();
        }

        private async void ClosePopup(object sender, EventArgs e)
        {
            //await Navigation.PopAllPopupAsync(true);
            await Navigation.PopPopupAsync(true);
            if (_instance == null)
            {
                System.Diagnostics.Debug.WriteLine("_instance is null");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Not NULL");
                System.Diagnostics.Debug.WriteLine(_instance);
            }
        }
    }
}