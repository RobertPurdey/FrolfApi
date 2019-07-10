using Application.Command.AppUsers.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Command.Tests.AppUsers.Commands
{
    [TestFixture]
    public class UpdateAppUserCommandHandlerTests
    {
        #region Setup

        UpdateAppUserCommand command;
        UpdateAppUserCommandHandler handler;

        Guid appUserAId;
        AppUser appUserA;

        Player playerA1;
        Player playerA2;
        List<Player> playerDataStore;

        Mock<IQueryService<Player>> mockPlayerQuery;

        Mock<IRepository<AppUser>> mockAppUserRepo;
        Mock<IRepository<Player>> mockPlayerRepo;

        Mock<IWorkUnit> mockWorkUnit;     
        
        [SetUp]
        public void Setup()
        {
            SetupUser();
            SetupPlayerDataStore();
            SetupMockPlayerQuery();
            SetupWorkUnit();

            command = new UpdateAppUserCommand { User = appUserA };
            handler = new UpdateAppUserCommandHandler(mockWorkUnit.Object, mockPlayerQuery.Object);
        }

        private void SetupUser()
        {
            appUserAId  = Guid.NewGuid();
            appUserA    = new AppUser { EntityKey = appUserAId, Handle = "Earthshaker" };
        }

        private void SetupPlayerDataStore()
        {
            playerA1 = new Player { AppUserId = appUserAId, Handle = "Lion" };
            playerA2 = new Player { AppUserId = appUserAId, Handle = "Rubik" };

            playerDataStore = new List<Player>
            {
                playerA1,
                playerA2,
                new Player { AppUserId = Guid.NewGuid(), Handle = "Dazzle" },
            };
        }

        private void SetupMockPlayerQuery()
        {
            mockPlayerQuery = new Mock<IQueryService<Player>>();

            mockPlayerQuery.Setup(m => m.GetAll()).Returns(playerDataStore.AsQueryable);
        }

        private void SetupWorkUnit()
        {
            mockAppUserRepo = new Mock<IRepository<AppUser>>();
            mockPlayerRepo  = new Mock<IRepository<Player>>();
            mockWorkUnit    = new Mock<IWorkUnit>();

            mockWorkUnit.Setup(m => m.GetRepository<AppUser>()).Returns(mockAppUserRepo.Object);
            mockWorkUnit.Setup(m => m.GetRepository<Player>()).Returns(mockPlayerRepo.Object);
        }

        #endregion

        [Test]
        public void UpdateAppUserCommandHandler_Handle_NewUserUpdatedToRepo()
        {
            handler.Handle(command);

            mockAppUserRepo.Verify(m => m.Update(appUserA), Times.Once);
        }

        [Test]
        public void UpdateAppUserCommandHandler_Handle_AssociatedPlayerHandlesChanged()
        {
            handler.Handle(command);

            mockPlayerQuery.Verify(m => m.GetAll(), Times.Once);        
            mockPlayerRepo.Verify(m => m.Update(It.IsAny<Player>()), Times.Exactly(2));

            Assert.AreEqual(command.User.Handle, playerA1.Handle);
            Assert.AreEqual(command.User.Handle, playerA2.Handle);
        }
    }
}
