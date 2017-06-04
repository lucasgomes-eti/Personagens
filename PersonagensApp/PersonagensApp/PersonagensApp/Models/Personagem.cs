using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace PersonagensApp.Models
{
    public  class Personagem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string FotoUrl { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string NomeReal { get; set; }
        public string Genero { get; set; }
        public string Altura { get; set; }
        public string Peso { get; set; }
        public string Poderes { get; set; }
        public string Habilidades { get; set; }
        public string Afiliacoes { get; set; }
        public string Origem { get; set; }
    }
}
