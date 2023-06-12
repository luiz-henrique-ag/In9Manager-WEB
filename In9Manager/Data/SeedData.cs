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
                if (context.Cliente.Any())
                {
                    return;   // DB has been seeded
                }
                context.Cliente.AddRange(
                    
                );
                context.SaveChanges();
            }
        }
    }
}

