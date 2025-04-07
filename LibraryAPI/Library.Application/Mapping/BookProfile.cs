using AutoMapper;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using System.Linq;

namespace Library.Application.Mapping;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
            .ForMember(dest => dest.BorrowedDate, opt => opt.MapFrom(src => src.BorrowedDate))
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => new GenreDTO { Id = g.Id, Name = g.Name })))
            .ForMember(dest => dest.IsBorrowed, opt => opt.MapFrom(src => src.IsBorrowed));

        CreateMap<BookDTO, Book>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
            .ForMember(dest => dest.BorrowedDate, opt => opt.MapFrom(src => src.BorrowedDate))
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate))
            .ForMember(dest => dest.IsBorrowed, opt => opt.MapFrom(src => src.IsBorrowed))
            .ForMember(dest => dest.Genres, opt => opt.Ignore())
            .ForMember(dest => dest.Author, opt => opt.Ignore());

        CreateMap<CreateBook, Book>()
            .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
            .ForMember(dest => dest.BorrowedDate, opt => opt.MapFrom(src => src.BorrowedDate))
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate))
            .ForMember(dest => dest.IsBorrowed, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.Genres, opt => opt.Ignore())
            .ForMember(dest => dest.Author, opt => opt.Ignore());

        CreateMap<UpdateBook, Book>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
            .ForMember(dest => dest.BorrowedDate, opt => opt.MapFrom(src => src.BorrowedDate))
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate))
            .ForMember(dest => dest.IsBorrowed, opt => opt.MapFrom(src => src.IsBorrowed))
            .ForMember(dest => dest.Genres, opt => opt.Ignore())
            .ForMember(dest => dest.Author, opt => opt.Ignore());
    }
}