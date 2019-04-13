using Frolf.Api.Models.Contracts;
using System;

namespace Frolf.Api.Models.FrolfGroups
{
    public class FrolfGroupModel : IApiDataModel
    {
        public Guid IdKey { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class FrolfGroupFilterModel : IFilterModel
    {

    }
}