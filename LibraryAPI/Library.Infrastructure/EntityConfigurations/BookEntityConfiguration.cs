using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class BookEntityConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.ISBN).IsRequired().HasMaxLength(20);
            builder.Property(b => b.Title).IsRequired().HasMaxLength(200);
            builder.Property(b => b.Description).HasMaxLength(2000);
            builder.Property(b => b.AuthorId).IsRequired();
            builder.Property(b => b.IsBorrowed).IsRequired().HasDefaultValue(false);

            builder.HasOne(b => b.Author).WithMany(a => a.Books).HasForeignKey(b => b.AuthorId);

            builder.HasMany(b => b.Genres).WithMany(g => g.Books);
        }
    }
}
