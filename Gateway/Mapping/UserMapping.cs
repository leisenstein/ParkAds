using AutoMapper;
using DataTransferObjects;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Mapping
{
    public static class UserMapping
    {
        #region Domain to DTO
        public static UserDTO MapDomainToDTOObject(User source)
        {
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<User, UserDTO>();
            });

            IMapper iMapper = config.CreateMapper();

            return iMapper.Map<User, UserDTO>(source);
        }

        public static IEnumerable<UserDTO> MapDomainToDTOCollection(IEnumerable<User> source)
        {
            foreach (var item in source)
            {
                yield return MapDomainToDTOObject(item);
            }
        }

        #endregion

        #region Map DTO to Domain
        public static User MapDTOToDomainObject(UserDTO source)
        {
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<UserDTO, User>();
            });

            IMapper iMapper = config.CreateMapper();

            return iMapper.Map<UserDTO, User>(source);
        }

        public static IEnumerable<User> MapDTOToDomainCollection(IEnumerable<UserDTO> source)
        {
            foreach (var item in source)
            {
                yield return MapDTOToDomainObject(item);
            }
        }
        #endregion
    }
}
