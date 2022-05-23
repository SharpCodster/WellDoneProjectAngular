using Ardalis.Specification.EntityFrameworkCore;
using WellDoneProjectAngular.Core.Interfaces.Data;
using WellDoneProjectAngular.Core.Models;
using WellDoneProjectAngular.Infrastructure.Data;

namespace WellDoneProjectAngular.Infrastructure.Repositories
{
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : BaseEntity
    {
        public EfRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
