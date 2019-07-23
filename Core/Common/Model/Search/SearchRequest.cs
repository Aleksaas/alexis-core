using System.Collections.Generic;

namespace AlexisCorePro.Business.Common.Model.Search
{
    public class SearchRequest<T> where T : BaseQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public List<SearchOrder> Orders { get; set; }

        public T Query { get; set; }
    }
}
