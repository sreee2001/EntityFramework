using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Text;

namespace Repository.Services
{
    //public class ExampleDbContext : DbContext
    //{
    //    public ExampleDbContext() 
    //    {
    //        // Set Initializer
    //        Database.SetInitializer(new ExampleDbInitializer());
    //        Configuration.LazyLoadingEnabled = true;

    //    }
    //    // Define your DbSets here
    //    // public DbSet<YourEntity> YourEntities { get; set; }
    //}

    //internal class ExampleDbInitializer : DropCreateDatabaseAlways<ExampleDbContext>
    //{
    //    public override void InitializeDatabase(ExampleDbContext context)
    //    {
    //        base.InitializeDatabase(context);
    //    }

    //    protected override void Seed(ExampleDbContext context)
    //    {
    //        base.Seed(context);
    //        // Add additional Seeding of entries into Db in here
    //    }

    //    public override string ToString()
    //    {
    //        return base.ToString();
    //    }

    //    public override bool Equals(object obj)
    //    {
    //        return base.Equals(obj);
    //    }

    //    public override int GetHashCode()
    //    {
    //        return base.GetHashCode();
    //    }
    //}

    //[Export(typeof(IDbService))]
    //public class ExampleDbService : DbServiceBase<ExampleDbContext>
    //{

    //}

    //[Export(typeof(IDbService))]
    public abstract class DbServiceBase<T> : IDbService where T : DbContext, new()
    {
        [Import]
        protected T DbContext { get; set; }
        //[Import]
        //public IMessagingService MessagingService { get; set; }

        /// <inheritdoc />
        public bool CanSaveChanges()
        {
            try
            {
                return DbContext.ChangeTracker.HasChanges();
            }
            catch (SqlException sex)
            {
                ReportValidationErrors(sex);
                return false;
            }
            catch (Exception e)
            {
                //MessagingService.PostMessage(MessageType.Error, e.Message);
                return false;
            }
        }

        private void ReportValidationErrors(SqlException sex) // Fully qualify the SqlException type
        {
            var message = new StringBuilder();
            message.AppendLine("The following validation errors were encountered while saving:");

            foreach (SqlError error in sex.Errors) // Fully qualify the SqlError type
            {
                message.AppendLine("   Error:" + error.Number + " - " + error.Message);
            }
            //MessagingService.PostMessage(MessageType.Error, message.ToString());
        }


        /// <inheritdoc />
        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        /// <inheritdoc />
        public void SetState<TEntity>(TEntity entity, EntityState entityState) where TEntity : Entity
        {
            DbContext.Entry(entity).State = entityState;
        }

        ///// <inheritdoc />
        //public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : Entity
        //{
        //    DbSet<TEntity> collection = null;
        //    DbContext.GetType().GetProperties().ToList().ForEach(propertyInfo =>
        //    {
        //        if (typeof(DbSet<TEntity>).IsAssignableFrom(propertyInfo.PropertyType))
        //        {
        //            MethodInfo methodInfo = propertyInfo.GetGetMethod();

        //            collection =  (DbSet<TEntity>)methodInfo.Invoke(DbContext, null);
        //        }
        //    });
        //    return collection;

        //}

        /// <inheritdoc />
        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return DbContext.Set<TEntity>();
        }

        /// <inheritdoc />
        public void Refresh(Entity entity)
        {
            if (entity == null || entity.Id == 0)
                return;
            DbContext.Entry(entity).Reload();
        }

        /// <inheritdoc />
        public void Refresh()
        {
            IEnumerable<DbEntityEntry> dbEntityEntries = DbContext.ChangeTracker.Entries();
            foreach (DbEntityEntry dbEntityEntry in dbEntityEntries)
            {
                dbEntityEntry.Reload();
            }
        }

        //public DbEntityEntry<T> Entry<T>(T entity) where T : Entity
        //{
        //    return DbContext.Entry(entity);
        //}

        /// <inheritdoc />
        public void ResetContext()
        {
            DbContext = new T();
        }

        /// <inheritdoc />
        public void Remove<TEntity>(TEntity entity) where TEntity : Entity
        {
            DbContext.Set<TEntity>().Remove(entity);
        }
    }
}
