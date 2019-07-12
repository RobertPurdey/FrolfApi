using Application.Command.FrolfGroups.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Moq;
using NUnit.Framework;

namespace Application.Command.Tests.FrolfGroups.Commands
{
    [TestFixture]
    public class AddFrolfGroupCommandHandlerTests
    {
        #region Setup

        AddFrolfGroupCommand command;
        AddFrolfGroupCommandHandler handler;

        FrolfGroup newFrolfGroup;

        Mock<IRepository<FrolfGroup>> mockFrolfGroupRepo;
        Mock<IWorkUnit> mockWorkUnit;

        [SetUp]
        public void Setup()
        {
            SetupNewUser();
            SetupWorkUnit();

            command = new AddFrolfGroupCommand { newEntity = newFrolfGroup };
            handler = new AddFrolfGroupCommandHandler(mockWorkUnit.Object);
        }

        private void SetupNewUser()
        {
            newFrolfGroup = new FrolfGroup
            {
                Name = "summer tournament",
            };
        }

        private void SetupWorkUnit()
        {
            mockFrolfGroupRepo = new Mock<IRepository<FrolfGroup>>();
            mockWorkUnit       = new Mock<IWorkUnit>();

            mockWorkUnit.Setup(m => m.GetRepository<FrolfGroup>()).Returns(mockFrolfGroupRepo.Object);
        }

        #endregion

        [Test]
        public void AddFrolfGroupCommandHandler_Handle_NewFrolfGroupAddedToRepo()
        {
            handler.Handle(command);

            mockFrolfGroupRepo.Verify(m => m.Add(newFrolfGroup), Times.Once);
        }
    }
}
