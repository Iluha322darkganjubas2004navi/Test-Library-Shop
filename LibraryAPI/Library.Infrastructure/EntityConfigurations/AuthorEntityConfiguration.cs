using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.EntityConfigurations
{
    internal class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.FirstName).IsRequired().HasMaxLength(128);
            builder.Property(a => a.LastName).IsRequired().HasMaxLength(128);
            builder.Property(a => a.DateOfBirth).IsRequired();
            builder.Property(a => a.Country).IsRequired().HasMaxLength(128);

            builder.HasMany(a => a.Books).WithOne(b => b.Author).HasForeignKey(b => b.AuthorId);
        }
    }
}
