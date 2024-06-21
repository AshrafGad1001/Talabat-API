namespace Talabat.core.Specifications
{
    public class ProductSpecParms
    {
        private const int MaxpageSize = 10;
        public string? sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

        private int pageSize { get; set; } = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxpageSize ? MaxpageSize : value; }
        }
        private string? search;
        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }
        public int pageIndex { get; set; } = 1;
    }
}
