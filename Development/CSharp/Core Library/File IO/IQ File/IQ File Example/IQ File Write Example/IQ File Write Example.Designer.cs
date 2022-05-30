namespace IQ_File_Write_Example
{
    partial class Form1
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
            SeeSharpTools.JY.GUI.EasyChartXSeries easyChartXSeries7 = new SeeSharpTools.JY.GUI.EasyChartXSeries();
            SeeSharpTools.JY.GUI.EasyChartXSeries easyChartXSeries8 = new SeeSharpTools.JY.GUI.EasyChartXSeries();
            this.easyChartXTimeWavefrom = new SeeSharpTools.JY.GUI.EasyChartX();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonSaveBinFile = new System.Windows.Forms.Button();
            this.buttonSaveVector = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownSampleRate = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCenterFreq = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownSignalLength = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownSignalFreqOffset = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownNoiseLevel = new System.Windows.Forms.NumericUpDown();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSampleRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCenterFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSignalLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSignalFreqOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNoiseLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // easyChartXTimeWavefrom
            // 
            this.easyChartXTimeWavefrom.AutoClear = true;
            this.easyChartXTimeWavefrom.AxisX.AutoScale = true;
            this.easyChartXTimeWavefrom.AxisX.AutoZoomReset = false;
            this.easyChartXTimeWavefrom.AxisX.Color = System.Drawing.Color.Black;
            this.easyChartXTimeWavefrom.AxisX.InitWithScaleView = false;
            this.easyChartXTimeWavefrom.AxisX.IsLogarithmic = false;
            this.easyChartXTimeWavefrom.AxisX.LabelAngle = 0;
            this.easyChartXTimeWavefrom.AxisX.LabelEnabled = true;
            this.easyChartXTimeWavefrom.AxisX.LabelFormat = null;
            this.easyChartXTimeWavefrom.AxisX.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXTimeWavefrom.AxisX.MajorGridCount = -1;
            this.easyChartXTimeWavefrom.AxisX.MajorGridEnabled = true;
            this.easyChartXTimeWavefrom.AxisX.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXTimeWavefrom.AxisX.MaxGridCountPerPixel = 0.012D;
            this.easyChartXTimeWavefrom.AxisX.Maximum = 1000D;
            this.easyChartXTimeWavefrom.AxisX.MinGridCountPerPixel = 0.004D;
            this.easyChartXTimeWavefrom.AxisX.Minimum = 0D;
            this.easyChartXTimeWavefrom.AxisX.MinorGridColor = System.Drawing.Color.DimGray;
            this.easyChartXTimeWavefrom.AxisX.MinorGridEnabled = false;
            this.easyChartXTimeWavefrom.AxisX.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXTimeWavefrom.AxisX.TickWidth = 1F;
            this.easyChartXTimeWavefrom.AxisX.Title = "";
            this.easyChartXTimeWavefrom.AxisX.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXTimeWavefrom.AxisX.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXTimeWavefrom.AxisX.ViewMaximum = 1000D;
            this.easyChartXTimeWavefrom.AxisX.ViewMinimum = 0D;
            this.easyChartXTimeWavefrom.AxisX2.AutoScale = true;
            this.easyChartXTimeWavefrom.AxisX2.AutoZoomReset = false;
            this.easyChartXTimeWavefrom.AxisX2.Color = System.Drawing.Color.Black;
            this.easyChartXTimeWavefrom.AxisX2.InitWithScaleView = false;
            this.easyChartXTimeWavefrom.AxisX2.IsLogarithmic = false;
            this.easyChartXTimeWavefrom.AxisX2.LabelAngle = 0;
            this.easyChartXTimeWavefrom.AxisX2.LabelEnabled = true;
            this.easyChartXTimeWavefrom.AxisX2.LabelFormat = null;
            this.easyChartXTimeWavefrom.AxisX2.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXTimeWavefrom.AxisX2.MajorGridCount = -1;
            this.easyChartXTimeWavefrom.AxisX2.MajorGridEnabled = true;
            this.easyChartXTimeWavefrom.AxisX2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXTimeWavefrom.AxisX2.MaxGridCountPerPixel = 0.012D;
            this.easyChartXTimeWavefrom.AxisX2.Maximum = 1000D;
            this.easyChartXTimeWavefrom.AxisX2.MinGridCountPerPixel = 0.004D;
            this.easyChartXTimeWavefrom.AxisX2.Minimum = 0D;
            this.easyChartXTimeWavefrom.AxisX2.MinorGridColor = System.Drawing.Color.DimGray;
            this.easyChartXTimeWavefrom.AxisX2.MinorGridEnabled = false;
            this.easyChartXTimeWavefrom.AxisX2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXTimeWavefrom.AxisX2.TickWidth = 1F;
            this.easyChartXTimeWavefrom.AxisX2.Title = "";
            this.easyChartXTimeWavefrom.AxisX2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXTimeWavefrom.AxisX2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXTimeWavefrom.AxisX2.ViewMaximum = 1000D;
            this.easyChartXTimeWavefrom.AxisX2.ViewMinimum = 0D;
            this.easyChartXTimeWavefrom.AxisY.AutoScale = true;
            this.easyChartXTimeWavefrom.AxisY.AutoZoomReset = false;
            this.easyChartXTimeWavefrom.AxisY.Color = System.Drawing.Color.Black;
            this.easyChartXTimeWavefrom.AxisY.InitWithScaleView = false;
            this.easyChartXTimeWavefrom.AxisY.IsLogarithmic = false;
            this.easyChartXTimeWavefrom.AxisY.LabelAngle = 0;
            this.easyChartXTimeWavefrom.AxisY.LabelEnabled = true;
            this.easyChartXTimeWavefrom.AxisY.LabelFormat = null;
            this.easyChartXTimeWavefrom.AxisY.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXTimeWavefrom.AxisY.MajorGridCount = 6;
            this.easyChartXTimeWavefrom.AxisY.MajorGridEnabled = true;
            this.easyChartXTimeWavefrom.AxisY.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXTimeWavefrom.AxisY.MaxGridCountPerPixel = 0.012D;
            this.easyChartXTimeWavefrom.AxisY.Maximum = 3.5D;
            this.easyChartXTimeWavefrom.AxisY.MinGridCountPerPixel = 0.004D;
            this.easyChartXTimeWavefrom.AxisY.Minimum = 0.5D;
            this.easyChartXTimeWavefrom.AxisY.MinorGridColor = System.Drawing.Color.DimGray;
            this.easyChartXTimeWavefrom.AxisY.MinorGridEnabled = false;
            this.easyChartXTimeWavefrom.AxisY.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXTimeWavefrom.AxisY.TickWidth = 1F;
            this.easyChartXTimeWavefrom.AxisY.Title = "";
            this.easyChartXTimeWavefrom.AxisY.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXTimeWavefrom.AxisY.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXTimeWavefrom.AxisY.ViewMaximum = 3.5D;
            this.easyChartXTimeWavefrom.AxisY.ViewMinimum = 0.5D;
            this.easyChartXTimeWavefrom.AxisY2.AutoScale = true;
            this.easyChartXTimeWavefrom.AxisY2.AutoZoomReset = false;
            this.easyChartXTimeWavefrom.AxisY2.Color = System.Drawing.Color.Black;
            this.easyChartXTimeWavefrom.AxisY2.InitWithScaleView = false;
            this.easyChartXTimeWavefrom.AxisY2.IsLogarithmic = false;
            this.easyChartXTimeWavefrom.AxisY2.LabelAngle = 0;
            this.easyChartXTimeWavefrom.AxisY2.LabelEnabled = true;
            this.easyChartXTimeWavefrom.AxisY2.LabelFormat = null;
            this.easyChartXTimeWavefrom.AxisY2.MajorGridColor = System.Drawing.Color.Black;
            this.easyChartXTimeWavefrom.AxisY2.MajorGridCount = 6;
            this.easyChartXTimeWavefrom.AxisY2.MajorGridEnabled = true;
            this.easyChartXTimeWavefrom.AxisY2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this.easyChartXTimeWavefrom.AxisY2.MaxGridCountPerPixel = 0.012D;
            this.easyChartXTimeWavefrom.AxisY2.Maximum = 3.5D;
            this.easyChartXTimeWavefrom.AxisY2.MinGridCountPerPixel = 0.004D;
            this.easyChartXTimeWavefrom.AxisY2.Minimum = 0.5D;
            this.easyChartXTimeWavefrom.AxisY2.MinorGridColor = System.Drawing.Color.DimGray;
            this.easyChartXTimeWavefrom.AxisY2.MinorGridEnabled = false;
            this.easyChartXTimeWavefrom.AxisY2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this.easyChartXTimeWavefrom.AxisY2.TickWidth = 1F;
            this.easyChartXTimeWavefrom.AxisY2.Title = "";
            this.easyChartXTimeWavefrom.AxisY2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this.easyChartXTimeWavefrom.AxisY2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this.easyChartXTimeWavefrom.AxisY2.ViewMaximum = 3.5D;
            this.easyChartXTimeWavefrom.AxisY2.ViewMinimum = 0.5D;
            this.easyChartXTimeWavefrom.BackColor = System.Drawing.Color.White;
            this.easyChartXTimeWavefrom.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChartXTimeWavefrom.Cumulitive = false;
            this.easyChartXTimeWavefrom.GradientStyle = SeeSharpTools.JY.GUI.EasyChartX.ChartGradientStyle.None;
            this.easyChartXTimeWavefrom.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChartXTimeWavefrom.LegendFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.easyChartXTimeWavefrom.LegendForeColor = System.Drawing.Color.Black;
            this.easyChartXTimeWavefrom.LegendVisible = true;
            easyChartXSeries7.Color = System.Drawing.Color.Red;
            easyChartXSeries7.Marker = SeeSharpTools.JY.GUI.EasyChartXSeries.MarkerType.None;
            easyChartXSeries7.Name = "I";
            easyChartXSeries7.Type = SeeSharpTools.JY.GUI.EasyChartXSeries.LineType.FastLine;
            easyChartXSeries7.Visible = true;
            easyChartXSeries7.Width = SeeSharpTools.JY.GUI.EasyChartXSeries.LineWidth.Thin;
            easyChartXSeries7.XPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries7.YPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries8.Color = System.Drawing.Color.Blue;
            easyChartXSeries8.Marker = SeeSharpTools.JY.GUI.EasyChartXSeries.MarkerType.None;
            easyChartXSeries8.Name = "Q";
            easyChartXSeries8.Type = SeeSharpTools.JY.GUI.EasyChartXSeries.LineType.FastLine;
            easyChartXSeries8.Visible = true;
            easyChartXSeries8.Width = SeeSharpTools.JY.GUI.EasyChartXSeries.LineWidth.Thin;
            easyChartXSeries8.XPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries8.YPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            this.easyChartXTimeWavefrom.LineSeries.Add(easyChartXSeries7);
            this.easyChartXTimeWavefrom.LineSeries.Add(easyChartXSeries8);
            this.easyChartXTimeWavefrom.Location = new System.Drawing.Point(12, 12);
            this.easyChartXTimeWavefrom.Miscellaneous.CheckInfinity = false;
            this.easyChartXTimeWavefrom.Miscellaneous.CheckNaN = false;
            this.easyChartXTimeWavefrom.Miscellaneous.CheckNegtiveOrZero = false;
            this.easyChartXTimeWavefrom.Miscellaneous.DataStorage = SeeSharpTools.JY.GUI.DataStorageType.Clone;
            this.easyChartXTimeWavefrom.Miscellaneous.DirectionChartCount = 3;
            this.easyChartXTimeWavefrom.Miscellaneous.Fitting = SeeSharpTools.JY.GUI.EasyChartX.FitType.Range;
            this.easyChartXTimeWavefrom.Miscellaneous.MarkerSize = 7;
            this.easyChartXTimeWavefrom.Miscellaneous.MaxSeriesCount = 32;
            this.easyChartXTimeWavefrom.Miscellaneous.MaxSeriesPointCount = 4000;
            this.easyChartXTimeWavefrom.Miscellaneous.ShowFunctionMenu = true;
            this.easyChartXTimeWavefrom.Miscellaneous.SplitLayoutColumnInterval = 0F;
            this.easyChartXTimeWavefrom.Miscellaneous.SplitLayoutDirection = SeeSharpTools.JY.GUI.EasyChartXUtility.LayoutDirection.LeftToRight;
            this.easyChartXTimeWavefrom.Miscellaneous.SplitLayoutRowInterval = 0F;
            this.easyChartXTimeWavefrom.Miscellaneous.SplitViewAutoLayout = true;
            this.easyChartXTimeWavefrom.Name = "easyChartXTimeWavefrom";
            this.easyChartXTimeWavefrom.SeriesCount = 0;
            this.easyChartXTimeWavefrom.Size = new System.Drawing.Size(731, 269);
            this.easyChartXTimeWavefrom.SplitView = false;
            this.easyChartXTimeWavefrom.TabIndex = 0;
            this.easyChartXTimeWavefrom.XCursor.AutoInterval = true;
            this.easyChartXTimeWavefrom.XCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this.easyChartXTimeWavefrom.XCursor.Interval = 0.001D;
            this.easyChartXTimeWavefrom.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Zoom;
            this.easyChartXTimeWavefrom.XCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.easyChartXTimeWavefrom.XCursor.Value = double.NaN;
            this.easyChartXTimeWavefrom.YCursor.AutoInterval = true;
            this.easyChartXTimeWavefrom.YCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this.easyChartXTimeWavefrom.YCursor.Interval = 0.001D;
            this.easyChartXTimeWavefrom.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Disabled;
            this.easyChartXTimeWavefrom.YCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.easyChartXTimeWavefrom.YCursor.Value = double.NaN;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(749, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Generate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonSaveBinFile
            // 
            this.buttonSaveBinFile.Location = new System.Drawing.Point(749, 57);
            this.buttonSaveBinFile.Name = "buttonSaveBinFile";
            this.buttonSaveBinFile.Size = new System.Drawing.Size(97, 23);
            this.buttonSaveBinFile.TabIndex = 1;
            this.buttonSaveBinFile.Text = "Save Bin";
            this.buttonSaveBinFile.UseVisualStyleBackColor = true;
            this.buttonSaveBinFile.Click += new System.EventHandler(this.buttonSaveBinFile_Click);
            // 
            // buttonSaveVector
            // 
            this.buttonSaveVector.Location = new System.Drawing.Point(749, 102);
            this.buttonSaveVector.Name = "buttonSaveVector";
            this.buttonSaveVector.Size = new System.Drawing.Size(97, 23);
            this.buttonSaveVector.TabIndex = 1;
            this.buttonSaveVector.Text = "Save Vector";
            this.buttonSaveVector.UseVisualStyleBackColor = true;
            this.buttonSaveVector.Click += new System.EventHandler(this.buttonSaveVector_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 284);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sample Rate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 284);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Center Frequency";
            // 
            // numericUpDownSampleRate
            // 
            this.numericUpDownSampleRate.Location = new System.Drawing.Point(12, 299);
            this.numericUpDownSampleRate.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericUpDownSampleRate.Name = "numericUpDownSampleRate";
            this.numericUpDownSampleRate.Size = new System.Drawing.Size(100, 21);
            this.numericUpDownSampleRate.TabIndex = 3;
            this.numericUpDownSampleRate.ThousandsSeparator = true;
            this.numericUpDownSampleRate.Value = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            // 
            // numericUpDownCenterFreq
            // 
            this.numericUpDownCenterFreq.Location = new System.Drawing.Point(122, 299);
            this.numericUpDownCenterFreq.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericUpDownCenterFreq.Name = "numericUpDownCenterFreq";
            this.numericUpDownCenterFreq.Size = new System.Drawing.Size(100, 21);
            this.numericUpDownCenterFreq.TabIndex = 3;
            this.numericUpDownCenterFreq.ThousandsSeparator = true;
            this.numericUpDownCenterFreq.Value = new decimal(new int[] {
            70000000,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(227, 284);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Signal Length";
            // 
            // numericUpDownSignalLength
            // 
            this.numericUpDownSignalLength.Location = new System.Drawing.Point(229, 299);
            this.numericUpDownSignalLength.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownSignalLength.Name = "numericUpDownSignalLength";
            this.numericUpDownSignalLength.Size = new System.Drawing.Size(100, 21);
            this.numericUpDownSignalLength.TabIndex = 3;
            this.numericUpDownSignalLength.ThousandsSeparator = true;
            this.numericUpDownSignalLength.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(333, 284);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "Signal Freq Offset";
            // 
            // numericUpDownSignalFreqOffset
            // 
            this.numericUpDownSignalFreqOffset.Location = new System.Drawing.Point(335, 299);
            this.numericUpDownSignalFreqOffset.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownSignalFreqOffset.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numericUpDownSignalFreqOffset.Name = "numericUpDownSignalFreqOffset";
            this.numericUpDownSignalFreqOffset.Size = new System.Drawing.Size(100, 21);
            this.numericUpDownSignalFreqOffset.TabIndex = 3;
            this.numericUpDownSignalFreqOffset.ThousandsSeparator = true;
            this.numericUpDownSignalFreqOffset.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(451, 284);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "Noise Level";
            // 
            // numericUpDownNoiseLevel
            // 
            this.numericUpDownNoiseLevel.DecimalPlaces = 3;
            this.numericUpDownNoiseLevel.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownNoiseLevel.Location = new System.Drawing.Point(453, 299);
            this.numericUpDownNoiseLevel.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNoiseLevel.Name = "numericUpDownNoiseLevel";
            this.numericUpDownNoiseLevel.Size = new System.Drawing.Size(100, 21);
            this.numericUpDownNoiseLevel.TabIndex = 3;
            this.numericUpDownNoiseLevel.ThousandsSeparator = true;
            this.numericUpDownNoiseLevel.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 331);
            this.Controls.Add(this.numericUpDownNoiseLevel);
            this.Controls.Add(this.numericUpDownSignalFreqOffset);
            this.Controls.Add(this.numericUpDownSignalLength);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDownCenterFreq);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownSampleRate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSaveVector);
            this.Controls.Add(this.buttonSaveBinFile);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.easyChartXTimeWavefrom);
            this.Name = "Form1";
            this.Text = "IQFileWriteExampleForm";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSampleRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCenterFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSignalLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSignalFreqOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNoiseLevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SeeSharpTools.JY.GUI.EasyChartX easyChartXTimeWavefrom;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonSaveBinFile;
        private System.Windows.Forms.Button buttonSaveVector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownSampleRate;
        private System.Windows.Forms.NumericUpDown numericUpDownCenterFreq;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownSignalLength;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownSignalFreqOffset;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownNoiseLevel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

