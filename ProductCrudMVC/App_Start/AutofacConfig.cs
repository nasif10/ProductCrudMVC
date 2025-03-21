using Autofac.Integration.Mvc;
using Autofac;
using ProductCrudMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ProductCrudMVC.App_Start
{
    public class AutofacConfig
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            // Register MVC Controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // Register Services Directly
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerRequest();
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerRequest();

            // Build Container
            var container = builder.Build();

            // Set Autofac as Dependency Resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}