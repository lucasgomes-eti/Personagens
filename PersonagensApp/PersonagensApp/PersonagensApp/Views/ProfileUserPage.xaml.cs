using PersonagensApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PersonagensApp.Views
{
    public partial class ProfileUserPage : ContentPage
    {
        public ProfileUserPage(Usuario user)
        {
            InitializeComponent();

            imgCover.Source = user.CoverFotoUrl;
            imgProfile.Source = user.ProfileFotoUrl;
            lblNome.Text = user.Nome;
            lblIdade.Text = user.Idade;
            lblGenero.Text = user.Genero;
            lblEmail.Text = user.Email;
        }
    }
}
