using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace UpService.Models
{
    public class CEP
    {
        
        [JsonProperty(PropertyName = "cep")]
        public string CEP_Code { get; set; }

        [JsonProperty(PropertyName = "logradouro")]
        public string Endereco { get; set; }

        [JsonProperty(PropertyName = "complemento")]
        public string Complemento { get; set; }

        [JsonProperty(PropertyName = "bairro")]
        public string Bairro { get; set; }

        [JsonProperty(PropertyName = "localidade")]
        public string Cidade { get; set; }

        [JsonProperty(PropertyName = "uf")]
        public string Estado { get; set; }
    }
}
