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
    public class UserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<Int32>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserToken<Int32>> builder)
        {
            builder.ToTable("UserTokens");
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.Name).IsUnicode().HasMaxLength(250);
            builder.Property(x => x.Value).IsUnicode().HasMaxLength(250);
        }
    }
}
