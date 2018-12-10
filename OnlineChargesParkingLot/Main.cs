using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using LogHelper;

namespace OnlineChargesParkingLot
{
    /// <summary>
    /// 这是一个测试
    /// </summary>
    public partial class Main : Form
    {
        /// <summary>
        /// 当前管理员信息
        /// </summary>
        private AdminInfo m_AdminInfo;

        private CameraController m_Camera;


        public Main(AdminInfo adminInfo)
        {
            m_AdminInfo = adminInfo;

            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            m_Camera = new CameraController(EnterDoor, ExitDoor);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void ExitDoor()
        {

        }

        private void EnterDoor()
        {

        }

    }
}
