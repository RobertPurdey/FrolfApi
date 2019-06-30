using System;
using Frolf.Api.Models.Contracts;

namespace Frolf.Api.Models
{
    public class IdModel : IApiDataModel
    {
        public Guid IdKey { get; set; }
    }
}