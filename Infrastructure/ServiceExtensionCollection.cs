using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceExtensionCollection
    {
        public static void AddInfrastructure(this IServiceCollection services) {
            services.AddDbContext<WarehouseContext>(options =>
            options.UseSqlite("Data Source=bazadanych.db"));

            services.AddScoped<DatabaseSeeder>();
        }
    }
}
