namespace SimpleVectorDataViewer
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label labelNumOfSamplesToDisp;
            System.Windows.Forms.Label labelIQDisplayDataType;
            System.Windows.Forms.Label labelIQDisplayUnit;
            System.Windows.Forms.Label labelSpectrumAverageMode;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            SeeSharpTools.JY.GUI.EasyChartSeries easyChartSeries1 = new SeeSharpTools.JY.GUI.EasyChartSeries();
            SeeSharpTools.JY.GUI.EasyChartSeries easyChartSeries2 = new SeeSharpTools.JY.GUI.EasyChartSeries();
            this._guiOverviewChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this._guiFilePath = new System.Windows.Forms.TextBox();
            this.labelFilePath = new System.Windows.Forms.Label();
            this._guiOpenFileButton = new System.Windows.Forms.Button();
            this._guiStatusStrip = new System.Windows.Forms.StatusStrip();
            this._guiStatusStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this._guiIQChart = new SeeSharpTools.JY.GUI.EasyChart();
            this._guiSpectrumChart = new SeeSharpTools.JY.GUI.EasyChart();
            this._guiIQDisplaySamples = new System.Windows.Forms.NumericUpDown();
            this._guiIQDisplayDataType = new System.Windows.Forms.ComboBox();
            this._guiIQDisplayUnit = new System.Windows.Forms.ComboBox();
            this._guiSpectrumAvgMode = new System.Windows.Forms.ComboBox();
            this._guiSpectrumAvgSize = new System.Windows.Forms.NumericUpDown();
            labelNumOfSamplesToDisp = new System.Windows.Forms.Label();
            labelIQDisplayDataType = new System.Windows.Forms.Label();
            labelIQDisplayUnit = new System.Windows.Forms.Label();
            labelSpectrumAverageMode = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._guiOverviewChart)).BeginInit();
            this._guiStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._guiIQDisplaySamples)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiSpectrumAvgSize)).BeginInit();
            this.SuspendLayout();
            // 
            // labelNumOfSamplesToDisp
            // 
            labelNumOfSamplesToDisp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            labelNumOfSamplesToDisp.AutoSize = true;
            labelNumOfSamplesToDisp.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelNumOfSamplesToDisp.Location = new System.Drawing.Point(12, 609);
            labelNumOfSamplesToDisp.Name = "labelNumOfSamplesToDisp";
            labelNumOfSamplesToDisp.Size = new System.Drawing.Size(143, 22);
            labelNumOfSamplesToDisp.TabIndex = 14;
            labelNumOfSamplesToDisp.Text = "Num Of Samples";
            // 
            // labelIQDisplayDataType
            // 
            labelIQDisplayDataType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            labelIQDisplayDataType.AutoSize = true;
            labelIQDisplayDataType.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelIQDisplayDataType.Location = new System.Drawing.Point(313, 609);
            labelIQDisplayDataType.Name = "labelIQDisplayDataType";
            labelIQDisplayDataType.Size = new System.Drawing.Size(92, 22);
            labelIQDisplayDataType.TabIndex = 14;
            labelIQDisplayDataType.Text = "Data Type";
            // 
            // labelIQDisplayUnit
            // 
            labelIQDisplayUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            labelIQDisplayUnit.AutoSize = true;
            labelIQDisplayUnit.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelIQDisplayUnit.Location = new System.Drawing.Point(602, 610);
            labelIQDisplayUnit.Name = "labelIQDisplayUnit";
            labelIQDisplayUnit.Size = new System.Drawing.Size(66, 22);
            labelIQDisplayUnit.TabIndex = 14;
            labelIQDisplayUnit.Text = "IQ Unit";
            // 
            // labelSpectrumAverageMode
            // 
            labelSpectrumAverageMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            labelSpectrumAverageMode.AutoSize = true;
            labelSpectrumAverageMode.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelSpectrumAverageMode.Location = new System.Drawing.Point(796, 610);
            labelSpectrumAverageMode.Name = "labelSpectrumAverageMode";
            labelSpectrumAverageMode.Size = new System.Drawing.Size(125, 22);
            labelSpectrumAverageMode.TabIndex = 14;
            labelSpectrumAverageMode.Text = "Average Mode";
            // 
            // label1
            // 
            label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(1067, 610);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(111, 22);
            label1.TabIndex = 14;
            label1.Text = "Average Size";
            // 
            // _guiOverviewChart
            // 
            this._guiOverviewChart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AxisX.IsMarginVisible = false;
            chartArea1.AxisX.LabelStyle.Format = "HH:mm:ss.fffffff";
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.SystemColors.InactiveCaptionText;
            chartArea1.AxisX.ScrollBar.BackColor = System.Drawing.SystemColors.Control;
            chartArea1.AxisX.ScrollBar.ButtonColor = System.Drawing.SystemColors.Highlight;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.SystemColors.InactiveCaptionText;
            chartArea1.AxisY.ScaleView.Zoomable = false;
            chartArea1.AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.AxisY2.IsMarksNextToAxis = false;
            chartArea1.AxisY2.MajorGrid.Enabled = false;
            chartArea1.AxisY2.MajorTickMark.Enabled = false;
            chartArea1.AxisY2.ScrollBar.Enabled = false;
            chartArea1.BackColor = System.Drawing.Color.Black;
            chartArea1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.CursorX.AutoScroll = false;
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorX.Position = 10D;
            chartArea1.CursorY.AutoScroll = false;
            chartArea1.InnerPlotPosition.Auto = false;
            chartArea1.InnerPlotPosition.Height = 75F;
            chartArea1.InnerPlotPosition.Width = 95F;
            chartArea1.InnerPlotPosition.X = 5F;
            chartArea1.InnerPlotPosition.Y = 6F;
            chartArea1.IsSameFontSizeForAllAxes = true;
            chartArea1.Name = "ChartArea";
            this._guiOverviewChart.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Power";
            this._guiOverviewChart.Legends.Add(legend1);
            this._guiOverviewChart.Location = new System.Drawing.Point(1, 67);
            this._guiOverviewChart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._guiOverviewChart.Name = "_guiOverviewChart";
            series1.ChartArea = "ChartArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Color = System.Drawing.Color.Yellow;
            series1.Legend = "Power";
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "Power vs Time";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            this._guiOverviewChart.Series.Add(series1);
            this._guiOverviewChart.Size = new System.Drawing.Size(1261, 263);
            this._guiOverviewChart.TabIndex = 7;
            this._guiOverviewChart.Text = "Overview Chart";
            this._guiOverviewChart.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.GuiOverviewChart_AxisViewChanged);
            // 
            // _guiFilePath
            // 
            this._guiFilePath.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiFilePath.Location = new System.Drawing.Point(115, 18);
            this._guiFilePath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._guiFilePath.Multiline = true;
            this._guiFilePath.Name = "_guiFilePath";
            this._guiFilePath.Size = new System.Drawing.Size(700, 36);
            this._guiFilePath.TabIndex = 8;
            // 
            // labelFilePath
            // 
            this.labelFilePath.AutoSize = true;
            this.labelFilePath.Font = new System.Drawing.Font("Cambria", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFilePath.Location = new System.Drawing.Point(8, 21);
            this.labelFilePath.Name = "labelFilePath";
            this.labelFilePath.Size = new System.Drawing.Size(103, 28);
            this.labelFilePath.TabIndex = 9;
            this.labelFilePath.Text = "File Path";
            // 
            // _guiOpenFileButton
            // 
            this._guiOpenFileButton.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiOpenFileButton.Location = new System.Drawing.Point(829, 19);
            this._guiOpenFileButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._guiOpenFileButton.Name = "_guiOpenFileButton";
            this._guiOpenFileButton.Size = new System.Drawing.Size(107, 35);
            this._guiOpenFileButton.TabIndex = 10;
            this._guiOpenFileButton.Text = "Open File";
            this._guiOpenFileButton.UseVisualStyleBackColor = true;
            this._guiOpenFileButton.Click += new System.EventHandler(this.GuiOpenFileButton_Click);
            // 
            // _guiStatusStrip
            // 
            this._guiStatusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._guiStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._guiStatusStripProgressBar});
            this._guiStatusStrip.Location = new System.Drawing.Point(0, 643);
            this._guiStatusStrip.Name = "_guiStatusStrip";
            this._guiStatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this._guiStatusStrip.Size = new System.Drawing.Size(1262, 30);
            this._guiStatusStrip.TabIndex = 11;
            this._guiStatusStrip.Text = "statusStrip1";
            // 
            // _guiStatusStripProgressBar
            // 
            this._guiStatusStripProgressBar.Name = "_guiStatusStripProgressBar";
            this._guiStatusStripProgressBar.Size = new System.Drawing.Size(160, 24);
            // 
            // _guiIQChart
            // 
            this._guiIQChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._guiIQChart.AxisX.AutoScale = true;
            this._guiIQChart.AxisX.InitWithScaleView = false;
            this._guiIQChart.AxisX.LabelEnabled = true;
            this._guiIQChart.AxisX.LabelFormat = "";
            this._guiIQChart.AxisX.Maximum = 1001D;
            this._guiIQChart.AxisX.Minimum = 0D;
            this._guiIQChart.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this._guiIQChart.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this._guiIQChart.AxisX.Title = "";
            this._guiIQChart.AxisX.ViewMaximum = 1001D;
            this._guiIQChart.AxisX.ViewMinimum = 0D;
            this._guiIQChart.AxisY.AutoScale = true;
            this._guiIQChart.AxisY.InitWithScaleView = false;
            this._guiIQChart.AxisY.LabelEnabled = true;
            this._guiIQChart.AxisY.LabelFormat = "";
            this._guiIQChart.AxisY.Maximum = 3.5D;
            this._guiIQChart.AxisY.Minimum = 0D;
            this._guiIQChart.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this._guiIQChart.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this._guiIQChart.AxisY.Title = "";
            this._guiIQChart.AxisY.ViewMaximum = 3.5D;
            this._guiIQChart.AxisY.ViewMinimum = 0D;
            this._guiIQChart.AxisYMax = 3.5D;
            this._guiIQChart.AxisYMin = 0D;
            this._guiIQChart.ChartAreaBackColor = System.Drawing.Color.Empty;
            this._guiIQChart.EasyChartBackColor = System.Drawing.Color.White;
            this._guiIQChart.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this._guiIQChart.LegendBackColor = System.Drawing.Color.Transparent;
            this._guiIQChart.LegendVisible = false;
            easyChartSeries1.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries1.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries1.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this._guiIQChart.LineSeries.Add(easyChartSeries1);
            this._guiIQChart.Location = new System.Drawing.Point(1, 334);
            this._guiIQChart.MajorGridColor = System.Drawing.Color.Black;
            this._guiIQChart.MajorGridEnabled = true;
            this._guiIQChart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._guiIQChart.MinorGridColor = System.Drawing.Color.Black;
            this._guiIQChart.MinorGridEnabled = false;
            this._guiIQChart.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this._guiIQChart.Name = "_guiIQChart";
            this._guiIQChart.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this._guiIQChart.SeriesNames = new string[] {
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
            this._guiIQChart.Size = new System.Drawing.Size(771, 262);
            this._guiIQChart.TabIndex = 12;
            this._guiIQChart.XAxisLogarithmic = false;
            this._guiIQChart.XAxisTitle = "";
            this._guiIQChart.XCursor.AutoInterval = true;
            this._guiIQChart.XCursor.Color = System.Drawing.Color.Red;
            this._guiIQChart.XCursor.Interval = 1D;
            this._guiIQChart.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this._guiIQChart.XCursor.Value = double.NaN;
            this._guiIQChart.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this._guiIQChart.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this._guiIQChart.YAutoEnable = true;
            this._guiIQChart.YAxisLogarithmic = false;
            this._guiIQChart.YAxisTitle = "";
            this._guiIQChart.YCursor.AutoInterval = true;
            this._guiIQChart.YCursor.Color = System.Drawing.Color.Red;
            this._guiIQChart.YCursor.Interval = 0.001D;
            this._guiIQChart.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this._guiIQChart.YCursor.Value = double.NaN;
            this._guiIQChart.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this._guiIQChart.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // _guiSpectrumChart
            // 
            this._guiSpectrumChart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._guiSpectrumChart.AxisX.AutoScale = true;
            this._guiSpectrumChart.AxisX.InitWithScaleView = false;
            this._guiSpectrumChart.AxisX.LabelEnabled = true;
            this._guiSpectrumChart.AxisX.LabelFormat = "";
            this._guiSpectrumChart.AxisX.Maximum = 1001D;
            this._guiSpectrumChart.AxisX.Minimum = 0D;
            this._guiSpectrumChart.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this._guiSpectrumChart.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this._guiSpectrumChart.AxisX.Title = "";
            this._guiSpectrumChart.AxisX.ViewMaximum = 1001D;
            this._guiSpectrumChart.AxisX.ViewMinimum = 0D;
            this._guiSpectrumChart.AxisY.AutoScale = true;
            this._guiSpectrumChart.AxisY.InitWithScaleView = false;
            this._guiSpectrumChart.AxisY.LabelEnabled = true;
            this._guiSpectrumChart.AxisY.LabelFormat = "";
            this._guiSpectrumChart.AxisY.Maximum = 3.5D;
            this._guiSpectrumChart.AxisY.Minimum = 0D;
            this._guiSpectrumChart.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this._guiSpectrumChart.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this._guiSpectrumChart.AxisY.Title = "";
            this._guiSpectrumChart.AxisY.ViewMaximum = 3.5D;
            this._guiSpectrumChart.AxisY.ViewMinimum = 0D;
            this._guiSpectrumChart.AxisYMax = 3.5D;
            this._guiSpectrumChart.AxisYMin = 0D;
            this._guiSpectrumChart.ChartAreaBackColor = System.Drawing.Color.Empty;
            this._guiSpectrumChart.EasyChartBackColor = System.Drawing.Color.White;
            this._guiSpectrumChart.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this._guiSpectrumChart.LegendBackColor = System.Drawing.Color.Transparent;
            this._guiSpectrumChart.LegendVisible = false;
            easyChartSeries2.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries2.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries2.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this._guiSpectrumChart.LineSeries.Add(easyChartSeries2);
            this._guiSpectrumChart.Location = new System.Drawing.Point(800, 334);
            this._guiSpectrumChart.MajorGridColor = System.Drawing.Color.Black;
            this._guiSpectrumChart.MajorGridEnabled = true;
            this._guiSpectrumChart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._guiSpectrumChart.MinorGridColor = System.Drawing.Color.Black;
            this._guiSpectrumChart.MinorGridEnabled = false;
            this._guiSpectrumChart.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this._guiSpectrumChart.Name = "_guiSpectrumChart";
            this._guiSpectrumChart.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this._guiSpectrumChart.SeriesNames = new string[] {
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
            this._guiSpectrumChart.Size = new System.Drawing.Size(462, 262);
            this._guiSpectrumChart.TabIndex = 12;
            this._guiSpectrumChart.XAxisLogarithmic = false;
            this._guiSpectrumChart.XAxisTitle = "";
            this._guiSpectrumChart.XCursor.AutoInterval = true;
            this._guiSpectrumChart.XCursor.Color = System.Drawing.Color.Red;
            this._guiSpectrumChart.XCursor.Interval = 1D;
            this._guiSpectrumChart.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this._guiSpectrumChart.XCursor.Value = double.NaN;
            this._guiSpectrumChart.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this._guiSpectrumChart.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this._guiSpectrumChart.YAutoEnable = true;
            this._guiSpectrumChart.YAxisLogarithmic = false;
            this._guiSpectrumChart.YAxisTitle = "";
            this._guiSpectrumChart.YCursor.AutoInterval = true;
            this._guiSpectrumChart.YCursor.Color = System.Drawing.Color.Red;
            this._guiSpectrumChart.YCursor.Interval = 0.001D;
            this._guiSpectrumChart.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this._guiSpectrumChart.YCursor.Value = double.NaN;
            this._guiSpectrumChart.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this._guiSpectrumChart.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // _guiIQDisplaySamples
            // 
            this._guiIQDisplaySamples.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._guiIQDisplaySamples.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiIQDisplaySamples.Increment = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this._guiIQDisplaySamples.Location = new System.Drawing.Point(161, 607);
            this._guiIQDisplaySamples.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this._guiIQDisplaySamples.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._guiIQDisplaySamples.Name = "_guiIQDisplaySamples";
            this._guiIQDisplaySamples.Size = new System.Drawing.Size(120, 29);
            this._guiIQDisplaySamples.TabIndex = 13;
            this._guiIQDisplaySamples.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._guiIQDisplaySamples.ThousandsSeparator = true;
            this._guiIQDisplaySamples.Value = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this._guiIQDisplaySamples.ValueChanged += new System.EventHandler(this.IQAndSpectrumSettings_ValueChanged);
            // 
            // _guiIQDisplayDataType
            // 
            this._guiIQDisplayDataType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._guiIQDisplayDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._guiIQDisplayDataType.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiIQDisplayDataType.FormattingEnabled = true;
            this._guiIQDisplayDataType.Location = new System.Drawing.Point(411, 606);
            this._guiIQDisplayDataType.Name = "_guiIQDisplayDataType";
            this._guiIQDisplayDataType.Size = new System.Drawing.Size(169, 29);
            this._guiIQDisplayDataType.TabIndex = 15;
            this._guiIQDisplayDataType.SelectedIndexChanged += new System.EventHandler(this.IQAndSpectrumSettings_ValueChanged);
            // 
            // _guiIQDisplayUnit
            // 
            this._guiIQDisplayUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._guiIQDisplayUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._guiIQDisplayUnit.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiIQDisplayUnit.FormattingEnabled = true;
            this._guiIQDisplayUnit.Location = new System.Drawing.Point(674, 606);
            this._guiIQDisplayUnit.Name = "_guiIQDisplayUnit";
            this._guiIQDisplayUnit.Size = new System.Drawing.Size(98, 29);
            this._guiIQDisplayUnit.TabIndex = 15;
            this._guiIQDisplayUnit.SelectedIndexChanged += new System.EventHandler(this.IQAndSpectrumSettings_ValueChanged);
            // 
            // _guiSpectrumAvgMode
            // 
            this._guiSpectrumAvgMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._guiSpectrumAvgMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._guiSpectrumAvgMode.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiSpectrumAvgMode.FormattingEnabled = true;
            this._guiSpectrumAvgMode.Location = new System.Drawing.Point(918, 607);
            this._guiSpectrumAvgMode.Name = "_guiSpectrumAvgMode";
            this._guiSpectrumAvgMode.Size = new System.Drawing.Size(130, 29);
            this._guiSpectrumAvgMode.TabIndex = 15;
            this._guiSpectrumAvgMode.SelectedIndexChanged += new System.EventHandler(this.IQAndSpectrumSettings_ValueChanged);
            // 
            // _guiSpectrumAvgSize
            // 
            this._guiSpectrumAvgSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._guiSpectrumAvgSize.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiSpectrumAvgSize.Location = new System.Drawing.Point(1184, 607);
            this._guiSpectrumAvgSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._guiSpectrumAvgSize.Name = "_guiSpectrumAvgSize";
            this._guiSpectrumAvgSize.Size = new System.Drawing.Size(66, 29);
            this._guiSpectrumAvgSize.TabIndex = 13;
            this._guiSpectrumAvgSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._guiSpectrumAvgSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._guiSpectrumAvgSize.ValueChanged += new System.EventHandler(this.IQAndSpectrumSettings_ValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this._guiIQDisplayUnit);
            this.Controls.Add(this._guiSpectrumAvgMode);
            this.Controls.Add(this._guiIQDisplayDataType);
            this.Controls.Add(labelIQDisplayUnit);
            this.Controls.Add(label1);
            this.Controls.Add(labelSpectrumAverageMode);
            this.Controls.Add(labelIQDisplayDataType);
            this.Controls.Add(labelNumOfSamplesToDisp);
            this.Controls.Add(this._guiSpectrumAvgSize);
            this.Controls.Add(this._guiIQDisplaySamples);
            this.Controls.Add(this._guiSpectrumChart);
            this.Controls.Add(this._guiIQChart);
            this.Controls.Add(this._guiOverviewChart);
            this.Controls.Add(this._guiStatusStrip);
            this.Controls.Add(this._guiOpenFileButton);
            this.Controls.Add(this.labelFilePath);
            this.Controls.Add(this._guiFilePath);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "Simple Vector Data Viewer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._guiOverviewChart)).EndInit();
            this._guiStatusStrip.ResumeLayout(false);
            this._guiStatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._guiIQDisplaySamples)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiSpectrumAvgSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart _guiOverviewChart;
        private System.Windows.Forms.TextBox _guiFilePath;
        private System.Windows.Forms.Label labelFilePath;
        private System.Windows.Forms.Button _guiOpenFileButton;
        private System.Windows.Forms.StatusStrip _guiStatusStrip;
        private System.Windows.Forms.ToolStripProgressBar _guiStatusStripProgressBar;
        private SeeSharpTools.JY.GUI.EasyChart _guiIQChart;
        private SeeSharpTools.JY.GUI.EasyChart _guiSpectrumChart;
        private System.Windows.Forms.NumericUpDown _guiIQDisplaySamples;
        private System.Windows.Forms.ComboBox _guiIQDisplayDataType;
        private System.Windows.Forms.ComboBox _guiIQDisplayUnit;
        private System.Windows.Forms.ComboBox _guiSpectrumAvgMode;
        private System.Windows.Forms.NumericUpDown _guiSpectrumAvgSize;
    }
}

