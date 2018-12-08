using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Camera.Model;

namespace Camera
{

    public delegate void FindCameraHandle(object sender, CameraEventArgs e);

    public delegate void PlateReceivedHandle(object sender, PlateEventArgs info);

    internal class Common
    {
        private static Common m_Default = null;

        public static Common Default
        {
            get
            {
                if (m_Default == null)
                {
                    m_Default = new Common();
                }
                return m_Default;
            }
        }

        private List<ConnectionConfiguration> ConnCameras = new List<ConnectionConfiguration>();

        public string ImagePath { get; set; }

        private event FindCameraHandle m_FindCameraEvent;
        public event FindCameraHandle FindCameraEvent
        {
            add { m_FindCameraEvent += value; }
            remove { m_FindCameraEvent -= value; }
        }
        
        public void ExecuteFindCamera(object sender, CameraEventArgs e)
        {
            m_FindCameraEvent?.Invoke(sender, e);
        }

        private event PlateReceivedHandle m_PlateReceivedEvent;
        public event PlateReceivedHandle PlateReceivedEvent
        {
            add { m_PlateReceivedEvent += value; }
            remove { m_PlateReceivedEvent -= value; }
        }

        public void ExecutePlateReceived(object sender, PlateEventArgs e)
        {
            m_PlateReceivedEvent?.Invoke(sender, e);
        }

        public void Add(ConnectionConfiguration configuration)
        {
            ConnCameras.Add(configuration);
        }

        public void Del(ConnectionConfiguration configuration)
        {
            ConnCameras.Remove(configuration);
        }

        public ConnectionConfiguration GetConfiguration(string ip)
        {
            return ConnCameras.Where(w => w.IP == ip).FirstOrDefault();
        }

        public int CameraIpToHandle(string ip)
        {
            return ConnCameras.Where(w => w.IP == ip).Select(s => s.CameraHwnd).FirstOrDefault();
        }

        public int CameraIpToHandle(string ip, string brand)
        {
            return ConnCameras.Where(w => w.IP == ip && w.Brand == brand).Select(s => s.CameraHwnd).FirstOrDefault();
        }

        public string CameraHandleToIp(int handle)
        {
            return ConnCameras.Where(w => w.CameraHwnd == handle).Select(s => s.IP).FirstOrDefault();
        }

        public string CameraHandleToIp(int handle, string brand)
        {
            return ConnCameras.Where(w => w.CameraHwnd == handle && w.Brand == brand).Select(s => s.IP).FirstOrDefault();
        }

    }
}