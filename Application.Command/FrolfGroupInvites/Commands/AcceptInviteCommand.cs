using Domain.Commands.Contracts;
using Domain.Entities;

namespace Application.Command.FrolfGroupInvites.Commands
{
    public class AcceptInviteCommand : ICommand
    {
        public FrolfGroupInvite Invite { get; set; }
    }
}
