using CarpenterAPI.Data;
using CarpenterAPI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarpenterAPI.Repository
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal APIDBContext dbContext;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(APIDBContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>>[] filter = null,
            Expression<Func<TEntity, object>> orderBy = null,
            string includeProperties = "",
            Paging page = null)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();

            if (filter != null)
            {
                foreach(var filterExpression in filter)
                    query = query.Where(filterExpression);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if(page != null && page.PageIndex != 0)
            {
                query = query.Skip((page.PageIndex - 1) * page.PageSize).Take(page.PageSize);
            }

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }

             return query.ToList();
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void InsertRange(TEntity[] entities)
        {
            dbSet.AddRange(entities);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
