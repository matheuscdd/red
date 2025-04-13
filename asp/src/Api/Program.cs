using Api.Services;
using Api.Middlewares;
using Domain.Services;
using IoC.Swagger;
using IoC.Identity;
using IoC.Jwt;
using IoC.Mapster;
using IoC.MediatR;
using IoC.Exceptions;
using IoC.Controllers;
using IoC.Repositories;
using IoC.Database;
using IoC.IP;


var builder = WebApplication.CreateBuilder(args);

// carrega variáveis de ambiente
var sqlServerUrl = Environment.GetEnvironmentVariable("MSSQL_URL") ?? throw new Exception("MSSQL_URL cannot be empty");
var geoIpPath = Environment.GetEnvironmentVariable("GEO_IP_PATH") ?? throw new Exception("GEO_IP_PATH cannot be empty");
var host = Environment.GetEnvironmentVariable("HOST") ?? throw new Exception("HOST cannot be empty");
var secretKey = Environment.GetEnvironmentVariable("SECRET_KEY") ?? throw new Exception("SECRET_KEY cannot be empty");
var deployUrl = Environment.GetEnvironmentVariable("DEPLOY_URL") ?? "http://localhost";

builder.Configuration["JWT:Issuer"] = host;
builder.Configuration["JWT:Audience"] = host;
builder.Configuration["JWT:SigningKey"] = secretKey;
builder.Configuration["ConnectionStrings:DefaultConnection"] = sqlServerUrl;


builder
    .AddIPConf()
    .AddExceptionsConf() // Personaliza as exceções
    .AddDatabaseConf() // adiciona as configurações de conexão com o banco de dados
    .AddControllersConf() // Captura os controllers
    .AddMediatRConf() // Carrega o driver de acesso ao banco de dados com as models
    .AddIdentityConf() // Configuração de senha do identity
    .AddJwtConf() // Configuração do JWT
    .AddMapsterConf() // Adicionar o mapters que já configura os métodos de mapping Id = Id
    .AddSwaggerConf() // Configuração para colocar no swagger
    .AddRepositoriesConf() // Adiciona a injeção de dependências nos repositórios
;

builder.Services.AddScoped<ITokenService, TokenService>(); // Estabelece as configurações para geração do token
builder.Services.AddSingleton<IGeoIpService>(provider =>
{
    return new GeoIpService(geoIpPath);
});

// if (builder.Environment.EnvironmentName.Equals("Production"))
// {
//     builder.AddSigNozConf(); // Configuração signoz
// }

var app = builder.Build();

app.UseForwardedHeaders();
app.UseAuthentication();
app.UseAuthorization();

app
    .AddExceptionsConf() // personaliza as exceções
    .AddSwaggerConf(deployUrl) // configurações da documentação
    .AddControllersConf() // Mapeia os controllers para serem acessíveis via rota
    .UseMiddleware<TokenValidationMiddleware>() // Registra os middlewares
;


app.Run();
// Necessário para testes
public partial class Program { }
