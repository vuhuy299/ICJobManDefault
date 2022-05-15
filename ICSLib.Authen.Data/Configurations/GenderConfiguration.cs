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
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.ToTable("Genders");
            builder.HasKey(x => x.GenderId);
            builder.Property(x => x.GenderId).ValueGeneratedNever();
            builder.Property(x => x.GenderName).IsRequired(false).IsUnicode().HasMaxLength(150);
            builder.Property(x => x.GenderDesc).IsRequired(false).IsUnicode().HasMaxLength(150);
        }
    }
}
