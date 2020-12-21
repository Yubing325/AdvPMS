using Adv.BusinessLogic.Services;
using Adv.Data.Interfaces;
using Adv.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Adv.Web.Extensions
{
    public static class AddAppReposExtension
    {
        public static IServiceCollection AddAppRepositories(this IServiceCollection services)
        {
            services.AddScoped<IIterationRepository, IterationRepository>();
            services.AddScoped<IWorkItemRepository, WorkItemRepository>();
            services.AddScoped<WorkItemService>();

            return services;
        }        
    }
}