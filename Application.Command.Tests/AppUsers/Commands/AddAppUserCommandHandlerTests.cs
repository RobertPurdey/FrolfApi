using Application.Command.AppUsers.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Moq;
using NUnit.Framework;

namespace Application.Command.Tests.AppUsers.Commands
{
    [TestFixture]
    public class AddAppUserCommandHandlerTests
    {
        #region Setup

        AddAppUserCommand command;
        AddAppUserCommandHandler handler;

        AppUser newAppUser;

        Mock<IRepository<AppUser>> mockAppUserRepo;
        Mock<IWorkUnit> mockWorkUnit;     
        
        [SetUp]
        public void Setup()
        {
            SetupNewUser();
            SetupWorkUnit();

            command = new AddAppUserCommand { NewUser = newAppUser };
            handler = new AddAppUserCommandHandler(mockWorkUnit.Object);
        }

        private void SetupNewUser()
        {
            newAppUser = new AppUser
            {
                LoginName  = "Patrick",
                FriendCode = "toomuchfun",
            };
        }

        private void SetupWorkUnit()
        {
            mockAppUserRepo = new Mock<IRepository<AppUser>>();
            mockWorkUnit    = new Mock<IWorkUnit>();

            mockWorkUnit.Setup(m => m.GetRepository<AppUser>()).Returns(mockAppUserRepo.Object);
        }

        #endregion

        [Test]
        public void AddAppUserCommandHandler_Handle_NewUserAddedToRepo()
        {
            handler.Handle(command);

            mockAppUserRepo.Verify(m => m.Add(newAppUser), Times.Once);
        }
    }
}
