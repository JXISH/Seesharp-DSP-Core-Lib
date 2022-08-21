namespace FrequencyShiftExample
{
    partial class Form1
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
            SeeSharpTools.JY.GUI.EasyChartSeries easyChartSeries2 = new SeeSharpTools.JY.GUI.EasyChartSeries();
            SeeSharpTools.JY.GUI.EasyChartSeries easyChartSeries3 = new SeeSharpTools.JY.GUI.EasyChartSeries();
            SeeSharpTools.JY.GUI.EasyChartSeries easyChartSeries4 = new SeeSharpTools.JY.GUI.EasyChartSeries();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Start = new System.Windows.Forms.Button();
            this.Frequency3 = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.Frequency2 = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.Frequency = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.SamplingRate = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.FreqOffset = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.DCoffset = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.Amplitude = new System.Windows.Forms.NumericUpDown();
            this.label001 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RawWaveForm = new SeeSharpTools.JY.GUI.EasyChart();
            this.TransformedWaveForm = new SeeSharpTools.JY.GUI.EasyChart();
            this.RawSpectrum = new SeeSharpTools.JY.GUI.EasyChart();
            this.TransformedSpectrum = new SeeSharpTools.JY.GUI.EasyChart();
            ((System.ComponentModel.ISupportInitialize)(this.Frequency3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Frequency2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Frequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SamplingRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FreqOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DCoffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amplitude)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("SimHei", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(-288, 188);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 15);
            this.label7.TabIndex = 20;
            this.label7.Text = "Raw Spectrum";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimHei", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(99, -101);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 15);
            this.label2.TabIndex = 19;
            this.label2.Text = "Transformed Waveform";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimHei", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(-288, -99);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 15);
            this.label1.TabIndex = 18;
            this.label1.Text = "Raw Waveform";
            // 
            // Start
            // 
            this.Start.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Start.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Start.Location = new System.Drawing.Point(835, 426);
            this.Start.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(156, 57);
            this.Start.TabIndex = 17;
            this.Start.Text = "Start to Shift!";
            this.Start.UseVisualStyleBackColor = false;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // Frequency3
            // 
            this.Frequency3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Frequency3.Location = new System.Drawing.Point(193, 309);
            this.Frequency3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Frequency3.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.Frequency3.Name = "Frequency3";
            this.Frequency3.Size = new System.Drawing.Size(131, 26);
            this.Frequency3.TabIndex = 15;
            this.Frequency3.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 309);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(119, 16);
            this.label10.TabIndex = 14;
            this.label10.Text = "Frequency3(Hz)";
            // 
            // Frequency2
            // 
            this.Frequency2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Frequency2.Location = new System.Drawing.Point(193, 264);
            this.Frequency2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Frequency2.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.Frequency2.Name = "Frequency2";
            this.Frequency2.Size = new System.Drawing.Size(131, 26);
            this.Frequency2.TabIndex = 13;
            this.Frequency2.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 264);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(119, 16);
            this.label9.TabIndex = 12;
            this.label9.Text = "Frequency2(Hz)";
            // 
            // Frequency
            // 
            this.Frequency.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Frequency.Location = new System.Drawing.Point(193, 221);
            this.Frequency.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Frequency.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.Frequency.Name = "Frequency";
            this.Frequency.Size = new System.Drawing.Size(131, 26);
            this.Frequency.TabIndex = 11;
            this.Frequency.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 221);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Frequency(Hz)";
            // 
            // SamplingRate
            // 
            this.SamplingRate.Location = new System.Drawing.Point(193, 177);
            this.SamplingRate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SamplingRate.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.SamplingRate.Name = "SamplingRate";
            this.SamplingRate.Size = new System.Drawing.Size(131, 26);
            this.SamplingRate.TabIndex = 9;
            this.SamplingRate.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 177);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "SamplingRate\r\n";
            // 
            // FreqOffset
            // 
            this.FreqOffset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.FreqOffset.Location = new System.Drawing.Point(193, 135);
            this.FreqOffset.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.FreqOffset.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.FreqOffset.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.FreqOffset.Name = "FreqOffset";
            this.FreqOffset.Size = new System.Drawing.Size(131, 26);
            this.FreqOffset.TabIndex = 7;
            this.FreqOffset.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 135);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "FreqOffset(Hz)";
            // 
            // DCoffset
            // 
            this.DCoffset.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.DCoffset.Location = new System.Drawing.Point(193, 93);
            this.DCoffset.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DCoffset.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.DCoffset.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            -2147483648});
            this.DCoffset.Name = "DCoffset";
            this.DCoffset.Size = new System.Drawing.Size(131, 26);
            this.DCoffset.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 94);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "DCoffset(V)";
            // 
            // Amplitude
            // 
            this.Amplitude.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Amplitude.Location = new System.Drawing.Point(193, 51);
            this.Amplitude.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Amplitude.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Amplitude.Name = "Amplitude";
            this.Amplitude.Size = new System.Drawing.Size(131, 26);
            this.Amplitude.TabIndex = 1;
            this.Amplitude.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label001
            // 
            this.label001.AutoSize = true;
            this.label001.Location = new System.Drawing.Point(13, 52);
            this.label001.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label001.Name = "label001";
            this.label001.Size = new System.Drawing.Size(103, 16);
            this.label001.TabIndex = 0;
            this.label001.Text = "Amplitude(V)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Frequency3);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.Frequency2);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.Frequency);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.SamplingRate);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.FreqOffset);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.DCoffset);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Amplitude);
            this.groupBox1.Controls.Add(this.label001);
            this.groupBox1.Font = new System.Drawing.Font("SimHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(739, 46);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(334, 349);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration";
            // 
            // RawWaveForm
            // 
            this.RawWaveForm.AxisX.AutoScale = true;
            this.RawWaveForm.AxisX.InitWithScaleView = false;
            this.RawWaveForm.AxisX.LabelEnabled = true;
            this.RawWaveForm.AxisX.LabelFormat = "";
            this.RawWaveForm.AxisX.Maximum = 1001D;
            this.RawWaveForm.AxisX.Minimum = 0D;
            this.RawWaveForm.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.RawWaveForm.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.RawWaveForm.AxisX.Title = "";
            this.RawWaveForm.AxisX.ViewMaximum = 1001D;
            this.RawWaveForm.AxisX.ViewMinimum = 0D;
            this.RawWaveForm.AxisY.AutoScale = true;
            this.RawWaveForm.AxisY.InitWithScaleView = false;
            this.RawWaveForm.AxisY.LabelEnabled = true;
            this.RawWaveForm.AxisY.LabelFormat = "";
            this.RawWaveForm.AxisY.Maximum = 3.5D;
            this.RawWaveForm.AxisY.Minimum = 0D;
            this.RawWaveForm.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.RawWaveForm.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.RawWaveForm.AxisY.Title = "";
            this.RawWaveForm.AxisY.ViewMaximum = 3.5D;
            this.RawWaveForm.AxisY.ViewMinimum = 0D;
            this.RawWaveForm.AxisYMax = 3.5D;
            this.RawWaveForm.AxisYMin = 0D;
            this.RawWaveForm.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.RawWaveForm.EasyChartBackColor = System.Drawing.Color.PeachPuff;
            this.RawWaveForm.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.RawWaveForm.LegendBackColor = System.Drawing.Color.Transparent;
            this.RawWaveForm.LegendVisible = true;
            easyChartSeries1.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries1.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries1.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.RawWaveForm.LineSeries.Add(easyChartSeries1);
            this.RawWaveForm.Location = new System.Drawing.Point(33, 28);
            this.RawWaveForm.MajorGridColor = System.Drawing.Color.Black;
            this.RawWaveForm.MajorGridEnabled = true;
            this.RawWaveForm.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.RawWaveForm.MinorGridColor = System.Drawing.Color.Black;
            this.RawWaveForm.MinorGridEnabled = false;
            this.RawWaveForm.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.RawWaveForm.Name = "RawWaveForm";
            this.RawWaveForm.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.RawWaveForm.SeriesNames = new string[] {
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
            this.RawWaveForm.Size = new System.Drawing.Size(328, 240);
            this.RawWaveForm.TabIndex = 21;
            this.RawWaveForm.XAxisLogarithmic = false;
            this.RawWaveForm.XAxisTitle = "";
            this.RawWaveForm.XCursor.AutoInterval = true;
            this.RawWaveForm.XCursor.Color = System.Drawing.Color.Red;
            this.RawWaveForm.XCursor.Interval = 1D;
            this.RawWaveForm.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.RawWaveForm.XCursor.Value = double.NaN;
            this.RawWaveForm.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.RawWaveForm.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.RawWaveForm.YAutoEnable = true;
            this.RawWaveForm.YAxisLogarithmic = false;
            this.RawWaveForm.YAxisTitle = "";
            this.RawWaveForm.YCursor.AutoInterval = true;
            this.RawWaveForm.YCursor.Color = System.Drawing.Color.Red;
            this.RawWaveForm.YCursor.Interval = 0.001D;
            this.RawWaveForm.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.RawWaveForm.YCursor.Value = double.NaN;
            this.RawWaveForm.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.RawWaveForm.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // TransformedWaveForm
            // 
            this.TransformedWaveForm.AxisX.AutoScale = true;
            this.TransformedWaveForm.AxisX.InitWithScaleView = false;
            this.TransformedWaveForm.AxisX.LabelEnabled = true;
            this.TransformedWaveForm.AxisX.LabelFormat = "";
            this.TransformedWaveForm.AxisX.Maximum = 1001D;
            this.TransformedWaveForm.AxisX.Minimum = 0D;
            this.TransformedWaveForm.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.TransformedWaveForm.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.TransformedWaveForm.AxisX.Title = "";
            this.TransformedWaveForm.AxisX.ViewMaximum = 1001D;
            this.TransformedWaveForm.AxisX.ViewMinimum = 0D;
            this.TransformedWaveForm.AxisY.AutoScale = true;
            this.TransformedWaveForm.AxisY.InitWithScaleView = false;
            this.TransformedWaveForm.AxisY.LabelEnabled = true;
            this.TransformedWaveForm.AxisY.LabelFormat = "";
            this.TransformedWaveForm.AxisY.Maximum = 3.5D;
            this.TransformedWaveForm.AxisY.Minimum = 0D;
            this.TransformedWaveForm.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.TransformedWaveForm.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.TransformedWaveForm.AxisY.Title = "";
            this.TransformedWaveForm.AxisY.ViewMaximum = 3.5D;
            this.TransformedWaveForm.AxisY.ViewMinimum = 0D;
            this.TransformedWaveForm.AxisYMax = 3.5D;
            this.TransformedWaveForm.AxisYMin = 0D;
            this.TransformedWaveForm.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.TransformedWaveForm.EasyChartBackColor = System.Drawing.Color.PeachPuff;
            this.TransformedWaveForm.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.TransformedWaveForm.LegendBackColor = System.Drawing.Color.Transparent;
            this.TransformedWaveForm.LegendVisible = true;
            easyChartSeries2.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries2.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries2.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.TransformedWaveForm.LineSeries.Add(easyChartSeries2);
            this.TransformedWaveForm.Location = new System.Drawing.Point(384, 28);
            this.TransformedWaveForm.MajorGridColor = System.Drawing.Color.Black;
            this.TransformedWaveForm.MajorGridEnabled = true;
            this.TransformedWaveForm.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.TransformedWaveForm.MinorGridColor = System.Drawing.Color.Black;
            this.TransformedWaveForm.MinorGridEnabled = false;
            this.TransformedWaveForm.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.TransformedWaveForm.Name = "TransformedWaveForm";
            this.TransformedWaveForm.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.TransformedWaveForm.SeriesNames = new string[] {
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
            this.TransformedWaveForm.Size = new System.Drawing.Size(328, 240);
            this.TransformedWaveForm.TabIndex = 22;
            this.TransformedWaveForm.XAxisLogarithmic = false;
            this.TransformedWaveForm.XAxisTitle = "";
            this.TransformedWaveForm.XCursor.AutoInterval = true;
            this.TransformedWaveForm.XCursor.Color = System.Drawing.Color.Red;
            this.TransformedWaveForm.XCursor.Interval = 1D;
            this.TransformedWaveForm.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.TransformedWaveForm.XCursor.Value = double.NaN;
            this.TransformedWaveForm.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.TransformedWaveForm.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.TransformedWaveForm.YAutoEnable = true;
            this.TransformedWaveForm.YAxisLogarithmic = false;
            this.TransformedWaveForm.YAxisTitle = "";
            this.TransformedWaveForm.YCursor.AutoInterval = true;
            this.TransformedWaveForm.YCursor.Color = System.Drawing.Color.Red;
            this.TransformedWaveForm.YCursor.Interval = 0.001D;
            this.TransformedWaveForm.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.TransformedWaveForm.YCursor.Value = double.NaN;
            this.TransformedWaveForm.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.TransformedWaveForm.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // RawSpectrum
            // 
            this.RawSpectrum.AxisX.AutoScale = true;
            this.RawSpectrum.AxisX.InitWithScaleView = false;
            this.RawSpectrum.AxisX.LabelEnabled = true;
            this.RawSpectrum.AxisX.LabelFormat = "";
            this.RawSpectrum.AxisX.Maximum = 1001D;
            this.RawSpectrum.AxisX.Minimum = 0D;
            this.RawSpectrum.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.RawSpectrum.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.RawSpectrum.AxisX.Title = "";
            this.RawSpectrum.AxisX.ViewMaximum = 1001D;
            this.RawSpectrum.AxisX.ViewMinimum = 0D;
            this.RawSpectrum.AxisY.AutoScale = true;
            this.RawSpectrum.AxisY.InitWithScaleView = false;
            this.RawSpectrum.AxisY.LabelEnabled = true;
            this.RawSpectrum.AxisY.LabelFormat = "";
            this.RawSpectrum.AxisY.Maximum = 3.5D;
            this.RawSpectrum.AxisY.Minimum = 0D;
            this.RawSpectrum.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.RawSpectrum.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.RawSpectrum.AxisY.Title = "";
            this.RawSpectrum.AxisY.ViewMaximum = 3.5D;
            this.RawSpectrum.AxisY.ViewMinimum = 0D;
            this.RawSpectrum.AxisYMax = 3.5D;
            this.RawSpectrum.AxisYMin = 0D;
            this.RawSpectrum.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.RawSpectrum.EasyChartBackColor = System.Drawing.Color.PeachPuff;
            this.RawSpectrum.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.RawSpectrum.LegendBackColor = System.Drawing.Color.Transparent;
            this.RawSpectrum.LegendVisible = true;
            easyChartSeries3.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries3.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries3.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.RawSpectrum.LineSeries.Add(easyChartSeries3);
            this.RawSpectrum.Location = new System.Drawing.Point(33, 289);
            this.RawSpectrum.MajorGridColor = System.Drawing.Color.Black;
            this.RawSpectrum.MajorGridEnabled = true;
            this.RawSpectrum.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.RawSpectrum.MinorGridColor = System.Drawing.Color.Black;
            this.RawSpectrum.MinorGridEnabled = false;
            this.RawSpectrum.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.RawSpectrum.Name = "RawSpectrum";
            this.RawSpectrum.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.RawSpectrum.SeriesNames = new string[] {
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
            this.RawSpectrum.Size = new System.Drawing.Size(328, 240);
            this.RawSpectrum.TabIndex = 23;
            this.RawSpectrum.XAxisLogarithmic = false;
            this.RawSpectrum.XAxisTitle = "";
            this.RawSpectrum.XCursor.AutoInterval = true;
            this.RawSpectrum.XCursor.Color = System.Drawing.Color.Red;
            this.RawSpectrum.XCursor.Interval = 1D;
            this.RawSpectrum.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.RawSpectrum.XCursor.Value = double.NaN;
            this.RawSpectrum.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.RawSpectrum.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.RawSpectrum.YAutoEnable = true;
            this.RawSpectrum.YAxisLogarithmic = false;
            this.RawSpectrum.YAxisTitle = "";
            this.RawSpectrum.YCursor.AutoInterval = true;
            this.RawSpectrum.YCursor.Color = System.Drawing.Color.Red;
            this.RawSpectrum.YCursor.Interval = 0.001D;
            this.RawSpectrum.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.RawSpectrum.YCursor.Value = double.NaN;
            this.RawSpectrum.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.RawSpectrum.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // TransformedSpectrum
            // 
            this.TransformedSpectrum.AxisX.AutoScale = true;
            this.TransformedSpectrum.AxisX.InitWithScaleView = false;
            this.TransformedSpectrum.AxisX.LabelEnabled = true;
            this.TransformedSpectrum.AxisX.LabelFormat = "";
            this.TransformedSpectrum.AxisX.Maximum = 1001D;
            this.TransformedSpectrum.AxisX.Minimum = 0D;
            this.TransformedSpectrum.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.TransformedSpectrum.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.TransformedSpectrum.AxisX.Title = "";
            this.TransformedSpectrum.AxisX.ViewMaximum = 1001D;
            this.TransformedSpectrum.AxisX.ViewMinimum = 0D;
            this.TransformedSpectrum.AxisY.AutoScale = true;
            this.TransformedSpectrum.AxisY.InitWithScaleView = false;
            this.TransformedSpectrum.AxisY.LabelEnabled = true;
            this.TransformedSpectrum.AxisY.LabelFormat = "";
            this.TransformedSpectrum.AxisY.Maximum = 3.5D;
            this.TransformedSpectrum.AxisY.Minimum = 0D;
            this.TransformedSpectrum.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.TransformedSpectrum.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.TransformedSpectrum.AxisY.Title = "";
            this.TransformedSpectrum.AxisY.ViewMaximum = 3.5D;
            this.TransformedSpectrum.AxisY.ViewMinimum = 0D;
            this.TransformedSpectrum.AxisYMax = 3.5D;
            this.TransformedSpectrum.AxisYMin = 0D;
            this.TransformedSpectrum.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.TransformedSpectrum.EasyChartBackColor = System.Drawing.Color.PeachPuff;
            this.TransformedSpectrum.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.TransformedSpectrum.LegendBackColor = System.Drawing.Color.Transparent;
            this.TransformedSpectrum.LegendVisible = true;
            easyChartSeries4.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries4.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries4.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.TransformedSpectrum.LineSeries.Add(easyChartSeries4);
            this.TransformedSpectrum.Location = new System.Drawing.Point(384, 289);
            this.TransformedSpectrum.MajorGridColor = System.Drawing.Color.Black;
            this.TransformedSpectrum.MajorGridEnabled = true;
            this.TransformedSpectrum.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.TransformedSpectrum.MinorGridColor = System.Drawing.Color.Black;
            this.TransformedSpectrum.MinorGridEnabled = false;
            this.TransformedSpectrum.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.TransformedSpectrum.Name = "TransformedSpectrum";
            this.TransformedSpectrum.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.TransformedSpectrum.SeriesNames = new string[] {
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
            this.TransformedSpectrum.Size = new System.Drawing.Size(328, 240);
            this.TransformedSpectrum.TabIndex = 24;
            this.TransformedSpectrum.XAxisLogarithmic = false;
            this.TransformedSpectrum.XAxisTitle = "";
            this.TransformedSpectrum.XCursor.AutoInterval = true;
            this.TransformedSpectrum.XCursor.Color = System.Drawing.Color.Red;
            this.TransformedSpectrum.XCursor.Interval = 1D;
            this.TransformedSpectrum.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.TransformedSpectrum.XCursor.Value = double.NaN;
            this.TransformedSpectrum.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.TransformedSpectrum.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.TransformedSpectrum.YAutoEnable = true;
            this.TransformedSpectrum.YAxisLogarithmic = false;
            this.TransformedSpectrum.YAxisTitle = "";
            this.TransformedSpectrum.YCursor.AutoInterval = true;
            this.TransformedSpectrum.YCursor.Color = System.Drawing.Color.Red;
            this.TransformedSpectrum.YCursor.Interval = 0.001D;
            this.TransformedSpectrum.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.TransformedSpectrum.YCursor.Value = double.NaN;
            this.TransformedSpectrum.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.TransformedSpectrum.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1271, 563);
            this.Controls.Add(this.TransformedSpectrum);
            this.Controls.Add(this.RawSpectrum);
            this.Controls.Add(this.TransformedWaveForm);
            this.Controls.Add(this.RawWaveForm);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Frequency Shift Example";
            ((System.ComponentModel.ISupportInitialize)(this.Frequency3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Frequency2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Frequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SamplingRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FreqOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DCoffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amplitude)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.NumericUpDown Frequency3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown Frequency2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown Frequency;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown SamplingRate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown FreqOffset;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown DCoffset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown Amplitude;
        private System.Windows.Forms.Label label001;
        private System.Windows.Forms.GroupBox groupBox1;
        private SeeSharpTools.JY.GUI.EasyChart RawWaveForm;
        private SeeSharpTools.JY.GUI.EasyChart TransformedWaveForm;
        private SeeSharpTools.JY.GUI.EasyChart RawSpectrum;
        private SeeSharpTools.JY.GUI.EasyChart TransformedSpectrum;
    }
}

