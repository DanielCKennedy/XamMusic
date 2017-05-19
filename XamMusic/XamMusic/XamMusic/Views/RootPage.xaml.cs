using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamMusic.Models;

namespace XamMusic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPage : MasterDetailPage
    {
        public RootPage()
        {
            Detail = new NavigationPage(new HomePage());
            InitializeComponent();

            MenuPage.PlaylistList.ItemSelected += (s, e) =>
            {
                PlaylistItem item = e.SelectedItem as PlaylistItem;
                if (item != null)
                {
                    // To display the Home page since it isn't a PlaylistPage
                    if (!item.Playlist.IsDynamic && item.Playlist.Title == "Home")
                    {
                        Detail = new NavigationPage(new HomePage());
                    }
                    else
                    {
                        PlaylistPage page = new PlaylistPage(item);
                        Detail = new NavigationPage(page);
                    }
                    
                    IsPresented = false;
                }
            };
        }
    }
}
