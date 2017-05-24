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

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamMusic.Droid.Effects;
using Android.Graphics;

[assembly: ExportEffect(typeof(BlueProgressBarEffect), "BlueProgressBarEffect")]
namespace XamMusic.Droid.Effects
{
    public class BlueProgressBarEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var progressBar = (Android.Widget.ProgressBar)Control;
            progressBar.ProgressDrawable.SetColorFilter(new PorterDuffColorFilter(new Android.Graphics.Color(34, 135, 202), PorterDuff.Mode.SrcIn));
            //progressBar.Thumb.SetColorFilter(new PorterDuffColorFilter(new Android.Graphics.Color(34, 135, 202), PorterDuff.Mode.SrcIn));
        }

        protected override void OnDetached()
        {

        }
    }
}