using Library.Application.Commands.Author.DeleteAuthorCommand;
using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Tests.UseCases
{
    public class DeleteAuthorUseCaseTests
    {
        private readonly Mock<IAuthorRepository> authorRepositoryMock;
        private readonly DeleteAuthorCommandHandler handler;

        public DeleteAuthorUseCaseTests()
        {
            authorRepositoryMock = new Mock<IAuthorRepository>();
            handler = new DeleteAuthorCommandHandler(authorRepositoryMock.Object);
        }

        [Fact]
        public async Task Execute_ExistingAuthorId_DeletesAuthorSuccessfully()
        {
            // Arrange
            var authorIdToDelete = Guid.NewGuid();

            // Мокаем репозиторий, чтобы он возвращал существующего автора
            authorRepositoryMock.Setup(repo => repo.GetByIdAsync(authorIdToDelete, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Author { Id = authorIdToDelete });

            // Мокаем метод DeleteAsync
            authorRepositoryMock.Setup(repo => repo.DeleteAsync(It.Is<Author>(a => a.Id == authorIdToDelete), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Author { Id = authorIdToDelete }); // Возвращаем того же автора

            var command = new DeleteAuthorCommand(authorIdToDelete);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            // Проверяем, что метод GetByIdAsync был вызван с правильным ID и CancellationToken
            authorRepositoryMock.Verify(repo => repo.GetByIdAsync(authorIdToDelete, It.IsAny<CancellationToken>()), Times.Once);

            // Проверяем, что метод DeleteAsync был вызван один раз с правильным автором и CancellationToken
            authorRepositoryMock.Verify(repo => repo.DeleteAsync(It.Is<Author>(a => a.Id == authorIdToDelete), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Execute_NonExistingAuthorId_ThrowsNotFoundException()
        {
            // Arrange
            var nonExistingAuthorId = Guid.NewGuid();

            // Мокаем репозиторий, чтобы он возвращал null (автор не найден)
            authorRepositoryMock.Setup(repo => repo.GetByIdAsync(nonExistingAuthorId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Author)null);

            var command = new DeleteAuthorCommand(nonExistingAuthorId);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

            // Проверяем, что метод GetByIdAsync был вызван с правильным ID и CancellationToken
            authorRepositoryMock.Verify(repo => repo.GetByIdAsync(nonExistingAuthorId, It.IsAny<CancellationToken>()), Times.Once);

            // Проверяем, что метод DeleteAsync НЕ был вызван
            authorRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Author>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Execute_ExceptionDuringDeletion_ShouldNotSwallowException()
        {
            // Arrange
            var authorIdToDelete = Guid.NewGuid();
            var expectedException = new Exception("Database error during deletion");

            // Мокаем репозиторий, чтобы он возвращал существующего автора
            authorRepositoryMock.Setup(repo => repo.GetByIdAsync(authorIdToDelete, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Author { Id = authorIdToDelete });

            // Мокаем метод DeleteAsync, чтобы он выбрасывал исключение
            authorRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Author>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(expectedException);

            var command = new DeleteAuthorCommand(authorIdToDelete);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
            Assert.Equal(expectedException.Message, exception.Message);

            // Проверяем, что метод GetByIdAsync был вызван с правильным ID и CancellationToken
            authorRepositoryMock.Verify(repo => repo.GetByIdAsync(authorIdToDelete, It.IsAny<CancellationToken>()), Times.Once);

            // Проверяем, что метод DeleteAsync был вызван с CancellationToken
            authorRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Author>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}