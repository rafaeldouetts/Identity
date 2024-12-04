# API de Gerenciamento de Contas

Esta API fornece endpoints para o gerenciamento de contas de usuários, incluindo registro, login, gerenciamento de perfil, autenticação em dois fatores (2FA) e mais.

---

## Endpoints Principais

### Autenticação e Usuário
- `POST /api/account/register` - Registro de novo usuário.
- `POST /api/account/login` - Login do usuário.
- `POST /api/account/logout` - Logout do usuário.
- `POST /api/account/change-password` - Troca de senha.
- `POST /api/account/forgot-password` - Recuperação de senha.
- `POST /api/account/reset-password` - Redefinição de senha.
- `PUT /api/account/update-profile` - Atualização de perfil do usuário.
- `POST /api/account/upload-profile-picture` - Atualização da foto de perfil.

### Confirmação e Verificação
- `POST /api/account/send-email-confirmation` - Envio de token para confirmação de e-mail.
- `POST /api/account/validate-email-token` - Validação de token de e-mail.
- `POST /api/account/send-phone-confirmation` - Envio de token para confirmação de telefone.
- `POST /api/account/validate-phone-token` - Validação de token de telefone.

### Autenticação em Dois Fatores (2FA)
- `POST /api/account/send-2fa-code` - Envio de código para autenticação em dois fatores.
- `POST /api/account/validate-2fa-code` - Validação do código de autenticação em dois fatores.

---

## Tecnologias Utilizadas

- **ASP.NET Core**: Desenvolvimento da API.
- **Identity Framework**: Gerenciamento de autenticação e usuários.
- **Redis**: Armazenamento temporário de tokens.
- **Blob Storage**: Upload e armazenamento de arquivos.
- **2FA (Autenticação em Dois Fatores)**: Implementado via e-mail ou telefone.

---

## Configuração

1. **Banco de Dados**: Configure o acesso ao banco de dados no `appsettings.json`.
2. **Redis**: Insira as credenciais para o Redis no arquivo de configuração.
3. **Blob Storage**: Configure o acesso para upload de arquivos.
4. **Serviços Externos**: Configure provedores de e-mail e WhatsApp para notificações.

---

## Estrutura de Testes

### 1. Testes de Unidade
- **Frameworks utilizados**: 
  - [xUnit](https://xunit.net/) para estruturar os testes.
  - [FluentAssertions](https://fluentassertions.com/) para criar assertivas legíveis e fluentes.
- **Objetivo**: 
  - Garantir que as regras de negócio e os métodos individuais da aplicação funcionem como esperado.
- **Escopo**:
  - Validação de entrada de dados.
  - Métodos de utilitários e serviços internos.

### 2. Testes de Integração
- **Frameworks utilizados**:
  - [xUnit](https://xunit.net/) para execução dos testes.
  - [Ductus.FluentDocker](https://github.com/mariotoffia/FluentDocker) para gerenciar containers Docker a partir de um arquivo `docker-compose.yml`.
  - [HttpClient](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient) para simular chamadas à API.
- **Objetivo**:
  - Verificar a integração entre os componentes principais do sistema, como API, banco de dados e Redis.
- **Processo**:
  - Subir o ambiente de teste (Redis, banco de dados, e API) usando o `docker-compose`.
  - Executar os testes simulando cenários reais de uso.

### 3. Testes de Fim a Fim (E2E)
- **Frameworks utilizados**:
  - [Selenium WebDriver](https://www.selenium.dev/) para automação dos testes no navegador.
  - Selenium Hub para executar os testes em múltiplos navegadores.
- **Navegadores testados**:
  - Microsoft Edge.
  - Google Chrome.
  - Mozilla Firefox.
- **Objetivo**:
  - Validar a experiência completa do usuário na interface da aplicação, verificando se todos os fluxos funcionam conforme esperado.
- **Processo**:
  - Configurar o Selenium Hub com os nós para cada navegador.
  - Executar os testes nos três navegadores.

---

## Recursos Adicionais

### Segurança
- **JWT (JSON Web Token)**: Usado para autenticação e autorização de endpoints.
- **Criptografia**: Todas as senhas são armazenadas utilizando hashing seguro (ex.: BCrypt).
- **Políticas de Acesso**: Implementação de roles e permissões para controle granular de acessos.

### Escalabilidade
- **Redis**: Para armazenamento temporário e suporte a autenticação em dois fatores.
- **Blob Storage**: Armazenamento de arquivos de forma escalável.

---

## Como Contribuir

1. Faça um fork deste repositório.
2. Crie uma branch para suas alterações (`git checkout -b feature/sua-feature`).
3. Faça commit das alterações (`git commit -m 'Adicionando uma nova feature'`).
4. Envie um push para sua branch (`git push origin feature/sua-feature`).
5. Abra um pull request para revisão.

---

## Licença

Este projeto é licenciado sob a licença [MIT](LICENSE).

---

## Observação

Para detalhes técnicos sobre os endpoints, como exemplos de body e respostas, consulte a documentação gerada automaticamente pelo Swagger disponível no `/swagger`.

