using Ninject;
using Ninject.Modules;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tempalate.Business.Implements;
using Tempalate.Business.Interface;

namespace Tempalate.Business
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
                Bind<ILoginBusiness>().To<LoginBusiness>();
                Bind<ILogicBaseBusiness>().To<LogicBaseBusiness>();
            }
        }
    }
}
