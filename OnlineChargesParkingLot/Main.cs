using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using BLL.Interface;
using LogHelper;
using Camera;
using OnlineChargesParkingLot.ViewModel;
using OnlineChargesParkingLot.DoorModule;

namespace OnlineChargesParkingLot
{
    public partial class Main : Form
    {
        /// <summary>
        /// 当前管理员信息
        /// </summary>
        private AdminInfo m_AdminInfo;
        /// <summary>
        /// 当前停车场信息
        /// </summary>
        private ParkingLotInfo m_ParkingLotInfo;

        private Controller m_CameraController;
        private List<CameraParam> m_CameraParams = new List<CameraParam>();

        private IDoor EnterDoor;
        private IDoor ExitDoor;

        public Main(AdminInfo adminInfo)
        {
            m_AdminInfo = adminInfo;

            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //获取停车场信息
            IParkingLotInfoService parkingLotInfoService = BLL.Container.Container.Resolve<IParkingLotInfoService>();
            m_ParkingLotInfo = parkingLotInfoService.GetModels().FirstOrDefault();

            EnterDoor = new Enter(m_ParkingLotInfo, EnterInfoShow);
            ExitDoor = new Exit(m_ParkingLotInfo, ExitInfoShow);

            //初始化摄像机控制器
            m_CameraController = new Camera.Controller(Application.StartupPath + @"\Imgs");
            m_CameraController.FindCameraChange += FindCamera;
            m_CameraController.PlateReceivedChange += PlateReceived;
        }

        private void Main_Shown(object sender, EventArgs e)
        {


            //搜索摄像机
            m_CameraController.FindDevice();
        }

        private void FindCamera(object sender, CameraEventArgs e)
        {
            CameraParam cameraParam = new CameraParam(e.IpAddress, e.Port, e.Brand);
            m_CameraParams.Add(cameraParam);
        }

        private void PlateReceived(object sender, PlateEventArgs e)
        {
            CameraParam cameraParam = m_CameraParams.Where(w => w.Ip == e.IP).FirstOrDefault();
            IDoor door;
            if (cameraParam.Direction == Directions.Enter)
            {
                door = EnterDoor;
            }
            else
            {
                door = ExitDoor;
            }
            door.Execute(e.LicensePlateNumber, e.LicensePlateType, e.LicensePlateColor, e.IdentificationTime);
        }

        private void EnterInfoShow()
        {

        }

        private void ExitInfoShow()
        {

        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

    }
}
