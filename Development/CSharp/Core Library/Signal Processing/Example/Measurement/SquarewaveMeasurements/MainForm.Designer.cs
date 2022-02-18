namespace Winform_SquarewaveMeasurements
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
            this.DCOffset = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.DutyCycle = new System.Windows.Forms.NumericUpDown();
            this.label23 = new System.Windows.Forms.Label();
            this.Phase = new System.Windows.Forms.NumericUpDown();
            this.Amplitude = new System.Windows.Forms.NumericUpDown();
            this.Frequency = new System.Windows.Forms.NumericUpDown();
            this.SampleRate = new System.Windows.Forms.NumericUpDown();
            this.SamplesCount = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.easyChartWave = new SeeSharpTools.JY.GUI.EasyChart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelPhase = new System.Windows.Forms.Label();
            this.labeldutyCycle = new System.Windows.Forms.Label();
            this.labelPulseCount = new System.Windows.Forms.Label();
            this.labelMinWidth = new System.Windows.Forms.Label();
            this.labelMaxWidth = new System.Windows.Forms.Label();
            this.labelPeriod = new System.Windows.Forms.Label();
            this.labelLowLevel = new System.Windows.Forms.Label();
            this.labelHighLevel = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.groupBoxConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DCOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DutyCycle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Phase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amplitude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Frequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SampleRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SamplesCount)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxConfig
            // 
            this.groupBoxConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxConfig.Controls.Add(this.DCOffset);
            this.groupBoxConfig.Controls.Add(this.label4);
            this.groupBoxConfig.Controls.Add(this.DutyCycle);
            this.groupBoxConfig.Controls.Add(this.label23);
            this.groupBoxConfig.Controls.Add(this.Phase);
            this.groupBoxConfig.Controls.Add(this.Amplitude);
            this.groupBoxConfig.Controls.Add(this.Frequency);
            this.groupBoxConfig.Controls.Add(this.SampleRate);
            this.groupBoxConfig.Controls.Add(this.SamplesCount);
            this.groupBoxConfig.Controls.Add(this.label7);
            this.groupBoxConfig.Controls.Add(this.label1);
            this.groupBoxConfig.Controls.Add(this.label6);
            this.groupBoxConfig.Controls.Add(this.label2);
            this.groupBoxConfig.Controls.Add(this.label3);
            this.groupBoxConfig.Location = new System.Drawing.Point(511, 28);
            this.groupBoxConfig.Name = "groupBoxConfig";
            this.groupBoxConfig.Size = new System.Drawing.Size(369, 216);
            this.groupBoxConfig.TabIndex = 8;
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
            this.DCOffset.Location = new System.Drawing.Point(138, 183);
            this.DCOffset.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.DCOffset.Name = "DCOffset";
            this.DCOffset.Size = new System.Drawing.Size(219, 21);
            this.DCOffset.TabIndex = 6;
            this.DCOffset.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.No;
            this.label4.Location = new System.Drawing.Point(11, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "DC paranoia:";
            // 
            // DutyCycle
            // 
            this.DutyCycle.DecimalPlaces = 1;
            this.DutyCycle.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.DutyCycle.Location = new System.Drawing.Point(137, 131);
            this.DutyCycle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.DutyCycle.Name = "DutyCycle";
            this.DutyCycle.Size = new System.Drawing.Size(219, 21);
            this.DutyCycle.TabIndex = 4;
            this.DutyCycle.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(11, 135);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(77, 12);
            this.label23.TabIndex = 5;
            this.label23.Text = "Duty cycle：";
            // 
            // Phase
            // 
            this.Phase.DecimalPlaces = 1;
            this.Phase.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Phase.Location = new System.Drawing.Point(138, 158);
            this.Phase.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.Phase.Name = "Phase";
            this.Phase.Size = new System.Drawing.Size(219, 21);
            this.Phase.TabIndex = 2;
            this.Phase.Value = new decimal(new int[] {
            180,
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
            this.Amplitude.Location = new System.Drawing.Point(138, 103);
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
            this.Frequency.Location = new System.Drawing.Point(138, 76);
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
            this.SampleRate.Location = new System.Drawing.Point(138, 22);
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
            // SamplesCount
            // 
            this.SamplesCount.Location = new System.Drawing.Point(138, 49);
            this.SamplesCount.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.SamplesCount.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.SamplesCount.Name = "SamplesCount";
            this.SamplesCount.Size = new System.Drawing.Size(219, 21);
            this.SamplesCount.TabIndex = 2;
            this.SamplesCount.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "Phase:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sampling rate:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "Waveform amplitude:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Points:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 80);
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
            this.easyChartWave.Location = new System.Drawing.Point(0, 54);
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
            this.easyChartWave.Size = new System.Drawing.Size(504, 252);
            this.easyChartWave.TabIndex = 9;
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
            this.groupBox1.Controls.Add(this.labelPhase);
            this.groupBox1.Controls.Add(this.labeldutyCycle);
            this.groupBox1.Controls.Add(this.labelPulseCount);
            this.groupBox1.Controls.Add(this.labelMinWidth);
            this.groupBox1.Controls.Add(this.labelMaxWidth);
            this.groupBox1.Controls.Add(this.labelPeriod);
            this.groupBox1.Controls.Add(this.labelLowLevel);
            this.groupBox1.Controls.Add(this.labelHighLevel);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Location = new System.Drawing.Point(511, 250);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(369, 232);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Waveform GenerateConfiguration";
            // 
            // labelPhase
            // 
            this.labelPhase.AutoSize = true;
            this.labelPhase.Location = new System.Drawing.Point(311, 208);
            this.labelPhase.Name = "labelPhase";
            this.labelPhase.Size = new System.Drawing.Size(11, 12);
            this.labelPhase.TabIndex = 15;
            this.labelPhase.Text = "0";
            // 
            // labeldutyCycle
            // 
            this.labeldutyCycle.AutoSize = true;
            this.labeldutyCycle.Location = new System.Drawing.Point(311, 182);
            this.labeldutyCycle.Name = "labeldutyCycle";
            this.labeldutyCycle.Size = new System.Drawing.Size(11, 12);
            this.labeldutyCycle.TabIndex = 14;
            this.labeldutyCycle.Text = "0";
            // 
            // labelPulseCount
            // 
            this.labelPulseCount.AutoSize = true;
            this.labelPulseCount.Location = new System.Drawing.Point(311, 156);
            this.labelPulseCount.Name = "labelPulseCount";
            this.labelPulseCount.Size = new System.Drawing.Size(11, 12);
            this.labelPulseCount.TabIndex = 13;
            this.labelPulseCount.Text = "0";
            // 
            // labelMinWidth
            // 
            this.labelMinWidth.AutoSize = true;
            this.labelMinWidth.Location = new System.Drawing.Point(311, 130);
            this.labelMinWidth.Name = "labelMinWidth";
            this.labelMinWidth.Size = new System.Drawing.Size(11, 12);
            this.labelMinWidth.TabIndex = 12;
            this.labelMinWidth.Text = "0";
            // 
            // labelMaxWidth
            // 
            this.labelMaxWidth.AutoSize = true;
            this.labelMaxWidth.Location = new System.Drawing.Point(311, 104);
            this.labelMaxWidth.Name = "labelMaxWidth";
            this.labelMaxWidth.Size = new System.Drawing.Size(11, 12);
            this.labelMaxWidth.TabIndex = 11;
            this.labelMaxWidth.Text = "0";
            // 
            // labelPeriod
            // 
            this.labelPeriod.AutoSize = true;
            this.labelPeriod.Location = new System.Drawing.Point(311, 78);
            this.labelPeriod.Name = "labelPeriod";
            this.labelPeriod.Size = new System.Drawing.Size(11, 12);
            this.labelPeriod.TabIndex = 10;
            this.labelPeriod.Text = "0";
            // 
            // labelLowLevel
            // 
            this.labelLowLevel.AutoSize = true;
            this.labelLowLevel.Location = new System.Drawing.Point(311, 52);
            this.labelLowLevel.Name = "labelLowLevel";
            this.labelLowLevel.Size = new System.Drawing.Size(11, 12);
            this.labelLowLevel.TabIndex = 9;
            this.labelLowLevel.Text = "0";
            // 
            // labelHighLevel
            // 
            this.labelHighLevel.AutoSize = true;
            this.labelHighLevel.Location = new System.Drawing.Point(311, 26);
            this.labelHighLevel.Name = "labelHighLevel";
            this.labelHighLevel.Size = new System.Drawing.Size(11, 12);
            this.labelHighLevel.TabIndex = 8;
            this.labelHighLevel.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(11, 156);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(118, 12);
            this.label14.TabIndex = 7;
            this.label14.Text = "Number of pulses：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(11, 130);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(254, 12);
            this.label13.TabIndex = 6;
            this.label13.Text = "Minimum pulse width（Sampling points）：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "Duty cycle（%）：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 208);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "Phase（°）：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 12);
            this.label9.TabIndex = 3;
            this.label9.Text = "High level（V)：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 104);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(258, 12);
            this.label10.TabIndex = 3;
            this.label10.Text = "Maximum pulse width（Sampling points）：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 12);
            this.label11.TabIndex = 3;
            this.label11.Text = "Low level（V）：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 78);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(165, 12);
            this.label12.TabIndex = 3;
            this.label12.Text = "Cycle（Sampling points）：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label15.Location = new System.Drawing.Point(10, 28);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(143, 12);
            this.label15.TabIndex = 11;
            this.label15.Text = "Time domain waveform:";
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonStart.Location = new System.Drawing.Point(181, 391);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(119, 38);
            this.buttonStart.TabIndex = 12;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 495);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.easyChartWave);
            this.Controls.Add(this.groupBoxConfig);
            this.MaximumSize = new System.Drawing.Size(903, 534);
            this.MinimumSize = new System.Drawing.Size(903, 534);
            this.Name = "MainForm";
            this.Text = "SquarewaveMeasurements";
            this.groupBoxConfig.ResumeLayout(false);
            this.groupBoxConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DCOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DutyCycle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Phase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amplitude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Frequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SampleRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SamplesCount)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxConfig;
        private System.Windows.Forms.NumericUpDown Phase;
        private System.Windows.Forms.NumericUpDown Amplitude;
        private System.Windows.Forms.NumericUpDown Frequency;
        private System.Windows.Forms.NumericUpDown SampleRate;
        private System.Windows.Forms.NumericUpDown SamplesCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private SeeSharpTools.JY.GUI.EasyChart easyChartWave;
        private System.Windows.Forms.NumericUpDown DutyCycle;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label labelPhase;
        private System.Windows.Forms.Label labeldutyCycle;
        private System.Windows.Forms.Label labelPulseCount;
        private System.Windows.Forms.Label labelMinWidth;
        private System.Windows.Forms.Label labelMaxWidth;
        private System.Windows.Forms.Label labelPeriod;
        private System.Windows.Forms.Label labelLowLevel;
        private System.Windows.Forms.Label labelHighLevel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.NumericUpDown DCOffset;
        private System.Windows.Forms.Label label4;
    }
}

