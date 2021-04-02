using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Helper;

namespace tlrsCartonManager.DAL.Models.ResponseModels
{
    //public class ResponseModel
    //{
    //    public int PageIndex { get; }

    //    public int PageSize { get; }

    //    public int TotalRecords { get; }

    //    public int TotalPages { get; }


    //}
    public class PagedModel<TModel>
    {
        private PagedListSP<CustomerSearch> customerList;

        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public IList<TModel> Items { get; set; }

        public PagedModel()
        {
            Items = new List<TModel>();
        }

        public PagedModel(PagedListSP<CustomerSearch> customerList)
        {
            this.customerList = customerList;
        }
    }
}
