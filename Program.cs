using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrilhaApiDesafio.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração da conexão com o banco de dados
builder.Services.AddDbContext<OrganizadorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

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

// Usar CORS antes de qualquer outro middleware
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers(); // Mapeia os controladores

app.Run();
