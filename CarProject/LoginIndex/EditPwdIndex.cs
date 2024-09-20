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
    }
}
