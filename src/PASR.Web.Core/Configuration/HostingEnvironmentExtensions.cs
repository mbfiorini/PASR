using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace PASR.Configuration
{
    public static class HostingEnvironmentExtensions
    {
        //Busca o arquivo appSettings.json no root directory e armazzena em cache no _configurationCache da classe estática AppConfigurations
        public static IConfigurationRoot GetAppConfiguration(this IWebHostEnvironment env)
        {
            return AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment());
        }
    }
}
