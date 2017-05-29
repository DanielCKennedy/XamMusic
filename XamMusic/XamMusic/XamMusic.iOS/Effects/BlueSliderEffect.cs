using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

using XamMusic.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamMusic.iOS.Effects;

[assembly: ResolutionGroupName("XamMusic")]
[assembly: ExportEffect(typeof(BlueSliderEffect), "BlueSliderEffect")]
namespace XamMusic.iOS.Effects
{
    class BlueSliderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var slider = (UISlider)Control;
            slider.ThumbTintColor = UIColor.FromRGB(34, 135, 202);
            slider.MinimumTrackTintColor = UIColor.FromRGB(34, 135, 202);
            //slider.MaximumTrackTintColor = UIColor.FromRGB(34, 135, 202);
        }

        protected override void OnDetached()
        {

        }
    }
}