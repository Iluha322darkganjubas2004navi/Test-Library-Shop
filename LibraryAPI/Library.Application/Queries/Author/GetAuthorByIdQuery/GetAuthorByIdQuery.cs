using Library.Domain.DTOs;
using MediatR;
using System;

namespace Library.Application.Queries.Author.GetAuthorByIdQuery;

public sealed record GetAuthorByIdQuery(Guid AuthorId) : IRequest<AuthorDTO>;