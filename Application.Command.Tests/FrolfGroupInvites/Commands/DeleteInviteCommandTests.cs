using Application.Command.FrolfGroupInvites.Commands;
using Domain.Entities;
using NUnit.Framework;
using System;

namespace Application.Command.Tests.FrolfGroupInvites.Commands
{
    [TestFixture]
    public class DeleteInviteCommandTests
    {
        #region Setup
        
        FrolfGroupInvite invite;
        DeleteInviteCommand command;

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
        public void DeleteInviteCommand_Constructor_ExpectNoException_WhenNoNullObjects()
        {
            Assert.DoesNotThrow( () => command = new DeleteInviteCommand(invite) );
        }

        [Test]
        public void DeleteInviteCommand_Constructor_ExpectException_WhenInviteNull()
        {
            invite = null;

            Assert.Throws<ArgumentNullException>( () => command = new DeleteInviteCommand(invite) );
        }

        [Test]
        public void DeleteInviteCommand_Constructor_ExpectException_WhenInviteeNull()
        {
            invite.Invitee = null;

            Assert.Throws<ArgumentNullException>( () => command = new DeleteInviteCommand(invite) );
        }

        [Test]
        public void DeleteInviteCommand_Constructor_ExpectException_WhenInviterNull()
        {
            invite.Inviter = null;

            Assert.Throws<ArgumentNullException>( () => command = new DeleteInviteCommand(invite) );
        }

        [Test]
        public void DeleteInviteCommand_Constructor_ExpectException_WhenFrolfGroupNull()
        {
            invite.FrolfGroup = null;

            Assert.Throws<ArgumentNullException>( () => command = new DeleteInviteCommand(invite) );
        }
    }
}
