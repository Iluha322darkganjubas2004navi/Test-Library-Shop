using AutoMapper;
using FluentValidation;
using Library.Application.Commands.Author.CreateAuthorCommand;
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
    public class AddAuthorUseCaseTests
    {
        private readonly Mock<IAuthorRepository> authorRepositoryMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IValidator<CreateAuthorCommand>> validatorMock;
        private readonly CreateAuthorCommandHandler handler;

        public AddAuthorUseCaseTests()
        {
            authorRepositoryMock = new Mock<IAuthorRepository>();

            mapperMock = new Mock<IMapper>();

            validatorMock = new Mock<IValidator<CreateAuthorCommand>>();

            handler = new CreateAuthorCommandHandler(
                authorRepositoryMock.Object,
                mapperMock.Object,
                validatorMock.Object);
        }

        [Fact]
        public async Task MyExecute_ValidRequest_AddsAuthorSuccessfully()
        {
            // Arrange
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            var mapperMock = new Mock<IMapper>();
            var validatorMock = new Mock<IValidator<CreateAuthorCommand>>();

            var dto = new CreateAuthor
            {
                FirstName = "Ernest",
                LastName = "Hemingway",
                Country = "USA",
                DateOfBirth = new DateTime(1899, 7, 21)
            };

            var command = new CreateAuthorCommand(dto);
            var expectedAuthor = new Author
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Country = dto.Country,
                DateOfBirth = dto.DateOfBirth
            };

            // Мокаем валидацию на успех
            validatorMock.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            // Мокаем маппинг DTO (CreateAuthor) в сущность (Author)
            mapperMock.Setup(m => m.Map<Author>(dto)) // Изменили на маппинг DTO
                      .Returns(expectedAuthor);

            // Мокаем репозиторий на успешное добавление
            authorRepositoryMock.Setup(repo => repo.AddAsync(expectedAuthor))
                                .ReturnsAsync(expectedAuthor);

            var handler = new CreateAuthorCommandHandler(
                authorRepositoryMock.Object,
                mapperMock.Object,
                validatorMock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            // Проверяем, что валидатор был вызван
            validatorMock.Verify(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()), Times.Once);

            // Проверяем, что маппер был вызван для DTO
            mapperMock.Verify(m => m.Map<Author>(dto), Times.Once); // Изменили на проверку маппинга DTO

            // Проверяем, что метод AddAsync репозитория был вызван с правильной сущностью
            authorRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Author>(a =>
                    a.FirstName == expectedAuthor.FirstName &&
                    a.LastName == expectedAuthor.LastName &&
                    a.Country == expectedAuthor.Country &&
                    a.DateOfBirth == expectedAuthor.DateOfBirth)), Times.Once);
        }

        [Fact]
        public async Task Execute_InvalidRequest_ThrowsValidationException()
        {
            // Arrange: создаем DTO с ошибками
            var dto = new CreateAuthor
            {
                FirstName = "", // Пропускаем обязательное поле
                LastName = "Doe",
                Country = "USA",
                DateOfBirth = DateTime.Now.AddYears(-40)
            };

            var request = new CreateAuthorCommand(dto);

            // Мокаем валидацию, которая не проходит
            validatorMock
                .Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult
                {
                    Errors = { new FluentValidation.Results.ValidationFailure("FirstName", "First name is required") }
                });

            // Act & Assert: ожидаем, что будет выброшено исключение валидации
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(request, CancellationToken.None));
        }
    }
}
