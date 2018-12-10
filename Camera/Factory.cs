﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;

namespace Camera
{
    public class Factory
    {

        private Dictionary<string, ICamera> m_cameraContainer = null;

        public string ImagePath
        {
            get { return Common.Default.ImagePath; }
            set
            {
                Common.Default.ImagePath = value;
                if (Directory.Exists(value) == false)
                {
                    Directory.CreateDirectory(value);
                }
            }
        }

        public Factory()
        {
            Initialise();
        }

        private void Initialise()
        {
            m_cameraContainer = new Dictionary<string, ICamera>();

            Type baseType = typeof(ICamera);
            List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            List<Type> allTypes = assemblies.SelectMany(t => t.GetTypes()).Where(w => w != baseType && baseType.IsAssignableFrom(w)).ToList();

            foreach (Type item in allTypes)
            {
                ICamera camera = (ICamera)Activator.CreateInstance(item);
                m_cameraContainer.Add(item.Name, camera);
            }
        }

        public void UnInit()
        {
            foreach (KeyValuePair<string, ICamera> item in m_cameraContainer)
            {
                item.Value.UnInit();
            }
        }

        public void FindDevice()
        {
            foreach (KeyValuePair<string, ICamera> item in m_cameraContainer)
            {
                item.Value.FindCamera();
            }
        }

        public bool Connection(ConnectionConfiguration configuration)
        {
            ICamera camera = m_cameraContainer[configuration.Brand];
            bool ret = camera.Connection(configuration);
            if (ret)
            {
                Common.Default.Add(configuration);
            }
            return ret;
        }

        public void Close(string ip)
        {
            ConnectionConfiguration configuration = Common.Default.GetConfiguration(ip);
            ICamera camera = m_cameraContainer[configuration.Brand];
            camera.Close(configuration);

            Common.Default.Del(configuration);
        }

        public bool ManualCapture(string ip, string strFullPath)
        {
            ConnectionConfiguration configuration = Common.Default.GetConfiguration(ip);
            ICamera camera = m_cameraContainer[configuration.Brand];
            bool ret = camera.ManualCapture(configuration, strFullPath);
            return ret;
        }

        public void RegisterFind(FindCameraHandle func)
        {
            foreach (ICamera item in m_cameraContainer.Values)
            {
                item.FindCameraChange += func;
            }
        }

        public void UnregisterFind(FindCameraHandle func)
        {
            foreach (ICamera item in m_cameraContainer.Values)
            {
                item.FindCameraChange -= func;
            }
        }

        public void RegisterReceived(PlateReceivedHandle func)
        {
            foreach (ICamera item in m_cameraContainer.Values)
            {
                item.PlateReceivedChange += func;
            }
        }

        public void UnregisterReceived(PlateReceivedHandle func)
        {
            foreach (ICamera item in m_cameraContainer.Values)
            {
                item.PlateReceivedChange -= func;
            }
        }

    }
}
