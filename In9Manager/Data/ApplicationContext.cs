using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using In9Manager.Models;
using System.Reflection.Metadata;

namespace In9Manager.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext (DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; } = default!;
        public DbSet<ClienteEndereco> ClienteEnderecos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Prestador> Prestadores { get; set; }
        public DbSet<PrestadorEndereco> PrestadorEnderecos { get; set; }
        public DbSet<Orcamento> Orcamentos { get; set; }
        public DbSet<OrcamentoServico> OrcamentoServicos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasIndex(x => x.CPF);

            modelBuilder.Entity<Cliente>()
                .HasAlternateKey(x => x.CPF)
                .HasName("CPF_Cliente");

            modelBuilder.Entity<Usuario>()
                .HasAlternateKey(x => x.Login)
                .HasName("Login");

            modelBuilder.Entity<Prestador>()
                .HasAlternateKey(x => x.CPF)
                .HasName("CPF_Prestador");

            modelBuilder.Entity<Veiculo>()
                .HasAlternateKey(x => x.Placa)
                .HasName("Placa");
        }
    }
}
