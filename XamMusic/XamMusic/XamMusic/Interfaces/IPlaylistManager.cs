using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamMusic.Models;

namespace XamMusic.Interfaces
{
    public interface IPlaylistManager
    {
        void AddToPlaylist(Playlist playlist, Song song);

        Playlist CreatePlaylist(string name);

        IList<Playlist> GetPlaylists();

        IList<Song> GetPlaylistSongs(ulong playlistId); 

        List<Song> GetAllSongs();
    }
}
