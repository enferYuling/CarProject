using CarProject.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public HomeIndex(SqlSugarClient datadb)
        {
            InitializeComponent();
            this.db = datadb;
        }
    }
}
