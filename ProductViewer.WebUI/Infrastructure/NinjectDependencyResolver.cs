using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Ninject;

namespace ProductViewer.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            this.kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            // e.g. kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}