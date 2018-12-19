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
            try
            {
                if (container == null)
                {
                    Initialise();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return container.Resolve<T>();
        }

        private static void Initialise()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<AdminInfoDAL>().As<IAdminInfoDAL>().InstancePerLifetimeScope();
            builder.RegisterType<ChargesRecordDAL>().As<IChargesRecordDAL>().InstancePerLifetimeScope();
            builder.RegisterType<EnteranceRecordDAL>().As<IEnteranceRecordDAL>().InstancePerLifetimeScope();
            builder.RegisterType<OwnerDelayRecordDAL>().As<IOwnerDelayRecordDAL>().InstancePerLifetimeScope();
            builder.RegisterType<OwnerInfoDAL>().As<IOwnerInfoDAL>().InstancePerLifetimeScope();
            builder.RegisterType<ParkingLotInfoDAL>().As<IParkingLotInfoDAL>().InstancePerLifetimeScope();

            container = builder.Build();
        }
    }
}