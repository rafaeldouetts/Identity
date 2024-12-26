using System;
using Identity.SpecFlow.Tests.Drivers;
using Identity.SpecFlow.Tests.PageModel;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Identity.SpecFlow.Tests.StepDefinitions
{
    [Binding]
    public class RegistroDeUsuarioStepDefinitions
    {
        private IWebDriver _driver;
        private readonly RemoteDriverFactory _driverFactory;
        private readonly RegisterPageModel _registerPageModel;

        public RegistroDeUsuarioStepDefinitions(RemoteDriverFactory driverFactory, ScenarioContext scenarioContext)
        {
            _driverFactory = driverFactory;
            _driver = _driverFactory.CreateDriver(BrowserType.Chrome, scenarioContext);

            _driver.Manage().Window.Maximize();

            Thread.Sleep(TimeSpan.FromSeconds(5));

            _registerPageModel = new RegisterPageModel(_driver);
        }

        // Criação do driver antes de cada cenário
        [BeforeScenario]
        public void BeforeScenario()
        {
            //_driver = _driverFactory.CreateDriver(BrowserType.Chrome);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver.Quit();
            _driver.Dispose();
            _driverFactory.Dispose();
        }

        [Given(@"que o usuario esta na pagina de cadastro")]
        public void GivenQueOUsuarioEstaNaPaginaDeCadastro()
        {
            Thread.Sleep(TimeSpan.FromSeconds(10));
            _registerPageModel.NavigateToRegisterPage();
        }

        [Given(@"que o usuario preenche todos os campos corretamente")]
        public void GivenQueOUsuarioPreencheTodosOsCamposCorretamente()
        {
            Thread.Sleep(TimeSpan.FromSeconds(15));

            _registerPageModel.SetNome("testeddddd");
            _registerPageModel.SetEmail("teste2@teste.com");
            _registerPageModel.SetTelefone("11986782886");
            _registerPageModel.SetDataNascimento("01012000");
            _registerPageModel.SetPassword("Teste@123");
            _registerPageModel.SetConfirmPassword("Teste@123");
        }

        [When(@"ele envia o formulario de registro")]
        public void WhenEleEnviaOFormularioDeRegistro()
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));

            _registerPageModel.SubmitForm();
        }

        [Then(@"ele deve ser registrado com sucesso")]
        public void ThenEleDeveSerRegistradoComSucesso()
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));

            var loginpage = _driver.Url.Contains("login");

            Assert.True(loginpage);
        }
        [Given(@"que o usuario preenche a senha como ""([^""]*)""")]
        public void GivenQueOUsuarioPreencheASenhaComo(string senha)
        {
            _registerPageModel.SetPassword(senha);
        }

        [Given(@"a confirmação de senha como ""([^""]*)""")]
        public void GivenAConfirmacaoDeSenhaComo(string confirmacaoSenha)
        {
            _registerPageModel.SetConfirmPassword(confirmacaoSenha);
        }

        [Given(@"que o usuario preenche o e-mail como ""([^""]*)""")]
        public void GivenQueOUsuarioPreencheOE_MailComo(string email)
        {
            _registerPageModel.SetEmail(email);
        }

        [Then(@"o email deve exibir a mensagem ""([^""]*)""")]
        public void ThenOEmailDeveExibirAMensagem(string mensagem)
        {
            var error = _registerPageModel.GetEmailErro();

            Assert.Equal(mensagem, error);
        }

        //[Given(@"que o usuario preenche a senha como ""([^""]*)""")]
        //public void GivenQueOUsuarioPreencheASenhaComo(string senha)
        //{
        //    _registerPageModel.SetPassword(senha);
        //}

        [Then(@"a senha deve exibir a mensagem ""([^""]*)""")]
        public void ThenASenhaDeveExibirAMensagem(string mensagem)
        {
            var error = _registerPageModel.GetSenhaErro();

            Assert.Equal(mensagem, error);
        }

        [Then(@"a confirmacao de senha deve exibir a mensagem ""([^""]*)""")]
        public void ThenAConfirmacaoDeSenhaDeveExibirAMensagem(string mensagem)
        {
            var error = _registerPageModel.GetConfirmacaoSenhaErro();

            Assert.Equal(mensagem, error);
        }

        [Given(@"que o usuario preenche o telefone como ""([^""]*)""")]
        public void GivenQueOUsuarioPreencheOTelefoneComo(string telefone)
        {
            _registerPageModel.SetTelefone(telefone);
        }

        [Then(@"o telefone deve exibir a mensagem ""(.*)""")]
        public void ThenOTelefoneDeveExibirAMensagem(string mensagem)
        {
            var error = _registerPageModel.GetTelefoneErro();

            Assert.Equal(mensagem, error);
        }

        [Given(@"que o usuario deixa o campo nome vazio")]
        public void GivenQueOUsuarioDeixaOCampoNomeVazio()
        {
            _registerPageModel.SetNome("");
        }

        [Then(@"o nome deve exibir a mensagem ""([^""]*)""")]
        public void ThenONomeDeveExibirAMensagem(string mensagem)
        {
            var error = _registerPageModel.GetNomeErro();

            Assert.Equal(mensagem, error);
        }

        [Given(@"que o usuario deixa o campo e-mail vazio")]
        public void GivenQueOUsuarioDeixaOCampoE_MailVazio()
        {
            _registerPageModel.SetEmail("");
        }

        [Given(@"que o usuario deixa o campo telefone vazio")]
        public void GivenQueOUsuarioDeixaOCampoTelefoneVazio()
        {
            _registerPageModel.SetTelefone("");
        }

        [Given(@"que o usuario deixa o campo data de nascimento vazio")]
        public void GivenQueOUsuarioDeixaOCampoDataDeNascimentoVazio()
        {
            _registerPageModel.SetDataNascimento("");
        }

        [Given(@"que o usuario deixa o campo senha vazio")]
        public void GivenQueOUsuarioDeixaOCampoSenhaVazio()
        {
            _registerPageModel.SetPassword("");
        }

        [Given(@"que o usuario deixa o campo confirmar senha vazio")]
        public void GivenQueOUsuarioDeixaOCampoConfirmarSenhaVazio()
        {
            _registerPageModel.SetConfirmPassword("");
        }

        [Given(@"que o usuário preenche a senha com ""([^""]*)""")]
        public void GivenQueOUsuarioPreencheASenhaCom(string senha)
        {
            _registerPageModel.SetPassword(senha);
        }

        [Given(@"que o usuário preenche a data de nascimento com ""([^""]*)""")]
        public void GivenQueOUsuarioPreencheADataDeNascimentoCom(string dataNascimento)
        {
            _registerPageModel.SetDataNascimento(dataNascimento);
        }

        [Then(@"a data de nascimento deve exibir a mensagem ""([^""]*)""")]
        public void ThenADataDeNascimentoDeveExibirAMensagem(string mensagem)
        {
            var error = _registerPageModel.GetDataNascimentoErro();

            Assert.Equal(mensagem, error);
        }

        [Given(@"que o usuário preenche o nome com ""([^""]*)""")]
        public void GivenQueOUsuarioPreencheONomeCom(string nome)
        {
            _registerPageModel.SetNome(nome);
        }

        //[Then(@"o nome deve exibir a mensagem  ""([^""]*)""")]
        //public void ThenONomeDeveExibirAMensagem(string p0)
        //{
        //    throw new PendingStepException();
        //}

    }
}
