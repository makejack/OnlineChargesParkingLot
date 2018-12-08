using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OnlineChargesParkingLot
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
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
            }
        }

        private void PasswordText_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbPassword.Text))
            {
                tbPassword.Text = "Password";
                tbPassword.ForeColor = Color.Gray;
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
