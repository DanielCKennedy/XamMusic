using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamMusic.Interfaces;
using XamMusic.ViewModels;

namespace XamMusic.Models
{
    public class PlaylistItem
    {
        public Playlist Playlist { get; set; }

        public bool IsDynamic { get; set; }

        public Command AddSong { get; set; }

        public PlaylistItem(Playlist playlist, bool isDynamic)
        {
            Playlist = playlist;
            IsDynamic = isDynamic;
            if (IsDynamic)
            {
                AddSong = new Command(() =>
                {
                    DependencyService.Get<IPlaylistManager>().AddToPlaylist(
                        Playlist,
                        MusicStateViewModel.Instance.SelectedSong);
                });
            }
        }
    }
}
