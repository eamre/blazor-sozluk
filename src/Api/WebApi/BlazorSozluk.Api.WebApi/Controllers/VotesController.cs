using BlazorSozluk.Api.Application.Features.Commands.Entry.DeleteVote;
using BlazorSozluk.Api.Application.Features.Commands.EntryComment.DeleteVote;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Models;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotesController : BaseController
    {
        private readonly IMediator _mediator;

        public VotesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Entry/{entryId}")]
        public async Task<IActionResult> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote)
        {
            var result = await _mediator.Send(new CreateEntryVoteCommand(entryId, voteType, UserId.Value));

            return Ok(result);
        }

        [HttpPost]
        [Route("EntryComment/{entryCommentId}")]
        public async Task<IActionResult> CreateEntryCommentVote(Guid entryId, VoteType voteType = VoteType.UpVote)
        {
            var result = await _mediator.Send(new CreateEntryCommentVoteCommand(entryId, voteType, UserId.Value));

            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteEntryVote/{entryId}")]
        public async Task<IActionResult> DeleteEntryVote(Guid entryId)
        {
            var result = await _mediator.Send(new DeleteEntryVoteCommand(entryId, UserId.Value));

            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteEntryCommentVote/{entryCommentId}")]
        public async Task<IActionResult> DeleteEntryCommentVote(Guid entryCommentId)
        {
            var result = await _mediator.Send(new DeleteEntryCommentVoteCommand(entryCommentId, UserId.Value));

            return Ok(result);
        }
    }
}
