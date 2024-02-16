using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common.Models.Page
{
    public class BasePageQuery
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public BasePageQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}
