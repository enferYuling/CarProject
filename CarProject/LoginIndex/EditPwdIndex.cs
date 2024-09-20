using CarProject.Common;
using CarProject.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarProject.LoginIndex
{
    public partial class EditPwdIndex : Form
    {
        public readonly SqlSugarClient db;
        public EditPwdIndex(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(account_text.Text))
            {
               MessageBox.Show("请输入要修改的账号","提示");
                return;
            }
            var base_user = this.db.Queryable<Base_User>().Where(a => a.account == account_text.Text).First();
            if (base_user == null)
            {
                MessageBox.Show("该账号不存在，请先注册", "提示"); 
                return;
            }
            account_text.Visible = false;
            uiLabel1.Visible = false;
            search_btn.Visible = false;

            uiLabel2.Visible = true;
            uiLabel3.Visible = true;
            pwd_text.Visible = true;
            pwd1_text.Visible = true;
            srue_btn.Visible = true;
        }

        private void pwd1_text_TextChanged(object sender, EventArgs e)
        {
            var pwd1 = pwd_text.Text;
            var pwd2 = pwd1_text.Text;
            if (pwd1 != pwd2)
            {
                warn1.Visible = true;
            }
            else
            {
                warn1.Visible = false;
            }
            
        }

        private void srue_btn_Click(object sender, EventArgs e)
        {
            var pwdtext1 = pwd_text.Text;
            var pwdtext2 = pwd1_text.Text;

            if (pwdtext1 != pwdtext2)
            {

                MessageBox.Show("两次密码不正确，请重新输入", "提示");
                return;
            }
            bool containsDigitAndLetter = Regex.IsMatch(pwdtext1, @"^(?=.*[A-Za-z])(?=.*\d).+$");
            if (!containsDigitAndLetter)
            {
                MessageBox.Show("密码需包含字母和数字，请重新输入", "提示"); 
                return;
            }
            var base_user = this.db.Queryable<Base_User>().Where(a => a.account == account_text.Text).First();
            var pwd = MD5Help.GetMD5Hash(pwdtext1 + "CarProject");
            base_user.password = pwd;
            var query = this.db.Queryable<Base_Userpwd>().Where(a => a.userid == base_user.userid).First();
            query.password = pwd;
            query.oldpassword = pwdtext1;
            try
            {
                this.db.Updateable<Base_User>(base_user).ExecuteCommand();
                this.db.Updateable<Base_Userpwd>(query).ExecuteCommand();
                MessageBox.Show("修改成功");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void EditPwdIndex_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoginIndex login = new LoginIndex(db);
            login.Show();
        }
    }
}
