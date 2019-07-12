using Application.Command.FrolfGroups.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Application.Command.Tests.FrolfGroups.Commands
{
    [TestFixture]
    public class AddFrolfGroupCommandValidationTests
    {
        #region Setup

        AddFrolfGroupCommand command;
        AddFrolfGroupCommandValidation handler;

        Guid currentUserId;
        Player adminPlayer;
        FrolfGroup newFrolfGroup;

        Mock<IWorkUnit> mockWorkUnit;

        [SetUp]
        public void Setup()
        {
            SetupPlayer();
            SetupFrolfGroup();
            SetupWorkUnit();

            command = new AddFrolfGroupCommand { newEntity = newFrolfGroup };
            handler = new AddFrolfGroupCommandValidation(mockWorkUnit.Object);
        }

        private void SetupPlayer()
        {
            currentUserId = default(Guid);

            adminPlayer = new Player
            {
                AppUserId = currentUserId,
                GroupRole = GroupRole.Administrator
            };
        }

        private void SetupFrolfGroup()
        {
            newFrolfGroup = new FrolfGroup
            {
                Name = "dudes n' bros",

                Members = new List<Player>
                {
                    adminPlayer
                }
            };
        }

        private void SetupWorkUnit()
        {
            mockWorkUnit = new Mock<IWorkUnit>();
        }

        #endregion

        [Test]
        public void AddFrolfGroupCommandValidation_PreHandle_ExpectNoExceptions_WhenFrolfGroupValid()
        {
            UserExtensions.ImpersonateUser(currentUserId);

            Assert.DoesNotThrow( () => handler.PreHandle(command) );
        }

        [Test]
        public void AddFrolfGroupCommandValidation_PreHandle_ExpectException_WhenNameNull()
        {
            UserExtensions.ImpersonateUser(currentUserId);
            command.newEntity.Name = null;

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void AddFrolfGroupCommandValidation_PreHandle_ExpectException_WhenNameEmpty()
        {
            UserExtensions.ImpersonateUser(currentUserId);
            command.newEntity.Name = "";

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void AddFrolfGroupCommandValidation_PreHandle_ExpectException_WhenGroupHasInvites()
        {
            UserExtensions.ImpersonateUser(currentUserId);      
            command.newEntity.Invites = new List<FrolfGroupInvite> { new FrolfGroupInvite() };

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void AddFrolfGroupCommandValidation_PreHandle_ExpectException_WhenMemberRuleBroken()
        {
            UserExtensions.ImpersonateUser(currentUserId);

            // too many members
            command.newEntity.Members.Add(new Player
            {
                AppUserId = Guid.NewGuid(),
                GroupRole = GroupRole.Member
            });

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }
    }
}
