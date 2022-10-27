
using Qick.Repositories;
using Qick.Repositories.Interfaces;
using Scrutor;

namespace Qick.Configuration
{
    public static class RepositoryStartup
    {
        
            public static IServiceCollection AddRepository(this IServiceCollection services)
            {
                services.Scan(scan => scan
                .FromAssembliesOf(typeof(RepositoriesInterfacesAssemblyHelper), typeof(RepositoriesClassesAssemblyHelper))
                .AddClasses(classes => classes.InNamespaces(RepositoriesClassesAssemblyHelper.Namespace))
                .UsingRegistrationStrategy(RegistrationStrategy.Replace(ReplacementBehavior.ServiceType))
                .AsMatchingInterface()
                .WithScopedLifetime()
                );
                return services;
            }
        
    }
}
