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

namespace XamMusic.Droid.Audio
{
    public class AudioServiceBinder : Binder
    {
        private AudioService _audioService;

        public AudioServiceBinder(AudioService audioService)
        {
            _audioService = audioService;
        }

        public AudioService GetAudioService()
        {
            return _audioService;
        }
    }
}