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

        public DbSet<Cliente> Cliente { get; set; } = default!;
        public DbSet<ClienteEndereco> ClienteEndereco { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasIndex(x => x.CPF);

            modelBuilder.Entity<Cliente>()
                .HasAlternateKey(x => x.CPF)
                .HasName("CPF");

            modelBuilder.Entity<Usuario>()
                .HasAlternateKey(x => x.Login)
                .HasName("Login");
        }
    }
}
