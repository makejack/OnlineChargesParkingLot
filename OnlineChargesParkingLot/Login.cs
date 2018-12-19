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
using WinFormAnimation;

namespace OnlineChargesParkingLot
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            //窗体设置为透明的，启动时有一个动画效果
            this.Opacity = 0;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //动画效果
            new Animator(new Path((float)this.Opacity, 1, 250)).Play(this, "Opacity");
        }

        private void AccountText_Enter(object sender, EventArgs e)
        {
            if (tbAccount.Text.Equals("Account"))
            {
                tbAccount.Text = string.Empty;
                tbAccount.ForeColor = Color.Black;
            }
        }

        private void AccountText_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbAccount.Text))
            {
                tbAccount.Text = "Account";
                tbAccount.ForeColor = Color.Gray;
            }
        }

        private void PasswordText_Enter(object sender, EventArgs e)
        {
            if (tbPassword.Text.Equals("Password"))
            {
                tbPassword.Text = string.Empty;
                tbPassword.ForeColor = Color.Black;
                tbPassword.PasswordChar = '●';
            }
        }

        private void PasswordText_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbPassword.Text))
            {
                tbPassword.Text = "Password";
                tbPassword.ForeColor = Color.Gray;
                tbPassword.PasswordChar = '\0';
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_Click(object sender, EventArgs e)
        {
            string strAccount = tbAccount.Text;
            string strPassword = tbPassword.Text;

            if (strPassword == "Password")
            {
                strPassword = string.Empty;
            }

            if (strAccount == "Account" || strAccount.Length == 0)
            {
                MessageBox.Show("帐号不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (strAccount.ToUpper() != "ADMIN")
            {
                if (strPassword.Length == 0)
                {
                    MessageBox.Show("密码不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            IAdminInfoService adminInfoService = BLL.Container.Container.Resolve<IAdminInfoService>();
            AdminInfo adminInfo = adminInfoService.Query(strAccount, strPassword);
            if (adminInfo == null)
            {
                MessageBox.Show("帐号或密码错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Tag = adminInfo;
            this.DialogResult = DialogResult.OK;
        }
    }
}
