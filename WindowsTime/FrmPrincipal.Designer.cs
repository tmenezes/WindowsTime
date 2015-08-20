
namespace WindowsTime
{
    partial class FrmPrincipal
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 39D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 18D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 15D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 12D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint5 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 8D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint6 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 4.5D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint7 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 3.20000004768372D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint8 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 2D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint9 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 1D);
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gridProgramas = new System.Windows.Forms.DataGridView();
            this.ColIcone = new System.Windows.Forms.DataGridViewImageColumn();
            this.colPrograma = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTempo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colJanelas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.abrirTrayMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.previewTrayMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.sairTrayMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.picExportar = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chkSempreVisivel = new System.Windows.Forms.CheckBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProgramas)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExportar)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(223)))), ((int)(((byte)(240)))));
            this.chart1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            this.chart1.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chart1.BorderlineWidth = 2;
            this.chart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Emboss;
            chartArea1.Area3DStyle.IsClustered = true;
            chartArea1.Area3DStyle.IsRightAngleAxes = false;
            chartArea1.Area3DStyle.PointGapDepth = 900;
            chartArea1.Area3DStyle.Rotation = 162;
            chartArea1.Area3DStyle.WallWidth = 25;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisX2.MajorGrid.Enabled = false;
            chartArea1.AxisX2.MajorTickMark.Enabled = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.MajorTickMark.Enabled = false;
            chartArea1.AxisY2.MajorGrid.Enabled = false;
            chartArea1.AxisY2.MajorTickMark.Enabled = false;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea1.BackSecondaryColor = System.Drawing.Color.Transparent;
            chartArea1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.Name = "Area1";
            chartArea1.ShadowColor = System.Drawing.Color.Transparent;
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.IsSoftShadows = false;
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Bold);
            legend1.IsEquallySpacedItems = true;
            legend1.IsTextAutoFit = false;
            legend1.Name = "Default";
            legend1.TextWrapThreshold = 30;
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(9, 135);
            this.chart1.Margin = new System.Windows.Forms.Padding(0);
            this.chart1.Name = "chart1";
            series1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            series1.ChartArea = "Area1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(65)))), ((int)(((byte)(140)))), ((int)(((byte)(240)))));
            series1.CustomProperties = "CollectedLabel=Other, MinimumRelativePieSize=20, DoughnutRadius=25, PieDrawingSty" +
    "le=Concave";
            series1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            series1.Label = "#PERCENT{P1}";
            series1.Legend = "Default";
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "Series1";
            dataPoint1.CustomProperties = "OriginalPointIndex=0";
            dataPoint1.LegendText = "RUS";
            dataPoint2.CustomProperties = "OriginalPointIndex=1";
            dataPoint2.LegendText = "CAN";
            dataPoint3.CustomProperties = "OriginalPointIndex=2";
            dataPoint3.LegendText = "USA";
            dataPoint4.CustomProperties = "OriginalPointIndex=3";
            dataPoint4.LegendText = "PRC";
            dataPoint5.CustomProperties = "OriginalPointIndex=5";
            dataPoint5.LegendText = "DEN";
            dataPoint6.LegendText = "AUS";
            dataPoint7.CustomProperties = "OriginalPointIndex=4";
            dataPoint7.LegendText = "IND";
            dataPoint8.LegendText = "ARG";
            dataPoint9.LegendText = "FRA";
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            series1.Points.Add(dataPoint3);
            series1.Points.Add(dataPoint4);
            series1.Points.Add(dataPoint5);
            series1.Points.Add(dataPoint6);
            series1.Points.Add(dataPoint7);
            series1.Points.Add(dataPoint8);
            series1.Points.Add(dataPoint9);
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(615, 298);
            this.chart1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(633, 86);
            this.panel1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe WP", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(87, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(447, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sua ferramenta de produtividade que mede tempo de utilização dos seus aplicativos" +
    "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe WP", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(87, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "WindowsTime";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WindowsTime.Properties.Resources.android_icon_72x72;
            this.pictureBox1.Location = new System.Drawing.Point(8, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(72, 72);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(69)))));
            this.label3.Location = new System.Drawing.Point(6, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(206, 31);
            this.label3.TabIndex = 3;
            this.label3.Text = "Atividade do dia";
            // 
            // gridProgramas
            // 
            this.gridProgramas.AllowUserToAddRows = false;
            this.gridProgramas.AllowUserToDeleteRows = false;
            this.gridProgramas.AllowUserToResizeColumns = false;
            this.gridProgramas.AllowUserToResizeRows = false;
            this.gridProgramas.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.gridProgramas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProgramas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColIcone,
            this.colPrograma,
            this.colTempo,
            this.colJanelas});
            this.gridProgramas.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridProgramas.Location = new System.Drawing.Point(12, 436);
            this.gridProgramas.Name = "gridProgramas";
            this.gridProgramas.ReadOnly = true;
            this.gridProgramas.RowHeadersVisible = false;
            this.gridProgramas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridProgramas.Size = new System.Drawing.Size(609, 247);
            this.gridProgramas.TabIndex = 4;
            this.gridProgramas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridProgramas_CellDoubleClick);
            this.gridProgramas.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gridProgramas_DataError);
            // 
            // ColIcone
            // 
            this.ColIcone.DataPropertyName = "Icone";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle1.NullValue")));
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
            this.ColIcone.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColIcone.HeaderText = "";
            this.ColIcone.Name = "ColIcone";
            this.ColIcone.ReadOnly = true;
            this.ColIcone.Width = 40;
            // 
            // colPrograma
            // 
            this.colPrograma.DataPropertyName = "Nome";
            this.colPrograma.HeaderText = "Programa";
            this.colPrograma.Name = "colPrograma";
            this.colPrograma.ReadOnly = true;
            this.colPrograma.Width = 250;
            // 
            // colTempo
            // 
            this.colTempo.DataPropertyName = "TempoFormatado";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colTempo.DefaultCellStyle = dataGridViewCellStyle2;
            this.colTempo.HeaderText = "Tempo de Utilização";
            this.colTempo.Name = "colTempo";
            this.colTempo.ReadOnly = true;
            this.colTempo.Width = 148;
            // 
            // colJanelas
            // 
            this.colJanelas.DataPropertyName = "TotalJanelas";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colJanelas.DefaultCellStyle = dataGridViewCellStyle3;
            this.colJanelas.HeaderText = "Abas/Janelas Visitadas";
            this.colJanelas.Name = "colJanelas";
            this.colJanelas.ReadOnly = true;
            this.colJanelas.Width = 148;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirTrayMenu,
            this.previewTrayMenu,
            this.toolStripSeparator1,
            this.sairTrayMenu});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 76);
            this.contextMenuStrip1.Text = "Abrir...";
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // abrirTrayMenu
            // 
            this.abrirTrayMenu.Name = "abrirTrayMenu";
            this.abrirTrayMenu.Size = new System.Drawing.Size(124, 22);
            this.abrirTrayMenu.Tag = "1";
            this.abrirTrayMenu.Text = "&Abrir...";
            // 
            // previewTrayMenu
            // 
            this.previewTrayMenu.Name = "previewTrayMenu";
            this.previewTrayMenu.Size = new System.Drawing.Size(124, 22);
            this.previewTrayMenu.Tag = "2";
            this.previewTrayMenu.Text = "&Preview...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(121, 6);
            // 
            // sairTrayMenu
            // 
            this.sairTrayMenu.Name = "sairTrayMenu";
            this.sairTrayMenu.Size = new System.Drawing.Size(124, 22);
            this.sairTrayMenu.Tag = "3";
            this.sairTrayMenu.Text = "&Fechar";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "O WindowsTime está observando sua produtividade em segundo plano...";
            this.notifyIcon1.BalloonTipTitle = "Windows Time";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Windows Time";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // picExportar
            // 
            this.picExportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picExportar.Image = global::WindowsTime.Properties.Resources._64px_Sharethis_svg;
            this.picExportar.Location = new System.Drawing.Point(585, 96);
            this.picExportar.Name = "picExportar";
            this.picExportar.Size = new System.Drawing.Size(36, 36);
            this.picExportar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picExportar.TabIndex = 3;
            this.picExportar.TabStop = false;
            this.toolTip1.SetToolTip(this.picExportar, "Exportar dados...");
            this.picExportar.Click += new System.EventHandler(this.picExportar_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // chkSempreVisivel
            // 
            this.chkSempreVisivel.AutoSize = true;
            this.chkSempreVisivel.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSempreVisivel.Location = new System.Drawing.Point(483, 105);
            this.chkSempreVisivel.Name = "chkSempreVisivel";
            this.chkSempreVisivel.Size = new System.Drawing.Size(96, 17);
            this.chkSempreVisivel.TabIndex = 5;
            this.chkSempreVisivel.Text = "Sempre visível";
            this.chkSempreVisivel.UseVisualStyleBackColor = true;
            this.chkSempreVisivel.CheckedChanged += new System.EventHandler(this.chkSempreVisivel_CheckedChanged);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "csv";
            this.saveFileDialog1.Filter = "CSV files|*.csv|Todos os arquivos|*.*";
            this.saveFileDialog1.Title = "Exportar dados...";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(389, 101);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(633, 688);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.picExportar);
            this.Controls.Add(this.chkSempreVisivel);
            this.Controls.Add(this.gridProgramas);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chart1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Windows Time - TMenezes 2015";
            this.Activated += new System.EventHandler(this.FrmPrincipal_Activated);
            this.Deactivate += new System.EventHandler(this.FrmPrincipal_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPrincipal_FormClosing);
            this.Resize += new System.EventHandler(this.FrmPrincipal_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProgramas)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picExportar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView gridProgramas;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem abrirTrayMenu;
        private System.Windows.Forms.ToolStripMenuItem previewTrayMenu;
        private System.Windows.Forms.ToolStripMenuItem sairTrayMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox chkSempreVisivel;
        private System.Windows.Forms.DataGridViewImageColumn ColIcone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrograma;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTempo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colJanelas;
        private System.Windows.Forms.PictureBox picExportar;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button1;
    }
}