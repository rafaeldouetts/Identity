using Identity.API.Controllers;
using Identity.Blob;
using Identity.Domain.Services;
using Identity.Infra.Repositories.Context;
using Identity.webapi.Extenssions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Definindo o environment a partir de uma variável de ambiente ou parâmetro customizado
var environment = builder.Environment.EnvironmentName;

// Configurando o appsettings específico com base no environment
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddHttpClient<AccountController>();

// Configuração do CORS para permitir múltiplas origens
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
              //.AllowCredentials();  // Permite o envio de credenciais (tokens, cookies)
    });
});

// Adiciona os serviços da API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configuração do Swagger
builder.Services.AddSwaggerGen(options =>
{
    // Adicionar o esquema de segurança para JWT no Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = $"API executando no ambiente: {environment}"
    });

    // Adicionar o requisito de segurança global para JWT
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// Configura a conexão com o Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = builder.Configuration.GetConnectionString("RedisConnection");
    return ConnectionMultiplexer.Connect(configuration);
});

var teste = builder.Configuration.GetConnectionString("DefaultConnection");

// Configuração do banco de dados (SQL Server)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);


var configurationRoot = builder.Configuration as IConfigurationRoot;
builder.Services.AddSingleton(configurationRoot);

// Configuração dos serviços (Blob, Redis, Email, etc.)
builder.Services.AddDIAuthentication(builder);

builder.Services.AddTransient<IBlobService, BlobService>();
builder.Services.AddTransient<IRedisService, RedisService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IWhatsAppService, WhatsAppService>();
builder.Services.AddTransient<ITwoFactorAuthService, TwoFactorAuthService>();


// Configuração de autenticação com JWT
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.RequireHttpsMetadata = false;  // Permitindo uso sem HTTPS para desenvolvimento local
//        options.SaveToken = true;
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = builder.Configuration["Jwt:Issuer"],  // Backend issuer (valor do "Issuer" no JWT)
//            ValidAudience = builder.Configuration["Jwt:Audience"],  // Audience do JWT
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))  // Chave secreta para assinatura
//        };

// Adicionando eventos de diagnóstico para JWT
//options.Events = new JwtBearerEvents
//{
//    OnAuthenticationFailed = context =>
//    {
//        Console.WriteLine($"Falha na autenticação: {context.Exception.Message}");
//        return Task.CompletedTask;
//    },
//    OnTokenValidated = context =>
//    {
//        Console.WriteLine("Token validado com sucesso.");
//        return Task.CompletedTask;
//    },
//    OnChallenge = context =>
//    {
//        Console.WriteLine($"Falha ao autenticar: {context.ErrorDescription}");
//        return Task.CompletedTask;
//    }
//};
//});

//builder.Services.Configure<CookieAuthenticationOptions>(options =>
//{
//    options.LoginPath = "/Account/Login";  // Caminho de login
//    options.LogoutPath = "/Account/Logout"; // Caminho de logout
//    options.AccessDeniedPath = "/AccessDenied";  // Caminho para acesso negado
//});


builder.Logging.AddConsole(); // Para logs no console

var app = builder.Build();

// Middleware para exibir o token recebido
app.Use(async (context, next) =>
{
    var token = context.Request.Headers["Authorization"].ToString();

    if (string.IsNullOrEmpty(token))
    {
        Console.WriteLine("Token não fornecido.");
        await next();
        return;
    }

    // Remover o prefixo 'Bearer' se presente
    if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
    {
        token = token.Substring(7);
    }

    // Verificar a validade do token
    try
    {
        // Criar o validador do token JWT
        var tokenHandler = new JwtSecurityTokenHandler();
        var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        if (jsonToken == null)
        {
            Console.WriteLine("Token JWT inválido.");
        }
        else
        {
            Console.WriteLine($"Token JWT válido: {jsonToken.Header.Alg}");
            // Se desejar, pode acessar o payload do token: jsonToken.Payload
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao validar o token: {ex.Message}");
    }

    await next();
});

// Configuração do CORS
app.UseCors();

// Aplica as migrations automaticamente no banco de dados
app.ApplyMigrations<ApplicationDbContext>();

// Configuração do pipeline de requisição HTTP
//if (!app.Environment.IsProduction())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

// Autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

// Mapeia os controladores
app.MapControllers();

app.Run();
