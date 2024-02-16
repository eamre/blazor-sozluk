using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Queries.GetMainPageEntries
{
    public class GetMainPageEntriesQuery : BasePageQuery, IRequest<PagedViewModel<GetEntryDetailViewModel>>
    {
        public Guid? UserId { get; set; }

        public GetMainPageEntriesQuery(Guid? userId,int page, int pageSize) : base(page, pageSize)
        {
            UserId = userId;
        }
    }
}
