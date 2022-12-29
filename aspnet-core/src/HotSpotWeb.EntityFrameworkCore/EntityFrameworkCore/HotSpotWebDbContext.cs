using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using HotSpotWeb.Applications;
using HotSpotWeb.Authorization.Roles;
using HotSpotWeb.Authorization.Users;
using HotSpotWeb.MultiTenancy;

namespace HotSpotWeb.EntityFrameworkCore
{
    public class HotSpotWebDbContext : AbpZeroDbContext<Tenant, Role, User, HotSpotWebDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public virtual DbSet<Application> Applications { get; set; }
        public HotSpotWebDbContext(DbContextOptions<HotSpotWebDbContext> options)
            : base(options)
        {
        }
    }
}
