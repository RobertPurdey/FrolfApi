using Application.Command.AppUsers.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Moq;
using NUnit.Framework;
using System;

namespace Application.Command.Tests.AppUsers.Commands
{
    [TestFixture]
    public class AddAppUserRefreshTokenCommandHandlerTests
    {
        #region Setup

        AddAppUserRefreshTokenCommand command;
        AddAppUserRefreshTokenCommandHandler handler;

        AppUserRefreshToken newRefreshToken;

        Mock<IRepository<AppUserRefreshToken>> mockRefreshTokenRepo;
        Mock<IWorkUnit> mockWorkUnit;     
        
        [SetUp]
        public void Setup()
        {
            SetupAppUserRefreshToken();
            SetupWorkUnit();

            command = new AddAppUserRefreshTokenCommand { Token = newRefreshToken };
            handler = new AddAppUserRefreshTokenCommandHandler(mockWorkUnit.Object);
        }

        private void SetupAppUserRefreshToken()
        {
            newRefreshToken = new AppUserRefreshToken
            {
                Id               = Guid.NewGuid(),
                UserId           = Guid.NewGuid(),
                ExpiresOn        = DateTime.UtcNow,
                IssuedOn         = DateTime.UtcNow,
                SerializedTicket = "ticket",
                Token            = "token",
            };
        }

        private void SetupWorkUnit()
        {
            mockRefreshTokenRepo = new Mock<IRepository<AppUserRefreshToken>>();
            mockWorkUnit         = new Mock<IWorkUnit>();

            mockWorkUnit.Setup(m => m.GetRepository<AppUserRefreshToken>()).Returns(mockRefreshTokenRepo.Object);
        }

        #endregion

        [Test]
        public void AddAppUserRefreshTokenCommandHandler_Handle_NewRefreshTokenddedToRepo()
        {
            handler.Handle(command);

            mockRefreshTokenRepo.Verify( m => m.Add(newRefreshToken), Times.Once );
        }
    }
}
