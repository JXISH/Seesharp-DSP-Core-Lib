namespace Winform_Noise_Generator
{
    partial class MainForm
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
            SeeSharpTools.JY.GUI.EasyChartSeries easyChartSeries3 = new SeeSharpTools.JY.GUI.EasyChartSeries();
            SeeSharpTools.JY.GUI.EasyChartSeries easyChartSeries4 = new SeeSharpTools.JY.GUI.EasyChartSeries();
            this.numericUpDownSigma = new System.Windows.Forms.NumericUpDown();
            this.Sigma = new System.Windows.Forms.Label();
            this.noiseConfiguration = new System.Windows.Forms.GroupBox();
            this.noiseType = new System.Windows.Forms.Label();
            this.domainUpDownNoiseType = new System.Windows.Forms.DomainUpDown();
            this.Mean = new System.Windows.Forms.Label();
            this.numericUpDownMean = new System.Windows.Forms.NumericUpDown();
            this.easyChartNoise = new SeeSharpTools.JY.GUI.EasyChart();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonNoise = new System.Windows.Forms.Button();
            this.easyChartHistogram = new SeeSharpTools.JY.GUI.EasyChart();
            this.groupBoxStatistical = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownBarChartNum = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSigma)).BeginInit();
            this.noiseConfiguration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMean)).BeginInit();
            this.groupBoxStatistical.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBarChartNum)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownSigma
            // 
            this.numericUpDownSigma.Location = new System.Drawing.Point(98, 70);
            this.numericUpDownSigma.Name = "numericUpDownSigma";
            this.numericUpDownSigma.Size = new System.Drawing.Size(120, 21);
            this.numericUpDownSigma.TabIndex = 0;
            this.numericUpDownSigma.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Sigma
            // 
            this.Sigma.AutoSize = true;
            this.Sigma.Font = new System.Drawing.Font("宋体", 9F);
            this.Sigma.Location = new System.Drawing.Point(17, 72);
            this.Sigma.Name = "Sigma";
            this.Sigma.Size = new System.Drawing.Size(41, 12);
            this.Sigma.TabIndex = 1;
            this.Sigma.Text = "Sigma:";
            this.Sigma.Click += new System.EventHandler(this.label1_Click);
            // 
            // noiseConfiguration
            // 
            this.noiseConfiguration.Controls.Add(this.noiseType);
            this.noiseConfiguration.Controls.Add(this.domainUpDownNoiseType);
            this.noiseConfiguration.Controls.Add(this.Mean);
            this.noiseConfiguration.Controls.Add(this.numericUpDownMean);
            this.noiseConfiguration.Controls.Add(this.numericUpDownSigma);
            this.noiseConfiguration.Controls.Add(this.Sigma);
            this.noiseConfiguration.Location = new System.Drawing.Point(514, 63);
            this.noiseConfiguration.Name = "noiseConfiguration";
            this.noiseConfiguration.Size = new System.Drawing.Size(238, 158);
            this.noiseConfiguration.TabIndex = 2;
            this.noiseConfiguration.TabStop = false;
            this.noiseConfiguration.Text = "Noise Configuration";
            this.noiseConfiguration.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // noiseType
            // 
            this.noiseType.AutoSize = true;
            this.noiseType.Font = new System.Drawing.Font("宋体", 9F);
            this.noiseType.Location = new System.Drawing.Point(17, 45);
            this.noiseType.Name = "noiseType";
            this.noiseType.Size = new System.Drawing.Size(71, 12);
            this.noiseType.TabIndex = 6;
            this.noiseType.Text = "Noise Type:";
            // 
            // domainUpDownNoiseType
            // 
            this.domainUpDownNoiseType.Items.Add("Gauss Noise");
            this.domainUpDownNoiseType.Items.Add("White Noise");
            this.domainUpDownNoiseType.Location = new System.Drawing.Point(98, 43);
            this.domainUpDownNoiseType.Name = "domainUpDownNoiseType";
            this.domainUpDownNoiseType.Size = new System.Drawing.Size(120, 21);
            this.domainUpDownNoiseType.TabIndex = 5;
            this.domainUpDownNoiseType.Text = "Gauss Noise";
            // 
            // Mean
            // 
            this.Mean.AutoSize = true;
            this.Mean.Font = new System.Drawing.Font("宋体", 9F);
            this.Mean.Location = new System.Drawing.Point(17, 99);
            this.Mean.Name = "Mean";
            this.Mean.Size = new System.Drawing.Size(35, 12);
            this.Mean.TabIndex = 4;
            this.Mean.Text = "Mean:";
            // 
            // numericUpDownMean
            // 
            this.numericUpDownMean.Location = new System.Drawing.Point(98, 97);
            this.numericUpDownMean.Name = "numericUpDownMean";
            this.numericUpDownMean.Size = new System.Drawing.Size(120, 21);
            this.numericUpDownMean.TabIndex = 3;
            // 
            // easyChartNoise
            // 
            this.easyChartNoise.AxisX.AutoScale = true;
            this.easyChartNoise.AxisX.InitWithScaleView = false;
            this.easyChartNoise.AxisX.LabelEnabled = true;
            this.easyChartNoise.AxisX.LabelFormat = "";
            this.easyChartNoise.AxisX.Maximum = 1001D;
            this.easyChartNoise.AxisX.Minimum = 0D;
            this.easyChartNoise.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartNoise.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartNoise.AxisX.Title = "";
            this.easyChartNoise.AxisX.ViewMaximum = 1001D;
            this.easyChartNoise.AxisX.ViewMinimum = 0D;
            this.easyChartNoise.AxisY.AutoScale = true;
            this.easyChartNoise.AxisY.InitWithScaleView = false;
            this.easyChartNoise.AxisY.LabelEnabled = true;
            this.easyChartNoise.AxisY.LabelFormat = "";
            this.easyChartNoise.AxisY.Maximum = 3.5D;
            this.easyChartNoise.AxisY.Minimum = 0D;
            this.easyChartNoise.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartNoise.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartNoise.AxisY.Title = "";
            this.easyChartNoise.AxisY.ViewMaximum = 3.5D;
            this.easyChartNoise.AxisY.ViewMinimum = 0D;
            this.easyChartNoise.AxisYMax = 3.5D;
            this.easyChartNoise.AxisYMin = 0D;
            this.easyChartNoise.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartNoise.EasyChartBackColor = System.Drawing.Color.White;
            this.easyChartNoise.FixAxisX = false;
            this.easyChartNoise.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.easyChartNoise.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartNoise.LegendVisible = true;
            easyChartSeries3.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries3.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries3.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.easyChartNoise.LineSeries.Add(easyChartSeries3);
            this.easyChartNoise.Location = new System.Drawing.Point(20, 37);
            this.easyChartNoise.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartNoise.MajorGridEnabled = true;
            this.easyChartNoise.Margin = new System.Windows.Forms.Padding(2);
            this.easyChartNoise.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartNoise.MinorGridEnabled = false;
            this.easyChartNoise.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.easyChartNoise.Name = "easyChartNoise";
            this.easyChartNoise.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.easyChartNoise.SeriesNames = new string[] {
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
            this.easyChartNoise.Size = new System.Drawing.Size(460, 165);
            this.easyChartNoise.TabIndex = 11;
            this.easyChartNoise.XAxisLogarithmic = false;
            this.easyChartNoise.XAxisTitle = "";
            this.easyChartNoise.XCursor.AutoInterval = true;
            this.easyChartNoise.XCursor.Color = System.Drawing.Color.Red;
            this.easyChartNoise.XCursor.Interval = 1D;
            this.easyChartNoise.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.easyChartNoise.XCursor.Value = double.NaN;
            this.easyChartNoise.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartNoise.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartNoise.YAutoEnable = true;
            this.easyChartNoise.YAxisLogarithmic = false;
            this.easyChartNoise.YAxisTitle = "";
            this.easyChartNoise.YCursor.AutoInterval = true;
            this.easyChartNoise.YCursor.Color = System.Drawing.Color.Red;
            this.easyChartNoise.YCursor.Interval = 0.001D;
            this.easyChartNoise.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.easyChartNoise.YCursor.Value = double.NaN;
            this.easyChartNoise.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartNoise.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartNoise.AxisViewChanged += new SeeSharpTools.JY.GUI.EasyChart.ViewEvents(this.easyChartGaussNoise_AxisViewChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "Gauss Noise";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "Statistical characteristics";
            // 
            // buttonNoise
            // 
            this.buttonNoise.Location = new System.Drawing.Point(514, 339);
            this.buttonNoise.Name = "buttonNoise";
            this.buttonNoise.Size = new System.Drawing.Size(238, 35);
            this.buttonNoise.TabIndex = 15;
            this.buttonNoise.Text = "Generate Noise and Histogram";
            this.buttonNoise.UseVisualStyleBackColor = true;
            this.buttonNoise.Click += new System.EventHandler(this.buttonGaussNoise_Click);
            // 
            // easyChartHistogram
            // 
            this.easyChartHistogram.AxisX.AutoScale = true;
            this.easyChartHistogram.AxisX.InitWithScaleView = false;
            this.easyChartHistogram.AxisX.LabelEnabled = true;
            this.easyChartHistogram.AxisX.LabelFormat = "";
            this.easyChartHistogram.AxisX.Maximum = 1001D;
            this.easyChartHistogram.AxisX.Minimum = 0D;
            this.easyChartHistogram.AxisX.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartHistogram.AxisX.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartHistogram.AxisX.Title = "";
            this.easyChartHistogram.AxisX.ViewMaximum = 1001D;
            this.easyChartHistogram.AxisX.ViewMinimum = 0D;
            this.easyChartHistogram.AxisY.AutoScale = true;
            this.easyChartHistogram.AxisY.InitWithScaleView = false;
            this.easyChartHistogram.AxisY.LabelEnabled = true;
            this.easyChartHistogram.AxisY.LabelFormat = "";
            this.easyChartHistogram.AxisY.Maximum = 3.5D;
            this.easyChartHistogram.AxisY.Minimum = 0D;
            this.easyChartHistogram.AxisY.Orientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartHistogram.AxisY.Position = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartHistogram.AxisY.Title = "";
            this.easyChartHistogram.AxisY.ViewMaximum = 3.5D;
            this.easyChartHistogram.AxisY.ViewMinimum = 0D;
            this.easyChartHistogram.AxisYMax = 3.5D;
            this.easyChartHistogram.AxisYMin = 0D;
            this.easyChartHistogram.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartHistogram.EasyChartBackColor = System.Drawing.Color.White;
            this.easyChartHistogram.FixAxisX = false;
            this.easyChartHistogram.GradientStyle = SeeSharpTools.JY.GUI.EasyChart.EasyChartGradientStyle.None;
            this.easyChartHistogram.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartHistogram.LegendVisible = true;
            easyChartSeries4.InterpolationStyle = SeeSharpTools.JY.GUI.EasyChartSeries.Interpolation.FastLine;
            easyChartSeries4.MarkerType = SeeSharpTools.JY.GUI.EasyChartSeries.PointStyle.None;
            easyChartSeries4.Width = SeeSharpTools.JY.GUI.EasyChartSeries.LineWidth.Thin;
            this.easyChartHistogram.LineSeries.Add(easyChartSeries4);
            this.easyChartHistogram.Location = new System.Drawing.Point(20, 242);
            this.easyChartHistogram.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartHistogram.MajorGridEnabled = true;
            this.easyChartHistogram.Margin = new System.Windows.Forms.Padding(2);
            this.easyChartHistogram.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartHistogram.MinorGridEnabled = false;
            this.easyChartHistogram.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.easyChartHistogram.Name = "easyChartHistogram";
            this.easyChartHistogram.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.easyChartHistogram.SeriesNames = new string[] {
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
            this.easyChartHistogram.Size = new System.Drawing.Size(460, 183);
            this.easyChartHistogram.TabIndex = 17;
            this.easyChartHistogram.XAxisLogarithmic = false;
            this.easyChartHistogram.XAxisTitle = "";
            this.easyChartHistogram.XCursor.AutoInterval = true;
            this.easyChartHistogram.XCursor.Color = System.Drawing.Color.Red;
            this.easyChartHistogram.XCursor.Interval = 1D;
            this.easyChartHistogram.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Zoom;
            this.easyChartHistogram.XCursor.Value = double.NaN;
            this.easyChartHistogram.XTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartHistogram.XTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            this.easyChartHistogram.YAutoEnable = true;
            this.easyChartHistogram.YAxisLogarithmic = false;
            this.easyChartHistogram.YAxisTitle = "";
            this.easyChartHistogram.YCursor.AutoInterval = true;
            this.easyChartHistogram.YCursor.Color = System.Drawing.Color.Red;
            this.easyChartHistogram.YCursor.Interval = 0.001D;
            this.easyChartHistogram.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartCursor.CursorMode.Disabled;
            this.easyChartHistogram.YCursor.Value = double.NaN;
            this.easyChartHistogram.YTitleOrientation = SeeSharpTools.JY.GUI.EasyChart.TitleOrientation.Auto;
            this.easyChartHistogram.YTitlePosition = SeeSharpTools.JY.GUI.EasyChart.TitlePosition.Center;
            // 
            // groupBoxStatistical
            // 
            this.groupBoxStatistical.Controls.Add(this.label4);
            this.groupBoxStatistical.Controls.Add(this.numericUpDownBarChartNum);
            this.groupBoxStatistical.Location = new System.Drawing.Point(514, 242);
            this.groupBoxStatistical.Name = "groupBoxStatistical";
            this.groupBoxStatistical.Size = new System.Drawing.Size(238, 68);
            this.groupBoxStatistical.TabIndex = 7;
            this.groupBoxStatistical.TabStop = false;
            this.groupBoxStatistical.Text = "Statistical Configuration";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F);
            this.label4.Location = new System.Drawing.Point(17, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "Bar Chart Number:";
            // 
            // numericUpDownBarChartNum
            // 
            this.numericUpDownBarChartNum.Location = new System.Drawing.Point(142, 29);
            this.numericUpDownBarChartNum.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDownBarChartNum.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownBarChartNum.Name = "numericUpDownBarChartNum";
            this.numericUpDownBarChartNum.Size = new System.Drawing.Size(76, 21);
            this.numericUpDownBarChartNum.TabIndex = 3;
            this.numericUpDownBarChartNum.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownBarChartNum.ValueChanged += new System.EventHandler(this.numericUpDownBarChartNum_ValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBoxStatistical);
            this.Controls.Add(this.easyChartHistogram);
            this.Controls.Add(this.buttonNoise);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.easyChartNoise);
            this.Controls.Add(this.noiseConfiguration);
            this.Name = "MainForm";
            this.Text = "Noise Generator";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSigma)).EndInit();
            this.noiseConfiguration.ResumeLayout(false);
            this.noiseConfiguration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMean)).EndInit();
            this.groupBoxStatistical.ResumeLayout(false);
            this.groupBoxStatistical.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBarChartNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownSigma;
        private System.Windows.Forms.Label Sigma;
        private System.Windows.Forms.GroupBox noiseConfiguration;
        private System.Windows.Forms.NumericUpDown numericUpDownMean;
        private System.Windows.Forms.Label Mean;
        private SeeSharpTools.JY.GUI.EasyChart easyChartNoise;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonNoise;
        private System.Windows.Forms.Label noiseType;
        private System.Windows.Forms.DomainUpDown domainUpDownNoiseType;
        private SeeSharpTools.JY.GUI.EasyChart easyChartHistogram;
        private System.Windows.Forms.GroupBox groupBoxStatistical;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownBarChartNum;
    }
}