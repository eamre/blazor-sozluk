using BlazorSozluk.Api.Application.Features.Commands.Entry.CreateFav;
using BlazorSozluk.Api.Application.Features.Commands.Entry.DeleteFav;
using BlazorSozluk.Api.Application.Features.Commands.EntryComment.CreateFav;
using BlazorSozluk.Api.Application.Features.Commands.EntryComment.DeleteFav;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : BaseController
    {
        private readonly IMediator _mediator;

        public FavoritesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("entry/{entryId}")]
        public async Task<IActionResult> CreateEntryFav(Guid entryId)
        {
            var result = await _mediator.Send(new CreateEntryFavCommand(entryId, UserId));
            return Ok(result);
        }

        [HttpPost]
        [Route("EntryComment/{entryCommentId}")]
        public async Task<IActionResult> CreateEntryCommentFav(Guid entryCommentId)
        {
            var result = await _mediator.Send(new CreateEntryCommentFavCommand(entryCommentId, UserId.Value));
            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteEntryFav/{entryId}")]
        public async Task<IActionResult> DeleteEntryFav(Guid entryId)
        {
            var result = await _mediator.Send(new DeleteEntryFavCommand(entryId, UserId.Value));
            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteEntryCommentFav/{entryCommentId}")]
        public async Task<IActionResult> DeleteEntryCommentFav(Guid entryCommentId)
        {
            var result = await _mediator.Send(new DeleteEntryCommentFavCommand(entryCommentId, UserId.Value));
            return Ok(result);
        }
    }
}
