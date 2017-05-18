using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XamMusic.Droid;
using XamMusic.Interfaces;
using Xamarin.Forms;
using XamMusic.Models;
using System.Threading.Tasks;
using XamMusic.Droid.Audio;
using System.Collections.ObjectModel;
using System.Threading;

[assembly: Dependency(typeof(MusicManagerDroid))]
namespace XamMusic.Droid
{
    public class MusicManagerDroid : IMusicManager
    {
        private AudioService _audioService;
        private bool _isConnected = false;

        public void Init(Action<bool> IsPlaying, Action<double> GetSongPos, Action<int> GetQueuePos, Action<IList<Song>> GetQueue)
        {
            Task.Run(() =>
            {
                while (MainActivity.Binder == null)
                {
                    System.Threading.Thread.Sleep(100);
                }
                if (MainActivity.Binder != null)
                {
                    _audioService = MainActivity.Binder.GetAudioService();
                    _audioService.Init(IsPlaying, GetSongPos, GetQueuePos, GetQueue);
                    _isConnected = true;
                }
            });
            
        }

        public async void Next()
        {
            await Task.Run(() =>
            {
                if (_isConnected)
                {
                    _audioService?.Next();
                }
            });            
        }

        public async void Pause()
        {
            await Task.Run(() =>
            {
                if (_isConnected)
                {
                    _audioService?.Pause();
                }
            });
        }

        public async void Play()
        {
            await Task.Run(() =>
            {
                if (_isConnected)
                {
                    _audioService?.Play();
                }
            });
        }

        public async void Prev()
        {
            await Task.Run(() =>
            {
                if (_isConnected)
                {
                    _audioService?.Prev();
                }
            });
        }

        public async void Seek(double position)
        {
            await Task.Run(() =>
            {
                if (_isConnected)
                {
                    _audioService?.Seek(position);
                }
            });
        }

        public async Task SetQueue(IList<Song> songs)
        {
            await Task.Run(() =>
            {
                while (!_isConnected)
                {
                    System.Threading.Thread.Sleep(100);
                }
                if (_isConnected)
                {
                    _audioService?.SetQueue(songs);
                    _audioService?.Prepare(0);
                }
            });
        }

        public async void StartQueue(IList<Song> songs, int pos)
        {
            await Task.Run(() =>
            {
                if (_isConnected)
                {
                    _audioService?.SetQueue(songs);
                    _audioService?.Start(pos);
                }
            });
        }

        public async void Shuffle()
        {
            await Task.Run(() =>
            {
                if (_isConnected)
                {
                    _audioService?.Shuffle();
                }
            });
        }

        public async void Start(int pos)
        {
            await Task.Run(() =>
            {
                if (_isConnected)
                {
                    _audioService?.Start(pos);
                }
            });
        }

        public async void ClearQueue()
        {
            await Task.Run(async () =>
            {
                if (_isConnected)
                {
                    await _audioService?.Stop();
                    Thread.Sleep(100);
                    _audioService?.SetQueue(null);
                    //await _audioService?.Stop();
                    
                    //await _audioService?.Stop();
                    //_audioService?.StopNotification();

                    //_audioService?.SetQueue(new ObservableCollection<Song>());
                }
            });
        }
    }
}