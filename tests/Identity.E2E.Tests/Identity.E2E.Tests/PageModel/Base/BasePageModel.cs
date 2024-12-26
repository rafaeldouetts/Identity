using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Identity.E2E.Tests.PageModel.Base
{
    namespace Identity.SpecFlow.Tests.PageModel.Base
    {
        // Classe base para o PageModel
        public class BasePageModel
        {
            // Propriedade para armazenar o driver do Selenium
            protected IWebDriver Driver { get; private set; }

            // Construtor que recebe o driver
            public BasePageModel(IWebDriver driver)
            {
                Driver = driver;
            }

            // Método para navegar até uma URL
            public void NavigateToUrl(string url)
            {
                Driver.Navigate().GoToUrl(url);
            }

            // Método para esperar até que um elemento esteja visível
            public void WaitForElement(By locator)
            {
                var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                wait.Until(driver => driver.FindElement(locator).Displayed);
            }
        }
    }
}