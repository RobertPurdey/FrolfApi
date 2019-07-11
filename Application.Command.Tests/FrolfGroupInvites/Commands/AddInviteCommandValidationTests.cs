using Application.Command.FrolfGroupInvites.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Command.Tests.FrolfGroupInvites.Commands
{
    [TestFixture]
    public class AddInviteCommandValidationTests
    { 
        #region Setup

        AddInviteCommand command;
        AddInviteCommandValidation handler;

        Guid inviteeUserId;
        Guid inviterUserId;
        Guid frolfGroupId;

        FrolfGroupInvite invite;
        FrolfGroup frolfGroup;

        List<FrolfGroupInvite> inviteDataStore;
        List<FrolfGroup> frolfGroupDataStore;

        Mock<IQueryService<FrolfGroupInvite>> mockFrolfGroupInviteQuery;
        Mock<IQueryService<FrolfGroup>> mockFrolfGroupQuery;

        Mock<IWorkUnit> mockWorkUnit;

        [SetUp]
        public void Setup()
        {
            SetupFrolfGroup();
            SetupFrolfGroupDataStore();
            SetupMockFrolfGroupQuery();

            SetupFrolfGroupInvite();
            SetupFrolfGroupInviteDataStore();
            SetupMockFrolfGroupInviteQuery();

            SetupMockWorkUnit();

            command = new AddInviteCommand(invite);
            handler = new AddInviteCommandValidation(
                mockWorkUnit.Object,
                mockFrolfGroupInviteQuery.Object,
                mockFrolfGroupQuery.Object);
        }

        private void SetupMockWorkUnit()
        {
            mockWorkUnit = new Mock<IWorkUnit>();
        }

        private void SetupFrolfGroup()
        {
            frolfGroupId  = Guid.NewGuid();
            inviterUserId = default(Guid);

            frolfGroup = new FrolfGroup
            {
                EntityKey = frolfGroupId,                         

                Members = new List<Player>
                {
                    new Player { AppUserId = inviterUserId, GroupRole = GroupRole.Administrator }
                }
            };
        }

        private void SetupFrolfGroupDataStore()
        {
            frolfGroupDataStore = new List<FrolfGroup>
            {
                frolfGroup
            };
        }

        private void SetupMockFrolfGroupQuery()
        {
            mockFrolfGroupQuery = new Mock<IQueryService<FrolfGroup>>();

            mockFrolfGroupQuery.Setup(m => m.GetAll()).Returns(frolfGroupDataStore.AsQueryable);
        }

        private void SetupFrolfGroupInvite()
        {
            inviteeUserId = Guid.NewGuid();

            invite = new FrolfGroupInvite
            {
                InviterId     = inviterUserId,
                InviteeId     = inviteeUserId,
                FrolfGroupId  = frolfGroupId,               
            };
        }

        private void SetupFrolfGroupInviteDataStore()
        {
            inviteDataStore = new List<FrolfGroupInvite>
            {
            };
        }

        private void SetupMockFrolfGroupInviteQuery()
        {
            mockFrolfGroupInviteQuery = new Mock<IQueryService<FrolfGroupInvite>>();

            mockFrolfGroupInviteQuery.Setup(m => m.GetAll()).Returns(inviteDataStore.AsQueryable);
        }

        #endregion

        [Test]
        public void AddInviteCommandValidation_PreHandle_ExpectNoException_WhenAllRulesPass()
        {
            UserExtensions.ImpersonateUser(inviterUserId);

            Assert.DoesNotThrow( () => handler.PreHandle(command) );
        }

        [Test]
        public void AddInviteCommandValidation_PreHandle_ExpectException_WhenInviterNotCurrentUser()
        {
            command.Invite.InviterId = Guid.NewGuid();
            UserExtensions.ImpersonateUser(inviterUserId);

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void AddInviteCommandValidation_PreHandle_ExpectException_WhenInviteAlreadySent()
        {
            UserExtensions.ImpersonateUser(inviterUserId);
            inviteDataStore.Add(invite);

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void AddInviteCommandValidation_PreHandle_ExpectException_WhenFrolfGroupDoesNotExist()
        {
            UserExtensions.ImpersonateUser(inviterUserId);
            frolfGroupDataStore.Clear();

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void AddInviteCommandValidation_PreHandle_ExpectException_WhenInviterNotAdminOfGroup()
        {
            UserExtensions.ImpersonateUser(inviterUserId);

            frolfGroup.Members.Clear();
            frolfGroup.Members.Add(new Player
            {
                AppUserId = inviterUserId,
                GroupRole = GroupRole.Member
            });

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }

        [Test]
        public void AddInviteCommandValidation_PreHandle_ExpectException_WhenInviteeInGroup()
        {
            UserExtensions.ImpersonateUser(inviterUserId);

            frolfGroup.Members.Add(new Player { AppUserId = inviteeUserId });

            Assert.Throws<Exception>( () => handler.PreHandle(command) );
        }
    }
}
