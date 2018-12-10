using Camera;
using Camera.Interface;

namespace OnlineChargesParkingLot
{
    public class CameraController
    {
        private Factory m_CameraFactory;

        private List<CameraModel> m_Cameras = new List<CameraModel>();


        public CameraController()
        {

            m_CameraFactory = new Factory
            {
                ImagePath = Application.StartupPath + @"\Imgs"
            };
            m_CameraFactory.RegisterFind(FindCamera);
            m_CameraFactory.RegisterReceived(PlateReceived);
        }


        private void FindCamera(object sender, CameraEventArgs e)
        {
            CameraModel camera = new CameraModel(e.Brand, e.IpAddress, e.Port);
            m_Cameras.Add(camera);
        }

        private void PlateReceived(object sender, PlateEventArgs e)
        {

        }
    }
}