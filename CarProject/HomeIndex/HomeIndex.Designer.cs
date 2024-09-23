namespace CarProject.HomeIndex
{
    partial class HomeIndex
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeIndex));
            this.uiTabControlMenu1 = new Sunny.UI.UITabControlMenu();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.dqsj_lab = new Sunny.UI.UILabel();
            this.Nav_table = new Sunny.UI.UITableLayoutPanel();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.uiNavBar1 = new Sunny.UI.UINavBar();
            this.uiTabControlMenu1.SuspendLayout();
            this.Nav_table.SuspendLayout();
            this.uiNavBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiTabControlMenu1
            // 
            this.uiTabControlMenu1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.uiTabControlMenu1.Controls.Add(this.tabPage1);
            this.uiTabControlMenu1.Controls.Add(this.tabPage2);
            this.uiTabControlMenu1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTabControlMenu1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.uiTabControlMenu1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTabControlMenu1.Location = new System.Drawing.Point(0, 63);
            this.uiTabControlMenu1.Multiline = true;
            this.uiTabControlMenu1.Name = "uiTabControlMenu1";
            this.uiTabControlMenu1.SelectedIndex = 0;
            this.uiTabControlMenu1.Size = new System.Drawing.Size(1121, 707);
            this.uiTabControlMenu1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.uiTabControlMenu1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(201, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(920, 707);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(201, 0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(920, 706);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // uiLabel1
            // 
            this.uiLabel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiLabel1.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.ForeColor = System.Drawing.Color.White;
            this.uiLabel1.Image = global::CarProject.Properties.Resources.favicon;
            this.uiLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel1.Location = new System.Drawing.Point(0, 0);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(84, 63);
            this.uiLabel1.TabIndex = 1;
            this.uiLabel1.Text = "作业车";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dqsj_lab
            // 
            this.dqsj_lab.Font = new System.Drawing.Font("楷体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dqsj_lab.ForeColor = System.Drawing.Color.White;
            this.dqsj_lab.Location = new System.Drawing.Point(3, 0);
            this.dqsj_lab.Name = "dqsj_lab";
            this.dqsj_lab.Size = new System.Drawing.Size(127, 30);
            this.dqsj_lab.TabIndex = 2;
            this.dqsj_lab.Text = "当前时间:";
            this.dqsj_lab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Nav_table
            // 
            this.Nav_table.BackColor = System.Drawing.Color.Transparent;
            this.Nav_table.ColumnCount = 8;
            this.Nav_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.82546F));
            this.Nav_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.65767F));
            this.Nav_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.92189F));
            this.Nav_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.353906F));
            this.Nav_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.Nav_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.Nav_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.Nav_table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.Nav_table.Controls.Add(this.uiLabel3, 2, 0);
            this.Nav_table.Controls.Add(this.uiLabel2, 1, 0);
            this.Nav_table.Controls.Add(this.dqsj_lab, 0, 0);
            this.Nav_table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Nav_table.ForeColor = System.Drawing.Color.White;
            this.Nav_table.Location = new System.Drawing.Point(84, 0);
            this.Nav_table.Name = "Nav_table";
            this.Nav_table.RowCount = 2;
            this.Nav_table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Nav_table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Nav_table.Size = new System.Drawing.Size(1037, 63);
            this.Nav_table.TabIndex = 2;
            this.Nav_table.TagString = null;
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("楷体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.ForeColor = System.Drawing.Color.White;
            this.uiLabel2.Location = new System.Drawing.Point(136, 0);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(146, 30);
            this.uiLabel2.TabIndex = 3;
            this.uiLabel2.Text = "车辆编号:";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel3
            // 
            this.uiLabel3.Font = new System.Drawing.Font("楷体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel3.ForeColor = System.Drawing.Color.White;
            this.uiLabel3.Location = new System.Drawing.Point(288, 0);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(128, 30);
            this.uiLabel3.TabIndex = 4;
            this.uiLabel3.Text = "操作时长:";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiNavBar1
            // 
            this.uiNavBar1.Controls.Add(this.Nav_table);
            this.uiNavBar1.Controls.Add(this.uiLabel1);
            this.uiNavBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiNavBar1.DropMenuFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiNavBar1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiNavBar1.Location = new System.Drawing.Point(0, 0);
            this.uiNavBar1.Name = "uiNavBar1";
            this.uiNavBar1.Size = new System.Drawing.Size(1121, 63);
            this.uiNavBar1.TabIndex = 1;
            this.uiNavBar1.Text = "uiNavBar1";
            // 
            // HomeIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1121, 770);
            this.Controls.Add(this.uiTabControlMenu1);
            this.Controls.Add(this.uiNavBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "HomeIndex";
            this.Text = "首页";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HomeIndex_FormClosed);
            this.uiTabControlMenu1.ResumeLayout(false);
            this.Nav_table.ResumeLayout(false);
            this.uiNavBar1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UITabControlMenu uiTabControlMenu1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UITableLayoutPanel Nav_table;
        private Sunny.UI.UILabel dqsj_lab;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UINavBar uiNavBar1;
    }
}