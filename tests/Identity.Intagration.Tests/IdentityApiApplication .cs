using Identity.Infra.Repositories.Context;
using Identity.Shared.Tests;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;


namespace Identity.Intagration.Tests
{
    public class IdentityApiApplication : WebApplicationFactory<Program>
    {
        //nao preciso invocar esse metodo, o proprio Program ja faz isso 
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment("Test");

            DockerComposeService.RunDockerCompose("docker-compose-integration-test.yml");

            Thread.Sleep(TimeSpan.FromSeconds(10));

            return base.CreateHost(builder);
        }
    }
}