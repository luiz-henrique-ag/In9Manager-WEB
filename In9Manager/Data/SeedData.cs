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
                if (context.Clientes.Any())
                {
                    return;   // DB has been seeded
                }
                context.Clientes.AddRange(
                    
                );
                context.SaveChanges();
            }
        }
    }
}

