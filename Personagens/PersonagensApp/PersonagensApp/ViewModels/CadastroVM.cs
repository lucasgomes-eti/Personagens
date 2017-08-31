using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using PersonagensApp.Helpers;
using PersonagensApp.Models;
using PersonagensApp.Services;
using PersonagensApp.Views;
using Xamarin.Forms;

namespace PersonagensApp.ViewModels
{
    public class CadastroVM : BaseVM
    {
        RestService _restService = new RestService();

        private string _nome;
        public string Nome
        {
            get => _nome;
            set
            {
                _nome = value;
                OnPropertyChanged(nameof(Nome));
            }
        }

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

        public ICommand CadastrarCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var user = new Usuario()
                    {
                        UserName = Nome, Email = Email, Password = Senha, ConfirmPassword = ConfirmacaoSenha
                    };
                    UserDialogs.Instance.ShowLoading("Cadastrando");
                    var response = await _restService.RegisterAsync(user);
                    UserDialogs.Instance.HideLoading();

                    if (response.ModelState != null)
                        UserDialogs.Instance.Alert(string.Join(", ", response.ModelState.Values.ToArray()[0]),
                            response.Message);
                    else
                    {
                        UserDialogs.Instance.Alert(response.Message);
                        if (response.Message == "O Cadastro foi realizado com sucesso")
                        {
                            UserDialogs.Instance.ShowLoading("Logando");
                            var accessToken = await _restService.LoginAsync(_email, _senha);
                            UserDialogs.Instance.HideLoading();
                            Settings.UserEmail = Email;
                            Settings.AccessToken = accessToken;
                            Application.Current.MainPage = new NavigationPage(new MainPage());
                        }
                    }
                });
            }
        }
    }
}
