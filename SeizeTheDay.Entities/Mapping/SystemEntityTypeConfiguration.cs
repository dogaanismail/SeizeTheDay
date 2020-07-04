using System.Data.Entity.ModelConfiguration;

namespace SeizeTheDay.Entities.Mapping
{
    public abstract class SystemEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : class
    {
        protected SystemEntityTypeConfiguration()
        {
            PostInitialize();
        }

        /// <summary>
        /// We can override this method in custom partial classes
        /// in order to add some custom initialization to constructors
        /// </summary>
        protected virtual void PostInitialize()
        {

        }
    }
}
