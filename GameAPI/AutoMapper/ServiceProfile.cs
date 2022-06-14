using AutoMapper;
using GameAPI.Dto;
using GameAPI.Models;

namespace GameAPI.AutoMapper
{
    public static class ServiceProfile
    {
        public static MapperConfiguration RegiserMaps()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<Game, GameDto>().ReverseMap();
            });

        }
    }
}
