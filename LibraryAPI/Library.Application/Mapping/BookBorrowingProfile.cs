using AutoMapper;
using Library.Domain.DTOs;
using Library.Domain.Entities;

namespace Library.Application.Mapping;

public class BookBorrowingProfile : Profile
{
    public BookBorrowingProfile()
    {
        CreateMap<BookBorrowing, BookBorrowingDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.BorrowedDate, opt => opt.MapFrom(src => src.BorrowedDate))
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate))
            .ForMember(dest => dest.ReturnedAt, opt => opt.MapFrom(src => src.ReturnedAt));

        CreateMap<BookBorrowingDTO, BookBorrowing>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.BorrowedDate, opt => opt.MapFrom(src => src.BorrowedDate))
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate))
            .ForMember(dest => dest.ReturnedAt, opt => opt.MapFrom(src => src.ReturnedAt))
            .ForMember(dest => dest.Book, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());

        CreateMap<CreateBookBorrowing, BookBorrowing>()
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.BorrowedDate, opt => opt.MapFrom(src => src.BorrowedDate))
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate))
            .ForMember(dest => dest.Book, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());

        CreateMap<UpdateBookBorrowing, BookBorrowing>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.BorrowedDate, opt => opt.MapFrom(src => src.BorrowedDate))
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate))
            .ForMember(dest => dest.ReturnedAt, opt => opt.MapFrom(src => src.ReturnedAt))
            .ForMember(dest => dest.Book, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());
    }
}