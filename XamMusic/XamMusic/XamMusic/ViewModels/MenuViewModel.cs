using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamMusic.Interfaces;
using XamMusic.Models;

namespace XamMusic.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private static MenuViewModel _instance;
        public static MenuViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MenuViewModel();
                }
                return _instance;
            }
        }
        
        private MenuViewModel()
        {
            Refresh();
        }

        private IList<PlaylistItem> _playlistItems;

        public IList<PlaylistItem> PlaylistItems
        {
            get { return _playlistItems; }
            set
            {
                _playlistItems = value;
                OnPropertyChanged(nameof(PlaylistItems));
            }
        }

        public void Refresh()
        {
            PlaylistItems = new ObservableCollection<PlaylistItem>();
            PlaylistItems.Add(new PlaylistItem(
                new Playlist { Title = "Home" },
                false));
            PlaylistItems.Add(new PlaylistItem(
                new Playlist { Title = "All Songs" },
                false));
            var playlists = DependencyService.Get<IPlaylistManager>().GetPlaylists();
            foreach (var playlist in playlists)
            {
                PlaylistItems.Add(new PlaylistItem(playlist, true));
            }
        }
    }
}
