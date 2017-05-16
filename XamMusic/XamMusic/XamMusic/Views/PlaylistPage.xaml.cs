using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamMusic.Models;
using XamMusic.ViewModels;

namespace XamMusic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaylistPage : ContentPage
    {
        private PlaylistViewModel _vm;
        public PlaylistPage(PlaylistItem playlistItem)
        {
            _vm = new PlaylistViewModel(playlistItem);
            this.BindingContext = _vm;
            InitializeComponent();
            
        }

        private void playlistListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            _vm.PlayCommand.Execute(e.Item);
            playlistListView.SelectedItem = null;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Page1.Instance.Navigation.PushAsync(this);

        }
    }
}
