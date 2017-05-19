using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamMusic.ViewModels;

namespace XamMusic.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SliderControl : ContentView
    {
        private static SliderControl _instance;
        public static SliderControl Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SliderControl();
                }
                return _instance;
            }
        }
        public SliderControl()
        {
            this.BindingContext = MusicStateViewModel.Instance;
            InitializeComponent();
        }
    }
}