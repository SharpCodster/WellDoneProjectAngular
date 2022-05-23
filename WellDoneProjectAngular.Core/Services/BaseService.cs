using Ardalis.Specification;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellDoneProjectAngular.Core.Extensions;
using WellDoneProjectAngular.Core.Interfaces.Data;
using WellDoneProjectAngular.Core.Interfaces.Entities;
using WellDoneProjectAngular.Core.Interfaces.Services;
using WellDoneProjectAngular.Core.Models;

namespace WellDoneProjectAngular.Core.Services
{
    public class BaseService<TEntity, TDtoIn, TDtoOut> : IBaseService<TEntity, TDtoIn, TDtoOut>
        where TEntity : BaseEntity
        where TDtoIn : IIdentified
        where TDtoOut : IIdentified
    {
        protected readonly IMapper _mapper;
        protected readonly IRepository<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork;

        public BaseService(IMapper mapper,
            IUnitOfWork unitOfWork,
            IRepository<TEntity> repository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public virtual async Task<IEnumerable<TDtoOut>> ListAllAsync()
        {
            IEnumerable<TEntity> entities = await _repository.ListAsync().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<TDtoOut>>(entities);
        }

        public virtual async Task<IEnumerable<TDtoOut>> ListAsync(ISpecification<TEntity> spec)
        {
            IEnumerable<TEntity> entities = await _repository.ListAsync(spec).ConfigureAwait(false);
            return _mapper.Map<IEnumerable<TDtoOut>>(entities);
        }

        public virtual async Task<int> CountAsync(ISpecification<TEntity> spec)
        {
            int entities = await _repository.CountAsync(spec).ConfigureAwait(false);
            return entities;
        }

        public virtual async Task<TDtoOut> GetByIdAsync(int id)
        {
            TEntity entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TDtoOut>(entity);
        }

        /////// CREATE /////////////////////////////////////////////////
        public virtual async Task<TDtoOut> CreateAsync(TDtoIn dtoIn)
        {
            using (IUnitOfWorkScope scope = _unitOfWork.BeginScope())
            {
                await ValidateForCreateOrThrowAsync(dtoIn);

                await TransformDtoForCreateAsync(dtoIn);

                TEntity entity = _mapper.Map<TEntity>(dtoIn);

                await BeforeCreateAsync(dtoIn, entity);

                await ApplyAuditInfoForCreateAsync(entity);

                TEntity result = await _repository.AddAsync(entity);
                TDtoOut resultDto = _mapper.Map<TDtoOut>(result);

                await AfterCreateAsync(resultDto, result);

                scope.Commit();
                return resultDto;
            }
        }

        protected virtual async Task ValidateForCreateOrThrowAsync(TDtoIn dto)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task TransformDtoForCreateAsync(TDtoIn dto)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task BeforeCreateAsync(TDtoIn dtoIn, TEntity entity)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task AfterCreateAsync(TDtoOut dtoOut, TEntity result)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task ApplyAuditInfoForCreateAsync(TEntity entity)
        {
            await Task.CompletedTask;
        }

        /////// UPDATE /////////////////////////////////////////////////
        public virtual async Task<TDtoOut> UpdateAsync(TDtoIn dto)
        {
            using (IUnitOfWorkScope scope = _unitOfWork.BeginScope())
            {
                TEntity entity = await FindForUpdateOrThrowAsync(dto);
                TDtoOut dtoResult = await UpdateCoreAsync(dto, entity);
                scope.Commit();
                return dtoResult;
            }
        }

        protected virtual async Task<TDtoOut> UpdateCoreAsync(TDtoIn dto, TEntity entity)
        {
            await ValidateForUpdateOrThrowAsync(dto, entity);

            await TransformDtoForUpdateAsync(dto, entity);

            // automapper sometimes returns the same entity reference! ref: https://github.com/AutoMapper/AutoMapper/issues/1962
            // ? mapper.Map<TEntity>(entity)
            TEntity originalEntity = entity.JsonClone();

            _mapper.Map(dto, entity);

            await BeforeUpdateAsync(dto, originalEntity, entity);

            await ApplyAuditInfoForUpdateAsync(entity);

            await _repository.UpdateAsync(entity);

            TEntity result = await _repository.GetByIdAsync(entity.Id);
            TDtoOut dtoResult = _mapper.Map<TDtoOut>(result);

            await AfterUpdateAsync(dto, originalEntity, entity, result, dtoResult);
            return dtoResult;
        }

        protected virtual Task<TEntity> FindForUpdateOrThrowAsync(TDtoIn dto)
        {
            return _repository.GetByIdAsync(dto.Id);
        }

        protected virtual async Task TransformDtoForUpdateAsync(TDtoIn dto, TEntity entity)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task ValidateForUpdateOrThrowAsync(TDtoIn dto, TEntity entity)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task BeforeUpdateAsync(TDtoIn dto, TEntity originalEntity, TEntity entity)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task AfterUpdateAsync(TDtoIn dto, TEntity originalEntity, TEntity entity, TEntity result, TDtoOut dtoResult)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task ApplyAuditInfoForUpdateAsync(TEntity entity)
        {
            await Task.CompletedTask;
        }

        /////// PATCH /////////////////////////////////////////////////
        public virtual async Task<TDtoOut> PatchAsync(int id, Dictionary<string, object> valuesToPatch)
        {
            using (IUnitOfWorkScope scope = _unitOfWork.BeginScope())
            {
                TEntity entity = await _repository.GetByIdAsync(id);
                TDtoIn dto = _mapper.Map<TDtoIn>(entity);
                JsonConvert.PopulateObject(valuesToPatch.ToJson(), dto);
                TDtoOut result = await UpdateCoreAsync(dto, entity);
                scope.Commit();
                return result;
            }
        }

        /////// DELETE /////////////////////////////////////////////////
        public virtual async Task DeleteAsync(long id)
        {
            using (IUnitOfWorkScope scope = _unitOfWork.BeginScope())
            {
                TEntity entity = await _repository.GetByIdAsync(id);

                await ValidateForDeleteOrThrowAsync(entity);
                await BeforeDeleteAsync(entity);

                await ApplyAuditForDeleteAsync(entity);

                await _repository.DeleteAsync(entity);

                await AfterDeleteAsync(entity);

                scope.Commit();
            }
        }

        protected virtual async Task ValidateForDeleteOrThrowAsync(TEntity entity)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task BeforeDeleteAsync(TEntity entity)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task AfterDeleteAsync(TEntity result)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task ApplyAuditForDeleteAsync(TEntity entity)
        {
            await Task.CompletedTask;
        }
    }
}
