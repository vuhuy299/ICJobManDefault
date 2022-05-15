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
    public class UserRoleGroupConfiguration : IEntityTypeConfiguration<UserRoleGroup>
    {
        public void Configure(EntityTypeBuilder<UserRoleGroup> builder)
        {
            builder.ToTable("UserRoleGoups");
            builder.HasKey(x => new { x.UserId, x.RoleGoupId });
        }
    }
}