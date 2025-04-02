using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.EntityConfigurations
{
    internal class BookBorrowingEntityConfiguration : IEntityTypeConfiguration<BookBorrowing>
    {
        public void Configure(EntityTypeBuilder<BookBorrowing> builder)
        {
            builder.HasKey(bb => bb.Id);

            builder.Property(bb => bb.BookId).IsRequired();
            builder.Property(bb => bb.UserId).IsRequired();
            builder.Property(bb => bb.BorrowedDate).IsRequired();
            builder.Property(bb => bb.ReturnDate).IsRequired();

            builder.HasOne(bb => bb.Book).WithMany().HasForeignKey(bb => bb.BookId);

            builder.HasOne(bb => bb.User).WithMany(u => u.BorrowedBooks).HasForeignKey(bb => bb.UserId);
        }
    }
}