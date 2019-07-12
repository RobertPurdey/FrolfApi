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
    public class UpdateFrolfGroupCommandValidationTests
    {
        #region Setup

        UpdateFrolfGroupCommand command;
        UpdateFrolfGroupCommandValidation handler;

        Guid currentUserId;
        Player adminPlayer;
        FrolfGroup frolfGroup;

        Mock<IWorkUnit> mockWorkUnit;

        [SetUp]
        public void Setup()
        {
            SetupPlayer();
            SetupFrolfGroup();
            SetupWorkUnit();

            command = new UpdateFrolfGroupCommand { entity = frolfGroup };
            handler = new UpdateFrolfGroupCommandValidation(mockWorkUnit.Object);
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
            frolfGroup = new FrolfGroup
            {
                Name      = "dudes n' bros",
                CreatedBy = currentUserId,

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
        public void UpdateFrolfGroupCommandValidation_PreHandle_ExpectNoExceptions_WhenFrolfGroupValid()
        {
            UserExtensions.ImpersonateUser(currentUserId);

            Assert.DoesNotThrow( () => handler.PreHandle(command) );
        }

        [Test]
        public void UpdateFrolfGroupCommandValidation_PreHandle_ExpectException_WhenNameNull()
        {
            UserExtensions.ImpersonateUser(currentUserId);
            command.entity.Name = null;

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void UpdateFrolfGroupCommandValidation_PreHandle_ExpectException_WhenNameEmpty()
        {
            UserExtensions.ImpersonateUser(currentUserId);
            command.entity.Name = "";

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void UpdateFrolfGroupCommandValidation_PreHandle_ExpectException_WhenMemberRuleBroken()
        {
            UserExtensions.ImpersonateUser(currentUserId);
            command.entity.CreatedBy = Guid.NewGuid();

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }
    }
}
