using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tlrsCartonManager.DAL.Exceptions;
using tlrsCartonManager.DAL.Models;
using tlrsCartonManager.DAL.Models.Base;
using tlrsCartonManager.DAL.Reporsitory.IRepository;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class BaseMetadataRepository<TEntity, TDto> : IMetadataRepository<TEntity, TDto> where TEntity : class where TDto : class
    {

        public readonly tlrmCartonContext _tcContext;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IMapper _mapper;

        public BaseMetadataRepository(tlrmCartonContext tccontext, IMapper mapper)
        {
            _tcContext = tccontext;
            _dbSet = _tcContext.Set<TEntity>();
            _mapper = mapper;
        }

        public async Task<TDto> AddItem(TDto item)
        {          
                var entity = _mapper.Map<TEntity>(item);

                //await ValidateItem(entity);

                await _dbSet.AddAsync(entity);

                await _tcContext.SaveChangesAsync();

                return _mapper.Map<TDto>(entity);
           
           
        }

        public async Task<TDto> EditItem(TDto item)
        {
            var entity = _mapper.Map<TEntity>(item);

            //await ValidateItem(entity);

            _dbSet.Update(entity);

            await _tcContext.SaveChangesAsync();

            return _mapper.Map<TDto>(entity);

        }

        public async Task DeleteItem(int id)
        {

            var entity = await _dbSet.FindAsync(id);

            if (entity is ISoftDelete softDeletedEntity)
            {
                softDeletedEntity.Deleted = true;
            }

            await _tcContext.SaveChangesAsync();
        }

        public async Task<IList<TDto>> GetAll()
        {
            return (await _dbSet.ToListAsync())
                .Select(_mapper.Map<TDto>).ToList();
        }

        public async Task<TDto> GetById(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity is ISoftDelete softDeletedEntity && softDeletedEntity.Deleted)
            {
                throw new ServiceException(new ErrorMessage[]
                {
                   new ErrorMessage()
                   {
                      Code = string.Empty,
                     Message = $"Unable to find meta data by {id}"
                   }
                });
            }

            return _mapper.Map<TDto>(entity);

        }
        //public async Task<bool> ValidateItem(TEntity item)
        //{
        //    try
        //    {
        //        var entity = await _dbSet.AsNoTracking().ToListAsync();

        //        var currentItem = _mapper.Map<MetadataValidator>(item);
        //        if(!string.IsNullOrEmpty(currentItem.Type))
        //        {



        //        }

        //        var validatingEntity = _mapper.Map<List<MetadataValidator>>(entity);

        //        if (currentItem.Id == 0 && validatingEntity.Any(x => x.Description.ToLower() == currentItem.Description.ToLower() ||x.Type.ToLower() == currentItem.Type.ToLower()))                     
        //        {
        //            throw new ServiceException(new ErrorMessage[]
        //                {
        //                   new ErrorMessage()
        //                   {
        //                      Code = string.Empty,
        //                     Message = $"Exsiting description found "
        //                   }
        //                });
        //        }

        //        else if (currentItem.Id> 0 && validatingEntity.Any(x => x.Id != currentItem.Id && (x.Description.ToLower() == currentItem.Description.ToLower() ||
        //              x.Type.ToLower() == currentItem.Type.ToLower())))
        //        {
        //            throw new ServiceException(new ErrorMessage[]
        //                {
        //                   new ErrorMessage()
        //                   {
        //                      Code = string.Empty,
        //                     Message = $"Exsiting description found "
        //                   }
        //                });

        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

    }
}
