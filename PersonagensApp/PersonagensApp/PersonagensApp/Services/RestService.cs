using Newtonsoft.Json;
using PersonagensApp.Models;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PersonagensApp.Services
{
    public class RestService
    {
        public List<Personagem> personagem { get; private set; }

        public string caminhoFoto = "http://52.67.109.13:3003";
        public async Task<List<Personagem>> GetPersonagemAsync()
        {
            personagem = new List<Personagem>();

            var uri = new Uri(string.Format(Constants.RestUrl, string.Empty));

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        personagem = JsonConvert.DeserializeObject<List<Personagem>>(content);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return personagem;
        }
        public async Task PostPersonagemAsync(Personagem personagem)
        {
            var uri = new Uri(Constants.RestUrl);
            personagem.FotoUrl = caminhoFoto.Replace("\"", "");
            try
            {
                var json = JsonConvert.SerializeObject(personagem);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                using (var client = new HttpClient())
                {
                    response = await client.PostAsync(uri, content);
                }

                caminhoFoto = "http://52.67.109.13:3003";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        public async Task PutPersonagemAsync(Personagem personagem)
        {
            var uri = new Uri($"{Constants.RestUrl}/{personagem.ID.ToString()}");

            try
            {
                var json = JsonConvert.SerializeObject(personagem);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                using (var client = new HttpClient())
                {
                    response = await client.PutAsync(uri, content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        public async Task DeletePersonagemAsync(string id)
        {
            var uri = new Uri($"{Constants.RestUrl}/{id}");

            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.DeleteAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine(response.StatusCode);
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        public async Task UploadFotoPersonagemAsync(MediaFile mediaFile)
        {
            using (var client = new HttpClient())
            {
                var content = new MultipartFormDataContent();

                content.Add(new StreamContent(mediaFile.GetStream()),
                    "\"file\"",
                    $"\"{mediaFile.Path}\"");

                var httpResponseMessage = await client.PostAsync(Constants.UploadService, content);

                caminhoFoto = caminhoFoto + await httpResponseMessage.Content.ReadAsStringAsync();

                content = null;
            }
        }
    }
}
