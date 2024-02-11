using BlazorSozluk.Common.Events.EntryComment;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.EntryComment.DeleteVote
{
    public class DeleteEntryCommentVoteCommandHandler : IRequestHandler<DeleteEntryCommentVoteCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(
                           exchangeName: SozlukConstants.VoteExchangeName,
                           exchangeType: SozlukConstants.DefaultExchangeType,
                           queueName: SozlukConstants.DeleteEntryCommentVoteQueueName,
                           obj: new DeleteEntryCommentVoteEvent()
                           {
                               CreatedBy = request.UserId,
                               EntryCommentId = request.EntryCommentId
                           });

            return await Task.FromResult(true);
        }
    }
}
