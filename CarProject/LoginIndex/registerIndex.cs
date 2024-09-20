using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
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

        }
    }
}
