namespace Winform_FrequencyResponseFunction
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
            this.buttonStart = new System.Windows.Forms.Button();
            this.easyChartMagenitude = new SeeSharpTools.JY.GUI.EasyChart();
            this.easyChartPhase = new SeeSharpTools.JY.GUI.EasyChart();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.easyChartCoherent = new SeeSharpTools.JY.GUI.EasyChart();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonStart.Location = new System.Drawing.Point(689, 401);
            this.buttonStart.MaximumSize = new System.Drawing.Size(115, 38);
            this.buttonStart.MinimumSize = new System.Drawing.Size(115, 38);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(115, 38);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // easyChartMagenitude
            // 
            this.easyChartMagenitude.AxisX.AutoScale = true;
            this.easyChartMagenitude.AxisX.InitWithScaleView = false;
            this.easyChartMagenitude.AxisX.LabelEnabled = true;
            this.easyChartMagenitude.AxisX.LabelFormat = "";
            this.easyChartMagenitude.AxisX.Maximum = 1001D;
            this.easyChartMagenitude.AxisX.Minimum = 0D;
            this.easyChartMagenitude.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartMagenitude.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartMagenitude.AxisX.Title = "";
            this.easyChartMagenitude.AxisX.ViewMaximum = 1001D;
            this.easyChartMagenitude.AxisX.ViewMinimum = 0D;
            this.easyChartMagenitude.AxisY.AutoScale = true;
            this.easyChartMagenitude.AxisY.InitWithScaleView = false;
            this.easyChartMagenitude.AxisY.LabelEnabled = true;
            this.easyChartMagenitude.AxisY.LabelFormat = "";
            this.easyChartMagenitude.AxisY.Maximum = 3.5D;
            this.easyChartMagenitude.AxisY.Minimum = 0D;
            this.easyChartMagenitude.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartMagenitude.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartMagenitude.AxisY.Title = "";
            this.easyChartMagenitude.AxisY.ViewMaximum = 3.5D;
            this.easyChartMagenitude.AxisY.ViewMinimum = 0D;
            this.easyChartMagenitude.AxisYMax = 3.5D;
            this.easyChartMagenitude.AxisYMin = 0D;
            this.easyChartMagenitude.BackGradientStyle = null;
            this.easyChartMagenitude.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartMagenitude.EasyChartBackColor = System.Drawing.Color.White;
            this.easyChartMagenitude.FixAxisX = false;
            this.easyChartMagenitude.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.easyChartMagenitude.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartMagenitude.LegendVisible = true;
            easyChartSeries1.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries1.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries1.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.easyChartMagenitude.LineSeries.Add(easyChartSeries1);
            this.easyChartMagenitude.Location = new System.Drawing.Point(9, 41);
            this.easyChartMagenitude.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartMagenitude.MajorGridEnabled = true;
            this.easyChartMagenitude.Margin = new System.Windows.Forms.Padding(2);
            this.easyChartMagenitude.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartMagenitude.MinorGridEnabled = false;
            this.easyChartMagenitude.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.easyChartMagenitude.Name = "easyChartMagenitude";
            this.easyChartMagenitude.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.easyChartMagenitude.SeriesNames = new string[] {
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
            this.easyChartMagenitude.Size = new System.Drawing.Size(573, 226);
            this.easyChartMagenitude.TabIndex = 1;
            this.easyChartMagenitude.XAxisLogarithmic = false;
            this.easyChartMagenitude.XAxisTitle = "";
            this.easyChartMagenitude.XCursor.AutoInterval = true;
            this.easyChartMagenitude.XCursor.Color = System.Drawing.Color.Red;
            this.easyChartMagenitude.XCursor.Interval = 1D;
            this.easyChartMagenitude.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.easyChartMagenitude.XCursor.Value = double.NaN;
            this.easyChartMagenitude.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartMagenitude.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartMagenitude.YAutoEnable = true;
            this.easyChartMagenitude.YAxisLogarithmic = false;
            this.easyChartMagenitude.YAxisTitle = "";
            this.easyChartMagenitude.YCursor.AutoInterval = true;
            this.easyChartMagenitude.YCursor.Color = System.Drawing.Color.Red;
            this.easyChartMagenitude.YCursor.Interval = 0.001D;
            this.easyChartMagenitude.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.easyChartMagenitude.YCursor.Value = double.NaN;
            this.easyChartMagenitude.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartMagenitude.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // easyChartPhase
            // 
            this.easyChartPhase.AxisX.AutoScale = true;
            this.easyChartPhase.AxisX.InitWithScaleView = false;
            this.easyChartPhase.AxisX.LabelEnabled = true;
            this.easyChartPhase.AxisX.LabelFormat = "";
            this.easyChartPhase.AxisX.Maximum = 1001D;
            this.easyChartPhase.AxisX.Minimum = 0D;
            this.easyChartPhase.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartPhase.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartPhase.AxisX.Title = "";
            this.easyChartPhase.AxisX.ViewMaximum = 1001D;
            this.easyChartPhase.AxisX.ViewMinimum = 0D;
            this.easyChartPhase.AxisY.AutoScale = true;
            this.easyChartPhase.AxisY.InitWithScaleView = false;
            this.easyChartPhase.AxisY.LabelEnabled = true;
            this.easyChartPhase.AxisY.LabelFormat = "";
            this.easyChartPhase.AxisY.Maximum = 3.5D;
            this.easyChartPhase.AxisY.Minimum = 0D;
            this.easyChartPhase.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartPhase.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartPhase.AxisY.Title = "";
            this.easyChartPhase.AxisY.ViewMaximum = 3.5D;
            this.easyChartPhase.AxisY.ViewMinimum = 0D;
            this.easyChartPhase.AxisYMax = 3.5D;
            this.easyChartPhase.AxisYMin = 0D;
            this.easyChartPhase.BackGradientStyle = null;
            this.easyChartPhase.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartPhase.EasyChartBackColor = System.Drawing.Color.White;
            this.easyChartPhase.FixAxisX = false;
            this.easyChartPhase.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.easyChartPhase.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartPhase.LegendVisible = true;
            easyChartSeries2.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries2.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries2.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.easyChartPhase.LineSeries.Add(easyChartSeries2);
            this.easyChartPhase.Location = new System.Drawing.Point(587, 41);
            this.easyChartPhase.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartPhase.MajorGridEnabled = true;
            this.easyChartPhase.Margin = new System.Windows.Forms.Padding(2);
            this.easyChartPhase.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartPhase.MinorGridEnabled = false;
            this.easyChartPhase.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.easyChartPhase.Name = "easyChartPhase";
            this.easyChartPhase.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.easyChartPhase.SeriesNames = new string[] {
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
            this.easyChartPhase.Size = new System.Drawing.Size(569, 228);
            this.easyChartPhase.TabIndex = 12;
            this.easyChartPhase.XAxisLogarithmic = false;
            this.easyChartPhase.XAxisTitle = "";
            this.easyChartPhase.XCursor.AutoInterval = true;
            this.easyChartPhase.XCursor.Color = System.Drawing.Color.Red;
            this.easyChartPhase.XCursor.Interval = 1D;
            this.easyChartPhase.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.easyChartPhase.XCursor.Value = double.NaN;
            this.easyChartPhase.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartPhase.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartPhase.YAutoEnable = true;
            this.easyChartPhase.YAxisLogarithmic = false;
            this.easyChartPhase.YAxisTitle = "";
            this.easyChartPhase.YCursor.AutoInterval = true;
            this.easyChartPhase.YCursor.Color = System.Drawing.Color.Red;
            this.easyChartPhase.YCursor.Interval = 0.001D;
            this.easyChartPhase.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.easyChartPhase.YCursor.Value = double.NaN;
            this.easyChartPhase.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartPhase.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "Amplitude frequency response:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(584, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "Phase frequency response:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 284);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "Related:";
            // 
            // easyChartCoherent
            // 
            this.easyChartCoherent.AxisX.AutoScale = true;
            this.easyChartCoherent.AxisX.InitWithScaleView = false;
            this.easyChartCoherent.AxisX.LabelEnabled = true;
            this.easyChartCoherent.AxisX.LabelFormat = "";
            this.easyChartCoherent.AxisX.Maximum = 1001D;
            this.easyChartCoherent.AxisX.Minimum = 0D;
            this.easyChartCoherent.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartCoherent.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartCoherent.AxisX.Title = "";
            this.easyChartCoherent.AxisX.ViewMaximum = 1001D;
            this.easyChartCoherent.AxisX.ViewMinimum = 0D;
            this.easyChartCoherent.AxisY.AutoScale = true;
            this.easyChartCoherent.AxisY.InitWithScaleView = false;
            this.easyChartCoherent.AxisY.LabelEnabled = true;
            this.easyChartCoherent.AxisY.LabelFormat = "";
            this.easyChartCoherent.AxisY.Maximum = 3.5D;
            this.easyChartCoherent.AxisY.Minimum = 0D;
            this.easyChartCoherent.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartCoherent.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartCoherent.AxisY.Title = "";
            this.easyChartCoherent.AxisY.ViewMaximum = 3.5D;
            this.easyChartCoherent.AxisY.ViewMinimum = 0D;
            this.easyChartCoherent.AxisYMax = 3.5D;
            this.easyChartCoherent.AxisYMin = 0D;
            this.easyChartCoherent.BackGradientStyle = null;
            this.easyChartCoherent.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartCoherent.EasyChartBackColor = System.Drawing.Color.White;
            this.easyChartCoherent.FixAxisX = false;
            this.easyChartCoherent.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.easyChartCoherent.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartCoherent.LegendVisible = true;
            easyChartSeries3.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries3.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries3.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.easyChartCoherent.LineSeries.Add(easyChartSeries3);
            this.easyChartCoherent.Location = new System.Drawing.Point(13, 298);
            this.easyChartCoherent.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartCoherent.MajorGridEnabled = true;
            this.easyChartCoherent.Margin = new System.Windows.Forms.Padding(2);
            this.easyChartCoherent.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartCoherent.MinorGridEnabled = false;
            this.easyChartCoherent.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.easyChartCoherent.Name = "easyChartCoherent";
            this.easyChartCoherent.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.easyChartCoherent.SeriesNames = new string[] {
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
            this.easyChartCoherent.Size = new System.Drawing.Size(573, 226);
            this.easyChartCoherent.TabIndex = 15;
            this.easyChartCoherent.XAxisLogarithmic = false;
            this.easyChartCoherent.XAxisTitle = "";
            this.easyChartCoherent.XCursor.AutoInterval = true;
            this.easyChartCoherent.XCursor.Color = System.Drawing.Color.Red;
            this.easyChartCoherent.XCursor.Interval = 1D;
            this.easyChartCoherent.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.easyChartCoherent.XCursor.Value = double.NaN;
            this.easyChartCoherent.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartCoherent.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartCoherent.YAutoEnable = true;
            this.easyChartCoherent.YAxisLogarithmic = false;
            this.easyChartCoherent.YAxisTitle = "";
            this.easyChartCoherent.YCursor.AutoInterval = true;
            this.easyChartCoherent.YCursor.Color = System.Drawing.Color.Red;
            this.easyChartCoherent.YCursor.Interval = 0.001D;
            this.easyChartCoherent.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.easyChartCoherent.YCursor.Value = double.NaN;
            this.easyChartCoherent.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartCoherent.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1177, 538);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.easyChartCoherent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.easyChartPhase);
            this.Controls.Add(this.easyChartMagenitude);
            this.Controls.Add(this.buttonStart);
            this.Name = "MainForm";
            this.Text = "FrequencyResponseFunction";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private SeeSharpTools.JY.GUI.EasyChart easyChartMagenitude;
        private SeeSharpTools.JY.GUI.EasyChart easyChartPhase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private SeeSharpTools.JY.GUI.EasyChart easyChartCoherent;
    }
}

