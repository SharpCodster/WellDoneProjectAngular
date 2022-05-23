using Ardalis.Specification;
using WellDoneProjectAngular.Core.Interfaces.Entities;
using WellDoneProjectAngular.Core.Models;

namespace WellDoneProjectAngular.Core.Interfaces.Services
{
    public interface IBaseService<TEntity, TDtoIn, TDtoOut>
       where TEntity : BaseEntity
       where TDtoIn : IIdentified
       where TDtoOut : IIdentified
    {
        Task<IEnumerable<TDtoOut>> ListAllAsync();
        Task<IEnumerable<TDtoOut>> ListAsync(ISpecification<TEntity> spec);
        Task<int> CountAsync(ISpecification<TEntity> spec);
        Task<TDtoOut> GetByIdAsync(int id);
        Task<TDtoOut> CreateAsync(TDtoIn dtoIn);
        Task<TDtoOut> UpdateAsync(TDtoIn tag);
        Task<TDtoOut> PatchAsync(int id, Dictionary<string, object> valuesToPatch);
        Task DeleteAsync(long id);
    }
}
