using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Newtonsoft.Json;
using PersonagensApp.Helpers;
using PersonagensApp.Models;

namespace PersonagensApp.Services
{
    public class RestService
    {
        public async Task<T> GetAsync<T>(string url) where T : class
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var uri = new Uri(Constants.RestUrl + url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.GetAsync(uri);

                    if (response.IsSuccessStatusCode)
                    {
                        var stringValue = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject(stringValue);
                        return (T) result;
                    }
                    return null;
                }
                ;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await UserDialogs.Instance.AlertAsync("Ocorreu um erro na sua solicitação");
                throw;
            }
        }

        public async Task<R> PostAsync<T, R>(string url, T postObject) where T : class where R : class
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public async Task<IList<T>> GetListAsync<T>(string url) where T : class
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public async Task<string> UploadFotoAsync()
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public async Task<RequestResponse> RegisterAsync(Usuario user)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var uri = new Uri(Constants.RestUrl + Constants.Register);
                    var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(uri, content);
                    var errorObj = response.Content.ReadAsStringAsync().Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var success = new RequestResponse { Message = "O Cadastro foi realizado com sucesso" };
                        return success;
                    }
                    else
                    {
                        var responseError = JsonConvert.DeserializeObject<RequestResponse>(errorObj);
                        return responseError;
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                var error = new RequestResponse { Message = "Uma conexão com o servidor não pôde ser estabelecida" };
                return error;
            }
        }

        public async Task<string> LoginAsync(string userName, string password)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var keyValues = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("username", userName),
                        new KeyValuePair<string, string>("password", password),
                        new KeyValuePair<string, string>("grant_type", "password")
                    };

                    var request =
                        new HttpRequestMessage(HttpMethod.Post, Constants.RestUrl + Constants.Login)
                        {
                            Content = new FormUrlEncodedContent(keyValues)
                        };
                    var response = await client.SendAsync(request);
                    var jwt = await response.Content.ReadAsStringAsync();
                    var jwtDynamic = JsonConvert.DeserializeObject<dynamic>(jwt);
                    var accessToken = jwtDynamic.Value<string>("access_token");
                    return accessToken;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return "InternalServerError";
            }
        }
    }
}