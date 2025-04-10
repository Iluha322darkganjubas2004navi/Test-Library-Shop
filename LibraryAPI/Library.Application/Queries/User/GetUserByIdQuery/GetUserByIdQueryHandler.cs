using AutoMapper;
using FluentValidation;
using Library.Application.DTOs.User;
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

namespace Library.Application.Queries.User.GetUserByIdQuery
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<GetUserByIdQuery> _validator;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper, IValidator<GetUserByIdQuery> validator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<UserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var user = await _userRepository.GetByIdAsync(request.userId, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            return _mapper.Map<UserDTO>(user);
        }
    }
}
