using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer
{
    public interface IGenericRepository<T> where T : class
    {
        //Add entity to table
        public void Add(T entity);

        //Remove entity from table
        public void Remove(T entity);

        //Return all entities in table
        public IEnumerable<T> ReturnAll();

        //Find entities in table
        public IEnumerable<T> Find(Func<T, bool> predicate);

        //Find one entity in table
        public T FirstOrDefault(Func<T, bool> predicate);

        //Check if empty
        public bool IsEmpty();

        private static IList<T> table;
    }
}
