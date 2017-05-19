using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamMusic.Interfaces;
using XamMusic.Models;

namespace XamMusic.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SongOptionsPopup : PopupPage
    {
        public SongOptionsPopup(Song song)
        {
            if (song == null)
            {
                return;
            }
            this.BindingContext = song;
            InitializeComponent();
        }

        private void PlayNext(object sender, EventArgs e)
        {
            DependencyService.Get<IMusicManager>().PlayNext(new Song(this.BindingContext as Song));
            Navigation.PopPopupAsync(true);
        }

        private void AddToQueue(object sender, EventArgs e)
        {
            ObservableCollection<Song> songs = new ObservableCollection<Song>();
            songs.Add(new Song(this.BindingContext as Song));
            DependencyService.Get<IMusicManager>().AddToEndOfQueue(songs);
            Navigation.PopPopupAsync(true);
        }
    }
}