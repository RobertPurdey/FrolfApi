using Frolf.Api.Models.Contracts;
using System;
using System.Collections.Generic;

namespace Frolf.Api.Models.Games
{
    public class GameModel : IApiDataModel
    {
        public Guid IdKey { get; set; }
        public Guid GroupId { get; set; }
        public Guid CourseId { get; set; }
        public Guid CreatorId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Name { get; set; }
        public string CourseName { get; set; }

        public IEnumerable<Guid> RoundIds { get; set; }
        public IEnumerable<Guid> PlayerIds { get; set; }
        public IEnumerable<Guid> HoleIds { get; set; }
        public IDictionary<int,int> HolePars { get; set; }
    }

    public class GameFilterModel : IFilterModel
    {

    }
}