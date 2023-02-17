using SampleBank.Presentation;

namespace SampleBank.Web
{
    public static class RegisterServicesExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection builder)
        {
            builder.AddScoped<BankingService>();

            return builder;
        }
    }
}
