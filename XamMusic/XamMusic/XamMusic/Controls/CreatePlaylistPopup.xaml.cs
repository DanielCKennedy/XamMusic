using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamMusic.Interfaces;
using XamMusic.ViewModels;

namespace XamMusic.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatePlaylistPopup : PopupPage
    {
        public CreatePlaylistPopup()
        {
            InitializeComponent();
        }

        private async void CreatePlaylist(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(PlaylistNameEntry.Text))
            {
                DependencyService.Get<IPlaylistManager>().CreatePlaylist("Untitled Playlist");
            }
            else
            {
                DependencyService.Get<IPlaylistManager>().CreatePlaylist(PlaylistNameEntry.Text);
            }
            MenuViewModel.Instance.Refresh();
            await Navigation.PopAllPopupAsync(true);
        }
    }
}
