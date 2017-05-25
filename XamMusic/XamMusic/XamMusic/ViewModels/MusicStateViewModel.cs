using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamMusic.Interfaces;
using XamMusic.Models;

namespace XamMusic.ViewModels
{
    public class MusicStateViewModel : BaseViewModel
    {
        private static MusicStateViewModel _instance;
        public static MusicStateViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MusicStateViewModel();
                }
                return _instance;
            }
        }
        
        public Command PlayCommand { get; private set; }
        public Command PauseCommand { get; private set; }
        public Command NextCommand { get; private set; }
        public Command PrevCommand { get; private set; }
        public Command ShuffleCommand { get; private set; }
        public Command ToggleCommand { get; private set; }
        public Command ClearQueueCommand { get; private set; }

        private MusicStateViewModel()
        {
            DependencyService.Get<IMusicManager>().Init(
                        isPlaying =>
                        {
                            IsPlaying = isPlaying;
                        },
                        getPosition =>
                        {
                            ActualPosition = getPosition;
                        },
                        getQueuePos =>
                        {
                            QueuePos = getQueuePos;
                        },
                        getQueue =>
                        {
                            Queue = getQueue;
                        });
            Task.Run(async () =>
            {
                var songs = await DependencyService.Get<IPlaylistManager>().GetAllSongs();
                await DependencyService.Get<IMusicManager>().SetQueue(
                    songs);
            });
            
            PlayCommand = new Command(() => DependencyService.Get<IMusicManager>().Play());
            PauseCommand = new Command(() => DependencyService.Get<IMusicManager>().Pause());
            NextCommand = new Command(() => DependencyService.Get<IMusicManager>().Next());
            PrevCommand = new Command(() => DependencyService.Get<IMusicManager>().Prev());
            ShuffleCommand = new Command( () => DependencyService.Get<IMusicManager>().Shuffle());
            ToggleCommand = new Command(() =>
            {
                if (IsPlaying)
                {
                    DependencyService.Get<IMusicManager>().Pause();
                }
                else
                {
                    DependencyService.Get<IMusicManager>().Play();
                }
            });
            ClearQueueCommand = new Command(() =>
            {
                DependencyService.Get<IMusicManager>().ClearQueue();
            });
        }

        public bool IsNotPlaying { get { return !_isPlaying; }}
        private bool _isPlaying;

        public bool IsPlaying
        {
            get { return _isPlaying; }
            set
            {
                if (_isPlaying != value)
                {
                    _isPlaying = value;
                    OnPropertyChanged(nameof(IsPlaying));
                    OnPropertyChanged(nameof(IsNotPlaying));
                }
            }
        }


        private IList<Song> _queue;

        public IList<Song> Queue
        {
            get { return _queue ?? new ObservableCollection<Song>(); }
            set
            {
                if (_queue != value)
                {
                    _queue = value;
                }
                OnPropertyChanged(nameof(Queue));
                OnPropertyChanged(nameof(Progress));
            }
        }

        private int _queuePos;

        public int QueuePos
        {
            get { return _queuePos; }
            set
            {
                _queuePos = value;
                OnPropertyChanged(nameof(QueuePos));
                OnPropertyChanged(nameof(Progress));
                OnPropertyChanged(nameof(SelectedSong));
            }
        }

        private Song _selectedSong;

        public Song SelectedSong
        {
            get
            {
                if (Queue.Count > _queuePos)
                {
                    _selectedSong = Queue[_queuePos];
                }
                return _selectedSong;
            }
            set
            {
                if (Queue.Contains(value))
                {
                    QueuePos = Queue.IndexOf(value);
                    DependencyService.Get<IMusicManager>().Start(_queuePos);
                    OnPropertyChanged(nameof(DurationString));
                }
            }
        }



        private double _actualPosition;

        public double ActualPosition
        {
            get { return _actualPosition; }
            set
            {
                _actualPosition = value;
                Position = _actualPosition;
            }
        }


        private double _position;

        public double Position
        {
            get { return _position; }
            set
            {
                if (_position != value && value < _queue[_queuePos].Duration)
                {
                    double temp = _position;
                    double val = value;
                    if (_position > _queue[_queuePos].Duration)
                        val = 0;
                    _position = val;
                    OnPropertyChanged(nameof(Position));
                    OnPropertyChanged(nameof(Progress));
                    OnPropertyChanged(nameof(PositionString));
                    if (Math.Abs(_position - _actualPosition) > 1 && temp <= _queue[_queuePos].Duration)
                    {
                        DependencyService.Get<IMusicManager>().Seek(val);
                    }
                }
            }
        }

        public double Progress
        {
            get
            {
                if (_queue == null || _queue.Count == 0)
                    return 0;
                double ret = _position / _queue[_queuePos].Duration;
                return ret;
            }
        }

        public string PositionString
        {
            get
            {
                return Timify(_position);                
            }
        }

        public string DurationString
        {
            get
            {
                return Timify(_selectedSong.Duration);
            }
        }

        private string Timify(double time)
        {
            String ret = "--:--";
            int hour, min, second;
            if (time < 0)
                return ret;

            if (time < 3600)
            {
                min = (int)(time / 60);
                second = (int)(time % 60);
                ret = $"{min.ToString("00")}:{second.ToString("00")}";
            }
            else
            {
                hour = (int)(time / 3600);
                time = time / 60;
                min = (int)(time / 60);
                second = (int)(time % 60);
                ret = $"{hour.ToString("00")}:{min.ToString("00")}:{second.ToString("00")}";
            }

            return ret;
        }
    }
}
