using AutoMapper;
using DataTransferObjects;
using Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Mapping
{
    public static class ReservationMapping
    {
        #region Domain to DTO
        public static object MapDomainToDTOObject(object source)
        {
            MapperConfiguration config = null;
            if(source.GetType().ToString().Equals("Domain.Payment"))
            {
                config = new MapperConfiguration(cfg => {

                    cfg.CreateMap<Payment, PaymentDTO>()
                        .ForMember(dto => dto.BookingDTO, map => map.MapFrom(domain => domain.Booking))
                            .ForPath(dto => dto.BookingDTO.SpotDTO, map => map.MapFrom(domain => domain.Booking.Spot))
                            .ForPath(dto => dto.BookingDTO.UserDTO, map => map.MapFrom(domain => domain.Booking.User));
                    cfg.CreateMap<Booking, BookingDTO>();
                    cfg.CreateMap<Spot, SpotDTO>()
                        .ForMember(dto => dto.FreeSpots, map => map.MapFrom(domain => domain.free_count))
                        .ForMember(dto => dto.Latitude, map => map.MapFrom(domain => domain.latitude))
                        .ForMember(dto => dto.Longitude, map => map.MapFrom(domain => domain.longitude))
                        .ForMember(dto => dto.Name, map => map.MapFrom(domain => domain.name));
                    cfg.CreateMap<User, UserDTO>();
                });
                
                IMapper iMapper = config.CreateMapper();

                return iMapper.Map<object, PaymentDTO>(source);
            }
            else
            {
                config = new MapperConfiguration(cfg => {

                    cfg.CreateMap<Booking, BookingDTO>()
                            .ForMember(dto => dto.SpotDTO, map => map.MapFrom(domain => domain.Spot))
                            .ForMember(dto => dto.UserDTO, map => map.MapFrom(domain => domain.User));
                    cfg.CreateMap<Spot, SpotDTO>()
                        .ForMember(dto => dto.FreeSpots, map => map.MapFrom(domain => domain.free_count))
                        .ForMember(dto => dto.Latitude, map => map.MapFrom(domain => domain.latitude))
                        .ForMember(dto => dto.Longitude, map => map.MapFrom(domain => domain.longitude))
                        .ForMember(dto => dto.Name, map => map.MapFrom(domain => domain.name));
                    cfg.CreateMap<User, UserDTO>();
                });

                IMapper iMapper = config.CreateMapper();

                return iMapper.Map<object, BookingDTO>(source);
            }
        }

        public static IEnumerable<object> MapDomainToDTOCollection(IEnumerable<object> source)
        {
            foreach (var item in source)
            {
                yield return MapDomainToDTOObject(item);
            }
        }

        #endregion

        #region DTO to Domain
        public static object MapDTOToDomainObject(object source)
        {
            MapperConfiguration config = null;
            var jObject = JObject.Parse(source.ToString());
            if (jObject["Price"] != null)
            {
                config = new MapperConfiguration(cfg => {

                    cfg.CreateMap<PaymentDTO, Payment>()
                        .ForMember(domain => domain.Booking, map => map.MapFrom(dto => dto.BookingDTO))
                            .ForPath(domain => domain.Booking.Spot, map => map.MapFrom(dto => dto.BookingDTO.SpotDTO))
                            .ForPath(domain => domain.Booking.User, map => map.MapFrom(dto => dto.BookingDTO.UserDTO));
                    cfg.CreateMap<BookingDTO, Booking>();
                    cfg.CreateMap<SpotDTO, Spot>()
                        .ForMember(domain => domain.free_count, map => map.MapFrom(dto => dto.FreeSpots))
                        .ForMember(domain => domain.latitude, map => map.MapFrom(dto => dto.Latitude))
                        .ForMember(domain => domain.longitude, map => map.MapFrom(dto => dto.Longitude))
                        .ForMember(domain => domain.name, map => map.MapFrom(dto => dto.Name));
                    cfg.CreateMap<UserDTO, User>();
                });


                IMapper iMapper = config.CreateMapper();

                return iMapper.Map<PaymentDTO, Payment>(JsonConvert.DeserializeObject<PaymentDTO>(source.ToString()));
            }
            else
            {
                config = new MapperConfiguration(cfg => {

                    cfg.CreateMap<BookingDTO, Booking>()
                        .ForMember(domain => domain.Spot, map => map.MapFrom(dto => dto.SpotDTO))
                        .ForMember(domain => domain.User, map => map.MapFrom(dto => dto.UserDTO));
                    cfg.CreateMap<SpotDTO, Spot>()
                        .ForMember(domain => domain.free_count, map => map.MapFrom(dto => dto.FreeSpots))
                        .ForMember(domain => domain.latitude, map => map.MapFrom(dto => dto.Latitude))
                        .ForMember(domain => domain.longitude, map => map.MapFrom(dto => dto.Longitude))
                        .ForMember(domain => domain.name, map => map.MapFrom(dto => dto.Name));
                    cfg.CreateMap<UserDTO, User>();

                });


                IMapper iMapper = config.CreateMapper();

                return iMapper.Map<BookingDTO, Booking>(JsonConvert.DeserializeObject<BookingDTO>(source.ToString()));
            }
        }

        public static IEnumerable<object> MapDTOToDomainCollection(IEnumerable<object> source)
        {
            foreach (var item in source)
            {
                yield return MapDTOToDomainObject(item);
            }
        }
        #endregion
    }
}
