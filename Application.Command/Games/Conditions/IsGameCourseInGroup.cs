using Domain.Commands;
using Domain.Entities;
using System;

namespace Application.Command.Games.Conditions
{
    /// <summary>
    /// Determines if the course referenced by a game is part of the group the game is referencing.
    /// </summary>
    public class IsGameCourseInGroup : Condition<Game>
    {
        public override bool Validate(Game entity)
        {
            return entity.Course.FrolfGroupId == entity.FrolfGroupId;
        }
    }
}
