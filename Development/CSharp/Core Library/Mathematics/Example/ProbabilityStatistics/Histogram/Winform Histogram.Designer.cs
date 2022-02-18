namespace Demo_for_HistgramCreating
{
    partial class HistogramForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.nudNbrOfIntervals = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nudDataLength = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.easyChart1 = new SeeSharpTools.JY.GUI.EasyChart();
            this.Input = new System.Windows.Forms.GroupBox();
            this.txtboxTimeElapsed = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudTestTimes = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNbrOfIntervals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDataLength)).BeginInit();
            this.Input.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTestTimes)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(205, 298);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(717, 297);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // nudNbrOfIntervals
            // 
            this.nudNbrOfIntervals.Font = new System.Drawing.Font("宋体", 10F);
            this.nudNbrOfIntervals.Location = new System.Drawing.Point(39, 139);
            this.nudNbrOfIntervals.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.nudNbrOfIntervals.Name = "nudNbrOfIntervals";
            this.nudNbrOfIntervals.Size = new System.Drawing.Size(109, 27);
            this.nudNbrOfIntervals.TabIndex = 3;
            this.nudNbrOfIntervals.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudNbrOfIntervals.ValueChanged += new System.EventHandler(this.HistogramEvent);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "NumberOfIntervals";
            // 
            // nudDataLength
            // 
            this.nudDataLength.Font = new System.Drawing.Font("宋体", 10F);
            this.nudDataLength.Location = new System.Drawing.Point(39, 58);
            this.nudDataLength.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.nudDataLength.Name = "nudDataLength";
            this.nudDataLength.Size = new System.Drawing.Size(109, 27);
            this.nudDataLength.TabIndex = 3;
            this.nudDataLength.Value = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.nudDataLength.ValueChanged += new System.EventHandler(this.HistogramEvent);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "DataLength";
            // 
            // easyChart1
            // 
            this.easyChart1.AxisYMax = double.NaN;
            this.easyChart1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.None;
            this.easyChart1.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChart1.EasyChartBackColor = System.Drawing.Color.White;
            this.easyChart1.FixAxisX = false;
            this.easyChart1.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChart1.LegendVisible = false;
            this.easyChart1.Location = new System.Drawing.Point(204, 11);
            this.easyChart1.MajorGridColor = System.Drawing.Color.Black;
            this.easyChart1.MajorGridEnabled = true;
            this.easyChart1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.easyChart1.MinorGridColor = System.Drawing.Color.Black;
            this.easyChart1.MinorGridEnabled = false;
            this.easyChart1.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.easyChart1.Name = "easyChart1";
            this.easyChart1.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.easyChart1.SeriesNames = new string[] {
        "Series1",
        "Series2",
        "Series3",
        "Series4",
        "Series5",
        "Series6",
        "Series7",
        "Series8",
        "Series9",
        "Series10",
        "Series11",
        "Series12",
        "Series13",
        "Series14",
        "Series15",
        "Series16",
        "Series17",
        "Series18",
        "Series19",
        "Series20",
        "Series21",
        "Series22",
        "Series23",
        "Series24",
        "Series25",
        "Series26",
        "Series27",
        "Series28",
        "Series29",
        "Series30",
        "Series31",
        "Series32"};
            this.easyChart1.Size = new System.Drawing.Size(676, 297);
            this.easyChart1.TabIndex = 5;
            this.easyChart1.XAxisLogarithmic = false;
            this.easyChart1.YAutoEnable = true;
            this.easyChart1.YAxisLogarithmic = false;
            // 
            // Input
            // 
            this.Input.Controls.Add(this.txtboxTimeElapsed);
            this.Input.Controls.Add(this.nudDataLength);
            this.Input.Controls.Add(this.label1);
            this.Input.Controls.Add(this.label4);
            this.Input.Controls.Add(this.label3);
            this.Input.Controls.Add(this.label2);
            this.Input.Controls.Add(this.nudTestTimes);
            this.Input.Controls.Add(this.nudNbrOfIntervals);
            this.Input.Location = new System.Drawing.Point(18, 23);
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(200, 548);
            this.Input.TabIndex = 6;
            this.Input.TabStop = false;
            // 
            // txtboxTimeElapsed
            // 
            this.txtboxTimeElapsed.Location = new System.Drawing.Point(36, 463);
            this.txtboxTimeElapsed.Name = "txtboxTimeElapsed";
            this.txtboxTimeElapsed.Size = new System.Drawing.Size(106, 25);
            this.txtboxTimeElapsed.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 445);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "Efficiency:ms";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 359);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "LoopTimes";
            // 
            // nudTestTimes
            // 
            this.nudTestTimes.Font = new System.Drawing.Font("宋体", 10F);
            this.nudTestTimes.Location = new System.Drawing.Point(39, 377);
            this.nudTestTimes.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudTestTimes.Name = "nudTestTimes";
            this.nudTestTimes.Size = new System.Drawing.Size(109, 27);
            this.nudTestTimes.TabIndex = 3;
            this.nudTestTimes.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudTestTimes.ValueChanged += new System.EventHandler(this.EfficiencyEvent);
            // 
            // HistogramForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(940, 600);
            this.Controls.Add(this.Input);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.easyChart1);
            this.Name = "HistogramForm";
            this.Text = "HistogramDemo";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNbrOfIntervals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDataLength)).EndInit();
            this.Input.ResumeLayout(false);
            this.Input.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTestTimes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NumericUpDown nudNbrOfIntervals;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudDataLength;
        private System.Windows.Forms.Label label2;
        private SeeSharpTools.JY.GUI.EasyChart easyChart1;
        private System.Windows.Forms.GroupBox Input;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudTestTimes;
        private System.Windows.Forms.TextBox txtboxTimeElapsed;
        private System.Windows.Forms.Label label4;
    }
}

