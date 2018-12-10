using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Camera;
using OnlineChargesParkingLot.Model;
using OnlineChargesParkingLot.Interface;
using OnlineChargesParkingLot.Doors;

namespace OnlineChargesParkingLot
{
    public class CameraController : IEnumerable<CameraModel>
    {
        private IDoor m_EnterDoor;
        private IDoor m_ExitDoor;

        private Factory m_CameraFactory;

        private List<CameraModel> m_Cameras = new List<CameraModel>();


        public CameraController(Action enterCallBack, Action exitCallBack)
        {
            m_EnterDoor = new Enter(enterCallBack);
            m_ExitDoor = new Exit(exitCallBack);

            m_CameraFactory = new Factory
            {
                ImagePath = Environment.CurrentDirectory + @"\Imgs"
            };
            m_CameraFactory.RegisterFind(FindCamera);
            m_CameraFactory.RegisterReceived(PlateReceived);

            m_CameraFactory.FindDevice();
        }

        public IEnumerator<CameraModel> GetEnumerator()
        {
            return m_Cameras.GetEnumerator();
        }

        private void FindCamera(object sender, CameraEventArgs e)
        {
            CameraModel camera = new CameraModel(e.Brand, e.IpAddress, e.Port);
            m_Cameras.Add(camera);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void PlateReceived(object sender, PlateEventArgs e)
        {
            CameraModel camera = m_Cameras.Where(w => w.IpAddress == e.IP && w.IsConnection).FirstOrDefault();
            camera.Door.Execute(e.LicensePlateNumber, e.LicensePlateType, e.LicensePlateColor, e.IdentificationTime, e.PanoramaImage, e.VehicleImage);
        }

        public void ConnectionCamera(int index, IntPtr mainHwnd, IntPtr containerHwnd, Directions direction)
        {
            CameraModel camera = m_Cameras[index];
            ConnectionCamera(camera, mainHwnd, containerHwnd, direction);
        }

        public void ConnectionCamera(string ipAddress, IntPtr mainHwnd, IntPtr containerHwnd, Directions direction)
        {
            CameraModel camera = m_Cameras.Where(w => w.IpAddress == ipAddress).FirstOrDefault();
            ConnectionCamera(camera, mainHwnd, containerHwnd, direction);
        }

        public void ConnectionCamera(CameraModel camera, IntPtr mainHwnd, IntPtr containerHwnd, Directions direction)
        {
            camera.Direction = direction;

            ConnectionConfiguration configuration = new ConnectionConfiguration(camera.Brand, camera.IpAddress, camera.Port, mainHwnd, containerHwnd, 0);
            bool ret = m_CameraFactory.Connection(configuration);
            if (ret)
            {
                camera.Door = direction == Directions.Enter ? m_EnterDoor : m_ExitDoor;
            }
        }

        public void CloseCamera(int index)
        {
            CameraModel camera = m_Cameras[index];
            m_CameraFactory.Close(camera.IpAddress);
            camera.IsConnection = false;
        }

    }
}