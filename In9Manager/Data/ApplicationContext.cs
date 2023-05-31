using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using In9Manager.Models;

namespace In9Manager.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext (DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<In9Manager.Models.Cliente> Cliente { get; set; } = default!;
        public DbSet<ClienteEndereco> ClienteEndereco { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
