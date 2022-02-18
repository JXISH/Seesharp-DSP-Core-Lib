namespace FIRFlter_Winform
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
            SeeSharpTools.JY.GUI.EasyChartSeries easyChartSeries3 = new SeeSharpTools.JY.GUI.EasyChartSeries();
            SeeSharpTools.JY.GUI.EasyChartSeries easyChartSeries4 = new SeeSharpTools.JY.GUI.EasyChartSeries();
            this.easyChartWaveform = new SeeSharpTools.JY.GUI.EasyChart();
            this.easyChartSpectrum = new SeeSharpTools.JY.GUI.EasyChart();
            this.easyChartWaveformFilter = new SeeSharpTools.JY.GUI.EasyChart();
            this.easyChartSpectrumFilter = new SeeSharpTools.JY.GUI.EasyChart();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxFilterType = new System.Windows.Forms.GroupBox();
            this.radioButtonBSF = new System.Windows.Forms.RadioButton();
            this.radioButtonBPF = new System.Windows.Forms.RadioButton();
            this.radioButtonHPF = new System.Windows.Forms.RadioButton();
            this.radioButtonLPF = new System.Windows.Forms.RadioButton();
            this.buttonStart = new System.Windows.Forms.Button();
            this.labelFilterNum1 = new System.Windows.Forms.Label();
            this.labelFilterNum1Value = new System.Windows.Forms.Label();
            this.groupBoxFilter = new System.Windows.Forms.GroupBox();
            this.labelFilterNum4Value = new System.Windows.Forms.Label();
            this.labelFilterNum3Value = new System.Windows.Forms.Label();
            this.labelFilterNum4 = new System.Windows.Forms.Label();
            this.labelFilterNum3 = new System.Windows.Forms.Label();
            this.labelFilterNum2 = new System.Windows.Forms.Label();
            this.labelFilterNum2Value = new System.Windows.Forms.Label();
            this.groupBoxWave = new System.Windows.Forms.GroupBox();
            this.labelSampleRate = new System.Windows.Forms.Label();
            this.labelFrequency = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelFilteredPoints = new System.Windows.Forms.Label();
            this.groupBoxFilterType.SuspendLayout();
            this.groupBoxFilter.SuspendLayout();
            this.groupBoxWave.SuspendLayout();
            this.SuspendLayout();
            // 
            // easyChartWaveform
            // 
            this.easyChartWaveform.AxisX.AutoScale = true;
            this.easyChartWaveform.AxisX.InitWithScaleView = false;
            this.easyChartWaveform.AxisX.LabelEnabled = true;
            this.easyChartWaveform.AxisX.LabelFormat = "";
            this.easyChartWaveform.AxisX.Maximum = 1001D;
            this.easyChartWaveform.AxisX.Minimum = 0D;
            this.easyChartWaveform.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartWaveform.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartWaveform.AxisX.Title = "";
            this.easyChartWaveform.AxisX.ViewMaximum = 1001D;
            this.easyChartWaveform.AxisX.ViewMinimum = 0D;
            this.easyChartWaveform.AxisY.AutoScale = true;
            this.easyChartWaveform.AxisY.InitWithScaleView = false;
            this.easyChartWaveform.AxisY.LabelEnabled = true;
            this.easyChartWaveform.AxisY.LabelFormat = "";
            this.easyChartWaveform.AxisY.Maximum = 3.5D;
            this.easyChartWaveform.AxisY.Minimum = 0D;
            this.easyChartWaveform.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartWaveform.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartWaveform.AxisY.Title = "";
            this.easyChartWaveform.AxisY.ViewMaximum = 3.5D;
            this.easyChartWaveform.AxisY.ViewMinimum = 0D;
            this.easyChartWaveform.AxisYMax = 3.5D;
            this.easyChartWaveform.AxisYMin = 0D;
            this.easyChartWaveform.BackGradientStyle = null;
            this.easyChartWaveform.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartWaveform.EasyChartBackColor = System.Drawing.Color.White;
            this.easyChartWaveform.FixAxisX = false;
            this.easyChartWaveform.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.easyChartWaveform.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartWaveform.LegendVisible = true;
            easyChartSeries1.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries1.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries1.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.easyChartWaveform.LineSeries.Add(easyChartSeries1);
            this.easyChartWaveform.Location = new System.Drawing.Point(2, 33);
            this.easyChartWaveform.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartWaveform.MajorGridEnabled = true;
            this.easyChartWaveform.Margin = new System.Windows.Forms.Padding(2);
            this.easyChartWaveform.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartWaveform.MinorGridEnabled = false;
            this.easyChartWaveform.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.easyChartWaveform.Name = "easyChartWaveform";
            this.easyChartWaveform.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.easyChartWaveform.SeriesNames = new string[] {
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
            this.easyChartWaveform.Size = new System.Drawing.Size(612, 251);
            this.easyChartWaveform.TabIndex = 0;
            this.easyChartWaveform.XAxisLogarithmic = false;
            this.easyChartWaveform.XAxisTitle = "";
            this.easyChartWaveform.XCursor.AutoInterval = true;
            this.easyChartWaveform.XCursor.Color = System.Drawing.Color.Red;
            this.easyChartWaveform.XCursor.Interval = 1D;
            this.easyChartWaveform.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.easyChartWaveform.XCursor.Value = double.NaN;
            this.easyChartWaveform.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartWaveform.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartWaveform.YAutoEnable = true;
            this.easyChartWaveform.YAxisLogarithmic = false;
            this.easyChartWaveform.YAxisTitle = "";
            this.easyChartWaveform.YCursor.AutoInterval = true;
            this.easyChartWaveform.YCursor.Color = System.Drawing.Color.Red;
            this.easyChartWaveform.YCursor.Interval = 0.001D;
            this.easyChartWaveform.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.easyChartWaveform.YCursor.Value = double.NaN;
            this.easyChartWaveform.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartWaveform.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // easyChartSpectrum
            // 
            this.easyChartSpectrum.AxisX.AutoScale = true;
            this.easyChartSpectrum.AxisX.InitWithScaleView = false;
            this.easyChartSpectrum.AxisX.LabelEnabled = true;
            this.easyChartSpectrum.AxisX.LabelFormat = "";
            this.easyChartSpectrum.AxisX.Maximum = 1001D;
            this.easyChartSpectrum.AxisX.Minimum = 0D;
            this.easyChartSpectrum.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartSpectrum.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartSpectrum.AxisX.Title = "";
            this.easyChartSpectrum.AxisX.ViewMaximum = 1001D;
            this.easyChartSpectrum.AxisX.ViewMinimum = 0D;
            this.easyChartSpectrum.AxisY.AutoScale = true;
            this.easyChartSpectrum.AxisY.InitWithScaleView = false;
            this.easyChartSpectrum.AxisY.LabelEnabled = true;
            this.easyChartSpectrum.AxisY.LabelFormat = "";
            this.easyChartSpectrum.AxisY.Maximum = 3.5D;
            this.easyChartSpectrum.AxisY.Minimum = 0D;
            this.easyChartSpectrum.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartSpectrum.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartSpectrum.AxisY.Title = "";
            this.easyChartSpectrum.AxisY.ViewMaximum = 3.5D;
            this.easyChartSpectrum.AxisY.ViewMinimum = 0D;
            this.easyChartSpectrum.AxisYMax = 3.5D;
            this.easyChartSpectrum.AxisYMin = 0D;
            this.easyChartSpectrum.BackGradientStyle = null;
            this.easyChartSpectrum.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartSpectrum.EasyChartBackColor = System.Drawing.Color.White;
            this.easyChartSpectrum.FixAxisX = false;
            this.easyChartSpectrum.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.easyChartSpectrum.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartSpectrum.LegendVisible = true;
            easyChartSeries2.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries2.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries2.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.easyChartSpectrum.LineSeries.Add(easyChartSeries2);
            this.easyChartSpectrum.Location = new System.Drawing.Point(636, 33);
            this.easyChartSpectrum.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartSpectrum.MajorGridEnabled = true;
            this.easyChartSpectrum.Margin = new System.Windows.Forms.Padding(2);
            this.easyChartSpectrum.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartSpectrum.MinorGridEnabled = false;
            this.easyChartSpectrum.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.easyChartSpectrum.Name = "easyChartSpectrum";
            this.easyChartSpectrum.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.easyChartSpectrum.SeriesNames = new string[] {
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
            this.easyChartSpectrum.Size = new System.Drawing.Size(612, 251);
            this.easyChartSpectrum.TabIndex = 1;
            this.easyChartSpectrum.XAxisLogarithmic = false;
            this.easyChartSpectrum.XAxisTitle = "";
            this.easyChartSpectrum.XCursor.AutoInterval = true;
            this.easyChartSpectrum.XCursor.Color = System.Drawing.Color.Red;
            this.easyChartSpectrum.XCursor.Interval = 1D;
            this.easyChartSpectrum.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.easyChartSpectrum.XCursor.Value = double.NaN;
            this.easyChartSpectrum.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartSpectrum.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartSpectrum.YAutoEnable = true;
            this.easyChartSpectrum.YAxisLogarithmic = false;
            this.easyChartSpectrum.YAxisTitle = "";
            this.easyChartSpectrum.YCursor.AutoInterval = true;
            this.easyChartSpectrum.YCursor.Color = System.Drawing.Color.Red;
            this.easyChartSpectrum.YCursor.Interval = 0.001D;
            this.easyChartSpectrum.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.easyChartSpectrum.YCursor.Value = double.NaN;
            this.easyChartSpectrum.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartSpectrum.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // easyChartWaveformFilter
            // 
            this.easyChartWaveformFilter.AxisX.AutoScale = true;
            this.easyChartWaveformFilter.AxisX.InitWithScaleView = false;
            this.easyChartWaveformFilter.AxisX.LabelEnabled = true;
            this.easyChartWaveformFilter.AxisX.LabelFormat = "";
            this.easyChartWaveformFilter.AxisX.Maximum = 1001D;
            this.easyChartWaveformFilter.AxisX.Minimum = 0D;
            this.easyChartWaveformFilter.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartWaveformFilter.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartWaveformFilter.AxisX.Title = "";
            this.easyChartWaveformFilter.AxisX.ViewMaximum = 1001D;
            this.easyChartWaveformFilter.AxisX.ViewMinimum = 0D;
            this.easyChartWaveformFilter.AxisY.AutoScale = true;
            this.easyChartWaveformFilter.AxisY.InitWithScaleView = false;
            this.easyChartWaveformFilter.AxisY.LabelEnabled = true;
            this.easyChartWaveformFilter.AxisY.LabelFormat = "";
            this.easyChartWaveformFilter.AxisY.Maximum = 3.5D;
            this.easyChartWaveformFilter.AxisY.Minimum = 0D;
            this.easyChartWaveformFilter.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartWaveformFilter.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartWaveformFilter.AxisY.Title = "";
            this.easyChartWaveformFilter.AxisY.ViewMaximum = 3.5D;
            this.easyChartWaveformFilter.AxisY.ViewMinimum = 0D;
            this.easyChartWaveformFilter.AxisYMax = 3.5D;
            this.easyChartWaveformFilter.AxisYMin = 0D;
            this.easyChartWaveformFilter.BackGradientStyle = null;
            this.easyChartWaveformFilter.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartWaveformFilter.EasyChartBackColor = System.Drawing.Color.White;
            this.easyChartWaveformFilter.FixAxisX = false;
            this.easyChartWaveformFilter.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.easyChartWaveformFilter.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartWaveformFilter.LegendVisible = true;
            easyChartSeries3.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries3.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries3.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.easyChartWaveformFilter.LineSeries.Add(easyChartSeries3);
            this.easyChartWaveformFilter.Location = new System.Drawing.Point(2, 306);
            this.easyChartWaveformFilter.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartWaveformFilter.MajorGridEnabled = true;
            this.easyChartWaveformFilter.Margin = new System.Windows.Forms.Padding(2);
            this.easyChartWaveformFilter.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartWaveformFilter.MinorGridEnabled = false;
            this.easyChartWaveformFilter.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.easyChartWaveformFilter.Name = "easyChartWaveformFilter";
            this.easyChartWaveformFilter.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.easyChartWaveformFilter.SeriesNames = new string[] {
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
            this.easyChartWaveformFilter.Size = new System.Drawing.Size(612, 251);
            this.easyChartWaveformFilter.TabIndex = 2;
            this.easyChartWaveformFilter.XAxisLogarithmic = false;
            this.easyChartWaveformFilter.XAxisTitle = "";
            this.easyChartWaveformFilter.XCursor.AutoInterval = true;
            this.easyChartWaveformFilter.XCursor.Color = System.Drawing.Color.Red;
            this.easyChartWaveformFilter.XCursor.Interval = 1D;
            this.easyChartWaveformFilter.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.easyChartWaveformFilter.XCursor.Value = double.NaN;
            this.easyChartWaveformFilter.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartWaveformFilter.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartWaveformFilter.YAutoEnable = true;
            this.easyChartWaveformFilter.YAxisLogarithmic = false;
            this.easyChartWaveformFilter.YAxisTitle = "";
            this.easyChartWaveformFilter.YCursor.AutoInterval = true;
            this.easyChartWaveformFilter.YCursor.Color = System.Drawing.Color.Red;
            this.easyChartWaveformFilter.YCursor.Interval = 0.001D;
            this.easyChartWaveformFilter.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.easyChartWaveformFilter.YCursor.Value = double.NaN;
            this.easyChartWaveformFilter.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartWaveformFilter.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // easyChartSpectrumFilter
            // 
            this.easyChartSpectrumFilter.AxisX.AutoScale = true;
            this.easyChartSpectrumFilter.AxisX.InitWithScaleView = false;
            this.easyChartSpectrumFilter.AxisX.LabelEnabled = true;
            this.easyChartSpectrumFilter.AxisX.LabelFormat = "";
            this.easyChartSpectrumFilter.AxisX.Maximum = 1001D;
            this.easyChartSpectrumFilter.AxisX.Minimum = 0D;
            this.easyChartSpectrumFilter.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartSpectrumFilter.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartSpectrumFilter.AxisX.Title = "";
            this.easyChartSpectrumFilter.AxisX.ViewMaximum = 1001D;
            this.easyChartSpectrumFilter.AxisX.ViewMinimum = 0D;
            this.easyChartSpectrumFilter.AxisY.AutoScale = true;
            this.easyChartSpectrumFilter.AxisY.InitWithScaleView = false;
            this.easyChartSpectrumFilter.AxisY.LabelEnabled = true;
            this.easyChartSpectrumFilter.AxisY.LabelFormat = "";
            this.easyChartSpectrumFilter.AxisY.Maximum = 3.5D;
            this.easyChartSpectrumFilter.AxisY.Minimum = 0D;
            this.easyChartSpectrumFilter.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartSpectrumFilter.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartSpectrumFilter.AxisY.Title = "";
            this.easyChartSpectrumFilter.AxisY.ViewMaximum = 3.5D;
            this.easyChartSpectrumFilter.AxisY.ViewMinimum = 0D;
            this.easyChartSpectrumFilter.AxisYMax = 3.5D;
            this.easyChartSpectrumFilter.AxisYMin = 0D;
            this.easyChartSpectrumFilter.BackGradientStyle = null;
            this.easyChartSpectrumFilter.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartSpectrumFilter.EasyChartBackColor = System.Drawing.Color.White;
            this.easyChartSpectrumFilter.FixAxisX = false;
            this.easyChartSpectrumFilter.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.easyChartSpectrumFilter.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartSpectrumFilter.LegendVisible = true;
            easyChartSeries4.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries4.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries4.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.easyChartSpectrumFilter.LineSeries.Add(easyChartSeries4);
            this.easyChartSpectrumFilter.Location = new System.Drawing.Point(636, 305);
            this.easyChartSpectrumFilter.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartSpectrumFilter.MajorGridEnabled = true;
            this.easyChartSpectrumFilter.Margin = new System.Windows.Forms.Padding(2);
            this.easyChartSpectrumFilter.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartSpectrumFilter.MinorGridEnabled = false;
            this.easyChartSpectrumFilter.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.easyChartSpectrumFilter.Name = "easyChartSpectrumFilter";
            this.easyChartSpectrumFilter.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.easyChartSpectrumFilter.SeriesNames = new string[] {
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
            this.easyChartSpectrumFilter.Size = new System.Drawing.Size(612, 251);
            this.easyChartSpectrumFilter.TabIndex = 3;
            this.easyChartSpectrumFilter.XAxisLogarithmic = false;
            this.easyChartSpectrumFilter.XAxisTitle = "";
            this.easyChartSpectrumFilter.XCursor.AutoInterval = true;
            this.easyChartSpectrumFilter.XCursor.Color = System.Drawing.Color.Red;
            this.easyChartSpectrumFilter.XCursor.Interval = 1D;
            this.easyChartSpectrumFilter.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.easyChartSpectrumFilter.XCursor.Value = double.NaN;
            this.easyChartSpectrumFilter.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartSpectrumFilter.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartSpectrumFilter.YAutoEnable = true;
            this.easyChartSpectrumFilter.YAxisLogarithmic = false;
            this.easyChartSpectrumFilter.YAxisTitle = "";
            this.easyChartSpectrumFilter.YCursor.AutoInterval = true;
            this.easyChartSpectrumFilter.YCursor.Color = System.Drawing.Color.Red;
            this.easyChartSpectrumFilter.YCursor.Interval = 0.001D;
            this.easyChartSpectrumFilter.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.easyChartSpectrumFilter.YCursor.Value = double.NaN;
            this.easyChartSpectrumFilter.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartSpectrumFilter.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(304, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Time domain waveforms and spectra before filtering:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 290);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(261, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Filtered time domain waveforms and spectra:";
            // 
            // groupBoxFilterType
            // 
            this.groupBoxFilterType.Controls.Add(this.radioButtonBSF);
            this.groupBoxFilterType.Controls.Add(this.radioButtonBPF);
            this.groupBoxFilterType.Controls.Add(this.radioButtonHPF);
            this.groupBoxFilterType.Controls.Add(this.radioButtonLPF);
            this.groupBoxFilterType.Location = new System.Drawing.Point(1262, 56);
            this.groupBoxFilterType.Name = "groupBoxFilterType";
            this.groupBoxFilterType.Size = new System.Drawing.Size(156, 172);
            this.groupBoxFilterType.TabIndex = 6;
            this.groupBoxFilterType.TabStop = false;
            this.groupBoxFilterType.Text = "Filter type";
            // 
            // radioButtonBSF
            // 
            this.radioButtonBSF.AutoSize = true;
            this.radioButtonBSF.Location = new System.Drawing.Point(21, 139);
            this.radioButtonBSF.Name = "radioButtonBSF";
            this.radioButtonBSF.Size = new System.Drawing.Size(103, 16);
            this.radioButtonBSF.TabIndex = 3;
            this.radioButtonBSF.TabStop = true;
            this.radioButtonBSF.Text = "Bandstop filter";
            this.radioButtonBSF.UseVisualStyleBackColor = true;
            this.radioButtonBSF.CheckedChanged += new System.EventHandler(this.radioButtonBSF_CheckedChanged);
            // 
            // radioButtonBPF
            // 
            this.radioButtonBPF.AutoSize = true;
            this.radioButtonBPF.Location = new System.Drawing.Point(21, 103);
            this.radioButtonBPF.Name = "radioButtonBPF";
            this.radioButtonBPF.Size = new System.Drawing.Size(107, 16);
            this.radioButtonBPF.TabIndex = 2;
            this.radioButtonBPF.TabStop = true;
            this.radioButtonBPF.Text = "Bandpass filter";
            this.radioButtonBPF.UseVisualStyleBackColor = true;
            this.radioButtonBPF.CheckedChanged += new System.EventHandler(this.radioButtonBPF_CheckedChanged);
            // 
            // radioButtonHPF
            // 
            this.radioButtonHPF.AutoSize = true;
            this.radioButtonHPF.Location = new System.Drawing.Point(21, 67);
            this.radioButtonHPF.Name = "radioButtonHPF";
            this.radioButtonHPF.Size = new System.Drawing.Size(116, 16);
            this.radioButtonHPF.TabIndex = 1;
            this.radioButtonHPF.TabStop = true;
            this.radioButtonHPF.Text = "Qualcomm Filter";
            this.radioButtonHPF.UseVisualStyleBackColor = true;
            this.radioButtonHPF.CheckedChanged += new System.EventHandler(this.radioButtonHPF_CheckedChanged);
            // 
            // radioButtonLPF
            // 
            this.radioButtonLPF.AutoSize = true;
            this.radioButtonLPF.Location = new System.Drawing.Point(21, 31);
            this.radioButtonLPF.Name = "radioButtonLPF";
            this.radioButtonLPF.Size = new System.Drawing.Size(106, 16);
            this.radioButtonLPF.TabIndex = 0;
            this.radioButtonLPF.TabStop = true;
            this.radioButtonLPF.Text = "Low pass filter";
            this.radioButtonLPF.UseVisualStyleBackColor = true;
            this.radioButtonLPF.CheckedChanged += new System.EventHandler(this.radioButtonLPF_CheckedChanged);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(1273, 509);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(134, 37);
            this.buttonStart.TabIndex = 7;
            this.buttonStart.Text = "Filter";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // labelFilterNum1
            // 
            this.labelFilterNum1.AutoSize = true;
            this.labelFilterNum1.Location = new System.Drawing.Point(17, 37);
            this.labelFilterNum1.Name = "labelFilterNum1";
            this.labelFilterNum1.Size = new System.Drawing.Size(48, 12);
            this.labelFilterNum1.TabIndex = 8;
            this.labelFilterNum1.Text = "fpass：";
            // 
            // labelFilterNum1Value
            // 
            this.labelFilterNum1Value.AutoSize = true;
            this.labelFilterNum1Value.Location = new System.Drawing.Point(98, 37);
            this.labelFilterNum1Value.Name = "labelFilterNum1Value";
            this.labelFilterNum1Value.Size = new System.Drawing.Size(21, 12);
            this.labelFilterNum1Value.TabIndex = 9;
            this.labelFilterNum1Value.Text = "0.2";
            // 
            // groupBoxFilter
            // 
            this.groupBoxFilter.Controls.Add(this.labelFilterNum4Value);
            this.groupBoxFilter.Controls.Add(this.labelFilterNum3Value);
            this.groupBoxFilter.Controls.Add(this.labelFilterNum4);
            this.groupBoxFilter.Controls.Add(this.labelFilterNum3);
            this.groupBoxFilter.Controls.Add(this.labelFilterNum2);
            this.groupBoxFilter.Controls.Add(this.labelFilterNum2Value);
            this.groupBoxFilter.Controls.Add(this.labelFilterNum1);
            this.groupBoxFilter.Controls.Add(this.labelFilterNum1Value);
            this.groupBoxFilter.Location = new System.Drawing.Point(1262, 363);
            this.groupBoxFilter.Name = "groupBoxFilter";
            this.groupBoxFilter.Size = new System.Drawing.Size(156, 128);
            this.groupBoxFilter.TabIndex = 10;
            this.groupBoxFilter.TabStop = false;
            this.groupBoxFilter.Text = "Filter parameter (normalized)";
            // 
            // labelFilterNum4Value
            // 
            this.labelFilterNum4Value.AutoSize = true;
            this.labelFilterNum4Value.Location = new System.Drawing.Point(98, 103);
            this.labelFilterNum4Value.Name = "labelFilterNum4Value";
            this.labelFilterNum4Value.Size = new System.Drawing.Size(21, 12);
            this.labelFilterNum4Value.TabIndex = 15;
            this.labelFilterNum4Value.Text = "0.4";
            // 
            // labelFilterNum3Value
            // 
            this.labelFilterNum3Value.AutoSize = true;
            this.labelFilterNum3Value.Location = new System.Drawing.Point(98, 81);
            this.labelFilterNum3Value.Name = "labelFilterNum3Value";
            this.labelFilterNum3Value.Size = new System.Drawing.Size(21, 12);
            this.labelFilterNum3Value.TabIndex = 14;
            this.labelFilterNum3Value.Text = "0.4";
            // 
            // labelFilterNum4
            // 
            this.labelFilterNum4.AutoSize = true;
            this.labelFilterNum4.Location = new System.Drawing.Point(17, 103);
            this.labelFilterNum4.Name = "labelFilterNum4";
            this.labelFilterNum4.Size = new System.Drawing.Size(48, 12);
            this.labelFilterNum4.TabIndex = 13;
            this.labelFilterNum4.Text = "fpass：";
            // 
            // labelFilterNum3
            // 
            this.labelFilterNum3.AutoSize = true;
            this.labelFilterNum3.Location = new System.Drawing.Point(17, 81);
            this.labelFilterNum3.Name = "labelFilterNum3";
            this.labelFilterNum3.Size = new System.Drawing.Size(48, 12);
            this.labelFilterNum3.TabIndex = 12;
            this.labelFilterNum3.Text = "fpass：";
            // 
            // labelFilterNum2
            // 
            this.labelFilterNum2.AutoSize = true;
            this.labelFilterNum2.Location = new System.Drawing.Point(17, 59);
            this.labelFilterNum2.Name = "labelFilterNum2";
            this.labelFilterNum2.Size = new System.Drawing.Size(44, 12);
            this.labelFilterNum2.TabIndex = 10;
            this.labelFilterNum2.Text = "fstop：";
            // 
            // labelFilterNum2Value
            // 
            this.labelFilterNum2Value.AutoSize = true;
            this.labelFilterNum2Value.Location = new System.Drawing.Point(98, 59);
            this.labelFilterNum2Value.Name = "labelFilterNum2Value";
            this.labelFilterNum2Value.Size = new System.Drawing.Size(21, 12);
            this.labelFilterNum2Value.TabIndex = 11;
            this.labelFilterNum2Value.Text = "0.3";
            // 
            // groupBoxWave
            // 
            this.groupBoxWave.Controls.Add(this.labelSampleRate);
            this.groupBoxWave.Controls.Add(this.labelFrequency);
            this.groupBoxWave.Controls.Add(this.label7);
            this.groupBoxWave.Controls.Add(this.label6);
            this.groupBoxWave.Controls.Add(this.label4);
            this.groupBoxWave.Controls.Add(this.labelFilteredPoints);
            this.groupBoxWave.Location = new System.Drawing.Point(1262, 244);
            this.groupBoxWave.Name = "groupBoxWave";
            this.groupBoxWave.Size = new System.Drawing.Size(156, 100);
            this.groupBoxWave.TabIndex = 11;
            this.groupBoxWave.TabStop = false;
            this.groupBoxWave.Text = "Waveform parameter";
            // 
            // labelSampleRate
            // 
            this.labelSampleRate.AutoSize = true;
            this.labelSampleRate.Location = new System.Drawing.Point(108, 75);
            this.labelSampleRate.Name = "labelSampleRate";
            this.labelSampleRate.Size = new System.Drawing.Size(23, 12);
            this.labelSampleRate.TabIndex = 13;
            this.labelSampleRate.Text = "100";
            // 
            // labelFrequency
            // 
            this.labelFrequency.AutoSize = true;
            this.labelFrequency.Location = new System.Drawing.Point(108, 49);
            this.labelFrequency.Name = "labelFrequency";
            this.labelFrequency.Size = new System.Drawing.Size(37, 12);
            this.labelFrequency.TabIndex = 12;
            this.labelFrequency.Text = "10, 40";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "Sampling rate:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "Frequency:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "Filter points:";
            // 
            // labelFilteredPoints
            // 
            this.labelFilteredPoints.AutoSize = true;
            this.labelFilteredPoints.Location = new System.Drawing.Point(108, 23);
            this.labelFilteredPoints.Name = "labelFilteredPoints";
            this.labelFilteredPoints.Size = new System.Drawing.Size(11, 12);
            this.labelFilteredPoints.TabIndex = 9;
            this.labelFilteredPoints.Text = "0";
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1430, 568);
            this.Controls.Add(this.groupBoxWave);
            this.Controls.Add(this.groupBoxFilter);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.groupBoxFilterType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.easyChartSpectrumFilter);
            this.Controls.Add(this.easyChartWaveformFilter);
            this.Controls.Add(this.easyChartSpectrum);
            this.Controls.Add(this.easyChartWaveform);
            this.Name = "Mainform";
            this.Text = "FIRFilter";
            this.Load += new System.EventHandler(this.Mainform_Load);
            this.groupBoxFilterType.ResumeLayout(false);
            this.groupBoxFilterType.PerformLayout();
            this.groupBoxFilter.ResumeLayout(false);
            this.groupBoxFilter.PerformLayout();
            this.groupBoxWave.ResumeLayout(false);
            this.groupBoxWave.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SeeSharpTools.JY.GUI.EasyChart easyChartWaveform;
        private SeeSharpTools.JY.GUI.EasyChart easyChartSpectrum;
        private SeeSharpTools.JY.GUI.EasyChart easyChartWaveformFilter;
        private SeeSharpTools.JY.GUI.EasyChart easyChartSpectrumFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxFilterType;
        private System.Windows.Forms.RadioButton radioButtonBSF;
        private System.Windows.Forms.RadioButton radioButtonBPF;
        private System.Windows.Forms.RadioButton radioButtonHPF;
        private System.Windows.Forms.RadioButton radioButtonLPF;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label labelFilterNum1;
        private System.Windows.Forms.Label labelFilterNum1Value;
        private System.Windows.Forms.GroupBox groupBoxFilter;
        private System.Windows.Forms.GroupBox groupBoxWave;
        private System.Windows.Forms.Label labelSampleRate;
        private System.Windows.Forms.Label labelFrequency;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelFilteredPoints;
        private System.Windows.Forms.Label labelFilterNum2;
        private System.Windows.Forms.Label labelFilterNum2Value;
        private System.Windows.Forms.Label labelFilterNum4Value;
        private System.Windows.Forms.Label labelFilterNum3Value;
        private System.Windows.Forms.Label labelFilterNum4;
        private System.Windows.Forms.Label labelFilterNum3;
    }
}

