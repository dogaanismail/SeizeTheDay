using Ninject;
using SeizeTheDay.Ninject.Modules;

namespace SeizeTheDay.Ninject.Factories
{
    public class InstanceFactory
    {
        public static T GetInstance<T>()
        {
            var kernel = new StandardKernel(new BusinessModule());
            return kernel.Get<T>();
        }

    }
}
