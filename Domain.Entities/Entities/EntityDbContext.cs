using Domain.Entities.Contracts;
using Domain.Entities.Contracts.Exceptions;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;

namespace Domain.Entities.Entities
{
    /// <summary>
    /// Provides access to the database.
    /// </summary>
    public class EntityDbContext : DbContext, IEntityDbContext
    {
        static EntityDbContext()
        {
            Database.SetInitializer<EntityDbContext>(null);
        }

        public EntityDbContext()
            : base("Name=DefaultCollection")
        {

        }

        public EntityDbContext(DbConnection connection)
            : base(connection, false)
        {

        }

        public TEntity Add<TEntity>(TEntity entityToAdd) where TEntity : class
        {
            return Set<TEntity>().Add(entityToAdd);
        }
        public void Remove<TEntity>(TEntity entityToRemove) where TEntity : class
        {
            Set<TEntity>().Remove(entityToRemove);
        }

        public IQueryable<TEntity> GetCollection<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public void CommitChanges()
        {
            if ( !ChangeTracker.HasChanges() )
            {
                return;
            }

            try
            {
                SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw new DbValidationException(
                    "Entity validation failed. Check inner exception for details",
                    ex);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            OnCreateModelMapping(modelBuilder);
        }

        private void OnCreateModelMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AppUserMap());
            modelBuilder.Configurations.Add(new PlayerMap());
            modelBuilder.Configurations.Add(new FrolfGroupMap());
            modelBuilder.Configurations.Add(new FrolfGroupInviteMap());
        }
    }
}
