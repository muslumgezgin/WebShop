using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace WepShop.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationRegistiration(this IServiceCollection serviceCollection)
        {
            var asmb = Assembly.GetExecutingAssembly();
            
        }
    }
}