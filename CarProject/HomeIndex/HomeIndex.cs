using CarProject.Common;
using CarProject.Method;
using CarProject.Models;
using SqlSugar;
using Sunny.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

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
        /// <summary>
        /// 首页方法
        /// </summary>
        public HomeMethod homemethod;
        public HomeIndex(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
            homemethod=new HomeMethod(db);
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
             network = NetworkInfo.TryGetRealNetworkInfo("");
             oldRate = network.GetIpv4Speed(); 
            stopwatch.Start();
            caozuo_timer.Start();
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        public void InitializationControl()
        {
            dczt_lab.Text = uiBattery1.Power.ToString() + "%";
            user_lab.Text +=string.IsNullOrEmpty(_User.account)?"":_User.account;
            dqsj_lab.Text= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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
           
            dqsj_lab.Text = dqsj_lab.Text.ToDateTime().AddSeconds(1).ToString("yyyy-MM-dd HH:mm:ss");
            elapsedTime = stopwatch.Elapsed;
            czsc_lab.Text = $"操作时长: {elapsedTime:hh\\时mm\\分}";
            NetWord();
        }
        #region 获取实时网速
        Rate oldRate;
        NetworkInfo network;
        /// <summary>
        /// 获取网速
        /// </summary>
        private void NetWord()
        {
          
            var newRate = network.GetIpv4Speed();
            var speed = NetworkInfo.GetSpeed(oldRate, newRate);
            oldRate = newRate;

            //xh_lab.Text = $"上传：{speed.Sent.Size} {speed.Sent.SizeType}/S    下载：{speed.Received.Size} {speed.Received.SizeType}/S";
            xh_lab.Text = $"{speed.Sent.Size}{speed.Sent.SizeType}/ S";
            switch (speed.Sent.SizeType)
            {
                case UnitType.B:
                    {
                        if (speed.Sent.Size <= 20)
                        {
                            uiSignal1.Level = 1;
                        }
                        else if(speed.Sent.Size <= 200)
                        {
                            uiSignal1.Level = 2;
                        }
                        else
                        {
                            uiSignal1.Level = 3;
                        }
                        
                    }
                    break;
                case UnitType.KB:
                    {
                        uiSignal1.Level = 4;

                    }
                    break;
                case UnitType.MB:
                    {
                        uiSignal1.Level = 5;
                    }
                    break;
            }
             
               
             
        }
        #endregion
        #region 电池1
        private TcpClient tcpClient;
        public async void dc1()
        {
            CONNECTfunction("192.168.2.101", 8886);
            NetworkCommunication1 communicator = new NetworkCommunication1();
            try
            {
                using (NetworkStream stream = tcpClient.GetStream())
                {
                    // 获取NetworkStream用于发送和接收
                    using (NetworkStream networkStream = tcpClient.GetStream())
                    {
                        while (true)
                        {
                            // 接收数据
                            byte[] receivedData = await homemethod.ReceiveBytesAsync(networkStream, 1024);
                            if (receivedData != null && receivedData.Length > 0)
                            {
                                // 处理接收到的数据
                                AddBytes(receivedData); // 添加到处理器                           
                                ProcessBufferedBytes();
                            }
                            else
                            {
                                MessageBox.Show("No data received or connection closed by the server.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                dczt_lab.Text = "电池异常";

                uiBattery1.Power = 0;
            }
        }
        private Queue<byte> buffer = new Queue<byte>(); // 用于存储接收到的字节
        private int groupSize = 13; // 分组大小
        public void ProcessBufferedBytes()
        {
            // 检查缓冲区中是否有足够的字节来形成一个组
            while (buffer.Count >= groupSize)
            {
                byte[] group = ExtractGroup();
                if (group != null)
                {
                    // 对提取的分组进行处理
                    ClassifyGroup(group);
                }
            }
        }
        public byte[] ExtractGroup()
        {
            if (buffer.Count < groupSize) return null; // 如果不足一个分组，返回null

            var group = new byte[groupSize];
            for (int i = 0; i < groupSize; i++)
            {
                group[i] = buffer.Dequeue();
            }
            return group;
        }
        private byte BMS = 244;
        private byte DPLY = 40;
        private byte MCU = 239;
        private byte MMC = 56;
        private byte CCS = 229;
        private byte BCA = 80;
        // 定义PF的范围
        private int VStart = 200; // 电压详细信息的报文的开始
        private int VEnd = 249;   // 电压详细信息的报文的结束

        private int TemperatureStart = 180;//温度详细信息报文的开始（各探头的采样值）
        private int TemperatureEnd = 199;//温度详细信息报文的结束（各探头的采样值）

        private byte MAX = 255;//PF的最大值
        private byte NextM = 254;
        private byte CJ2 = 250;
        private byte CJ1 = 245;
        private void ClassifyGroup(byte[] group)
        {

            //按照PS,SA和PF开始分类
            if (group[4] == BMS && group[3] == DPLY)
            {
                // 电压详细信息的报文
                if (group[2] >= VStart && group[2] <= VEnd)
                {
                    byte highByte1 = group[5]; // 索引从0开始，所以第6个字节是索引5
                    byte lowByte1 = group[6];  // 第7个字节是索引6
                    byte highByte2 = group[7];
                    byte lowByte2 = group[8];
                    byte highByte3 = group[9];
                    byte lowByte3 = group[10];
                    byte highByte4 = group[11];
                    byte lowByte4 = group[12];

                    // 将两个字节合并为UInt16
                    ushort combined1 = (ushort)((highByte1 << 8) | lowByte1);
                    ushort combined2 = (ushort)((highByte2 << 8) | lowByte2);
                    ushort combined3 = (ushort)((highByte3 << 8) | lowByte3);
                    ushort combined4 = (ushort)((highByte4 << 8) | lowByte4);
                    // 转换为10进制并输出或处理
                    int decimalValue1 = combined1;//string decimalValueString = combined.ToString();这将把combined 转换为一个表示其10进制值的字符串。
                    int decimalValue2 = combined2;
                    int decimalValue3 = combined3;
                    int decimalValue4 = combined4;
                }
                //温度详细报文（探头采样值）
                if (group[2] >= TemperatureStart && group[2] <= TemperatureEnd)
                {
                    // 直接将byte转换为int，已经是十进制数
                    int decimalValue1 = group[5]; //string decimalValueString = receivedByte.ToString(); // 将十进制数转换为字符串表示
                    int decimalValue2 = group[6];
                    int decimalValue3 = group[7];
                    int decimalValue4 = group[8];
                    int decimalValue5 = group[9];
                    int decimalValue6 = group[10];
                    int decimalValue7 = group[11];
                    int decimalValue8 = group[12];
                }
                //BMS基本信息报文1
                if (group[2] == MAX)
                {
                    byte highByte1 = group[8];//电池组充放电总电流高字节
                    byte lowByte1 = group[7];//电池组充放电总电流低字节
                    byte highByte2 = group[10];//电池组总电压高字节
                    byte lowByte2 = group[9];//电池组总电压低字节                   
                    ushort combined1 = (ushort)((highByte1 << 8) | lowByte1);//电池组充放电总电流
                    ushort combined2 = (ushort)((highByte2 << 8) | lowByte2);//电池组总电压

                    if ((group[5] & 0x01) != 0) //充电线是否连接，0未连接，1连接
                    {

                    }
                    else { }
                    if ((group[5] & 0x02) != 0) //电池组充电状态，0未充电，1充电
                    {
                        dczt_lab.Text = "正在充电"; 
                    }  
                    int SOC = group[6];//SOC的值
                                       // CompareSOC(SOC, 20, 80);
                                       //  string SOC1 = SOC.ToString();
                    if (!string.IsNullOrEmpty(dczt_lab.Text))
                    {
                        if (SOC == 100)
                        {
                            dczt_lab.Text = "已充满";
                           
                        }
                        uiBattery1.Power = SOC; 
                    }
                    else
                    {
                        dczt_lab.Text = SOC.ToString() + "%";//SOC
                        uiBattery1.Power = SOC; 
                    }

                }
                //BMS基本信息报文2
                if (group[2] == NextM)
                {
                    byte highByte1 = group[6];//最高单体电压高字节
                    byte lowByte1 = group[5];//最高单体电压低字节
                    byte highByte2 = group[8];//最低单体电压高字节
                    byte lowByte2 = group[7];//最低单体电压低字节
                    byte highByte3 = group[12];//最大允许放电电流高字节
                    byte lowByte3 = group[11];//最大允许放电电流低字节

                    ushort combined1 = (ushort)((highByte1 << 8) | lowByte1);//最高单体电压
                    ushort combined2 = (ushort)((highByte2 << 8) | lowByte2);//最低单体电压
                    ushort combined3 = (ushort)((highByte3 << 8) | lowByte3);//最大允许放电电流




                    int decimalValue4 = group[9];//单体最高温度
                    double SMAXT = 1 * decimalValue4;
                    string SMAXT1 = SMAXT.ToString();
                    dcwd1_lab.Text = "电池1温度：" + SMAXT1 + "℃";//单体最高温度

                    //int decimalValue5 = group[10];//单体最低温度
                    //double SMINT = 0.001 * decimalValue5;
                    //string SMINT1 = SMINT.ToString();
                    //TextBoxSMINT.Text = "单体最低温度" + SMINT1 + "℃";//单体最低温度
                }
            }
            //充电需求报文
            if (group[4] == BMS && group[3] == CCS)
            {
                if (group[2] == MAX)
                {
                    byte highByte1 = group[5];//最高允许充电端电压高字节
                    byte lowByte1 = group[6];//最高允许充电端电压低字节
                    byte highByte2 = group[7];//最高允许充电电流高字节
                    byte lowByte2 = group[8];//最高允许充电电流低字节

                    ushort combined1 = (ushort)((highByte1 << 8) | lowByte1);//最高允许充电端电压
                    ushort combined2 = (ushort)((highByte2 << 8) | lowByte2);//最高允许充电电流

                    int decimalValue1 = combined1;//最高允许充电端电压
                    double YXmaxv = 0.1 * decimalValue1;
                    int decimalValue2 = combined2;//最高允许充电电流
                    double YXmaxA = 0.1 * decimalValue2;
                    if ((group[9] & 0x01) != 0) //充电机是否开始充电。0开是充电，1电池保护，关闭充电
                    {

                    }
                    else { }
                }
            }
            //充电机反馈信息报文
            if (group[4] == CCS && group[3] == BCA)
            {
                if (group[2] == MAX)
                {
                    byte highByte1 = group[5];//输出电压高字节
                    byte lowByte1 = group[6];//输出电压低字节
                    byte highByte2 = group[7];//输出电流高字节
                    byte lowByte2 = group[8];//输出电流低字节
                    ushort combined1 = (ushort)((highByte1 << 8) | lowByte1);//输出电压
                    ushort combined2 = (ushort)((highByte2 << 8) | lowByte2);//输出电流
                    int decimalValue1 = combined1;//输出电压
                    double OUTPUTV = 0.1 * decimalValue1;
                    int decimalValue2 = combined2;//输出电流
                    double OUTPUTA = 0.1 * decimalValue2;
                    if ((group[9] & 0x01) != 0) //硬件故障。0正常，1硬件故障
                    {

                    }
                    else { }
                    if ((group[9] & 0x02) != 0) //充电机温度故障，0正常，1充电机温度过高保护
                    {

                    }
                    else { }
                    if ((group[9] & 0x04) != 0) //低压限功率模式，0输入电压正常，1输入电压偏低，进入低功率模式
                    {

                    }
                    else { }
                    if ((group[9] & 0x08) != 0) //输入电压状态，0输入电压正常，1输入过压或欠压故障
                    {

                    }
                    else { }
                    if ((group[9] & 0x10) != 0) //输出过流，0输出电流正常，1输出过流
                    {

                    }
                    else { }
                    if ((group[9] & 0x20) != 0) //启动状态，0充电机关闭状态，1处于充电状态
                    {

                    }
                    else { }
                    if ((group[9] & 0x40) != 0) //通信状态，0通信正常，1通信接收超时
                    {

                    }
                    else { }
                    if ((group[9] & 0x80) != 0) //电池连接状态，0电池连接正常，1电池反接或未接
                    {

                    }
                    else { }
                }
            }
            //电机控制器与仪表通讯报文
            if (group[4] == MCU && group[3] == MMC)
            {
                //电机控制器与仪表通讯报文1
                if (group[2] == CJ1)
                {
                    if ((group[9] & 0x01) != 0) //挡位状态。00无效，01前进，10后退
                    {
                        if ((group[9] & 0x02) == 0) //
                        {

                        }
                    }
                    else
                    {
                        if ((group[9] & 0x02) != 0) //
                        {

                        }
                    }
                    if ((group[9] & 0x04) != 0) //刹车状态，0无刹车，1有刹车
                    {

                    }
                    else { }
                    if ((group[9] & 0x08) != 0) //运行模式，00默认模式，01经济模式，10高速模式
                    {
                        if ((group[9] & 0x10) == 0)
                        {

                        }
                    }
                    else
                    {
                        if ((group[9] & 0x10) != 0)
                        {

                        }
                        else { }
                    }
                    if ((group[9] & 0x20) != 0) //控制器状态，0 not ready，1 ready
                    {

                    }
                    else { }
                    if ((group[9] & 0x40) != 0) //限功耗状态，0正常运行，1降功率运行
                    {

                    }
                    else { }

                    int decimalValue1 = group[6];//控制器故障代码
                    byte highByte1 = group[8];//电机转速高字节
                    byte lowByte1 = group[7];//电机转速低字节
                    byte highByte2 = group[10];//小计里程高字节
                    byte lowByte2 = group[9];//小计里程低字节

                    ushort combined1 = (ushort)((highByte1 << 8) | lowByte1);//电机转速
                    ushort combined2 = (ushort)((highByte2 << 8) | lowByte2);//小计里程

                    int decimalValue2 = combined1;
                    int decimalValue3 = combined2;
                    double XJLC = 0.1 * decimalValue3;
                    int decimalValue4 = group[11];//车速
                }
                //电机控制器与仪表通讯报文2
                if (group[2] == CJ2)
                {
                    byte highByte1 = group[6];//电池电压高字节
                    byte lowByte1 = group[5];//电池电压低字节
                    byte highByte2 = group[8];//电机电流高字节
                    byte lowByte2 = group[7];//电机电流低字节
                    byte highByte3 = group[10];//电机温度高字节
                    byte lowByte3 = group[9];//电机温度低字节
                    byte highByte4 = group[12];//控制器温度高字节
                    byte lowByte4 = group[11];//控制器温度低字节

                    ushort combined1 = (ushort)((highByte1 << 8) | lowByte1);//电池电压
                    ushort combined2 = (ushort)((highByte2 << 8) | lowByte2);//电机电流
                    ushort combined3 = (ushort)((highByte3 << 8) | lowByte3);//电机温度
                    ushort combined4 = (ushort)((highByte4 << 8) | lowByte4);//控制器温度

                    int decimalValue1 = combined1;//电池电压
                    double DCV = 0.1 * decimalValue1;
                    int decimalValue2 = combined2;//电机电流
                    double DCA = 0.1 * decimalValue2;
                    int decimalValue3 = combined3;//电机温度
                    double DJT = 0.1 * decimalValue3;
                    int decimalValue4 = combined4;//控制器温度
                    double KZQT = 0.1 * decimalValue4;
                }
            }
        }
        private NetworkStream networkStream;
        //连接函数
        private void CONNECTfunction(string ip, int Desport)
        {
            string ipAddress = ip;
            int port = Desport;
            try
            {
                // 关闭 NetworkStream
                if (networkStream != null)
                {
                    networkStream.Close();
                    networkStream.Dispose();
                }
                // 断开 TcpClient 连接并释放资源
                if (tcpClient != null && tcpClient.Connected)
                {
                    tcpClient.Close();
                }
            }
            catch (Exception ex)
            {
                // 处理关闭连接时可能出现的异常
                MessageBox.Show("关闭连接时发生错误: " + ex.Message);
            }
            finally
            {
                // 确保 TcpClient 和 NetworkStream 被设置为 null 或重置状态
                tcpClient = null;
                networkStream = null;
            }
            try
            {
                if (tcpClient == null || !tcpClient.Connected)
                {
                    // 连接到硬件设备
                    tcpClient = new TcpClient();
                    tcpClient.Connect(ipAddress, port);
                    networkStream = tcpClient.GetStream();

                }
                else
                {
                    // 如果连接已存在，可以跳过或提醒
                    MessageBox.Show("已存在活动的连接。");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接失败!" + ex.Message);
            }
        }
        public void AddBytes(byte[] newBytes)
        {

            foreach (byte b in newBytes)
            {
                buffer.Enqueue(b);
            }
        }
        #endregion

        private void cd_timer_Tick(object sender, EventArgs e)
        {
            if (uiBattery1.Power == 100)
            {
                if (dczt_lab.Text == "正在充电")
                {
                    uiBattery1.Power = 0;
                }
            }
            else
            {
                if (dczt_lab.Text == "正在充电")
                {
                    uiBattery1.Power += 20;
                }
            }
        }
    }
}
