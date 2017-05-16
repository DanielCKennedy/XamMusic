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
            string title;
            if (String.IsNullOrWhiteSpace(PlaylistNameEntry.Text))
            {
                title = "Untitled Playlist";
            }
            else
            {
                title = PlaylistNameEntry.Text;
            }

            if (MenuViewModel.Instance.PlaylistItems.Where(r => r.Playlist?.Title == title).Count() > 0)
            {
                int i = 1;
                while (MenuViewModel.Instance.PlaylistItems.Where(q => q.Playlist?.Title == $"{title}{i}").Count() > 0)
                {
                    i++;
                }
                title = $"{title}{i}";
            }

            DependencyService.Get<IPlaylistManager>().CreatePlaylist(title);
            MenuViewModel.Instance.Refresh();
            await Navigation.PopAllPopupAsync(true);
        }
    }
}
