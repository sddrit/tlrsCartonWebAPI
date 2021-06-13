using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Helper
{
    public class PagedResponse<T>
    {
        public PagedResponse() { }

        public PagedResponse(IEnumerable<T> data, int _pageNumber, int _pageSize, int _totalCount)
        {
            Data = data;
            pageNumber = _pageNumber;
            pageSize = _pageSize;
            totalCount = _totalCount;
            totalPages = (int)Math.Ceiling(_totalCount / (double)_pageSize);
        }

        public IEnumerable<T> Data { get; set; }

        public int? pageNumber { get; set; }
        public int? pageSize { get; set; }
        public int? totalCount { get; set; }
        public int? totalPages { get; set; }

    }
}
