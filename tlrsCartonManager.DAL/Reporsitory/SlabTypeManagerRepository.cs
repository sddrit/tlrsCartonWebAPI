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
using AutoMapper.QueryableExtensions;
using tlrsCartonManager.DAL.Models.Invoice;

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

        public async Task<InvoiceProfileDto> AddInvoiceProfile(InvoiceProfileDto invProfDto)
        {
            
                await _tcContext.InvoiceProfiles.AddRangeAsync(_mapper.Map<InvoiceProfile>(invProfDto));

                if (await SaveContextAsync())
                {
                    return invProfDto;
                }
                else
                {
                    return null;
                }
            
            

        }

        public async Task<InvoiceSlabTypeHeaderDto> AddInvoiceSlabTypeHeader(InvoiceSlabTypeHeaderDto invSlabTypeHeader)
        {
            await _tcContext.InvoiceSlabTypeHeaders.AddRangeAsync(_mapper.Map<InvoiceSlabTypeHeader>(invSlabTypeHeader));

            if (await SaveContextAsync())
            {
                return invSlabTypeHeader;
            }
            else
            {
                return null;
            }

        }

        public async Task<ICollection<InvoiceSlabTypeDetailDto>> AddSlabTypeDetail(ICollection<InvoiceSlabTypeDetailDto> invSlabTypeDetails)
        {
            await _tcContext.InvoiceSlabTypeDetails.AddRangeAsync(_mapper.Map<ICollection<InvoiceSlabTypeDetail>>(invSlabTypeDetails));

            if (await SaveContextAsync())
            {
                return invSlabTypeDetails;
            }
            else
            {
                return null;
            }
        }

        public async Task<InvoiceSlabTypeHeaderDto> EditInvoiceSlabTypeHeader(InvoiceSlabTypeHeaderDto invSlabTypeHeader)
        {
            var invSlabHeader = await _tcContext.InvoiceSlabTypeHeaders.Include(x=>x.InvoiceSlabTypeDetails).FirstOrDefaultAsync(x => x.TrackingId == invSlabTypeHeader.TrackingId);
            

            invSlabHeader.Description = invSlabTypeHeader.Description;
            invSlabHeader.CalucationType = invSlabTypeHeader.CalucationType;
            invSlabHeader.RouteCode = invSlabTypeHeader.RouteCode;
            invSlabHeader.InvoiceChargingType = invSlabTypeHeader.InvoiceChargingType;
            invSlabHeader.CartonType = invSlabTypeHeader.CartonType;
            invSlabHeader.Active = invSlabTypeHeader.Active;
            invSlabHeader.LuUser = invSlabTypeHeader.LuUser;
            invSlabHeader.LuDate = invSlabTypeHeader.LuDate;

            var invSlabDetails = await _tcContext.InvoiceSlabTypeDetails.Where(x => x.Id == invSlabTypeHeader.TrackingId).ToListAsync();

            _tcContext.InvoiceSlabTypeDetails.RemoveRange(invSlabDetails);

            var _newSlabDetails = invSlabTypeHeader.InvoiceSlabTypeDetails.ToList();

            invSlabHeader.InvoiceSlabTypeDetails = _mapper.Map<ICollection<InvoiceSlabTypeDetail>>(_newSlabDetails);


            if (await SaveContextAsync())
            {
                return invSlabTypeHeader;
            }
            else
            {
                return null;
            }
        }

        public async Task<ICollection<InvoiceSlabTypeDetailDto>> EditSlabTypeDetail(int existingRow, ICollection<InvoiceSlabTypeDetailDto> invSlabTypeDetails)
        {
            var _slabDetails = await _tcContext.InvoiceSlabTypeDetails.Where(x => x.Id == existingRow).ToListAsync();

            _tcContext.InvoiceSlabTypeDetails.RemoveRange(_slabDetails);

            await _tcContext.InvoiceSlabTypeDetails.AddRangeAsync(_mapper.Map<InvoiceSlabTypeDetail>(invSlabTypeDetails));

            if (await SaveContextAsync())
            {
                return invSlabTypeDetails;
            }
            else
            {
                return null;
            }

        }

        public async Task<PageList<InvoiceProfileDto>> GetInvoiceProfile(int pageIndex, int pageSize)
        {           
                var invProfileList = _tcContext.InvoiceProfiles
                .ProjectTo<InvoiceProfileDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();           

                return await PageList<InvoiceProfileDto>.CreateAsync(invProfileList, pageIndex, pageSize);
        }

        public async Task<IEnumerable<InvoiceSlabTypeDetailDto>> GetInvoiceSlabTypeDetails(int invSlabId)
        {
            return _mapper.Map<IEnumerable<InvoiceSlabTypeDetailDto>>(await _tcContext.InvoiceSlabTypeDetails.Where(x => x.Id == invSlabId).ToListAsync());

        }

        public async Task<PageList<InvoiceSlabTypeHeaderDto>> GetInvoiceTypeslabHeader(int invProfile, int pageIndex, int pageSize)
        {
            var invSlabHeaderList = _tcContext.InvoiceSlabTypeHeaders.Where(x=>x.InvoiceProfileId == invProfile)
                .ProjectTo<InvoiceSlabTypeHeaderDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            return await PageList<InvoiceSlabTypeHeaderDto>.CreateAsync(invSlabHeaderList, pageIndex, pageSize);
        }

        public async Task<bool> SaveContextAsync()
        {
            return await _tcContext.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
