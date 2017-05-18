using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamMusic.Helpers;
using XamMusic.Interfaces;
using XamMusic.Models;

namespace XamMusic.ViewModels
{
    public class PlaylistViewModel : BaseViewModel
    {
        public MusicStateViewModel MusicState { get { return MusicStateViewModel.Instance; } }

        public Command PlayCommand { get; set; }

        private SongComparer _comparer;

        public PlaylistViewModel(PlaylistItem playlistItem)
        {
            _comparer = new SongComparer();
             
            Title = playlistItem.Playlist.Title;
            Task.Run(async () =>
            {
                if (playlistItem.Playlist.Title == "All Songs" && !playlistItem.Playlist.IsDynamic)
                {
                    Songs = await DependencyService.Get<IPlaylistManager>().GetAllSongs();
                }
                else
                {
                    Songs = await DependencyService.Get<IPlaylistManager>().GetPlaylistSongs(playlistItem.Playlist.Id);
                }
            });

            PlayCommand = new Command((item) =>
            {
                DependencyService.Get<IMusicManager>().StartQueue(Songs, Songs.IndexOf(item as Song));
            });
        }

        public string Title { get; set; }

        private IList<Song> _songs;

        public IList<Song> Songs
        {
            get { return _songs; }
            set
            {
                _songs = value;
                OnPropertyChanged(nameof(Songs));
            }
        }

    }
}
