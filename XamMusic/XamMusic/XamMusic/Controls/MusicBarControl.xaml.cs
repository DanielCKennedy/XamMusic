using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamMusic.ViewModels;

namespace XamMusic.Controls
{
    public partial class MusicBarControl : ContentView
    {
        public MusicBarControl()
        {
            this.BindingContext = MusicStateViewModel.Instance;
            InitializeComponent();
        }
    }
}
