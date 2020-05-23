using System.Collections.Generic;

namespace SeizeTheDay.Core.DataAccess.Abstract
{
    public interface IDataMapper<T>
    {
        void Delete(int id);
        IEnumerable<T> FindAll();
        T FindById(int id);
        void Insert(T item);
        void Update(T item);
    }
}
