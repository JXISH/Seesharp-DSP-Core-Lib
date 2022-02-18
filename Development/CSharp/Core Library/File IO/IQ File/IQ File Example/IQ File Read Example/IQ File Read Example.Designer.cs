namespace IQ_File_Read_Example
{
    partial class IQFileReadExample
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
            this.components = new System.ComponentModel.Container();
            SeeSharpTools.JY.GUI.EasyChartXSeries easyChartXSeries1 = new SeeSharpTools.JY.GUI.EasyChartXSeries();
            SeeSharpTools.JY.GUI.EasyChartXSeries easyChartXSeries2 = new SeeSharpTools.JY.GUI.EasyChartXSeries();
            this.easyChartXIQPlot = new SeeSharpTools.JY.GUI.EasyChartX();
            this.buttonRead = new System.Windows.Forms.Button();
            this.textBoxFileInfo = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // easyChartXIQPlot
            // 
            this.easyChartXIQPlot.AutoClear = true;
            this.easyChartXIQPlot.AxisX.AutoScale = true;
            this.easyChartXIQPlot.AxisX.AutoZoomReset = false;
            this.easyChartXIQPlot.AxisX.Color = System.Drawing.Color.Black;
            this.easyChartXIQPlot.AxisX.InitWithScaleView = false;
            this.easyChartXIQPlot.AxisX.IsLogarithmic = false;
            this.easyChartXIQPlot.AxisX.LabelAngle = 0;
            this.easyChartXIQPlot.AxisX.LabelEnabled = true;
            this.easyChartXIQPlot.AxisX.LabelFormat = null;
            this.easyChartXIQPlot.AxisX.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXIQPlot.AxisX.MajorGridCount = -1;
            this.easyChartXIQPlot.AxisX.MajorGridEnabled = true;
            this.easyChartXIQPlot.AxisX.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXIQPlot.AxisX.MaxGridCountPerPixel = 0.012D;
            this.easyChartXIQPlot.AxisX.Maximum = 1000D;
            this.easyChartXIQPlot.AxisX.MinGridCountPerPixel = 0.004D;
            this.easyChartXIQPlot.AxisX.Minimum = 0D;
            this.easyChartXIQPlot.AxisX.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXIQPlot.AxisX.MinorGridEnabled = false;
            this.easyChartXIQPlot.AxisX.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXIQPlot.AxisX.TickWidth = 1F;
            this.easyChartXIQPlot.AxisX.Title = "";
            this.easyChartXIQPlot.AxisX.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXIQPlot.AxisX.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXIQPlot.AxisX.ViewMaximum = 1000D;
            this.easyChartXIQPlot.AxisX.ViewMinimum = 0D;
            this.easyChartXIQPlot.AxisX2.AutoScale = true;
            this.easyChartXIQPlot.AxisX2.AutoZoomReset = false;
            this.easyChartXIQPlot.AxisX2.Color = System.Drawing.Color.Black;
            this.easyChartXIQPlot.AxisX2.InitWithScaleView = false;
            this.easyChartXIQPlot.AxisX2.IsLogarithmic = false;
            this.easyChartXIQPlot.AxisX2.LabelAngle = 0;
            this.easyChartXIQPlot.AxisX2.LabelEnabled = true;
            this.easyChartXIQPlot.AxisX2.LabelFormat = null;
            this.easyChartXIQPlot.AxisX2.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXIQPlot.AxisX2.MajorGridCount = -1;
            this.easyChartXIQPlot.AxisX2.MajorGridEnabled = true;
            this.easyChartXIQPlot.AxisX2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXIQPlot.AxisX2.MaxGridCountPerPixel = 0.012D;
            this.easyChartXIQPlot.AxisX2.Maximum = 1000D;
            this.easyChartXIQPlot.AxisX2.MinGridCountPerPixel = 0.004D;
            this.easyChartXIQPlot.AxisX2.Minimum = 0D;
            this.easyChartXIQPlot.AxisX2.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXIQPlot.AxisX2.MinorGridEnabled = false;
            this.easyChartXIQPlot.AxisX2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXIQPlot.AxisX2.TickWidth = 1F;
            this.easyChartXIQPlot.AxisX2.Title = "";
            this.easyChartXIQPlot.AxisX2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXIQPlot.AxisX2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXIQPlot.AxisX2.ViewMaximum = 1000D;
            this.easyChartXIQPlot.AxisX2.ViewMinimum = 0D;
            this.easyChartXIQPlot.AxisY.AutoScale = true;
            this.easyChartXIQPlot.AxisY.AutoZoomReset = false;
            this.easyChartXIQPlot.AxisY.Color = System.Drawing.Color.Black;
            this.easyChartXIQPlot.AxisY.InitWithScaleView = false;
            this.easyChartXIQPlot.AxisY.IsLogarithmic = false;
            this.easyChartXIQPlot.AxisY.LabelAngle = 0;
            this.easyChartXIQPlot.AxisY.LabelEnabled = true;
            this.easyChartXIQPlot.AxisY.LabelFormat = null;
            this.easyChartXIQPlot.AxisY.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXIQPlot.AxisY.MajorGridCount = 6;
            this.easyChartXIQPlot.AxisY.MajorGridEnabled = true;
            this.easyChartXIQPlot.AxisY.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXIQPlot.AxisY.MaxGridCountPerPixel = 0.012D;
            this.easyChartXIQPlot.AxisY.Maximum = 3.5D;
            this.easyChartXIQPlot.AxisY.MinGridCountPerPixel = 0.004D;
            this.easyChartXIQPlot.AxisY.Minimum = 0.5D;
            this.easyChartXIQPlot.AxisY.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXIQPlot.AxisY.MinorGridEnabled = false;
            this.easyChartXIQPlot.AxisY.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXIQPlot.AxisY.TickWidth = 1F;
            this.easyChartXIQPlot.AxisY.Title = "";
            this.easyChartXIQPlot.AxisY.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXIQPlot.AxisY.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXIQPlot.AxisY.ViewMaximum = 3.5D;
            this.easyChartXIQPlot.AxisY.ViewMinimum = 0.5D;
            this.easyChartXIQPlot.AxisY2.AutoScale = true;
            this.easyChartXIQPlot.AxisY2.AutoZoomReset = false;
            this.easyChartXIQPlot.AxisY2.Color = System.Drawing.Color.Black;
            this.easyChartXIQPlot.AxisY2.InitWithScaleView = false;
            this.easyChartXIQPlot.AxisY2.IsLogarithmic = false;
            this.easyChartXIQPlot.AxisY2.LabelAngle = 0;
            this.easyChartXIQPlot.AxisY2.LabelEnabled = true;
            this.easyChartXIQPlot.AxisY2.LabelFormat = null;
            this.easyChartXIQPlot.AxisY2.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXIQPlot.AxisY2.MajorGridCount = 6;
            this.easyChartXIQPlot.AxisY2.MajorGridEnabled = true;
            this.easyChartXIQPlot.AxisY2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXIQPlot.AxisY2.MaxGridCountPerPixel = 0.012D;
            this.easyChartXIQPlot.AxisY2.Maximum = 3.5D;
            this.easyChartXIQPlot.AxisY2.MinGridCountPerPixel = 0.004D;
            this.easyChartXIQPlot.AxisY2.Minimum = 0.5D;
            this.easyChartXIQPlot.AxisY2.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXIQPlot.AxisY2.MinorGridEnabled = false;
            this.easyChartXIQPlot.AxisY2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXIQPlot.AxisY2.TickWidth = 1F;
            this.easyChartXIQPlot.AxisY2.Title = "";
            this.easyChartXIQPlot.AxisY2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXIQPlot.AxisY2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXIQPlot.AxisY2.ViewMaximum = 3.5D;
            this.easyChartXIQPlot.AxisY2.ViewMinimum = 0.5D;
            this.easyChartXIQPlot.BackColor = System.Drawing.Color.White;
            this.easyChartXIQPlot.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartXIQPlot.Cumulitive = false;
            this.easyChartXIQPlot.GradientStyle = SeeSharpTools.JY.GUI.EasyChartX.ChartGradientStyle.None;
            this.easyChartXIQPlot.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartXIQPlot.LegendFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.easyChartXIQPlot.LegendForeColor = System.Drawing.Color.Black;
            this.easyChartXIQPlot.LegendVisible = true;
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
            easyChartXSeries2.Visible = true;
            easyChartXSeries2.Width = SeeSharpTools.JY.GUI.EasyChartXSeries.LineWidth.Thin;
            easyChartXSeries2.XPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries2.YPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            this.easyChartXIQPlot.LineSeries.Add(easyChartXSeries1);
            this.easyChartXIQPlot.LineSeries.Add(easyChartXSeries2);
            this.easyChartXIQPlot.Location = new System.Drawing.Point(32, 23);
            this.easyChartXIQPlot.Miscellaneous.CheckInfinity = false;
            this.easyChartXIQPlot.Miscellaneous.CheckNaN = false;
            this.easyChartXIQPlot.Miscellaneous.CheckNegtiveOrZero = false;
            this.easyChartXIQPlot.Miscellaneous.DataStorage = SeeSharpTools.JY.GUI.DataStorageType.Clone;
            this.easyChartXIQPlot.Miscellaneous.DirectionChartCount = 3;
            this.easyChartXIQPlot.Miscellaneous.Fitting = SeeSharpTools.JY.GUI.EasyChartX.FitType.Range;
            this.easyChartXIQPlot.Miscellaneous.MarkerSize = 7;
            this.easyChartXIQPlot.Miscellaneous.MaxSeriesCount = 32;
            this.easyChartXIQPlot.Miscellaneous.MaxSeriesPointCount = 4000;
            this.easyChartXIQPlot.Miscellaneous.ShowFunctionMenu = true;
            this.easyChartXIQPlot.Miscellaneous.SplitLayoutColumnInterval = 0F;
            this.easyChartXIQPlot.Miscellaneous.SplitLayoutDirection = SeeSharpTools.JY.GUI.EasyChartXUtility.LayoutDirection.LeftToRight;
            this.easyChartXIQPlot.Miscellaneous.SplitLayoutRowInterval = 0F;
            this.easyChartXIQPlot.Miscellaneous.SplitViewAutoLayout = true;
            this.easyChartXIQPlot.Name = "easyChartXIQPlot";
            this.easyChartXIQPlot.SeriesCount = 0;
            this.easyChartXIQPlot.Size = new System.Drawing.Size(977, 330);
            this.easyChartXIQPlot.SplitView = false;
            this.easyChartXIQPlot.TabIndex = 0;
            this.easyChartXIQPlot.XCursor.AutoInterval = true;
            this.easyChartXIQPlot.XCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this.easyChartXIQPlot.XCursor.Interval = 0.001D;
            this.easyChartXIQPlot.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Zoom;
            this.easyChartXIQPlot.XCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.easyChartXIQPlot.XCursor.Value = double.NaN;
            this.easyChartXIQPlot.YCursor.AutoInterval = true;
            this.easyChartXIQPlot.YCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this.easyChartXIQPlot.YCursor.Interval = 0.001D;
            this.easyChartXIQPlot.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Disabled;
            this.easyChartXIQPlot.YCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.easyChartXIQPlot.YCursor.Value = double.NaN;
            // 
            // buttonRead
            // 
            this.buttonRead.Location = new System.Drawing.Point(897, 381);
            this.buttonRead.Name = "buttonRead";
            this.buttonRead.Size = new System.Drawing.Size(112, 23);
            this.buttonRead.TabIndex = 1;
            this.buttonRead.Text = "Read IQ File";
            this.buttonRead.UseVisualStyleBackColor = true;
            this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
            // 
            // textBoxFileInfo
            // 
            this.textBoxFileInfo.Location = new System.Drawing.Point(31, 359);
            this.textBoxFileInfo.Multiline = true;
            this.textBoxFileInfo.Name = "textBoxFileInfo";
            this.textBoxFileInfo.ReadOnly = true;
            this.textBoxFileInfo.Size = new System.Drawing.Size(853, 70);
            this.textBoxFileInfo.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // IQFileReadExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 441);
            this.Controls.Add(this.textBoxFileInfo);
            this.Controls.Add(this.buttonRead);
            this.Controls.Add(this.easyChartXIQPlot);
            this.Name = "IQFileReadExample";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SeeSharpTools.JY.GUI.EasyChartX easyChartXIQPlot;
        private System.Windows.Forms.Button buttonRead;
        private System.Windows.Forms.TextBox textBoxFileInfo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}

