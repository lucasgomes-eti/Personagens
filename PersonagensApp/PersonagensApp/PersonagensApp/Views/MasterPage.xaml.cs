using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonagensApp.Views;

using Xamarin.Forms;

namespace PersonagensApp.Views
{
    public partial class MasterPage : ContentPage
    {
        Models.MenuItem itmLoginFace = new Models.MenuItem();

        public MasterPage()
        {
            InitializeComponent();

            itmLoginFace.Icon = "ic_facebook_box.png";
            itmLoginFace.Title = "Conecte-se";
        }

        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as Models.MenuItem;
            switch (item.Title)
            {
                case "Conecte-se":
                    {
                        await App.NavigationMasterDetail(new LoginPage());
                        return;
                    }
                default:
                    break;
            }
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            listView.IsRefreshing = true;
            listView.ItemsSource = ListarItensMenu();
            listView.IsRefreshing = false;
        }

        public List<Models.MenuItem> ListarItensMenu()
        {
            List<Models.MenuItem> itens = new List<Models.MenuItem>
            {
                itmLoginFace,
            };
            return itens;
        }

    }
}
