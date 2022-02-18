namespace JTFA
{
    partial class Mainform
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
            SeeSharpTools.JY.GUI.EasyChartSeries easyChartSeries7 = new SeeSharpTools.JY.GUI.EasyChartSeries();
            SeeSharpTools.JY.GUI.EasyChartXSeries easyChartXSeries7 = new SeeSharpTools.JY.GUI.EasyChartXSeries();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.NoiseAmplitude = new System.Windows.Forms.NumericUpDown();
            this.SignalType = new System.Windows.Forms.ComboBox();
            this.DCOffset = new System.Windows.Forms.NumericUpDown();
            this.Amplitude = new System.Windows.Forms.NumericUpDown();
            this.Frequency = new System.Windows.Forms.NumericUpDown();
            this.MaxFrequency = new System.Windows.Forms.NumericUpDown();
            this.SampleCount = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.easyChart1 = new SeeSharpTools.JY.GUI.EasyChart();
            this.buttonAnalysis = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SpectralLineCount = new System.Windows.Forms.NumericUpDown();
            this.WindowTypes = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox_frequency_time = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.labeldf = new System.Windows.Forms.Label();
            this.groupBoxOutputInformation = new System.Windows.Forms.GroupBox();
            this.labelf0 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.labeldt = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBoxColorType = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.easyChartXWaterfall = new SeeSharpTools.JY.GUI.EasyChartX();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NoiseAmplitude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DCOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amplitude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Frequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxFrequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SampleCount)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpectralLineCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_frequency_time)).BeginInit();
            this.groupBoxOutputInformation.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.NoiseAmplitude);
            this.groupBox1.Controls.Add(this.SignalType);
            this.groupBox1.Controls.Add(this.DCOffset);
            this.groupBox1.Controls.Add(this.Amplitude);
            this.groupBox1.Controls.Add(this.Frequency);
            this.groupBox1.Controls.Add(this.MaxFrequency);
            this.groupBox1.Controls.Add(this.SampleCount);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(698, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 217);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Waveform GenerateConfiguration";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Noise:";
            // 
            // NoiseAmplitude
            // 
            this.NoiseAmplitude.DecimalPlaces = 2;
            this.NoiseAmplitude.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.NoiseAmplitude.Location = new System.Drawing.Point(120, 185);
            this.NoiseAmplitude.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.NoiseAmplitude.Name = "NoiseAmplitude";
            this.NoiseAmplitude.Size = new System.Drawing.Size(188, 21);
            this.NoiseAmplitude.TabIndex = 5;
            this.NoiseAmplitude.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // SignalType
            // 
            this.SignalType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SignalType.FormattingEnabled = true;
            this.SignalType.Items.AddRange(new object[] {
            "Real",
            "Complex"});
            this.SignalType.Location = new System.Drawing.Point(120, 20);
            this.SignalType.Name = "SignalType";
            this.SignalType.Size = new System.Drawing.Size(188, 20);
            this.SignalType.TabIndex = 4;
            // 
            // DCOffset
            // 
            this.DCOffset.DecimalPlaces = 1;
            this.DCOffset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.DCOffset.Location = new System.Drawing.Point(120, 158);
            this.DCOffset.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.DCOffset.Name = "DCOffset";
            this.DCOffset.Size = new System.Drawing.Size(188, 21);
            this.DCOffset.TabIndex = 2;
            this.DCOffset.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // Amplitude
            // 
            this.Amplitude.DecimalPlaces = 1;
            this.Amplitude.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Amplitude.Location = new System.Drawing.Point(120, 131);
            this.Amplitude.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.Amplitude.Name = "Amplitude";
            this.Amplitude.Size = new System.Drawing.Size(188, 21);
            this.Amplitude.TabIndex = 2;
            this.Amplitude.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // Frequency
            // 
            this.Frequency.Location = new System.Drawing.Point(120, 104);
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
            // MaxFrequency
            // 
            this.MaxFrequency.Location = new System.Drawing.Point(120, 50);
            this.MaxFrequency.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.MaxFrequency.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.MaxFrequency.Name = "MaxFrequency";
            this.MaxFrequency.Size = new System.Drawing.Size(188, 21);
            this.MaxFrequency.TabIndex = 2;
            this.MaxFrequency.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // SampleCount
            // 
            this.SampleCount.Location = new System.Drawing.Point(120, 77);
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
            20000,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "DC offset:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 12);
            this.label12.TabIndex = 3;
            this.label12.Text = "Signal types of:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sampling rate:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "Waveform amplitude:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Points:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "Waveform frequency:";
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
            this.easyChart1.ChartAreaBackColor = System.Drawing.Color.Transparent;
            this.easyChart1.EasyChartBackColor = System.Drawing.Color.White;
            this.easyChart1.FixAxisX = false;
            this.easyChart1.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.easyChart1.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChart1.LegendVisible = true;
            easyChartSeries7.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries7.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries7.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.easyChart1.LineSeries.Add(easyChartSeries7);
            this.easyChart1.Location = new System.Drawing.Point(1, 27);
            this.easyChart1.MajorGridColor = System.Drawing.Color.Black;
            this.easyChart1.MajorGridEnabled = true;
            this.easyChart1.Margin = new System.Windows.Forms.Padding(2);
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
            this.easyChart1.Size = new System.Drawing.Size(683, 206);
            this.easyChart1.TabIndex = 7;
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
            // buttonAnalysis
            // 
            this.buttonAnalysis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAnalysis.Location = new System.Drawing.Point(823, 636);
            this.buttonAnalysis.Name = "buttonAnalysis";
            this.buttonAnalysis.Size = new System.Drawing.Size(93, 41);
            this.buttonAnalysis.TabIndex = 8;
            this.buttonAnalysis.Text = "Generate and analyze";
            this.buttonAnalysis.UseVisualStyleBackColor = true;
            this.buttonAnalysis.Click += new System.EventHandler(this.buttonAnalysis_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.SpectralLineCount);
            this.groupBox2.Controls.Add(this.WindowTypes);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(698, 274);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(315, 91);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SpectrumConfiguration";
            // 
            // SpectralLineCount
            // 
            this.SpectralLineCount.DecimalPlaces = 1;
            this.SpectralLineCount.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.SpectralLineCount.Location = new System.Drawing.Point(116, 54);
            this.SpectralLineCount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.SpectralLineCount.Name = "SpectralLineCount";
            this.SpectralLineCount.Size = new System.Drawing.Size(188, 21);
            this.SpectralLineCount.TabIndex = 5;
            this.SpectralLineCount.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // WindowTypes
            // 
            this.WindowTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WindowTypes.FormattingEnabled = true;
            this.WindowTypes.Location = new System.Drawing.Point(116, 24);
            this.WindowTypes.Name = "WindowTypes";
            this.WindowTypes.Size = new System.Drawing.Size(188, 20);
            this.WindowTypes.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "Window length:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "Window type:";
            // 
            // pictureBox_frequency_time
            // 
            this.pictureBox_frequency_time.Location = new System.Drawing.Point(5, 497);
            this.pictureBox_frequency_time.Name = "pictureBox_frequency_time";
            this.pictureBox_frequency_time.Size = new System.Drawing.Size(679, 204);
            this.pictureBox_frequency_time.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_frequency_time.TabIndex = 79;
            this.pictureBox_frequency_time.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(149, 12);
            this.label9.TabIndex = 80;
            this.label9.Text = "Frequency interval (Hz):";
            // 
            // labeldf
            // 
            this.labeldf.AutoSize = true;
            this.labeldf.Location = new System.Drawing.Point(188, 29);
            this.labeldf.Name = "labeldf";
            this.labeldf.Size = new System.Drawing.Size(11, 12);
            this.labeldf.TabIndex = 81;
            this.labeldf.Text = "0";
            this.labeldf.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBoxOutputInformation
            // 
            this.groupBoxOutputInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxOutputInformation.Controls.Add(this.labelf0);
            this.groupBoxOutputInformation.Controls.Add(this.label11);
            this.groupBoxOutputInformation.Controls.Add(this.labeldt);
            this.groupBoxOutputInformation.Controls.Add(this.label10);
            this.groupBoxOutputInformation.Controls.Add(this.label9);
            this.groupBoxOutputInformation.Controls.Add(this.labeldf);
            this.groupBoxOutputInformation.Location = new System.Drawing.Point(698, 481);
            this.groupBoxOutputInformation.Name = "groupBoxOutputInformation";
            this.groupBoxOutputInformation.Size = new System.Drawing.Size(315, 113);
            this.groupBoxOutputInformation.TabIndex = 82;
            this.groupBoxOutputInformation.TabStop = false;
            this.groupBoxOutputInformation.Text = "SpectrumConfiguration";
            // 
            // labelf0
            // 
            this.labelf0.AutoSize = true;
            this.labelf0.Location = new System.Drawing.Point(188, 81);
            this.labelf0.Name = "labelf0";
            this.labelf0.Size = new System.Drawing.Size(11, 12);
            this.labelf0.TabIndex = 85;
            this.labelf0.Text = "0";
            this.labelf0.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 81);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(131, 12);
            this.label11.TabIndex = 84;
            this.label11.Text = "Start frequency(Hz)：";
            // 
            // labeldt
            // 
            this.labeldt.AutoSize = true;
            this.labeldt.Location = new System.Drawing.Point(188, 56);
            this.labeldt.Name = "labeldt";
            this.labeldt.Size = new System.Drawing.Size(11, 12);
            this.labeldt.TabIndex = 83;
            this.labeldt.Text = "0";
            this.labeldt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 12);
            this.label10.TabIndex = 82;
            this.label10.Text = "Time interval(s)：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 12);
            this.label13.TabIndex = 84;
            this.label13.Text = "Input signal";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 244);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 12);
            this.label14.TabIndex = 85;
            this.label14.Text = "Waterfall";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 481);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(209, 12);
            this.label15.TabIndex = 86;
            this.label15.Text = "Intensity map (abscissa frequency)";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.comboBoxColorType);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Location = new System.Drawing.Point(698, 391);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(315, 62);
            this.groupBox3.TabIndex = 87;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Intensity map configuration";
            // 
            // comboBoxColorType
            // 
            this.comboBoxColorType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxColorType.FormattingEnabled = true;
            this.comboBoxColorType.Items.AddRange(new object[] {
            "BlackWrite",
            "BlueGreenRed",
            "Rainbow",
            "BalancedRainbow",
            "Fire",
            "BlueSpirit",
            "BlueFairy",
            "BlueOrange30-Hue"});
            this.comboBoxColorType.Location = new System.Drawing.Point(116, 26);
            this.comboBoxColorType.Name = "comboBoxColorType";
            this.comboBoxColorType.Size = new System.Drawing.Size(188, 20);
            this.comboBoxColorType.TabIndex = 4;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(9, 30);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(131, 12);
            this.label17.TabIndex = 3;
            this.label17.Text = "Intensity chart type:";
            // 
            // easyChartXWaterfall
            // 
            this.easyChartXWaterfall.AxisX.AutoScale = true;
            this.easyChartXWaterfall.AxisX.AutoZoomReset = false;
            this.easyChartXWaterfall.AxisX.Color = System.Drawing.Color.Black;
            this.easyChartXWaterfall.AxisX.InitWithScaleView = false;
            this.easyChartXWaterfall.AxisX.IsLogarithmic = false;
            this.easyChartXWaterfall.AxisX.LabelAngle = 0;
            this.easyChartXWaterfall.AxisX.LabelEnabled = true;
            this.easyChartXWaterfall.AxisX.LabelFormat = null;
            this.easyChartXWaterfall.AxisX.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXWaterfall.AxisX.MajorGridCount = -1;
            this.easyChartXWaterfall.AxisX.MajorGridEnabled = true;
            this.easyChartXWaterfall.AxisX.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXWaterfall.AxisX.Maximum = 1000D;
            this.easyChartXWaterfall.AxisX.Minimum = 0D;
            this.easyChartXWaterfall.AxisX.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXWaterfall.AxisX.MinorGridEnabled = false;
            this.easyChartXWaterfall.AxisX.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXWaterfall.AxisX.TickWidth = 1F;
            this.easyChartXWaterfall.AxisX.Title = "";
            this.easyChartXWaterfall.AxisX.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXWaterfall.AxisX.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXWaterfall.AxisX.ViewMaximum = 1000D;
            this.easyChartXWaterfall.AxisX.ViewMinimum = 0D;
            this.easyChartXWaterfall.AxisX2.AutoScale = true;
            this.easyChartXWaterfall.AxisX2.AutoZoomReset = false;
            this.easyChartXWaterfall.AxisX2.Color = System.Drawing.Color.Black;
            this.easyChartXWaterfall.AxisX2.InitWithScaleView = false;
            this.easyChartXWaterfall.AxisX2.IsLogarithmic = false;
            this.easyChartXWaterfall.AxisX2.LabelAngle = 0;
            this.easyChartXWaterfall.AxisX2.LabelEnabled = true;
            this.easyChartXWaterfall.AxisX2.LabelFormat = null;
            this.easyChartXWaterfall.AxisX2.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXWaterfall.AxisX2.MajorGridCount = -1;
            this.easyChartXWaterfall.AxisX2.MajorGridEnabled = true;
            this.easyChartXWaterfall.AxisX2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXWaterfall.AxisX2.Maximum = 1000D;
            this.easyChartXWaterfall.AxisX2.Minimum = 0D;
            this.easyChartXWaterfall.AxisX2.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXWaterfall.AxisX2.MinorGridEnabled = false;
            this.easyChartXWaterfall.AxisX2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXWaterfall.AxisX2.TickWidth = 1F;
            this.easyChartXWaterfall.AxisX2.Title = "";
            this.easyChartXWaterfall.AxisX2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXWaterfall.AxisX2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXWaterfall.AxisX2.ViewMaximum = 1000D;
            this.easyChartXWaterfall.AxisX2.ViewMinimum = 0D;
            this.easyChartXWaterfall.AxisY.AutoScale = true;
            this.easyChartXWaterfall.AxisY.AutoZoomReset = false;
            this.easyChartXWaterfall.AxisY.Color = System.Drawing.Color.Black;
            this.easyChartXWaterfall.AxisY.InitWithScaleView = false;
            this.easyChartXWaterfall.AxisY.IsLogarithmic = false;
            this.easyChartXWaterfall.AxisY.LabelAngle = 0;
            this.easyChartXWaterfall.AxisY.LabelEnabled = true;
            this.easyChartXWaterfall.AxisY.LabelFormat = null;
            this.easyChartXWaterfall.AxisY.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXWaterfall.AxisY.MajorGridCount = 6;
            this.easyChartXWaterfall.AxisY.MajorGridEnabled = true;
            this.easyChartXWaterfall.AxisY.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXWaterfall.AxisY.Maximum = 3D;
            this.easyChartXWaterfall.AxisY.Minimum = 0D;
            this.easyChartXWaterfall.AxisY.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXWaterfall.AxisY.MinorGridEnabled = false;
            this.easyChartXWaterfall.AxisY.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXWaterfall.AxisY.TickWidth = 1F;
            this.easyChartXWaterfall.AxisY.Title = "";
            this.easyChartXWaterfall.AxisY.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXWaterfall.AxisY.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXWaterfall.AxisY.ViewMaximum = 3.5D;
            this.easyChartXWaterfall.AxisY.ViewMinimum = 0.5D;
            this.easyChartXWaterfall.AxisY2.AutoScale = true;
            this.easyChartXWaterfall.AxisY2.AutoZoomReset = false;
            this.easyChartXWaterfall.AxisY2.Color = System.Drawing.Color.Black;
            this.easyChartXWaterfall.AxisY2.InitWithScaleView = false;
            this.easyChartXWaterfall.AxisY2.IsLogarithmic = false;
            this.easyChartXWaterfall.AxisY2.LabelAngle = 0;
            this.easyChartXWaterfall.AxisY2.LabelEnabled = true;
            this.easyChartXWaterfall.AxisY2.LabelFormat = null;
            this.easyChartXWaterfall.AxisY2.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXWaterfall.AxisY2.MajorGridCount = 6;
            this.easyChartXWaterfall.AxisY2.MajorGridEnabled = true;
            this.easyChartXWaterfall.AxisY2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXWaterfall.AxisY2.Maximum = 3.5D;
            this.easyChartXWaterfall.AxisY2.Minimum = 0.5D;
            this.easyChartXWaterfall.AxisY2.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXWaterfall.AxisY2.MinorGridEnabled = false;
            this.easyChartXWaterfall.AxisY2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXWaterfall.AxisY2.TickWidth = 1F;
            this.easyChartXWaterfall.AxisY2.Title = "";
            this.easyChartXWaterfall.AxisY2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXWaterfall.AxisY2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXWaterfall.AxisY2.ViewMaximum = 3.5D;
            this.easyChartXWaterfall.AxisY2.ViewMinimum = 0.5D;
            this.easyChartXWaterfall.BackColor = System.Drawing.Color.White;
            this.easyChartXWaterfall.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartXWaterfall.Cumulitive = false;
            this.easyChartXWaterfall.Cursor = System.Windows.Forms.Cursors.No;
            this.easyChartXWaterfall.GradientStyle = SeeSharpTools.JY.GUI.EasyChartX.ChartGradientStyle.None;
            this.easyChartXWaterfall.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartXWaterfall.LegendFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.easyChartXWaterfall.LegendForeColor = System.Drawing.Color.Black;
            this.easyChartXWaterfall.LegendVisible = false;
            easyChartXSeries7.Color = System.Drawing.Color.Red;
            easyChartXSeries7.Marker = SeeSharpTools.JY.GUI.EasyChartXSeries.MarkerType.None;
            easyChartXSeries7.Name = "Series1";
            easyChartXSeries7.Type = SeeSharpTools.JY.GUI.EasyChartXSeries.LineType.FastLine;
            easyChartXSeries7.Visible = true;
            easyChartXSeries7.Width = SeeSharpTools.JY.GUI.EasyChartXSeries.LineWidth.Thin;
            easyChartXSeries7.XPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries7.YPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            this.easyChartXWaterfall.LineSeries.Add(easyChartXSeries7);
            this.easyChartXWaterfall.Location = new System.Drawing.Point(1, 259);
            this.easyChartXWaterfall.Miscellaneous.CheckInfinity = false;
            this.easyChartXWaterfall.Miscellaneous.CheckNaN = true;
            this.easyChartXWaterfall.Miscellaneous.CheckNegtiveOrZero = false;
            this.easyChartXWaterfall.Miscellaneous.DirectionChartCount = 3;
            this.easyChartXWaterfall.Miscellaneous.Fitting = SeeSharpTools.JY.GUI.EasyChartX.FitType.Range;
            this.easyChartXWaterfall.Miscellaneous.MarkerSize = 2;
            this.easyChartXWaterfall.Miscellaneous.MaxSeriesCount = 32;
            this.easyChartXWaterfall.Miscellaneous.MaxSeriesPointCount = 4000;
            this.easyChartXWaterfall.Miscellaneous.ShowFunctionMenu = true;
            this.easyChartXWaterfall.Miscellaneous.SplitLayoutColumnInterval = 0F;
            this.easyChartXWaterfall.Miscellaneous.SplitLayoutDirection = SeeSharpTools.JY.GUI.EasyChartXUtility.LayoutDirection.LeftToRight;
            this.easyChartXWaterfall.Miscellaneous.SplitLayoutRowInterval = 0F;
            this.easyChartXWaterfall.Miscellaneous.SplitViewAutoLayout = true;
            this.easyChartXWaterfall.Name = "easyChartXWaterfall";
            this.easyChartXWaterfall.SeriesCount = 1;
            this.easyChartXWaterfall.Size = new System.Drawing.Size(683, 219);
            this.easyChartXWaterfall.SplitView = false;
            this.easyChartXWaterfall.TabIndex = 88;
            this.easyChartXWaterfall.XCursor.AutoInterval = true;
            this.easyChartXWaterfall.XCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this.easyChartXWaterfall.XCursor.Interval = 0.001D;
            this.easyChartXWaterfall.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Zoom;
            this.easyChartXWaterfall.XCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.easyChartXWaterfall.XCursor.Value = double.NaN;
            this.easyChartXWaterfall.YCursor.AutoInterval = true;
            this.easyChartXWaterfall.YCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this.easyChartXWaterfall.YCursor.Interval = 0.001D;
            this.easyChartXWaterfall.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Disabled;
            this.easyChartXWaterfall.YCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.easyChartXWaterfall.YCursor.Value = double.NaN;
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 732);
            this.Controls.Add(this.easyChartXWaterfall);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.groupBoxOutputInformation);
            this.Controls.Add(this.pictureBox_frequency_time);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonAnalysis);
            this.Controls.Add(this.easyChart1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Mainform";
            this.Text = "JTFA Winform";
            this.Load += new System.EventHandler(this.Mainform_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NoiseAmplitude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DCOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amplitude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Frequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxFrequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SampleCount)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpectralLineCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_frequency_time)).EndInit();
            this.groupBoxOutputInformation.ResumeLayout(false);
            this.groupBoxOutputInformation.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox SignalType;
        private System.Windows.Forms.NumericUpDown DCOffset;
        private System.Windows.Forms.NumericUpDown Amplitude;
        private System.Windows.Forms.NumericUpDown Frequency;
        private System.Windows.Forms.NumericUpDown MaxFrequency;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private SeeSharpTools.JY.GUI.EasyChart easyChart1;
        private System.Windows.Forms.Button buttonAnalysis;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox WindowTypes;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown SampleCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown SpectralLineCount;
        private System.Windows.Forms.PictureBox pictureBox_frequency_time;
        private System.Windows.Forms.NumericUpDown NoiseAmplitude;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labeldf;
        private System.Windows.Forms.GroupBox groupBoxOutputInformation;
        private System.Windows.Forms.Label labelf0;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label labeldt;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBoxColorType;
        private System.Windows.Forms.Label label17;
        private SeeSharpTools.JY.GUI.EasyChartX easyChartXWaterfall;
    }
}

