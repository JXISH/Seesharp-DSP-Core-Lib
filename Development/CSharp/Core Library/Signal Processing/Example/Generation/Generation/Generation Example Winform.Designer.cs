namespace Generation_Example
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
            this.numericUpDownNoiseStd = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.AddGaussianNoise = new System.Windows.Forms.Button();
            this.Generate = new System.Windows.Forms.Button();
            this.numericUpDownPhase = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownA = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownFrequency = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSampleRate = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Wavetype = new System.Windows.Forms.GroupBox();
            this.WaveForm = new System.Windows.Forms.ComboBox();
            this.Configuration = new System.Windows.Forms.GroupBox();
            this.numericUpDownDutyCycle = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.fig = new SeeSharpTools.JY.GUI.EasyChart();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNoiseStd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPhase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSampleRate)).BeginInit();
            this.Wavetype.SuspendLayout();
            this.Configuration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDutyCycle)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownNoiseStd
            // 
            this.numericUpDownNoiseStd.DecimalPlaces = 1;
            this.numericUpDownNoiseStd.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownNoiseStd.Location = new System.Drawing.Point(696, 304);
            this.numericUpDownNoiseStd.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.numericUpDownNoiseStd.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNoiseStd.Name = "numericUpDownNoiseStd";
            this.numericUpDownNoiseStd.Size = new System.Drawing.Size(46, 21);
            this.numericUpDownNoiseStd.TabIndex = 40;
            this.numericUpDownNoiseStd.TabStop = false;
            this.numericUpDownNoiseStd.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // label5
            // 
            this.label5.AllowDrop = true;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(514, 309);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(167, 12);
            this.label5.TabIndex = 39;
            this.label5.Text = "Standard deviation of noise";
            // 
            // AddGaussianNoise
            // 
            this.AddGaussianNoise.Location = new System.Drawing.Point(509, 326);
            this.AddGaussianNoise.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.AddGaussianNoise.Name = "AddGaussianNoise";
            this.AddGaussianNoise.Size = new System.Drawing.Size(121, 18);
            this.AddGaussianNoise.TabIndex = 38;
            this.AddGaussianNoise.Text = "Add Gaussian Noise";
            this.AddGaussianNoise.UseVisualStyleBackColor = true;
            this.AddGaussianNoise.Click += new System.EventHandler(this.AddGaussianNoise_Click);
            // 
            // Generate
            // 
            this.Generate.Location = new System.Drawing.Point(509, 284);
            this.Generate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Generate.Name = "Generate";
            this.Generate.Size = new System.Drawing.Size(121, 18);
            this.Generate.TabIndex = 37;
            this.Generate.Text = "Generate";
            this.Generate.UseVisualStyleBackColor = true;
            this.Generate.Click += new System.EventHandler(this.Generate_Click);
            // 
            // numericUpDownPhase
            // 
            this.numericUpDownPhase.DecimalPlaces = 1;
            this.numericUpDownPhase.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownPhase.Location = new System.Drawing.Point(650, 222);
            this.numericUpDownPhase.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.numericUpDownPhase.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numericUpDownPhase.Name = "numericUpDownPhase";
            this.numericUpDownPhase.Size = new System.Drawing.Size(120, 21);
            this.numericUpDownPhase.TabIndex = 36;
            this.numericUpDownPhase.TabStop = false;
            this.numericUpDownPhase.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // numericUpDownA
            // 
            this.numericUpDownA.Location = new System.Drawing.Point(650, 195);
            this.numericUpDownA.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.numericUpDownA.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownA.Name = "numericUpDownA";
            this.numericUpDownA.Size = new System.Drawing.Size(120, 21);
            this.numericUpDownA.TabIndex = 35;
            this.numericUpDownA.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // numericUpDownFrequency
            // 
            this.numericUpDownFrequency.Location = new System.Drawing.Point(650, 170);
            this.numericUpDownFrequency.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.numericUpDownFrequency.Name = "numericUpDownFrequency";
            this.numericUpDownFrequency.Size = new System.Drawing.Size(120, 21);
            this.numericUpDownFrequency.TabIndex = 34;
            this.numericUpDownFrequency.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numericUpDownSampleRate
            // 
            this.numericUpDownSampleRate.Location = new System.Drawing.Point(650, 144);
            this.numericUpDownSampleRate.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.numericUpDownSampleRate.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownSampleRate.Name = "numericUpDownSampleRate";
            this.numericUpDownSampleRate.Size = new System.Drawing.Size(120, 21);
            this.numericUpDownSampleRate.TabIndex = 33;
            this.numericUpDownSampleRate.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(516, 226);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 32;
            this.label4.Text = "phase";
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(516, 200);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 31;
            this.label3.Text = "Amplitude";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(516, 173);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 30;
            this.label2.Text = "Frequency:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(516, 147);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 29;
            this.label1.Text = "Sampling Rate:";
            // 
            // Wavetype
            // 
            this.Wavetype.Controls.Add(this.WaveForm);
            this.Wavetype.Location = new System.Drawing.Point(517, 50);
            this.Wavetype.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Wavetype.Name = "Wavetype";
            this.Wavetype.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Wavetype.Size = new System.Drawing.Size(150, 70);
            this.Wavetype.TabIndex = 27;
            this.Wavetype.TabStop = false;
            this.Wavetype.Text = "Wave Type";
            // 
            // WaveForm
            // 
            this.WaveForm.FormattingEnabled = true;
            this.WaveForm.Items.AddRange(new object[] {
            "SineWave",
            "SquareWave"});
            this.WaveForm.Location = new System.Drawing.Point(32, 30);
            this.WaveForm.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.WaveForm.Name = "WaveForm";
            this.WaveForm.Size = new System.Drawing.Size(92, 20);
            this.WaveForm.TabIndex = 6;
            this.WaveForm.Text = "SineWave";
            // 
            // Configuration
            // 
            this.Configuration.Controls.Add(this.numericUpDownDutyCycle);
            this.Configuration.Controls.Add(this.label6);
            this.Configuration.Location = new System.Drawing.Point(509, 128);
            this.Configuration.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Configuration.Name = "Configuration";
            this.Configuration.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Configuration.Size = new System.Drawing.Size(278, 147);
            this.Configuration.TabIndex = 28;
            this.Configuration.TabStop = false;
            this.Configuration.Text = "Wave Type";
            // 
            // numericUpDownDutyCycle
            // 
            this.numericUpDownDutyCycle.DecimalPlaces = 1;
            this.numericUpDownDutyCycle.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownDutyCycle.Location = new System.Drawing.Point(141, 119);
            this.numericUpDownDutyCycle.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.numericUpDownDutyCycle.Name = "numericUpDownDutyCycle";
            this.numericUpDownDutyCycle.Size = new System.Drawing.Size(120, 21);
            this.numericUpDownDutyCycle.TabIndex = 27;
            this.numericUpDownDutyCycle.TabStop = false;
            this.numericUpDownDutyCycle.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 123);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 27;
            this.label6.Text = "DutyCycle";
            // 
            // fig
            // 
            this.fig.AxisX.AutoScale = true;
            this.fig.AxisX.InitWithScaleView = false;
            this.fig.AxisX.LabelEnabled = true;
            this.fig.AxisX.LabelFormat = "";
            this.fig.AxisX.Maximum = 1001D;
            this.fig.AxisX.Minimum = 0D;
            this.fig.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.fig.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.fig.AxisX.Title = "";
            this.fig.AxisX.ViewMaximum = 1001D;
            this.fig.AxisX.ViewMinimum = 0D;
            this.fig.AxisY.AutoScale = true;
            this.fig.AxisY.InitWithScaleView = false;
            this.fig.AxisY.LabelEnabled = true;
            this.fig.AxisY.LabelFormat = "";
            this.fig.AxisY.Maximum = 3.5D;
            this.fig.AxisY.Minimum = 0D;
            this.fig.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.fig.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.fig.AxisY.Title = "";
            this.fig.AxisY.ViewMaximum = 3.5D;
            this.fig.AxisY.ViewMinimum = 0D;
            this.fig.AxisYMax = 3.5D;
            this.fig.AxisYMin = 0D;
            this.fig.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.fig.EasyChartBackColor = System.Drawing.Color.White;
            this.fig.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.fig.LegendBackColor = System.Drawing.Color.Transparent;
            this.fig.LegendVisible = true;
            easyChartSeries1.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries1.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries1.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.fig.LineSeries.Add(easyChartSeries1);
            this.fig.Location = new System.Drawing.Point(28, 38);
            this.fig.MajorGridColor = System.Drawing.Color.Black;
            this.fig.MajorGridEnabled = true;
            this.fig.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.fig.MinorGridColor = System.Drawing.Color.Black;
            this.fig.MinorGridEnabled = false;
            this.fig.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.fig.Name = "fig";
            this.fig.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.fig.SeriesNames = new string[] {
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
            this.fig.Size = new System.Drawing.Size(450, 320);
            this.fig.TabIndex = 41;
            this.fig.XAxisLogarithmic = false;
            this.fig.XAxisTitle = "";
            this.fig.XCursor.AutoInterval = true;
            this.fig.XCursor.Color = System.Drawing.Color.Red;
            this.fig.XCursor.Interval = 1D;
            this.fig.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.fig.XCursor.Value = double.NaN;
            this.fig.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.fig.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.fig.YAutoEnable = true;
            this.fig.YAxisLogarithmic = false;
            this.fig.YAxisTitle = "";
            this.fig.YCursor.AutoInterval = true;
            this.fig.YCursor.Color = System.Drawing.Color.Red;
            this.fig.YCursor.Interval = 0.001D;
            this.fig.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.fig.YCursor.Value = double.NaN;
            this.fig.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.fig.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 391);
            this.Controls.Add(this.fig);
            this.Controls.Add(this.numericUpDownNoiseStd);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.AddGaussianNoise);
            this.Controls.Add(this.Generate);
            this.Controls.Add(this.numericUpDownPhase);
            this.Controls.Add(this.numericUpDownA);
            this.Controls.Add(this.numericUpDownFrequency);
            this.Controls.Add(this.numericUpDownSampleRate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Wavetype);
            this.Controls.Add(this.Configuration);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Waveform Generation Example";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNoiseStd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPhase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSampleRate)).EndInit();
            this.Wavetype.ResumeLayout(false);
            this.Configuration.ResumeLayout(false);
            this.Configuration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDutyCycle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownNoiseStd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button AddGaussianNoise;
        private System.Windows.Forms.Button Generate;
        private System.Windows.Forms.NumericUpDown numericUpDownPhase;
        private System.Windows.Forms.NumericUpDown numericUpDownA;
        private System.Windows.Forms.NumericUpDown numericUpDownFrequency;
        private System.Windows.Forms.NumericUpDown numericUpDownSampleRate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox Wavetype;
        private System.Windows.Forms.ComboBox WaveForm;
        private System.Windows.Forms.GroupBox Configuration;
        private System.Windows.Forms.NumericUpDown numericUpDownDutyCycle;
        private System.Windows.Forms.Label label6;
        private SeeSharpTools.JY.GUI.EasyChart fig;
    }
}

