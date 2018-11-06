using Domain.Commands.Contracts;
using Domain.Entities.Contracts;
using Domain.Entities.Contracts.Exceptions;
using System;

namespace Domain.Commands
{
    public class WorkUnit : IWorkUnit
    {
        protected IEntityDbContext EntityContext { get; private set; }
        protected ICommandLocator CommandLocator { get; }

        public WorkUnit(IEntityDbContext context, ICommandLocator commandLocator)
        {
            EntityContext  = context ?? throw new ArgumentNullException(nameof(context));
            CommandLocator = commandLocator ?? throw new ArgumentNullException(nameof(commandLocator));
        }

        public virtual IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(
                EntityContext,
                CommandLocator.GetEntityValidators<TEntity>());
        }

        public void Commit()
        {
            if (EntityContext == null)
            {
                throw new InvalidOperationException("Cannot commit using a disposed context.");
            }

            try
            {
                EntityContext.CommitChanges();
            }
            catch (DbValidationException ex)
            {
                RollbackDbContext();

                throw new DbValidationException(
                    "Commit was unsuccessful. Check the inner exception for details",
                    ex);
            }
            catch (Exception ex)
            {
                RollbackDbContext();

                throw new Exception(
                    "Unexpected database error. Check the inner exception for details",
                    ex);
            }
        }

        private void RollbackDbContext()
        {
            EntityContext = null;
        }
    }
}
