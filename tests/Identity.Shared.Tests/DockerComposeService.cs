using Ductus.FluentDocker.Model.Compose;
using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Services.Impl;
using Identity.SpecFlow.Tests.Support;

namespace Identity.Shared.Tests
{
    public static class DockerComposeService
    {
        public static ICompositeService CompositeService { get; private set; }
        public static IHostService DockerHost { get; private set; }

        public static void Dispose()
        {
            CompositeService.Dispose();
            DockerHost.Dispose();
        }

        public static void RunDockerCompose()
        {
            // Certifique-se de que o Docker Host está em execução
            EnsureDockerHost();

            // Caminho para o arquivo docker-compose.yml
            var projectDirectory = FileSupport.GetSolutionDirectory();

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

            // Iniciar os contêineres
            CompositeService.Start();
        }

        private static void EnsureDockerHost()
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
    }
}
