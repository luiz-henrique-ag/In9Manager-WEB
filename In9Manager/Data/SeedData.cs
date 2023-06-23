using In9Manager.Helpers.HashGenerator;
using In9Manager.Migrations;
using Microsoft.EntityFrameworkCore;

namespace In9Manager.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationContext>>()))
            {
                if (context.Usuarios.Any())
                {
                    return;   // DB has been seeded
                }

                var usuario = new Models.Usuario
                {
                    Cpf = "000.000.000-00",
                    Login = "admin",
                    Permissao = 1,
                    Nome = "admin",
                    Senha = "admin".GenerateHash()
                };

                context.Add(usuario);
                context.SaveChanges();
            }
        }
    }
}

