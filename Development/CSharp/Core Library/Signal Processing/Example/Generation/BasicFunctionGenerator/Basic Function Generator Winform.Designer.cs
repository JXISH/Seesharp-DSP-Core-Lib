namespace BasicFunctionGeneratorExample
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
            this.fig1 = new SeeSharpTools.JY.GUI.EasyChart();
            this.FunctionSelect = new System.Windows.Forms.ComboBox();
            this.Start = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SamplingRate = new System.Windows.Forms.NumericUpDown();
            this.Frequency = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.Amplitude = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.Initialphase = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.SNR = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.NoiseSelect = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.SamplingRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Frequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amplitude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Initialphase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SNR)).BeginInit();
            this.SuspendLayout();
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
            this.fig1.Location = new System.Drawing.Point(39, 30);
            this.fig1.MajorGridColor = System.Drawing.Color.Black;
            this.fig1.MajorGridEnabled = true;
            this.fig1.Margin = new System.Windows.Forms.Padding(1);
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
            this.fig1.Size = new System.Drawing.Size(489, 318);
            this.fig1.TabIndex = 0;
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
            // FunctionSelect
            // 
            this.FunctionSelect.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.FunctionSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FunctionSelect.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FunctionSelect.FormattingEnabled = true;
            this.FunctionSelect.Items.AddRange(new object[] {
            "生成单一复数正弦波",
            "生成I/Q交错格式的单一复数正弦波",
            "生成单一正弦波",
            "正弦波发生器",
            "三角波发生器",
            "锯齿波发生器",
            "方波发生器",
            "复数正弦波发生器"});
            this.FunctionSelect.Location = new System.Drawing.Point(583, 30);
            this.FunctionSelect.Margin = new System.Windows.Forms.Padding(2);
            this.FunctionSelect.Name = "FunctionSelect";
            this.FunctionSelect.Size = new System.Drawing.Size(231, 22);
            this.FunctionSelect.TabIndex = 1;
            this.FunctionSelect.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Start
            // 
            this.Start.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Start.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start.ForeColor = System.Drawing.Color.Navy;
            this.Start.Location = new System.Drawing.Point(638, 312);
            this.Start.Margin = new System.Windows.Forms.Padding(2);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(150, 59);
            this.Start.TabIndex = 2;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = false;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(556, 87);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "SamplingRate";
            // 
            // SamplingRate
            // 
            this.SamplingRate.DecimalPlaces = 1;
            this.SamplingRate.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SamplingRate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.SamplingRate.Location = new System.Drawing.Point(704, 86);
            this.SamplingRate.Margin = new System.Windows.Forms.Padding(2);
            this.SamplingRate.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.SamplingRate.Name = "SamplingRate";
            this.SamplingRate.Size = new System.Drawing.Size(146, 24);
            this.SamplingRate.TabIndex = 4;
            this.SamplingRate.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // Frequency
            // 
            this.Frequency.DecimalPlaces = 1;
            this.Frequency.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Frequency.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Frequency.Location = new System.Drawing.Point(704, 113);
            this.Frequency.Margin = new System.Windows.Forms.Padding(2);
            this.Frequency.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.Frequency.Name = "Frequency";
            this.Frequency.Size = new System.Drawing.Size(146, 24);
            this.Frequency.TabIndex = 6;
            this.Frequency.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(556, 114);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Frequency(Hz)";
            // 
            // Amplitude
            // 
            this.Amplitude.DecimalPlaces = 1;
            this.Amplitude.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Amplitude.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Amplitude.Location = new System.Drawing.Point(704, 140);
            this.Amplitude.Margin = new System.Windows.Forms.Padding(2);
            this.Amplitude.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.Amplitude.Name = "Amplitude";
            this.Amplitude.Size = new System.Drawing.Size(146, 24);
            this.Amplitude.TabIndex = 8;
            this.Amplitude.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(556, 142);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Amplitude(V)";
            // 
            // Initialphase
            // 
            this.Initialphase.DecimalPlaces = 1;
            this.Initialphase.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Initialphase.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Initialphase.Location = new System.Drawing.Point(704, 167);
            this.Initialphase.Margin = new System.Windows.Forms.Padding(2);
            this.Initialphase.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.Initialphase.Name = "Initialphase";
            this.Initialphase.Size = new System.Drawing.Size(146, 24);
            this.Initialphase.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(556, 170);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "InitialPhase";
            // 
            // SNR
            // 
            this.SNR.DecimalPlaces = 1;
            this.SNR.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SNR.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.SNR.Location = new System.Drawing.Point(704, 194);
            this.SNR.Margin = new System.Windows.Forms.Padding(2);
            this.SNR.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.SNR.Name = "SNR";
            this.SNR.Size = new System.Drawing.Size(146, 24);
            this.SNR.TabIndex = 12;
            this.SNR.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Location = new System.Drawing.Point(556, 197);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "SNR(dB)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label6.Location = new System.Drawing.Point(556, 222);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 17);
            this.label6.TabIndex = 13;
            this.label6.Text = "NoiseType";
            // 
            // NoiseSelect
            // 
            this.NoiseSelect.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.NoiseSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NoiseSelect.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoiseSelect.FormattingEnabled = true;
            this.NoiseSelect.Items.AddRange(new object[] {
            "None",
            "UniformWhiteNoise"});
            this.NoiseSelect.Location = new System.Drawing.Point(704, 222);
            this.NoiseSelect.Margin = new System.Windows.Forms.Padding(2);
            this.NoiseSelect.Name = "NoiseSelect";
            this.NoiseSelect.Size = new System.Drawing.Size(146, 22);
            this.NoiseSelect.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(870, 410);
            this.Controls.Add(this.NoiseSelect);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.SNR);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Initialphase);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Amplitude);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Frequency);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SamplingRate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.FunctionSelect);
            this.Controls.Add(this.fig1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Basic Function Generator Example";
            ((System.ComponentModel.ISupportInitialize)(this.SamplingRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Frequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amplitude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Initialphase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SNR)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SeeSharpTools.JY.GUI.EasyChart fig1;
        private System.Windows.Forms.ComboBox FunctionSelect;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown SamplingRate;
        private System.Windows.Forms.NumericUpDown Frequency;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown Amplitude;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown Initialphase;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown SNR;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox NoiseSelect;
    }
}

