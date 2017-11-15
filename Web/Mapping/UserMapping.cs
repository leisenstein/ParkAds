using AutoMapper;
using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Mapping
{
    public static class UserMapping<TEntity> where TEntity : class
    {
        #region ViewModel to DTO
        public static UserDTO MapViewModelDTOObject(TEntity source)
        {
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<TEntity, UserDTO>();
            });

            IMapper iMapper = config.CreateMapper();

            return iMapper.Map<TEntity, UserDTO>(source);
        }

        public static IEnumerable<UserDTO> MapViewModelToDTOCollection(IEnumerable<TEntity> source)
        {
            foreach (var item in source)
            {
                yield return MapViewModelDTOObject(item);
            }
        }

        #endregion

        #region Map DTO to ViewModel
        public static TEntity MapDTOToViewModelObject(UserDTO source)
        {
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<UserDTO, TEntity>();
            });

            IMapper iMapper = config.CreateMapper();

            return iMapper.Map<UserDTO, TEntity>(source);
        }

        public static IEnumerable<TEntity> MapDTOToViewModelCollection(IEnumerable<UserDTO> source)
        {
            foreach (var item in source)
            {
                yield return MapDTOToViewModelObject(item);
            }
        }
        #endregion
    }
}
