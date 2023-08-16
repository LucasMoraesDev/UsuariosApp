using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Infra.Data.Mappings;

namespace UsuariosApp.Infra.Data.Contexts
{
    //Classe para conexão com o banco de dados no EntityFramework
    public class DataContext : DbContext
    {
        //método para configurarmos o caminho do banco de dados
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDUsuariosApp;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        //método para adicionarmos cada classe de mapeamento criada no projeto
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new HistoricoAtividadeMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
        }

        //para cada entidade do banco de dados, vamos declarar um DbSet
        //este DbSet nos permitirá fazer operações no banco de dados
        //com cada entidade mapeada (CRUD)
        public DbSet<HistoricoAtividade> HistoricoAtividade { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
