﻿using Library.Domain.Entities.Base;

namespace Library.Domain.Entities;
public class Book : BaseEntity
{
    public string ISBN { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    public Author Author { get; set; }
    public DateTime? BorrowedDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}