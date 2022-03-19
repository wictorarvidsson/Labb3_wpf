using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccesLayer
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public MyContext context;
        public DbSet<T> dbSet;

        public GenericRepository(MyContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
            //context.Set<T>().Add(entity);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return dbSet.Where(predicate);
        }

        public T FirstOrDefault(Func<T, bool> predicate)
        {
            return dbSet.FirstOrDefault(predicate);
        }

        public bool IsEmpty()
        {
            return (dbSet.Count() == 0);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public IEnumerable<T> ReturnAll()
        {
            return dbSet.ToList();
        }
    }
}
