using Adv.BusinessLogic.Interfaces;
using Adv.BusinessLogic.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Adv.Web.Extensions
{
    public static class AddAppReposExtension
    {
        public static IServiceCollection AddAppRepositories(this IServiceCollection services)
        {
            services.AddScoped<IIterationRepository, IterationRepository>();
            services.AddScoped<IWorkItemRepository, WorkItemRepository>();

            return services;
        }        
    }
}