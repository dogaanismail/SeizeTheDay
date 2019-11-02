using System.Linq;

namespace SeizeTheDay.Core.DataAccess.Abstract.MySQL
{
    public interface IMyQueryableRepository<T> where T : class, new()
    {
        IQueryable<T> Table { get; }
    }
}
