using PersonagensApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PersonagensApp.Data
{
    public class DataAccess : IDisposable
    {
        SQLite.Net.SQLiteConnection _conexao;

        public DataAccess()
        {
            var config = DependencyService.Get<IConfig>();
            _conexao = new SQLite.Net.SQLiteConnection(config.Plataforma, System.IO.Path.Combine(config.DiretorioDB, "bancodados.db3"));

            _conexao.CreateTable<Personagem>();
        }

        public void Insert(Personagem personagem)
        {
            if(_conexao.Table<Personagem>().FirstOrDefault(c => c.ID == personagem.ID) == null)
            _conexao.Insert(personagem);
        }

        public void Delete(Personagem personagem)
        {
            _conexao.Delete(personagem);
        }

        public Personagem GetById(int id)
        {
            return _conexao.Table<Personagem>().FirstOrDefault(c => c.ID == id);
        }

        public void Update(List<Personagem> personagem)
        {
            _conexao.Update(personagem);
        }

        public List<Personagem> Listar()
        {
            return _conexao.Table<Personagem>().OrderBy(c => c.ID).ToList();
        }

        public void Dispose()
        {
            _conexao.Dispose();
        }
    }
}
