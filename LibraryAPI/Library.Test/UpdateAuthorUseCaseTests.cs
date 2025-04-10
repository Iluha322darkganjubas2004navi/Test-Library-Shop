using AutoMapper;
using FluentValidation;
using Library.Application.Commands.Author.UpdateAuthorCommand;
using Library.Application.Exceptions;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.Tests.UseCases
{
    public class UpdateAuthorUseCaseTests
    {
        private readonly Mock<IAuthorRepository> authorRepositoryMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IValidator<UpdateAuthorCommand>> validatorMock;
        private readonly UpdateAuthorCommandHandler handler;

        public UpdateAuthorUseCaseTests()
        {
            authorRepositoryMock = new Mock<IAuthorRepository>();
            mapperMock = new Mock<IMapper>();
            validatorMock = new Mock<IValidator<UpdateAuthorCommand>>();
            handler = new UpdateAuthorCommandHandler(
                authorRepositoryMock.Object,
                mapperMock.Object,
                validatorMock.Object);
        }

        [Fact]
        public async Task Execute_InvalidRequest_ThrowsValidationException()
        {
            // Arrange
            var dto = new UpdateAuthor { Id = Guid.NewGuid(), FirstName = "" }; // Invalid FirstName
            var command = new UpdateAuthorCommand(dto);
            var validationResult = new FluentValidation.Results.ValidationResult();
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("FirstName", "First name is required"));

            // Мокаем валидацию на провал
            validatorMock.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationResult);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));

            validatorMock.Verify(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()), Times.Once);
            authorRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
            mapperMock.Verify(m => m.Map(It.IsAny<UpdateAuthor>(), It.IsAny<Author>()), Times.Never);
            authorRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Author>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Execute_ValidRequest_AuthorNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var dto = new UpdateAuthor { Id = authorId, FirstName = "New First Name" };
            var command = new UpdateAuthorCommand(dto);

            // Мокаем валидацию на успех
            validatorMock.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            // Мокаем отсутствие автора
            authorRepositoryMock.Setup(repo => repo.GetByIdAsync(authorId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Author)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

            validatorMock.Verify(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()), Times.Once);
            authorRepositoryMock.Verify(repo => repo.GetByIdAsync(authorId, It.IsAny<CancellationToken>()), Times.Once);
            mapperMock.Verify(m => m.Map(It.IsAny<UpdateAuthor>(), It.IsAny<Author>()), Times.Never);
            authorRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Author>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}