using AutoMapper;
using WellDoneProjectAngular.Core.Dtos;
using WellDoneProjectAngular.Core.Models;

namespace WellDoneProjectAngular.Core.Maps
{
    public class CatalogTypeProfile : Profile
    {
        public CatalogTypeProfile()
        {
            CreateMap<CatalogType, CatalogTypeDto>()
                .ForBaseEntityToDto()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(source => source.Type))
            .ReverseMap();

            CreateMap<CatalogTypeDto, CatalogType>()
                .ForBaseDtoToEntity()
                ;
        }
    }
}
