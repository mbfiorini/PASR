using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace PASR.EntityFrameworkCore
{
    public static class PASRDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<PASRDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<PASRDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
