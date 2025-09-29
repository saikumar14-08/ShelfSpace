using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shelf.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T: class
    {
        // T - Category controller/class
        // In T, we have CRUD operations. But we only use
        // Create, Read and delete operation in this bcause update and savechanges will have custom implementation and it will be tough to handle for categoris and others going forward


        // Read Operation:
        // for getting all the categories.
        IEnumerable<T> GetAll();
        // for getting only the required category using Linq
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
