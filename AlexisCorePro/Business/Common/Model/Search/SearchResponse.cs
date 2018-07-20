using System;
using System.Collections.Generic;

namespace AlexisCorePro.Business.Common.Model.Search
{
    public class SearchResponse<T>
    {
        public int PageSize { get; set; }
        public int EntriesCount { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages =>
            (int) Math.Ceiling((double)EntriesCount / (double)PageSize);

        public List<T> Result { get; set; }
    }
}
