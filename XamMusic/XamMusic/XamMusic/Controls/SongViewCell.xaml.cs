using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FFImageLoading.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamMusic.Models;
using Rg.Plugins.Popup.Extensions;
using XamMusic.Views;

namespace XamMusic.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SongViewCell : ViewCell
    {
        public SongViewCell()
        {
            InitializeComponent();
        }

        private void SongOptionsPopup(object sender, EventArgs e)
        {
            ((CachedImage)sender).Opacity = 0.6;
            ((CachedImage)sender).FadeTo(1);
            Song song = BindingContext as Song;
            PlaylistPage.Nav?.PushPopupAsync(new SongOptionsPopup(song));
        }
    }
}