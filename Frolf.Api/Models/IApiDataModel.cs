using System;

namespace Frolf.Api.Models
{
    public interface IApiDataModel
    {
        Guid IdKey { get; set; }
    }
}