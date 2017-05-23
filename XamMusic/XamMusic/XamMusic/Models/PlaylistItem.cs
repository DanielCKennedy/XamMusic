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

        public Command AddSong { get; set; }

        public PlaylistItem(Playlist playlist)
        {
            Playlist = playlist;
            if (Playlist.IsDynamic)
            {
                AddSong = new Command(() =>
                {
                    Task.Run(async () =>
                    {
                        await DependencyService.Get<IPlaylistManager>().AddToPlaylist(
                            Playlist,
                            MusicStateViewModel.Instance.SelectedSong);
                        playlist.Songs = await DependencyService.Get<IPlaylistManager>().GetPlaylistSongs(
                            playlist.Id);
                        //System.Diagnostics.Debug.WriteLine("Instance = " + PlaylistViewModel.Instance);
                        if (PlaylistViewModel.Instance != null)
                        {
                            PlaylistViewModel.Instance.Songs = playlist.Songs;
                        }
                    });
                    
                    
                });
            }
        }
    }
}
