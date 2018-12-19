using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using WinFormAnimation;
using System.Windows.Forms;
using Model;
using BLL.Interface;
using LogHelper;
using Camera;
using OnlineChargesParkingLot.ViewModel;
using OnlineChargesParkingLot.DoorModule;
using System.Threading.Tasks;

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
            this.Opacity = 0;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ControlExpansion.ButtonExpansion.ClearFocus(this.Controls);

            try
            {
                //加载自定义字体
                Fonts.FontFactory.InitiailseFont();
                //实例化字体
                Font ledFont = new Font(Fonts.FontFactory.LedFont.Families[0], lParkingVehicleCount.Font.Size, lParkingVehicleCount.Font.Style);
                //场内车辆数
                lParkingVehicleCount.Font = ledFont;
                //出场车辆数
                lExitVehicleCount.Font = ledFont;


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
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            new Animator(new Path((float)this.Opacity, 1, 250)).Play(this, "Opacity");

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
            IdentificationInfo info = new IdentificationInfo(e.LicensePlateNumber, e.LicensePlateType, e.LicensePlateColor, e.PanoramaImage, e.VehicleImage, e.IdentificationTime);
            CameraParam cameraParam = m_CameraParams.Where(w => w.Ip == e.IP).FirstOrDefault();
            if (cameraParam != null)
            {
                IDoor door = cameraParam.Direction == Directions.Enter ? EnterDoor : ExitDoor;
                door.Execute(info);
            }
        }

        private void EnterInfoShow(EnterVehicleInfo info)
        {

        }

        private void ExitInfoShow()
        {

        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void ButtonEnabledChanged(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.FlatStyle = btn.Enabled ? FlatStyle.Flat : FlatStyle.Standard;
        }

        private void BtnFree_Click(object sender, EventArgs e)
        {

        }

        private void BtnFree_MouseEnter(object sender, EventArgs e)
        {
            cmsFreeSelected.Show(btnFree, new Point(0, btnFree.Height + btnFree.Margin.Top));
        }

    }
}
