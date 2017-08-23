using System;
using Xamarin.Forms;
using PersonagensApp.Models;
using PersonagensApp.Data;
using Plugin.Connectivity;
using System.Linq;
using System.Collections.Generic;
using PersonagensApp.Services;

namespace PersonagensApp
{
    public partial class MainPage : ContentPage
    {
        DataAccess dados = new DataAccess();

        RestService restService = new RestService();

        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            listView.IsRefreshing = true;

            fab.Clicked = OnFabClicked;

            if (!CrossConnectivity.Current.IsConnected)
            {
                listView.ItemsSource = dados.Listar();

            }
            else
            {
                listView.ItemsSource = await restService.GetPersonagemAsync();
            }

            listView.IsRefreshing = false;
        }

        async void OnRefreshing(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                listView.ItemsSource = dados.Listar();

            }
            else
            {
                listView.ItemsSource = await restService.GetPersonagemAsync();
            }
            listView.IsRefreshing = false;
        }

        //void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        //{
        //    var key = sbrBusca.Text;

        //    //listView.ItemsSource = personagens.Where(personagens => personagens.Nome.ToLower().Contains(key));
        //    var personagens = listView.ItemsSource as List<Personagem>;
        //    listView.ItemsSource = personagens.Where(p => p.Nome.ToLower().Contains(key));
        //}

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var personagem = e.SelectedItem as Personagem;
            dados.Insert(personagem);
            var profilePage = new Views.ProfilePage();
            profilePage.BindingContext = personagem;
            Navigation.PushAsync(profilePage);
        }
        async void OnFabClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.RegistryPage());
        }
    }
}
