namespace Harmonic_Analysis_CSV
{
    partial class FormHarmonicAnalysisTest
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
            this.textBoxCSVFilePath = new System.Windows.Forms.TextBox();
            this.buttonAnalysis = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.easyChartXWaveform = new SeeSharpTools.JY.GUI.EasyChartX();
            this.textBoxFundamentalFreq = new System.Windows.Forms.TextBox();
            this.textBoxTHD = new System.Windows.Forms.TextBox();
            this.textBoxHarmonics = new System.Windows.Forms.TextBox();
            this.textBoxSINAD = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericSampleRate = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxTHDPlusNoise = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericSampleRate)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxCSVFilePath
            // 
            this.textBoxCSVFilePath.Location = new System.Drawing.Point(82, 33);
            this.textBoxCSVFilePath.Name = "textBoxCSVFilePath";
            this.textBoxCSVFilePath.Size = new System.Drawing.Size(976, 21);
            this.textBoxCSVFilePath.TabIndex = 0;
            this.textBoxCSVFilePath.Text = "D:\\Git\\SeesharpTools\\JXI\\DSP\\Source\\SeeSharpTools.JXI.DSPTests\\Sine_Noise001.csv";
            // 
            // buttonAnalysis
            // 
            this.buttonAnalysis.Location = new System.Drawing.Point(1064, 31);
            this.buttonAnalysis.Name = "buttonAnalysis";
            this.buttonAnalysis.Size = new System.Drawing.Size(75, 23);
            this.buttonAnalysis.TabIndex = 1;
            this.buttonAnalysis.Text = "Analysis";
            this.buttonAnalysis.UseVisualStyleBackColor = true;
            this.buttonAnalysis.Click += new System.EventHandler(this.buttonAnalysis_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "CSV Path";
            // 
            // easyChartXWaveform
            // 
            this.easyChartXWaveform.AxisX.AutoScale = true;
            this.easyChartXWaveform.AxisX.AutoZoomReset = false;
            this.easyChartXWaveform.AxisX.Color = System.Drawing.Color.Black;
            this.easyChartXWaveform.AxisX.InitWithScaleView = false;
            this.easyChartXWaveform.AxisX.IsLogarithmic = false;
            this.easyChartXWaveform.AxisX.LabelAngle = 0;
            this.easyChartXWaveform.AxisX.LabelEnabled = true;
            this.easyChartXWaveform.AxisX.LabelFormat = null;
            this.easyChartXWaveform.AxisX.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXWaveform.AxisX.MajorGridCount = -1;
            this.easyChartXWaveform.AxisX.MajorGridEnabled = true;
            this.easyChartXWaveform.AxisX.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXWaveform.AxisX.Maximum = 1000D;
            this.easyChartXWaveform.AxisX.Minimum = 0D;
            this.easyChartXWaveform.AxisX.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXWaveform.AxisX.MinorGridEnabled = false;
            this.easyChartXWaveform.AxisX.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXWaveform.AxisX.TickWidth = 1F;
            this.easyChartXWaveform.AxisX.Title = "";
            this.easyChartXWaveform.AxisX.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXWaveform.AxisX.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXWaveform.AxisX.ViewMaximum = 1000D;
            this.easyChartXWaveform.AxisX.ViewMinimum = 0D;
            this.easyChartXWaveform.AxisX2.AutoScale = true;
            this.easyChartXWaveform.AxisX2.AutoZoomReset = false;
            this.easyChartXWaveform.AxisX2.Color = System.Drawing.Color.Black;
            this.easyChartXWaveform.AxisX2.InitWithScaleView = false;
            this.easyChartXWaveform.AxisX2.IsLogarithmic = false;
            this.easyChartXWaveform.AxisX2.LabelAngle = 0;
            this.easyChartXWaveform.AxisX2.LabelEnabled = true;
            this.easyChartXWaveform.AxisX2.LabelFormat = null;
            this.easyChartXWaveform.AxisX2.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXWaveform.AxisX2.MajorGridCount = -1;
            this.easyChartXWaveform.AxisX2.MajorGridEnabled = true;
            this.easyChartXWaveform.AxisX2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXWaveform.AxisX2.Maximum = 1000D;
            this.easyChartXWaveform.AxisX2.Minimum = 0D;
            this.easyChartXWaveform.AxisX2.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXWaveform.AxisX2.MinorGridEnabled = false;
            this.easyChartXWaveform.AxisX2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXWaveform.AxisX2.TickWidth = 1F;
            this.easyChartXWaveform.AxisX2.Title = "";
            this.easyChartXWaveform.AxisX2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXWaveform.AxisX2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXWaveform.AxisX2.ViewMaximum = 1000D;
            this.easyChartXWaveform.AxisX2.ViewMinimum = 0D;
            this.easyChartXWaveform.AxisY.AutoScale = true;
            this.easyChartXWaveform.AxisY.AutoZoomReset = false;
            this.easyChartXWaveform.AxisY.Color = System.Drawing.Color.Black;
            this.easyChartXWaveform.AxisY.InitWithScaleView = false;
            this.easyChartXWaveform.AxisY.IsLogarithmic = false;
            this.easyChartXWaveform.AxisY.LabelAngle = 0;
            this.easyChartXWaveform.AxisY.LabelEnabled = true;
            this.easyChartXWaveform.AxisY.LabelFormat = null;
            this.easyChartXWaveform.AxisY.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXWaveform.AxisY.MajorGridCount = 6;
            this.easyChartXWaveform.AxisY.MajorGridEnabled = true;
            this.easyChartXWaveform.AxisY.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXWaveform.AxisY.Maximum = 3.5D;
            this.easyChartXWaveform.AxisY.Minimum = 0.5D;
            this.easyChartXWaveform.AxisY.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXWaveform.AxisY.MinorGridEnabled = false;
            this.easyChartXWaveform.AxisY.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXWaveform.AxisY.TickWidth = 1F;
            this.easyChartXWaveform.AxisY.Title = "";
            this.easyChartXWaveform.AxisY.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXWaveform.AxisY.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXWaveform.AxisY.ViewMaximum = 3.5D;
            this.easyChartXWaveform.AxisY.ViewMinimum = 0.5D;
            this.easyChartXWaveform.AxisY2.AutoScale = true;
            this.easyChartXWaveform.AxisY2.AutoZoomReset = false;
            this.easyChartXWaveform.AxisY2.Color = System.Drawing.Color.Black;
            this.easyChartXWaveform.AxisY2.InitWithScaleView = false;
            this.easyChartXWaveform.AxisY2.IsLogarithmic = false;
            this.easyChartXWaveform.AxisY2.LabelAngle = 0;
            this.easyChartXWaveform.AxisY2.LabelEnabled = true;
            this.easyChartXWaveform.AxisY2.LabelFormat = null;
            this.easyChartXWaveform.AxisY2.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXWaveform.AxisY2.MajorGridCount = 6;
            this.easyChartXWaveform.AxisY2.MajorGridEnabled = true;
            this.easyChartXWaveform.AxisY2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXWaveform.AxisY2.Maximum = 3.5D;
            this.easyChartXWaveform.AxisY2.Minimum = 0.5D;
            this.easyChartXWaveform.AxisY2.MinorGridColor = System.Drawing.Color.Black;
            this.easyChartXWaveform.AxisY2.MinorGridEnabled = false;
            this.easyChartXWaveform.AxisY2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXWaveform.AxisY2.TickWidth = 1F;
            this.easyChartXWaveform.AxisY2.Title = "";
            this.easyChartXWaveform.AxisY2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXWaveform.AxisY2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXWaveform.AxisY2.ViewMaximum = 3.5D;
            this.easyChartXWaveform.AxisY2.ViewMinimum = 0.5D;
            this.easyChartXWaveform.BackColor = System.Drawing.Color.White;
            this.easyChartXWaveform.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartXWaveform.Cumulitive = false;
            this.easyChartXWaveform.GradientStyle = SeeSharpTools.JY.GUI.EasyChartX.ChartGradientStyle.None;
            this.easyChartXWaveform.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartXWaveform.LegendFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.easyChartXWaveform.LegendForeColor = System.Drawing.Color.Black;
            this.easyChartXWaveform.LegendVisible = true;
            easyChartXSeries1.Color = System.Drawing.Color.Red;
            easyChartXSeries1.Marker = SeeSharpTools.JY.GUI.EasyChartXSeries.MarkerType.None;
            easyChartXSeries1.Name = "Series1";
            easyChartXSeries1.Type = SeeSharpTools.JY.GUI.EasyChartXSeries.LineType.FastLine;
            easyChartXSeries1.Visible = true;
            easyChartXSeries1.Width = SeeSharpTools.JY.GUI.EasyChartXSeries.LineWidth.Thin;
            easyChartXSeries1.XPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries1.YPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            this.easyChartXWaveform.LineSeries.Add(easyChartXSeries1);
            this.easyChartXWaveform.Location = new System.Drawing.Point(25, 82);
            this.easyChartXWaveform.Miscellaneous.CheckInfinity = false;
            this.easyChartXWaveform.Miscellaneous.CheckNaN = false;
            this.easyChartXWaveform.Miscellaneous.CheckNegtiveOrZero = false;
            this.easyChartXWaveform.Miscellaneous.DirectionChartCount = 3;
            this.easyChartXWaveform.Miscellaneous.Fitting = SeeSharpTools.JY.GUI.EasyChartX.FitType.Range;
            this.easyChartXWaveform.Miscellaneous.MarkerSize = 3;
            this.easyChartXWaveform.Miscellaneous.MaxSeriesCount = 32;
            this.easyChartXWaveform.Miscellaneous.MaxSeriesPointCount = 4000;
            this.easyChartXWaveform.Miscellaneous.ShowFunctionMenu = true;
            this.easyChartXWaveform.Miscellaneous.SplitLayoutColumnInterval = 0F;
            this.easyChartXWaveform.Miscellaneous.SplitLayoutDirection = SeeSharpTools.JY.GUI.EasyChartXUtility.LayoutDirection.LeftToRight;
            this.easyChartXWaveform.Miscellaneous.SplitLayoutRowInterval = 0F;
            this.easyChartXWaveform.Miscellaneous.SplitViewAutoLayout = true;
            this.easyChartXWaveform.Name = "easyChartXWaveform";
            this.easyChartXWaveform.SeriesCount = 1;
            this.easyChartXWaveform.Size = new System.Drawing.Size(1032, 224);
            this.easyChartXWaveform.SplitView = false;
            this.easyChartXWaveform.TabIndex = 3;
            this.easyChartXWaveform.XCursor.AutoInterval = true;
            this.easyChartXWaveform.XCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this.easyChartXWaveform.XCursor.Interval = 0.001D;
            this.easyChartXWaveform.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Zoom;
            this.easyChartXWaveform.XCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.easyChartXWaveform.XCursor.Value = double.NaN;
            this.easyChartXWaveform.YCursor.AutoInterval = true;
            this.easyChartXWaveform.YCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this.easyChartXWaveform.YCursor.Interval = 0.001D;
            this.easyChartXWaveform.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Disabled;
            this.easyChartXWaveform.YCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.easyChartXWaveform.YCursor.Value = double.NaN;
            // 
            // textBoxFundamentalFreq
            // 
            this.textBoxFundamentalFreq.Location = new System.Drawing.Point(26, 325);
            this.textBoxFundamentalFreq.Name = "textBoxFundamentalFreq";
            this.textBoxFundamentalFreq.Size = new System.Drawing.Size(100, 21);
            this.textBoxFundamentalFreq.TabIndex = 4;
            // 
            // textBoxTHD
            // 
            this.textBoxTHD.Location = new System.Drawing.Point(165, 325);
            this.textBoxTHD.Name = "textBoxTHD";
            this.textBoxTHD.Size = new System.Drawing.Size(100, 21);
            this.textBoxTHD.TabIndex = 5;
            // 
            // textBoxHarmonics
            // 
            this.textBoxHarmonics.Location = new System.Drawing.Point(301, 325);
            this.textBoxHarmonics.Name = "textBoxHarmonics";
            this.textBoxHarmonics.Size = new System.Drawing.Size(756, 21);
            this.textBoxHarmonics.TabIndex = 6;
            // 
            // textBoxSINAD
            // 
            this.textBoxSINAD.Location = new System.Drawing.Point(25, 369);
            this.textBoxSINAD.Name = "textBoxSINAD";
            this.textBoxSINAD.Size = new System.Drawing.Size(100, 21);
            this.textBoxSINAD.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 310);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Frequency";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(163, 310);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "THD";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(299, 309);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Harmonics";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 354);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "SINAD (dB)";
            // 
            // numericSampleRate
            // 
            this.numericSampleRate.Location = new System.Drawing.Point(1063, 82);
            this.numericSampleRate.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericSampleRate.Name = "numericSampleRate";
            this.numericSampleRate.Size = new System.Drawing.Size(76, 21);
            this.numericSampleRate.TabIndex = 12;
            this.numericSampleRate.Value = new decimal(new int[] {
            51200,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(164, 354);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "THD plus Noise";
            // 
            // textBoxTHDPlusNoise
            // 
            this.textBoxTHDPlusNoise.Location = new System.Drawing.Point(165, 369);
            this.textBoxTHDPlusNoise.Name = "textBoxTHDPlusNoise";
            this.textBoxTHDPlusNoise.Size = new System.Drawing.Size(100, 21);
            this.textBoxTHDPlusNoise.TabIndex = 13;
            // 
            // FormHarmonicAnalysisTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 402);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxTHDPlusNoise);
            this.Controls.Add(this.numericSampleRate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxSINAD);
            this.Controls.Add(this.textBoxHarmonics);
            this.Controls.Add(this.textBoxTHD);
            this.Controls.Add(this.textBoxFundamentalFreq);
            this.Controls.Add(this.easyChartXWaveform);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonAnalysis);
            this.Controls.Add(this.textBoxCSVFilePath);
            this.Name = "FormHarmonicAnalysisTest";
            this.Text = "Harmonic Analysis";
            ((System.ComponentModel.ISupportInitialize)(this.numericSampleRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxCSVFilePath;
        private System.Windows.Forms.Button buttonAnalysis;
        private System.Windows.Forms.Label label1;
        private SeeSharpTools.JY.GUI.EasyChartX easyChartXWaveform;
        private System.Windows.Forms.TextBox textBoxFundamentalFreq;
        private System.Windows.Forms.TextBox textBoxTHD;
        private System.Windows.Forms.TextBox textBoxHarmonics;
        private System.Windows.Forms.TextBox textBoxSINAD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericSampleRate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxTHDPlusNoise;
    }
}

