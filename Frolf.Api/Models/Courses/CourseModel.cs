using Frolf.Api.Models.Contracts;
using System;

namespace Frolf.Api.Models.Courses
{
    public class CourseModel : IApiDataModel
    {
        public Guid IdKey { get; set; }
        public string Name { get; set; }
        public int Par { get; set; }
        public int HoleCount { get; set; }

        // todo: give back hole ids? 
    }

    public class CourseFilterModel : IFilterModel
    {

    }
}