using Identity.Domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Identity.Domain.Services
{
    public class WhatsAppService : IWhatsappService
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _jsonSerializerOptions;
        private string accessToken;
        private string version;
        private string numeroRemetente;

        public WhatsAppService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            accessToken = configuration["Token:Whatsapp"];
            version = configuration["Versoes:Whatsapp"];
            numeroRemetente = configuration["Telefones:Whatsapp"];
        }

        public async Task<object> AdicionarNumero(string numero)
        {
            var apiUrl = $"{version}/{numeroRemetente}/register";

            var body = new AdicionarNumeroViewModel(numero);

            var content = new StringContent(JToken.FromObject(body).ToString(), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _httpClient.PostAsync(apiUrl, content);

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStringAsync();

            return JToken.Parse(responseStream);
        }

        public async Task<object> EnviarMensagem(string numero, string template, List<Parameters> parameters = null)
        {
            var apiUrl = $"{version}/{numeroRemetente}/messages";

            var body = new Mensagem(numero, template, parameters);

            var content = new StringContent(JToken.FromObject(body).ToString(), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _httpClient.PostAsync(apiUrl, content);

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStringAsync();

            return JToken.Parse(responseStream);
        }
        public async Task<Object> ObterNumerosCadastrados(string idProjeto)
        {

            //https://business.facebook.com/settings/whatsapp-business-accounts/102177005988755?business_id=179182074675490

            var apiUrl = $"{version}/{idProjeto}/phone_numbers";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _httpClient.GetAsync(apiUrl);

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStringAsync();

            return JToken.Parse(responseStream);

        }

        public async Task<Object> ObterNumeroCadastrado(string idNumero)
        {
            //https://business.facebook.com/settings/whatsapp-business-accounts/102177005988755?business_id=179182074675490

            var apiUrl = $"{version}/{idNumero}/phone_numbers";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _httpClient.GetAsync(apiUrl);

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStringAsync();

            return JToken.Parse(responseStream);
        }

        public Task SendSmsAsync(string phoneNumber, string message)
        {
            throw new NotImplementedException();
        }
    }


    public class Mensagem
    {
        public Mensagem(string to, string template, List<Parameters> parameters = null)
        {
            this.to = to;
            this.template = new Template(template);
            this.parameters = parameters;
        }

        public string messaging_product { get => "whatsapp"; }
        public string to { get; set; }
        public string type { get => "template"; }
        public Template template { get; set; }
        public List<Parameters> parameters { get; set; }
    }

    public class Template
    {
        public Template(string name)
        {
            this.name = name;
            language = new Idioma();
        }

        public string name { get; set; }
        public Idioma language { get; set; }
    }

    public class Idioma
    {
        public string code { get => "en_US"; }
    }

    public enum TipoMensagem
    {
        template = 0
    }

    public class Parameters
    {
        public Parameters(string value)
        {
            Text = value;    
        }

        public string Type { get => "text"; }
        public string Text { get; set; }
    }

    public class AdicionarNumeroViewModel
    {
        public AdicionarNumeroViewModel(string pin)
        {
            this.pin = pin;
        }

        public string messaging_product { get => "whatsapp"; }
        public string pin { get; set; }
    }
}
