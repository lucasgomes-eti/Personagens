using Acr.UserDialogs;
using PersonagensApp.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonagensApp.Services;
using Xamarin.Forms;

namespace PersonagensApp.Views
{
    public partial class RegistryPage : ContentPage
    {
        MediaFile mediaFile;
        Personagem personagem = new Personagem();

        RestService restService = new RestService();

        public RegistryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtAltura.Text = sldAltura.Value.ToString();
            txtPeso.Text = sldPeso.Value.ToString();
        }

        async void OnTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.ImageViewPage(imgFile), true);
        }

        void OnAlturaValueChanged(object sender, EventArgs e)
        {
            txtAltura.Text = sldAltura.Value.ToString();
        }

        void OnPesoValueChanged(object sender, EventArgs e)
        {
            txtPeso.Text = sldPeso.Value.ToString();
        }

        async void OnCadastrarClicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading(title: Constants.ShowLoadingMessage);
                await restService.UploadFotoPersonagemAsync(mediaFile);
                personagem.Afiliacoes = txtAfiliacoes.Text;
                personagem.Altura = $"{txtAltura.Text} m";
                personagem.Descricao = txtDescricao.Text;
                personagem.Genero = pkrGenero.SelectedItem.ToString();
                personagem.Habilidades = txtHabilidades.Text;
                personagem.Nome = txtNome.Text;
                personagem.NomeReal = txtNomeReal.Text;
                personagem.Origem = txtOrigem.Text;
                personagem.Peso = $"{txtPeso.Text} kg";
                personagem.Poderes = txtPoderes.Text;
                await restService.PostPersonagemAsync(personagem);
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.ShowSuccess("Sucesso!");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {

                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.ShowError("Erro!");
                return;
            }
        }

        async void OnSelectSourceClicked(object sender, EventArgs e)
        {
            var selectSource = await UserDialogs.Instance.ActionSheetAsync("Selecionar imagem com", "Cancelar", null, "Câmera", "Galeria");

            switch (selectSource)
            {
                case "Câmera":
                    {
                        await CrossMedia.Current.Initialize();

                        if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                        {
                            UserDialogs.Instance.ErrorToast("Câmera não está disponível");
                            return;
                        }
                        mediaFile = null;

                        mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                        {
                            Directory = "Personagens",
                            Name = $"IMG{DateTime.Now.ToString().Replace(" ", "-")}.jpg"
                        });

                        if (mediaFile == null)
                        {
                            return;
                        }

                        imgFile.Source = ImageSource.FromStream(() =>
                        {
                            return mediaFile.GetStream();
                        });
                        return;
                    }

                case "Galeria":
                    {
                        await CrossMedia.Current.Initialize();

                        if (!CrossMedia.Current.IsPickPhotoSupported)
                        {
                            UserDialogs.Instance.ErrorToast("Galeria não está disponível");
                            return;
                        }
                        mediaFile = null;
                        mediaFile = await CrossMedia.Current.PickPhotoAsync();

                        if (mediaFile == null)
                        {
                            return;
                        }

                        imgFile.Source = ImageSource.FromStream(() =>
                        {
                            return mediaFile.GetStream();
                        });
                        return;
                    }
                default:
                    break;
            }
        }
    }
}
