using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            SongsLoading = true;
            OnPropertyChanged(nameof(SongsLoading));
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
                SongsLoading = false;
                OnPropertyChanged(nameof(SongsLoading));
            });

            PlayCommand = new Command((item) =>
            {
                DependencyService.Get<IMusicManager>().StartQueue(new ObservableCollection<Song>(Songs), Songs.IndexOf(item as Song));
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
                OnPropertyChanged(nameof(CountText));
            }
        }
        
        public string CountText
        {
            get
            {
                return _songs != null ? $"{_songs.Count} Songs" : "0 Songs";
            }
        }

        public bool SongsLoading { get; set; }

    }
}
