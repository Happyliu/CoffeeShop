﻿using CoffeeStore.Domain.Abstract;
using CoffeeStore.Domain.Concrete;
using Ninject;
using Ninject.Extensions.ChildKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Ninject.Web.Common;

namespace CoffeeStore.Infrastructure
{
    public class NinjectResolver : System.Web.Http.Dependencies.IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        private IKernel kernel;
        public NinjectResolver() : this(new StandardKernel()) { }

        public NinjectResolver(IKernel ninjectKernel)
        {
            kernel = ninjectKernel;
            AddBindings(kernel);
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        public void Dispose()
        {
            // do nothing
        }
        private void AddBindings(IKernel kernel)
        {
            // singleton and transient bindings go here
            kernel.Bind<IProductRepository>().To<ProductRepository>().InRequestScope();
            kernel.Bind<ICommentRepository>().To<CommentRepository>().InRequestScope();
            kernel.Bind<IOrderRepository>().To<OrderRepository>().InRequestScope();
            kernel.Bind<ILoginRepository>().To<LoginRepository>().InRequestScope();
        }
    }
}