using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebShop.Infrastructure.Context;

namespace WebShop.Infrastructure
{
    public static class ServiceRegistration
    {

        public static void AddPersistenceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(opt =>
            {
                opt.UseInMemoryDatabase("webshop")
                    .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));   
                    //Logging sql
                    //options.EnableSensitiveDataLogging
            });
            
        }
    }
}