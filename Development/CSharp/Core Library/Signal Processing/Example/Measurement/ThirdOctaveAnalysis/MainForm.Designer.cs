namespace Winform_ThirdOctaveAnalysis
{
    partial class MainForm
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

        #region Windows 窗体设计器Generate的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            SeeSharpTools.JY.GUI.EasyChartSeries easyChartSeries1 = new SeeSharpTools.JY.GUI.EasyChartSeries();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.groupBoxConfig = new System.Windows.Forms.GroupBox();
            this.DCOffset = new System.Windows.Forms.NumericUpDown();
            this.Amplitude = new System.Windows.Forms.NumericUpDown();
            this.Frequency = new System.Windows.Forms.NumericUpDown();
            this.SampleRate = new System.Windows.Forms.NumericUpDown();
            this.CycleCount = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.easyChartWave = new SeeSharpTools.JY.GUI.EasyChart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxWeightingFilterType = new System.Windows.Forms.ComboBox();
            this.comboBoxTimeAveragingMode = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.chartResult = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBoxConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DCOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amplitude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Frequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SampleRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CycleCount)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartResult)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxConfig
            // 
            this.groupBoxConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxConfig.Controls.Add(this.DCOffset);
            this.groupBoxConfig.Controls.Add(this.Amplitude);
            this.groupBoxConfig.Controls.Add(this.Frequency);
            this.groupBoxConfig.Controls.Add(this.SampleRate);
            this.groupBoxConfig.Controls.Add(this.CycleCount);
            this.groupBoxConfig.Controls.Add(this.label7);
            this.groupBoxConfig.Controls.Add(this.label1);
            this.groupBoxConfig.Controls.Add(this.label6);
            this.groupBoxConfig.Controls.Add(this.label2);
            this.groupBoxConfig.Controls.Add(this.label3);
            this.groupBoxConfig.Location = new System.Drawing.Point(541, 63);
            this.groupBoxConfig.Name = "groupBoxConfig";
            this.groupBoxConfig.Size = new System.Drawing.Size(369, 163);
            this.groupBoxConfig.TabIndex = 7;
            this.groupBoxConfig.TabStop = false;
            this.groupBoxConfig.Text = "Waveform GenerateConfiguration";
            // 
            // DCOffset
            // 
            this.DCOffset.DecimalPlaces = 1;
            this.DCOffset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.DCOffset.Location = new System.Drawing.Point(137, 130);
            this.DCOffset.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.DCOffset.Name = "DCOffset";
            this.DCOffset.Size = new System.Drawing.Size(219, 21);
            this.DCOffset.TabIndex = 2;
            this.DCOffset.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // Amplitude
            // 
            this.Amplitude.DecimalPlaces = 1;
            this.Amplitude.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Amplitude.Location = new System.Drawing.Point(137, 103);
            this.Amplitude.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.Amplitude.Name = "Amplitude";
            this.Amplitude.Size = new System.Drawing.Size(219, 21);
            this.Amplitude.TabIndex = 2;
            this.Amplitude.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Frequency
            // 
            this.Frequency.Location = new System.Drawing.Point(137, 76);
            this.Frequency.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.Frequency.Name = "Frequency";
            this.Frequency.Size = new System.Drawing.Size(219, 21);
            this.Frequency.TabIndex = 2;
            this.Frequency.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // SampleRate
            // 
            this.SampleRate.Location = new System.Drawing.Point(137, 22);
            this.SampleRate.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.SampleRate.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.SampleRate.Name = "SampleRate";
            this.SampleRate.Size = new System.Drawing.Size(219, 21);
            this.SampleRate.TabIndex = 2;
            this.SampleRate.Value = new decimal(new int[] {
            51200,
            0,
            0,
            0});
            // 
            // CycleCount
            // 
            this.CycleCount.Location = new System.Drawing.Point(137, 49);
            this.CycleCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.CycleCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CycleCount.Name = "CycleCount";
            this.CycleCount.Size = new System.Drawing.Size(219, 21);
            this.CycleCount.TabIndex = 2;
            this.CycleCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "DC offset:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sampling rate:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "Waveform amplitude:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Acquisition time:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "Waveform frequency:";
            // 
            // easyChartWave
            // 
            this.easyChartWave.AxisX.AutoScale = true;
            this.easyChartWave.AxisX.InitWithScaleView = false;
            this.easyChartWave.AxisX.LabelEnabled = true;
            this.easyChartWave.AxisX.LabelFormat = "";
            this.easyChartWave.AxisX.Maximum = 1001D;
            this.easyChartWave.AxisX.Minimum = 0D;
            this.easyChartWave.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartWave.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartWave.AxisX.Title = "";
            this.easyChartWave.AxisX.ViewMaximum = 1001D;
            this.easyChartWave.AxisX.ViewMinimum = 0D;
            this.easyChartWave.AxisY.AutoScale = true;
            this.easyChartWave.AxisY.InitWithScaleView = false;
            this.easyChartWave.AxisY.LabelEnabled = true;
            this.easyChartWave.AxisY.LabelFormat = "";
            this.easyChartWave.AxisY.Maximum = 3.5D;
            this.easyChartWave.AxisY.Minimum = 0D;
            this.easyChartWave.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartWave.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartWave.AxisY.Title = "";
            this.easyChartWave.AxisY.ViewMaximum = 3.5D;
            this.easyChartWave.AxisY.ViewMinimum = 0D;
            this.easyChartWave.AxisYMax = 3.5D;
            this.easyChartWave.AxisYMin = 0D;
            this.easyChartWave.BackGradientStyle = null;
            this.easyChartWave.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartWave.EasyChartBackColor = System.Drawing.Color.White;
            this.easyChartWave.FixAxisX = false;
            this.easyChartWave.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.easyChartWave.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartWave.LegendVisible = true;
            easyChartSeries1.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries1.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries1.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.easyChartWave.LineSeries.Add(easyChartSeries1);
            this.easyChartWave.Location = new System.Drawing.Point(13, 20);
            this.easyChartWave.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartWave.MajorGridEnabled = true;
            this.easyChartWave.Margin = new System.Windows.Forms.Padding(2);
            this.easyChartWave.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartWave.MinorGridEnabled = false;
            this.easyChartWave.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.easyChartWave.Name = "easyChartWave";
            this.easyChartWave.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.easyChartWave.SeriesNames = new string[] {
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
            this.easyChartWave.Size = new System.Drawing.Size(518, 206);
            this.easyChartWave.TabIndex = 8;
            this.easyChartWave.XAxisLogarithmic = false;
            this.easyChartWave.XAxisTitle = "";
            this.easyChartWave.XCursor.AutoInterval = true;
            this.easyChartWave.XCursor.Color = System.Drawing.Color.Red;
            this.easyChartWave.XCursor.Interval = 1D;
            this.easyChartWave.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.easyChartWave.XCursor.Value = double.NaN;
            this.easyChartWave.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartWave.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartWave.YAutoEnable = true;
            this.easyChartWave.YAxisLogarithmic = false;
            this.easyChartWave.YAxisTitle = "";
            this.easyChartWave.YCursor.AutoInterval = true;
            this.easyChartWave.YCursor.Color = System.Drawing.Color.Red;
            this.easyChartWave.YCursor.Interval = 0.001D;
            this.easyChartWave.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.easyChartWave.YCursor.Value = double.NaN;
            this.easyChartWave.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartWave.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.comboBoxWeightingFilterType);
            this.groupBox1.Controls.Add(this.comboBoxTimeAveragingMode);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(541, 255);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 82);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameter configuration";
            // 
            // comboBoxWeightingFilterType
            // 
            this.comboBoxWeightingFilterType.FormattingEnabled = true;
            this.comboBoxWeightingFilterType.Items.AddRange(new object[] {
            "AWeighting",
            "None"});
            this.comboBoxWeightingFilterType.Location = new System.Drawing.Point(172, 48);
            this.comboBoxWeightingFilterType.Name = "comboBoxWeightingFilterType";
            this.comboBoxWeightingFilterType.Size = new System.Drawing.Size(174, 20);
            this.comboBoxWeightingFilterType.TabIndex = 5;
            this.comboBoxWeightingFilterType.Text = "AWeighting";
            // 
            // comboBoxTimeAveragingMode
            // 
            this.comboBoxTimeAveragingMode.FormattingEnabled = true;
            this.comboBoxTimeAveragingMode.Items.AddRange(new object[] {
            "Fast",
            "Impulsive",
            "Slow"});
            this.comboBoxTimeAveragingMode.Location = new System.Drawing.Point(172, 22);
            this.comboBoxTimeAveragingMode.Name = "comboBoxTimeAveragingMode";
            this.comboBoxTimeAveragingMode.Size = new System.Drawing.Size(174, 20);
            this.comboBoxTimeAveragingMode.TabIndex = 4;
            this.comboBoxTimeAveragingMode.Text = "Fast";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "Time average method:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(136, 12);
            this.label9.TabIndex = 3;
            this.label9.Text = "Weighted filter types of:";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(593, 379);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(313, 36);
            this.buttonStart.TabIndex = 11;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "Time domain signal:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 240);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "Analysis results:";
            // 
            // chartResult
            // 
            chartArea1.Name = "ChartArea1";
            this.chartResult.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartResult.Legends.Add(legend1);
            this.chartResult.Location = new System.Drawing.Point(16, 255);
            this.chartResult.Name = "chartResult";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartResult.Series.Add(series1);
            this.chartResult.Size = new System.Drawing.Size(515, 215);
            this.chartResult.TabIndex = 14;
            this.chartResult.Text = "chartResult";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 482);
            this.Controls.Add(this.chartResult);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.easyChartWave);
            this.Controls.Add(this.groupBoxConfig);
            this.Name = "MainForm";
            this.Text = "ThirdOctaveAnalysis";
            this.groupBoxConfig.ResumeLayout(false);
            this.groupBoxConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DCOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amplitude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Frequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SampleRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CycleCount)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxConfig;
        private System.Windows.Forms.NumericUpDown DCOffset;
        private System.Windows.Forms.NumericUpDown Amplitude;
        private System.Windows.Forms.NumericUpDown Frequency;
        private System.Windows.Forms.NumericUpDown SampleRate;
        private System.Windows.Forms.NumericUpDown CycleCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private SeeSharpTools.JY.GUI.EasyChart easyChartWave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxWeightingFilterType;
        private System.Windows.Forms.ComboBox comboBoxTimeAveragingMode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartResult;
    }
}

