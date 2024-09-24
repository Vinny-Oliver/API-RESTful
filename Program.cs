using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MODULOAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner

// Estamos falando para adicionar um db context da Agenda Context e passando algumas opções. Falamos assim: Agenda Context use UseSqlServer
// Builder.Configuration estou pegando a configuração do appsettings e o GetConnectionString ele pega alguma chave da ConnectionString e o nome da minha conexão chamada ConexaoPadrao
// Indo em appsetting veremos que está acessando a chave ConnectionStrings e dentro da chave acessando o valor ConexaoPadrao
builder.Services.AddDbContext<AgendaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

builder.Services.AddControllers();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar o pipeline de solicitações HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    // Configure o Swagger UI para estar disponível em duas rotas
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ModuloAPI v1");
        c.RoutePrefix = string.Empty; // Acesso na raiz
    });

    // Adicionar uma segunda instância do Swagger UI em outra rota
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ModuloAPI v1");
        c.RoutePrefix = "swagger"; // Acesso em /swagger
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers(); // Mapeia os controladores

app.Run();
