using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.Entities.Contracts;
using Domain.Entities.Entities;
using Domain.Query.Contracts;
using Frolf.Api.Mappers;
using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.ModelDataControllers.FrolfGroups;
using Frolf.Api.ModelDataControllers.Users;
using Security.Contracts;
using Security.OAuth;
using StructureMap;
using System.Web.Http;

namespace Frolf.Api.IoC
{
    public class ApiRegistry : Registry
    {
        public ApiRegistry()
        {
            Scan(s =>
            {
                s.AssembliesFromApplicationBaseDirectory();

                s.AddAllTypesOf<ApiController>();
                s.AddAllTypesOf(typeof(IReadOnlyEntityMapper<,>));
                s.AddAllTypesOf(typeof(IReadWriteEntityMapper<,>));
                s.AddAllTypesOf(typeof(IModelDataController<,>));
                s.AddAllTypesOf(typeof(IQueryService<>));
            });

            For<IEntityDbContext>().Use(() => new EntityDbContext());
            For<IWorkUnit>().Use<WorkUnit>();
            For<ISecurityKeyProvider>().Use<SecurityKeyProvider>();

            // ModelDataControllers
            For<IAppUserModelDataController>().Use<AppUserModelDataController>();
            For<IFrolfGroupModelDataController>().Use<FrolfGroupModelDataController>();
        }
    }
}