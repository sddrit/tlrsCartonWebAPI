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
        Task<IEnumerable<CustomerDto>> GetCustomerList();
        Task<CustomerDto> GetCustomerById(int customerId);
        Task<IEnumerable<CustomerSearchDto>> GetCustomerByName(string customerName, bool isAll);
        Task<IEnumerable<CustomerSearchDto>> GetCustomerByCode(string customerName, bool isAll);
        Task<IEnumerable<CustomerMainCodeSearchDto>> GetCustomerByMainName(string customerName);
        Task<IEnumerable<CustomerMainCodeSearchDto>> GetCustomerByMainId(int customerId);
        Task<PagedResponse<CustomerSearchDto>> SearchCustomer(string columnValue, string searchColumn, string sortOrder, int pageIndex, int pageSize);
        Task<List<CustomerAuthorizationHeaderDto>> GetCustomerAuthorizationById(int customerId);

        Task<IEnumerable<CustomerSearchDto>> GetCustomerForInvoiceByCode(string customerCode);

        Task<IEnumerable<CustomerSearchDto>> GetCustomerForInvoice(string customerName);

        int? GetCustomerId(string customerCode);
        bool ValidateCustomer(CustomerDto customer, string transcationType);
        bool AddCustomer(CustomerDto customerInsert);
        bool UpdateCustomer(CustomerDto customerUpdate);
        bool DeleteCustomer(CustomerDeleteDto customerDelete);
    }
}
