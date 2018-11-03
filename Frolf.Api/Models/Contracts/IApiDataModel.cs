using System;

namespace Frolf.Api.Models.Contracts
{
    public interface IApiDataModel
    {
        Guid IdKey { get; set; }
    }
}