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
    }
}
