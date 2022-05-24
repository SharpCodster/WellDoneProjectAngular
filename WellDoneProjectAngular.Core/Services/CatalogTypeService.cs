using Ardalis.Specification;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellDoneProjectAngular.Core.Dtos;
using WellDoneProjectAngular.Core.Interfaces;
using WellDoneProjectAngular.Core.Interfaces.Data;
using WellDoneProjectAngular.Core.Interfaces.Services;
using WellDoneProjectAngular.Core.Models;

namespace WellDoneProjectAngular.Core.Services
{
    public class CatalogTypeService : AuditableBaseService<CatalogType, CatalogTypeDto, CatalogTypeDto>, ICatalogTypeService
    {

        public CatalogTypeService(IMapper mapper,
            IUnitOfWork unitOfWork,
            IRepository<CatalogType> repository,
            IRequestContextProvider requestContextProvider) 
                : base(mapper, unitOfWork, repository, requestContextProvider)
        {

        }



    }
}
