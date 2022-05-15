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
    public class RoleGroupConfiguration : IEntityTypeConfiguration<RoleGroup>
    {
        public void Configure(EntityTypeBuilder<RoleGroup> builder)
        {
            builder.ToTable("RoleGroups");
            builder.HasKey(x => x.RoleGroupId);
            builder.Property(x => x.RoleGroupName).IsRequired(false).IsUnicode().HasMaxLength(150);
            builder.Property(x => x.RoleGroupDesc).IsRequired(false).IsUnicode().HasMaxLength(150);
        }
    }
}
