using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using UpService.Models;
using System.Threading.Tasks;

namespace UpService.ConnectionAPI
{
    public class Connection
    {
        public static async Task<bool> AddClient(Client cliente)
        {
            Uri uri = new Uri("https://api-upservices-azurewebsites.net/api/v1/");
            using (HttpClient http = new HttpClient())
            {
                try
                {
                    http.BaseAddress = uri;
                    var json = JsonConvert.SerializeObject(cliente);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await http.PostAsync("client/insert/", content);
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        string status = response.StatusCode.ToString();
                        return true;
                    }
                    if (!string.IsNullOrWhiteSpace(mensagem))
                    {
                        throw new Exception(mensagem);
                    }
                    throw new Exception(response.StatusCode.ToString());

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
        }

        public static async Task<Client> GetClient(string cpf_email, string password)
        {
            Uri uri = new Uri("https://api-upservices.azurewebsites.net/api/");

            using (HttpClient http = new HttpClient())
            {
                try
                {
                    http.BaseAddress = uri;
                    string s = "{\"username\":\""+cpf_email+"\",\"password\":\""+password+"\"}";
                    //string json = JsonConvert.SerializeObject(s);
                    var content = new StringContent(s, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await http.PostAsync("v1/client/login", content);
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        string status = response.StatusCode.ToString();
                        Client user = JsonConvert.DeserializeObject<Client>(mensagem);
                        return user;
                    }
                    if (!string.IsNullOrWhiteSpace(mensagem))
                    {
                        throw new Exception(mensagem);
                    }
                    throw new Exception(response.StatusCode.ToString());

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
        }
    }
}
