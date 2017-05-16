using MediaPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamMusic.Controls
{
    public class ArtworkImage : Image
    {
        public static readonly BindableProperty ArtworkProperty = BindableProperty.Create<ArtworkImage, object>(p => p.Artwork, false);

        public object Artwork
        {
            
            get { return (object)GetValue(ArtworkProperty); }
            set
            {
                SetValue(ArtworkProperty, value);
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            switch(propertyName)
            {
                case "Artwork":
                    if (!string.IsNullOrEmpty(Artwork.ToString()))
                    {
                        if (Device.OS == TargetPlatform.iOS)
                        {
                            System.IO.Stream stream = ((MPMediaItemArtwork)Artwork).ImageWithSize(new CoreGraphics.CGSize(WidthRequest, HeightRequest)).AsPNG().AsStream();
                            Source = ImageSource.FromStream(() => stream);
                        }
                        else if (Device.OS == TargetPlatform.Android)
                        {
                            Source = ImageSource.FromFile(Artwork.ToString());
                        }
                    }
                    else
                    {
                        Source = null;
                    }
                    break;
            }
        }
    }
}
