using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;

namespace SeizeTheDay.Core.DataAccess.Abstract.MySQL
{
    public interface IMyQueryableRepository<T> where T : EntityObject, new()
    {
        IQueryable<T> Table { get; }
    }
}
