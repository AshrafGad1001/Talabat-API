using Talabat.DTOs;

namespace Talabat.APIs.Helpers
{
    public class Pagination<T>
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }

        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
        {
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
            this.Count = count;
            Data = data;
        }
    }
}
