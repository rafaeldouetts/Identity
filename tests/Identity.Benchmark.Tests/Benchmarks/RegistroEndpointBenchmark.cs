using BenchmarkDotNet.Attributes;
using Identity.Domain.Domain;
using Newtonsoft.Json;
using System.Text;

namespace Identity.Benchmark.Tests.Benchmarks
{
    [Config(typeof(AntiVirusFriendlyConfig))]
    [MemoryDiagnoser]
    public class RegistroBenchmark
    {
        private static readonly HttpClient _client = new HttpClient();
        private const string _url = "http://localhost:5001/api/Account/register";

        // Método para simular o envio do formulário de registro
        [Benchmark]
        public void RegisterUserBenchmark()
        {
            try
            {
                var result = MakeApiRequestUntilSuccessful(_url).Result;

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
                var response = _client.PostAsync(_url, content).Result;

                // Simples validação da resposta (pode ser ajustada conforme o seu caso)
                //if ((int)response.EnsureSuccessStatusCode().StatusCode != StatusCodes.Status200OK)
                //{
                //    Console.WriteLine(response.Content.ToString());
                //}
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // Método separado para fazer a requisição até a API responder com sucesso
        public async Task<bool> MakeApiRequestUntilSuccessful(string apiUrl)
        {
            bool apiResponded = false;
            int maxAttempts = 10;  // Número máximo de tentativas
            int attempts = 0;

            while (!apiResponded && attempts < maxAttempts)
            {
                try
                {
                    HttpResponseMessage response = await _client.PostAsync(apiUrl, null);

                    if (response.IsSuccessStatusCode || (int)response.StatusCode == 404 || (int)response.StatusCode == 400 || (int)response.StatusCode == 415)
                    {
                        apiResponded = true;
                        // Processamento da resposta, se necessário
                        string responseData = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Dados da resposta: " + responseData);
                    }
                    else
                    {
                        Console.WriteLine($"Tentativa {attempts + 1}: API respondeu com erro: {response.StatusCode}");
                        Console.WriteLine($"Url: {_url}");
                        attempts++;
                        await Task.Delay(2000);  // Espera de 2 segundos antes de tentar novamente
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Tentativa {attempts + 1}: Erro ao fazer a requisição: {e.Message}");
                    attempts++;
                    await Task.Delay(2000);  // Espera de 2 segundos antes de tentar novamente
                }
            }

            return apiResponded;
        }
    }
}
