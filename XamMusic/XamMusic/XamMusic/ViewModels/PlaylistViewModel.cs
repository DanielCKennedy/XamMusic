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
        public static PlaylistViewModel Instance { get; set; }
        public MusicStateViewModel MusicState { get { return MusicStateViewModel.Instance; } }

        public Command PlayCommand { get; set; }

        public ulong Id { get; set; }

        private SongComparer _comparer;
        private Playlist _playlist;

        public PlaylistViewModel(PlaylistItem playlistItem)
        {
            Instance = this;
            _comparer = new SongComparer();
            _playlist = playlistItem.Playlist;
            Id = (ulong)_playlist?.Id;

            Title = playlistItem.Playlist.Title;
            if (_playlist.Songs == null || _playlist.Songs.Count == 0)
            {
                Task.Run(async () =>
                {
                    SongsLoading = true;
                    OnPropertyChanged(nameof(SongsLoading));
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
            }
            else
            {
                Songs = _playlist.Songs;
            }

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

                _playlist.Songs = _songs;
                OnPropertyChanged(nameof(Songs));
                OnPropertyChanged(nameof(CountText));
                OnPropertyChanged(nameof(HasSongs));
            }
        }

        public bool HasSongs { get { return _songs != null && _songs.Count > 0; } }

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
