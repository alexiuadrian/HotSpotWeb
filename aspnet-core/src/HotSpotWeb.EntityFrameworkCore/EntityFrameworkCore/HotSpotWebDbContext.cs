﻿using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using HotSpotWeb.Applications;
using HotSpotWeb.Authorization.Roles;
using HotSpotWeb.Authorization.Users;
using HotSpotWeb.Dependencies;
using HotSpotWeb.GithubProfiles;
using HotSpotWeb.MultiTenancy;
using HotSpotWeb.GithubRepositories;

namespace HotSpotWeb.EntityFrameworkCore
{
    public class HotSpotWebDbContext : AbpZeroDbContext<Tenant, Role, User, HotSpotWebDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Application> Applications { get; set; }
        public DbSet<Dependency> Dependencies { get; set; }
        public DbSet<Configurations.Configuration> Configurations { get; set; }
        public DbSet<GithubProfile> GithubProfiles { get; set; }
        public DbSet<GithubRepository> GithubRepositories { get; set; }
        public HotSpotWebDbContext(DbContextOptions<HotSpotWebDbContext> options)
            : base(options)
        {
        }
    }
}
