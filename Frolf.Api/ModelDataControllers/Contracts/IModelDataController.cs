using Frolf.Api.Models.Contracts;
using System;
using System.Collections.Generic;

namespace Frolf.Api.ModelDataControllers.Contracts
{
    public interface IModelDataController<TApiModel, in TFilterModel>
        where TApiModel    : class, IApiDataModel
        where TFilterModel : class, IFilterModel
    {
        TApiModel GetById(Guid id);
        IEnumerable<TApiModel> GetAll();
        IEnumerable<TApiModel> GetWithFilter(TFilterModel filter);
        void Insert(TApiModel newModel);
        void Update(TApiModel modelToUpdate);
        void Delete(TApiModel modelToDelete);
    }
}