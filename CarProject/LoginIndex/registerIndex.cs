using CarProject.Common;
using CarProject.Models;
using SqlSugar;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarProject.LoginIndex
{
    public partial class registerIndex : Form
    {
        public readonly SqlSugarClient db;
        public registerIndex(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }

        private void Submit_btn_Click(object sender, EventArgs e)
        {

        }

        private void pwd1_text_TextChanged(object sender, EventArgs e)
        {
            var pwdtext1 = pwd_text.Text;
            var pwdtext2 = pwd1_text.Text;
            if (pwdtext1 != pwdtext2)
            {
                warn3.Visible = true;
               
            }
            else if (string.IsNullOrEmpty(pwdtext2))
            {
                warn3.Visible = false;
                
            }
            else
            {
                warn3.Visible = false;
                
            }
        }

        private void Submit_btn_Click_1(object sender, EventArgs e)
        {
            #region 检查非空
            if (string.IsNullOrEmpty(account_text.Text))
            {
                warn1.Visible = true;
            }else if (string.IsNullOrEmpty(pwd_text.Text))
            {
                warn2.Visible = true;
            }
            else if (string.IsNullOrEmpty(pwd1_text.Text))
            {
                warn3.Visible = true;
            }
            else if (string.IsNullOrEmpty(address_text.Text))
            {
                warn4.Visible = true;
            }
            else if (role_ComboBox.SelectedIndex==-1)
            {
                warn5.Visible = true;
            }
            else
            {
                warn1.Visible = false;
                warn2.Visible = false;
                warn3.Visible = false;
                warn4.Visible = false;
                warn5.Visible = false;
            }
            if(warn1.Visible|| warn2.Visible|| warn3.Visible|| warn4.Visible|| warn5.Visible)
            {
                return;
            }
            #endregion


            var pwdtext1 = pwd_text.Text;
            var pwdtext2 = pwd1_text.Text;

            if (pwdtext1 != pwdtext2)
            {
                warn3.Visible = true;
               
                return;
            } 
            else
            {
                warn3.Visible = false;
            }
            Base_User base_User = this.db.Queryable<Base_User>().Where(a => a.account == account_text.Text).First();
            if (base_User != null)
            {
                MessageBox.Show.Text = "该账号已存在";
                return;
            }
            base_User = new Base_User();
            base_User.account = account_text.Text.Trim();
            base_User.realname = realName_text.Text;
            base_User.mobile = Mobile_text.Text;
            base_User.email = Email_text.Text;
            base_User.address = address_text.Text.Trim();
            base_User.issystem = issystemCheckBox.Checked ? "1" : "0";
            CodeHelp codeHelp = new CodeHelp(db);

            if (string.IsNullOrEmpty(base_User.account))
            {
                tips_lab.Text = "用户账号不能为空";
                return;
            }
            if (string.IsNullOrEmpty(password_text.Text))
            {
                tips_lab.Text = "用户密码不能为空";
                return;
            }
            if (string.IsNullOrEmpty(realName_text.Text))
            {
                tips_lab.Text = "真实姓名不能为空";
                return;
            }
            bool containsDigitAndLetter = Regex.IsMatch(password_text.Text, @"^(?=.*[A-Za-z])(?=.*\d).+$");
            if (!containsDigitAndLetter)
            {
                tips_lab.Text = "密码需包含字母和数字";
                return;
            }
            try
            {
                base_User.Create();
                base_User.createusername = realName_text.Text;
                if (!string.IsNullOrEmpty(account))
                {
                    base_User.createuserid = userid;
                    base_User.createusername = realName;
                }
                var pwd = MD5Help.GetMD5Hash(password_text.Text + "CarProject");
                base_User.password = pwd;
                string code = codeHelp.encoded(base_User.account, DateTime.Now, "base_User");
                base_User.usercode = code;
                this.db.Insertable<Base_User>(base_User).ExecuteCommand();
                Base_Userpwd base_Userpwd = new Base_Userpwd();
                base_Userpwd.Create();
                base_Userpwd.userid = base_User.userid;
                base_Userpwd.password = pwd;
                base_Userpwd.oldpassword = password_text.Text;
                base_User.logintime = DateTime.Now;
                this.db.Insertable<Base_Userpwd>(base_Userpwd).ExecuteCommand();
                //角色用户
                var roleid = this.db.Queryable<Base_Role>().Where(a => a.RoleName == js_text.Text).First().RoleId;
                Base_Role_User base_Role_User = new Base_Role_User();
                base_Role_User.Create();
                base_Role_User.RoleId = roleid;
                base_Role_User.UserId = base_User.userid;
                this.db.Insertable<Base_Role_User>(base_Role_User).ExecuteCommand();
                if (string.IsNullOrEmpty(account))
                {
                    MessageBox.Show("注册成功");
                    this.Hide();
                    Home.Home home = new Home.Home(db);
                    home.account = base_User.account;
                    home.realName = base_User.realname;
                    home.UserId = base_User.userid;
                    home.Show();
                }
                else
                {
                    MessageBox.Show("保存成功");
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                tips_lab.Text = ex.Message;
            }
        }
    }
}
