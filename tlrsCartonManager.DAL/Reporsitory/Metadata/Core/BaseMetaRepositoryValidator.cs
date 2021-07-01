using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Exceptions;
using tlrsCartonManager.DAL.Models.Base;
using tlrsCartonManager.DAL.Reporsitory.IRepository;

namespace tlrsCartonManager.DAL.Reporsitory.Metadata.Core
{
    public class BaseMetaRepositoryValidator
    {
        public void ValidateItemByDescription(MetadataBase currentItem, List<MetadataBase> validatingEntity)
        {

            if (currentItem.Id == 0 && validatingEntity.Any(x => x.Description.ToLower() == currentItem.Description.ToLower()))
            {
                throw new ServiceException(new ErrorMessage[]
                    {
                           new ErrorMessage()
                           {
                              Code = string.Empty,
                             Message = $"Exsiting description found "
                           }
                    });
            }

            else if (currentItem.Id > 0 && validatingEntity.Any(x => x.Id != currentItem.Id && (x.Description.ToLower() == currentItem.Description.ToLower())))
            {
                throw new ServiceException(new ErrorMessage[]
                    {
                           new ErrorMessage()
                           {
                              Code = string.Empty,
                             Message = $"Exsiting description found "
                           }
                    });
            }
        }

        public void ValidateItemByType(MetadataBase currentItem, List<MetadataBase> validatingEntity)
        {

            if (validatingEntity.Where(x => x.Type != null).Any() &&
                (currentItem.Id == 0 && validatingEntity.Any(x => x.Type.ToLower() == currentItem.Type.ToLower())) ||
                (currentItem.Id > 0 && validatingEntity.Any(x => x.Id != currentItem.Id && (x.Type.ToLower() == currentItem.Type.ToLower()))))
            {
                throw new ServiceException(new ErrorMessage[]
                    {
                           new ErrorMessage()
                           {
                              Code = string.Empty,
                             Message = $"Exsiting type found "
                           }
                    });
            }



        }
    }
}
