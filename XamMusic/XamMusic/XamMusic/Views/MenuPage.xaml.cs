using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamMusic.ViewModels;

namespace XamMusic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public ListView PlaylistList => ListViewMenuItems;

        public MenuPage()
        {
            this.BindingContext = MenuViewModel.Instance;
            InitializeComponent();
        }
    }
}
