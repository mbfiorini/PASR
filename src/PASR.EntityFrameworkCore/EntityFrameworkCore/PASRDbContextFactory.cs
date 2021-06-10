using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PASR.Configuration;
using PASR.Web;

namespace PASR.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class PASRDbContextFactory : IDesignTimeDbContextFactory<PASRDbContext>
    {
        public PASRDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PASRDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(),"Development");

            PASRDbContextConfigurer.Configure(builder, configuration.GetConnectionString(PASRConsts.ConnectionStringName));

            return new PASRDbContext(builder.Options);
        }
    }
}
