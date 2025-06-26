using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceExtensionCollection
    {
        public static void AddApplication(this IServiceCollection services) {

            services.AddScoped<IDokumentPrzyjeciaService, DokumentPrzyjeciaService>();
            services.AddScoped<IKontrahentService, KontrahentService>();
            services.AddScoped<IPozycjaDokumentuService, PozycjaDokumentuService>();
            services.AddScoped<ITowarServices, TowarServices>();

        }
    }
}
