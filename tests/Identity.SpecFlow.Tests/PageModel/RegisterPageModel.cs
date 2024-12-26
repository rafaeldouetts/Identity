using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.SpecFlow.Tests.PageModel.Base;
using Identity.SpecFlow.Tests.PageModel.Base.Identity.SpecFlow.Tests.PageModel.Base;
using OpenQA.Selenium;

namespace Identity.SpecFlow.Tests.PageModel
{
    public class RegisterPageModel : BasePageModel
    {
        // Elementos da página
        private readonly By nomeField = By.Id("name");
        private readonly By nomeErrorField = By.Id("name-error");
        private readonly By emailField = By.Id("email");
        private readonly By emailErrorField = By.Id("email-error");
        private readonly By telefoneField = By.Id("phone");
        private readonly By telefoneErrorField = By.Id("phone-error");
        private readonly By dataNascimentoField = By.Id("birthDate");
        private readonly By dataNascimentoErrorField = By.Id("birthDate-error");
        private readonly By passwordField = By.Id("password");
        private readonly By passwordErrorField = By.Id("password-error");
        private readonly By confirmPasswordField = By.Id("confirm-password");
        private readonly By confirmPasswordErrorField = By.Id("confirm-password-error");
        private readonly By submitButton = By.CssSelector(".btn-submit");

        public void NavigateToRegisterPage()
        {
            Driver.Navigate().GoToUrl("http://identity.WebApp/account/register");
        }

        // Construtor que recebe o driver
        public RegisterPageModel(IWebDriver driver) : base(driver) { }

        // Preenche o campo de nome
        public void SetNome(string nome)
        {
            Driver.FindElement(nomeField).SendKeys(nome);
            Driver.FindElement(passwordField).SendKeys(Keys.Tab);
        }

        // Preenche o campo de e-mail
        public void SetEmail(string email)
        {
            Driver.FindElement(emailField).SendKeys(email);
            Driver.FindElement(passwordField).SendKeys(Keys.Tab);
        }

        // Preenche o campo de telefone
        public void SetTelefone(string telefone)
        {
            Driver.FindElement(telefoneField).SendKeys(telefone);
            Driver.FindElement(passwordField).SendKeys(Keys.Tab);
        }

        // Preenche o campo de data de nascimento
        public void SetDataNascimento(string dataNascimento)
        {
            Driver.FindElement(dataNascimentoField).SendKeys(dataNascimento);
            Driver.FindElement(passwordField).SendKeys(Keys.Tab);
            Driver.FindElement(passwordField).SendKeys(Keys.Tab);
        }

        // Preenche o campo de senha
        public void SetPassword(string password)
        {
            Driver.FindElement(passwordField).SendKeys(password);
            Driver.FindElement(passwordField).SendKeys(Keys.Tab);
        }

        // Preenche o campo de confirmação de senha
        public void SetConfirmPassword(string confirmPassword)
        {
            Driver.FindElement(confirmPasswordField).SendKeys(confirmPassword);
            Driver.FindElement(passwordField).SendKeys(Keys.Tab);
        }

        // Clica no botão de cadastro
        public void SubmitForm()
        {
            Driver.FindElement(submitButton).Click();
        }

        // Método para preencher todos os campos e submeter o formulário
        public void FillAndSubmitForm(string nome, string email, string telefone, string dataNascimento, string password, string confirmPassword)
        {
            SetNome(nome);
            SetEmail(email);
            SetTelefone(telefone);
            SetDataNascimento(dataNascimento);
            SetPassword(password);
            SetConfirmPassword(confirmPassword);
            SubmitForm();
        }

        public string GetNomeErro()
        {
            return Driver.FindElement(nomeErrorField).Text;
        }

        public string GetEmailErro()
        {
            return Driver.FindElement(emailErrorField).Text;
        }
        public string GetTelefoneErro()
        {
            return Driver.FindElement(telefoneErrorField).Text;
        }

        public string GetDataNascimentoErro()
        {
            return Driver.FindElement(dataNascimentoErrorField).Text;
        }

        public string GetSenhaErro()
        {
            return Driver.FindElement(passwordErrorField).Text;
        }

        public string GetConfirmacaoSenhaErro()
        {
            return Driver.FindElement(confirmPasswordErrorField).Text;
        }


    }
}
