using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PersonagensApp.Views
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }
        async void OnTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.ImageViewPage(imgFile), true);
        }
    }
}
