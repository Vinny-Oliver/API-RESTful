using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MODULOAPI.Entities;

// Contexto é uma classe que centraliza todas as informações em um determinado banco de dados

namespace MODULOAPI.Context
{
    public class AgendaContext : DbContext // Aqui está herdando as operações que vamos utilizar para trabalhar com Banco de Dados que vem do pacote que instalamos Microsoft.EntityFrameworkCore
    {
        // Criando um construtor especial para mais pra frente passar a conexão do meu banco de dados
        // (DbContextOptions<AgendaContext> option) recebe uma configuração para conexão com o banco. Vai passar para a classe pai base(DbContext), ou seja para o DbContext que gerencia a comunicação com o BD
        // O Context faz a ligação com o banco de dados
        public AgendaContext(DbContextOptions<AgendaContext> option) : base(option)
        { 
            // Aqui recebe a configuração do banco de dados com a DbContext
        }

        // Representa uma tabela através do DbSet
        // Propriedade que refere a entidade que é Contato.cs
        // Entidade significa que é uma classe no meu programa e ao mesmo tempo é uma tabela do meu Sql do BD
        // Essa classe contato é a mesma Contato.cs em Entities
        // Está dentro de um Dbset pois está sendo representado por uma classe em um nosso programa e também será representado por uma tabela no nosso banco de dados
        public DbSet<Contato> Contatos { get; set; }

    }
}