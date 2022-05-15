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
    public class UserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<Int32>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<Int32>> builder)
        {
            builder.ToTable("UserLogins");
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.LoginProvider).IsUnicode().HasMaxLength(250);
            builder.Property(x => x.ProviderKey).IsUnicode().HasMaxLength(250);
            builder.Property(x => x.ProviderDisplayName).IsUnicode().HasMaxLength(250);
        }
    }
}
