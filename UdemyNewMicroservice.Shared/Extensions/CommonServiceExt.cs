using Microsoft.Extensions.DependencyInjection;

namespace UdemyNewMicroservice.Shared.Extensions
{
    public static class CommonServiceExt
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services,Type assambly)
        {
            //Type assambly ile mikroservislerin assambly'si alınabilir ve oradan ortak servisler eklenebilir
            services.AddHttpContextAccessor();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(assambly));//Bana bir class ver bu class hangi assambly 'de ise ben onu tariyacam
            return services;
        }
    }
}
