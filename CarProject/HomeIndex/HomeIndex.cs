using CarProject.Common;
using CarProject.Models;
using SqlSugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarProject.HomeIndex
{
    public partial class HomeIndex : Form
    {
        public readonly SqlSugarClient db;
        public Base_User _User;
        /// <summary>
        /// 操作时长
        /// </summary>
        private TimeSpan elapsedTime;
        /// <summary>
        /// 程序运行时间
        /// </summary>
        private Stopwatch stopwatch;
        public HomeIndex(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
            
        }

        private void HomeIndex_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoginIndex.LoginIndex login = new LoginIndex.LoginIndex(db);
            login.Show();
        }

        private void HomeIndex_Load(object sender, EventArgs e)
        {
            //if(_User == null)
            //{
            //    MessageBox.Show("未登录");
            //    return;
            //}
            InitializationControl();
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        public void InitializationControl()
        {
            dczt_lab.Text = uiBattery1.Power.ToString() + "%";
            user_lab.Text +=string.IsNullOrEmpty(_User.account)?"":_User.account;
            dqsj_lab.Text= DateTime.Now.ToString("yyyy年MM月dd日 HH点mm分ss秒");
            stopwatch = new Stopwatch();
            czsc_lab.Text = $"操作时长: {elapsedTime:hh\\时mm\\分}";
             
        }
        /// <summary>
        /// 操作计时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void caozuo_timer_Tick(object sender, EventArgs e)
        {
            stopwatch.Start();
            dqsj_lab.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH点mm分ss秒");
            elapsedTime = stopwatch.Elapsed;
            czsc_lab.Text = $"操作时长: {elapsedTime:hh\\时mm\\分}";
        }
        #region 获取实时网速
        private ArrayList adapters;
        NetworkAdapter workAdapter = null;
        /// <summary>
        /// 获取网速
        /// </summary>
        private void NetWord()
        {
            var receiveCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", "Ethernet");
            var sendCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", "Ethernet");

            while (true)
            {
                float received = receiveCounter.NextValue();
                float sent = sendCounter.NextValue();

               //la$"Received: {received} B/s");
               // Console.WriteLine($"Sent: {sent} B/s");

                // 为了避免过于频繁的输出，可以添加延时
                System.Threading.Thread.Sleep(1000);
            }
        }
        #endregion
    }
}
