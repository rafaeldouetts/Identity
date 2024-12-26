#Funcionalidade: Registro de usuario
#
#  Como um usuário
#  Eu quero me registrar no sistema
#  Para que eu possa acessar a plataforma
#
#@tag
#  Cenario: Registrar um usuario com sucesso
#    Dado que o usuario esta na pagina de cadastro
#    E que o usuario preenche todos os campos corretamente
#    Quando ele envia o formulario de registro
#    Entao ele deve ser registrado com sucesso
#
#  Cenario: Erro ao tentar registrar com senhas que não coincidem
#    Dado que o usuario esta na pagina de cadastro
#    E que o usuario preenche a senha como "Senha123"
#    E a confirmacao de senha como "Senha321"
#    Entao a confirmacao de senha deve exibir a mensagem "As senhas não coincidem."
#
#
##  Cenario: Erro ao tentar registrar com senhas que não coincidem
##    Dado que o usuario esta na pagina de cadastro
##    E que o usuario preenche a senha com o valor "Senha123" 
##    E a confirmação de senha como "Senha321"
##    Entao a confirmacao de senha deve exibir a mensagem "As senhas não coincidem."
##
##  Cenario: Erro ao tentar registrar com e-mail inválido
##    Dado que o usuario esta na pagina de cadastro
##    E que o usuario preenche o e-mail como "usuario_invalido.com"
##    Entao o email deve exibir a mensagem "Email inválido."
##
##  Cenario: Erro ao tentar registrar com senha fraca
##    Dado que o usuario esta na pagina de cadastro
##    E que o usuario preenche a senha com o valor invalido
##    Entao a senha deve exibir a mensagem "A senha deve ter no mínimo 8 caracteres."
##
##  Cenario: Erro ao tentar registrar com número de telefone inválido
##    Dado que o usuario esta na pagina de cadastro
##    E que o usuario preenche o telefone como "123"
##    Entao o telefone deve exibir a mensagem "Número de telefone inválido."
##
##  Cenario: Erro ao tentar registrar sem preencher o campo nome
##    Dado que o usuario esta na pagina de cadastro
##    E que o usuario deixa o campo nome vazio
##    Entao o nome deve exibir a mensagem "O nome é obrigatório."
##
##  Cenario: Erro ao tentar registrar sem preencher o campo e-mail
##    Dado que o usuario esta na pagina de cadastro
##    E que o usuario deixa o campo e-mail vazio
##    Entao o email deve exibir a mensagem "O email é obrigatório."
##
##  Cenario: Erro ao tentar registrar sem preencher o campo telefone
##    Dado que o usuario esta na pagina de cadastro
##    E que o usuario deixa o campo telefone vazio
##    Entao o telefone deve exibir a mensagem "O telefone é obrigatório."
##
##  Cenario: Erro ao tentar registrar sem preencher o campo data de nascimento
##    Dado que o usuario esta na pagina de cadastro
##    E que o usuario deixa o campo data de nascimento vazio
##    Entao a data de nascimento deve exibir a mensagem "A data de nascimento é obrigatória."
##
##  Cenario: Erro ao tentar registrar sem preencher o campo senha
##    Dado que o usuario esta na pagina de cadastro
##    E que o usuario deixa o campo senha vazio
##    Entao a senha deve exibir a mensagem "A senha deve ter no mínimo 8 caracteres."
##
##  Cenario: Tentar registrar um usuário com senha curta
##    Dado que o usuario esta na pagina de cadastro
##    E que o usuário preenche a senha com "12345"
##    Entao a senha deve exibir a mensagem "A senha deve ter no mínimo 8 caracteres."
##
##  Cenario: Tentar registrar um usuário com senha sem letra maiúscula
##    Dado que o usuario esta na pagina de cadastro
##    E que o usuário preenche a senha com "senha123"
##    Entao a senha deve exibir a mensagem "A senha deve conter pelo menos 8 caracteres, incluindo uma letra maiúscula, uma letra minúscula, um número e um símbolo."
##
##  Cenario: Tentar registrar um usuário com senha sem símbolo
##    Dado que o usuario esta na pagina de cadastro
##    E que o usuário preenche a senha com "Senha123"
##    Entao a senha deve exibir a mensagem "A senha deve conter pelo menos 8 caracteres, incluindo uma letra maiúscula, uma letra minúscula, um número e um símbolo."
##
##  Cenario: Tentar registrar um usuário com uma data de nascimento inválida
##    Dado que o usuario esta na pagina de cadastro
##    E que o usuário preenche a data de nascimento com "32/13/2024"
##    Entao a data de nascimento deve exibir a mensagem "Data inválida."
##
##  Cenario: Tentar registrar um usuário com nome muito curto
##    Dado que o usuario esta na pagina de cadastro
##    E que o usuário preenche o nome com "Ana"
##    Entao o nome deve exibir a mensagem "O nome deve ter no mínimo 8 caracteres."
