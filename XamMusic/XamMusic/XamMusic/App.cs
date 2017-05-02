using MediaPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamMusic.Interfaces;
using XamMusic.Models;
using XamMusic.Views;

namespace XamMusic
{
    public class App : Application
    {
        //private Song song;
        //private Playlist playlist;
        //private Image image;
        //private IList<Song> songs;
        //private ListView lv;


        public App()
        {
            //playlist = new Playlist
            //{
            //    Title = "Test Playlist",
            //    Id = 2626,
            //    Songs = DependencyService.Get<IPlaylistManager>().GetAllSongs() as List<Song>
            //};

            //playlist.Count = (int)playlist.Songs?.Count;

            //song = playlist.Songs.First(s => !string.IsNullOrEmpty(s.Artwork.ToString()));

            //image = new Image();
            //image.HeightRequest = 100;
            //image.WidthRequest = 300;

            //Button btn = new Button();
            //btn.Text = "Get Artwork";
            //btn.Clicked += Btn_Clicked;

            //Button btn2 = new Button();
            //btn2.Text = "Init";
            //btn2.Clicked += Btn2_Clicked;

            //Button btn3 = new Button();
            //btn3.Text = "Shuffle";
            //btn3.Clicked += Btn3_Clicked;


            //lv = new ListView();
            //var dataTemplate = new DataTemplate(() =>
            //{
            //    var title = new Label();
            //    title.SetBinding(Label.TextProperty, "Title");
            //    Grid grid = new Grid();
            //    grid.Children.Add(title);
            //    return new ViewCell { View = grid };
            //});
            //lv.ItemTemplate = dataTemplate;
            //lv.ItemsSource = playlist.Songs;

            ////The root page of your application
            //var content = new ContentPage
            //{
            //    Title = "XamMusic",
            //    Content = new StackLayout
            //    {
            //        VerticalOptions = LayoutOptions.Center,
            //        Children = {
            //            btn,
            //            btn2,
            //            btn3,
            //            new Label {
            //                HorizontalTextAlignment = TextAlignment.Center,
            //                Text = "Welcome to Xamarin Forms!"
            //            },
            //            lv,
            //            image
            //        }
            //    }
            //};
            MainPage = new NavigationPage(new Page1());
        }

        //private async void Btn3_Clicked(object sender, EventArgs e)
        //{
        //    await Task.Run(() =>
        //    {
        //        DependencyService.Get<IMusicManager>().Shuffle();
        //    });
        //}

        //bool first = true;
        //private void Btn2_Clicked(object sender, EventArgs e)
        //{
        //    DependencyService.Get<IMusicManager>().Init(
        //        isPlaying =>
        //        {
                    
        //        },
        //        getSongPos =>
        //        {

        //        },
        //        getQueuePos =>
        //        {

        //        },
        //        getQueue =>
        //        {
        //            lv.ItemsSource = getQueue;
        //            Task.Run(() =>
        //           {
        //               System.Diagnostics.Debug.WriteLine("Queue:");
        //               foreach (Song song in getQueue)
        //               {
        //                   System.Diagnostics.Debug.WriteLine(song.Title);
        //               }
        //           });
        //        });
        //    if (first)
        //    {
        //        DependencyService.Get<IMusicManager>().SetQueue(playlist.Songs);
        //        first = false;
        //    }
        //}

        //private void Btn_Clicked(object sender, EventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine($"artwork = {song?.Artwork}");
        //    if (!string.IsNullOrEmpty(song?.Artwork?.ToString()))
        //    {
        //        if (Device.OS == TargetPlatform.Android)
        //            image.Source = ImageSource.FromFile(song.Artwork.ToString());
        //        else if (Device.OS == TargetPlatform.iOS)
        //        {
        //            image.Source = ImageSource.FromStream(() => ((MPMediaItemArtwork)song.Artwork)?.ImageWithSize(new CoreGraphics.CGSize(image.Width, image.Height))?.AsPNG()?.AsStream());
        //        }
        //    }
        //}

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
