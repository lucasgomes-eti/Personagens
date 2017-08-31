using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using PersonagensApp.Helpers;
using PersonagensApp.Services;
using PersonagensApp.Views;
using Xamarin.Forms;

namespace PersonagensApp.ViewModels
{
    public class LoginVM : BaseVM
    {
        RestService _restService = new RestService();

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _senha;
        public string Senha
        {
            get => _senha;
            set
            {
                _senha = value;
                OnPropertyChanged(nameof(Senha));
            }
        }

        private string _confirmacaoSenha;
        public string ConfirmacaoSenha
        {
            get => _confirmacaoSenha;
            set
            {
                _confirmacaoSenha = value;
                OnPropertyChanged(nameof(ConfirmacaoSenha));
            }
        }

        public ICommand EntrarCommand
        {
            get
            {
                return new Command(async () =>
                {
                    UserDialogs.Instance.ShowLoading("Logando");
                    var accessToken = await _restService.LoginAsync(_email, _senha);
                    UserDialogs.Instance.HideLoading();
                    if (string.IsNullOrEmpty(accessToken))
                    {
                        UserDialogs.Instance.Alert("Nome de usuário ou senha incorretos!");
                        return;
                    }
                    if (accessToken == "InternalServerError")
                    {
                        UserDialogs.Instance.Alert("Uma conexão com o servidor não pôde ser estabelecida");
                        return;
                    }
                    else
                    {
                        Settings.UserEmail = Email;
                        Settings.AccessToken = accessToken;
                        Application.Current.MainPage = new NavigationPage(new MainPage());
                    }
                });
            }
        }

        public ICommand CadastrarCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new CadastroPage(), true);
                });
            }
        }
    }
}
