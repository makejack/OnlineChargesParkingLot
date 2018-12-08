using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using DAL.Interface;

namespace DAL.Container
{
    public class Container
    {
        private static IContainer container = null;

        public static T Resolve<T>()
        {
            if (container == null)
            {
                Initialise();
            }
            return container.Resolve<T>();
        }

        private static void Initialise()
        {
            ContainerBuilder builder = new ContainerBuilder();

            Type baseType = typeof(IDependency);
            List<Assembly> assemblis = AppDomain.CurrentDomain.GetAssemblies().ToList();
            builder.RegisterAssemblyTypes(assemblis.ToArray()).Where(w => baseType.IsAssignableFrom(w) && w != baseType).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.Build();
        }
    }
}