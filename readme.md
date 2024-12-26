# API de Gerenciamento de Contas

Esta API fornece endpoints para o gerenciamento de contas de usu�rios, incluindo registro, login, gerenciamento de perfil, autentica��o em dois fatores (2FA) e mais.

---

## Endpoints Principais

### Autentica��o e Usu�rio
- `POST /api/account/register` - Registro de novo usu�rio.
- `POST /api/account/login` - Login do usu�rio.
- `POST /api/account/logout` - Logout do usu�rio.
- `POST /api/account/change-password` - Troca de senha.
- `POST /api/account/forgot-password` - Recupera��o de senha.
- `POST /api/account/reset-password` - Redefini��o de senha.
- `PUT /api/account/update-profile` - Atualiza��o de perfil do usu�rio.
- `POST /api/account/upload-profile-picture` - Atualiza��o da foto de perfil.

### Confirma��o e Verifica��o
- `POST /api/account/send-email-confirmation` - Envio de token para confirma��o de e-mail.
- `POST /api/account/validate-email-token` - Valida��o de token de e-mail.
- `POST /api/account/send-phone-confirmation` - Envio de token para confirma��o de telefone.
- `POST /api/account/validate-phone-token` - Valida��o de token de telefone.

### Autentica��o em Dois Fatores (2FA)
- `POST /api/account/send-2fa-code` - Envio de c�digo para autentica��o em dois fatores.
- `POST /api/account/validate-2fa-code` - Valida��o do c�digo de autentica��o em dois fatores.

---

## Tecnologias Utilizadas

- **ASP.NET Core**: Desenvolvimento da API.
- **Identity Framework**: Gerenciamento de autentica��o e usu�rios.
- **Redis**: Armazenamento tempor�rio de tokens.
- **Blob Storage**: Upload e armazenamento de arquivos.
- **2FA (Autentica��o em Dois Fatores)**: Implementado via e-mail ou telefone.

---

## Configura��o

1. **Banco de Dados**: Configure o acesso ao banco de dados no `appsettings.json`.
2. **Redis**: Insira as credenciais para o Redis no arquivo de configura��o.
3. **Blob Storage**: Configure o acesso para upload de arquivos.
4. **Servi�os Externos**: Configure provedores de e-mail e WhatsApp para notifica��es.

---

## Estrutura de Testes

### 1. Testes de Unidade
- **Frameworks utilizados**: 
  - [xUnit](https://xunit.net/) para estruturar os testes.
  - [FluentAssertions](https://fluentassertions.com/) para criar assertivas leg�veis e fluentes.
- **Objetivo**: 
  - Garantir que as regras de neg�cio e os m�todos individuais da aplica��o funcionem como esperado.
- **Escopo**:
  - Valida��o de entrada de dados.
  - M�todos de utilit�rios e servi�os internos.

### 2. Testes de Integra��o
- **Frameworks utilizados**:
  - [xUnit](https://xunit.net/) para execu��o dos testes.
  - [Ductus.FluentDocker](https://github.com/mariotoffia/FluentDocker) para gerenciar containers Docker a partir de um arquivo `docker-compose.yml`.
  - [HttpClient](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient) para simular chamadas � API.
- **Objetivo**:
  - Verificar a integra��o entre os componentes principais do sistema, como API, banco de dados e Redis.
- **Processo**:
  - Subir o ambiente de teste (Redis, banco de dados, e API) usando o `docker-compose`.
  - Executar os testes simulando cen�rios reais de uso.

### 3. Testes de Fim a Fim (E2E)
- **Frameworks utilizados**:
  - [Selenium WebDriver](https://www.selenium.dev/) para automa��o dos testes no navegador.
  - Selenium Hub para executar os testes em m�ltiplos navegadores.
- **Navegadores testados**:
  - Microsoft Edge.
  - Google Chrome.
  - Mozilla Firefox.
- **Objetivo**:
  - Validar a experi�ncia completa do usu�rio na interface da aplica��o, verificando se todos os fluxos funcionam conforme esperado.
- **Processo**:
  - Configurar o Selenium Hub com os n�s para cada navegador.
  - Executar os testes nos tr�s navegadores.

---

## Recursos Adicionais

### Seguran�a
- **JWT (JSON Web Token)**: Usado para autentica��o e autoriza��o de endpoints.
- **Criptografia**: Todas as senhas s�o armazenadas utilizando hashing seguro (ex.: BCrypt).
- **Pol�ticas de Acesso**: Implementa��o de roles e permiss�es para controle granular de acessos.

### Escalabilidade
- **Redis**: Para armazenamento tempor�rio e suporte a autentica��o em dois fatores.
- **Blob Storage**: Armazenamento de arquivos de forma escal�vel.

---

## Como Contribuir

1. Fa�a um fork deste reposit�rio.
2. Crie uma branch para suas altera��es (`git checkout -b feature/sua-feature`).
3. Fa�a commit das altera��es (`git commit -m 'Adicionando uma nova feature'`).
4. Envie um push para sua branch (`git push origin feature/sua-feature`).
5. Abra um pull request para revis�o.

---

## Licen�a

Este projeto � licenciado sob a licen�a [MIT](LICENSE).

---

## Observa��o

Para detalhes t�cnicos sobre os endpoints, como exemplos de body e respostas, consulte a documenta��o gerada automaticamente pelo Swagger dispon�vel no `/swagger`.


para alterar o specflow para portugues e conseguir utilizar as palavras (Dado, Quando, Entao) precisamos configurar o arquivo specflow.json com as seguintes configuracoes 
{
  "bindingCulture": {
    "language": "pt-br"
  },
  "language": {
    "feature": "pt-br"
  }
}


e para os metodos conseguirem interpretar caracteres especiais dentro das variaveis, precisamos salvar o arquivo como utf8


Abra o arquivo .feature no Visual Studio.

Vá até o menu Arquivo.

Selecione Salvar Como....

Na janela de salvar, clique na seta para baixo (ao lado do botão Salvar).

Selecione a opção Codificação....

Isso abrirá uma janela onde você poderá ver a codificação atual do arquivo. Se estiver usando UTF-8, você verá essa opção selecionada. Caso contrário, você pode alterar a codificação para UTF-8.