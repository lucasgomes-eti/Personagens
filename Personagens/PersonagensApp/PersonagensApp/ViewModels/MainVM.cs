using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonagensApp.Models;

namespace PersonagensApp.ViewModels
{
    public class MainVM : BaseVM
    {
        private ObservableCollection<Personagem> _personagemsList;
        public ObservableCollection<Personagem> PersonagemsList
        {
            get => _personagemsList;
            set
            {
                _personagemsList = value;
                OnPropertyChanged(nameof(PersonagemsList));
            }
        }
    }
}
