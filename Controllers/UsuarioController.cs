using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// Controller é o ponto de entrada principal da API
//É uma classe que agrupa requisições HTTP e DISPONIBILIZA OS ENDPOINTS
//Exemplo classe WatherForeCastController = Tudo que for relacionado a previsão do tempo Será agrupado nesta controller
// A Parte Controller é ignorada. Ela serve apenas para identificar o padrão
// Podemos acessar a API Manualmente sem entrar no Swagger através do http://localhost:5068/Usuario/ObterDataHoraAtual

namespace ModuloAPI.Controllers
{
    // 2 atributos: Route rota controller
    [ApiController] // ApiController para identificar que ela de fato é uma api
    [Route("[controller]")] // Route rota controller
    public class UsuarioController : ControllerBase // Herda de ControllerBase
    {
        // HTTPGET ObterDataHoraAtual é como podemos nomear o método nas sessões da Api
        // O Caminho na URL será 1º a Controller Usuario e depois o Método ObterDataHoraAtual http://localhost:5068/Usuario/ObterDataHoraAtual
        // Quando chamamos o endereço ele vai reconhecer que chame o método abaixo e que por sua vez vai retonar a data e a hora atual
        [HttpGet("ObterDataHoraAtual")]

        // Método que retorna a data e a hora atual
        public IActionResult ObterDataHora()
        {
            // CRIANDO UM OBJETO ANONIMO RETORNANDO A DATA E DEPOIS MAIS UMA PROPRIEDADE HORA PARA RETORNAR A DATA E A HORA
            var obj = new
            {
                Data = DateTime.Now.ToLongDateString(),
                Hora = DateTime.Now.ToShortDateString()
            };

            // Retornando a requisição HTTP NO CASO O OBJETO. OK É UM MÉTODO E O OBJ É O OBJETO
            return Ok(obj);
        }
        // Criado mais um método "Apresentar" e que recebeu um parámetro chamado nome
        [HttpGet("Apresentar/{nome}")] // Tudo que for passado aqui entre chaves será repassado para o string nome
        public IActionResult Apresentar(string nome) // 
        {
            var mensagem = $"Olá {nome}, seja bem-vindo!"; // Dando tudo certo irá mostrar o nome passado
            return Ok(new { mensagem }); // Aqui retorna a mensagem para o usuário
        }

    }
}