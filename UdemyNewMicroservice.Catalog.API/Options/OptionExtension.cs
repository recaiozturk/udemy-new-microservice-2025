namespace UdemyNewMicroservice.Catalog.API.Options
{
    public static class OptionExtension
    {
        public static IServiceCollection AddOptionsExtension(this IServiceCollection services)
        {
            services.AddOptions<MongoOption>().BindConfiguration(nameof(MongoOption)).ValidateDataAnnotations().ValidateOnStart();

            return services;
        }
    }
}
