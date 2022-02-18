namespace SeeSharpTools.JXI.RFCommunications.AnalogModem_Example
{
    partial class FormAnalogModem
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
            SeeSharpTools.JY.GUI.EasyChartXSeries easyChartXSeries1 = new SeeSharpTools.JY.GUI.EasyChartXSeries();
            SeeSharpTools.JY.GUI.EasyChartXSeries easyChartXSeries2 = new SeeSharpTools.JY.GUI.EasyChartXSeries();
            SeeSharpTools.JY.GUI.EasyChartXSeries easyChartXSeries3 = new SeeSharpTools.JY.GUI.EasyChartXSeries();
            this.easyChartXIQ = new SeeSharpTools.JY.GUI.EasyChartX();
            this.easyChartXMessage = new SeeSharpTools.JY.GUI.EasyChartX();
            this.listBoxModulationType = new System.Windows.Forms.ListBox();
            this.listBoxMessageWaveformType = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonGo = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // easyChartXIQ
            // 
            this.easyChartXIQ.AutoClear = true;
            this.easyChartXIQ.AxisX.AutoScale = true;
            this.easyChartXIQ.AxisX.AutoZoomReset = false;
            this.easyChartXIQ.AxisX.Color = System.Drawing.Color.Black;
            this.easyChartXIQ.AxisX.InitWithScaleView = false;
            this.easyChartXIQ.AxisX.IsLogarithmic = false;
            this.easyChartXIQ.AxisX.LabelAngle = 0;
            this.easyChartXIQ.AxisX.LabelEnabled = true;
            this.easyChartXIQ.AxisX.LabelFormat = null;
            this.easyChartXIQ.AxisX.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXIQ.AxisX.MajorGridCount = -1;
            this.easyChartXIQ.AxisX.MajorGridEnabled = true;
            this.easyChartXIQ.AxisX.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXIQ.AxisX.MaxGridCountPerPixel = 0.012D;
            this.easyChartXIQ.AxisX.Maximum = 1000D;
            this.easyChartXIQ.AxisX.MinGridCountPerPixel = 0.004D;
            this.easyChartXIQ.AxisX.Minimum = 0D;
            this.easyChartXIQ.AxisX.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXIQ.AxisX.MinorGridEnabled = false;
            this.easyChartXIQ.AxisX.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXIQ.AxisX.TickWidth = 1F;
            this.easyChartXIQ.AxisX.Title = "";
            this.easyChartXIQ.AxisX.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXIQ.AxisX.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXIQ.AxisX.ViewMaximum = 1000D;
            this.easyChartXIQ.AxisX.ViewMinimum = 0D;
            this.easyChartXIQ.AxisX2.AutoScale = true;
            this.easyChartXIQ.AxisX2.AutoZoomReset = false;
            this.easyChartXIQ.AxisX2.Color = System.Drawing.Color.Black;
            this.easyChartXIQ.AxisX2.InitWithScaleView = false;
            this.easyChartXIQ.AxisX2.IsLogarithmic = false;
            this.easyChartXIQ.AxisX2.LabelAngle = 0;
            this.easyChartXIQ.AxisX2.LabelEnabled = true;
            this.easyChartXIQ.AxisX2.LabelFormat = null;
            this.easyChartXIQ.AxisX2.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXIQ.AxisX2.MajorGridCount = -1;
            this.easyChartXIQ.AxisX2.MajorGridEnabled = true;
            this.easyChartXIQ.AxisX2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXIQ.AxisX2.MaxGridCountPerPixel = 0.012D;
            this.easyChartXIQ.AxisX2.Maximum = 1000D;
            this.easyChartXIQ.AxisX2.MinGridCountPerPixel = 0.004D;
            this.easyChartXIQ.AxisX2.Minimum = 0D;
            this.easyChartXIQ.AxisX2.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXIQ.AxisX2.MinorGridEnabled = false;
            this.easyChartXIQ.AxisX2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXIQ.AxisX2.TickWidth = 1F;
            this.easyChartXIQ.AxisX2.Title = "";
            this.easyChartXIQ.AxisX2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXIQ.AxisX2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXIQ.AxisX2.ViewMaximum = 1000D;
            this.easyChartXIQ.AxisX2.ViewMinimum = 0D;
            this.easyChartXIQ.AxisY.AutoScale = true;
            this.easyChartXIQ.AxisY.AutoZoomReset = false;
            this.easyChartXIQ.AxisY.Color = System.Drawing.Color.Black;
            this.easyChartXIQ.AxisY.InitWithScaleView = false;
            this.easyChartXIQ.AxisY.IsLogarithmic = false;
            this.easyChartXIQ.AxisY.LabelAngle = 0;
            this.easyChartXIQ.AxisY.LabelEnabled = true;
            this.easyChartXIQ.AxisY.LabelFormat = null;
            this.easyChartXIQ.AxisY.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXIQ.AxisY.MajorGridCount = 6;
            this.easyChartXIQ.AxisY.MajorGridEnabled = true;
            this.easyChartXIQ.AxisY.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXIQ.AxisY.MaxGridCountPerPixel = 0.012D;
            this.easyChartXIQ.AxisY.Maximum = 3.5D;
            this.easyChartXIQ.AxisY.MinGridCountPerPixel = 0.004D;
            this.easyChartXIQ.AxisY.Minimum = 0.5D;
            this.easyChartXIQ.AxisY.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXIQ.AxisY.MinorGridEnabled = false;
            this.easyChartXIQ.AxisY.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXIQ.AxisY.TickWidth = 1F;
            this.easyChartXIQ.AxisY.Title = "";
            this.easyChartXIQ.AxisY.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXIQ.AxisY.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXIQ.AxisY.ViewMaximum = 3.5D;
            this.easyChartXIQ.AxisY.ViewMinimum = 0.5D;
            this.easyChartXIQ.AxisY2.AutoScale = true;
            this.easyChartXIQ.AxisY2.AutoZoomReset = false;
            this.easyChartXIQ.AxisY2.Color = System.Drawing.Color.Black;
            this.easyChartXIQ.AxisY2.InitWithScaleView = false;
            this.easyChartXIQ.AxisY2.IsLogarithmic = false;
            this.easyChartXIQ.AxisY2.LabelAngle = 0;
            this.easyChartXIQ.AxisY2.LabelEnabled = true;
            this.easyChartXIQ.AxisY2.LabelFormat = null;
            this.easyChartXIQ.AxisY2.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXIQ.AxisY2.MajorGridCount = 6;
            this.easyChartXIQ.AxisY2.MajorGridEnabled = true;
            this.easyChartXIQ.AxisY2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXIQ.AxisY2.MaxGridCountPerPixel = 0.012D;
            this.easyChartXIQ.AxisY2.Maximum = 3.5D;
            this.easyChartXIQ.AxisY2.MinGridCountPerPixel = 0.004D;
            this.easyChartXIQ.AxisY2.Minimum = 0.5D;
            this.easyChartXIQ.AxisY2.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXIQ.AxisY2.MinorGridEnabled = false;
            this.easyChartXIQ.AxisY2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXIQ.AxisY2.TickWidth = 1F;
            this.easyChartXIQ.AxisY2.Title = "";
            this.easyChartXIQ.AxisY2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXIQ.AxisY2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXIQ.AxisY2.ViewMaximum = 3.5D;
            this.easyChartXIQ.AxisY2.ViewMinimum = 0.5D;
            this.easyChartXIQ.BackColor = System.Drawing.Color.White;
            this.easyChartXIQ.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartXIQ.Cumulitive = false;
            this.easyChartXIQ.GradientStyle = SeeSharpTools.JY.GUI.EasyChartX.ChartGradientStyle.None;
            this.easyChartXIQ.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartXIQ.LegendFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.easyChartXIQ.LegendForeColor = System.Drawing.Color.Black;
            this.easyChartXIQ.LegendVisible = true;
            easyChartXSeries1.Color = System.Drawing.Color.Red;
            easyChartXSeries1.Marker = SeeSharpTools.JY.GUI.EasyChartXSeries.MarkerType.None;
            easyChartXSeries1.Name = "I";
            easyChartXSeries1.Type = SeeSharpTools.JY.GUI.EasyChartXSeries.LineType.FastLine;
            easyChartXSeries1.Visible = true;
            easyChartXSeries1.Width = SeeSharpTools.JY.GUI.EasyChartXSeries.LineWidth.Thin;
            easyChartXSeries1.XPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries1.YPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries2.Color = System.Drawing.Color.Blue;
            easyChartXSeries2.Marker = SeeSharpTools.JY.GUI.EasyChartXSeries.MarkerType.None;
            easyChartXSeries2.Name = "Q";
            easyChartXSeries2.Type = SeeSharpTools.JY.GUI.EasyChartXSeries.LineType.FastLine;
            easyChartXSeries2.Visible = false;
            easyChartXSeries2.Width = SeeSharpTools.JY.GUI.EasyChartXSeries.LineWidth.Thin;
            easyChartXSeries2.XPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries2.YPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            this.easyChartXIQ.LineSeries.Add(easyChartXSeries1);
            this.easyChartXIQ.LineSeries.Add(easyChartXSeries2);
            this.easyChartXIQ.Location = new System.Drawing.Point(12, 24);
            this.easyChartXIQ.Miscellaneous.CheckInfinity = false;
            this.easyChartXIQ.Miscellaneous.CheckNaN = false;
            this.easyChartXIQ.Miscellaneous.CheckNegtiveOrZero = false;
            this.easyChartXIQ.Miscellaneous.DataStorage = SeeSharpTools.JY.GUI.DataStorageType.Clone;
            this.easyChartXIQ.Miscellaneous.DirectionChartCount = 3;
            this.easyChartXIQ.Miscellaneous.Fitting = SeeSharpTools.JY.GUI.EasyChartX.FitType.Range;
            this.easyChartXIQ.Miscellaneous.MarkerSize = 7;
            this.easyChartXIQ.Miscellaneous.MaxSeriesCount = 32;
            this.easyChartXIQ.Miscellaneous.MaxSeriesPointCount = 4000;
            this.easyChartXIQ.Miscellaneous.ShowFunctionMenu = true;
            this.easyChartXIQ.Miscellaneous.SplitLayoutColumnInterval = 0F;
            this.easyChartXIQ.Miscellaneous.SplitLayoutDirection = SeeSharpTools.JY.GUI.EasyChartXUtility.LayoutDirection.LeftToRight;
            this.easyChartXIQ.Miscellaneous.SplitLayoutRowInterval = 0F;
            this.easyChartXIQ.Miscellaneous.SplitViewAutoLayout = true;
            this.easyChartXIQ.Name = "easyChartXIQ";
            this.easyChartXIQ.SeriesCount = 0;
            this.easyChartXIQ.Size = new System.Drawing.Size(678, 213);
            this.easyChartXIQ.SplitView = false;
            this.easyChartXIQ.TabIndex = 15;
            this.easyChartXIQ.XCursor.AutoInterval = true;
            this.easyChartXIQ.XCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this.easyChartXIQ.XCursor.Interval = 0.001D;
            this.easyChartXIQ.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Zoom;
            this.easyChartXIQ.XCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.easyChartXIQ.XCursor.Value = double.NaN;
            this.easyChartXIQ.YCursor.AutoInterval = true;
            this.easyChartXIQ.YCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this.easyChartXIQ.YCursor.Interval = 0.001D;
            this.easyChartXIQ.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Disabled;
            this.easyChartXIQ.YCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.easyChartXIQ.YCursor.Value = double.NaN;
            // 
            // easyChartXMessage
            // 
            this.easyChartXMessage.AutoClear = true;
            this.easyChartXMessage.AxisX.AutoScale = true;
            this.easyChartXMessage.AxisX.AutoZoomReset = false;
            this.easyChartXMessage.AxisX.Color = System.Drawing.Color.Black;
            this.easyChartXMessage.AxisX.InitWithScaleView = false;
            this.easyChartXMessage.AxisX.IsLogarithmic = false;
            this.easyChartXMessage.AxisX.LabelAngle = 0;
            this.easyChartXMessage.AxisX.LabelEnabled = true;
            this.easyChartXMessage.AxisX.LabelFormat = null;
            this.easyChartXMessage.AxisX.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXMessage.AxisX.MajorGridCount = -1;
            this.easyChartXMessage.AxisX.MajorGridEnabled = true;
            this.easyChartXMessage.AxisX.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXMessage.AxisX.MaxGridCountPerPixel = 0.012D;
            this.easyChartXMessage.AxisX.Maximum = 1000D;
            this.easyChartXMessage.AxisX.MinGridCountPerPixel = 0.004D;
            this.easyChartXMessage.AxisX.Minimum = 0D;
            this.easyChartXMessage.AxisX.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXMessage.AxisX.MinorGridEnabled = false;
            this.easyChartXMessage.AxisX.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXMessage.AxisX.TickWidth = 1F;
            this.easyChartXMessage.AxisX.Title = "";
            this.easyChartXMessage.AxisX.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXMessage.AxisX.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXMessage.AxisX.ViewMaximum = 1000D;
            this.easyChartXMessage.AxisX.ViewMinimum = 0D;
            this.easyChartXMessage.AxisX2.AutoScale = true;
            this.easyChartXMessage.AxisX2.AutoZoomReset = false;
            this.easyChartXMessage.AxisX2.Color = System.Drawing.Color.Black;
            this.easyChartXMessage.AxisX2.InitWithScaleView = false;
            this.easyChartXMessage.AxisX2.IsLogarithmic = false;
            this.easyChartXMessage.AxisX2.LabelAngle = 0;
            this.easyChartXMessage.AxisX2.LabelEnabled = true;
            this.easyChartXMessage.AxisX2.LabelFormat = null;
            this.easyChartXMessage.AxisX2.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXMessage.AxisX2.MajorGridCount = -1;
            this.easyChartXMessage.AxisX2.MajorGridEnabled = true;
            this.easyChartXMessage.AxisX2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXMessage.AxisX2.MaxGridCountPerPixel = 0.012D;
            this.easyChartXMessage.AxisX2.Maximum = 1000D;
            this.easyChartXMessage.AxisX2.MinGridCountPerPixel = 0.004D;
            this.easyChartXMessage.AxisX2.Minimum = 0D;
            this.easyChartXMessage.AxisX2.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXMessage.AxisX2.MinorGridEnabled = false;
            this.easyChartXMessage.AxisX2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXMessage.AxisX2.TickWidth = 1F;
            this.easyChartXMessage.AxisX2.Title = "";
            this.easyChartXMessage.AxisX2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXMessage.AxisX2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXMessage.AxisX2.ViewMaximum = 1000D;
            this.easyChartXMessage.AxisX2.ViewMinimum = 0D;
            this.easyChartXMessage.AxisY.AutoScale = true;
            this.easyChartXMessage.AxisY.AutoZoomReset = false;
            this.easyChartXMessage.AxisY.Color = System.Drawing.Color.Black;
            this.easyChartXMessage.AxisY.InitWithScaleView = false;
            this.easyChartXMessage.AxisY.IsLogarithmic = false;
            this.easyChartXMessage.AxisY.LabelAngle = 0;
            this.easyChartXMessage.AxisY.LabelEnabled = true;
            this.easyChartXMessage.AxisY.LabelFormat = null;
            this.easyChartXMessage.AxisY.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXMessage.AxisY.MajorGridCount = 6;
            this.easyChartXMessage.AxisY.MajorGridEnabled = true;
            this.easyChartXMessage.AxisY.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXMessage.AxisY.MaxGridCountPerPixel = 0.012D;
            this.easyChartXMessage.AxisY.Maximum = 3.5D;
            this.easyChartXMessage.AxisY.MinGridCountPerPixel = 0.004D;
            this.easyChartXMessage.AxisY.Minimum = 0.5D;
            this.easyChartXMessage.AxisY.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXMessage.AxisY.MinorGridEnabled = false;
            this.easyChartXMessage.AxisY.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXMessage.AxisY.TickWidth = 1F;
            this.easyChartXMessage.AxisY.Title = "";
            this.easyChartXMessage.AxisY.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXMessage.AxisY.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXMessage.AxisY.ViewMaximum = 3.5D;
            this.easyChartXMessage.AxisY.ViewMinimum = 0.5D;
            this.easyChartXMessage.AxisY2.AutoScale = true;
            this.easyChartXMessage.AxisY2.AutoZoomReset = false;
            this.easyChartXMessage.AxisY2.Color = System.Drawing.Color.Black;
            this.easyChartXMessage.AxisY2.InitWithScaleView = false;
            this.easyChartXMessage.AxisY2.IsLogarithmic = false;
            this.easyChartXMessage.AxisY2.LabelAngle = 0;
            this.easyChartXMessage.AxisY2.LabelEnabled = true;
            this.easyChartXMessage.AxisY2.LabelFormat = null;
            this.easyChartXMessage.AxisY2.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXMessage.AxisY2.MajorGridCount = 6;
            this.easyChartXMessage.AxisY2.MajorGridEnabled = true;
            this.easyChartXMessage.AxisY2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXMessage.AxisY2.MaxGridCountPerPixel = 0.012D;
            this.easyChartXMessage.AxisY2.Maximum = 3.5D;
            this.easyChartXMessage.AxisY2.MinGridCountPerPixel = 0.004D;
            this.easyChartXMessage.AxisY2.Minimum = 0.5D;
            this.easyChartXMessage.AxisY2.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXMessage.AxisY2.MinorGridEnabled = false;
            this.easyChartXMessage.AxisY2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXMessage.AxisY2.TickWidth = 1F;
            this.easyChartXMessage.AxisY2.Title = "";
            this.easyChartXMessage.AxisY2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXMessage.AxisY2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXMessage.AxisY2.ViewMaximum = 3.5D;
            this.easyChartXMessage.AxisY2.ViewMinimum = 0.5D;
            this.easyChartXMessage.BackColor = System.Drawing.Color.White;
            this.easyChartXMessage.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartXMessage.Cumulitive = false;
            this.easyChartXMessage.GradientStyle = SeeSharpTools.JY.GUI.EasyChartX.ChartGradientStyle.None;
            this.easyChartXMessage.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartXMessage.LegendFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.easyChartXMessage.LegendForeColor = System.Drawing.Color.Black;
            this.easyChartXMessage.LegendVisible = true;
            easyChartXSeries3.Color = System.Drawing.Color.Red;
            easyChartXSeries3.Marker = SeeSharpTools.JY.GUI.EasyChartXSeries.MarkerType.None;
            easyChartXSeries3.Name = "Message";
            easyChartXSeries3.Type = SeeSharpTools.JY.GUI.EasyChartXSeries.LineType.FastLine;
            easyChartXSeries3.Visible = true;
            easyChartXSeries3.Width = SeeSharpTools.JY.GUI.EasyChartXSeries.LineWidth.Thin;
            easyChartXSeries3.XPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries3.YPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            this.easyChartXMessage.LineSeries.Add(easyChartXSeries3);
            this.easyChartXMessage.Location = new System.Drawing.Point(12, 255);
            this.easyChartXMessage.Miscellaneous.CheckInfinity = false;
            this.easyChartXMessage.Miscellaneous.CheckNaN = false;
            this.easyChartXMessage.Miscellaneous.CheckNegtiveOrZero = false;
            this.easyChartXMessage.Miscellaneous.DataStorage = SeeSharpTools.JY.GUI.DataStorageType.Clone;
            this.easyChartXMessage.Miscellaneous.DirectionChartCount = 3;
            this.easyChartXMessage.Miscellaneous.Fitting = SeeSharpTools.JY.GUI.EasyChartX.FitType.Range;
            this.easyChartXMessage.Miscellaneous.MarkerSize = 7;
            this.easyChartXMessage.Miscellaneous.MaxSeriesCount = 32;
            this.easyChartXMessage.Miscellaneous.MaxSeriesPointCount = 4000;
            this.easyChartXMessage.Miscellaneous.ShowFunctionMenu = true;
            this.easyChartXMessage.Miscellaneous.SplitLayoutColumnInterval = 0F;
            this.easyChartXMessage.Miscellaneous.SplitLayoutDirection = SeeSharpTools.JY.GUI.EasyChartXUtility.LayoutDirection.LeftToRight;
            this.easyChartXMessage.Miscellaneous.SplitLayoutRowInterval = 0F;
            this.easyChartXMessage.Miscellaneous.SplitViewAutoLayout = true;
            this.easyChartXMessage.Name = "easyChartXMessage";
            this.easyChartXMessage.SeriesCount = 0;
            this.easyChartXMessage.Size = new System.Drawing.Size(678, 213);
            this.easyChartXMessage.SplitView = false;
            this.easyChartXMessage.TabIndex = 15;
            this.easyChartXMessage.XCursor.AutoInterval = true;
            this.easyChartXMessage.XCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this.easyChartXMessage.XCursor.Interval = 0.001D;
            this.easyChartXMessage.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Zoom;
            this.easyChartXMessage.XCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.easyChartXMessage.XCursor.Value = double.NaN;
            this.easyChartXMessage.YCursor.AutoInterval = true;
            this.easyChartXMessage.YCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this.easyChartXMessage.YCursor.Interval = 0.001D;
            this.easyChartXMessage.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Disabled;
            this.easyChartXMessage.YCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.easyChartXMessage.YCursor.Value = double.NaN;
            // 
            // listBoxModulationType
            // 
            this.listBoxModulationType.FormattingEnabled = true;
            this.listBoxModulationType.ItemHeight = 12;
            this.listBoxModulationType.Items.AddRange(new object[] {
            "AM",
            "FM"});
            this.listBoxModulationType.Location = new System.Drawing.Point(696, 25);
            this.listBoxModulationType.Name = "listBoxModulationType";
            this.listBoxModulationType.Size = new System.Drawing.Size(75, 28);
            this.listBoxModulationType.TabIndex = 16;
            // 
            // listBoxMessageWaveformType
            // 
            this.listBoxMessageWaveformType.FormattingEnabled = true;
            this.listBoxMessageWaveformType.ItemHeight = 12;
            this.listBoxMessageWaveformType.Items.AddRange(new object[] {
            "Sine",
            "Triangle",
            "Square-wave"});
            this.listBoxMessageWaveformType.Location = new System.Drawing.Point(696, 73);
            this.listBoxMessageWaveformType.Name = "listBoxMessageWaveformType";
            this.listBoxMessageWaveformType.Size = new System.Drawing.Size(75, 40);
            this.listBoxMessageWaveformType.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(696, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "Modulation Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(696, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "Message Type";
            // 
            // buttonGo
            // 
            this.buttonGo.Location = new System.Drawing.Point(696, 137);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(75, 23);
            this.buttonGo.TabIndex = 18;
            this.buttonGo.Text = "Go";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "Modulated IQ Waveform";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 240);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "Demodulated Waveform";
            // 
            // FormAnalogModem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 481);
            this.Controls.Add(this.buttonGo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxMessageWaveformType);
            this.Controls.Add(this.listBoxModulationType);
            this.Controls.Add(this.easyChartXMessage);
            this.Controls.Add(this.easyChartXIQ);
            this.Name = "FormAnalogModem";
            this.Text = "Analog Modem";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SeeSharpTools.JY.GUI.EasyChartX easyChartXIQ;
        private SeeSharpTools.JY.GUI.EasyChartX easyChartXMessage;
        private System.Windows.Forms.ListBox listBoxModulationType;
        private System.Windows.Forms.ListBox listBoxMessageWaveformType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

