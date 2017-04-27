using AVFoundation;
using Foundation;
using MediaPlayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using XamMusic.Interfaces;
using XamMusic.iOS;
using XamMusic.Models;

[assembly: Dependency(typeof(PlaylistManagerIOS))]
namespace XamMusic.iOS
{
    public class PlaylistManagerIOS : IPlaylistManager
    {
        public void AddToPlaylist(Playlist playlist, Song song)
        {
            MPMediaQuery mq = MPMediaQuery.PlaylistsQuery;
            MPMediaItemCollection[] playlistArray = mq.Collections;

            foreach (MPMediaPlaylist pl in playlistArray)
            {
                if (pl.PersistentID == playlist.Id)
                {
                    MPMediaQuery m = MPMediaQuery.SongsQuery;
                    var p = MPMediaPropertyPredicate.PredicateWithValue(NSNumber.FromUInt64(song.Id), MPMediaItem.PersistentIDProperty);
                    m.AddFilterPredicate(p);
                    if (m.Items.Length > 0)
                    {
                        pl.AddMediaItems(m.Items, (err) =>
                        {
                            if (err != null)
                                err.ToString();
                        });
                    }
                }
            }
        }

        public Playlist CreatePlaylist(string name)
        {
            MPMediaPlaylist playlist = MPMediaLibrary.DefaultMediaLibrary.GetPlaylistAsync(new NSUuid(), new MPMediaPlaylistCreationMetadata(name)).Result;

            if (playlist != null)
            {
                return new Playlist
                {
                    Id = playlist.PersistentID,
                    Title = playlist.Name,
                    Count = 0
                };
            }
            return null;
        }

        public List<Song> GetAllSongs()
        {
            List<Song> songs = new List<Song>();

            MPMediaQuery mq = new MPMediaQuery();
            mq.GroupingType = MPMediaGrouping.Title;
            var value = NSNumber.FromInt32((int)MPMediaType.Music);
            var predicate = MPMediaPropertyPredicate.PredicateWithValue(value, MPMediaItem.MediaTypeProperty);
            mq.AddFilterPredicate(predicate);
            var items = mq.Items;

            foreach (var item in items)
            {
                if (item != null && !item.IsCloudItem && item.AssetURL != null)
                {
                    songs.Add(new Song
                    {
                        Id = item.PersistentID,
                        Title = item.Title,
                        Artist = item.Artist,
                        Album = item.AlbumTitle,
                        Genre = item.Genre,
                        Artwork = item.Artwork,
                        Duration = (ulong)item.PlaybackDuration * 1000,
                        Uri = item.AssetURL.AbsoluteString
                    });
                }
            }

            return songs;
        }

        public IList<Playlist> GetPlaylists()
        {
            List<Playlist> playlists = new List<Playlist>();

            MPMediaQuery mq = MPMediaQuery.PlaylistsQuery;
            MPMediaItemCollection[] playlistArray = mq.Collections;

            foreach (var playlist in playlistArray)
            {
                playlists.Add(new Playlist
                {
                    Id = ulong.Parse(playlist.ValueForProperty(MPMediaPlaylistProperty.PersistentID).ToString()),
                    Title = playlist.ValueForProperty(MPMediaPlaylistProperty.Name).ToString()
                });
            }

            return playlists;
        }

        public IList<Song> GetPlaylistSongs(ulong playlistId)
        {
            List<Song> songs = new List<Song>();

            MPMediaQuery mq = MPMediaQuery.SongsQuery;
            var value = NSNumber.FromUInt64(playlistId);
            MPMediaPropertyPredicate predicate = MPMediaPropertyPredicate.PredicateWithValue(value, MPMediaPlaylistProperty.PersistentID);
            mq.AddFilterPredicate(predicate);
            var items = mq.Items;

            foreach (var item in items)
            {
                if (item != null && item.AssetURL != null)
                {
                    songs.Add(new Song
                    {
                        Id = item.PersistentID,
                        Title = item.Title,
                        Artist = item.Artist,
                        Album = item.AlbumTitle,
                        Genre = item.Genre,
                        Artwork = item.Artwork,
                        Duration = (ulong)item.PlaybackDuration * 1000,
                        Uri = item.AssetURL.AbsoluteString
                    });
                }
            }

            return songs;
        }
    }
}
