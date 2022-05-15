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
    public class RoleGroupRoleConfiguration : IEntityTypeConfiguration<RoleGroupRole>
    {
        public void Configure(EntityTypeBuilder<RoleGroupRole> builder)
        {
            builder.ToTable("RoleGroupRoles");
            builder.HasKey(x => new { x.RoleGroupId, x.RoleId });
        }
    }
}