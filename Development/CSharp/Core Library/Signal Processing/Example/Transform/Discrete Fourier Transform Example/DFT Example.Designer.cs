namespace Discrete_Fourier_Transform_Example
{
    partial class FormDFTExample
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SeeSharpTools.JY.GUI.EasyChartXSeries easyChartXSeries9 = new SeeSharpTools.JY.GUI.EasyChartXSeries();
            SeeSharpTools.JY.GUI.EasyChartXSeries easyChartXSeries10 = new SeeSharpTools.JY.GUI.EasyChartXSeries();
            SeeSharpTools.JY.GUI.EasyChartXSeries easyChartXSeries11 = new SeeSharpTools.JY.GUI.EasyChartXSeries();
            SeeSharpTools.JY.GUI.EasyChartXSeries easyChartXSeries12 = new SeeSharpTools.JY.GUI.EasyChartXSeries();
            SeeSharpTools.JY.GUI.EasyChartXSeries easyChartXSeries13 = new SeeSharpTools.JY.GUI.EasyChartXSeries();
            SeeSharpTools.JY.GUI.EasyChartXSeries easyChartXSeries14 = new SeeSharpTools.JY.GUI.EasyChartXSeries();
            SeeSharpTools.JY.GUI.EasyChartXSeries easyChartXSeries15 = new SeeSharpTools.JY.GUI.EasyChartXSeries();
            SeeSharpTools.JY.GUI.EasyChartXSeries easyChartXSeries16 = new SeeSharpTools.JY.GUI.EasyChartXSeries();
            this.easyChartXTimeWaveform = new SeeSharpTools.JY.GUI.EasyChartX();
            this.easyChartXSpectrum = new SeeSharpTools.JY.GUI.EasyChartX();
            this.buttonGo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // easyChartXTimeWaveform
            // 
            this.easyChartXTimeWaveform.AutoClear = true;
            this.easyChartXTimeWaveform.AxisX.AutoScale = true;
            this.easyChartXTimeWaveform.AxisX.AutoZoomReset = false;
            this.easyChartXTimeWaveform.AxisX.Color = System.Drawing.Color.Black;
            this.easyChartXTimeWaveform.AxisX.InitWithScaleView = false;
            this.easyChartXTimeWaveform.AxisX.IsLogarithmic = false;
            this.easyChartXTimeWaveform.AxisX.LabelAngle = 0;
            this.easyChartXTimeWaveform.AxisX.LabelEnabled = true;
            this.easyChartXTimeWaveform.AxisX.LabelFormat = null;
            this.easyChartXTimeWaveform.AxisX.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXTimeWaveform.AxisX.MajorGridCount = -1;
            this.easyChartXTimeWaveform.AxisX.MajorGridEnabled = true;
            this.easyChartXTimeWaveform.AxisX.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXTimeWaveform.AxisX.MaxGridCountPerPixel = 0.012D;
            this.easyChartXTimeWaveform.AxisX.Maximum = 1000D;
            this.easyChartXTimeWaveform.AxisX.MinGridCountPerPixel = 0.004D;
            this.easyChartXTimeWaveform.AxisX.Minimum = 0D;
            this.easyChartXTimeWaveform.AxisX.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXTimeWaveform.AxisX.MinorGridEnabled = false;
            this.easyChartXTimeWaveform.AxisX.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXTimeWaveform.AxisX.TickWidth = 1F;
            this.easyChartXTimeWaveform.AxisX.Title = "";
            this.easyChartXTimeWaveform.AxisX.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXTimeWaveform.AxisX.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXTimeWaveform.AxisX.ViewMaximum = 1000D;
            this.easyChartXTimeWaveform.AxisX.ViewMinimum = 0D;
            this.easyChartXTimeWaveform.AxisX2.AutoScale = true;
            this.easyChartXTimeWaveform.AxisX2.AutoZoomReset = false;
            this.easyChartXTimeWaveform.AxisX2.Color = System.Drawing.Color.Black;
            this.easyChartXTimeWaveform.AxisX2.InitWithScaleView = false;
            this.easyChartXTimeWaveform.AxisX2.IsLogarithmic = false;
            this.easyChartXTimeWaveform.AxisX2.LabelAngle = 0;
            this.easyChartXTimeWaveform.AxisX2.LabelEnabled = true;
            this.easyChartXTimeWaveform.AxisX2.LabelFormat = null;
            this.easyChartXTimeWaveform.AxisX2.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXTimeWaveform.AxisX2.MajorGridCount = -1;
            this.easyChartXTimeWaveform.AxisX2.MajorGridEnabled = true;
            this.easyChartXTimeWaveform.AxisX2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXTimeWaveform.AxisX2.MaxGridCountPerPixel = 0.012D;
            this.easyChartXTimeWaveform.AxisX2.Maximum = 1000D;
            this.easyChartXTimeWaveform.AxisX2.MinGridCountPerPixel = 0.004D;
            this.easyChartXTimeWaveform.AxisX2.Minimum = 0D;
            this.easyChartXTimeWaveform.AxisX2.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXTimeWaveform.AxisX2.MinorGridEnabled = false;
            this.easyChartXTimeWaveform.AxisX2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXTimeWaveform.AxisX2.TickWidth = 1F;
            this.easyChartXTimeWaveform.AxisX2.Title = "";
            this.easyChartXTimeWaveform.AxisX2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXTimeWaveform.AxisX2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXTimeWaveform.AxisX2.ViewMaximum = 1000D;
            this.easyChartXTimeWaveform.AxisX2.ViewMinimum = 0D;
            this.easyChartXTimeWaveform.AxisY.AutoScale = true;
            this.easyChartXTimeWaveform.AxisY.AutoZoomReset = false;
            this.easyChartXTimeWaveform.AxisY.Color = System.Drawing.Color.Black;
            this.easyChartXTimeWaveform.AxisY.InitWithScaleView = false;
            this.easyChartXTimeWaveform.AxisY.IsLogarithmic = false;
            this.easyChartXTimeWaveform.AxisY.LabelAngle = 0;
            this.easyChartXTimeWaveform.AxisY.LabelEnabled = true;
            this.easyChartXTimeWaveform.AxisY.LabelFormat = null;
            this.easyChartXTimeWaveform.AxisY.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXTimeWaveform.AxisY.MajorGridCount = 6;
            this.easyChartXTimeWaveform.AxisY.MajorGridEnabled = true;
            this.easyChartXTimeWaveform.AxisY.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXTimeWaveform.AxisY.MaxGridCountPerPixel = 0.012D;
            this.easyChartXTimeWaveform.AxisY.Maximum = 3.5D;
            this.easyChartXTimeWaveform.AxisY.MinGridCountPerPixel = 0.004D;
            this.easyChartXTimeWaveform.AxisY.Minimum = 0.5D;
            this.easyChartXTimeWaveform.AxisY.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXTimeWaveform.AxisY.MinorGridEnabled = false;
            this.easyChartXTimeWaveform.AxisY.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXTimeWaveform.AxisY.TickWidth = 1F;
            this.easyChartXTimeWaveform.AxisY.Title = "";
            this.easyChartXTimeWaveform.AxisY.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXTimeWaveform.AxisY.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXTimeWaveform.AxisY.ViewMaximum = 3.5D;
            this.easyChartXTimeWaveform.AxisY.ViewMinimum = 0.5D;
            this.easyChartXTimeWaveform.AxisY2.AutoScale = true;
            this.easyChartXTimeWaveform.AxisY2.AutoZoomReset = false;
            this.easyChartXTimeWaveform.AxisY2.Color = System.Drawing.Color.Black;
            this.easyChartXTimeWaveform.AxisY2.InitWithScaleView = false;
            this.easyChartXTimeWaveform.AxisY2.IsLogarithmic = false;
            this.easyChartXTimeWaveform.AxisY2.LabelAngle = 0;
            this.easyChartXTimeWaveform.AxisY2.LabelEnabled = true;
            this.easyChartXTimeWaveform.AxisY2.LabelFormat = null;
            this.easyChartXTimeWaveform.AxisY2.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXTimeWaveform.AxisY2.MajorGridCount = 6;
            this.easyChartXTimeWaveform.AxisY2.MajorGridEnabled = true;
            this.easyChartXTimeWaveform.AxisY2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXTimeWaveform.AxisY2.MaxGridCountPerPixel = 0.012D;
            this.easyChartXTimeWaveform.AxisY2.Maximum = 3.5D;
            this.easyChartXTimeWaveform.AxisY2.MinGridCountPerPixel = 0.004D;
            this.easyChartXTimeWaveform.AxisY2.Minimum = 0.5D;
            this.easyChartXTimeWaveform.AxisY2.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXTimeWaveform.AxisY2.MinorGridEnabled = false;
            this.easyChartXTimeWaveform.AxisY2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXTimeWaveform.AxisY2.TickWidth = 1F;
            this.easyChartXTimeWaveform.AxisY2.Title = "";
            this.easyChartXTimeWaveform.AxisY2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXTimeWaveform.AxisY2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXTimeWaveform.AxisY2.ViewMaximum = 3.5D;
            this.easyChartXTimeWaveform.AxisY2.ViewMinimum = 0.5D;
            this.easyChartXTimeWaveform.BackColor = System.Drawing.Color.White;
            this.easyChartXTimeWaveform.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartXTimeWaveform.Cumulitive = false;
            this.easyChartXTimeWaveform.GradientStyle = SeeSharpTools.JY.GUI.EasyChartX.ChartGradientStyle.None;
            this.easyChartXTimeWaveform.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartXTimeWaveform.LegendFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.easyChartXTimeWaveform.LegendForeColor = System.Drawing.Color.Black;
            this.easyChartXTimeWaveform.LegendVisible = true;
            easyChartXSeries9.Color = System.Drawing.Color.Red;
            easyChartXSeries9.Marker = SeeSharpTools.JY.GUI.EasyChartXSeries.MarkerType.None;
            easyChartXSeries9.Name = "Original-Re";
            easyChartXSeries9.Type = SeeSharpTools.JY.GUI.EasyChartXSeries.LineType.Point;
            easyChartXSeries9.Visible = true;
            easyChartXSeries9.Width = SeeSharpTools.JY.GUI.EasyChartXSeries.LineWidth.Thin;
            easyChartXSeries9.XPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries9.YPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries10.Color = System.Drawing.Color.Blue;
            easyChartXSeries10.Marker = SeeSharpTools.JY.GUI.EasyChartXSeries.MarkerType.None;
            easyChartXSeries10.Name = "Original-Im";
            easyChartXSeries10.Type = SeeSharpTools.JY.GUI.EasyChartXSeries.LineType.Point;
            easyChartXSeries10.Visible = true;
            easyChartXSeries10.Width = SeeSharpTools.JY.GUI.EasyChartXSeries.LineWidth.Thin;
            easyChartXSeries10.XPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries10.YPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries11.Color = System.Drawing.Color.Magenta;
            easyChartXSeries11.Marker = SeeSharpTools.JY.GUI.EasyChartXSeries.MarkerType.None;
            easyChartXSeries11.Name = "Recovered-Re";
            easyChartXSeries11.Type = SeeSharpTools.JY.GUI.EasyChartXSeries.LineType.FastLine;
            easyChartXSeries11.Visible = true;
            easyChartXSeries11.Width = SeeSharpTools.JY.GUI.EasyChartXSeries.LineWidth.Thin;
            easyChartXSeries11.XPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries11.YPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries12.Color = System.Drawing.Color.DodgerBlue;
            easyChartXSeries12.Marker = SeeSharpTools.JY.GUI.EasyChartXSeries.MarkerType.None;
            easyChartXSeries12.Name = "Recovered-Im";
            easyChartXSeries12.Type = SeeSharpTools.JY.GUI.EasyChartXSeries.LineType.FastLine;
            easyChartXSeries12.Visible = true;
            easyChartXSeries12.Width = SeeSharpTools.JY.GUI.EasyChartXSeries.LineWidth.Thin;
            easyChartXSeries12.XPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries12.YPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            this.easyChartXTimeWaveform.LineSeries.Add(easyChartXSeries9);
            this.easyChartXTimeWaveform.LineSeries.Add(easyChartXSeries10);
            this.easyChartXTimeWaveform.LineSeries.Add(easyChartXSeries11);
            this.easyChartXTimeWaveform.LineSeries.Add(easyChartXSeries12);
            this.easyChartXTimeWaveform.Location = new System.Drawing.Point(12, 21);
            this.easyChartXTimeWaveform.Miscellaneous.CheckInfinity = false;
            this.easyChartXTimeWaveform.Miscellaneous.CheckNaN = false;
            this.easyChartXTimeWaveform.Miscellaneous.CheckNegtiveOrZero = false;
            this.easyChartXTimeWaveform.Miscellaneous.DataStorage = SeeSharpTools.JY.GUI.DataStorageType.Clone;
            this.easyChartXTimeWaveform.Miscellaneous.DirectionChartCount = 3;
            this.easyChartXTimeWaveform.Miscellaneous.Fitting = SeeSharpTools.JY.GUI.EasyChartX.FitType.Range;
            this.easyChartXTimeWaveform.Miscellaneous.MarkerSize = 7;
            this.easyChartXTimeWaveform.Miscellaneous.MaxSeriesCount = 32;
            this.easyChartXTimeWaveform.Miscellaneous.MaxSeriesPointCount = 4000;
            this.easyChartXTimeWaveform.Miscellaneous.ShowFunctionMenu = true;
            this.easyChartXTimeWaveform.Miscellaneous.SplitLayoutColumnInterval = 0F;
            this.easyChartXTimeWaveform.Miscellaneous.SplitLayoutDirection = SeeSharpTools.JY.GUI.EasyChartXUtility.LayoutDirection.LeftToRight;
            this.easyChartXTimeWaveform.Miscellaneous.SplitLayoutRowInterval = 0F;
            this.easyChartXTimeWaveform.Miscellaneous.SplitViewAutoLayout = true;
            this.easyChartXTimeWaveform.Name = "easyChartXTimeWaveform";
            this.easyChartXTimeWaveform.SeriesCount = 0;
            this.easyChartXTimeWaveform.Size = new System.Drawing.Size(678, 213);
            this.easyChartXTimeWaveform.SplitView = false;
            this.easyChartXTimeWaveform.TabIndex = 16;
            this.easyChartXTimeWaveform.XCursor.AutoInterval = true;
            this.easyChartXTimeWaveform.XCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this.easyChartXTimeWaveform.XCursor.Interval = 0.001D;
            this.easyChartXTimeWaveform.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Zoom;
            this.easyChartXTimeWaveform.XCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.easyChartXTimeWaveform.XCursor.Value = double.NaN;
            this.easyChartXTimeWaveform.YCursor.AutoInterval = true;
            this.easyChartXTimeWaveform.YCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this.easyChartXTimeWaveform.YCursor.Interval = 0.001D;
            this.easyChartXTimeWaveform.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Disabled;
            this.easyChartXTimeWaveform.YCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.easyChartXTimeWaveform.YCursor.Value = double.NaN;
            // 
            // easyChartXSpectrum
            // 
            this.easyChartXSpectrum.AutoClear = true;
            this.easyChartXSpectrum.AxisX.AutoScale = true;
            this.easyChartXSpectrum.AxisX.AutoZoomReset = false;
            this.easyChartXSpectrum.AxisX.Color = System.Drawing.Color.Black;
            this.easyChartXSpectrum.AxisX.InitWithScaleView = false;
            this.easyChartXSpectrum.AxisX.IsLogarithmic = false;
            this.easyChartXSpectrum.AxisX.LabelAngle = 0;
            this.easyChartXSpectrum.AxisX.LabelEnabled = true;
            this.easyChartXSpectrum.AxisX.LabelFormat = null;
            this.easyChartXSpectrum.AxisX.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXSpectrum.AxisX.MajorGridCount = -1;
            this.easyChartXSpectrum.AxisX.MajorGridEnabled = true;
            this.easyChartXSpectrum.AxisX.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXSpectrum.AxisX.MaxGridCountPerPixel = 0.012D;
            this.easyChartXSpectrum.AxisX.Maximum = 1000D;
            this.easyChartXSpectrum.AxisX.MinGridCountPerPixel = 0.004D;
            this.easyChartXSpectrum.AxisX.Minimum = 0D;
            this.easyChartXSpectrum.AxisX.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXSpectrum.AxisX.MinorGridEnabled = false;
            this.easyChartXSpectrum.AxisX.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXSpectrum.AxisX.TickWidth = 1F;
            this.easyChartXSpectrum.AxisX.Title = "";
            this.easyChartXSpectrum.AxisX.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXSpectrum.AxisX.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXSpectrum.AxisX.ViewMaximum = 1000D;
            this.easyChartXSpectrum.AxisX.ViewMinimum = 0D;
            this.easyChartXSpectrum.AxisX2.AutoScale = true;
            this.easyChartXSpectrum.AxisX2.AutoZoomReset = false;
            this.easyChartXSpectrum.AxisX2.Color = System.Drawing.Color.Black;
            this.easyChartXSpectrum.AxisX2.InitWithScaleView = false;
            this.easyChartXSpectrum.AxisX2.IsLogarithmic = false;
            this.easyChartXSpectrum.AxisX2.LabelAngle = 0;
            this.easyChartXSpectrum.AxisX2.LabelEnabled = true;
            this.easyChartXSpectrum.AxisX2.LabelFormat = null;
            this.easyChartXSpectrum.AxisX2.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXSpectrum.AxisX2.MajorGridCount = -1;
            this.easyChartXSpectrum.AxisX2.MajorGridEnabled = true;
            this.easyChartXSpectrum.AxisX2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXSpectrum.AxisX2.MaxGridCountPerPixel = 0.012D;
            this.easyChartXSpectrum.AxisX2.Maximum = 1000D;
            this.easyChartXSpectrum.AxisX2.MinGridCountPerPixel = 0.004D;
            this.easyChartXSpectrum.AxisX2.Minimum = 0D;
            this.easyChartXSpectrum.AxisX2.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXSpectrum.AxisX2.MinorGridEnabled = false;
            this.easyChartXSpectrum.AxisX2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXSpectrum.AxisX2.TickWidth = 1F;
            this.easyChartXSpectrum.AxisX2.Title = "";
            this.easyChartXSpectrum.AxisX2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXSpectrum.AxisX2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXSpectrum.AxisX2.ViewMaximum = 1000D;
            this.easyChartXSpectrum.AxisX2.ViewMinimum = 0D;
            this.easyChartXSpectrum.AxisY.AutoScale = true;
            this.easyChartXSpectrum.AxisY.AutoZoomReset = false;
            this.easyChartXSpectrum.AxisY.Color = System.Drawing.Color.Black;
            this.easyChartXSpectrum.AxisY.InitWithScaleView = false;
            this.easyChartXSpectrum.AxisY.IsLogarithmic = false;
            this.easyChartXSpectrum.AxisY.LabelAngle = 0;
            this.easyChartXSpectrum.AxisY.LabelEnabled = true;
            this.easyChartXSpectrum.AxisY.LabelFormat = null;
            this.easyChartXSpectrum.AxisY.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXSpectrum.AxisY.MajorGridCount = 6;
            this.easyChartXSpectrum.AxisY.MajorGridEnabled = true;
            this.easyChartXSpectrum.AxisY.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXSpectrum.AxisY.MaxGridCountPerPixel = 0.012D;
            this.easyChartXSpectrum.AxisY.Maximum = 3.5D;
            this.easyChartXSpectrum.AxisY.MinGridCountPerPixel = 0.004D;
            this.easyChartXSpectrum.AxisY.Minimum = 0.5D;
            this.easyChartXSpectrum.AxisY.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXSpectrum.AxisY.MinorGridEnabled = false;
            this.easyChartXSpectrum.AxisY.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXSpectrum.AxisY.TickWidth = 1F;
            this.easyChartXSpectrum.AxisY.Title = "";
            this.easyChartXSpectrum.AxisY.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXSpectrum.AxisY.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXSpectrum.AxisY.ViewMaximum = 3.5D;
            this.easyChartXSpectrum.AxisY.ViewMinimum = 0.5D;
            this.easyChartXSpectrum.AxisY2.AutoScale = true;
            this.easyChartXSpectrum.AxisY2.AutoZoomReset = false;
            this.easyChartXSpectrum.AxisY2.Color = System.Drawing.Color.Black;
            this.easyChartXSpectrum.AxisY2.InitWithScaleView = false;
            this.easyChartXSpectrum.AxisY2.IsLogarithmic = false;
            this.easyChartXSpectrum.AxisY2.LabelAngle = 0;
            this.easyChartXSpectrum.AxisY2.LabelEnabled = true;
            this.easyChartXSpectrum.AxisY2.LabelFormat = null;
            this.easyChartXSpectrum.AxisY2.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXSpectrum.AxisY2.MajorGridCount = 6;
            this.easyChartXSpectrum.AxisY2.MajorGridEnabled = true;
            this.easyChartXSpectrum.AxisY2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXSpectrum.AxisY2.MaxGridCountPerPixel = 0.012D;
            this.easyChartXSpectrum.AxisY2.Maximum = 3.5D;
            this.easyChartXSpectrum.AxisY2.MinGridCountPerPixel = 0.004D;
            this.easyChartXSpectrum.AxisY2.Minimum = 0.5D;
            this.easyChartXSpectrum.AxisY2.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXSpectrum.AxisY2.MinorGridEnabled = false;
            this.easyChartXSpectrum.AxisY2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXSpectrum.AxisY2.TickWidth = 1F;
            this.easyChartXSpectrum.AxisY2.Title = "";
            this.easyChartXSpectrum.AxisY2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXSpectrum.AxisY2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXSpectrum.AxisY2.ViewMaximum = 3.5D;
            this.easyChartXSpectrum.AxisY2.ViewMinimum = 0.5D;
            this.easyChartXSpectrum.BackColor = System.Drawing.Color.White;
            this.easyChartXSpectrum.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartXSpectrum.Cumulitive = false;
            this.easyChartXSpectrum.GradientStyle = SeeSharpTools.JY.GUI.EasyChartX.ChartGradientStyle.None;
            this.easyChartXSpectrum.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartXSpectrum.LegendFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.easyChartXSpectrum.LegendForeColor = System.Drawing.Color.Black;
            this.easyChartXSpectrum.LegendVisible = true;
            easyChartXSeries13.Color = System.Drawing.Color.Red;
            easyChartXSeries13.Marker = SeeSharpTools.JY.GUI.EasyChartXSeries.MarkerType.None;
            easyChartXSeries13.Name = "Series1";
            easyChartXSeries13.Type = SeeSharpTools.JY.GUI.EasyChartXSeries.LineType.FastLine;
            easyChartXSeries13.Visible = true;
            easyChartXSeries13.Width = SeeSharpTools.JY.GUI.EasyChartXSeries.LineWidth.Thin;
            easyChartXSeries13.XPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries13.YPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries14.Color = System.Drawing.Color.Blue;
            easyChartXSeries14.Marker = SeeSharpTools.JY.GUI.EasyChartXSeries.MarkerType.None;
            easyChartXSeries14.Name = "Series2";
            easyChartXSeries14.Type = SeeSharpTools.JY.GUI.EasyChartXSeries.LineType.FastLine;
            easyChartXSeries14.Visible = false;
            easyChartXSeries14.Width = SeeSharpTools.JY.GUI.EasyChartXSeries.LineWidth.Thin;
            easyChartXSeries14.XPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries14.YPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries15.Color = System.Drawing.Color.DeepPink;
            easyChartXSeries15.Marker = SeeSharpTools.JY.GUI.EasyChartXSeries.MarkerType.None;
            easyChartXSeries15.Name = "Series3";
            easyChartXSeries15.Type = SeeSharpTools.JY.GUI.EasyChartXSeries.LineType.FastLine;
            easyChartXSeries15.Visible = false;
            easyChartXSeries15.Width = SeeSharpTools.JY.GUI.EasyChartXSeries.LineWidth.Thin;
            easyChartXSeries15.XPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries15.YPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries16.Color = System.Drawing.Color.Navy;
            easyChartXSeries16.Marker = SeeSharpTools.JY.GUI.EasyChartXSeries.MarkerType.None;
            easyChartXSeries16.Name = "Series4";
            easyChartXSeries16.Type = SeeSharpTools.JY.GUI.EasyChartXSeries.LineType.FastLine;
            easyChartXSeries16.Visible = false;
            easyChartXSeries16.Width = SeeSharpTools.JY.GUI.EasyChartXSeries.LineWidth.Thin;
            easyChartXSeries16.XPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries16.YPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            this.easyChartXSpectrum.LineSeries.Add(easyChartXSeries13);
            this.easyChartXSpectrum.LineSeries.Add(easyChartXSeries14);
            this.easyChartXSpectrum.LineSeries.Add(easyChartXSeries15);
            this.easyChartXSpectrum.LineSeries.Add(easyChartXSeries16);
            this.easyChartXSpectrum.Location = new System.Drawing.Point(12, 252);
            this.easyChartXSpectrum.Miscellaneous.CheckInfinity = false;
            this.easyChartXSpectrum.Miscellaneous.CheckNaN = false;
            this.easyChartXSpectrum.Miscellaneous.CheckNegtiveOrZero = false;
            this.easyChartXSpectrum.Miscellaneous.DataStorage = SeeSharpTools.JY.GUI.DataStorageType.Clone;
            this.easyChartXSpectrum.Miscellaneous.DirectionChartCount = 3;
            this.easyChartXSpectrum.Miscellaneous.Fitting = SeeSharpTools.JY.GUI.EasyChartX.FitType.Range;
            this.easyChartXSpectrum.Miscellaneous.MarkerSize = 7;
            this.easyChartXSpectrum.Miscellaneous.MaxSeriesCount = 32;
            this.easyChartXSpectrum.Miscellaneous.MaxSeriesPointCount = 4000;
            this.easyChartXSpectrum.Miscellaneous.ShowFunctionMenu = true;
            this.easyChartXSpectrum.Miscellaneous.SplitLayoutColumnInterval = 0F;
            this.easyChartXSpectrum.Miscellaneous.SplitLayoutDirection = SeeSharpTools.JY.GUI.EasyChartXUtility.LayoutDirection.LeftToRight;
            this.easyChartXSpectrum.Miscellaneous.SplitLayoutRowInterval = 0F;
            this.easyChartXSpectrum.Miscellaneous.SplitViewAutoLayout = true;
            this.easyChartXSpectrum.Name = "easyChartXSpectrum";
            this.easyChartXSpectrum.SeriesCount = 0;
            this.easyChartXSpectrum.Size = new System.Drawing.Size(678, 213);
            this.easyChartXSpectrum.SplitView = false;
            this.easyChartXSpectrum.TabIndex = 16;
            this.easyChartXSpectrum.XCursor.AutoInterval = true;
            this.easyChartXSpectrum.XCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this.easyChartXSpectrum.XCursor.Interval = 0.001D;
            this.easyChartXSpectrum.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Zoom;
            this.easyChartXSpectrum.XCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.easyChartXSpectrum.XCursor.Value = double.NaN;
            this.easyChartXSpectrum.YCursor.AutoInterval = true;
            this.easyChartXSpectrum.YCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this.easyChartXSpectrum.YCursor.Interval = 0.001D;
            this.easyChartXSpectrum.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Disabled;
            this.easyChartXSpectrum.YCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.easyChartXSpectrum.YCursor.Value = double.NaN;
            // 
            // buttonGo
            // 
            this.buttonGo.Location = new System.Drawing.Point(696, 21);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(75, 23);
            this.buttonGo.TabIndex = 17;
            this.buttonGo.Text = "Go";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "Time Waveform";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "Spectrum";
            // 
            // FormDFTExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 473);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonGo);
            this.Controls.Add(this.easyChartXSpectrum);
            this.Controls.Add(this.easyChartXTimeWaveform);
            this.Name = "FormDFTExample";
            this.Text = "DFT Example";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SeeSharpTools.JY.GUI.EasyChartX easyChartXTimeWaveform;
        private SeeSharpTools.JY.GUI.EasyChartX easyChartXSpectrum;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

