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
    public interface IRestService
    {
        Task<List<Personagem>> GetPersonagemAsync();

        Task PostPersonagemAsync(Personagem personagem);

        Task PutPersonagemAsync(Personagem personagem);

        Task DeletePersonagemAsync(string id);

        Task UploadFotoPersonagemAsync(MediaFile mediaFile);
    }
}
