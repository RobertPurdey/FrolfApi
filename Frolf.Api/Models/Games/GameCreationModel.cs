using System;
using System.Collections.Generic;

namespace Frolf.Api.Models.Games
{
    public class GameCreationModel
    {
        public Guid IdKey { get; set; }
        public Guid GroupId { get; set; }
        public Guid CourseId { get; set; }

        public string Name { get; set; }

        public IEnumerable<Guid> PlayerIds { get; set; }
    }
}