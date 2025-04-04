using AutoMapper;
using FluentValidation;
using Library.Application.Exceptions;
using MediatR;
using Library.Application.DTOs.User;
using Library.Application.Exceptions;
using Library.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.User.UpdateUserCommand
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateUserCommand> _validator;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IValidator<UpdateUserCommand> validator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingUser = await _userRepository.GetByIdAsync(request.updateUserDTO.Id);

            if (existingUser == null)
            {
                throw new NotFoundException("User not found");
            }

            _mapper.Map(request.updateUserDTO, existingUser);
            await _userRepository.UpdateAsync(existingUser);

            return true;
        }
    }
}
