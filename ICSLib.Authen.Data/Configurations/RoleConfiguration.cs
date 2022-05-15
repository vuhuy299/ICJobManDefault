/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ICSLib.Authen.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.Property(x => x.Name).IsRequired(false).IsUnicode().HasMaxLength(250);
            builder.Property(x => x.NormalizedName).IsRequired(false).IsUnicode().HasMaxLength(250);
            builder.Property(x => x.ConcurrencyStamp).IsRequired(false).IsUnicode().HasMaxLength(250);

            builder.Property(x => x.Controler).IsRequired(false).IsUnicode().HasMaxLength(100);
            builder.Property(x => x.Action).IsRequired(false).IsUnicode().HasMaxLength(100);
            builder.Property(x => x.Icon).IsRequired(false).IsUnicode().HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired(false).IsUnicode().HasMaxLength(250);
        }
    }
}
