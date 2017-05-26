using System;
using Rg.Plugins.Popup.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamMusic.ViewModels;
using XamMusic.Controls;
using XamMusic.Models;

namespace XamMusic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public ListView PlaylistList => ListViewMenuItems;

        public MenuPage()
        {
            this.BindingContext = MenuViewModel.Instance;
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new CreatePlaylistPopup(), true);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ((Image)sender).Opacity = 0.6;
            ((Image)sender).FadeTo(1, 150);
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            foreach (PlaylistItem item in MenuViewModel.Instance.PlaylistItems)
            {
                item.IsToggled = ((Switch)sender).IsToggled;
            }
        }
    }
}
