using Application.Command.AppUsers.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.Tests.AppUsers.Commands
{
    [TestFixture]
    public class DeleteAppUserRefreshTokenCommandHandlerTests
    {
        #region Setup

        DeleteAppUserRefreshTokenCommand command;
        DeleteAppUserRefreshTokenCommandHandler handler;

        AppUserRefreshToken newRefreshToken;

        Mock<IRepository<AppUserRefreshToken>> mockRefreshTokenRepo;
        Mock<IWorkUnit> mockWorkUnit;     
        
        [SetUp]
        public void Setup()
        {
            SetupAppUserRefreshToken();
            SetupWorkUnit();

            command = new DeleteAppUserRefreshTokenCommand { Token = newRefreshToken };
            handler = new DeleteAppUserRefreshTokenCommandHandler(mockWorkUnit.Object);
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
            mockRefreshTokenRepo
                .Setup(m => m.GetByKey(It.IsAny<Expression<Func<AppUserRefreshToken, bool>>>()))
                .Returns(newRefreshToken);

            mockWorkUnit = new Mock<IWorkUnit>();
            mockWorkUnit.Setup(m => m.GetRepository<AppUserRefreshToken>()).Returns(mockRefreshTokenRepo.Object);
        }

        #endregion

        [Test]
        public void AddAppUserRefreshTokenCommandHandler_Handle_NewRefreshTokenddedToRepo()
        {
            handler.Handle(command);

            mockRefreshTokenRepo.Verify( m => m.Remove(newRefreshToken), Times.Once );
        }
    }
}
