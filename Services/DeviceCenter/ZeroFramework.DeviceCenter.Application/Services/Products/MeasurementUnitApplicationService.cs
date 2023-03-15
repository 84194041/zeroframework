﻿using AutoMapper;
using ZeroFramework.DeviceCenter.Application.Models.Products;
using ZeroFramework.DeviceCenter.Application.Services.Generics;
using ZeroFramework.DeviceCenter.Domain.Aggregates.ProductAggregate;
using ZeroFramework.DeviceCenter.Domain.Repositories;

namespace ZeroFramework.DeviceCenter.Application.Services.Products
{
    public class MeasurementUnitApplicationService : CrudApplicationService<MeasurementUnit, int, MeasurementUnitGetResponseModel, MeasurementUnitPagedRequestModel, MeasurementUnitGetResponseModel, MeasurementUnitCreateRequestModel, MeasurementUnitUpdateRequestModel>, IMeasurementUnitApplicationService
    {
        public MeasurementUnitApplicationService(IRepository<MeasurementUnit, int> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override IQueryable<MeasurementUnit> CreateFilteredQuery(MeasurementUnitPagedRequestModel requestModel)
        {
            if (requestModel.Keyword is not null && !string.IsNullOrWhiteSpace(requestModel.Keyword))
            {
                return Repository.Query.Where(e => e.Unit.Contains(requestModel.Keyword) || e.UnitName.Contains(requestModel.Keyword));
            }

            return base.CreateFilteredQuery(requestModel);
        }
    }
}