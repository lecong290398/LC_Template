using Ninject;
using Ninject.Modules;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCommand
{
    public class NinjectFactory
    {
        private static readonly IKernel Kernel = new StandardKernel(new DataModuleLoader());

        public static T Get<T>(params IParameter[] param)
        {
            return Kernel.Get<T>(param);
        }

        public class DataModuleLoader : NinjectModule
        {
            public override void Load()
            {
                Bind<IDapperWrapper>().To<DapperWrapper>();
            }
        }
    }
}
