using Qick.Models;

namespace Qick.Configuration
{
    public static class MailStartup
    {
        public static IServiceCollection AddMail(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            return services;
        }
    }
}
