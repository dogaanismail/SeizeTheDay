using SeizeTheDay.Core.Entities;
using System.Linq;

namespace SeizeTheDay.Core.DataAccess.Abstract
{
    public interface IQueryableRepository<T> where T : class, IEntity, new()
    {
        IQueryable<T> Table { get; }
    }
}
