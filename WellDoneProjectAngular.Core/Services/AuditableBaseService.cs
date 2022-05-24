using Ardalis.Specification;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellDoneProjectAngular.Core.Interfaces;
using WellDoneProjectAngular.Core.Interfaces.Data;
using WellDoneProjectAngular.Core.Interfaces.Entities;
using WellDoneProjectAngular.Core.Interfaces.Services;
using WellDoneProjectAngular.Core.Models;

namespace WellDoneProjectAngular.Core.Services
{
    public abstract class AuditableBaseService<TEntity, TDtoIn, TDtoOut> : BaseService<TEntity, TDtoIn, TDtoOut>
        where TEntity : BaseAuditableEntity
        where TDtoIn : IAuditableUtc
        where TDtoOut : IAuditableUtc
    {
        protected IRequestContextProvider _requestContextProvider;

        public AuditableBaseService(IMapper mapper,
            IUnitOfWork unitOfWork,
            IRepository<TEntity> repository,
            IRequestContextProvider requestContextProvider) 
                : base(mapper, unitOfWork, repository)
        {
            _requestContextProvider = requestContextProvider;
        }


        public override async Task DeleteAsync(long id)
        {
            using (IUnitOfWorkScope scope = _unitOfWork.BeginScope())
            {
                TEntity entity = await _repository.GetByIdAsync(id);
                TDtoIn dto = _mapper.Map<TDtoIn>(entity);

                await ValidateForDeleteOrThrowAsync(entity);
                await BeforeDeleteAsync(entity);
                await ApplyAuditForDeleteAsync(entity);

                TDtoOut result = await UpdateCoreAsync(dto, entity);

                await AfterDeleteAsync(entity);

                scope.Commit();
            }
        }

        protected async override Task ApplyAuditInfoForCreateAsync(TEntity entity)
        {
            RequestContext requestContext = await _requestContextProvider.GetRequestContexAsync();

            entity.CreatedAtUtc = entity.UpdatedAtUtc = DateTime.Now;
            entity.CreatedBy = entity.UpdatedBy = requestContext?.User?.UserName ?? "NA";
            entity.IsDeleted = false;

            await base.ApplyAuditInfoForCreateAsync(entity);
        }

        protected override async Task ApplyAuditInfoForUpdateAsync(TEntity entity)
        {
            RequestContext requestContext = await _requestContextProvider.GetRequestContexAsync();

            entity.UpdatedAtUtc = DateTime.Now;
            entity.UpdatedBy = requestContext?.User?.UserName ?? "NA";

            await base.ApplyAuditInfoForUpdateAsync(entity);
        }

        protected override async Task ApplyAuditForDeleteAsync(TEntity entity)
        {
            entity.IsDeleted = true;
            await base.ApplyAuditInfoForUpdateAsync(entity);
        }
    }
}
