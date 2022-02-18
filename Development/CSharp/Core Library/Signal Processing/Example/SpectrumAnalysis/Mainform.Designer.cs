namespace PowerSpectrum_Demo
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
            SeeSharpTools.JY.GUI.EasyChartSeries easyChartSeries1 = new SeeSharpTools.JY.GUI.EasyChartSeries();
            SeeSharpTools.JY.GUI.EasyChartSeries easyChartSeries2 = new SeeSharpTools.JY.GUI.EasyChartSeries();
            this.easyChart1 = new SeeSharpTools.JY.GUI.EasyChart();
            this.easyChart2 = new SeeSharpTools.JY.GUI.EasyChart();
            this.button1 = new System.Windows.Forms.Button();
            this.SampleRate = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.SampleCount = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.Frequency = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.SpectrumUnits = new System.Windows.Forms.ComboBox();
            this.WindowTypes = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SignalType = new System.Windows.Forms.ComboBox();
            this.DCOffset = new System.Windows.Forms.NumericUpDown();
            this.Amplitude = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.AverageSize = new System.Windows.Forms.NumericUpDown();
            this.SpectralLineCount = new System.Windows.Forms.NumericUpDown();
            this.WeightType = new System.Windows.Forms.ComboBox();
            this.AverageMode = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelPowerInBand = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.labelPeak = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.FFTCount = new System.Windows.Forms.Label();
            this.FFTSize = new System.Windows.Forms.Label();
            this.df = new System.Windows.Forms.Label();
            this.f0 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.CommitConfig = new System.Windows.Forms.Button();
            this.ResetConfig = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.ElapsedTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SampleRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SampleCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Frequency)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DCOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amplitude)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AverageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpectralLineCount)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // easyChart1
            // 
            this.easyChart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.easyChart1.BackGradientStyle = null;
            this.easyChart1.ChartAreaBackColor = System.Drawing.Color.Transparent;
            this.easyChart1.EasyChartBackColor = System.Drawing.Color.White;
            this.easyChart1.FixAxisX = false;
            this.easyChart1.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.easyChart1.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChart1.LegendVisible = true;
            easyChartSeries1.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries1.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries1.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.easyChart1.LineSeries.Add(easyChartSeries1);
            this.easyChart1.Location = new System.Drawing.Point(13, 11);
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
            this.easyChart1.Size = new System.Drawing.Size(814, 299);
            this.easyChart1.TabIndex = 0;
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
            // easyChart2
            // 
            this.easyChart2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.easyChart2.AxisX.AutoScale = true;
            this.easyChart2.AxisX.InitWithScaleView = false;
            this.easyChart2.AxisX.LabelEnabled = true;
            this.easyChart2.AxisX.LabelFormat = "";
            this.easyChart2.AxisX.Maximum = 1001D;
            this.easyChart2.AxisX.Minimum = 0D;
            this.easyChart2.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChart2.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChart2.AxisX.Title = "";
            this.easyChart2.AxisX.ViewMaximum = 1001D;
            this.easyChart2.AxisX.ViewMinimum = 0D;
            this.easyChart2.AxisY.AutoScale = true;
            this.easyChart2.AxisY.InitWithScaleView = false;
            this.easyChart2.AxisY.LabelEnabled = true;
            this.easyChart2.AxisY.LabelFormat = "";
            this.easyChart2.AxisY.Maximum = 3.5D;
            this.easyChart2.AxisY.Minimum = 0D;
            this.easyChart2.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChart2.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChart2.AxisY.Title = "";
            this.easyChart2.AxisY.ViewMaximum = 3.5D;
            this.easyChart2.AxisY.ViewMinimum = 0D;
            this.easyChart2.AxisYMax = 3.5D;
            this.easyChart2.AxisYMin = 0D;
            this.easyChart2.BackGradientStyle = null;
            this.easyChart2.ChartAreaBackColor = System.Drawing.Color.Transparent;
            this.easyChart2.EasyChartBackColor = System.Drawing.Color.White;
            this.easyChart2.FixAxisX = false;
            this.easyChart2.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.easyChart2.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChart2.LegendVisible = true;
            easyChartSeries2.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries2.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries2.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.easyChart2.LineSeries.Add(easyChartSeries2);
            this.easyChart2.Location = new System.Drawing.Point(13, 314);
            this.easyChart2.MajorGridColor = System.Drawing.Color.Black;
            this.easyChart2.MajorGridEnabled = true;
            this.easyChart2.Margin = new System.Windows.Forms.Padding(2);
            this.easyChart2.MinorGridColor = System.Drawing.Color.Black;
            this.easyChart2.MinorGridEnabled = false;
            this.easyChart2.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.easyChart2.Name = "easyChart2";
            this.easyChart2.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.easyChart2.SeriesNames = new string[] {
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
            this.easyChart2.Size = new System.Drawing.Size(814, 291);
            this.easyChart2.TabIndex = 0;
            this.easyChart2.XAxisLogarithmic = false;
            this.easyChart2.XAxisTitle = "";
            this.easyChart2.XCursor.AutoInterval = true;
            this.easyChart2.XCursor.Color = System.Drawing.Color.Red;
            this.easyChart2.XCursor.Interval = 1D;
            this.easyChart2.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.easyChart2.XCursor.Value = double.NaN;
            this.easyChart2.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChart2.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChart2.YAutoEnable = true;
            this.easyChart2.YAxisLogarithmic = false;
            this.easyChart2.YAxisTitle = "";
            this.easyChart2.YCursor.AutoInterval = true;
            this.easyChart2.YCursor.Color = System.Drawing.Color.Red;
            this.easyChart2.YCursor.Interval = 0.001D;
            this.easyChart2.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.easyChart2.YCursor.Value = double.NaN;
            this.easyChart2.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChart2.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1093, 566);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 41);
            this.button1.TabIndex = 1;
            this.button1.Text = "Generate and analyze";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SampleRate
            // 
            this.SampleRate.Location = new System.Drawing.Point(135, 50);
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
            10000,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sampling rate:";
            // 
            // SampleCount
            // 
            this.SampleCount.Location = new System.Drawing.Point(135, 77);
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
            this.SampleCount.Size = new System.Drawing.Size(219, 21);
            this.SampleCount.TabIndex = 2;
            this.SampleCount.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.SampleCount.ValueChanged += new System.EventHandler(this.SampleCount_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Points:";
            // 
            // Frequency
            // 
            this.Frequency.Location = new System.Drawing.Point(135, 104);
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "Waveform frequency:";
            // 
            // SpectrumUnits
            // 
            this.SpectrumUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SpectrumUnits.FormattingEnabled = true;
            this.SpectrumUnits.Location = new System.Drawing.Point(137, 23);
            this.SpectrumUnits.Name = "SpectrumUnits";
            this.SpectrumUnits.Size = new System.Drawing.Size(219, 20);
            this.SpectrumUnits.TabIndex = 4;
            // 
            // WindowTypes
            // 
            this.WindowTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WindowTypes.FormattingEnabled = true;
            this.WindowTypes.Location = new System.Drawing.Point(137, 54);
            this.WindowTypes.Name = "WindowTypes";
            this.WindowTypes.Size = new System.Drawing.Size(219, 20);
            this.WindowTypes.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "SpectrumUnit：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "Window type:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.SignalType);
            this.groupBox1.Controls.Add(this.DCOffset);
            this.groupBox1.Controls.Add(this.Amplitude);
            this.groupBox1.Controls.Add(this.Frequency);
            this.groupBox1.Controls.Add(this.SampleRate);
            this.groupBox1.Controls.Add(this.SampleCount);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(836, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(368, 193);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Waveform GenerateConfiguration";
            // 
            // SignalType
            // 
            this.SignalType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SignalType.FormattingEnabled = true;
            this.SignalType.Location = new System.Drawing.Point(135, 22);
            this.SignalType.Name = "SignalType";
            this.SignalType.Size = new System.Drawing.Size(219, 20);
            this.SignalType.TabIndex = 4;
            this.SignalType.SelectedIndexChanged += new System.EventHandler(this.SignalType_SelectedIndexChanged);
            // 
            // DCOffset
            // 
            this.DCOffset.DecimalPlaces = 1;
            this.DCOffset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.DCOffset.Location = new System.Drawing.Point(135, 158);
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
            this.Amplitude.Location = new System.Drawing.Point(135, 131);
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "DC offset:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(93, 12);
            this.label12.TabIndex = 3;
            this.label12.Text = "Signal types of:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "Waveform amplitude:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.AverageSize);
            this.groupBox2.Controls.Add(this.SpectralLineCount);
            this.groupBox2.Controls.Add(this.WeightType);
            this.groupBox2.Controls.Add(this.AverageMode);
            this.groupBox2.Controls.Add(this.WindowTypes);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.SpectrumUnits);
            this.groupBox2.Location = new System.Drawing.Point(836, 210);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(368, 198);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SpectrumConfiguration";
            // 
            // AverageSize
            // 
            this.AverageSize.DecimalPlaces = 1;
            this.AverageSize.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.AverageSize.Location = new System.Drawing.Point(137, 167);
            this.AverageSize.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.AverageSize.Name = "AverageSize";
            this.AverageSize.Size = new System.Drawing.Size(219, 21);
            this.AverageSize.TabIndex = 2;
            this.AverageSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.AverageSize.ValueChanged += new System.EventHandler(this.AverageSize_ValueChanged);
            // 
            // SpectralLineCount
            // 
            this.SpectralLineCount.DecimalPlaces = 1;
            this.SpectralLineCount.Enabled = false;
            this.SpectralLineCount.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.SpectralLineCount.Location = new System.Drawing.Point(137, 84);
            this.SpectralLineCount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.SpectralLineCount.Name = "SpectralLineCount";
            this.SpectralLineCount.Size = new System.Drawing.Size(219, 21);
            this.SpectralLineCount.TabIndex = 2;
            this.SpectralLineCount.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // WeightType
            // 
            this.WeightType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WeightType.FormattingEnabled = true;
            this.WeightType.Location = new System.Drawing.Point(137, 139);
            this.WeightType.Name = "WeightType";
            this.WeightType.Size = new System.Drawing.Size(219, 20);
            this.WeightType.TabIndex = 4;
            this.WeightType.SelectedIndexChanged += new System.EventHandler(this.WeightType_SelectedIndexChanged);
            // 
            // AverageMode
            // 
            this.AverageMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AverageMode.FormattingEnabled = true;
            this.AverageMode.Location = new System.Drawing.Point(137, 113);
            this.AverageMode.Name = "AverageMode";
            this.AverageMode.Size = new System.Drawing.Size(219, 20);
            this.AverageMode.TabIndex = 4;
            this.AverageMode.SelectedIndexChanged += new System.EventHandler(this.AverageMode_SelectedIndexChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 172);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(114, 12);
            this.label19.TabIndex = 3;
            this.label19.Text = "Average # of times:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(8, 143);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(109, 12);
            this.label18.TabIndex = 3;
            this.label18.Text = "Weighting method:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 117);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(101, 12);
            this.label17.TabIndex = 3;
            this.label17.Text = "Average method:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "Number of lines:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(694, 237);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "Time domain signal";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(715, 404);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 7;
            this.label10.Text = "Spectrum";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.labelPowerInBand);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.labelPeak);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.FFTCount);
            this.groupBox3.Controls.Add(this.FFTSize);
            this.groupBox3.Controls.Add(this.df);
            this.groupBox3.Controls.Add(this.f0);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Location = new System.Drawing.Point(836, 461);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(368, 99);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Output information";
            // 
            // labelPowerInBand
            // 
            this.labelPowerInBand.AutoSize = true;
            this.labelPowerInBand.Location = new System.Drawing.Point(276, 75);
            this.labelPowerInBand.Name = "labelPowerInBand";
            this.labelPowerInBand.Size = new System.Drawing.Size(11, 12);
            this.labelPowerInBand.TabIndex = 7;
            this.labelPowerInBand.Text = "0";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(174, 75);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(91, 12);
            this.label21.TabIndex = 6;
            this.label21.Text = "powerInBand：";
            // 
            // labelPeak
            // 
            this.labelPeak.AutoSize = true;
            this.labelPeak.Location = new System.Drawing.Point(82, 75);
            this.labelPeak.Name = "labelPeak";
            this.labelPeak.Size = new System.Drawing.Size(11, 12);
            this.labelPeak.TabIndex = 5;
            this.labelPeak.Text = "0";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(28, 75);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(45, 12);
            this.label20.TabIndex = 4;
            this.label20.Text = "Peak：";
            // 
            // FFTCount
            // 
            this.FFTCount.AutoSize = true;
            this.FFTCount.Location = new System.Drawing.Point(275, 49);
            this.FFTCount.Name = "FFTCount";
            this.FFTCount.Size = new System.Drawing.Size(11, 12);
            this.FFTCount.TabIndex = 3;
            this.FFTCount.Text = "0";
            // 
            // FFTSize
            // 
            this.FFTSize.AutoSize = true;
            this.FFTSize.Location = new System.Drawing.Point(83, 49);
            this.FFTSize.Name = "FFTSize";
            this.FFTSize.Size = new System.Drawing.Size(11, 12);
            this.FFTSize.TabIndex = 3;
            this.FFTSize.Text = "0";
            // 
            // df
            // 
            this.df.AutoSize = true;
            this.df.Location = new System.Drawing.Point(275, 21);
            this.df.Name = "df";
            this.df.Size = new System.Drawing.Size(11, 12);
            this.df.TabIndex = 3;
            this.df.Text = "0";
            // 
            // f0
            // 
            this.f0.AutoSize = true;
            this.f0.Location = new System.Drawing.Point(83, 21);
            this.f0.Name = "f0";
            this.f0.Size = new System.Drawing.Size(11, 12);
            this.f0.TabIndex = 3;
            this.f0.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(16, 21);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 12);
            this.label15.TabIndex = 3;
            this.label15.Text = "f0(Hz)：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(209, 21);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 12);
            this.label13.TabIndex = 3;
            this.label13.Text = "df(Hz)：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(193, 49);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(72, 12);
            this.label16.TabIndex = 3;
            this.label16.Text = "FFTCount：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 47);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 12);
            this.label14.TabIndex = 3;
            this.label14.Text = "FFTSize：";
            // 
            // CommitConfig
            // 
            this.CommitConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CommitConfig.Location = new System.Drawing.Point(1092, 414);
            this.CommitConfig.Name = "CommitConfig";
            this.CommitConfig.Size = new System.Drawing.Size(108, 41);
            this.CommitConfig.TabIndex = 1;
            this.CommitConfig.Text = "Submit configuration";
            this.CommitConfig.UseVisualStyleBackColor = true;
            this.CommitConfig.Click += new System.EventHandler(this.CommitConfig_Click);
            // 
            // ResetConfig
            // 
            this.ResetConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ResetConfig.Enabled = false;
            this.ResetConfig.Location = new System.Drawing.Point(887, 414);
            this.ResetConfig.Name = "ResetConfig";
            this.ResetConfig.Size = new System.Drawing.Size(108, 41);
            this.ResetConfig.TabIndex = 1;
            this.ResetConfig.Text = "Reset Configuration";
            this.ResetConfig.UseVisualStyleBackColor = true;
            this.ResetConfig.Click += new System.EventHandler(this.ResetConfig_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(878, 581);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(105, 12);
            this.label11.TabIndex = 3;
            this.label11.Text = "Time spent (ms):";
            // 
            // ElapsedTime
            // 
            this.ElapsedTime.AutoSize = true;
            this.ElapsedTime.Location = new System.Drawing.Point(997, 581);
            this.ElapsedTime.Name = "ElapsedTime";
            this.ElapsedTime.Size = new System.Drawing.Size(11, 12);
            this.ElapsedTime.TabIndex = 3;
            this.ElapsedTime.Text = "0";
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 615);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.ElapsedTime);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ResetConfig);
            this.Controls.Add(this.CommitConfig);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.easyChart2);
            this.Controls.Add(this.easyChart1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1230, 654);
            this.MinimumSize = new System.Drawing.Size(1230, 654);
            this.Name = "Mainform";
            this.Text = "Spectrum Winform";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SampleRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SampleCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Frequency)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DCOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amplitude)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AverageSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpectralLineCount)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SeeSharpTools.JY.GUI.EasyChart easyChart1;
        private SeeSharpTools.JY.GUI.EasyChart easyChart2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown SampleRate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown SampleCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown Frequency;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox SpectrumUnits;
        private System.Windows.Forms.ComboBox WindowTypes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown DCOffset;
        private System.Windows.Forms.NumericUpDown Amplitude;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown SpectralLineCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox SignalType;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label FFTCount;
        private System.Windows.Forms.Label FFTSize;
        private System.Windows.Forms.Label df;
        private System.Windows.Forms.Label f0;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button CommitConfig;
        private System.Windows.Forms.Button ResetConfig;
        private System.Windows.Forms.ComboBox WeightType;
        private System.Windows.Forms.ComboBox AverageMode;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown AverageSize;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label ElapsedTime;
        private System.Windows.Forms.Label labelPowerInBand;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label labelPeak;
        private System.Windows.Forms.Label label20;
    }
}

