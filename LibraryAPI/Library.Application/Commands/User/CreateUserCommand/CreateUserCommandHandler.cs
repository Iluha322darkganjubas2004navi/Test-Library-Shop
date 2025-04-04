using AutoMapper;
using FluentValidation;
using Library.Application.Exceptions;
using MediatR;
using Library.Application.DTOs.User;
using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.User.CreateUserCommand
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUserCommand> _validator;
        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IValidator<CreateUserCommand> validator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingUser = await _userRepository.GetUserByEmailAsync(request.createUserDTO.Email);

            if (existingUser != null)
            {
                throw new BadRequestException("User with this email already exists");
            }

            var user = _mapper.Map<Domain.Entities.User>(request.createUserDTO);
            await _userRepository.AddAsync(user);

            return true;
        }
    }
}

