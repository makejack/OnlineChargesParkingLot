using System;
using System.Reflection;
using System.Collections;
using BLL.Interface;
using System.Linq;
using System.Collections.Generic;
using Autofac;

namespace BLL.Container
{
    public class Container
    {
        public static IContainer container = null;

        public static T Resolve<T>()
        {
            try
            {
                if (container == null)
                {
                    Initailize();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           return container.Resolve<T>();
        }

        private static void Initailize()
        {
            ContainerBuilder builder = new ContainerBuilder();
            Type baseType = typeof(IDependency);

            IEnumerable<Assembly> assemblys = AppDomain.CurrentDomain.GetAssemblies().ToList();
            builder.RegisterAssemblyTypes(assemblys.ToArray()).Where(w => baseType.IsAssignableFrom(w) && baseType != w).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.Build();
        }
    }
}