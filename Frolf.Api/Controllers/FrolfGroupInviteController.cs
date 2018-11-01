using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.Models.FrolfGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Frolf.Api.Controllers
{
    public class FrolfGroupInviteController : EditControllerBase<FrolfGroupInviteModel>
    {
        private readonly IFrolfGroupInviteModelDataController frolfGroupInviteModelDataController;

        public FrolfGroupInviteController(
            IFrolfGroupInviteModelDataController frolfGroupInviteDataController)
        {
            frolfGroupInviteModelDataController = frolfGroupInviteDataController;
        }

        public override Task<IEnumerable<FrolfGroupInviteModel>> GetAll()
        {
            var foundFrolfGroups = frolfGroupInviteModelDataController.GetAll();

            return Task.FromResult(foundFrolfGroups);
        }

        public override Task<FrolfGroupInviteModel> GetById([FromUri] Guid id)
        {
            var foundFrolfGroup = frolfGroupInviteModelDataController.GetById(id);

            return Task.FromResult(foundFrolfGroup);
        }

        protected override Task<FrolfGroupInviteModel> Create([FromBody] FrolfGroupInviteModel newEntity)
        {
            frolfGroupInviteModelDataController.Insert(newEntity);

            return Task.FromResult(newEntity);
        }

        protected override Task Remove([FromUri] Guid id)
        {
            throw new NotImplementedException();
        }

        protected override Task Update([FromBody] FrolfGroupInviteModel newDetails)
        {
            throw new NotImplementedException();
        }
    }
}