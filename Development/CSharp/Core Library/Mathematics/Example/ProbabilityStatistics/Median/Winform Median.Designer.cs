namespace Mathematics
{
    partial class MedianForm
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
            SeeSharpTools.JY.GUI.EasyChartSeries easyChartSeries1 = new SeeSharpTools.JY.GUI.EasyChartSeries();
            this.nudTestTimes = new System.Windows.Forms.NumericUpDown();
            this.txtboxMedian = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.easyChart1 = new SeeSharpTools.JY.GUI.EasyChart();
            this.txtboxTimeElapsed = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudLength = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudTestTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).BeginInit();
            this.SuspendLayout();
            // 
            // nudTestTimes
            // 
            this.nudTestTimes.Location = new System.Drawing.Point(487, 298);
            this.nudTestTimes.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nudTestTimes.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudTestTimes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTestTimes.Name = "nudTestTimes";
            this.nudTestTimes.Size = new System.Drawing.Size(134, 21);
            this.nudTestTimes.TabIndex = 33;
            this.nudTestTimes.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudTestTimes.ValueChanged += new System.EventHandler(this.EfficiencyEvent);
            // 
            // txtboxMedian
            // 
            this.txtboxMedian.Location = new System.Drawing.Point(487, 159);
            this.txtboxMedian.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtboxMedian.Name = "txtboxMedian";
            this.txtboxMedian.Size = new System.Drawing.Size(135, 21);
            this.txtboxMedian.TabIndex = 29;
            this.txtboxMedian.Text = " ";
            this.txtboxMedian.TextChanged += new System.EventHandler(this.txtboxMedian_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(489, 283);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 31;
            this.label3.Text = "LoopTimes";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(484, 145);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 28;
            this.label7.Text = "Median";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(484, 331);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 12);
            this.label9.TabIndex = 32;
            this.label9.Text = "Efficiency/ms";
            // 
            // easyChart1
            // 
            this.easyChart1.AxisX.AutoScale = true;
            this.easyChart1.AxisX.InitWithScaleView = false;
            this.easyChart1.AxisX.LabelEnabled = true;
            this.easyChart1.AxisX.LabelFormat = "";
            this.easyChart1.AxisX.Maximum = 1001D;
            this.easyChart1.AxisX.Minimum = 0D;
            this.easyChart1.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChart1.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChart1.AxisX.Title = "";
            this.easyChart1.AxisX.ViewMaximum = 1001D;
            this.easyChart1.AxisX.ViewMinimum = 0D;
            this.easyChart1.AxisY.AutoScale = true;
            this.easyChart1.AxisY.InitWithScaleView = false;
            this.easyChart1.AxisY.LabelEnabled = true;
            this.easyChart1.AxisY.LabelFormat = "";
            this.easyChart1.AxisY.Maximum = 3.5D;
            this.easyChart1.AxisY.Minimum = 0D;
            this.easyChart1.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChart1.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChart1.AxisY.Title = "";
            this.easyChart1.AxisY.ViewMaximum = 3.5D;
            this.easyChart1.AxisY.ViewMinimum = 0D;
            this.easyChart1.AxisYMax = 3.5D;
            this.easyChart1.AxisYMin = 0D;
            this.easyChart1.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChart1.EasyChartBackColor = System.Drawing.Color.White;
            this.easyChart1.FixAxisX = false;
            this.easyChart1.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.easyChart1.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChart1.LegendVisible = false;
            easyChartSeries1.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries1.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries1.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.easyChart1.LineSeries.Add(easyChartSeries1);
            this.easyChart1.Location = new System.Drawing.Point(22, 108);
            this.easyChart1.MajorGridColor = System.Drawing.Color.Black;
            this.easyChart1.MajorGridEnabled = true;
            this.easyChart1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.easyChart1.Size = new System.Drawing.Size(450, 320);
            this.easyChart1.TabIndex = 38;
            this.easyChart1.XAxisLogarithmic = false;
            this.easyChart1.XAxisTitle = "";
            this.easyChart1.XCursor.AutoInterval = true;
            this.easyChart1.XCursor.Color = System.Drawing.Color.Red;
            this.easyChart1.XCursor.Interval = 1D;
            this.easyChart1.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.easyChart1.XCursor.Value = double.NaN;
            this.easyChart1.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChart1.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChart1.YAutoEnable = true;
            this.easyChart1.YAxisLogarithmic = false;
            this.easyChart1.YAxisTitle = "";
            this.easyChart1.YCursor.AutoInterval = true;
            this.easyChart1.YCursor.Color = System.Drawing.Color.Red;
            this.easyChart1.YCursor.Interval = 0.001D;
            this.easyChart1.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.easyChart1.YCursor.Value = double.NaN;
            this.easyChart1.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChart1.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // txtboxTimeElapsed
            // 
            this.txtboxTimeElapsed.Location = new System.Drawing.Point(487, 346);
            this.txtboxTimeElapsed.Name = "txtboxTimeElapsed";
            this.txtboxTimeElapsed.Size = new System.Drawing.Size(134, 21);
            this.txtboxTimeElapsed.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 87);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 12);
            this.label1.TabIndex = 37;
            this.label1.Text = "Random Data Length(To be analyzed)";
            // 
            // nudLength
            // 
            this.nudLength.Location = new System.Drawing.Point(281, 84);
            this.nudLength.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nudLength.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLength.Name = "nudLength";
            this.nudLength.Size = new System.Drawing.Size(134, 21);
            this.nudLength.TabIndex = 36;
            this.nudLength.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudLength.ValueChanged += new System.EventHandler(this.MedianEvent);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(82, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(339, 35);
            this.label5.TabIndex = 35;
            this.label5.Text = "Median Calculation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 34;
            // 
            // MedianForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(648, 452);
            this.Controls.Add(this.nudTestTimes);
            this.Controls.Add(this.txtboxMedian);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.easyChart1);
            this.Controls.Add(this.txtboxTimeElapsed);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudLength);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Name = "MedianForm";
            this.Text = " MedianCalculationDemo";
            ((System.ComponentModel.ISupportInitialize)(this.nudTestTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudTestTimes;
        private System.Windows.Forms.TextBox txtboxMedian;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private SeeSharpTools.JY.GUI.EasyChart easyChart1;
        private System.Windows.Forms.TextBox txtboxTimeElapsed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudLength;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
    }
}

