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

        public HomeViewModel()
        {
            Playlists = DependencyService.Get<IPlaylistManager>().GetPlaylists().OrderBy(p => p.DateModified).ToList();
        }

    }
}
