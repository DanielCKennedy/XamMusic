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
                if (_instance == null)
                {
                    _instance = new NowPlayingPopup();
                }
                return _instance;
            }
        }

        private NowPlayingPopup()
        {
            this.BindingContext = MusicStateViewModel.Instance;
            InitializeComponent();
        }

        private async void ClosePopup(object sender, EventArgs e)
        {
            await Navigation.PopAllPopupAsync(true);
        }
    }
}