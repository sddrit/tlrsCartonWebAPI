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
            var result = validatingEntity.Where(x => x.Description != null);

            if (result.Any() &&(currentItem.Id == 0 && validatingEntity.Any(x => x.Description.ToLower() == currentItem.Description.ToLower() && x.Deleted == false)))
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

            else if (result.Any() && (currentItem.Id > 0 && validatingEntity.Any(x => x.Id != currentItem.Id && (x.Description.ToLower() == currentItem.Description.ToLower() && x.Deleted == false))))
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
            var result = validatingEntity.Where(x => x.Type != null);

            if (result.Any() &&
                ((currentItem.Id == 0 && validatingEntity.Any(x => x.Type.ToLower() == currentItem.Type.ToLower() && x.Deleted == false)) ||
                (currentItem.Id > 0 && validatingEntity.Any(x => x.Id != currentItem.Id && (x.Type.ToLower() == currentItem.Type.ToLower() && x.Deleted == false)))))
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

        public void ValidateItemByTypeCode(MetadataBase currentItem, List<MetadataBase> validatingEntity)
        {
            if(currentItem.Id>0 &&( validatingEntity.Where(x => x.Id == currentItem.Id).FirstOrDefault().TypeCode!= currentItem.TypeCode))
            {
                throw new ServiceException(new ErrorMessage[]
                {
                    new ErrorMessage()
                    {
                       Code = string.Empty,
                       Message = $"type code unauthorized to change"
                    }
                });
            }
           

            var result = validatingEntity.Where(x => x.TypeCode != null);

            if (result.Any() &&
                ((currentItem.Id == 0 && validatingEntity.Any(x => x.TypeCode.ToLower() == currentItem.TypeCode.ToLower() && x.Deleted == false)) ||
                (currentItem.Id > 0 && validatingEntity.Any(x => x.Id != currentItem.Id && (x.TypeCode.ToLower() == currentItem.TypeCode.ToLower() && x.Deleted == false)))))
            {
                throw new ServiceException(new ErrorMessage[]
                    {
                           new ErrorMessage()
                           {
                              Code = string.Empty,
                             Message = $"Exsiting type code found "
                           }
                    });
            }

        }

        public void ValidateItemByCode(MetadataBase currentItem, List<MetadataBase> validatingEntity)
        {
            if (currentItem.Id > 0 && (validatingEntity.Where(x => x.Id == currentItem.Id).FirstOrDefault().Code != currentItem.Code))
            {
                throw new ServiceException(new ErrorMessage[]
                {
                    new ErrorMessage()
                    {
                       Code = string.Empty,
                       Message = $"Code unauthorized to change"
                    }
                });
            }



            var result = validatingEntity.Where(x => x.Code != null);

            if (result.Any() &&
                ((currentItem.Id == 0 && validatingEntity.Any(x => x.Code.ToLower() == currentItem.Code.ToLower() && x.Deleted == false)) ||
                (currentItem.Id > 0 && validatingEntity.Any(x => x.Id != currentItem.Id && (x.Code.ToLower() == currentItem.Code.ToLower() && x.Deleted == false)))))
            {
                throw new ServiceException(new ErrorMessage[]
                    {
                           new ErrorMessage()
                           {
                              Code = string.Empty,
                             Message = $"Exsiting code found "
                           }
                    });
            }

        }
    }
}
