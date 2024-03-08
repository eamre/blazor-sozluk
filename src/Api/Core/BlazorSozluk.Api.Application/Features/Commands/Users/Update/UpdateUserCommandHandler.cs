using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Users.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await userRepository.GetByIdAsync(request.Id) ?? throw new DatabaseValidationException("User not found");
            var dbEmailAddress = dbUser.EmailAddress;
            var emailChanged = string.CompareOrdinal(dbEmailAddress, request.EmailAddress) != 0;
           
            //dbUser = mapper.Map<User>(request);
            mapper.Map(request, dbUser); //nesneden yaratmıyor requesttekini db user a gönderiyor.
           
            var user = await userRepository.UpdateAsync(dbUser);

            //check if email changed

            if (emailChanged && user > 0)
            {
                var @event = new UserEmailChangedEvent()
                {
                    OldEmailAddress = dbEmailAddress,
                    NewEmailAddress = dbUser.EmailAddress
                };

                QueueFactory.SendMessageToExchange(
                    exchangeName: SozlukConstants.UserExchangeName, 
                    exchangeType: SozlukConstants.DefaultExchangeType, 
                    queueName: SozlukConstants.UserEmailChangedQueueName, 
                    obj: @event);

                dbUser.EmailConfirmed = false;
                await userRepository.UpdateAsync(dbUser);
            }

            return dbUser.Id;

        }
    }
}
