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

namespace Identity.SpecFlow.Tests.Drivers
{
    public class RemoteDriverFactory
    {
        private readonly string seleniumHubUrl = "http://localhost:4444/wd/hub"; // URL do Selenium Hub

        public IWebDriver CreateDriver(BrowserType browserType)
        {
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
            return new RemoteWebDriver(new Uri(seleniumHubUrl), options.ToCapabilities());
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
    }
}