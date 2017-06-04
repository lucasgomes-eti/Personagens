using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PersonagensApp.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string ProfileFotoUrl { get; set; }
        public string CoverFotoUrl { get; set; }
        public string Idade { get; set; }
        public string Genero { get; set; }
        public string Email { get; set; }
    }
}
