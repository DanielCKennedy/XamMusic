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
        public NowPlayingPopup()
        {
            System.Diagnostics.Debug.WriteLine("NowPlayingPopup()");
            this.BindingContext = MusicStateViewModel.Instance;
            InitializeComponent();

            // Workaround to avoid song restarting on create in iOS
            SliderStackLayout.Children.Add(SliderControl.Instance);
        }

        private async void ClosePopup(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync(true);
        }
    }
}