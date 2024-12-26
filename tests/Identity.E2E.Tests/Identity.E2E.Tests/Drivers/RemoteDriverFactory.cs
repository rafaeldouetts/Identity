using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using System.Diagnostics;

namespace Identity.E2E.Tests.Drivers
{
    public class RemoteDriverFactory
    {
        private readonly string seleniumHubUrl = "http://localhost:4444/wd/hub"; // URL do Selenium Hub
        private ScenarioContext _scenarioContext;
        private Process _process;
        public IWebDriver CreateDriver(BrowserType browserType, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            switch (browserType)
            {
                case BrowserType.Chrome:
                    return CreateChromeDriver();
                case BrowserType.Firefox:
                    return CreateFirefoxDriver();
                case BrowserType.Edge:
                    return CreateEdgeDriver();
                default:
                    throw new ArgumentException("Browser type is not supported.");
            }
        }

        private IWebDriver CreateChromeDriver()
        {
            var options = new ChromeOptions();
            //options.AddArgument("--start-maximized");
            //options.AddAdditionalOption("se:recordVideo", true);
            //options.AddAdditionalOption("SE_VIDEO_FILE_NAME", "test-video.mp4");
            //options.AddArgument("--video=mp4"); // Ativa gravação de vídeo
            //options.AddArgument("--SE_VIDEO_FILE_NAME=teste-video-2.mp4");

            Environment.SetEnvironmentVariable("VIDEO_NAME", $"{_scenarioContext.ScenarioInfo.Title}.mp4");
            ProcessDockerUp();

            return new RemoteWebDriver(new Uri(seleniumHubUrl), options);
        }

        private void ProcessDockerUp()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "docker-compose",
                Arguments = "up -d",  // Executa o docker-compose em background
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (_process = Process.Start(startInfo))
            {
                _process.WaitForExit();
            }
        }

        private void ProcessDockerDown()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "docker-compose",
                Arguments = "down -d",  // Executa o docker-compose em background
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (_process = Process.Start(startInfo))
            {
                _process.WaitForExit();
            }
        }

        private IWebDriver CreateFirefoxDriver()
        {
            var options = new FirefoxOptions();
            return new RemoteWebDriver(new Uri(seleniumHubUrl), options.ToCapabilities());
        }

        private IWebDriver CreateEdgeDriver()
        {
            var options = new EdgeOptions();
            return new RemoteWebDriver(new Uri(seleniumHubUrl), options.ToCapabilities());
        }

        public void Dispose()
        {
            //_process.Kill();
            //_process?.Dispose();
        }
    }
}