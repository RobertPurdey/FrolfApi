using Frolf.Api.Models;
using System;
using System.Collections.Generic;

namespace Frolf.Api.ModelDataControllers.Contracts
{
    public interface IModelDataController<TApiModel>
        where TApiModel : class, IApiDataModel
    {
        TApiModel GetById(Guid id);
        IEnumerable<TApiModel> GetAll();
        //IEnumerable<TApiModel> Filter(TFilterModel filter);
        //PageResult<TApiModel> Filter(HttpRequestMessage request, ODataQueryOptions<TApiModel> options, TFilterModel filter);
        void Insert(TApiModel newModel);
        void Update(TApiModel modelToUpdate);
        void Delete(TApiModel modelToDelete);
    }
}