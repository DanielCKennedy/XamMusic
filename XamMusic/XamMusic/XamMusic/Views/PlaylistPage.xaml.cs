using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamMusic.Models;
using XamMusic.ViewModels;
using Rg.Plugins.Popup.Extensions;
using XamMusic.Controls;
using XamMusic.Interfaces;
using System.Collections.ObjectModel;

namespace XamMusic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaylistPage : ContentPage
    {
        private static INavigation _nav;
        private PlaylistViewModel _vm;
        public PlaylistPage(PlaylistItem playlistItem)
        {
            _vm = new PlaylistViewModel(playlistItem);
            this.BindingContext = _vm;
            InitializeComponent();
            _nav = Navigation;
        }

        private void playlistListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            _vm.PlayCommand.Execute(e.Item);
            playlistListView.SelectedItem = null;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(QueuePopup.Instance);
        }

        public static INavigation Nav
        {
            get
            {
                return _nav;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            PlaylistViewModel vm = this.BindingContext as PlaylistViewModel;
            DependencyService.Get<IMusicManager>().SetQueue(new ObservableCollection<Song>(
                vm.Songs.Select(s => new Models.Song(s))));
            DependencyService.Get<IMusicManager>().Shuffle();
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            PlaylistViewModel vm = this.BindingContext as PlaylistViewModel;
            DependencyService.Get<IMusicManager>().AddToEndOfQueue(new ObservableCollection<Song>(
                vm.Songs.Select(s => new Models.Song(s))));
        }
    }
}
