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
using tlrsCartonManager.DAL.Reporsitory.Metadata.Core;

namespace tlrsCartonManager.DAL.Reporsitory
{
    public class BaseMetadataRepository<TEntity, TDto> : IMetadataRepository<TEntity, TDto> where TEntity : class where TDto : class
    {

        public readonly tlrmCartonContext _tcContext;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IMapper _mapper;
        private readonly BaseMetaRepositoryValidator _validator;

        public BaseMetadataRepository(tlrmCartonContext tccontext, IMapper mapper, BaseMetaRepositoryValidator validator)
        {
            _tcContext = tccontext;
            _dbSet = _tcContext.Set<TEntity>();
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<TDto> AddItem(TDto item)
        {
            var entity = _mapper.Map<TEntity>(item);

            await ValidateItem(entity);

            await _dbSet.AddAsync(entity);

            await _tcContext.SaveChangesAsync();

            return _mapper.Map<TDto>(entity);


        }

        public async Task<TDto> EditItem(TDto item)
        {
            var entity = _mapper.Map<TEntity>(item);

            await ValidateItem(entity);

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

        public async Task<IList<TDto>> GetAllMetaData()
        {
            var result = (await _dbSet.ToListAsync())
                .Select(_mapper.Map<MetadataBase>).Where(x => x.Deleted == false && x.Active == true).ToList();

            return _mapper.Map<List<TDto>>(result);
        }
        public async Task<IList<TDto>> GetAll()
        {
            var result = (await _dbSet.ToListAsync())
                .Select(_mapper.Map<MetadataBase>).Where(x => x.Deleted == false).ToList();

            return _mapper.Map<List<TDto>>(result);
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
        public async Task<bool> ValidateItem(TEntity item)
        {

            var entity = await _dbSet.AsNoTracking().ToListAsync();

            var currentItem = _mapper.Map<MetadataBase>(item);

            var validatingEntity = _mapper.Map<List<MetadataBase>>(entity);

            _validator.ValidateItemByType(currentItem, validatingEntity);

            _validator.ValidateItemByDescription(currentItem, validatingEntity);

            return true;

        }

    }
}
