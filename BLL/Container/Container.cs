using System;
using System.Reflection;
using System.Collections;
using BLL.Interface;
using System.Linq;
using System.Collections.Generic;

namespace BLL.Container
{
    public class Container
    {
        public static void Resolve()
        {
            try
            {
                Initailize();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void Initailize()
        {
            Type baseType = typeof(IDependency);

            IEnumerable<Assembly> assemblys = AppDomain.CurrentDomain.GetAssemblies().ToList();
            
        }
    }
}