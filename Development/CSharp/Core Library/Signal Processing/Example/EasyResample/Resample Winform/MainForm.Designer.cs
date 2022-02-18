namespace Resample_Winform
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
            this.buttonResample = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBoxConfig = new System.Windows.Forms.GroupBox();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownDt = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownDelay = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSamples = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownFrequency = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSampleRate = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.easyChartBefore = new SeeSharpTools.JY.GUI.EasyChart();
            this.easyChartAfter = new SeeSharpTools.JY.GUI.EasyChart();
            this.groupBoxConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSamples)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSampleRate)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonResample
            // 
            this.buttonResample.Location = new System.Drawing.Point(688, 327);
            this.buttonResample.Name = "buttonResample";
            this.buttonResample.Size = new System.Drawing.Size(120, 36);
            this.buttonResample.TabIndex = 9;
            this.buttonResample.Text = "Start";
            this.buttonResample.UseVisualStyleBackColor = true;
            this.buttonResample.Click += new System.EventHandler(this.buttonResample_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 252);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(145, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "After re-sampling signal:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(144, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "Re-sampling pre-signal:";
            // 
            // groupBoxConfig
            // 
            this.groupBoxConfig.Controls.Add(this.comboBoxType);
            this.groupBoxConfig.Controls.Add(this.label8);
            this.groupBoxConfig.Controls.Add(this.numericUpDownDt);
            this.groupBoxConfig.Controls.Add(this.label7);
            this.groupBoxConfig.Controls.Add(this.numericUpDownDelay);
            this.groupBoxConfig.Controls.Add(this.numericUpDownSamples);
            this.groupBoxConfig.Controls.Add(this.numericUpDownFrequency);
            this.groupBoxConfig.Controls.Add(this.numericUpDownSampleRate);
            this.groupBoxConfig.Controls.Add(this.label4);
            this.groupBoxConfig.Controls.Add(this.label3);
            this.groupBoxConfig.Controls.Add(this.label2);
            this.groupBoxConfig.Controls.Add(this.label1);
            this.groupBoxConfig.Location = new System.Drawing.Point(579, 41);
            this.groupBoxConfig.Name = "groupBoxConfig";
            this.groupBoxConfig.Size = new System.Drawing.Size(319, 186);
            this.groupBoxConfig.TabIndex = 6;
            this.groupBoxConfig.TabStop = false;
            this.groupBoxConfig.Text = "Configuration";
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "Real",
            "Complex"});
            this.comboBoxType.Location = new System.Drawing.Point(165, 153);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(140, 20);
            this.comboBoxType.TabIndex = 14;
            this.comboBoxType.Text = "Real";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 157);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "Type of data:";
            // 
            // numericUpDownDt
            // 
            this.numericUpDownDt.DecimalPlaces = 1;
            this.numericUpDownDt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownDt.Location = new System.Drawing.Point(166, 125);
            this.numericUpDownDt.Name = "numericUpDownDt";
            this.numericUpDownDt.Size = new System.Drawing.Size(140, 21);
            this.numericUpDownDt.TabIndex = 12;
            this.numericUpDownDt.TabStop = false;
            this.numericUpDownDt.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 129);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "Sampling interval:";
            // 
            // numericUpDownDelay
            // 
            this.numericUpDownDelay.DecimalPlaces = 1;
            this.numericUpDownDelay.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownDelay.Location = new System.Drawing.Point(166, 100);
            this.numericUpDownDelay.Name = "numericUpDownDelay";
            this.numericUpDownDelay.Size = new System.Drawing.Size(140, 21);
            this.numericUpDownDelay.TabIndex = 7;
            this.numericUpDownDelay.TabStop = false;
            this.numericUpDownDelay.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // numericUpDownSamples
            // 
            this.numericUpDownSamples.Location = new System.Drawing.Point(166, 74);
            this.numericUpDownSamples.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownSamples.Name = "numericUpDownSamples";
            this.numericUpDownSamples.Size = new System.Drawing.Size(140, 21);
            this.numericUpDownSamples.TabIndex = 6;
            this.numericUpDownSamples.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // numericUpDownFrequency
            // 
            this.numericUpDownFrequency.Location = new System.Drawing.Point(166, 48);
            this.numericUpDownFrequency.Name = "numericUpDownFrequency";
            this.numericUpDownFrequency.Size = new System.Drawing.Size(140, 21);
            this.numericUpDownFrequency.TabIndex = 5;
            this.numericUpDownFrequency.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numericUpDownSampleRate
            // 
            this.numericUpDownSampleRate.Location = new System.Drawing.Point(166, 22);
            this.numericUpDownSampleRate.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownSampleRate.Name = "numericUpDownSampleRate";
            this.numericUpDownSampleRate.Size = new System.Drawing.Size(140, 21);
            this.numericUpDownSampleRate.TabIndex = 4;
            this.numericUpDownSampleRate.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "Delayed sampling points:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Sampling points:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Frequency:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sampling Rate:";
            // 
            // easyChartBefore
            // 
            this.easyChartBefore.AxisX.AutoScale = true;
            this.easyChartBefore.AxisX.InitWithScaleView = false;
            this.easyChartBefore.AxisX.LabelEnabled = true;
            this.easyChartBefore.AxisX.LabelFormat = "";
            this.easyChartBefore.AxisX.Maximum = 1001D;
            this.easyChartBefore.AxisX.Minimum = 0D;
            this.easyChartBefore.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartBefore.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartBefore.AxisX.Title = "";
            this.easyChartBefore.AxisX.ViewMaximum = 1001D;
            this.easyChartBefore.AxisX.ViewMinimum = 0D;
            this.easyChartBefore.AxisY.AutoScale = true;
            this.easyChartBefore.AxisY.InitWithScaleView = false;
            this.easyChartBefore.AxisY.LabelEnabled = true;
            this.easyChartBefore.AxisY.LabelFormat = "";
            this.easyChartBefore.AxisY.Maximum = 3.5D;
            this.easyChartBefore.AxisY.Minimum = 0D;
            this.easyChartBefore.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartBefore.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartBefore.AxisY.Title = "";
            this.easyChartBefore.AxisY.ViewMaximum = 3.5D;
            this.easyChartBefore.AxisY.ViewMinimum = 0D;
            this.easyChartBefore.AxisYMax = 3.5D;
            this.easyChartBefore.AxisYMin = 0D;
            this.easyChartBefore.BackGradientStyle = null;
            this.easyChartBefore.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartBefore.EasyChartBackColor = System.Drawing.Color.White;
            this.easyChartBefore.FixAxisX = false;
            this.easyChartBefore.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.easyChartBefore.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartBefore.LegendVisible = true;
            easyChartSeries1.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries1.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries1.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.easyChartBefore.LineSeries.Add(easyChartSeries1);
            this.easyChartBefore.Location = new System.Drawing.Point(13, 41);
            this.easyChartBefore.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartBefore.MajorGridEnabled = true;
            this.easyChartBefore.Margin = new System.Windows.Forms.Padding(2);
            this.easyChartBefore.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartBefore.MinorGridEnabled = false;
            this.easyChartBefore.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.easyChartBefore.Name = "easyChartBefore";
            this.easyChartBefore.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.easyChartBefore.SeriesNames = new string[] {
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
            this.easyChartBefore.Size = new System.Drawing.Size(548, 203);
            this.easyChartBefore.TabIndex = 10;
            this.easyChartBefore.XAxisLogarithmic = false;
            this.easyChartBefore.XAxisTitle = "";
            this.easyChartBefore.XCursor.AutoInterval = true;
            this.easyChartBefore.XCursor.Color = System.Drawing.Color.Red;
            this.easyChartBefore.XCursor.Interval = 1D;
            this.easyChartBefore.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.easyChartBefore.XCursor.Value = double.NaN;
            this.easyChartBefore.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartBefore.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartBefore.YAutoEnable = true;
            this.easyChartBefore.YAxisLogarithmic = false;
            this.easyChartBefore.YAxisTitle = "";
            this.easyChartBefore.YCursor.AutoInterval = true;
            this.easyChartBefore.YCursor.Color = System.Drawing.Color.Red;
            this.easyChartBefore.YCursor.Interval = 0.001D;
            this.easyChartBefore.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.easyChartBefore.YCursor.Value = double.NaN;
            this.easyChartBefore.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartBefore.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // easyChartAfter
            // 
            this.easyChartAfter.AxisX.AutoScale = true;
            this.easyChartAfter.AxisX.InitWithScaleView = false;
            this.easyChartAfter.AxisX.LabelEnabled = true;
            this.easyChartAfter.AxisX.LabelFormat = "";
            this.easyChartAfter.AxisX.Maximum = 1001D;
            this.easyChartAfter.AxisX.Minimum = 0D;
            this.easyChartAfter.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartAfter.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartAfter.AxisX.Title = "";
            this.easyChartAfter.AxisX.ViewMaximum = 1001D;
            this.easyChartAfter.AxisX.ViewMinimum = 0D;
            this.easyChartAfter.AxisY.AutoScale = true;
            this.easyChartAfter.AxisY.InitWithScaleView = false;
            this.easyChartAfter.AxisY.LabelEnabled = true;
            this.easyChartAfter.AxisY.LabelFormat = "";
            this.easyChartAfter.AxisY.Maximum = 3.5D;
            this.easyChartAfter.AxisY.Minimum = 0D;
            this.easyChartAfter.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartAfter.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartAfter.AxisY.Title = "";
            this.easyChartAfter.AxisY.ViewMaximum = 3.5D;
            this.easyChartAfter.AxisY.ViewMinimum = 0D;
            this.easyChartAfter.AxisYMax = 3.5D;
            this.easyChartAfter.AxisYMin = 0D;
            this.easyChartAfter.BackGradientStyle = null;
            this.easyChartAfter.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartAfter.EasyChartBackColor = System.Drawing.Color.White;
            this.easyChartAfter.FixAxisX = false;
            this.easyChartAfter.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.easyChartAfter.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartAfter.LegendVisible = true;
            easyChartSeries2.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries2.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries2.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.easyChartAfter.LineSeries.Add(easyChartSeries2);
            this.easyChartAfter.Location = new System.Drawing.Point(13, 270);
            this.easyChartAfter.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartAfter.MajorGridEnabled = true;
            this.easyChartAfter.Margin = new System.Windows.Forms.Padding(2);
            this.easyChartAfter.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartAfter.MinorGridEnabled = false;
            this.easyChartAfter.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.easyChartAfter.Name = "easyChartAfter";
            this.easyChartAfter.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.easyChartAfter.SeriesNames = new string[] {
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
            this.easyChartAfter.Size = new System.Drawing.Size(548, 203);
            this.easyChartAfter.TabIndex = 11;
            this.easyChartAfter.XAxisLogarithmic = false;
            this.easyChartAfter.XAxisTitle = "";
            this.easyChartAfter.XCursor.AutoInterval = true;
            this.easyChartAfter.XCursor.Color = System.Drawing.Color.Red;
            this.easyChartAfter.XCursor.Interval = 1D;
            this.easyChartAfter.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.easyChartAfter.XCursor.Value = double.NaN;
            this.easyChartAfter.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartAfter.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartAfter.YAutoEnable = true;
            this.easyChartAfter.YAxisLogarithmic = false;
            this.easyChartAfter.YAxisTitle = "";
            this.easyChartAfter.YCursor.AutoInterval = true;
            this.easyChartAfter.YCursor.Color = System.Drawing.Color.Red;
            this.easyChartAfter.YCursor.Interval = 0.001D;
            this.easyChartAfter.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.easyChartAfter.YCursor.Value = double.NaN;
            this.easyChartAfter.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartAfter.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 490);
            this.Controls.Add(this.easyChartAfter);
            this.Controls.Add(this.easyChartBefore);
            this.Controls.Add(this.buttonResample);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBoxConfig);
            this.Name = "MainForm";
            this.Text = "Resample";
            this.groupBoxConfig.ResumeLayout(false);
            this.groupBoxConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSamples)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSampleRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonResample;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBoxConfig;
        private System.Windows.Forms.NumericUpDown numericUpDownDt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownDelay;
        private System.Windows.Forms.NumericUpDown numericUpDownSamples;
        private System.Windows.Forms.NumericUpDown numericUpDownFrequency;
        private System.Windows.Forms.NumericUpDown numericUpDownSampleRate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private SeeSharpTools.JY.GUI.EasyChart easyChartBefore;
        private SeeSharpTools.JY.GUI.EasyChart easyChartAfter;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label label8;
    }
}

