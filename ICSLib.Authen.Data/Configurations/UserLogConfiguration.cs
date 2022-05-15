/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ICSLib.Authen.Data.Entities;
using Microsoft.Extensions.Configuration;
using System.IO;
using ICSLib.Utilities.Helpers;

namespace ICSLib.Authen.Data.Configurations
{
    public class UserLogConfiguration : IEntityTypeConfiguration<UserLog>
    {
        public void Configure(EntityTypeBuilder<UserLog> builder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var dbType = configuration.GetConnectionString(ConstantHelper.AuthenDBType).ToLower();

            builder.ToTable("UserLogs");
            builder.HasKey(x => x.UserLogId);
            builder.Property(x => x.UserId).IsRequired(false);
            builder.Property(x => x.UserName).IsRequired(false).IsUnicode(false).HasMaxLength(150);
            builder.Property(x => x.UserFullName).IsRequired(false).IsUnicode(false).HasMaxLength(150);
            builder.Property(x => x.IPAddress).IsRequired(false).IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.ActionCode).IsRequired(false).IsUnicode(false).HasMaxLength(150);
            builder.Property(x => x.ActionDesc).IsRequired(false).IsUnicode(true).HasMaxLength(250);
            if (dbType.Equals(ConstantHelper.SQLServer.ToLower()))
            {
                builder.Property(x => x.OldeData).IsRequired(false).HasColumnType("nvarchar(max)");
                builder.Property(x => x.NewData).IsRequired(false).HasColumnType("nvarchar(max)");
            }
            else if (dbType.Equals(ConstantHelper.MySQL.ToLower()))
            {
                builder.Property(x => x.OldeData).IsRequired(false).HasColumnType("text");
                builder.Property(x => x.NewData).IsRequired(false).HasColumnType("text");
            }
        }
    }
}
