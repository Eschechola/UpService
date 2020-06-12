using System;
using Java.Net;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using UpService.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UpService.ConnectionAPI
{
    public class Connection
    {
        private const string NOT_FOUND = "NotFound";
        private const string BAD_REQUEST = "BadRequest";
        private const string INTERNAL_SERVER_ERROR= "InternalServerError";
        private const string URL = "https://api-upservices.azurewebsites.net/api/";

        public static async Task<bool> AddClient(Client cliente)
        {
            Uri uri = new Uri(URL);
            using (HttpClient http = new HttpClient())
            {
                try
                {
                    http.BaseAddress = uri;
                    http.Timeout = TimeSpan.FromSeconds(20);
                    var json = JsonConvert.SerializeObject(cliente);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await http.PostAsync("v1/client/insert/", content);
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        string status = response.StatusCode.ToString();
                        return true;
                    }
                    if (response.StatusCode.ToString() == INTERNAL_SERVER_ERROR)
                    {
                        throw new Exception("Erro interno do servidor\nTente novamente mais tarde");
                    }
                    if (!string.IsNullOrWhiteSpace(mensagem))
                    {
                        throw new Exception(mensagem);
                    }
                    throw new Exception(response.StatusCode.ToString());

                }
                catch (UnknownHostException ex)
                {
                    Debug.WriteLine("Connection > AddClient " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Conexão com servidor indisponível", ex);
                }
                catch (Javax.Net.Ssl.SSLException ex)
                {
                    Debug.WriteLine("Connection > AddClient " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Conexão com a internet foi interrompida. Conecte-se e tente novamente.", ex);
                }
                catch (OperationCanceledException ex)
                {
                    Debug.WriteLine("Connection > AddClient " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Operação interrompida. Verifique sua conexão com a internet", ex);
                }
                catch (System.IO.EndOfStreamException ex)
                {
                    Debug.WriteLine("Connection > AddClient " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (System.IO.IOException ex)
                {
                    Debug.WriteLine("Connection > AddClient " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Connection > AddClient " + ex.Message +" | "+ex.InnerException);
                    throw ex;
                }
            }
        }
        public static async Task<Client> GetClient(string cpf_email, string password)
        {
            Uri uri = new Uri(URL);

            using (HttpClient http = new HttpClient())
            {
                try
                {
                    http.BaseAddress = uri;
                    http.Timeout = TimeSpan.FromSeconds(20);
                    string s = "{\"EmailOrCpf\":\""+cpf_email+"\",\"password\":\""+password+"\"}";
                    var content = new StringContent(s, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await http.PostAsync("v1/client/login", content);
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        string status = response.StatusCode.ToString();
                        Client user = JsonConvert.DeserializeObject<Client>(mensagem);
                        return user;
                    }
                    if(response.StatusCode.ToString() == INTERNAL_SERVER_ERROR)
                    {
                        throw new Exception("Erro interno do servidor\nTente novamente mais tarde");
                    }
                    if (!string.IsNullOrWhiteSpace(mensagem))
                    {
                        throw new Exception(mensagem);
                    }
                    throw new Exception(response.StatusCode.ToString());

                }
                catch (UnknownHostException ex)
                {
                    Debug.WriteLine("Connection > GetClient " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Conexão com servidor indisponível", ex);
                }
                catch (Javax.Net.Ssl.SSLException ex)
                {
                    Debug.WriteLine("Connection > GetClient " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Conexão com a internet foi interrompida. Conecte-se e tente novamente.", ex);
                }
                catch(OperationCanceledException ex)
                {
                    Debug.WriteLine("Connection > GetClient " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Operação interrompida. Verifique sua conexão com a internet", ex);
                }
                catch (System.IO.EndOfStreamException ex)
                {
                    Debug.WriteLine("Connection > GetClient " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (System.IO.IOException ex)
                {
                    Debug.WriteLine("Connection > GetClient " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Connection > GetClient " + ex.Message +" | "+ex.InnerException);
                    throw ex;
                }
            }
        }
        public static async Task<bool> AddJob(Job job)
        {
            Uri uri = new Uri(URL);

            using (HttpClient http = new HttpClient())
            {
                try
                {
                    http.BaseAddress = uri;
                    http.Timeout = TimeSpan.FromSeconds(20);
                    var json = JsonConvert.SerializeObject(job);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await http.PostAsync("v1/job/insert/", content);
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        string status = response.StatusCode.ToString();
                        return true;
                    }
                    if (response.StatusCode.ToString() == INTERNAL_SERVER_ERROR)
                    {
                        throw new Exception("Erro interno do servidor\nTente novamente mais tarde");
                    }
                    if (!string.IsNullOrWhiteSpace(mensagem))
                    {
                        throw new Exception(mensagem);
                    }
                    throw new Exception(response.StatusCode.ToString());
                }
                catch (UnknownHostException ex)
                {
                    Debug.WriteLine("Connection > AddJob " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Conexão com servidor indisponível", ex);
                }
                catch (Javax.Net.Ssl.SSLException ex)
                {
                    Debug.WriteLine("Connection > AddJob  " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Conexão com a internet foi interrompida. Conecte-se e tente novamente.", ex);
                }
                catch (OperationCanceledException ex)
                {
                    Debug.WriteLine("Connection > AddJob " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Operação interrompida. Verifique sua conexão com a internet", ex);
                }
                catch (System.IO.EndOfStreamException ex)
                {
                    Debug.WriteLine("Connection > AddJob " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (System.IO.IOException ex)
                {
                    Debug.WriteLine("Connection > AddJob " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Connection > AddJob " + ex.Message +" | "+ex.InnerException);
                    throw ex;
                }
            }
        }
        public static async Task<AllJobsPublishedResult> GetJobList(int page, int mountOfPage)
        {
            Uri uri = new Uri(URL);

            using (HttpClient http = new HttpClient())
            {
                try
                {
                    http.BaseAddress = uri;
                    http.Timeout = TimeSpan.FromSeconds(30);
                    HttpResponseMessage response = await http.GetAsync("v1/job/get-all-published-jobs/"+page+"/"+mountOfPage);
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        string status = response.StatusCode.ToString();
                        AllJobsPublishedResult obj = JsonConvert.DeserializeObject<AllJobsPublishedResult>(mensagem);
                        return obj;
                    }
                    if (response.StatusCode.ToString() == INTERNAL_SERVER_ERROR)
                    {
                        throw new Exception("Erro interno do servidor\nTente novamente mais tarde");
                    }
                    if (!string.IsNullOrWhiteSpace(mensagem))
                    {
                        throw new Exception(mensagem);
                    }
                    throw new Exception(response.StatusCode.ToString());

                }
                catch (UnknownHostException ex)
                {
                    Debug.WriteLine("Connection > GetJobList " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Conexão com servidor indisponível", ex);
                }
                catch (Javax.Net.Ssl.SSLException ex)
                {
                    Debug.WriteLine("Connection > GetJobList " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Conexão com a internet foi interrompida. Conecte-se e tente novamente.", ex);
                }
                catch (OperationCanceledException ex)
                {
                    Debug.WriteLine("Connection > GetJobList " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Operação interrompida. Verifique sua conexão com a internet", ex);
                }
                catch (System.IO.EndOfStreamException ex)
                {
                    Debug.WriteLine("Connection > GetJobList " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (System.IO.IOException ex)
                {
                    Debug.WriteLine("Connection > GetJobList " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Connection > GetJobList  " + ex.Message +" | "+ex.InnerException);
                    throw ex;
                }
            }
        }
        public static async Task<bool> SendOffer(int JobId, int idJobProvider, int value)
        {
            Uri uri = new Uri(URL);

            using (HttpClient http = new HttpClient())
            {
                try
                {
                    http.BaseAddress = uri;
                    http.Timeout = TimeSpan.FromSeconds(20);
                    string s = "{\"idJob\":" + JobId + ",\"idJobProvider\":" + idJobProvider + ",\"valueOffered\":" + value + "}";
                    var content = new StringContent(s, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await http.PostAsync("v1/job/send-offer/", content);
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        string status = response.StatusCode.ToString();
                        return true;
                    }
                    if (response.StatusCode.ToString() == INTERNAL_SERVER_ERROR)
                    {
                        throw new Exception("Erro interno do servidor\nTente novamente mais tarde");
                    }
                    if (!string.IsNullOrWhiteSpace(mensagem))
                    {
                        throw new Exception(mensagem);
                    }
                    throw new Exception(response.StatusCode.ToString());
                }
                catch (UnknownHostException ex)
                {
                    Debug.WriteLine("Connection > SendOffer " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Conexão com servidor indisponível", ex);
                }
                catch (Javax.Net.Ssl.SSLException ex)
                {
                    Debug.WriteLine("Connection > SendOffer  " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Conexão com a internet foi interrompida. Conecte-se e tente novamente.", ex);
                }
                catch (OperationCanceledException ex)
                {
                    Debug.WriteLine("Connection > SendOffer " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Operação interrompida. Verifique sua conexão com a internet", ex);
                }
                catch (System.IO.EndOfStreamException ex)
                {
                    Debug.WriteLine("Connection > SendOffer " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (System.IO.IOException ex)
                {
                    Debug.WriteLine("Connection > SendOffer " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Connection > SendOffer " + ex.Message +" | "+ex.InnerException);
                    throw ex;
                }
            }
        }
        public static async Task<List<Job>> GetAllFinishedJobs(int Id)
        {
            Uri uri = new Uri(URL);

            using (HttpClient http = new HttpClient())
            {
                try
                {
                    http.BaseAddress = uri;
                    http.Timeout = TimeSpan.FromSeconds(30);
                    HttpResponseMessage response = await http.GetAsync("v1/job/get-all-finished-jobs/" + Id);
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        string status = response.StatusCode.ToString();
                        List<Job> obj = JsonConvert.DeserializeObject<List<Job>>(mensagem);
                        return obj;
                    }
                    if (response.StatusCode.ToString() == INTERNAL_SERVER_ERROR)
                    {
                        throw new Exception("Erro interno do servidor\nTente novamente mais tarde");
                    }
                    if (!string.IsNullOrWhiteSpace(mensagem))
                    {
                        throw new Exception(mensagem);
                    }
                    throw new Exception(response.StatusCode.ToString());
                }
                catch (UnknownHostException ex)
                {
                    Debug.WriteLine("Connection > GetAllFinishedJobs " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Conexão com servidor indisponível", ex);
                }
                catch (Javax.Net.Ssl.SSLException ex)
                {
                    Debug.WriteLine("Connection > GetAllFinishedJobs " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Conexão com a internet foi interrompida. Conecte-se e tente novamente.", ex);
                }
                catch (OperationCanceledException ex)
                {
                    Debug.WriteLine("Connection > GetAllFinishedJobs " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Operação interrompida. Verifique sua conexão com a internet", ex);
                }
                catch (System.IO.EndOfStreamException ex)
                {
                    Debug.WriteLine("Connection > GetAllFinishedJobs " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (System.IO.IOException ex)
                {
                    Debug.WriteLine("Connection > GetAllFinishedJobs " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Connection > GetAllFinishedJobs " + ex.Message +" | "+ex.InnerException);
                    throw ex;
                }
            }
        }
        public static async Task<List<Job>> GetAllAcceptedJobs(int Id)
        {
            Uri uri = new Uri(URL);

            using (HttpClient http = new HttpClient())
            {
                try
                {
                    http.BaseAddress = uri;
                    http.Timeout = TimeSpan.FromSeconds(30);
                    HttpResponseMessage response = await http.GetAsync("v1/job/get-all-accepted-jobs/" + Id);
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        string status = response.StatusCode.ToString();
                        List<Job> obj = JsonConvert.DeserializeObject<List<Job>>(mensagem);
                        return obj;
                    }
                    if (response.StatusCode.ToString() == INTERNAL_SERVER_ERROR)
                    {
                        throw new Exception("Erro interno do servidor\nTente novamente mais tarde");
                    }
                    if (!string.IsNullOrWhiteSpace(mensagem))
                    {
                        throw new Exception(mensagem);
                    }
                    throw new Exception(response.StatusCode.ToString());
                }
                catch (UnknownHostException ex)
                {
                    Debug.WriteLine("Connection > GetAllAcceptedJobs  " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Conexão com servidor indisponível", ex);
                }
                catch (Javax.Net.Ssl.SSLException ex)
                {
                    Debug.WriteLine("Connection > GetAllAcceptedJobs  " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Conexão com a internet foi interrompida. Conecte-se e tente novamente.", ex);
                }
                catch (OperationCanceledException ex)
                {
                    Debug.WriteLine("Connection > GetAllAcceptedJobs  " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Operação interrompida. Verifique sua conexão com a internet", ex);
                }
                catch (System.IO.EndOfStreamException ex)
                {
                    Debug.WriteLine("Connection > GetAllAcceptedJobs " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (System.IO.IOException ex)
                {
                    Debug.WriteLine("Connection > GetAllAcceptedJobs " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Connection > GetAllAcceptedJobs  " + ex.Message +" | "+ex.InnerException);
                    throw ex;
                }
            }
        }
        public static async Task<List<Job>> GetAllPublishedJobsByClient(int Id)
        {
            Uri uri = new Uri(URL);

            using (HttpClient http = new HttpClient())
            {
                try
                {
                    http.BaseAddress = uri;
                    http.Timeout = TimeSpan.FromSeconds(30);
                    HttpResponseMessage response = await http.GetAsync("v1/job/get-all-published-jobs-by-client/" + Id);
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        string status = response.StatusCode.ToString();
                        List<Job> obj = JsonConvert.DeserializeObject<List<Job>>(mensagem);
                        return obj;
                    }
                    if (response.StatusCode.ToString() == INTERNAL_SERVER_ERROR)
                    {
                        throw new Exception("Erro interno do servidor\nTente novamente mais tarde");
                    }
                    if (!string.IsNullOrWhiteSpace(mensagem))
                    {
                        throw new Exception(mensagem);
                    }
                    throw new Exception(response.StatusCode.ToString());
                }
                catch (UnknownHostException ex)
                {
                    Debug.WriteLine("Connection > GetAllPublishedJobsByClient  " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com servidor indisponível", ex);
                }
                catch (Javax.Net.Ssl.SSLException ex)
                {
                    Debug.WriteLine("Connection > GetAllPublishedJobsByClient  " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet foi interrompida. Conecte-se e tente novamente.", ex);
                }
                catch (OperationCanceledException ex)
                {
                    Debug.WriteLine("Connection > GetAllPublishedJobsByClient  " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Operação interrompida. Verifique sua conexão com a internet", ex);
                }
                catch (System.IO.EndOfStreamException ex)
                {
                    Debug.WriteLine("Connection > GetAllPublishedJobsByClient " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (System.IO.IOException ex)
                {
                    Debug.WriteLine("Connection > GetAllPublishedJobsByClient " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Connection > GetAllPublishedJobsByClient  " + ex.Message + " | " + ex.InnerException);
                    throw ex;
                }
            }
        }
        public static async Task<bool> FinishJob(int JobId)
        {
            Uri uri = new Uri(URL);

            using(HttpClient http = new HttpClient())
            {
                try
                {
                    http.BaseAddress = uri;
                    http.Timeout = TimeSpan.FromSeconds(20);
                    //string s = "{\"Id\":"+ JobId +"}";
                    //var content = new StringContent(s, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await http.PutAsync("v1/job/finish-job/"+JobId,null);
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    if(response.StatusCode.ToString() == NOT_FOUND)
                    {
                        throw new Exception("Não encontrado. Tente novamente mais tarde");
                    }
                    if (response.StatusCode.ToString() == INTERNAL_SERVER_ERROR)
                    {
                        throw new Exception("Erro interno do servidor\nTente novamente mais tarde");
                    }
                    if (!string.IsNullOrWhiteSpace(mensagem))
                    {
                        throw new Exception(mensagem);
                    }
                    throw new Exception(response.StatusCode.ToString());

                }
                catch (UnknownHostException ex)
                {
                    Debug.WriteLine("Connection > FinishJob " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Conexão com servidor indisponível", ex);
                }
                catch (Javax.Net.Ssl.SSLException ex)
                {
                    Debug.WriteLine("Connection > FinishJob  " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Conexão com a internet foi interrompida. Conecte-se e tente novamente.", ex);
                }
                catch (OperationCanceledException ex)
                {
                    Debug.WriteLine("Connection > FinishJob " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Operação interrompida. Verifique sua conexão com a internet", ex);
                }
                catch (System.IO.EndOfStreamException ex)
                {
                    Debug.WriteLine("Connection > FinishJob " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (System.IO.IOException ex)
                {
                    Debug.WriteLine("Connection > FinishJob " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Connection > FinishJob " + ex.Message +" | "+ex.InnerException);
                    throw ex;
                }
            }
        }
        public static async Task<bool> SendFeedback(Job finalizado, float avaliacao)
        {
            Uri uri = new Uri(URL);
            try
            {
                using(HttpClient http =new HttpClient())
                {
                    http.Timeout = TimeSpan.FromSeconds(45);
                    var method = new HttpMethod("PATCH");
                    http.BaseAddress = uri;
                    string s = "{\"fkIdClient\":" + finalizado.FkIdClientJobProvider + ",\"note\":" + avaliacao.ToString().Replace(",",".") + "}";
                    var content = new StringContent(s, Encoding.UTF8, "application/json");

                    var req = new HttpRequestMessage(method, "v1/client/send-requester-evaluation/");
                    req.Content = content;
                    HttpResponseMessage response = await http.SendAsync(req);
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    if (response.StatusCode.ToString() == NOT_FOUND)
                    {
                        throw new Exception("Não encontrado. Tente novamente mais tarde");
                    }
                    if (response.StatusCode.ToString() == INTERNAL_SERVER_ERROR)
                    {
                        throw new Exception("Erro interno do servidor\nTente novamente mais tarde");
                    }
                    if (!string.IsNullOrWhiteSpace(mensagem))
                    {
                        throw new Exception(mensagem);
                    }
                    throw new Exception(response.StatusCode.ToString());
                }
            }
            catch (UnknownHostException ex)
            {
                Debug.WriteLine("Connection > SendFeedback " + ex.Message +" | "+ex.InnerException);
                throw new Exception("Conexão com servidor indisponível", ex);
            }
            catch (Javax.Net.Ssl.SSLException ex)
            {
                Debug.WriteLine("Connection > SendFeedback  " + ex.Message +" | "+ex.InnerException);
                throw new Exception("Conexão com a internet foi interrompida. Conecte-se e tente novamente.", ex);
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine("Connection > SendFeedback " + ex.Message +" | "+ex.InnerException);
                throw new Exception("Operação interrompida. Verifique sua conexão com a internet", ex);
            }
            catch (System.IO.EndOfStreamException ex)
            {
                Debug.WriteLine("Connection > SendFeedback " + ex.Message + " | " + ex.InnerException);
                throw new Exception("Conexão com a internet indisponível", ex);
            }
            catch (System.IO.IOException ex)
            {
                Debug.WriteLine("Connection > SendFeedback " + ex.Message + " | " + ex.InnerException);
                throw new Exception("Conexão com a internet indisponível", ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Connection > SendFeedback " + ex.Message +" | "+ex.InnerException);
                throw ex;
            }


        }
        public static async Task<List<Job>> SearchByTitle(string title)
        {
            Uri uri = new Uri(URL);

            using (HttpClient http = new HttpClient())
            {
                try
                {
                    http.BaseAddress = uri;
                    http.Timeout = TimeSpan.FromSeconds(20);
                    HttpResponseMessage response = await http.GetAsync("v1/job/search-by-title?title=" + title);
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        string status = response.StatusCode.ToString();
                        List<Job> obj = JsonConvert.DeserializeObject<List<Job>>(mensagem);
                        if(obj.Count == 0)
                        {
                            obj = await SearchByCity(title, http);
                        }
                        return obj;
                    }
                    if (response.StatusCode.ToString() == INTERNAL_SERVER_ERROR)
                    {
                        throw new Exception("Erro interno do servidor\nTente novamente mais tarde");
                    }
                    if (!string.IsNullOrWhiteSpace(mensagem))
                    {
                        throw new Exception(mensagem);
                    }
                    throw new Exception(response.StatusCode.ToString());
                }
                catch (UnknownHostException ex)
                {
                    Debug.WriteLine("Connection > SearchByTitle " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Conexão com servidor indisponível", ex);
                }
                catch (Javax.Net.Ssl.SSLException ex)
                {
                    Debug.WriteLine("Connection > SearchByTitle " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Conexão com a internet foi interrompida. Conecte-se e tente novamente.", ex);
                }
                catch (OperationCanceledException ex)
                {
                    Debug.WriteLine("Connection > SearchByTitle " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Operação interrompida. Verifique sua conexão com a internet", ex);
                }
                catch (System.IO.EndOfStreamException ex)
                {
                    Debug.WriteLine("Connection > SearchByTitle " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (System.IO.IOException ex)
                {
                    Debug.WriteLine("Connection > SearchByTitle " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        private static async Task<List<Job>> SearchByCity(string city, HttpClient httpClient)
        {
            Uri uri = new Uri(URL);

            using (HttpClient http = httpClient)
            {
                try
                {
                    http.BaseAddress = uri;
                    http.Timeout = TimeSpan.FromSeconds(20);
                    HttpResponseMessage response = await http.GetAsync("v1/job/search-by-city?city=" + city);
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        string status = response.StatusCode.ToString();
                        List<Job> obj = JsonConvert.DeserializeObject<List<Job>>(mensagem);
                        return obj;
                    }
                    if (response.StatusCode.ToString() == INTERNAL_SERVER_ERROR)
                    {
                        throw new Exception("Erro interno do servidor\nTente novamente mais tarde");
                    }
                    if (!string.IsNullOrWhiteSpace(mensagem))
                    {
                        throw new Exception(mensagem);
                    }
                    throw new Exception(response.StatusCode.ToString());
                }
                catch (UnknownHostException ex)
                {
                    Debug.WriteLine("Connection > SearchByCity " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Conexão com servidor indisponível", ex);
                }
                catch (Javax.Net.Ssl.SSLException ex)
                {
                    Debug.WriteLine("Connection > SearchByCity  " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Conexão com a internet foi interrompida. Conecte-se e tente novamente.", ex);
                }
                catch (OperationCanceledException ex)
                {
                    Debug.WriteLine("Connection > SearchByCity " + ex.Message +" | "+ex.InnerException);
                    throw new Exception("Operação interrompida. Verifique sua conexão com a internet", ex);
                }
                catch (System.IO.EndOfStreamException ex)
                {
                    Debug.WriteLine("Connection > SearchByCity " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (System.IO.IOException ex)
                {
                    Debug.WriteLine("Connection > SearchByCity " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Connection > SearchByCity " + ex.Message +" | "+ex.InnerException);
                    throw ex;
                }
            }
        }
        public static async Task<bool> ForgotPassword(string cpf_email)
        {
            Uri uri = new Uri(URL);

            using (HttpClient http = new HttpClient())
            {
                try
                {
                    http.BaseAddress = uri;
                    http.Timeout = TimeSpan.FromSeconds(20);
                    HttpResponseMessage response = await http.PostAsync("v1/client/forgot-password/"+cpf_email, null);
                    string mensagem = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    if (response.StatusCode.ToString() == INTERNAL_SERVER_ERROR)
                    {
                        throw new Exception("Erro interno do servidor\nTente novamente mais tarde");
                    }
                    if (!string.IsNullOrWhiteSpace(mensagem))
                    {
                        throw new Exception(mensagem);
                    }
                    throw new Exception(response.StatusCode.ToString());

                }
                catch (UnknownHostException ex)
                {
                    Debug.WriteLine("Connection > ForgotPassword " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com servidor indisponível", ex);
                }
                catch (Javax.Net.Ssl.SSLException ex)
                {
                    Debug.WriteLine("Connection > ForgotPassword " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet foi interrompida. Conecte-se e tente novamente.", ex);
                }
                catch (OperationCanceledException ex)
                {
                    Debug.WriteLine("Connection > ForgotPassword " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Operação interrompida. Verifique sua conexão com a internet", ex);
                }
                catch (System.IO.EndOfStreamException ex)
                {
                    Debug.WriteLine("Connection > ForgotPassword " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (System.IO.IOException ex)
                {
                    Debug.WriteLine("Connection > ForgotPassword " + ex.Message + " | " + ex.InnerException);
                    throw new Exception("Conexão com a internet indisponível", ex);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Connection > ForgotPassword " + ex.Message + " | " + ex.InnerException);
                    throw ex;
                }
            }
        }
    }
}
