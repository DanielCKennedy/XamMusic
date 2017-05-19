using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using XamMusic.Interfaces;
using Xamarin.Forms;
using XamMusic.Models;
using XamMusic.iOS;
using AVFoundation;
using System.Collections.ObjectModel;
using CoreMedia;
using CoreFoundation;
using XamMusic.Helpers;
using System.Threading.Tasks;
using System.Threading;
using MediaPlayer;

[assembly: Dependency(typeof(MusicManagerIOS))]
namespace XamMusic.iOS
{
    public class MusicManagerIOS : NSObject, IMusicManager
    {
        private AVPlayer _player;
        private IList<Song> _queue;
        private int _pos;

        private Action<bool> _isPlaying;
        private Action<double> _getPosition;
        private Action<int> _getQueuePos;
        private Action<IList<Song>> _getQueue;

        private SongComparer _comparer;
        private Random _random;

        public MusicManagerIOS()
        {
            _queue = new ObservableCollection<Song>();
            _pos = 0;
            _comparer = new SongComparer();
            _random = new Random();
            InitializePlayer();

            var commandCenter = MPRemoteCommandCenter.Shared;
            commandCenter.PreviousTrackCommand.Enabled = true;
            commandCenter.PreviousTrackCommand.AddTarget(PrevCommand);
            commandCenter.NextTrackCommand.Enabled = true;
            commandCenter.NextTrackCommand.AddTarget(NextCommand);
            commandCenter.TogglePlayPauseCommand.Enabled = true;
            commandCenter.TogglePlayPauseCommand.AddTarget(ToggleCommand);
            commandCenter.PlayCommand.Enabled = true;
            commandCenter.PlayCommand.AddTarget(PlayCommand);
            commandCenter.PauseCommand.Enabled = true;
            commandCenter.PauseCommand.AddTarget(PauseCommand);
        }
        
        public MPRemoteCommandHandlerStatus PrevCommand(MPRemoteCommandEvent commandEvent)
        {
            Prev();
            return MPRemoteCommandHandlerStatus.Success;
        }

        public MPRemoteCommandHandlerStatus NextCommand(MPRemoteCommandEvent commandEvent)
        {
            Next();
            return MPRemoteCommandHandlerStatus.Success;
        }

        public MPRemoteCommandHandlerStatus PlayCommand(MPRemoteCommandEvent commandEvent)
        {
            Play();
            return MPRemoteCommandHandlerStatus.Success;
        }

        public MPRemoteCommandHandlerStatus PauseCommand(MPRemoteCommandEvent commandEvent)
        {
            Pause();
            return MPRemoteCommandHandlerStatus.Success;
        }

        public MPRemoteCommandHandlerStatus ToggleCommand(MPRemoteCommandEvent commandEvent)
        {
            if (_player != null)
            {
                if (_player.Rate == 0)
                {
                    Play();
                }
                else
                {
                    Pause();
                }
            }
            return MPRemoteCommandHandlerStatus.Success;
        }

        private void InitializePlayer()
        {
            _player?.Dispose();
            _player = null;
            _player = new AVPlayer();
            _player.AutomaticallyWaitsToMinimizeStalling = false;
            var avSession = AVAudioSession.SharedInstance();
            avSession.SetCategory(AVAudioSessionCategory.Playback);
            NSError activationError = null;
            avSession.SetActive(true, out activationError);
        }

        public void Init(Action<bool> IsPlaying, Action<double> GetPosition, Action<int> GetQueuePos, Action<IList<Song>> GetQueue)
        {
            _isPlaying = IsPlaying;
            _getPosition = GetPosition;
            _getQueuePos = GetQueuePos;
            _getQueue = GetQueue;

            _isPlaying?.Invoke(_player != null ? _player.Rate != 0 && _player.Error == null : false);
            _getQueuePos?.Invoke(_pos);
            _getQueue?.Invoke(_queue);
        }

        public void Next()
        {
            System.Diagnostics.Debug.WriteLine("Next()");
            if (_queue.Count > 0)
            {
                _pos = (_pos + 1) % _queue.Count;
                Start(_pos);
            }
        }

        public void Pause()
        {
            System.Diagnostics.Debug.WriteLine("Pause()");
            _player?.Pause();
            _isPlaying?.Invoke(_player != null && _player.Rate != 0 && _player.Error == null);

            UpdateInfoCenter();
        }

        public void Play()
        {
            System.Diagnostics.Debug.WriteLine("Play()");
            if (_player != null)
            {
                _player.Play();

                Task.Run(() =>
                {
                    while (_player.Rate != 0 && _player.Error == null)
                    {
                        var seconds = _player.CurrentTime.Seconds;
                        _getPosition(_player.CurrentTime.Seconds);
                        Thread.Sleep(250);
                    }
                });
            }
            _isPlaying?.Invoke(_player != null && _player.Rate != 0 && _player.Error == null);
            UpdateInfoCenter();
        }

        public void Prev()
        {
            System.Diagnostics.Debug.WriteLine("Prev()");
            if (_queue.Count > 0)
            {
                _pos--;
                if (_pos < 0)
                {
                    _pos = _queue.Count > 0 ? _queue.Count - 1 : 0;
                }
                Start(_pos);
            }
        }

        public async void Seek(double position)
        {
            System.Diagnostics.Debug.WriteLine("Seek()");
            bool isSongPlaying = _player?.Rate != 0 && _player.Error == null;
            if (position < _player.CurrentItem.Duration.Seconds)
            {
                await _player?.SeekAsync(CMTime.FromSeconds(position, 4));
                if (!isSongPlaying)
                {
                    Pause();
                }
                UpdateInfoCenter();
            }
            else
            {
                Next();
            }
                
        }

        public async Task SetQueue(IList<Song> songs)
        {
            System.Diagnostics.Debug.WriteLine("SetQeueue()");
            await Task.Run(() =>
            {
                if (songs == null)
                {
                    songs = new ObservableCollection<Song>();
                }

                if (!Enumerable.SequenceEqual(_queue, songs, _comparer))
                {
                    _queue = songs;
                    _getQueue?.Invoke(_queue);
                }

                _pos = 0;
                _getQueuePos(_pos);
                try
                {
                    NSUrl url = new NSUrl(_queue[_pos].Uri);
                    NSMutableDictionary dict = new NSMutableDictionary();
                    dict.Add(new NSString("AVURLAssetPreferPreciseDurationAndTimingKey"), new NSNumber(true));
                    var playerItem = AVPlayerItem.FromAsset(AVUrlAsset.Create(url, new AVUrlAssetOptions(dict)));

                    _player.ReplaceCurrentItemWithPlayerItem(playerItem);
                    Pause();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            });
        }

        public async void Shuffle()
        {
            System.Diagnostics.Debug.WriteLine("Start()");
            await Task.Run(() =>
            {
                int n = _queue.Count;
                int k;
                Song temp;

                while (n > 1)
                {
                    n--;
                    k = _random.Next(n + 1);
                    temp = _queue[k];
                    _queue[k] = _queue[n];
                    _queue[n] = temp;
                }
                _getQueue(_queue);
                Start(0);
            });
        }


        bool isFirst = true;
        public void Start(int pos)
        {
            System.Diagnostics.Debug.WriteLine("Start()");

            // workaround to fix song auto-playing when app launches
            if (isFirst)
            {
                isFirst = false;
                return;
            }

            if (pos >= 0 && pos < _queue.Count)
            {
                _pos = pos;
                _getQueuePos(_pos);
                try
                {
                    NSUrl url = new NSUrl(_queue[_pos].Uri);
                    NSMutableDictionary dict = new NSMutableDictionary();
                    dict.Add(new NSString("AVURLAssetPreferPreciseDurationAndTimingKey"), new NSNumber(true));
                    var playerItem = AVPlayerItem.FromAsset(AVUrlAsset.Create(url, new AVUrlAssetOptions(dict)));
                    _player.ReplaceCurrentItemWithPlayerItem(playerItem);
                    NSNotificationCenter.DefaultCenter.AddObserver(AVPlayerItem.DidPlayToEndTimeNotification, OnCompletion);
                    UpdateInfoCenter();
                    Play();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
        }

        public async void StartQueue(IList<Song> songs, int pos)
        {
            System.Diagnostics.Debug.WriteLine("StartQueue()");
            await SetQueue(songs);
            Start(pos);
        }

        private void OnCompletion(NSNotification notif)
        {
            Next();
        }

        private void UpdateInfoCenter()
        {
            if (_queue != null && _queue.Count > 0)
            {
                var item = new MPNowPlayingInfo
                {
                    Title = _queue[_pos].Title,
                    AlbumTitle = _queue[_pos].Album,
                    Artist = _queue[_pos].Artist,
                    Genre = _queue[_pos].Genre,
                    ElapsedPlaybackTime = _player.CurrentTime.Seconds,
                    PlaybackDuration = _queue[_pos].Duration,
                    PlaybackQueueIndex = _pos,
                    PlaybackQueueCount = _queue.Count,
                    PlaybackRate = _player.Rate
                };
                if (_queue[_pos].Artwork != null)
                {
                    item.Artwork = (MPMediaItemArtwork)_queue[_pos].Artwork;
                }
                MPNowPlayingInfoCenter.DefaultCenter.NowPlaying = item;
            }
            else
            {
                MPNowPlayingInfoCenter.DefaultCenter.NowPlaying = null;
            }
            
            InvokeOnMainThread(() => {
                UIApplication.SharedApplication.BeginReceivingRemoteControlEvents();
            });
        }

        public async void ClearQueue()
        {
            System.Diagnostics.Debug.WriteLine("ClearQueue()");
            Pause();
            await SetQueue(null);
            UpdateInfoCenter();
        }

        public async void AddToEndOfQueue(IList<Song> songs)
        {
            if (_queue == null || _queue.Count == 0)
            {
                await SetQueue(songs);
            }
            else
            {
                foreach (Song song in songs)
                {
                    _queue.Add(song);
                }
                _getQueue?.Invoke(_queue);
            }
        }

        public async void PlayNext(Song song)
        {
            if (_queue == null || _queue.Count == 0)
            {
                var songs = new ObservableCollection<Song>();
                songs.Add(song);
                await SetQueue(songs);
                Start(0);
            }
            else
            {
                _queue.Insert(_pos + 1, song);
                _getQueue?.Invoke(_queue);
            }
        }
    }
}