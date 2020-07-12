using AutoMapper;
using Ninject.Modules;

namespace SeizeTheDay.Ninject.Modules
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMapper>().ToConstant(CreateConfiguration().CreateMapper()).InSingletonScope();
        }

        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(assembliesToScan: GetType().Assembly);
            });

            return config;
        }
    }
}
