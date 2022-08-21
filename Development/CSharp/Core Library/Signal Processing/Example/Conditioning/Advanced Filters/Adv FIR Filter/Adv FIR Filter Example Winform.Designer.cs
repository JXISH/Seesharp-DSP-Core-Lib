namespace Adv_FIR_Filter_Example
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
            this.ValueofDC = new System.Windows.Forms.NumericUpDown();
            this.Text_DC = new System.Windows.Forms.Label();
            this.ValueofPhase = new System.Windows.Forms.NumericUpDown();
            this.Text_Phase = new System.Windows.Forms.Label();
            this.ValueofAmplitude = new System.Windows.Forms.NumericUpDown();
            this.Text_Amplitude = new System.Windows.Forms.Label();
            this.ValueofFrequency = new System.Windows.Forms.NumericUpDown();
            this.Text_Freq = new System.Windows.Forms.Label();
            this.SinWaveConfigArea = new System.Windows.Forms.GroupBox();
            this.Clear = new System.Windows.Forms.Button();
            this.DownSampleFIR = new System.Windows.Forms.Button();
            this.UpSampleFIR = new System.Windows.Forms.Button();
            this.Add_Noise = new System.Windows.Forms.Button();
            this.FIR = new System.Windows.Forms.Button();
            this.Generate = new System.Windows.Forms.Button();
            this.fig1 = new SeeSharpTools.JY.GUI.EasyChart();
            this.fig2 = new SeeSharpTools.JY.GUI.EasyChart();
            ((System.ComponentModel.ISupportInitialize)(this.ValueofDC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValueofPhase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValueofAmplitude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValueofFrequency)).BeginInit();
            this.SuspendLayout();
            // 
            // ValueofDC
            // 
            this.ValueofDC.Location = new System.Drawing.Point(659, 173);
            this.ValueofDC.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ValueofDC.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.ValueofDC.Name = "ValueofDC";
            this.ValueofDC.Size = new System.Drawing.Size(100, 21);
            this.ValueofDC.TabIndex = 27;
            // 
            // Text_DC
            // 
            this.Text_DC.AutoSize = true;
            this.Text_DC.Font = new System.Drawing.Font("SimSun", 10F);
            this.Text_DC.Location = new System.Drawing.Point(567, 175);
            this.Text_DC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Text_DC.Name = "Text_DC";
            this.Text_DC.Size = new System.Drawing.Size(28, 14);
            this.Text_DC.TabIndex = 26;
            this.Text_DC.Text = "DC:";
            // 
            // ValueofPhase
            // 
            this.ValueofPhase.Location = new System.Drawing.Point(659, 98);
            this.ValueofPhase.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ValueofPhase.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.ValueofPhase.Name = "ValueofPhase";
            this.ValueofPhase.Size = new System.Drawing.Size(100, 21);
            this.ValueofPhase.TabIndex = 25;
            // 
            // Text_Phase
            // 
            this.Text_Phase.AutoSize = true;
            this.Text_Phase.Font = new System.Drawing.Font("SimSun", 10F);
            this.Text_Phase.Location = new System.Drawing.Point(567, 100);
            this.Text_Phase.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Text_Phase.Name = "Text_Phase";
            this.Text_Phase.Size = new System.Drawing.Size(49, 14);
            this.Text_Phase.TabIndex = 24;
            this.Text_Phase.Text = "Phase:";
            // 
            // ValueofAmplitude
            // 
            this.ValueofAmplitude.Location = new System.Drawing.Point(659, 135);
            this.ValueofAmplitude.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ValueofAmplitude.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.ValueofAmplitude.Name = "ValueofAmplitude";
            this.ValueofAmplitude.Size = new System.Drawing.Size(100, 21);
            this.ValueofAmplitude.TabIndex = 23;
            this.ValueofAmplitude.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // Text_Amplitude
            // 
            this.Text_Amplitude.AutoSize = true;
            this.Text_Amplitude.Font = new System.Drawing.Font("SimSun", 10F);
            this.Text_Amplitude.Location = new System.Drawing.Point(567, 138);
            this.Text_Amplitude.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Text_Amplitude.Name = "Text_Amplitude";
            this.Text_Amplitude.Size = new System.Drawing.Size(77, 14);
            this.Text_Amplitude.TabIndex = 22;
            this.Text_Amplitude.Text = "Amplitude:";
            // 
            // ValueofFrequency
            // 
            this.ValueofFrequency.Location = new System.Drawing.Point(659, 59);
            this.ValueofFrequency.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ValueofFrequency.Name = "ValueofFrequency";
            this.ValueofFrequency.Size = new System.Drawing.Size(100, 21);
            this.ValueofFrequency.TabIndex = 21;
            this.ValueofFrequency.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // Text_Freq
            // 
            this.Text_Freq.AutoSize = true;
            this.Text_Freq.Font = new System.Drawing.Font("SimSun", 10F);
            this.Text_Freq.Location = new System.Drawing.Point(567, 62);
            this.Text_Freq.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Text_Freq.Name = "Text_Freq";
            this.Text_Freq.Size = new System.Drawing.Size(42, 14);
            this.Text_Freq.TabIndex = 20;
            this.Text_Freq.Text = "Freq:";
            // 
            // SinWaveConfigArea
            // 
            this.SinWaveConfigArea.Location = new System.Drawing.Point(511, 38);
            this.SinWaveConfigArea.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SinWaveConfigArea.Name = "SinWaveConfigArea";
            this.SinWaveConfigArea.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SinWaveConfigArea.Size = new System.Drawing.Size(267, 170);
            this.SinWaveConfigArea.TabIndex = 28;
            this.SinWaveConfigArea.TabStop = false;
            this.SinWaveConfigArea.Text = "Wave Type";
            // 
            // Clear
            // 
            this.Clear.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Clear.Location = new System.Drawing.Point(511, 337);
            this.Clear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(189, 26);
            this.Clear.TabIndex = 34;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // DownSampleFIR
            // 
            this.DownSampleFIR.AccessibleRole = System.Windows.Forms.AccessibleRole.Sound;
            this.DownSampleFIR.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.DownSampleFIR.Location = new System.Drawing.Point(754, 337);
            this.DownSampleFIR.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DownSampleFIR.Name = "DownSampleFIR";
            this.DownSampleFIR.Size = new System.Drawing.Size(189, 26);
            this.DownSampleFIR.TabIndex = 33;
            this.DownSampleFIR.Text = "DownSampleFIR";
            this.DownSampleFIR.UseVisualStyleBackColor = true;
            this.DownSampleFIR.Click += new System.EventHandler(this.DownSampleFIR_Click);
            // 
            // UpSampleFIR
            // 
            this.UpSampleFIR.AccessibleRole = System.Windows.Forms.AccessibleRole.Sound;
            this.UpSampleFIR.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.UpSampleFIR.Location = new System.Drawing.Point(754, 291);
            this.UpSampleFIR.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.UpSampleFIR.Name = "UpSampleFIR";
            this.UpSampleFIR.Size = new System.Drawing.Size(189, 26);
            this.UpSampleFIR.TabIndex = 32;
            this.UpSampleFIR.Text = "UpSampleFIR";
            this.UpSampleFIR.UseVisualStyleBackColor = true;
            this.UpSampleFIR.Click += new System.EventHandler(this.UpSampleFIR_Click);
            // 
            // Add_Noise
            // 
            this.Add_Noise.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Add_Noise.Location = new System.Drawing.Point(511, 291);
            this.Add_Noise.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Add_Noise.Name = "Add_Noise";
            this.Add_Noise.Size = new System.Drawing.Size(189, 26);
            this.Add_Noise.TabIndex = 31;
            this.Add_Noise.Text = "Add Noise";
            this.Add_Noise.UseVisualStyleBackColor = true;
            this.Add_Noise.Click += new System.EventHandler(this.Add_Noise_Click);
            // 
            // FIR
            // 
            this.FIR.AccessibleRole = System.Windows.Forms.AccessibleRole.Sound;
            this.FIR.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.FIR.Location = new System.Drawing.Point(754, 246);
            this.FIR.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.FIR.Name = "FIR";
            this.FIR.Size = new System.Drawing.Size(189, 26);
            this.FIR.TabIndex = 30;
            this.FIR.Text = "FIR";
            this.FIR.UseVisualStyleBackColor = true;
            this.FIR.Click += new System.EventHandler(this.FIR_Click);
            // 
            // Generate
            // 
            this.Generate.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Generate.Location = new System.Drawing.Point(511, 246);
            this.Generate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Generate.Name = "Generate";
            this.Generate.Size = new System.Drawing.Size(189, 26);
            this.Generate.TabIndex = 29;
            this.Generate.Text = "Generate input sinwave";
            this.Generate.UseVisualStyleBackColor = true;
            this.Generate.Click += new System.EventHandler(this.SinwaveGeneration_Click);
            // 
            // fig1
            // 
            this.fig1.AxisX.AutoScale = true;
            this.fig1.AxisX.InitWithScaleView = false;
            this.fig1.AxisX.LabelEnabled = true;
            this.fig1.AxisX.LabelFormat = "";
            this.fig1.AxisX.Maximum = 1001D;
            this.fig1.AxisX.Minimum = 0D;
            this.fig1.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.fig1.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.fig1.AxisX.Title = "";
            this.fig1.AxisX.ViewMaximum = 1001D;
            this.fig1.AxisX.ViewMinimum = 0D;
            this.fig1.AxisY.AutoScale = true;
            this.fig1.AxisY.InitWithScaleView = false;
            this.fig1.AxisY.LabelEnabled = true;
            this.fig1.AxisY.LabelFormat = "";
            this.fig1.AxisY.Maximum = 3.5D;
            this.fig1.AxisY.Minimum = 0D;
            this.fig1.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.fig1.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.fig1.AxisY.Title = "";
            this.fig1.AxisY.ViewMaximum = 3.5D;
            this.fig1.AxisY.ViewMinimum = 0D;
            this.fig1.AxisYMax = 3.5D;
            this.fig1.AxisYMin = 0D;
            this.fig1.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.fig1.EasyChartBackColor = System.Drawing.Color.White;
            this.fig1.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.fig1.LegendBackColor = System.Drawing.Color.Transparent;
            this.fig1.LegendVisible = true;
            easyChartSeries1.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries1.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries1.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.fig1.LineSeries.Add(easyChartSeries1);
            this.fig1.Location = new System.Drawing.Point(27, 20);
            this.fig1.MajorGridColor = System.Drawing.Color.Black;
            this.fig1.MajorGridEnabled = true;
            this.fig1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.fig1.MinorGridColor = System.Drawing.Color.Black;
            this.fig1.MinorGridEnabled = false;
            this.fig1.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.fig1.Name = "fig1";
            this.fig1.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.fig1.SeriesNames = new string[] {
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
            this.fig1.Size = new System.Drawing.Size(450, 188);
            this.fig1.TabIndex = 35;
            this.fig1.XAxisLogarithmic = false;
            this.fig1.XAxisTitle = "";
            this.fig1.XCursor.AutoInterval = true;
            this.fig1.XCursor.Color = System.Drawing.Color.Red;
            this.fig1.XCursor.Interval = 1D;
            this.fig1.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.fig1.XCursor.Value = double.NaN;
            this.fig1.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.fig1.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.fig1.YAutoEnable = true;
            this.fig1.YAxisLogarithmic = false;
            this.fig1.YAxisTitle = "";
            this.fig1.YCursor.AutoInterval = true;
            this.fig1.YCursor.Color = System.Drawing.Color.Red;
            this.fig1.YCursor.Interval = 0.001D;
            this.fig1.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.fig1.YCursor.Value = double.NaN;
            this.fig1.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.fig1.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // fig2
            // 
            this.fig2.AxisX.AutoScale = true;
            this.fig2.AxisX.InitWithScaleView = false;
            this.fig2.AxisX.LabelEnabled = true;
            this.fig2.AxisX.LabelFormat = "";
            this.fig2.AxisX.Maximum = 1001D;
            this.fig2.AxisX.Minimum = 0D;
            this.fig2.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.fig2.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.fig2.AxisX.Title = "";
            this.fig2.AxisX.ViewMaximum = 1001D;
            this.fig2.AxisX.ViewMinimum = 0D;
            this.fig2.AxisY.AutoScale = true;
            this.fig2.AxisY.InitWithScaleView = false;
            this.fig2.AxisY.LabelEnabled = true;
            this.fig2.AxisY.LabelFormat = "";
            this.fig2.AxisY.Maximum = 3.5D;
            this.fig2.AxisY.Minimum = 0D;
            this.fig2.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.fig2.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.fig2.AxisY.Title = "";
            this.fig2.AxisY.ViewMaximum = 3.5D;
            this.fig2.AxisY.ViewMinimum = 0D;
            this.fig2.AxisYMax = 3.5D;
            this.fig2.AxisYMin = 0D;
            this.fig2.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.fig2.EasyChartBackColor = System.Drawing.Color.White;
            this.fig2.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.fig2.LegendBackColor = System.Drawing.Color.Transparent;
            this.fig2.LegendVisible = true;
            easyChartSeries2.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries2.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries2.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.fig2.LineSeries.Add(easyChartSeries2);
            this.fig2.Location = new System.Drawing.Point(27, 220);
            this.fig2.MajorGridColor = System.Drawing.Color.Black;
            this.fig2.MajorGridEnabled = true;
            this.fig2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.fig2.MinorGridColor = System.Drawing.Color.Black;
            this.fig2.MinorGridEnabled = false;
            this.fig2.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.fig2.Name = "fig2";
            this.fig2.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.fig2.SeriesNames = new string[] {
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
            this.fig2.Size = new System.Drawing.Size(450, 188);
            this.fig2.TabIndex = 36;
            this.fig2.XAxisLogarithmic = false;
            this.fig2.XAxisTitle = "";
            this.fig2.XCursor.AutoInterval = true;
            this.fig2.XCursor.Color = System.Drawing.Color.Red;
            this.fig2.XCursor.Interval = 1D;
            this.fig2.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.fig2.XCursor.Value = double.NaN;
            this.fig2.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.fig2.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.fig2.YAutoEnable = true;
            this.fig2.YAxisLogarithmic = false;
            this.fig2.YAxisTitle = "";
            this.fig2.YCursor.AutoInterval = true;
            this.fig2.YCursor.Color = System.Drawing.Color.Red;
            this.fig2.YCursor.Interval = 0.001D;
            this.fig2.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.fig2.YCursor.Value = double.NaN;
            this.fig2.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.fig2.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 425);
            this.Controls.Add(this.fig2);
            this.Controls.Add(this.fig1);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.DownSampleFIR);
            this.Controls.Add(this.UpSampleFIR);
            this.Controls.Add(this.Add_Noise);
            this.Controls.Add(this.FIR);
            this.Controls.Add(this.Generate);
            this.Controls.Add(this.ValueofDC);
            this.Controls.Add(this.Text_DC);
            this.Controls.Add(this.ValueofPhase);
            this.Controls.Add(this.Text_Phase);
            this.Controls.Add(this.ValueofAmplitude);
            this.Controls.Add(this.Text_Amplitude);
            this.Controls.Add(this.ValueofFrequency);
            this.Controls.Add(this.Text_Freq);
            this.Controls.Add(this.SinWaveConfigArea);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Adv FIR Filter Example";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ValueofDC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValueofPhase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValueofAmplitude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValueofFrequency)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown ValueofDC;
        private System.Windows.Forms.Label Text_DC;
        private System.Windows.Forms.NumericUpDown ValueofPhase;
        private System.Windows.Forms.Label Text_Phase;
        private System.Windows.Forms.NumericUpDown ValueofAmplitude;
        private System.Windows.Forms.Label Text_Amplitude;
        private System.Windows.Forms.NumericUpDown ValueofFrequency;
        private System.Windows.Forms.Label Text_Freq;
        private System.Windows.Forms.GroupBox SinWaveConfigArea;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Button DownSampleFIR;
        private System.Windows.Forms.Button UpSampleFIR;
        private System.Windows.Forms.Button Add_Noise;
        private System.Windows.Forms.Button FIR;
        private System.Windows.Forms.Button Generate;
        private SeeSharpTools.JY.GUI.EasyChart fig1;
        private SeeSharpTools.JY.GUI.EasyChart fig2;
    }
}

