using PersonagensApp.Models;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PersonagensApp.Data
{
    public class PersonagemManager
    {
        IRestService restService;

        public PersonagemManager(IRestService service)
        {
            restService = service;
        }

        public Task<List<Personagem>> GetPersonagemAsync()
        {
            return restService.GetPersonagemAsync();
        }

        public Task PostPersonagemAsync(Personagem personagem)
        {
            return restService.PostPersonagemAsync(personagem);
        }

        public Task PutPersonagemAsync(Personagem personagem)
        {
            return restService.PutPersonagemAsync(personagem);
        }

        public Task DeletePersonagemAsync(Personagem personagem)
        {
            return restService.DeletePersonagemAsync(personagem.ID.ToString());
        }

        public Task UploadFotoPersonagemAsync(MediaFile mediaFile)
        {
            return restService.UploadFotoPersonagemAsync(mediaFile);
        }
    }
}
