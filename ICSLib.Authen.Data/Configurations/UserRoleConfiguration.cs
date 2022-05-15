/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ICSLib.Authen.Data.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Int32>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<Int32>> builder)
        {
            builder.ToTable("UserRoles");
            builder.HasKey(x => new { x.UserId, x.RoleId });
        }
    }
}
