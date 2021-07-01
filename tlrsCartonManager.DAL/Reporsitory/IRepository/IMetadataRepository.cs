using System.Collections.Generic;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Reporsitory.IRepository
{
    public interface IMetadataRepository<TEntity, TDto> where TEntity : class where TDto : class
    {
        Task<IList<TDto>> GetAll();
        Task<IList<TDto>> GetAllMetaData();
        Task<TDto> AddItem(TDto item);
        Task<TDto> EditItem(TDto item);
        Task DeleteItem(int id);
        Task<TDto> GetById(int id);


    }
}
