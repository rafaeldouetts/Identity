using BenchmarkDotNet.Attributes;
using Identity.Domain.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Benchmark.Tests.Benchmarks
{
    public class RegistroBenchmark
    {
        private static readonly HttpClient _client = new HttpClient();
        private const string _url = "http://localhost:5001/api/Account/register"; 

        // Método para simular o envio do formulário de registro
        [Benchmark]
        public async Task RegisterUserBenchmark()
        {
            var registroData = new RegisterModel
            {
                Nome = "Teste Usuario",
                Email = "teste.usuario@exemplo.com",
                Password = "Teste@123",
                ConfirmPassword = "Teste@123",
                Telefone = "11986782886",
                DataNascimento = DateTime.Now.AddYears(-20)
            };

            var jsonContent = JsonConvert.SerializeObject(registroData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Enviar a requisição para o endpoint de registro
            var response = await _client.PostAsync(_url, content);

            // Simples validação da resposta (pode ser ajustada conforme o seu caso)
            response.EnsureSuccessStatusCode();
        }
    }
}
