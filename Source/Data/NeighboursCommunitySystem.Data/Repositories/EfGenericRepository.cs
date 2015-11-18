namespace NeighboursCommunitySystem.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using DbContexts;
    using System.Data.Entity.Validation;
    using System.Text;

    public class EfGenericRepository<T> : IRepository<T>
        where T : class
    {
        public EfGenericRepository(INeighboursCommunityDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("The data context must be instantiated.", "dbContext must have a non-NULL value");
            }

            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<T>();
        }

        protected INeighboursCommunityDbContext DbContext { get; set; }

        protected IDbSet<T> DbSet { get; set; }

        public IQueryable<T> All()
        {
            return this.DbSet.AsQueryable();
        }

        public T GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        public T Attach(T entity)
        {
            return this.DbContext.Set<T>().Attach(entity);
        }

        public void Add(T entity)
        {
            var entry = this.DbContext.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(entity);
            }
        }

        public void Delete(object id)
        {
            var entity = this.GetById(id);

            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        public void Delete(T entity)
        {
            var entry = this.DbContext.Entry(entity);

            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }
        }

        public void Detach(T entity)
        {
            var entry = this.DbContext.Entry(entity);
            entry.State = EntityState.Detached;
        }

        public void Dispose()
        {
            this.DbContext.Dispose();
        }

        public void Update(T entity)
        {
            var entry = this.DbContext.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public int SaveChanges()
        {
            try
            {
                return this.DbContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedDbEntityValidationException(e);
                throw newException;
            }
        }

        public class FormattedDbEntityValidationException : Exception
        {
            public FormattedDbEntityValidationException(DbEntityValidationException innerException) :
                base(null, innerException)
            {
            }

            public override string Message
            {
                get
                {
                    var innerException = InnerException as DbEntityValidationException;
                    if (innerException != null)
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine();
                        sb.AppendLine();
                        foreach (var eve in innerException.EntityValidationErrors)
                        {
                            sb.AppendLine(string.Format("- Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().FullName, eve.Entry.State));
                            foreach (var ve in eve.ValidationErrors)
                            {
                                sb.AppendLine(string.Format("-- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                                    ve.PropertyName,
                                    eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                                    ve.ErrorMessage));
                            }
                        }
                        sb.AppendLine();

                        return sb.ToString();
                    }

                    return base.Message;
                }
            }
        }
    }
}