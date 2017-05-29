using FFImageLoading.Forms;
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
    public class ArtworkImage : CachedImage
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
                    System.Diagnostics.Debug.WriteLine("Artwork Changed Started");
                    System.Diagnostics.Debug.WriteLine("Artwork = " + Artwork);
                    if (!string.IsNullOrEmpty(Artwork?.ToString()) && !Artwork.ToString().Equals("False"))
                    {
                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            double width = WidthRequest, height = HeightRequest;
                            if (WidthRequest == -1)
                            {
                                System.Diagnostics.Debug.WriteLine("WidthRequest = -1");
                                width = 400;
                                height = 400;
                            } 
                            
                            System.IO.Stream stream = ((MPMediaItemArtwork)Artwork).ImageWithSize(new CoreGraphics.CGSize(WidthRequest, HeightRequest)).AsPNG().AsStream();
                            Source = ImageSource.FromStream(() => stream);
                        }
                        else if (Device.RuntimePlatform == Device.Android)
                        {
                            Source = ImageSource.FromFile(Artwork.ToString());
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Null");
                        //if (Device.OS == TargetPlatform.iOS)
                        //{
                        //    Source = ImageSource.FromResource
                        //}
                        Source = null;
                    }
                    System.Diagnostics.Debug.WriteLine("Artwork Finished");
                    break;
            }
        }
    }
}
