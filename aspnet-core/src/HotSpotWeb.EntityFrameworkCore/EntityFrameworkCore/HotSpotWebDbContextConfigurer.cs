using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace HotSpotWeb.EntityFrameworkCore
{
    public static class HotSpotWebDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<HotSpotWebDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<HotSpotWebDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
