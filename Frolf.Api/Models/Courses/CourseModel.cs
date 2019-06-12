using Domain.Entities;
using Frolf.Api.Models.Contracts;
using Frolf.Api.Models.Holes;
using System;
using System.Collections.Generic;

namespace Frolf.Api.Models.Courses
{
    public class CourseModel : IApiDataModel
    {
        public Guid IdKey { get; set; }
        public Guid FrolfGroupId { get; set; }
        public string Name { get; set; }
        public int Par { get; set; }
        public int HoleCount { get; set; }
        // not respected mapping back to api (cannot be used to attach holes)
        public IEnumerable<Guid> HoleIds { get; set; }
        // not respected in mapping
        public IEnumerable<HoleModel> Holes { get; set; }
    }

    public class CourseFilterModel : IFilterModel
    {
        public Guid FrolfGroupId { get; set; }
    }
}