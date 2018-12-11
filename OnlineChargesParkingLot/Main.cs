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

        private Camera.Controller m_CameraController;


        

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
            string ip = e.IpAddress;
            ushort port = e.Port;
            string brand = e.Brand;


        }

        private void PlateReceived(object sender, PlateEventArgs e)
        {
            string ip = e.IP;
            int hwnd = e.Handle;
            string licensePlateNumber = e.LicensePlateNumber;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

    }
}
