using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Queries.GetEntryComments
{
    public class GetEntryCommentQuery : BasePageQuery, IRequest<PagedViewModel<GetEntryCommentsViewModel>>
    {
        public Guid EntryId { get; set; }
        public Guid? UserId { get; set; }

        public GetEntryCommentQuery(Guid entryId, Guid? userId, int page, int pageSize) : base(page, pageSize)
        {
            EntryId = entryId;
            UserId = userId;
        }
    }
}
