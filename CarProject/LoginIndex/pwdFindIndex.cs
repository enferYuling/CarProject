using CarProject.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarProject.LoginIndex
{
    public partial class pwdFindIndex : Form
    {
        public readonly SqlSugarClient db;
        public pwdFindIndex(SqlSugarClient client)
        {
            InitializeComponent();
            this.db = client;
        }

        private void find_btn_Click(object sender, EventArgs e)
        {
            #region 检查非空
            if (string.IsNullOrEmpty(account_text.Text))
            {
                warn1.Visible = true;
                return;
            }else if (string.IsNullOrEmpty(realname_text.Text))
            {
                warn2.Visible = true;
                return;
            }
            else if (string.IsNullOrEmpty(address_text.Text))
            {
                warn3.Visible = true;
                return;
            }
            else
            {
                warn1.Visible = false;
                warn2.Visible = false;
                warn3.Visible = false;
            }
            #endregion
            Base_User _User=this.db.Queryable<Base_User>().Where(a=>a.account==account_text.Text&&a.realname==realname_text.Text&&a.address== address_text.Text).First();
            if (_User == null)
            {
                MessageBox.Show("账号不存在或账号信息不符","提示");
                return;
            }
            Base_Userpwd _Userpwd=this.db.Queryable<Base_Userpwd>().Where(a=>a.userid==_User.userid).First();
            if (_Userpwd != null)
            {
                MessageBox.Show("账号原密码(未加密)为：【"+_Userpwd.oldpassword+"】", "结果");
            }
        }
    }
}
