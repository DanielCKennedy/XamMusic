using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamMusic.Models;
using Xamarin.Forms;
using XamMusic.Interfaces;

namespace XamMusic.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private IList<Playlist> _playlists;

        public IList<Playlist> Playlists
        {
            get
            {
                return _playlists ?? new List<Playlist>();
            }
            set
            {
                _playlists = value;
                OnPropertyChanged(nameof(Playlists));
            }
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }


        public HomeViewModel()
        {
            IsLoading = true;
            Task.Run(async () =>
            {
                var pls = DependencyService.Get<IPlaylistManager>().GetPlaylists().OrderBy(p => p.DateModified).ToList();
                foreach (Playlist pl in pls)
                {
                    pl.Songs = await DependencyService.Get<IPlaylistManager>().GetPlaylistSongs(pl.Id);
                    if (pl.Songs?.Count > 0)
                    {
                        foreach (Song song in pl.Songs)
                        {
                            if (song.HasArtwork)
                            {
                                pl.Artwork = song.Artwork;
                                break;
                            }
                        }
                    }
                }
                IsLoading = false;
                Playlists = pls;
            });
            

        }

    }
}
