using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Criando classe Contatos para o EF entender e criar a tabela de forma automática
// Essa classe vai virar uma tabela de um banco de dados

namespace MODULOAPI.Entities
{
    public class Contato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
    }
}