/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ICSLib.Authen.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.Property(x => x.UserName).IsRequired(false).IsUnicode().HasMaxLength(250);
            builder.Property(x => x.NormalizedUserName).IsRequired(false).IsUnicode().HasMaxLength(250);
            builder.Property(x => x.Email).IsRequired(false).IsUnicode().HasMaxLength(250);
            builder.Property(x => x.NormalizedEmail).IsRequired(false).IsUnicode().HasMaxLength(250);
            builder.Property(x => x.PasswordHash).IsRequired(true).IsUnicode().HasMaxLength(250);
            builder.Property(x => x.SecurityStamp).IsRequired(false).IsUnicode().HasMaxLength(250);
            builder.Property(x => x.ConcurrencyStamp).IsRequired(false).IsUnicode().HasMaxLength(250);
            builder.Property(x => x.PhoneNumber).IsRequired(false).IsUnicode().HasMaxLength(50);

            builder.Property(x => x.LastName).IsRequired(false).IsUnicode().HasMaxLength(150);
            builder.Property(x => x.FullName).IsRequired(false).IsUnicode().HasMaxLength(250);
            builder.Property(x => x.Avatar).IsRequired(false).IsUnicode().HasMaxLength(150);
            builder.Property(x => x.Address).IsRequired(false).IsUnicode().HasMaxLength(250);
            builder.Property(x => x.Comments).IsRequired(false).IsUnicode().HasMaxLength(2000);
            builder.Property(x => x.OAuthId).IsRequired(false).IsUnicode().HasMaxLength(250);
            builder.Property(x => x.OAuthName).IsRequired(false).IsUnicode().HasMaxLength(250);

            builder.Property(x => x.CrDateTime).HasDefaultValue(DateTime.Now);

            builder.HasOne(x => x.Gender).WithMany(x => x.AppUsers).HasForeignKey(x => x.GenderId);
            builder.HasOne(x => x.UserStatus).WithMany(x => x.AppUsers).HasForeignKey(x => x.UserStatusId);
        }
    }
}
