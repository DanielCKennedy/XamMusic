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
            SizeChanged += NowPlayingPopup_SizeChanged;
        }

        private void NowPlayingPopup_SizeChanged(object sender, EventArgs e)
        {
            var temp = artwork;
            artworkStackLayout.Children.Remove(artwork);
            if (Height > Width)
            {
                temp.HeightRequest = -1;
                temp.WidthRequest = -1;
            }
            else
            {
                temp.HeightRequest = Height / 3;
            }
            
            artworkStackLayout.Children.Add(temp);
        }

        private async void ClosePopup(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync(true);
        }

        
    }
}