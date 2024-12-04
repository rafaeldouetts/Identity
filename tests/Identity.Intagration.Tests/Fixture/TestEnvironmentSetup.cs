using Ductus.FluentDocker.Model.Compose;
using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Services.Impl;
using Microsoft.VisualStudio.TestPlatform.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Intagration.Tests.Fixture
{
    public class TestEnvironmentSetup : IAsyncLifetime
    {
        public ICompositeService CompositeService { get; private set; }
        public IHostService DockerHost { get; private set; }

        public TestEnvironmentSetup()
        {
            // Certifique-se de que o Docker Host está em execução
            EnsureDockerHost();

            // Caminho para o arquivo docker-compose.yml
            var projectDirectory = Helpers.FileHelper.GetSolutionDirectory();

            string dockerComposeFile = Path.Combine(projectDirectory, "docker-compose.yml");

            // Configuração para o Docker Compose
            var config = new DockerComposeConfig
            {
                ComposeFilePath = new[] { dockerComposeFile },
                ForceRecreate = true, // Forçar a recriação dos contêineres
                RemoveOrphans = true, // Remover contêineres órfãos
                StopOnDispose = true  // Parar os contêineres ao descartar a fixture
            };

            // Criar o serviço Docker Compose
            CompositeService = new DockerComposeCompositeService(DockerHost, config);

        }

        private void EnsureDockerHost()
        {
            // Verificar se o Docker Host está em execução
            if (DockerHost?.State == ServiceRunningState.Running)
                return;

            // Descobrir os hosts Docker disponíveis
            var hosts = new Hosts().Discover();

            // Selecionar o Docker Host nativo ou o padrão, se disponível
            DockerHost = hosts.FirstOrDefault(x => x.IsNative) ?? hosts.FirstOrDefault(x => x.Name == "default");

            // Iniciar o Docker Host, se necessário
            if (DockerHost?.State != ServiceRunningState.Running)
                DockerHost?.Start();
        }

        public async Task InitializeAsync()
        {
            // Levantar os containers do Docker
            Console.WriteLine("Iniciando os containers do Docker...");

            // Iniciar os contêineres
            CompositeService.Start();
        }

        public async Task DisposeAsync()
        {
            // Derrubar os containers após os testes
            Console.WriteLine("Parando os containers do Docker...");
            CompositeService?.Dispose();
            DockerHost?.Dispose();
        }
    }
}
