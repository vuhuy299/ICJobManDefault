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
    public class RoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<Int32>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<Int32>> builder)
        {
            builder.ToTable("RoleClaims");
            builder.Property(x => x.ClaimType).IsUnicode().HasMaxLength(250);
            builder.Property(x => x.ClaimValue).IsUnicode().HasMaxLength(250);
        }
    }
}
