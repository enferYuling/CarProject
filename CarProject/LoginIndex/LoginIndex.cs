using CarProject.Method;
using CarProject.Models;
using SqlSugar;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarProject.LoginIndex
{
    public partial class LoginIndex : Form
    {
        public readonly SqlSugarClient db;
        public LoginMethod loginMethod;
        public LoginIndex(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }

        private  void Login_btn_Click(object sender, EventArgs e)
        {
          
            var isok = loginMethod.Login(account_text.Text, pwd_text.Text, checkBox1.Checked, out Base_User _User);
            if (isok)
            {
                HomeIndex.HomeIndex from = new HomeIndex.HomeIndex(db);
                from._User = _User;
                this.Hide();
                from.Show();
                
            }
            else
            {

                pwd_text.Text = string.Empty;
            }
        }

        private void LoginIndex_Load(object sender, EventArgs e)
        {
            account_text.Focus();
            loginMethod = new LoginMethod(db);
        }

        private void register_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            registerIndex registerIndex = new registerIndex(db);
            registerIndex.Show();
            
        }

        private void editpwd_link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            EditPwdIndex index = new EditPwdIndex(db);

            index.Show();
            
        }

        private void pwdfind_link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            pwdFindIndex index = new pwdFindIndex(db); 
            index.Show();
        }

        private void LoginIndex_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void account_text_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter||e.KeyCode==Keys.Down)
            {
                pwd_text.Focus();
            }
            
        }

        private void pwd_text_KeyUp(object sender, KeyEventArgs e)
        {
            if ( e.KeyCode == Keys.Up)
            {
                account_text.Focus();
            }
        }
    }
}
