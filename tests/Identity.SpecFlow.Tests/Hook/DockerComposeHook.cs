
using Ductus.FluentDocker.Model.Compose;
using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Services.Impl;
using Identity.Shared.Tests;

namespace Identity.SpecFlow.Tests.Hook
{
    [Binding]
    public class DockerComposeHook
    {
        [AfterTestRun]
        public static void AfterTestRun()
        {
            DockerComposeService.Dispose();
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            DockerComposeService.RunDockerCompose();
        }
    }
}
