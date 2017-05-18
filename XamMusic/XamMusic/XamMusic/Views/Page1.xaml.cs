using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamMusic.ViewModels;
using Rg.Plugins.Popup.Extensions;
using XamMusic.Controls;

namespace XamMusic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        private static ContentPage _instance;
        public static ContentPage Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Page1();
                }
                return _instance;
            }
        }

        private Page1()
        {
            BindingContext = MusicStateViewModel.Instance;
            InitializeComponent();

            
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(QueuePopup.Instance);
        }
    }
}
