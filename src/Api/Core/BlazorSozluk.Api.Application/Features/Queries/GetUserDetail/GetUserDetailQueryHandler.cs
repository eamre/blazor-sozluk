﻿using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Common.Models.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Queries.GetUserDetail
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailViewModel>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public GetUserDetailQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<UserDetailViewModel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
        //    if (request.UserId.Equals(Guid.Empty) || string.IsNullOrWhiteSpace(request.UserName))
        //        throw new ArgumentException("Either 'userId' or 'userName' must be provided.");

            User dbUser = null;

            if (request.UserId != Guid.Empty)//nullable olarak tanımlarsak hasvalue ile kontrol edebiliriz
                dbUser = await userRepository.GetByIdAsync(request.UserId);

            else if (!string.IsNullOrEmpty(request.UserName))
                dbUser = await userRepository.GetSingleAsync(i => i.UserName == request.UserName);

            return mapper.Map<UserDetailViewModel>(dbUser);
        }
    }
}
