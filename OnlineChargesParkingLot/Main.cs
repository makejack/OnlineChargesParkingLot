using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Camera;
using Camera.Model;
using LogHelper;

namespace OnlineChargesParkingLot
{
    /// <summary>
    /// 这是一个测试
    /// </summary>
    public partial class Main : Form
    {
        private Factory m_CameraFactory;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                m_CameraFactory = new Factory
                {
                    ImagePath = Application.StartupPath + @"\Imgs"
                };
                m_CameraFactory.RegisterFind(FindCamera);
                m_CameraFactory.RegisterReceived(PlateReceived);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                m_CameraFactory.UnInit();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        private void FindCamera(object sender , CameraEventArgs e)
        {
            
        }

        private void PlateReceived(object sender, PlateEventArgs e)
        {
            
        }
    }
}
