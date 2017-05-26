using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FFImageLoading.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamMusic.ViewModels;

namespace XamMusic.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NextControl : ContentView
    {
        public NextControl()
        {
            this.BindingContext = MusicStateViewModel.Instance;
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ((CachedImage)sender).Opacity = 0.6;
            ((CachedImage)sender).FadeTo(1, 150);
        }
    }
}