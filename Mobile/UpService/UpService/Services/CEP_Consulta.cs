using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using UpService.Models;

namespace UpService.Services
{
    public class CEP_Consulta
    {
        private Uri url;
        public CEP_Consulta(string cep)
        {
            string link = "http://viacep.com.br/ws/{0}/json";
            url = new Uri(string.Format(link, cep));
        }

        public CEP GetCEPAndMore()
        {
            try
            {
                using(WebClient web = new WebClient())
                {
                    string result = web.DownloadString(url);
                    CEP obj = JsonConvert.DeserializeObject<CEP>(result);
                    return obj;
                }               
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
