namespace GaussianWhiteNoise_Winform
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
            SeeSharpTools.JY.GUI.EasyChartSeries easyChartSeries2 = new SeeSharpTools.JY.GUI.EasyChartSeries();
            SeeSharpTools.JY.GUI.EasyChartSeries easyChartSeries3 = new SeeSharpTools.JY.GUI.EasyChartSeries();
            this.easyChartNoise = new SeeSharpTools.JY.GUI.EasyChart();
            this.groupBoxParaConfig = new System.Windows.Forms.GroupBox();
            this.numericUpDownLength = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownStdev = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxHistogramConfig = new System.Windows.Forms.GroupBox();
            this.numericUpDownNumber = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.easyChartHistogram = new SeeSharpTools.JY.GUI.EasyChart();
            this.easyChartSpec = new SeeSharpTools.JY.GUI.EasyChart();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBoxParaConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStdev)).BeginInit();
            this.groupBoxHistogramConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // easyChartNoise
            // 
            this.easyChartNoise.AxisX.AutoScale = true;
            this.easyChartNoise.AxisX.InitWithScaleView = false;
            this.easyChartNoise.AxisX.LabelEnabled = true;
            this.easyChartNoise.AxisX.LabelFormat = "";
            this.easyChartNoise.AxisX.Maximum = 1001D;
            this.easyChartNoise.AxisX.Minimum = 0D;
            this.easyChartNoise.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartNoise.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartNoise.AxisX.Title = "";
            this.easyChartNoise.AxisX.ViewMaximum = 1001D;
            this.easyChartNoise.AxisX.ViewMinimum = 0D;
            this.easyChartNoise.AxisY.AutoScale = true;
            this.easyChartNoise.AxisY.InitWithScaleView = false;
            this.easyChartNoise.AxisY.LabelEnabled = true;
            this.easyChartNoise.AxisY.LabelFormat = "";
            this.easyChartNoise.AxisY.Maximum = 3.5D;
            this.easyChartNoise.AxisY.Minimum = 0D;
            this.easyChartNoise.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartNoise.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartNoise.AxisY.Title = "";
            this.easyChartNoise.AxisY.ViewMaximum = 3.5D;
            this.easyChartNoise.AxisY.ViewMinimum = 0D;
            this.easyChartNoise.AxisYMax = 3.5D;
            this.easyChartNoise.AxisYMin = 0D;
            this.easyChartNoise.BackGradientStyle = null;
            this.easyChartNoise.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartNoise.EasyChartBackColor = System.Drawing.Color.White;
            this.easyChartNoise.FixAxisX = false;
            this.easyChartNoise.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.easyChartNoise.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartNoise.LegendVisible = true;
            easyChartSeries1.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries1.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries1.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.easyChartNoise.LineSeries.Add(easyChartSeries1);
            this.easyChartNoise.Location = new System.Drawing.Point(13, 39);
            this.easyChartNoise.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartNoise.MajorGridEnabled = true;
            this.easyChartNoise.Margin = new System.Windows.Forms.Padding(2);
            this.easyChartNoise.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartNoise.MinorGridEnabled = false;
            this.easyChartNoise.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.easyChartNoise.Name = "easyChartNoise";
            this.easyChartNoise.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.easyChartNoise.SeriesNames = new string[] {
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
            this.easyChartNoise.Size = new System.Drawing.Size(525, 183);
            this.easyChartNoise.TabIndex = 0;
            this.easyChartNoise.XAxisLogarithmic = false;
            this.easyChartNoise.XAxisTitle = "";
            this.easyChartNoise.XCursor.AutoInterval = true;
            this.easyChartNoise.XCursor.Color = System.Drawing.Color.Red;
            this.easyChartNoise.XCursor.Interval = 1D;
            this.easyChartNoise.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.easyChartNoise.XCursor.Value = double.NaN;
            this.easyChartNoise.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartNoise.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartNoise.YAutoEnable = true;
            this.easyChartNoise.YAxisLogarithmic = false;
            this.easyChartNoise.YAxisTitle = "";
            this.easyChartNoise.YCursor.AutoInterval = true;
            this.easyChartNoise.YCursor.Color = System.Drawing.Color.Red;
            this.easyChartNoise.YCursor.Interval = 0.001D;
            this.easyChartNoise.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.easyChartNoise.YCursor.Value = double.NaN;
            this.easyChartNoise.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartNoise.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // groupBoxParaConfig
            // 
            this.groupBoxParaConfig.Controls.Add(this.numericUpDownLength);
            this.groupBoxParaConfig.Controls.Add(this.label5);
            this.groupBoxParaConfig.Controls.Add(this.numericUpDownStdev);
            this.groupBoxParaConfig.Controls.Add(this.label2);
            this.groupBoxParaConfig.Location = new System.Drawing.Point(562, 31);
            this.groupBoxParaConfig.Name = "groupBoxParaConfig";
            this.groupBoxParaConfig.Size = new System.Drawing.Size(287, 89);
            this.groupBoxParaConfig.TabIndex = 1;
            this.groupBoxParaConfig.TabStop = false;
            this.groupBoxParaConfig.Text = "Parameter configuration";
            // 
            // numericUpDownLength
            // 
            this.numericUpDownLength.Location = new System.Drawing.Point(133, 25);
            this.numericUpDownLength.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownLength.Name = "numericUpDownLength";
            this.numericUpDownLength.Size = new System.Drawing.Size(140, 21);
            this.numericUpDownLength.TabIndex = 7;
            this.numericUpDownLength.Value = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "Data length：";
            // 
            // numericUpDownStdev
            // 
            this.numericUpDownStdev.Location = new System.Drawing.Point(133, 52);
            this.numericUpDownStdev.Name = "numericUpDownStdev";
            this.numericUpDownStdev.Size = new System.Drawing.Size(140, 21);
            this.numericUpDownStdev.TabIndex = 5;
            this.numericUpDownStdev.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Standard deviation:";
            // 
            // groupBoxHistogramConfig
            // 
            this.groupBoxHistogramConfig.Controls.Add(this.numericUpDownNumber);
            this.groupBoxHistogramConfig.Controls.Add(this.label6);
            this.groupBoxHistogramConfig.Location = new System.Drawing.Point(562, 126);
            this.groupBoxHistogramConfig.Name = "groupBoxHistogramConfig";
            this.groupBoxHistogramConfig.Size = new System.Drawing.Size(287, 66);
            this.groupBoxHistogramConfig.TabIndex = 2;
            this.groupBoxHistogramConfig.TabStop = false;
            this.groupBoxHistogramConfig.Text = "Statistical configuration";
            // 
            // numericUpDownNumber
            // 
            this.numericUpDownNumber.Location = new System.Drawing.Point(149, 27);
            this.numericUpDownNumber.Name = "numericUpDownNumber";
            this.numericUpDownNumber.Size = new System.Drawing.Size(106, 21);
            this.numericUpDownNumber.TabIndex = 8;
            this.numericUpDownNumber.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "Bar chart number:";
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(708, 199);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(87, 23);
            this.buttonGenerate.TabIndex = 3;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(134, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "Gaussian White Noise:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 236);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(150, 12);
            this.label8.TabIndex = 9;
            this.label8.Text = "Statistical characteristics:";
            // 
            // easyChartHistogram
            // 
            this.easyChartHistogram.AxisX.AutoScale = true;
            this.easyChartHistogram.AxisX.InitWithScaleView = false;
            this.easyChartHistogram.AxisX.LabelEnabled = true;
            this.easyChartHistogram.AxisX.LabelFormat = "";
            this.easyChartHistogram.AxisX.Maximum = 1001D;
            this.easyChartHistogram.AxisX.Minimum = 0D;
            this.easyChartHistogram.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartHistogram.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartHistogram.AxisX.Title = "";
            this.easyChartHistogram.AxisX.ViewMaximum = 1001D;
            this.easyChartHistogram.AxisX.ViewMinimum = 0D;
            this.easyChartHistogram.AxisY.AutoScale = true;
            this.easyChartHistogram.AxisY.InitWithScaleView = false;
            this.easyChartHistogram.AxisY.LabelEnabled = true;
            this.easyChartHistogram.AxisY.LabelFormat = "";
            this.easyChartHistogram.AxisY.Maximum = 3.5D;
            this.easyChartHistogram.AxisY.Minimum = 0D;
            this.easyChartHistogram.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartHistogram.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartHistogram.AxisY.Title = "";
            this.easyChartHistogram.AxisY.ViewMaximum = 3.5D;
            this.easyChartHistogram.AxisY.ViewMinimum = 0D;
            this.easyChartHistogram.AxisYMax = 3.5D;
            this.easyChartHistogram.AxisYMin = 0D;
            this.easyChartHistogram.BackGradientStyle = null;
            this.easyChartHistogram.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartHistogram.EasyChartBackColor = System.Drawing.Color.White;
            this.easyChartHistogram.FixAxisX = false;
            this.easyChartHistogram.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.easyChartHistogram.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartHistogram.LegendVisible = true;
            easyChartSeries2.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries2.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries2.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.easyChartHistogram.LineSeries.Add(easyChartSeries2);
            this.easyChartHistogram.Location = new System.Drawing.Point(13, 254);
            this.easyChartHistogram.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartHistogram.MajorGridEnabled = true;
            this.easyChartHistogram.Margin = new System.Windows.Forms.Padding(2);
            this.easyChartHistogram.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartHistogram.MinorGridEnabled = false;
            this.easyChartHistogram.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.easyChartHistogram.Name = "easyChartHistogram";
            this.easyChartHistogram.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.easyChartHistogram.SeriesNames = new string[] {
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
            this.easyChartHistogram.Size = new System.Drawing.Size(525, 183);
            this.easyChartHistogram.TabIndex = 10;
            this.easyChartHistogram.XAxisLogarithmic = false;
            this.easyChartHistogram.XAxisTitle = "";
            this.easyChartHistogram.XCursor.AutoInterval = true;
            this.easyChartHistogram.XCursor.Color = System.Drawing.Color.Red;
            this.easyChartHistogram.XCursor.Interval = 1D;
            this.easyChartHistogram.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.easyChartHistogram.XCursor.Value = double.NaN;
            this.easyChartHistogram.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartHistogram.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartHistogram.YAutoEnable = true;
            this.easyChartHistogram.YAxisLogarithmic = false;
            this.easyChartHistogram.YAxisTitle = "";
            this.easyChartHistogram.YCursor.AutoInterval = true;
            this.easyChartHistogram.YCursor.Color = System.Drawing.Color.Red;
            this.easyChartHistogram.YCursor.Interval = 0.001D;
            this.easyChartHistogram.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.easyChartHistogram.YCursor.Value = double.NaN;
            this.easyChartHistogram.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartHistogram.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // easyChartSpec
            // 
            this.easyChartSpec.AxisX.AutoScale = true;
            this.easyChartSpec.AxisX.InitWithScaleView = false;
            this.easyChartSpec.AxisX.LabelEnabled = true;
            this.easyChartSpec.AxisX.LabelFormat = "";
            this.easyChartSpec.AxisX.Maximum = 1001D;
            this.easyChartSpec.AxisX.Minimum = 0D;
            this.easyChartSpec.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartSpec.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartSpec.AxisX.Title = "";
            this.easyChartSpec.AxisX.ViewMaximum = 1001D;
            this.easyChartSpec.AxisX.ViewMinimum = 0D;
            this.easyChartSpec.AxisY.AutoScale = true;
            this.easyChartSpec.AxisY.InitWithScaleView = false;
            this.easyChartSpec.AxisY.LabelEnabled = true;
            this.easyChartSpec.AxisY.LabelFormat = "";
            this.easyChartSpec.AxisY.Maximum = 3.5D;
            this.easyChartSpec.AxisY.Minimum = 0D;
            this.easyChartSpec.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartSpec.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartSpec.AxisY.Title = "";
            this.easyChartSpec.AxisY.ViewMaximum = 3.5D;
            this.easyChartSpec.AxisY.ViewMinimum = 0D;
            this.easyChartSpec.AxisYMax = 3.5D;
            this.easyChartSpec.AxisYMin = 0D;
            this.easyChartSpec.BackGradientStyle = null;
            this.easyChartSpec.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartSpec.EasyChartBackColor = System.Drawing.Color.White;
            this.easyChartSpec.FixAxisX = false;
            this.easyChartSpec.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.easyChartSpec.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartSpec.LegendVisible = true;
            easyChartSeries3.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries3.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries3.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.easyChartSpec.LineSeries.Add(easyChartSeries3);
            this.easyChartSpec.Location = new System.Drawing.Point(562, 254);
            this.easyChartSpec.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartSpec.MajorGridEnabled = true;
            this.easyChartSpec.Margin = new System.Windows.Forms.Padding(2);
            this.easyChartSpec.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartSpec.MinorGridEnabled = false;
            this.easyChartSpec.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.easyChartSpec.Name = "easyChartSpec";
            this.easyChartSpec.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.easyChartSpec.SeriesNames = new string[] {
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
            this.easyChartSpec.Size = new System.Drawing.Size(525, 183);
            this.easyChartSpec.TabIndex = 11;
            this.easyChartSpec.XAxisLogarithmic = false;
            this.easyChartSpec.XAxisTitle = "";
            this.easyChartSpec.XCursor.AutoInterval = true;
            this.easyChartSpec.XCursor.Color = System.Drawing.Color.Red;
            this.easyChartSpec.XCursor.Interval = 1D;
            this.easyChartSpec.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.easyChartSpec.XCursor.Value = double.NaN;
            this.easyChartSpec.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartSpec.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartSpec.YAutoEnable = true;
            this.easyChartSpec.YAxisLogarithmic = false;
            this.easyChartSpec.YAxisTitle = "";
            this.easyChartSpec.YCursor.AutoInterval = true;
            this.easyChartSpec.YCursor.Color = System.Drawing.Color.Red;
            this.easyChartSpec.YCursor.Interval = 0.001D;
            this.easyChartSpec.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.easyChartSpec.YCursor.Value = double.NaN;
            this.easyChartSpec.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartSpec.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(569, 236);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "Power spectrum:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 451);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.easyChartSpec);
            this.Controls.Add(this.easyChartHistogram);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.groupBoxHistogramConfig);
            this.Controls.Add(this.groupBoxParaConfig);
            this.Controls.Add(this.easyChartNoise);
            this.Name = "MainForm";
            this.Text = "GaussianWhiteNoise";
            this.groupBoxParaConfig.ResumeLayout(false);
            this.groupBoxParaConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStdev)).EndInit();
            this.groupBoxHistogramConfig.ResumeLayout(false);
            this.groupBoxHistogramConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SeeSharpTools.JY.GUI.EasyChart easyChartNoise;
        private System.Windows.Forms.GroupBox groupBoxParaConfig;
        private System.Windows.Forms.NumericUpDown numericUpDownStdev;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxHistogramConfig;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.NumericUpDown numericUpDownLength;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownNumber;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private SeeSharpTools.JY.GUI.EasyChart easyChartHistogram;
        private SeeSharpTools.JY.GUI.EasyChart easyChartSpec;
        private System.Windows.Forms.Label label9;
    }
}

