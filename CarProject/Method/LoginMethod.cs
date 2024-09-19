 using CarProject.Common;
using CarProject.Models;
using SqlSugar;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarProject.Method
{
    public class LoginMethod
    {
        public readonly SqlSugarClient db;
        public LoginMethod(SqlSugarClient datadb)
        {
            this.db = datadb;
        }

        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <param name="ischek">是否勾选</param>
        /// <returns></returns>
        public bool Login(string account, string password, bool ischek,out Base_User base_User)
        {
            base_User = null;
            if (string.IsNullOrEmpty(account))
            {
                MessageBox.Show("请输入账号");
                
                return false;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("请输入密码");
               
                return false;
            }
            if (!ischek)
            {
                MessageBox.Show("请勾选同意《用户远程操作协议》");
              
                return false;
            }
             base_User = this.db.Queryable<Base_User>().Where(a => a.account == account).First();
            if (base_User == null)
            {
                MessageBox.Show("账号不存在，请先注册");
              
                return false;
            }


            var pwd = MD5Help.GetMD5Hash(password + "CarProject");
            if (pwd != base_User.password)
            {
                MessageBox.Show("密码错误，请重新输入");
               
                return false;
            }

            base_User.Loginnumber += 1;
            if (DateTime.Now.ToString("MM").ToInt() == base_User.loginmonth)
            {
                base_User.monthnumber += 1;
            }
            else
            {
                base_User.loginmonth = DateTime.Now.ToString("MM").ToInt();
                base_User.monthnumber =1;
            }
            base_User.logintime = DateTime.Now;
            this.db.Updateable(base_User).UpdateColumns(it => new {it.logintime,it.loginmonth,it.Loginnumber,it.monthnumber}).ExecuteCommand();
            return true;
        }
    }
}
