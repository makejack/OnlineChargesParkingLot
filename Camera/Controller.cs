using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Drawing;

namespace Camera
{
    public delegate void FindCameraHandle(object sender, CameraEventArgs e);

    public delegate void PlateReceivedHandle(object sender, PlateEventArgs info);

    public class Controller
    {

        private Dictionary<string, Device> m_cameraContainer = null;

        private List<ConnectionConfiguration> m_connectionCamera = new List<ConnectionConfiguration>();

        private string m_ImagePath;
        public string ImagePath
        {
            get { return m_ImagePath; }
            set
            {
                m_ImagePath = value;
                if (Directory.Exists(value) == false)
                {
                    Directory.CreateDirectory(value);
                }
                if (m_cameraContainer != null)
                {
                    foreach (KeyValuePair<string, Device> item in m_cameraContainer)
                    {
                        item.Value.ImagePath = m_ImagePath;
                    }
                }
            }
        }

        /// <summary>
        /// 摄像机搜索变化
        /// </summary>
        public event FindCameraHandle FindCameraChange;
        /// <summary>
        /// 车牌识别数据接收
        /// </summary>
        public event PlateReceivedHandle PlateReceivedChange;

        public Controller(string imagePath)
        {
            this.ImagePath = imagePath;

            Initialise();
        }

        private void Initialise()
        {
            m_cameraContainer = new Dictionary<string, Device>();

            Type baseType = typeof(Device);
            List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            List<Type> allTypes = assemblies.SelectMany(t => t.GetTypes()).Where(w => w != baseType && baseType.IsAssignableFrom(w)).ToList();

            foreach (Type item in allTypes)
            {
                Device camera = (Device)Activator.CreateInstance(item);
                camera.ImagePath = this.ImagePath;
                camera.FindCameraCallback = FindCamera;
                m_cameraContainer.Add(item.Name, camera);
            }
        }

        public void UnInit()
        {
            foreach (KeyValuePair<string, Device> item in m_cameraContainer)
            {
                item.Value.UnInit();
            }
        }

        public void FindDevice()
        {
            foreach (KeyValuePair<string, Device> item in m_cameraContainer)
            {
                item.Value.FindCamera();
            }
        }

        public bool Connection(ConnectionConfiguration configuration)
        {
            Device camera = m_cameraContainer[configuration.Brand];
            bool ret = camera.Connection(configuration);
            if (ret)
            {
                m_connectionCamera.Add(configuration);
            }
            return ret;
        }

        public void Close(string ip)
        {
            ConnectionConfiguration configuration = IpToCameraConfiguration(ip);
            Device camera = m_cameraContainer[configuration.Brand];
            camera.Close(configuration);

            m_connectionCamera.Remove(configuration);
        }

        public bool ManualCapture(string ip, string strFullPath)
        {
            ConnectionConfiguration configuration = IpToCameraConfiguration(ip);
            Device camera = m_cameraContainer[configuration.Brand];
            bool ret = camera.ManualCapture(configuration, strFullPath);
            return ret;
        }

        private ConnectionConfiguration IpToCameraConfiguration(string ip)
        {
            return m_connectionCamera.Where(w => w.IP == ip).FirstOrDefault();
        }

        private void FindCamera(string ip, ushort port, string brand)
        {
            if (FindCameraChange != null)
            {
                CameraEventArgs eventArgs = new CameraEventArgs(ip, port, brand);
                FindCameraChange.BeginInvoke(this, eventArgs, null, null);
            }
        }

        private void PlateReceived(int hwnd, string ip, string licensePlateNumber, LicensePlateTypes licensePlateType, Color licensePlateColor, Image pImage, Image vImage, DateTime identificationTime)
        {
            if (hwnd == -1)
            {
                hwnd = m_connectionCamera.Where(w => w.IP == ip).Select(s => s.CameraHwnd).FirstOrDefault();
            }
            else if (string.IsNullOrEmpty(ip))
            {
                ip = m_connectionCamera.Where(w => w.CameraHwnd == hwnd).Select(s => s.IP).FirstOrDefault();
            }
            PlateEventArgs eventArgs = new PlateEventArgs(hwnd, ip, licensePlateNumber, licensePlateType, licensePlateColor, pImage, vImage, identificationTime);
            if (PlateReceivedChange != null)
            {
                PlateReceivedChange.BeginInvoke(this, eventArgs, null, null);
            }
        }

    }
}
