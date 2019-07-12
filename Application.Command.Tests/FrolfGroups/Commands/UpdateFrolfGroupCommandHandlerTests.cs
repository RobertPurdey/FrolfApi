using Application.Command.FrolfGroups.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Moq;
using NUnit.Framework;

namespace Application.Command.Tests.FrolfGroups.Commands
{
    [TestFixture]
    public class UpdateFrolfGroupCommandHandlerTests
    {
        #region Setup

        UpdateFrolfGroupCommand command;
        UpdateFrolfGroupCommandHandler handler;

        FrolfGroup frolfGroup;

        Mock<IRepository<FrolfGroup>> mockFrolfGroupRepo;
        Mock<IWorkUnit> mockWorkUnit;

        [SetUp]
        public void Setup()
        {
            SetupNewUser();
            SetupWorkUnit();

            command = new UpdateFrolfGroupCommand { entity = frolfGroup };
            handler = new UpdateFrolfGroupCommandHandler(mockWorkUnit.Object);
        }

        private void SetupNewUser()
        {
            frolfGroup = new FrolfGroup
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
        public void UpdateFrolfGroupCommandHandler_Handle_NewFrolfGroupUpdatedToRepo()
        {
            handler.Handle(command);

            mockFrolfGroupRepo.Verify(m => m.Update(frolfGroup), Times.Once);
        }
    }
}
