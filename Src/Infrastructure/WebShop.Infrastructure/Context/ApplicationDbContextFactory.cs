using Microsoft.EntityFrameworkCore;

namespace WebShop.Infrastructure.Context
{
    public class ApplicationDbContextFactory : DesignTimeDbContextFactoryBase<ApplicationDBContext>
    {
        protected override ApplicationDBContext CreateNewInstance(DbContextOptions<ApplicationDBContext> options)
        {
            return new ApplicationDBContext(options);
        }
    }
}