using AutoMapper;
using Library.Application.Queries.Author.GetAllAuthorsQuery;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Tests.UseCases
{
    public class GetAllAuthorsUseCaseTests
    {
        private readonly Mock<IAuthorRepository> authorRepositoryMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly GetAllAuthorsQueryHandler handler;

        public GetAllAuthorsUseCaseTests()
        {
            authorRepositoryMock = new Mock<IAuthorRepository>();
            mapperMock = new Mock<IMapper>();
            handler = new GetAllAuthorsQueryHandler(authorRepositoryMock.Object, mapperMock.Object);
        }

        [Fact]
        public async Task Execute_NoAuthorsExist_ReturnsEmptyIEnumerableOfAuthorDTO()
        {
            // Arrange
            authorRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Author>());

            var query = new GetAllAuthorsQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Empty(result);
            authorRepositoryMock.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
            mapperMock.Verify(m => m.Map<IEnumerable<AuthorDTO>>(It.IsAny<List<Author>>()), Times.Once);
        }

        [Fact]
        public async Task Execute_AuthorsExist_ReturnsMappedIEnumerableOfAuthorDTO()
        {
            // Arrange
            var authors = new List<Author>
            {
                new Author { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" },
                new Author { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith" }
            };

            var authorDTOs = new List<AuthorDTO>
            {
                new AuthorDTO { Id = authors[0].Id, FirstName = authors[0].FirstName, LastName = authors[0].LastName },
                new AuthorDTO { Id = authors[1].Id, FirstName = authors[1].FirstName, LastName = authors[1].LastName }
            };

            authorRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(authors);

            mapperMock.Setup(m => m.Map<IEnumerable<AuthorDTO>>(authors))
                .Returns(authorDTOs);

            var query = new GetAllAuthorsQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(authors.Count, result.Count());
            Assert.Equal(authorDTOs.First().Id, result.First().Id);
            Assert.Equal(authorDTOs.Last().LastName, result.Last().LastName);
            authorRepositoryMock.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
            mapperMock.Verify(m => m.Map<IEnumerable<AuthorDTO>>(authors), Times.Once);
        }

        [Fact]
        public async Task Execute_ExceptionFromRepository_ShouldNotSwallowException()
        {
            // Arrange
            var expectedException = new Exception("Database error while fetching authors");
            authorRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(expectedException);

            var query = new GetAllAuthorsQuery();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(query, CancellationToken.None));
            Assert.Equal(expectedException.Message, exception.Message);
            authorRepositoryMock.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
            mapperMock.Verify(m => m.Map<IEnumerable<AuthorDTO>>(It.IsAny<List<Author>>()), Times.Never);
        }
    }
}