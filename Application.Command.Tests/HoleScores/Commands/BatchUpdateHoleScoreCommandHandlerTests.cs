using Application.Command.HoleScores.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.Tests.HoleScores.Commands
{
    [TestFixture]
    public class BatchUpdateHoleScoreCommandHandlerTests
    {
        #region Setup

        BatchUpdateHoleScoreCommand command;
        BatchUpdateHoleScoreCommandHandler handler;

        Guid holeOneId;
        Guid holeTwoId;

        List<HoleScore> holeScores;

        Mock<IRepository<HoleScore>> mockHoleScoreRepo;
        Mock<IWorkUnit> mockWorkUnit;

        [SetUp]
        public void Setup()
        {
            SetupHoleScores();
            SetupMockWorkUnit();

            command = new BatchUpdateHoleScoreCommand(Guid.NewGuid(), holeScores);
            handler = new BatchUpdateHoleScoreCommandHandler(mockWorkUnit.Object);
        }

        private void SetupHoleScores()
        {
            holeOneId = Guid.NewGuid();
            holeTwoId = Guid.NewGuid();

            holeScores = new List<HoleScore>
            {
                new HoleScore { EntityKey = holeOneId, Strokes = 1 },
                new HoleScore { EntityKey = holeTwoId, Strokes = 2 }
            };
        }

        private void SetupMockWorkUnit()
        {
            mockHoleScoreRepo = new Mock<IRepository<HoleScore>>();
            mockWorkUnit      = new Mock<IWorkUnit>();

            mockWorkUnit.Setup(m => m.GetRepository<HoleScore>()).Returns(mockHoleScoreRepo.Object);
        }

        #endregion

        [Test]
        public void BatchUpdateHoleScoreCommandHandler_Handle_HolesUpdated()
        {
            handler.Handle(command);

            mockHoleScoreRepo.Verify(
                m => m.Update(It.Is<HoleScore>(h => h.EntityKey == holeOneId)),
                Times.Once()
            );

            mockHoleScoreRepo.Verify(
                m => m.Update(It.Is<HoleScore>(h => h.EntityKey == holeTwoId)),
                Times.Once()
            );
        }
    }
}
