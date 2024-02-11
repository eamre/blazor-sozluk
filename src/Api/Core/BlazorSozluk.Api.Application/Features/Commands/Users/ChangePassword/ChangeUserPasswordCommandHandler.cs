using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Users.ChangePassword
{
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, bool>
    {
        private readonly IUserRepository userRepository;

        public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            if(!request.UserId.HasValue)          
                throw new ArgumentNullException(nameof(request.UserId));

            var dbUser = await userRepository.GetByIdAsync(request.UserId.Value) ?? throw new DatabaseValidationException("user not found");

            var encryptedPassword = PasswordEncryptor.Encrypt(request.OldPassword);

            if (dbUser.Password != encryptedPassword)
                throw new DatabaseValidationException("old password wrong");

            dbUser.Password = encryptedPassword;

            await userRepository.UpdateAsync(dbUser);

            return true;
        }
    }
}
