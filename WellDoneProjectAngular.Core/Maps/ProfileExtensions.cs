using AutoMapper;
using WellDoneProjectAngular.Core.Dtos;
using WellDoneProjectAngular.Core.Models;

namespace WellDoneProjectAngular.Core.Maps
{
    public static class ProfileExtensionspublic
    {
        public static IMappingExpression<TSource, TDest> ForBaseDtoToEntity<TSource, TDest>(this IMappingExpression<TSource, TDest> expression, bool includeConversions = true)
            where TSource : class
            where TDest : BaseAuditableEntity
        {
            expression = expression
                        .ForMember(dest => dest.CreatedAtUtc, opt => opt.Ignore())
                        .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.UpdatedAtUtc, opt => opt.Ignore())
                        .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                        .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            return expression;
        }

        public static IMappingExpression<TSource, TDest> ForBaseEntityToDto<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
            where TSource : BaseAuditableEntity
            where TDest : BaseAuditableDto
        {
            expression = expression
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Id))
                        .ForMember(dest => dest.CreatedAtUtc, opt => opt.MapFrom(source => source.CreatedAtUtc))
                        .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(source => source.CreatedBy))
                        .ForMember(dest => dest.UpdatedAtUtc, opt => opt.MapFrom(source => source.UpdatedAtUtc))
                        .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(source => source.UpdatedBy))
                        .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(source => source.IsDeleted))
                        ;

            return expression;
        }
    }
}
