using SeizeTheDay.Core.DataAccess.Abstract.MySQL;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;

namespace SeizeTheDay.Core.DataAccess.Concrete.MySQL
{
    public class MyQueryableRepository<T> : IMyQueryableRepository<T> where T : EntityObject, new()
    {
        private readonly ObjectContext _context;
        private IObjectSet<T> _entities;

        public MyQueryableRepository(ObjectContext context)
        {
            _context = context;
        }
        public IQueryable<T> Table => this.Entities;

        protected virtual IObjectSet<T> Entities
        {
            get { return _entities ?? (_entities = _context.CreateObjectSet<T>()); }
        }

    }
}
