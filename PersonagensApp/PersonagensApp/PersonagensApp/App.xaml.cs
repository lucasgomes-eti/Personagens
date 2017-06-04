using Acr.UserDialogs;
using PersonagensApp.Data;
using PersonagensApp.Models;
using PersonagensApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PersonagensApp
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterDetail { get; set; }

        public async static Task NavigationMasterDetail(Page page)
        {
            App.MasterDetail.IsPresented = false;
            Page currentPage = MasterDetail.Detail.Navigation.NavigationStack.LastOrDefault();
            if (currentPage.Navigation.NavigationStack.Count > 1)
            {
                return;
            }
            await App.MasterDetail.Detail.Navigation.PushAsync(page);
        }
        public static PersonagemManager PersonagemManager { get; private set; }

        public static Action HideLoginView
        {
            get
            {
                return new Action(() => App.Current.MainPage.Navigation.PopModalAsync());
            }
        }

        public async static Task NavigateToProfile(Usuario user)
        {
            UserDialogs.Instance.ShowLoading(title: "Importando Dados");
            await MasterDetail.Detail.Navigation.PopAsync();
            await NavigationMasterDetail(new ProfileUserPage(user));
            UserDialogs.Instance.HideLoading();
        }

        public App()
        {
            InitializeComponent();
            PersonagemManager = new PersonagemManager(new RestService());
            MainPage = new Views.MasterDetailNavigationPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
