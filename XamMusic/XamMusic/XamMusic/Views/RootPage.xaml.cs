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
    public partial class RootPage : MasterDetailPage
    {
        public RootPage()
        {
            Detail = new NavigationPage(new HomePage(this))
            {
                BarBackgroundColor = Color.FromHex("#16171D"),
                BarTextColor = Color.White
            };
            InitializeComponent();

            MenuPage.PlaylistList.ItemSelected += (s, e) =>
            {
                PlaylistItem item = e.SelectedItem as PlaylistItem;
                if (item != null)
                {
                    // To display the Home page since it isn't a PlaylistPage
                    if (!item.Playlist.IsDynamic && item.Playlist.Title == "Home")
                    {
                        PlaylistViewModel.Instance = null;
                        Detail = new NavigationPage(new HomePage(this))
                        {
                            BarBackgroundColor = Color.FromHex("#16171D"),
                            BarTextColor = Color.White
                        };
                    }
                    else
                    {
                        PlaylistPage page = new PlaylistPage(item);
                        Detail = new NavigationPage(page)
                        {
                            BarBackgroundColor = Color.FromHex("#16171D"),
                            BarTextColor = Color.White
                        };
                    }
                    
                    IsPresented = false;
                }
            };

            UpdateSelected(MenuViewModel.Instance.PlaylistItems.First());
        }

        public void UpdateSelected(object item)
        {
            if (item != null)
            {
                MenuPage.PlaylistList.SelectedItem = item;

            }
        }
    }
}
