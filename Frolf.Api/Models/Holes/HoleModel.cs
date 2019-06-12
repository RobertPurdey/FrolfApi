using Frolf.Api.Models.Contracts;
using System;

namespace Frolf.Api.Models.Holes
{
    public class HoleModel : IApiDataModel
    {
        public Guid IdKey { get; set; }
        public Guid CourseId { get; set; }
        public int Par { get; set; }
        public int Order { get; set; }
    }
}