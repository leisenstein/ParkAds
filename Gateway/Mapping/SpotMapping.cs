using AutoMapper;
using DataTransferObjects;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Mapping
{
    public static class SpotMapping
    {
        #region Domain to DTO

        public static SpotDTO MapDomainToDTOObject(Spot source)
        {
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<Spot, SpotDTO>()
                    .ForMember(dto => dto.Name, map => map.MapFrom(domain => domain.name))
                    .ForMember(dto => dto.Longitude, map => map.MapFrom(domain => domain.longitude))
                    .ForMember(dto => dto.Latitude, map => map.MapFrom(domain => domain.latitude))
                    .ForMember(dto => dto.FreeSpots, map => map.MapFrom(domain => domain.free_count));
            });

            IMapper iMapper = config.CreateMapper();

            return iMapper.Map<Spot, SpotDTO>(source);
        }

        public static IEnumerable<SpotDTO> MapDomainToDTOCollection(IEnumerable<Spot> source)
        {
            foreach (var item in source)
            {
                yield return MapDomainToDTOObject(item);
            }
        }

        #endregion
    }
}
