using System;
using Identity.SpecFlow.Tests.Drivers;
using Identity.SpecFlow.Tests.PageModel;
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
        public RegistroDeUsuarioStepDefinitions(RemoteDriverFactory driverFactory)
        {
            _driverFactory = driverFactory;
            _driver = _driverFactory.CreateDriver(BrowserType.Chrome);
            _registerPageModel = new RegisterPageModel(_driver);
        }

        // Criação do driver antes de cada cenário
        [BeforeScenario]
        public void BeforeScenario()
        {
            //_driver = _driverFactory.CreateDriver(BrowserType.Chrome);
        }

        [Given(@"que o usuario esta na pagina de cadastro")]
        public void GivenQueOUsuarioEstaNaPaginaDeCadastro()
        {
            _registerPageModel.NavigateToRegisterPage();
        }


        [Given(@"que o usuário preenche todos os campos corretamente")]
        public void GivenQueOUsuarioPreencheTodosOsCamposCorretamente()
        {
            throw new PendingStepException();
        }

        [When(@"ele envia o formulário de registro")]
        public void WhenEleEnviaOFormularioDeRegistro()
        {
            throw new PendingStepException();
        }

        [Then(@"ele deve ser registrado com sucesso")]
        public void ThenEleDeveSerRegistradoComSucesso()
        {
            throw new PendingStepException();
        }

        [Given(@"que o usuário preenche o campo email com ""([^""]*)""")]
        public void GivenQueOUsuarioPreencheOCampoEmailCom(string emailinválido)
        {
            throw new PendingStepException();
        }

        [Then(@"ele deve ver a mensagem de erro ""([^""]*)""")]
        public void ThenEleDeveVerAMensagemDeErro(string p0)
        {
            throw new PendingStepException();
        }

        [Given(@"que o usuário não preenche o campo email")]
        public void GivenQueOUsuarioNaoPreencheOCampoEmail()
        {
            throw new PendingStepException();
        }

        [Given(@"que o usuário preenche a senha com ""([^""]*)""")]
        public void GivenQueOUsuarioPreencheASenhaCom(string p0)
        {
            throw new PendingStepException();
        }

        [Given(@"ele preenche a confirmação de senha com ""([^""]*)""")]
        public void GivenElePreencheAConfirmacaoDeSenhaCom(string p0)
        {
            throw new PendingStepException();
        }

        [Given(@"que o usuário não preenche o campo telefone")]
        public void GivenQueOUsuarioNaoPreencheOCampoTelefone()
        {
            throw new PendingStepException();
        }

        [Given(@"que o usuário preenche o telefone com ""([^""]*)""")]
        public void GivenQueOUsuarioPreencheOTelefoneCom(string p0)
        {
            throw new PendingStepException();
        }

        [Given(@"que o usuário não preenche o campo data de nascimento")]
        public void GivenQueOUsuarioNaoPreencheOCampoDataDeNascimento()
        {
            throw new PendingStepException();
        }

        [Given(@"que o usuário preenche a data de nascimento com ""([^""]*)""")]
        public void GivenQueOUsuarioPreencheADataDeNascimentoCom(string p0)
        {
            throw new PendingStepException();
        }

        [Given(@"que o usuário preenche o nome com ""([^""]*)""")]
        public void GivenQueOUsuarioPreencheONomeCom(string ana)
        {
            throw new PendingStepException();
        }

        [Given(@"que o usuário não preenche o campo nome")]
        public void GivenQueOUsuarioNaoPreencheOCampoNome()
        {
            throw new PendingStepException();
        }
    }
}
