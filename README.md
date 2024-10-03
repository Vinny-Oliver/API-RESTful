# Tutorial: Criando uma Aplicação ASP.NET Core MVC para Gerenciamento de Tarefas

### 1. Criar a Pasta do Projeto

  1.1. **Crie uma nova pasta na área de trabalho**.

      • **Vá até sua área de trabalho e clique com o botão direito do mouse**.

      • **Selecione "Novo" > "Pasta" e nomeie a pasta como TrilhaApiDesafio**.

### 2. Abrir o Visual Studio Code

2.1. **Abra o Visual Studio Code:**.

   • **No VS Code, vá até o menu superior e clique em File (Arquivo).**.

   • **Selecione Open Folder... (Abrir Pasta...) e navegue até a pasta TrilhaApiDesafio que você acabou de criar. Clique em Select Folder (Selecionar Pasta).**

### 3. Criar o Projeto:
   
3.1. **Criar um novo projeto ASP.NET Core Web API**
      
   • **Abra o terminal integrado no VS Code (Terminal > New Terminal)**.

   • **Execute o seguinte comando:**.


   ```bash
   dotnet new webapi -n TrilhaApiDesafio
   ```

   - `webapi`: especifica que estamos criando um projeto de API web.
   - `-n TrilhaApiDesafio`: define o nome do projeto como "TrilhaApiDesafio".

### 4. **Navegue até o diretório do projeto**:

   ```bash
   cd TrilhaApiDesafio
   ```

### 5. Adicionar as Dependências

5.1. **Adicione o Entity Framework Core e o provedor SQL Server** executando os seguintes comandos:

   ```bash
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   dotnet add package Microsoft.EntityFrameworkCore.Tools
   ```

   - Isso permitirá que você utilize o Entity Framework para interagir com um banco de dados SQL Server.

### 6. Criar o Model

6.1. **Crie a classe `Tarefa`**:
   - Navegue até a pasta `Models` e crie um arquivo chamado `Tarefa.cs` com o seguinte conteúdo:

   ```csharp
   using System;

   namespace TrilhaApiDesafio.Models
   {
       public class Tarefa
       {
           public int Id { get; set; }
           public string Titulo { get; set; }
           public string Descricao { get; set; }
           public DateTime Data { get; set; }
           public EnumStatusTarefa Status { get; set; }
       }
   }
   ```

6.2. **Crie a enumeração `EnumStatusTarefa`**:
   - Dentro da pasta `Models`, crie um arquivo chamado `EnumStatusTarefa.cs` com o seguinte conteúdo:

   ```csharp
   namespace TrilhaApiDesafio.Models
   {
       public enum EnumStatusTarefa
       {
           Pendente,
           Finalizado
       }
   }
   ```


### 8. Criar o Contexto

8.1. **Crie o contexto `OrganizadorContext`**:
   - Navegue até a pasta `Context` e crie um arquivo chamado `OrganizadorContext.cs` com o seguinte conteúdo:

   ```csharp
   using Microsoft.EntityFrameworkCore;
   using TrilhaApiDesafio.Models;

   namespace TrilhaApiDesafio.Context
   {
       public class OrganizadorContext : DbContext
       {
           public OrganizadorContext(DbContextOptions<OrganizadorContext> options) : base(options)
           {
           }

           public DbSet<Tarefa> Tarefas { get; set; }
       }
   }
   ```

### 9. **Configure a conexão com o banco de dados**:
   - Abra o arquivo `appsettings.Development.json` e adicione a seguinte configuração de conexão:

   ```json
   {
       "ConnectionStrings": {
           "ConexaoPadrao": "Server=SEU_SERVIDOR;Database=SEU_BANCO;User Id=SEU_USUARIO;Password=SUASENHA;"
       },
       "Logging": {
           "LogLevel": {
               "Default": "Information",
               "Microsoft.AspNetCore": "Warning"
           }
       },
       "AllowedHosts": "*"
   }
   ```

   - Substitua `SEU_SERVIDOR`, `SEU_BANCO`, `SEU_USUARIO` e `SUASENHA` pelas informações do seu banco de dados.

### 10. Criar o Controller

10.1. **Crie o `TarefaController`**:
   - Navegue até a pasta `Controllers` e crie um arquivo chamado `TarefaController.cs` com o seguinte conteúdo:

   ```csharp
   using Microsoft.AspNetCore.Mvc;
   using TrilhaApiDesafio.Context;
   using TrilhaApiDesafio.Models;
   using System.Linq;

   namespace TrilhaApiDesafio.Controllers
   {
       [ApiController]
       [Route("[controller]")]
       public class TarefaController : ControllerBase
       {
           private readonly OrganizadorContext _context;

           public TarefaController(OrganizadorContext context)
           {
               _context = context;
           }

           [HttpGet("{id}")]
           public IActionResult ObterPorId(int id)
           {
               var tarefa = _context.Tarefas.Find(id);
               if (tarefa == null) return NotFound();

               return Ok(tarefa);
           }

           [HttpGet("ObterTodos")]
           public IActionResult ObterTodos()
           {
               var tarefas = _context.Tarefas.ToList();
               return Ok(tarefas);
           }

           [HttpPost]
           public IActionResult Criar(Tarefa tarefa)
           {
               _context.Tarefas.Add(tarefa);
               _context.SaveChanges();

               return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
           }
       }
   }
   ```

### 11. Configurar o `Program.cs`

11.1. **Atualize o `Program.cs`** com as configurações necessárias:

    ```csharp
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using TrilhaApiDesafio.Context;
    using Microsoft.EntityFrameworkCore;

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<OrganizadorContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));
    builder.Services.AddControllers();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
    ```

### 12. Criar as Migrações

12.1. **Abra o terminal** novamente e execute os seguintes comandos para criar e aplicar as migrações:

    ```bash
    dotnet ef migrations add CriacaoTabelaTarefas
    dotnet ef database update
    ```

   - O primeiro comando cria uma nova migração com base nas mudanças feitas nos modelos.
   - O segundo comando aplica as migrações ao banco de dados, criando as tabelas necessárias.

### 13. Abrir o SQL Server

13.1. **Abra o SQL Server Management Studio** (ou a ferramenta de gerenciamento que você utiliza) e conecte-se ao banco de dados. Verifique se a tabela `Tarefas` foi criada corretamente.


### 14. Criar as Views

14.1. **Crie o arquivo `index.html`**:
   - Navegue até a pasta `Views` e crie um arquivo chamado `index.html` com o seguinte conteúdo:

   ```html
   <!DOCTYPE html>
   <html lang="pt-BR">
   <head>
       <meta charset="UTF-8">
       <meta name="viewport" content="width=device-width, initial-scale=1.0">
       <title>Adicionar Tarefa</title>
       <link rel="stylesheet" href="styles.css"> <!-- Link para o CSS -->
   </head>
   <body>
       <div class="container">
           <h1>Adicionar Nova Tarefa</h1>
           <form id="tarefaForm">
               <label for="titulo">Título:</label>
               <input type="text" id="titulo" name="titulo" required><br>

               <label for="descricao">Descrição:</label>
               <textarea id="descricao" name="descricao" required></textarea><br>

               <label for="data">Data:</label>
               <input type="datetime-local" id="data" name="data" required><br>

               <label for="status">Status:</label>
               <select id="status" name="status" required>
                   <option value="0">Pendente</option>
                   <option value="1">Finalizado</option>
               </select><br>

               <button type="submit">Adicionar Tarefa</button>
           </form>
       </div>

       <script src="script.js"></script> <!-- Link para o JavaScript -->
   </body>
   </html>
   ```

14.2. **Crie o arquivo `script.js`**:
   - Na mesma pasta, crie um arquivo chamado `script.js` com o seguinte conteúdo:

   ```javascript
   document.getElementById("tarefaForm").addEventListener("submit", async function(event) {
       event.preventDefault(); // Impede o envio do formulário padrão

       const titulo = document.getElementById("titulo").value;
       const descricao = document.getElementById("descricao").value;
       const data = new Date(document.getElementById("data").value).toISOString();
       const status = parseInt(document.getElementById("status").value);

       const response = await fetch("http://localhost:5068/Tarefa", {
           method: "POST",
           headers: {
               "Content-Type": "application/json"
           },
           body: JSON.stringify({
               Titulo: titulo,
               Descricao: descricao,
               Data: data,
               Status: status
           })
       });

       if (response.ok) {
           const result = await response.json();
           alert(`Tarefa adicionada com sucesso! ID: ${result.id}`);
       } else {
           const error = await response.json();
           alert(`Erro:

 ${error}`);
       }
   });
   ```

14.3. **Crie o arquivo `styles.css`** para estilizar a página:
   - Na mesma pasta, crie um arquivo chamado `styles.css` com o seguinte conteúdo:

   ```css
   body {
       font-family: Arial, sans-serif;
       margin: 0;
       padding: 0;
       background-color: #f4f4f4;
   }

   .container {
       width: 50%;
       margin: auto;
       background: #fff;
       padding: 20px;
       border-radius: 5px;
       box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
   }

   h1 {
       text-align: center;
   }

   label {
       display: block;
       margin: 10px 0 5px;
   }

   input, textarea, select {
       width: 100%;
       padding: 8px;
       margin-bottom: 10px;
       border: 1px solid #ccc;
       border-radius: 4px;
   }

   button {
       width: 100%;
       padding: 10px;
       background-color: #5cb85c;
       color: white;
       border: none;
       border-radius: 4px;
       cursor: pointer;
   }

   button:hover {
       background-color: #4cae4c;
   }
   ```

### 15. Executar a Aplicação

  • **No terminal do VS Code, execute o comando:**
  
 ```bash
    dotnet watch run
   ```

  • **Isso iniciará a aplicação e você poderá acessar a API através do Swagger em http://localhost:5000/swagger.**.

  ### 16. Interagir com o CRUD

  • **Use o Swagger para testar as operações CRUD.**

  • **Você também pode abrir o index.html diretamente em um navegador para agendar tarefas usando a interface.**

  


### Conclusão

Agora você possui uma aplicação ASP.NET Core MVC funcional para gerenciar tarefas! Você pode:

- Adicionar novas tarefas usando o formulário na página `index.html`.
- Acessar as APIs para obter tarefas ou adicionar novas diretamente.

### Dicas Finais

- **Testes**: Utilize o Swagger para testar suas APIs (acessível geralmente em `http://localhost:5068/swagger`).
- **Aprimoramento**: Considere adicionar autenticação e autorização para proteger suas rotas de API.

Sinta-se à vontade para modificar o código conforme necessário e personalizar a aplicação para atender às suas necessidades. Se tiver dúvidas ou precisar de mais assistência, é só avisar!
