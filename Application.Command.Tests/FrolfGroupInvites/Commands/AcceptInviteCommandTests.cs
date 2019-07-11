using Application.Command.FrolfGroupInvites.Commands;
using Domain.Entities;
using NUnit.Framework;
using System;

namespace Application.Command.Tests.FrolfGroupInvites.Commands
{
    [TestFixture]
    public class AcceptInviteCommandTests
    {
        #region Setup
        
        FrolfGroupInvite invite;
        AcceptInviteCommand command;

        [SetUp]
        public void Setup()
        {
            SetupInvite();
        }

        private void SetupInvite()
        {
            invite = new FrolfGroupInvite
            {
                Invitee    = new AppUser(),
                Inviter    = new AppUser(),
                FrolfGroup = new FrolfGroup()
            };
        }

        #endregion

        [Test]
        public void AcceptInviteCommand_Constructor_ExpectNoException_WhenNoNullObjects()
        {
            Assert.DoesNotThrow( () => command = new AcceptInviteCommand(invite) );
        }

        [Test]
        public void AcceptInviteCommand_Constructor_ExpectException_WhenInviteNull()
        {
            invite = null;

            Assert.Throws<ArgumentNullException>( () => command = new AcceptInviteCommand(invite) );
        }

        [Test]
        public void AcceptInviteCommand_Constructor_ExpectException_WhenInviteeNull()
        {
            invite.Invitee = null;

            Assert.Throws<ArgumentNullException>( () => command = new AcceptInviteCommand(invite) );
        }

        [Test]
        public void AcceptInviteCommand_Constructor_ExpectException_WhenInviterNull()
        {
            invite.Inviter = null;

            Assert.Throws<ArgumentNullException>( () => command = new AcceptInviteCommand(invite) );
        }

        [Test]
        public void AcceptInviteCommand_Constructor_ExpectException_WhenFrolfGroupNull()
        {
            invite.FrolfGroup = null;

            Assert.Throws<ArgumentNullException>( () => command = new AcceptInviteCommand(invite) );
        }
    }
}
