using System.Reflection;

namespace Identity.ArchUnit.Tests
{
    public class ProjectDependencyTests
    {
        [Fact]
        public void APICanOnlyReferenceDomain()
        {
            // Carrega o assembly da API (ajuste conforme seu nome de projeto)
            var apiAssembly = Assembly.Load("Identity.webapi"); // Nome do projeto de API
            var domainAssembly = Assembly.Load("Identity.Domain"); // Nome do projeto Domain
            var infraAssembly = Assembly.Load("Identity.Infra"); // Nome do projeto Infra

            // Verifica que o projeto da API só tem referência ao Domain, não ao Infra
            var apiReferences = apiAssembly.GetReferencedAssemblies();

            Assert.Contains(apiReferences, a => a.Name == domainAssembly.GetName().Name);
            Assert.DoesNotContain(apiReferences, a => a.Name == infraAssembly.GetName().Name);
        }

        [Fact]
        public void DomainCanOnlyReferenceInfra()
        {
            // Carrega os assemblies
            var domainAssembly = Assembly.Load("MyDomain");
            var infraAssembly = Assembly.Load("MyInfra");

            // Verifica que o projeto Domain só tem referência ao Infra
            var domainReferences = domainAssembly.GetReferencedAssemblies();

            Assert.Contains(domainReferences, a => a.Name == infraAssembly.GetName().Name);
            Assert.DoesNotContain(domainReferences, a => a.Name == "MyWebApp"); // Não pode depender da API
        }

        [Fact]
        public void InfraShouldNotReferenceAnyOtherAssembly()
        {
            // Carrega o assembly da Infra
            var infraAssembly = Assembly.Load("MyInfra");

            // Verifica que o projeto da Infra não tem referências externas
            var infraReferences = infraAssembly.GetReferencedAssemblies();

            Assert.Empty(infraReferences); // A Infra não deve referenciar nada
        }
    }
}