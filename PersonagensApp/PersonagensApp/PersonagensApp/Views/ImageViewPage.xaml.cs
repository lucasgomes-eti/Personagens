using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PersonagensApp.Views
{
    public partial class ImageViewPage : ContentPage
    {
        public ImageViewPage(Image _imgFile)
        {
            InitializeComponent();

            imgFile.Source = _imgFile.Source;
        }
        async void OnCloseTapped(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync(true);
        }
    }
}
