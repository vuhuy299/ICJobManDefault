/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ICSLib.Authen.Data.EF
{
    public class AuthenDbContextFactory : IDesignTimeDbContextFactory<AuthenDbContext>
    {
        public AuthenDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AuthenDbContext>();
            var dbType = configuration.GetConnectionString(ConstantHelper.AuthenDBType).ToLower();
            if (dbType.Equals(ConstantHelper.SQLServer.ToLower()))
            {
                var connectionString = configuration.GetConnectionString(ConstantHelper.AuthenSQLServer);
                optionsBuilder.UseSqlServer(connectionString);
            }
            else if (dbType.Equals(ConstantHelper.MySQL.ToLower()))
            {
                var connectionString = configuration.GetConnectionString(ConstantHelper.AuthenMySQL);
                optionsBuilder.UseMySql(connectionString,
                    ServerVersion.AutoDetect(connectionString),
                    mySqlOptions => mySqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null
                    )
                );
            }


            return new AuthenDbContext(optionsBuilder.Options);
        }
    }
}
