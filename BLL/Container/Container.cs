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
        private static IContainer container = null;

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

            builder.RegisterType<AdminInfoService>().As<IAdminInfoService>().InstancePerLifetimeScope();
            builder.RegisterType<ChargesRecordService>().As<IChargesRecordService>().InstancePerLifetimeScope();
            builder.RegisterType<EnteranceRecordService>().As<IEnteranceRecordService>().InstancePerLifetimeScope();
            builder.RegisterType<OwnerDelayRecordService>().As<IOwnerDelayRecordService>().InstancePerLifetimeScope();
            builder.RegisterType<OwnerInfoService>().As<IOwnerInfoService>().InstancePerLifetimeScope();
            builder.RegisterType<ParkingLotInfoService>().As<IParkingLotInfoService>().InstancePerLifetimeScope();

            container = builder.Build();
        }
    }
}