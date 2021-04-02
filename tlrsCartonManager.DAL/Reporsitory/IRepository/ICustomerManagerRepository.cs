using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.DAL.Models;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface ICustomerManagerRepository
    {
        Task<IEnumerable<CustomerDisplayDto>> GetCustomerList();
        Task<CustomerDisplayDto> GetCustomerById(int customerId);

        Task<IEnumerable<CustomerMainCodeSearchDto>> GetCustomerByMainId(string customerName);

        Task<PagedResponse<CustomerSearchDto>> SearchCustomer(string columnName, string columnValue,
            int pageIndex, int pageSize);

        bool AddCustomer(CustomerInsertDto customerInsert);
        bool UpdateCustomer(CustomerInsertUpdateDto customerUpdate);
        bool DeleteCustomer(CustomerDeleteDto customerDelete);
    }
}
