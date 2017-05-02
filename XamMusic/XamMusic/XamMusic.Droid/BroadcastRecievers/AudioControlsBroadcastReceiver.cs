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
using XamMusic.Droid.Audio;

namespace XamMusic.Droid.BroadcastRecievers
{
    [BroadcastReceiver]
    [IntentFilter(new String[] { Intent.ActionMediaButton })]
    public class AudioControlsBroadcastReceiver : BroadcastReceiver
    {
        public string ComponentName => Class.Name;

        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action != Intent.ActionMediaButton)
                return;

            var key = (KeyEvent)intent.GetParcelableExtra(Intent.ExtraKeyEvent);
            if (key.Action != KeyEventActions.Down)
                return;
            string action = AudioService.ActionPlay;
            switch (key.KeyCode)
            {
                case Keycode.Headsethook:
                case Keycode.MediaPlayPause: action = AudioService.ActionToggle; break;
                case Keycode.MediaPlay: action = AudioService.ActionPlay; break;
                case Keycode.MediaPause: action = AudioService.ActionPause; break;
                case Keycode.MediaStop: action = AudioService.ActionPause; break;
                case Keycode.MediaNext: action = AudioService.ActionNext; break;
                case Keycode.MediaPrevious: action = AudioService.ActionPrev; break;
                default: return;
            }
            Intent remoteIntent = new Intent(action);
            context.StartService(remoteIntent);
        }
    }
}