using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Users.ConfirmEmail
{
    public class ConfirmEmailCommand:IRequest<bool>
    {
        public Guid ConfirmationId { get; set; }

    }
}
