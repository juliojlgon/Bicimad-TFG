namespace Bicimad.Services.Query.Queries
{
    public class PagedQuery
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int OutTotalCount { get; set; }
    }
}
