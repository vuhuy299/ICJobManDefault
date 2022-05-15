/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using ICSLib.Authen.Data.Entities;
using ICSLib.Authen.Data.Extensions;

namespace ICSLib.Authen.Data.EF
{
    public class AuthenDbContext : IdentityDbContext<User, Role, Int32>
    {
        public AuthenDbContext(DbContextOptions<AuthenDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure using Fluent API
            modelBuilder.ApplyConfiguration(new UserStatusConfiguration());
            modelBuilder.ApplyConfiguration(new GenderConfiguration());
            modelBuilder.ApplyConfiguration(new UserLogConfiguration());

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            modelBuilder.ApplyConfiguration(new UserClaimConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserLoginConfiguration());
            modelBuilder.ApplyConfiguration(new RoleClaimConfiguration());
            modelBuilder.ApplyConfiguration(new UserTokenConfiguration());

            modelBuilder.ApplyConfiguration(new RoleGroupConfiguration());
            modelBuilder.ApplyConfiguration(new RoleGroupRoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleGroupConfiguration());

            //Data seeding
            modelBuilder.Seed();
        }

        public DbSet<UserStatus> UserStatuses { set; get; }
        public DbSet<Gender> Genders { set; get; }
        public DbSet<UserLog> UserLogs { set; get; }
        public DbSet<RoleGroup> RoleGroups { set; get; }
        public DbSet<RoleGroupRole> RoleGroupRoles { set; get; }
        public DbSet<UserRoleGroup> UserRoleGroups { set; get; }
    }
}
