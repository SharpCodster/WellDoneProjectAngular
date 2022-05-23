using Ardalis.Specification;
using WellDoneProjectAngular.Core.Models;

namespace WellDoneProjectAngular.Core.Interfaces.Data
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : BaseEntity
    {
        
    }

    public interface IRepository<T> : IRepositoryBase<T> where T : BaseEntity
    {

    }
}
