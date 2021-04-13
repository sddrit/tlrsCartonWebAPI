using AutoMapper;
using tlrsCartonManager.DAL.Reporsitory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Dtos;
using Microsoft.Data.SqlClient;
using tlrsCartonManager.DAL.Utility;
using System.Data;
using tlrsCartonManager.DAL.Helper;
using static tlrsCartonManager.DAL.Utility.Status;
using tlrsCartonManager.DAL.Extensions;
using Newtonsoft.Json;
using tlrsCartonManager.DAL.Dtos.Invoice;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class SlabTypeManagerRepository : ISlabTypeManagerRepository
    {
        private readonly tlrmCartonContext _tcContext;
        private readonly IMapper _mapper;

        public SlabTypeManagerRepository(tlrmCartonContext tccontext, IMapper mapper)
        {
            _tcContext = tccontext;
            _mapper = mapper;
        }

        public Task<InvoiceProfileDto> AddInvoiceProfile(InvoiceProfileDto invProfDto)
        {
            throw new NotImplementedException();
        }

        public Task<InvoiceSlabTypeHeaderDto> AddInvoiceSlabTypeHeader(InvoiceSlabTypeHeaderDto invSlabTypeHeader)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<InvoiceSlabTypeDetailDto>> AddSlabTypeDetail(ICollection<InvoiceSlabTypeDetailDto> invSlabTypeDetails)
        {
            throw new NotImplementedException();
        }

        public Task<InvoiceSlabTypeHeaderDto> EditInvoiceSlabTypeHeader(InvoiceSlabTypeHeaderDto invSlabTypeHeader)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<InvoiceSlabTypeDetailDto>> EditSlabTypeDetail(int existingRow, ICollection<InvoiceSlabTypeDetailDto> invSlabTypeDetails)
        {
            throw new NotImplementedException();
        }

        public Task<PageList<InvoiceProfileDto>> GetInvoiceProfile(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InvoiceSlabTypeDetailDto>> GetInvoiceSlabTypeDetails(int invSlabId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InvoiceSlabTypeHeaderDto>> GetInvoiceTypeslabHeader(int invProfile, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveContextAsync()
        {
            throw new NotImplementedException();
        }
    }
}
