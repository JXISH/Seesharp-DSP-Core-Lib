namespace WavFileReaderTestPanel
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label labelNumOfSamplesToDisp;
            System.Windows.Forms.Label label1;
            SeeSharpTools.JY.GUI.EasyChartXSeries easyChartXSeries2 = new SeeSharpTools.JY.GUI.EasyChartXSeries();
            this._guiFilePath = new System.Windows.Forms.TextBox();
            this.labelFilePath = new System.Windows.Forms.Label();
            this._guiOpenFileButton = new System.Windows.Forms.Button();
            this._guiSampleInfo = new System.Windows.Forms.TextBox();
            this._guieasyChartX = new SeeSharpTools.JY.GUI.EasyChartX();
            this._guiAudioDisplaySamples = new System.Windows.Forms.NumericUpDown();
            this._guiReadPosition = new SeeSharpTools.JY.GUI.Slide();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            labelNumOfSamplesToDisp = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._guiAudioDisplaySamples)).BeginInit();
            this.SuspendLayout();
            // 
            // labelNumOfSamplesToDisp
            // 
            labelNumOfSamplesToDisp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            labelNumOfSamplesToDisp.AutoSize = true;
            labelNumOfSamplesToDisp.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelNumOfSamplesToDisp.Location = new System.Drawing.Point(15, 424);
            labelNumOfSamplesToDisp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            labelNumOfSamplesToDisp.Name = "labelNumOfSamplesToDisp";
            labelNumOfSamplesToDisp.Size = new System.Drawing.Size(141, 17);
            labelNumOfSamplesToDisp.TabIndex = 17;
            labelNumOfSamplesToDisp.Text = "Num Of ReadSamples";
            // 
            // label1
            // 
            label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(15, 501);
            label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(142, 17);
            label1.TabIndex = 17;
            label1.Text = "ReadSamplesPosition";
            // 
            // _guiFilePath
            // 
            this._guiFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._guiFilePath.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiFilePath.Location = new System.Drawing.Point(85, 14);
            this._guiFilePath.Margin = new System.Windows.Forms.Padding(2);
            this._guiFilePath.Name = "_guiFilePath";
            this._guiFilePath.Size = new System.Drawing.Size(377, 24);
            this._guiFilePath.TabIndex = 7;
            // 
            // labelFilePath
            // 
            this.labelFilePath.AutoSize = true;
            this.labelFilePath.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFilePath.Location = new System.Drawing.Point(7, 16);
            this.labelFilePath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelFilePath.Name = "labelFilePath";
            this.labelFilePath.Size = new System.Drawing.Size(78, 19);
            this.labelFilePath.TabIndex = 8;
            this.labelFilePath.Text = "File Name";
            // 
            // _guiOpenFileButton
            // 
            this._guiOpenFileButton.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiOpenFileButton.Location = new System.Drawing.Point(479, 14);
            this._guiOpenFileButton.Margin = new System.Windows.Forms.Padding(2);
            this._guiOpenFileButton.Name = "_guiOpenFileButton";
            this._guiOpenFileButton.Size = new System.Drawing.Size(80, 28);
            this._guiOpenFileButton.TabIndex = 11;
            this._guiOpenFileButton.Text = "Open File";
            this._guiOpenFileButton.UseVisualStyleBackColor = true;
            this._guiOpenFileButton.Click += new System.EventHandler(this._guiOpenFileButton_Click);
            // 
            // _guiSampleInfo
            // 
            this._guiSampleInfo.Font = new System.Drawing.Font("Cambria", 10.8F);
            this._guiSampleInfo.Location = new System.Drawing.Point(479, 68);
            this._guiSampleInfo.Multiline = true;
            this._guiSampleInfo.Name = "_guiSampleInfo";
            this._guiSampleInfo.Size = new System.Drawing.Size(204, 320);
            this._guiSampleInfo.TabIndex = 13;
            // 
            // _guieasyChartX
            // 
            this._guieasyChartX.AxisX.AutoScale = true;
            this._guieasyChartX.AxisX.AutoZoomReset = false;
            this._guieasyChartX.AxisX.Color = System.Drawing.Color.Black;
            this._guieasyChartX.AxisX.InitWithScaleView = false;
            this._guieasyChartX.AxisX.IsLogarithmic = false;
            this._guieasyChartX.AxisX.LabelAngle = 0;
            this._guieasyChartX.AxisX.LabelEnabled = true;
            this._guieasyChartX.AxisX.LabelFormat = null;
            this._guieasyChartX.AxisX.MajorGridColor = System.Drawing.Color.Black;
            this._guieasyChartX.AxisX.MajorGridCount = -1;
            this._guieasyChartX.AxisX.MajorGridEnabled = true;
            this._guieasyChartX.AxisX.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this._guieasyChartX.AxisX.Maximum = 1000D;
            this._guieasyChartX.AxisX.Minimum = 0D;
            this._guieasyChartX.AxisX.MinorGridColor = System.Drawing.Color.Black;
            this._guieasyChartX.AxisX.MinorGridEnabled = false;
            this._guieasyChartX.AxisX.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this._guieasyChartX.AxisX.TickWidth = 1F;
            this._guieasyChartX.AxisX.Title = "";
            this._guieasyChartX.AxisX.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this._guieasyChartX.AxisX.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this._guieasyChartX.AxisX.ViewMaximum = 1000D;
            this._guieasyChartX.AxisX.ViewMinimum = 0D;
            this._guieasyChartX.AxisX2.AutoScale = true;
            this._guieasyChartX.AxisX2.AutoZoomReset = false;
            this._guieasyChartX.AxisX2.Color = System.Drawing.Color.Black;
            this._guieasyChartX.AxisX2.InitWithScaleView = false;
            this._guieasyChartX.AxisX2.IsLogarithmic = false;
            this._guieasyChartX.AxisX2.LabelAngle = 0;
            this._guieasyChartX.AxisX2.LabelEnabled = true;
            this._guieasyChartX.AxisX2.LabelFormat = null;
            this._guieasyChartX.AxisX2.MajorGridColor = System.Drawing.Color.Black;
            this._guieasyChartX.AxisX2.MajorGridCount = -1;
            this._guieasyChartX.AxisX2.MajorGridEnabled = true;
            this._guieasyChartX.AxisX2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this._guieasyChartX.AxisX2.Maximum = 1000D;
            this._guieasyChartX.AxisX2.Minimum = 0D;
            this._guieasyChartX.AxisX2.MinorGridColor = System.Drawing.Color.Black;
            this._guieasyChartX.AxisX2.MinorGridEnabled = false;
            this._guieasyChartX.AxisX2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this._guieasyChartX.AxisX2.TickWidth = 1F;
            this._guieasyChartX.AxisX2.Title = "";
            this._guieasyChartX.AxisX2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this._guieasyChartX.AxisX2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this._guieasyChartX.AxisX2.ViewMaximum = 1000D;
            this._guieasyChartX.AxisX2.ViewMinimum = 0D;
            this._guieasyChartX.AxisY.AutoScale = true;
            this._guieasyChartX.AxisY.AutoZoomReset = false;
            this._guieasyChartX.AxisY.Color = System.Drawing.Color.Black;
            this._guieasyChartX.AxisY.InitWithScaleView = false;
            this._guieasyChartX.AxisY.IsLogarithmic = false;
            this._guieasyChartX.AxisY.LabelAngle = 0;
            this._guieasyChartX.AxisY.LabelEnabled = true;
            this._guieasyChartX.AxisY.LabelFormat = null;
            this._guieasyChartX.AxisY.MajorGridColor = System.Drawing.Color.Black;
            this._guieasyChartX.AxisY.MajorGridCount = 6;
            this._guieasyChartX.AxisY.MajorGridEnabled = true;
            this._guieasyChartX.AxisY.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this._guieasyChartX.AxisY.Maximum = 3.5D;
            this._guieasyChartX.AxisY.Minimum = 0.5D;
            this._guieasyChartX.AxisY.MinorGridColor = System.Drawing.Color.Black;
            this._guieasyChartX.AxisY.MinorGridEnabled = false;
            this._guieasyChartX.AxisY.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this._guieasyChartX.AxisY.TickWidth = 1F;
            this._guieasyChartX.AxisY.Title = "";
            this._guieasyChartX.AxisY.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this._guieasyChartX.AxisY.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this._guieasyChartX.AxisY.ViewMaximum = 3.5D;
            this._guieasyChartX.AxisY.ViewMinimum = 0.5D;
            this._guieasyChartX.AxisY2.AutoScale = true;
            this._guieasyChartX.AxisY2.AutoZoomReset = false;
            this._guieasyChartX.AxisY2.Color = System.Drawing.Color.Black;
            this._guieasyChartX.AxisY2.InitWithScaleView = false;
            this._guieasyChartX.AxisY2.IsLogarithmic = false;
            this._guieasyChartX.AxisY2.LabelAngle = 0;
            this._guieasyChartX.AxisY2.LabelEnabled = true;
            this._guieasyChartX.AxisY2.LabelFormat = null;
            this._guieasyChartX.AxisY2.MajorGridColor = System.Drawing.Color.Black;
            this._guieasyChartX.AxisY2.MajorGridCount = 6;
            this._guieasyChartX.AxisY2.MajorGridEnabled = true;
            this._guieasyChartX.AxisY2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this._guieasyChartX.AxisY2.Maximum = 3.5D;
            this._guieasyChartX.AxisY2.Minimum = 0.5D;
            this._guieasyChartX.AxisY2.MinorGridColor = System.Drawing.Color.Black;
            this._guieasyChartX.AxisY2.MinorGridEnabled = false;
            this._guieasyChartX.AxisY2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this._guieasyChartX.AxisY2.TickWidth = 1F;
            this._guieasyChartX.AxisY2.Title = "";
            this._guieasyChartX.AxisY2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this._guieasyChartX.AxisY2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this._guieasyChartX.AxisY2.ViewMaximum = 3.5D;
            this._guieasyChartX.AxisY2.ViewMinimum = 0.5D;
            this._guieasyChartX.BackColor = System.Drawing.Color.White;
            this._guieasyChartX.ChartAreaBackColor = System.Drawing.Color.Empty;
            this._guieasyChartX.Cumulitive = false;
            this._guieasyChartX.GradientStyle = SeeSharpTools.JY.GUI.EasyChartX.ChartGradientStyle.None;
            this._guieasyChartX.LegendBackColor = System.Drawing.Color.Transparent;
            this._guieasyChartX.LegendFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this._guieasyChartX.LegendForeColor = System.Drawing.Color.Black;
            this._guieasyChartX.LegendVisible = true;
            easyChartXSeries2.Color = System.Drawing.Color.Red;
            easyChartXSeries2.Marker = SeeSharpTools.JY.GUI.EasyChartXSeries.MarkerType.None;
            easyChartXSeries2.Name = "Series1";
            easyChartXSeries2.Type = SeeSharpTools.JY.GUI.EasyChartXSeries.LineType.FastLine;
            easyChartXSeries2.Visible = true;
            easyChartXSeries2.Width = SeeSharpTools.JY.GUI.EasyChartXSeries.LineWidth.Thin;
            easyChartXSeries2.XPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries2.YPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            this._guieasyChartX.LineSeries.Add(easyChartXSeries2);
            this._guieasyChartX.Location = new System.Drawing.Point(12, 68);
            this._guieasyChartX.Miscellaneous.CheckInfinity = false;
            this._guieasyChartX.Miscellaneous.CheckNaN = false;
            this._guieasyChartX.Miscellaneous.CheckNegtiveOrZero = false;
            this._guieasyChartX.Miscellaneous.DirectionChartCount = 3;
            this._guieasyChartX.Miscellaneous.Fitting = SeeSharpTools.JY.GUI.EasyChartX.FitType.Range;
            this._guieasyChartX.Miscellaneous.MarkerSize = 7;
            this._guieasyChartX.Miscellaneous.MaxSeriesCount = 32;
            this._guieasyChartX.Miscellaneous.MaxSeriesPointCount = 4000;
            this._guieasyChartX.Miscellaneous.ShowFunctionMenu = true;
            this._guieasyChartX.Miscellaneous.SplitLayoutColumnInterval = 0F;
            this._guieasyChartX.Miscellaneous.SplitLayoutDirection = SeeSharpTools.JY.GUI.EasyChartXUtility.LayoutDirection.LeftToRight;
            this._guieasyChartX.Miscellaneous.SplitLayoutRowInterval = 0F;
            this._guieasyChartX.Miscellaneous.SplitViewAutoLayout = true;
            this._guieasyChartX.Name = "_guieasyChartX";
            this._guieasyChartX.SeriesCount = 1;
            this._guieasyChartX.Size = new System.Drawing.Size(450, 320);
            this._guieasyChartX.SplitView = false;
            this._guieasyChartX.TabIndex = 14;
            this._guieasyChartX.XCursor.AutoInterval = true;
            this._guieasyChartX.XCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this._guieasyChartX.XCursor.Interval = 0.001D;
            this._guieasyChartX.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Zoom;
            this._guieasyChartX.XCursor.SelectionColor = System.Drawing.Color.LightGray;
            this._guieasyChartX.XCursor.Value = double.NaN;
            this._guieasyChartX.YCursor.AutoInterval = true;
            this._guieasyChartX.YCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this._guieasyChartX.YCursor.Interval = 0.001D;
            this._guieasyChartX.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Disabled;
            this._guieasyChartX.YCursor.SelectionColor = System.Drawing.Color.LightGray;
            this._guieasyChartX.YCursor.Value = double.NaN;
            // 
            // _guiAudioDisplaySamples
            // 
            this._guiAudioDisplaySamples.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._guiAudioDisplaySamples.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiAudioDisplaySamples.Increment = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this._guiAudioDisplaySamples.Location = new System.Drawing.Point(173, 423);
            this._guiAudioDisplaySamples.Margin = new System.Windows.Forms.Padding(2);
            this._guiAudioDisplaySamples.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this._guiAudioDisplaySamples.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._guiAudioDisplaySamples.Name = "_guiAudioDisplaySamples";
            this._guiAudioDisplaySamples.Size = new System.Drawing.Size(166, 24);
            this._guiAudioDisplaySamples.TabIndex = 16;
            this._guiAudioDisplaySamples.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._guiAudioDisplaySamples.ThousandsSeparator = true;
            this._guiAudioDisplaySamples.Value = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            // 
            // _guiReadPosition
            // 
            this._guiReadPosition.BackColor = System.Drawing.Color.Transparent;
            this._guiReadPosition.Fill = false;
            this._guiReadPosition.FillColor = System.Drawing.Color.Blue;
            this._guiReadPosition.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiReadPosition.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this._guiReadPosition.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
            this._guiReadPosition.LineWidth = 3;
            this._guiReadPosition.Location = new System.Drawing.Point(152, 490);
            this._guiReadPosition.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._guiReadPosition.Max = 10D;
            this._guiReadPosition.Min = 0D;
            this._guiReadPosition.Name = "_guiReadPosition";
            this._guiReadPosition.NumberOfDivisions = 1;
            this._guiReadPosition.Size = new System.Drawing.Size(197, 46);
            this._guiReadPosition.TabIndex = 20;
            this._guiReadPosition.TextDecimals = 3;
            this._guiReadPosition.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this._guiReadPosition.TickWidth = 4;
            this._guiReadPosition.TrackerColor = System.Drawing.Color.DimGray;
            this._guiReadPosition.TrackerSize = new System.Drawing.Size(5, 15);
            this._guiReadPosition.Value = 0D;
            this._guiReadPosition.Valuedecimals = 0;
            this._guiReadPosition.ValueChanged += new SeeSharpTools.JY.GUI.ValueChangedHandler(this._guiReadPosition_ValueChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 400;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 539);
            this.Controls.Add(this._guiReadPosition);
            this.Controls.Add(label1);
            this.Controls.Add(labelNumOfSamplesToDisp);
            this.Controls.Add(this._guiAudioDisplaySamples);
            this.Controls.Add(this._guieasyChartX);
            this.Controls.Add(this._guiSampleInfo);
            this.Controls.Add(this._guiOpenFileButton);
            this.Controls.Add(this._guiFilePath);
            this.Controls.Add(this.labelFilePath);
            this.Name = "MainForm";
            this.Text = "Wav File Reader Test Panel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._guiAudioDisplaySamples)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox _guiFilePath;
        private System.Windows.Forms.Label labelFilePath;
        private System.Windows.Forms.Button _guiOpenFileButton;
        private System.Windows.Forms.TextBox _guiSampleInfo;
        private SeeSharpTools.JY.GUI.EasyChartX _guieasyChartX;
        private System.Windows.Forms.NumericUpDown _guiAudioDisplaySamples;
        private SeeSharpTools.JY.GUI.Slide _guiReadPosition;
        private System.Windows.Forms.Timer timer1;
    }
}

