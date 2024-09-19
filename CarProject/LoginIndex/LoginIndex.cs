﻿using CarProject.Method;
using CarProject.Models;
using SqlSugar;
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
                account_text.Text = string.Empty;
                pwd_text.Text = string.Empty;
                checkBox1.Checked = false;
                this.Show();
            }
            else
            {

                pwd_text.Text = string.Empty;
            }
        }

        private void LoginIndex_Load(object sender, EventArgs e)
        {
            loginMethod = new LoginMethod(db);
        }
    }
}
