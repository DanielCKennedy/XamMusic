using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamMusic.Controls;
using XamMusic.ViewModels;
using XamMusic.Models;

namespace XamMusic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private RootPage _root;

        public HomePage(RootPage root)
        {
            _root = root;
            this.BindingContext = new HomeViewModel();
            InitializeComponent();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(QueuePopup.Instance);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var item = MenuViewModel.Instance.PlaylistItems.Where(pi => pi.Playlist.Id == ((Playlist)((Grid)sender).BindingContext).Id).FirstOrDefault();
            _root.UpdateSelected(item);
        }

        // Because the Grid TapGesture wasn't registering on iOS
        private void TapGestureRecognizer_Tapped1(object sender, EventArgs e)
        {
            var item = MenuViewModel.Instance.PlaylistItems.Where(pi => pi.Playlist.Id == ((Playlist)((ArtworkImage)sender).BindingContext).Id).FirstOrDefault();
            _root.UpdateSelected(item);
        }
    }
}