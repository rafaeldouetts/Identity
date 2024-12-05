Feature: Registro de usuário

  Como um usuário
  Eu quero me registrar no sistema
  Para que eu possa acessar a plataforma

  Scenario: Registrar um usuário com sucesso
    Given que o usuário preenche todos os campos corretamente
    When ele envia o formulário de registro
    Then ele deve ser registrado com sucesso

  Scenario: Tentar registrar um usuário com email inválido
    Given que o usuário preenche o campo email com "emailinválido"
    When ele envia o formulário de registro
    Then ele deve ver a mensagem de erro "Email inválido."

  Scenario: Tentar registrar um usuário sem preencher o email
    Given que o usuário não preenche o campo email
    When ele envia o formulário de registro
    Then ele deve ver a mensagem de erro "O email é obrigatório."

  Scenario: Tentar registrar um usuário com senha curta
    Given que o usuário preenche a senha com "12345"
    When ele envia o formulário de registro
    Then ele deve ver a mensagem de erro "A senha deve ter no mínimo 8 caracteres."

  Scenario: Tentar registrar um usuário com senha sem letra maiúscula
    Given que o usuário preenche a senha com "senha123"
    When ele envia o formulário de registro
    Then ele deve ver a mensagem de erro "A senha deve conter pelo menos 8 caracteres, incluindo uma letra maiúscula, uma letra minúscula, um número e um símbolo."

  Scenario: Tentar registrar um usuário com senha sem símbolo
    Given que o usuário preenche a senha com "Senha123"
    When ele envia o formulário de registro
    Then ele deve ver a mensagem de erro "A senha deve conter pelo menos 8 caracteres, incluindo uma letra maiúscula, uma letra minúscula, um número e um símbolo."

  Scenario: Tentar registrar um usuário com senhas não coincidentes
    Given que o usuário preenche a senha com "Senha123!"
    And ele preenche a confirmação de senha com "Senha124!"
    When ele envia o formulário de registro
    Then ele deve ver a mensagem de erro "As senhas não coincidem."

  Scenario: Tentar registrar um usuário sem preencher o telefone
    Given que o usuário não preenche o campo telefone
    When ele envia o formulário de registro
    Then ele deve ver a mensagem de erro "O telefone é obrigatório."

  Scenario: Tentar registrar um usuário com telefone inválido
    Given que o usuário preenche o telefone com "12345"
    When ele envia o formulário de registro
    Then ele deve ver a mensagem de erro "Número de telefone inválido."

  Scenario: Tentar registrar um usuário sem preencher a data de nascimento
    Given que o usuário não preenche o campo data de nascimento
    When ele envia o formulário de registro
    Then ele deve ver a mensagem de erro "A data de nascimento é obrigatória."

  Scenario: Tentar registrar um usuário com uma data de nascimento inválida
    Given que o usuário preenche a data de nascimento com "32/13/2024"
    When ele envia o formulário de registro
    Then ele deve ver a mensagem de erro "Data inválida."

  Scenario: Tentar registrar um usuário com nome muito curto
    Given que o usuário preenche o nome com "Ana"
    When ele envia o formulário de registro
    Then ele deve ver a mensagem de erro "O nome deve ter no mínimo 8 caracteres."

  Scenario: Tentar registrar um usuário sem preencher o nome
    Given que o usuário não preenche o campo nome
    When ele envia o formulário de registro
    Then ele deve ver a mensagem de erro "O telefone é obrigatório."
