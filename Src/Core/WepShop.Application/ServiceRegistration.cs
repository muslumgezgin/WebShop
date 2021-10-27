using System.Collections.Generic;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using WepShop.Application.Behaviours;

namespace WepShop.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationRegistration(this IServiceCollection services)
        {
            var assmb = Assembly.GetExecutingAssembly();

            services.AddMediatR(assmb);
            services.AddAutoMapper(assmb);

            var assemblyToRegister = new List<Assembly>() { assmb };

            AssemblyScanner.FindValidatorsInAssemblies(assemblyToRegister).ForEach(pair =>
            {
                services.Add(ServiceDescriptor.Transient(pair.InterfaceType,pair.ValidatorType));
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        }
    }
}