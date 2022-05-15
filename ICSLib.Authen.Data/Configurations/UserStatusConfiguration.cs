/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ICSLib.Authen.Data.Entities;

namespace ICSLib.Authen.Data.Configurations
{
    public class UserStatusConfiguration : IEntityTypeConfiguration<UserStatus>
    {
        public void Configure(EntityTypeBuilder<UserStatus> builder)
        {
            builder.ToTable("UserStatuses");
            builder.HasKey(x => x.UserStatusId);
            builder.Property(x => x.UserStatusId).ValueGeneratedNever();
            builder.Property(x => x.UserStatusName).IsRequired(false).IsUnicode().HasMaxLength(150);
            builder.Property(x => x.UserStatusDesc).IsRequired(false).IsUnicode().HasMaxLength(150);
        }
    }
}
