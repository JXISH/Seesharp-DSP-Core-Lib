namespace Winform_HarmonicAnalysis
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
            this.groupBoxConfig = new System.Windows.Forms.GroupBox();
            this.Phase = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.DCOffset = new System.Windows.Forms.NumericUpDown();
            this.Amplitude = new System.Windows.Forms.NumericUpDown();
            this.Frequency = new System.Windows.Forms.NumericUpDown();
            this.SampleRate = new System.Windows.Forms.NumericUpDown();
            this.SampleCount = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.easyChartWave = new SeeSharpTools.JY.GUI.EasyChart();
            this.groupBoxResult = new System.Windows.Forms.GroupBox();
            this.labelFundamentalFreq = new System.Windows.Forms.Label();
            this.labelFundamentalFreqV2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labelDCV2 = new System.Windows.Forms.Label();
            this.labelSINAD = new System.Windows.Forms.Label();
            this.labelSINADValue = new System.Windows.Forms.Label();
            this.labelTHD = new System.Windows.Forms.Label();
            this.labelPhase = new System.Windows.Forms.Label();
            this.labelTHDValue = new System.Windows.Forms.Label();
            this.labelPha = new System.Windows.Forms.Label();
            this.labelAmp = new System.Windows.Forms.Label();
            this.labelAmplitude = new System.Windows.Forms.Label();
            this.labelfreq = new System.Windows.Forms.Label();
            this.labelFrequency = new System.Windows.Forms.Label();
            this.groupBoxConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Phase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DCOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amplitude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Frequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SampleRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SampleCount)).BeginInit();
            this.groupBoxResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxConfig
            // 
            this.groupBoxConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxConfig.Controls.Add(this.Phase);
            this.groupBoxConfig.Controls.Add(this.label4);
            this.groupBoxConfig.Controls.Add(this.DCOffset);
            this.groupBoxConfig.Controls.Add(this.Amplitude);
            this.groupBoxConfig.Controls.Add(this.Frequency);
            this.groupBoxConfig.Controls.Add(this.SampleRate);
            this.groupBoxConfig.Controls.Add(this.SampleCount);
            this.groupBoxConfig.Controls.Add(this.label7);
            this.groupBoxConfig.Controls.Add(this.label1);
            this.groupBoxConfig.Controls.Add(this.label6);
            this.groupBoxConfig.Controls.Add(this.label2);
            this.groupBoxConfig.Controls.Add(this.label3);
            this.groupBoxConfig.Location = new System.Drawing.Point(470, 6);
            this.groupBoxConfig.Name = "groupBoxConfig";
            this.groupBoxConfig.Size = new System.Drawing.Size(321, 193);
            this.groupBoxConfig.TabIndex = 6;
            this.groupBoxConfig.TabStop = false;
            this.groupBoxConfig.Text = "Waveform GenerateConfiguration";
            // 
            // Phase
            // 
            this.Phase.DecimalPlaces = 1;
            this.Phase.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Phase.Location = new System.Drawing.Point(121, 158);
            this.Phase.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.Phase.Name = "Phase";
            this.Phase.Size = new System.Drawing.Size(188, 21);
            this.Phase.TabIndex = 4;
            this.Phase.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "Phase:";
            // 
            // DCOffset
            // 
            this.DCOffset.DecimalPlaces = 1;
            this.DCOffset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.DCOffset.Location = new System.Drawing.Point(121, 129);
            this.DCOffset.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.DCOffset.Name = "DCOffset";
            this.DCOffset.Size = new System.Drawing.Size(188, 21);
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
            this.Amplitude.Location = new System.Drawing.Point(121, 102);
            this.Amplitude.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.Amplitude.Name = "Amplitude";
            this.Amplitude.Size = new System.Drawing.Size(188, 21);
            this.Amplitude.TabIndex = 2;
            this.Amplitude.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Frequency
            // 
            this.Frequency.Location = new System.Drawing.Point(121, 75);
            this.Frequency.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.Frequency.Name = "Frequency";
            this.Frequency.Size = new System.Drawing.Size(188, 21);
            this.Frequency.TabIndex = 2;
            this.Frequency.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // SampleRate
            // 
            this.SampleRate.Location = new System.Drawing.Point(121, 21);
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
            this.SampleRate.Size = new System.Drawing.Size(188, 21);
            this.SampleRate.TabIndex = 2;
            this.SampleRate.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // SampleCount
            // 
            this.SampleCount.Location = new System.Drawing.Point(121, 48);
            this.SampleCount.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.SampleCount.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.SampleCount.Name = "SampleCount";
            this.SampleCount.Size = new System.Drawing.Size(188, 21);
            this.SampleCount.TabIndex = 2;
            this.SampleCount.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "DC offset:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sampling rate:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "Waveform amplitude:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Points:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "Waveform frequency:";
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonStart.Location = new System.Drawing.Point(498, 396);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(269, 31);
            this.buttonStart.TabIndex = 7;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
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
            this.easyChartWave.Location = new System.Drawing.Point(11, 11);
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
            this.easyChartWave.Size = new System.Drawing.Size(445, 408);
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
            // groupBoxResult
            // 
            this.groupBoxResult.Controls.Add(this.labelFundamentalFreq);
            this.groupBoxResult.Controls.Add(this.labelFundamentalFreqV2);
            this.groupBoxResult.Controls.Add(this.label8);
            this.groupBoxResult.Controls.Add(this.labelDCV2);
            this.groupBoxResult.Controls.Add(this.labelSINAD);
            this.groupBoxResult.Controls.Add(this.labelSINADValue);
            this.groupBoxResult.Controls.Add(this.labelTHD);
            this.groupBoxResult.Controls.Add(this.labelPhase);
            this.groupBoxResult.Controls.Add(this.labelTHDValue);
            this.groupBoxResult.Controls.Add(this.labelPha);
            this.groupBoxResult.Controls.Add(this.labelAmp);
            this.groupBoxResult.Controls.Add(this.labelAmplitude);
            this.groupBoxResult.Controls.Add(this.labelfreq);
            this.groupBoxResult.Controls.Add(this.labelFrequency);
            this.groupBoxResult.Location = new System.Drawing.Point(470, 203);
            this.groupBoxResult.Name = "groupBoxResult";
            this.groupBoxResult.Size = new System.Drawing.Size(321, 182);
            this.groupBoxResult.TabIndex = 11;
            this.groupBoxResult.TabStop = false;
            this.groupBoxResult.Text = "Measurement result";
            // 
            // labelFundamentalFreq
            // 
            this.labelFundamentalFreq.AutoSize = true;
            this.labelFundamentalFreq.Location = new System.Drawing.Point(8, 45);
            this.labelFundamentalFreq.Name = "labelFundamentalFreq";
            this.labelFundamentalFreq.Size = new System.Drawing.Size(161, 12);
            this.labelFundamentalFreq.TabIndex = 20;
            this.labelFundamentalFreq.Text = "Fundamental Amplitude (V):";
            // 
            // labelFundamentalFreqV2
            // 
            this.labelFundamentalFreqV2.AutoSize = true;
            this.labelFundamentalFreqV2.Location = new System.Drawing.Point(173, 45);
            this.labelFundamentalFreqV2.Name = "labelFundamentalFreqV2";
            this.labelFundamentalFreqV2.Size = new System.Drawing.Size(11, 12);
            this.labelFundamentalFreqV2.TabIndex = 21;
            this.labelFundamentalFreqV2.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "DC amplitude (V):";
            // 
            // labelDCV2
            // 
            this.labelDCV2.AutoSize = true;
            this.labelDCV2.Location = new System.Drawing.Point(173, 24);
            this.labelDCV2.Name = "labelDCV2";
            this.labelDCV2.Size = new System.Drawing.Size(11, 12);
            this.labelDCV2.TabIndex = 19;
            this.labelDCV2.Text = "0";
            // 
            // labelSINAD
            // 
            this.labelSINAD.AutoSize = true;
            this.labelSINAD.Location = new System.Drawing.Point(173, 156);
            this.labelSINAD.Name = "labelSINAD";
            this.labelSINAD.Size = new System.Drawing.Size(11, 12);
            this.labelSINAD.TabIndex = 17;
            this.labelSINAD.Text = "0";
            // 
            // labelSINADValue
            // 
            this.labelSINADValue.AutoSize = true;
            this.labelSINADValue.Location = new System.Drawing.Point(8, 156);
            this.labelSINADValue.Name = "labelSINADValue";
            this.labelSINADValue.Size = new System.Drawing.Size(71, 12);
            this.labelSINADValue.TabIndex = 16;
            this.labelSINADValue.Text = "SINAD(dB)：";
            // 
            // labelTHD
            // 
            this.labelTHD.AutoSize = true;
            this.labelTHD.Location = new System.Drawing.Point(173, 134);
            this.labelTHD.Name = "labelTHD";
            this.labelTHD.Size = new System.Drawing.Size(11, 12);
            this.labelTHD.TabIndex = 15;
            this.labelTHD.Text = "0";
            // 
            // labelPhase
            // 
            this.labelPhase.AutoSize = true;
            this.labelPhase.Location = new System.Drawing.Point(173, 112);
            this.labelPhase.Name = "labelPhase";
            this.labelPhase.Size = new System.Drawing.Size(11, 12);
            this.labelPhase.TabIndex = 14;
            this.labelPhase.Text = "0";
            // 
            // labelTHDValue
            // 
            this.labelTHDValue.AutoSize = true;
            this.labelTHDValue.Location = new System.Drawing.Point(8, 134);
            this.labelTHDValue.Name = "labelTHDValue";
            this.labelTHDValue.Size = new System.Drawing.Size(53, 12);
            this.labelTHDValue.TabIndex = 13;
            this.labelTHDValue.Text = "THD(%)：";
            // 
            // labelPha
            // 
            this.labelPha.AutoSize = true;
            this.labelPha.Location = new System.Drawing.Point(8, 112);
            this.labelPha.Name = "labelPha";
            this.labelPha.Size = new System.Drawing.Size(71, 12);
            this.labelPha.TabIndex = 12;
            this.labelPha.Text = "Phase(°)：";
            // 
            // labelAmp
            // 
            this.labelAmp.AutoSize = true;
            this.labelAmp.Location = new System.Drawing.Point(8, 90);
            this.labelAmp.Name = "labelAmp";
            this.labelAmp.Size = new System.Drawing.Size(89, 12);
            this.labelAmp.TabIndex = 10;
            this.labelAmp.Text = "Amplitude(V)：";
            // 
            // labelAmplitude
            // 
            this.labelAmplitude.AutoSize = true;
            this.labelAmplitude.Location = new System.Drawing.Point(173, 90);
            this.labelAmplitude.Name = "labelAmplitude";
            this.labelAmplitude.Size = new System.Drawing.Size(11, 12);
            this.labelAmplitude.TabIndex = 11;
            this.labelAmplitude.Text = "0";
            // 
            // labelfreq
            // 
            this.labelfreq.AutoSize = true;
            this.labelfreq.Location = new System.Drawing.Point(8, 68);
            this.labelfreq.Name = "labelfreq";
            this.labelfreq.Size = new System.Drawing.Size(95, 12);
            this.labelfreq.TabIndex = 8;
            this.labelfreq.Text = "Frequency (Hz):";
            // 
            // labelFrequency
            // 
            this.labelFrequency.AutoSize = true;
            this.labelFrequency.Location = new System.Drawing.Point(173, 68);
            this.labelFrequency.Name = "labelFrequency";
            this.labelFrequency.Size = new System.Drawing.Size(11, 12);
            this.labelFrequency.TabIndex = 9;
            this.labelFrequency.Text = "0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 435);
            this.Controls.Add(this.groupBoxResult);
            this.Controls.Add(this.easyChartWave);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.groupBoxConfig);
            this.MaximumSize = new System.Drawing.Size(816, 474);
            this.MinimumSize = new System.Drawing.Size(816, 474);
            this.Name = "MainForm";
            this.Text = "HarmonicAnalysis";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBoxConfig.ResumeLayout(false);
            this.groupBoxConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Phase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DCOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amplitude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Frequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SampleRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SampleCount)).EndInit();
            this.groupBoxResult.ResumeLayout(false);
            this.groupBoxResult.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxConfig;
        private System.Windows.Forms.NumericUpDown DCOffset;
        private System.Windows.Forms.NumericUpDown Amplitude;
        private System.Windows.Forms.NumericUpDown Frequency;
        private System.Windows.Forms.NumericUpDown SampleRate;
        private System.Windows.Forms.NumericUpDown SampleCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonStart;
        private SeeSharpTools.JY.GUI.EasyChart easyChartWave;
        private System.Windows.Forms.GroupBox groupBoxResult;
        private System.Windows.Forms.Label labelSINADValue;
        private System.Windows.Forms.Label labelTHD;
        private System.Windows.Forms.Label labelPhase;
        private System.Windows.Forms.Label labelTHDValue;
        private System.Windows.Forms.Label labelPha;
        private System.Windows.Forms.Label labelAmp;
        private System.Windows.Forms.Label labelAmplitude;
        private System.Windows.Forms.Label labelfreq;
        private System.Windows.Forms.Label labelFrequency;
        private System.Windows.Forms.Label labelSINAD;
        private System.Windows.Forms.Label labelFundamentalFreq;
        private System.Windows.Forms.Label labelFundamentalFreqV2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelDCV2;
        private System.Windows.Forms.NumericUpDown Phase;
        private System.Windows.Forms.Label label4;
    }
}

