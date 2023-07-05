using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Interop.BLL
{
    public static class DependenciesBootstrapper
    {
        public static IServiceCollection AddInteropBll(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
