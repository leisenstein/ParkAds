using AutoMapper;
using DataTransferObjects;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Mapping
{
    public static class AdMapping
    {
        #region Domain to DTO

        public static AdDTO MapDomainToDTOObject(Ad source)
        {
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<Ad, AdDTO>()
                    .ForMember(dto => dto.AdEncoding, map => map.MapFrom(domain => domain.ImageData));
            });

            IMapper iMapper = config.CreateMapper();

            return iMapper.Map<Ad, AdDTO>(source);
        }

        public static IEnumerable<AdDTO> MapDomainToDTOCollection(IEnumerable<Ad> source)
        {
            foreach (var item in source)
            {
                yield return MapDomainToDTOObject(item);
            }
        }

        #endregion
    }
}
