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
using Android.Support.V4.Media.Session;

namespace XamMusic.Droid.Audio
{
    class AudioServiceCallback : MediaSessionCompat.Callback
    {
        private AudioServiceBinder _binder;
        private long _lastClickTime;

        public AudioServiceCallback(AudioServiceBinder binder)
        {
            _binder = binder;
        }

        public override void OnPause()
        {
            _binder.GetAudioService().Pause();
            base.OnPause();
        }

        public override void OnPlay()
        {
            _binder.GetAudioService().Play();
            base.OnPlay();
        }

        public override void OnSkipToNext()
        {
            _binder.GetAudioService().Next();
            base.OnSkipToNext();
        }

        public override void OnSkipToPrevious()
        {
            _binder.GetAudioService().Prev();
            base.OnSkipToPrevious();
        }

        public override void OnStop()
        {
            _binder.GetAudioService().Stop();
            base.OnStop();
        }

        public override bool OnMediaButtonEvent(Intent mediaButtonEvent)
        {
            KeyEvent keyEvent = (KeyEvent)mediaButtonEvent.GetParcelableExtra(Intent.ExtraKeyEvent);
            if (keyEvent != null && keyEvent.KeyCode == Keycode.Headsethook && keyEvent.Action == KeyEventActions.Down)
            {
                if (keyEvent.EventTime - _lastClickTime < 250)
                {
                    _binder.GetAudioService().Next();
                    _lastClickTime = 0;
                    return true;
                }
                else
                {
                    _lastClickTime = keyEvent.EventTime;
                }
            }
            return base.OnMediaButtonEvent(mediaButtonEvent);
        }
    }
}