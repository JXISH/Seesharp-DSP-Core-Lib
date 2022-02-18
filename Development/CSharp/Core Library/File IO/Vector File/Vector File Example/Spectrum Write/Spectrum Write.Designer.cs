namespace SpectrumFileReadExample
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
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.Label label12;
            System.Windows.Forms.Label label13;
            System.Windows.Forms.Label label14;
            System.Windows.Forms.Label label15;
            System.Windows.Forms.Label label16;
            System.Windows.Forms.Label label17;
            System.Windows.Forms.Label label18;
            System.Windows.Forms.GroupBox _guiSignalDisplay;
            SeeSharpTools.JY.GUI.EasyChartXSeries easyChartXSeries1 = new SeeSharpTools.JY.GUI.EasyChartXSeries();
            this._guiSpectrumChart = new SeeSharpTools.JY.GUI.EasyChartX();
            this.labelTitle = new System.Windows.Forms.Label();
            this._guiSignalParam = new System.Windows.Forms.GroupBox();
            this._guiBand2Enabled = new System.Windows.Forms.CheckBox();
            this._guiSignalBand2Info = new System.Windows.Forms.GroupBox();
            this._guiBand2FreqStop = new System.Windows.Forms.NumericUpDown();
            this._guiBand2NumOfLines = new System.Windows.Forms.NumericUpDown();
            this._guiBand2FreqStep = new System.Windows.Forms.NumericUpDown();
            this._guiBand2FreqStart = new System.Windows.Forms.NumericUpDown();
            this._guiSignalBand1Info = new System.Windows.Forms.GroupBox();
            this._guiBand1FreqStop = new System.Windows.Forms.NumericUpDown();
            this._guiBand1NumOfLines = new System.Windows.Forms.NumericUpDown();
            this._guiBand1FreqStep = new System.Windows.Forms.NumericUpDown();
            this._guiBand1FreqStart = new System.Windows.Forms.NumericUpDown();
            this._guiNumberOfChannels = new System.Windows.Forms.NumericUpDown();
            this._guiFileParam = new System.Windows.Forms.GroupBox();
            this._guiWriteDataType = new System.Windows.Forms.ComboBox();
            this._guiBrowseFolder = new System.Windows.Forms.Button();
            this._guiDestinationFolder = new System.Windows.Forms.TextBox();
            this._guiFileLength = new System.Windows.Forms.NumericUpDown();
            this._guiProgressValue = new System.Windows.Forms.TextBox();
            this._guiProgressBar = new System.Windows.Forms.ProgressBar();
            this._guiStop = new System.Windows.Forms.Button();
            this._guiStart = new System.Windows.Forms.Button();
            this._bgWorker = new System.ComponentModel.BackgroundWorker();
            this._guiFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            label3 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            label14 = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            label16 = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            label18 = new System.Windows.Forms.Label();
            _guiSignalDisplay = new System.Windows.Forms.GroupBox();
            _guiSignalDisplay.SuspendLayout();
            this._guiSignalParam.SuspendLayout();
            this._guiSignalBand2Info.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._guiBand2FreqStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiBand2NumOfLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiBand2FreqStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiBand2FreqStart)).BeginInit();
            this._guiSignalBand1Info.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._guiBand1FreqStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiBand1NumOfLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiBand1FreqStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiBand1FreqStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiNumberOfChannels)).BeginInit();
            this._guiFileParam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._guiFileLength)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.Location = new System.Drawing.Point(12, 29);
            label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(223, 28);
            label3.TabIndex = 7;
            label3.Text = "Number Of Channels";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(11, 41);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(115, 28);
            label1.TabIndex = 9;
            label1.Text = "Freq Start";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(199, 41);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(59, 28);
            label2.TabIndex = 9;
            label2.Text = "MHz";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(11, 73);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(111, 28);
            label4.TabIndex = 9;
            label4.Text = "Freq Stop";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(199, 73);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(59, 28);
            label5.TabIndex = 9;
            label5.Text = "MHz";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(11, 105);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(110, 28);
            label6.TabIndex = 9;
            label6.Text = "Freq Step";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(199, 105);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(59, 28);
            label7.TabIndex = 9;
            label7.Text = "MHz";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(11, 137);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(115, 28);
            label8.TabIndex = 9;
            label8.Text = "# Of Lines";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(199, 73);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(59, 28);
            label9.TabIndex = 9;
            label9.Text = "MHz";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(199, 105);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(59, 28);
            label10.TabIndex = 9;
            label10.Text = "MHz";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(199, 41);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(59, 28);
            label11.TabIndex = 9;
            label11.Text = "MHz";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new System.Drawing.Point(11, 73);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(111, 28);
            label12.TabIndex = 9;
            label12.Text = "Freq Stop";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new System.Drawing.Point(11, 137);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(115, 28);
            label13.TabIndex = 9;
            label13.Text = "# Of Lines";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new System.Drawing.Point(11, 105);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(110, 28);
            label14.TabIndex = 9;
            label14.Text = "Freq Step";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new System.Drawing.Point(11, 41);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(115, 28);
            label15.TabIndex = 9;
            label15.Text = "Freq Start";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label16.Location = new System.Drawing.Point(12, 31);
            label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(131, 28);
            label16.TabIndex = 7;
            label16.Text = "Folder Path";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label17.Location = new System.Drawing.Point(264, 84);
            label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(83, 28);
            label17.TabIndex = 10;
            label17.Text = "frames";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label18.Location = new System.Drawing.Point(12, 84);
            label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(127, 28);
            label18.TabIndex = 7;
            label18.Text = "File Length";
            // 
            // _guiSignalDisplay
            // 
            _guiSignalDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            _guiSignalDisplay.Controls.Add(this._guiSpectrumChart);
            _guiSignalDisplay.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            _guiSignalDisplay.Location = new System.Drawing.Point(7, 322);
            _guiSignalDisplay.Name = "_guiSignalDisplay";
            _guiSignalDisplay.Size = new System.Drawing.Size(760, 181);
            _guiSignalDisplay.TabIndex = 13;
            _guiSignalDisplay.TabStop = false;
            _guiSignalDisplay.Text = "Signal Display";
            // 
            // _guiSpectrumChart
            // 
            this._guiSpectrumChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._guiSpectrumChart.AxisX.AutoScale = true;
            this._guiSpectrumChart.AxisX.AutoZoomReset = false;
            this._guiSpectrumChart.AxisX.Color = System.Drawing.Color.Black;
            this._guiSpectrumChart.AxisX.InitWithScaleView = false;
            this._guiSpectrumChart.AxisX.IsLogarithmic = false;
            this._guiSpectrumChart.AxisX.LabelAngle = 0;
            this._guiSpectrumChart.AxisX.LabelEnabled = true;
            this._guiSpectrumChart.AxisX.LabelFormat = null;
            this._guiSpectrumChart.AxisX.MajorGridColor = System.Drawing.Color.Black;
            this._guiSpectrumChart.AxisX.MajorGridCount = -1;
            this._guiSpectrumChart.AxisX.MajorGridEnabled = true;
            this._guiSpectrumChart.AxisX.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this._guiSpectrumChart.AxisX.Maximum = 1000D;
            this._guiSpectrumChart.AxisX.Minimum = 0D;
            this._guiSpectrumChart.AxisX.MinorGridColor = System.Drawing.Color.Black;
            this._guiSpectrumChart.AxisX.MinorGridEnabled = false;
            this._guiSpectrumChart.AxisX.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this._guiSpectrumChart.AxisX.TickWidth = 1F;
            this._guiSpectrumChart.AxisX.Title = "";
            this._guiSpectrumChart.AxisX.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this._guiSpectrumChart.AxisX.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this._guiSpectrumChart.AxisX.ViewMaximum = 1000D;
            this._guiSpectrumChart.AxisX.ViewMinimum = 0D;
            this._guiSpectrumChart.AxisX2.AutoScale = true;
            this._guiSpectrumChart.AxisX2.AutoZoomReset = false;
            this._guiSpectrumChart.AxisX2.Color = System.Drawing.Color.Black;
            this._guiSpectrumChart.AxisX2.InitWithScaleView = false;
            this._guiSpectrumChart.AxisX2.IsLogarithmic = false;
            this._guiSpectrumChart.AxisX2.LabelAngle = 0;
            this._guiSpectrumChart.AxisX2.LabelEnabled = true;
            this._guiSpectrumChart.AxisX2.LabelFormat = null;
            this._guiSpectrumChart.AxisX2.MajorGridColor = System.Drawing.Color.Black;
            this._guiSpectrumChart.AxisX2.MajorGridCount = -1;
            this._guiSpectrumChart.AxisX2.MajorGridEnabled = true;
            this._guiSpectrumChart.AxisX2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this._guiSpectrumChart.AxisX2.Maximum = 1000D;
            this._guiSpectrumChart.AxisX2.Minimum = 0D;
            this._guiSpectrumChart.AxisX2.MinorGridColor = System.Drawing.Color.Black;
            this._guiSpectrumChart.AxisX2.MinorGridEnabled = false;
            this._guiSpectrumChart.AxisX2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this._guiSpectrumChart.AxisX2.TickWidth = 1F;
            this._guiSpectrumChart.AxisX2.Title = "";
            this._guiSpectrumChart.AxisX2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this._guiSpectrumChart.AxisX2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this._guiSpectrumChart.AxisX2.ViewMaximum = 1000D;
            this._guiSpectrumChart.AxisX2.ViewMinimum = 0D;
            this._guiSpectrumChart.AxisY.AutoScale = false;
            this._guiSpectrumChart.AxisY.AutoZoomReset = false;
            this._guiSpectrumChart.AxisY.Color = System.Drawing.Color.Black;
            this._guiSpectrumChart.AxisY.InitWithScaleView = false;
            this._guiSpectrumChart.AxisY.IsLogarithmic = false;
            this._guiSpectrumChart.AxisY.LabelAngle = 0;
            this._guiSpectrumChart.AxisY.LabelEnabled = true;
            this._guiSpectrumChart.AxisY.LabelFormat = null;
            this._guiSpectrumChart.AxisY.MajorGridColor = System.Drawing.Color.Black;
            this._guiSpectrumChart.AxisY.MajorGridCount = 6;
            this._guiSpectrumChart.AxisY.MajorGridEnabled = true;
            this._guiSpectrumChart.AxisY.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this._guiSpectrumChart.AxisY.Maximum = 20D;
            this._guiSpectrumChart.AxisY.Minimum = -120D;
            this._guiSpectrumChart.AxisY.MinorGridColor = System.Drawing.Color.Black;
            this._guiSpectrumChart.AxisY.MinorGridEnabled = false;
            this._guiSpectrumChart.AxisY.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this._guiSpectrumChart.AxisY.TickWidth = 1F;
            this._guiSpectrumChart.AxisY.Title = "";
            this._guiSpectrumChart.AxisY.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this._guiSpectrumChart.AxisY.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this._guiSpectrumChart.AxisY.ViewMaximum = 3.5D;
            this._guiSpectrumChart.AxisY.ViewMinimum = 0.5D;
            this._guiSpectrumChart.AxisY2.AutoScale = true;
            this._guiSpectrumChart.AxisY2.AutoZoomReset = false;
            this._guiSpectrumChart.AxisY2.Color = System.Drawing.Color.Black;
            this._guiSpectrumChart.AxisY2.InitWithScaleView = false;
            this._guiSpectrumChart.AxisY2.IsLogarithmic = false;
            this._guiSpectrumChart.AxisY2.LabelAngle = 0;
            this._guiSpectrumChart.AxisY2.LabelEnabled = true;
            this._guiSpectrumChart.AxisY2.LabelFormat = null;
            this._guiSpectrumChart.AxisY2.MajorGridColor = System.Drawing.Color.Black;
            this._guiSpectrumChart.AxisY2.MajorGridCount = 6;
            this._guiSpectrumChart.AxisY2.MajorGridEnabled = true;
            this._guiSpectrumChart.AxisY2.MajorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.Dash;
            this._guiSpectrumChart.AxisY2.Maximum = 3.5D;
            this._guiSpectrumChart.AxisY2.Minimum = 0.5D;
            this._guiSpectrumChart.AxisY2.MinorGridColor = System.Drawing.Color.Black;
            this._guiSpectrumChart.AxisY2.MinorGridEnabled = false;
            this._guiSpectrumChart.AxisY2.MinorGridType = SeeSharpTools.JY.GUI.EasyChartXAxis.GridStyle.DashDot;
            this._guiSpectrumChart.AxisY2.TickWidth = 1F;
            this._guiSpectrumChart.AxisY2.Title = "";
            this._guiSpectrumChart.AxisY2.TitleOrientation = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextOrientation.Auto;
            this._guiSpectrumChart.AxisY2.TitlePosition = SeeSharpTools.JY.GUI.EasyChartXAxis.AxisTextPosition.Center;
            this._guiSpectrumChart.AxisY2.ViewMaximum = 3.5D;
            this._guiSpectrumChart.AxisY2.ViewMinimum = 0.5D;
            this._guiSpectrumChart.BackColor = System.Drawing.Color.White;
            this._guiSpectrumChart.ChartAreaBackColor = System.Drawing.Color.Empty;
            this._guiSpectrumChart.Cumulitive = false;
            this._guiSpectrumChart.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiSpectrumChart.GradientStyle = SeeSharpTools.JY.GUI.EasyChartX.ChartGradientStyle.None;
            this._guiSpectrumChart.LegendBackColor = System.Drawing.Color.Transparent;
            this._guiSpectrumChart.LegendFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this._guiSpectrumChart.LegendForeColor = System.Drawing.Color.Black;
            this._guiSpectrumChart.LegendVisible = false;
            easyChartXSeries1.Color = System.Drawing.Color.Red;
            easyChartXSeries1.Marker = SeeSharpTools.JY.GUI.EasyChartXSeries.MarkerType.None;
            easyChartXSeries1.Name = "Series1";
            easyChartXSeries1.Type = SeeSharpTools.JY.GUI.EasyChartXSeries.LineType.Line;
            easyChartXSeries1.Visible = true;
            easyChartXSeries1.Width = SeeSharpTools.JY.GUI.EasyChartXSeries.LineWidth.Thin;
            easyChartXSeries1.XPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries1.YPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            this._guiSpectrumChart.LineSeries.Add(easyChartXSeries1);
            this._guiSpectrumChart.Location = new System.Drawing.Point(7, 27);
            this._guiSpectrumChart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._guiSpectrumChart.Miscellaneous.CheckInfinity = false;
            this._guiSpectrumChart.Miscellaneous.CheckNaN = false;
            this._guiSpectrumChart.Miscellaneous.CheckNegtiveOrZero = false;
            this._guiSpectrumChart.Miscellaneous.DirectionChartCount = 3;
            this._guiSpectrumChart.Miscellaneous.Fitting = SeeSharpTools.JY.GUI.EasyChartX.FitType.Range;
            this._guiSpectrumChart.Miscellaneous.MarkerSize = 7;
            this._guiSpectrumChart.Miscellaneous.MaxSeriesCount = 32;
            this._guiSpectrumChart.Miscellaneous.MaxSeriesPointCount = 4000;
            this._guiSpectrumChart.Miscellaneous.ShowFunctionMenu = true;
            this._guiSpectrumChart.Miscellaneous.SplitLayoutColumnInterval = 0F;
            this._guiSpectrumChart.Miscellaneous.SplitLayoutDirection = SeeSharpTools.JY.GUI.EasyChartXUtility.LayoutDirection.LeftToRight;
            this._guiSpectrumChart.Miscellaneous.SplitLayoutRowInterval = 0F;
            this._guiSpectrumChart.Miscellaneous.SplitViewAutoLayout = true;
            this._guiSpectrumChart.Name = "_guiSpectrumChart";
            this._guiSpectrumChart.SeriesCount = 1;
            this._guiSpectrumChart.Size = new System.Drawing.Size(741, 146);
            this._guiSpectrumChart.SplitView = false;
            this._guiSpectrumChart.TabIndex = 0;
            this._guiSpectrumChart.XCursor.AutoInterval = true;
            this._guiSpectrumChart.XCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this._guiSpectrumChart.XCursor.Interval = 0.001D;
            this._guiSpectrumChart.XCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Zoom;
            this._guiSpectrumChart.XCursor.SelectionColor = System.Drawing.Color.LightGray;
            this._guiSpectrumChart.XCursor.Value = double.NaN;
            this._guiSpectrumChart.YCursor.AutoInterval = true;
            this._guiSpectrumChart.YCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this._guiSpectrumChart.YCursor.Interval = 0.001D;
            this._guiSpectrumChart.YCursor.Mode = SeeSharpTools.JY.GUI.EasyChartXCursor.CursorMode.Disabled;
            this._guiSpectrumChart.YCursor.SelectionColor = System.Drawing.Color.LightGray;
            this._guiSpectrumChart.YCursor.Value = double.NaN;
            // 
            // labelTitle
            // 
            this.labelTitle.BackColor = System.Drawing.Color.DodgerBlue;
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTitle.Font = new System.Drawing.Font("Cambria", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(784, 71);
            this.labelTitle.TabIndex = 5;
            this.labelTitle.Text = "Spectrum File Write Example";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _guiSignalParam
            // 
            this._guiSignalParam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._guiSignalParam.Controls.Add(this._guiBand2Enabled);
            this._guiSignalParam.Controls.Add(this._guiSignalBand2Info);
            this._guiSignalParam.Controls.Add(this._guiSignalBand1Info);
            this._guiSignalParam.Controls.Add(this._guiNumberOfChannels);
            this._guiSignalParam.Controls.Add(label3);
            this._guiSignalParam.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiSignalParam.Location = new System.Drawing.Point(7, 74);
            this._guiSignalParam.Name = "_guiSignalParam";
            this._guiSignalParam.Size = new System.Drawing.Size(760, 242);
            this._guiSignalParam.TabIndex = 6;
            this._guiSignalParam.TabStop = false;
            this._guiSignalParam.Text = "Signal Param";
            // 
            // _guiBand2Enabled
            // 
            this._guiBand2Enabled.AutoSize = true;
            this._guiBand2Enabled.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiBand2Enabled.Location = new System.Drawing.Point(313, 55);
            this._guiBand2Enabled.Name = "_guiBand2Enabled";
            this._guiBand2Enabled.Size = new System.Drawing.Size(180, 32);
            this._guiBand2Enabled.TabIndex = 9;
            this._guiBand2Enabled.Text = "Enable Band2";
            this._guiBand2Enabled.UseVisualStyleBackColor = true;
            this._guiBand2Enabled.CheckedChanged += new System.EventHandler(this.GuiBand2Enabled_CheckedChanged);
            // 
            // _guiSignalBand2Info
            // 
            this._guiSignalBand2Info.Controls.Add(label9);
            this._guiSignalBand2Info.Controls.Add(label10);
            this._guiSignalBand2Info.Controls.Add(label11);
            this._guiSignalBand2Info.Controls.Add(label12);
            this._guiSignalBand2Info.Controls.Add(label13);
            this._guiSignalBand2Info.Controls.Add(label14);
            this._guiSignalBand2Info.Controls.Add(label15);
            this._guiSignalBand2Info.Controls.Add(this._guiBand2FreqStop);
            this._guiSignalBand2Info.Controls.Add(this._guiBand2NumOfLines);
            this._guiSignalBand2Info.Controls.Add(this._guiBand2FreqStep);
            this._guiSignalBand2Info.Controls.Add(this._guiBand2FreqStart);
            this._guiSignalBand2Info.Enabled = false;
            this._guiSignalBand2Info.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiSignalBand2Info.Location = new System.Drawing.Point(299, 57);
            this._guiSignalBand2Info.Name = "_guiSignalBand2Info";
            this._guiSignalBand2Info.Size = new System.Drawing.Size(258, 174);
            this._guiSignalBand2Info.TabIndex = 8;
            this._guiSignalBand2Info.TabStop = false;
            // 
            // _guiBand2FreqStop
            // 
            this._guiBand2FreqStop.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiBand2FreqStop.Location = new System.Drawing.Point(96, 71);
            this._guiBand2FreqStop.Maximum = new decimal(new int[] {
            18000,
            0,
            0,
            0});
            this._guiBand2FreqStop.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this._guiBand2FreqStop.Name = "_guiBand2FreqStop";
            this._guiBand2FreqStop.Size = new System.Drawing.Size(97, 36);
            this._guiBand2FreqStop.TabIndex = 9;
            this._guiBand2FreqStop.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._guiBand2FreqStop.Value = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this._guiBand2FreqStop.ValueChanged += new System.EventHandler(this.GuiSignalParam_ValueChanged);
            // 
            // _guiBand2NumOfLines
            // 
            this._guiBand2NumOfLines.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiBand2NumOfLines.Location = new System.Drawing.Point(96, 135);
            this._guiBand2NumOfLines.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this._guiBand2NumOfLines.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._guiBand2NumOfLines.Name = "_guiBand2NumOfLines";
            this._guiBand2NumOfLines.ReadOnly = true;
            this._guiBand2NumOfLines.Size = new System.Drawing.Size(97, 36);
            this._guiBand2NumOfLines.TabIndex = 9;
            this._guiBand2NumOfLines.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._guiBand2NumOfLines.Value = new decimal(new int[] {
            2001,
            0,
            0,
            0});
            // 
            // _guiBand2FreqStep
            // 
            this._guiBand2FreqStep.DecimalPlaces = 3;
            this._guiBand2FreqStep.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiBand2FreqStep.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this._guiBand2FreqStep.Location = new System.Drawing.Point(96, 103);
            this._guiBand2FreqStep.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._guiBand2FreqStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this._guiBand2FreqStep.Name = "_guiBand2FreqStep";
            this._guiBand2FreqStep.Size = new System.Drawing.Size(97, 36);
            this._guiBand2FreqStep.TabIndex = 9;
            this._guiBand2FreqStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._guiBand2FreqStep.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this._guiBand2FreqStep.ValueChanged += new System.EventHandler(this.GuiSignalParam_ValueChanged);
            // 
            // _guiBand2FreqStart
            // 
            this._guiBand2FreqStart.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiBand2FreqStart.Location = new System.Drawing.Point(96, 39);
            this._guiBand2FreqStart.Maximum = new decimal(new int[] {
            18000,
            0,
            0,
            0});
            this._guiBand2FreqStart.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this._guiBand2FreqStart.Name = "_guiBand2FreqStart";
            this._guiBand2FreqStart.Size = new System.Drawing.Size(97, 36);
            this._guiBand2FreqStart.TabIndex = 9;
            this._guiBand2FreqStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._guiBand2FreqStart.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this._guiBand2FreqStart.ValueChanged += new System.EventHandler(this.GuiSignalParam_ValueChanged);
            // 
            // _guiSignalBand1Info
            // 
            this._guiSignalBand1Info.Controls.Add(label5);
            this._guiSignalBand1Info.Controls.Add(label7);
            this._guiSignalBand1Info.Controls.Add(label2);
            this._guiSignalBand1Info.Controls.Add(label4);
            this._guiSignalBand1Info.Controls.Add(label8);
            this._guiSignalBand1Info.Controls.Add(label6);
            this._guiSignalBand1Info.Controls.Add(label1);
            this._guiSignalBand1Info.Controls.Add(this._guiBand1FreqStop);
            this._guiSignalBand1Info.Controls.Add(this._guiBand1NumOfLines);
            this._guiSignalBand1Info.Controls.Add(this._guiBand1FreqStep);
            this._guiSignalBand1Info.Controls.Add(this._guiBand1FreqStart);
            this._guiSignalBand1Info.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiSignalBand1Info.Location = new System.Drawing.Point(16, 57);
            this._guiSignalBand1Info.Name = "_guiSignalBand1Info";
            this._guiSignalBand1Info.Size = new System.Drawing.Size(258, 174);
            this._guiSignalBand1Info.TabIndex = 8;
            this._guiSignalBand1Info.TabStop = false;
            this._guiSignalBand1Info.Text = "Band1";
            // 
            // _guiBand1FreqStop
            // 
            this._guiBand1FreqStop.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiBand1FreqStop.Location = new System.Drawing.Point(96, 71);
            this._guiBand1FreqStop.Maximum = new decimal(new int[] {
            18000,
            0,
            0,
            0});
            this._guiBand1FreqStop.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this._guiBand1FreqStop.Name = "_guiBand1FreqStop";
            this._guiBand1FreqStop.Size = new System.Drawing.Size(97, 36);
            this._guiBand1FreqStop.TabIndex = 9;
            this._guiBand1FreqStop.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._guiBand1FreqStop.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this._guiBand1FreqStop.ValueChanged += new System.EventHandler(this.GuiSignalParam_ValueChanged);
            // 
            // _guiBand1NumOfLines
            // 
            this._guiBand1NumOfLines.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiBand1NumOfLines.Location = new System.Drawing.Point(96, 135);
            this._guiBand1NumOfLines.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this._guiBand1NumOfLines.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._guiBand1NumOfLines.Name = "_guiBand1NumOfLines";
            this._guiBand1NumOfLines.ReadOnly = true;
            this._guiBand1NumOfLines.Size = new System.Drawing.Size(97, 36);
            this._guiBand1NumOfLines.TabIndex = 9;
            this._guiBand1NumOfLines.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._guiBand1NumOfLines.Value = new decimal(new int[] {
            1001,
            0,
            0,
            0});
            // 
            // _guiBand1FreqStep
            // 
            this._guiBand1FreqStep.DecimalPlaces = 3;
            this._guiBand1FreqStep.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiBand1FreqStep.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this._guiBand1FreqStep.Location = new System.Drawing.Point(96, 103);
            this._guiBand1FreqStep.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._guiBand1FreqStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this._guiBand1FreqStep.Name = "_guiBand1FreqStep";
            this._guiBand1FreqStep.Size = new System.Drawing.Size(97, 36);
            this._guiBand1FreqStep.TabIndex = 9;
            this._guiBand1FreqStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._guiBand1FreqStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this._guiBand1FreqStep.ValueChanged += new System.EventHandler(this.GuiSignalParam_ValueChanged);
            // 
            // _guiBand1FreqStart
            // 
            this._guiBand1FreqStart.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiBand1FreqStart.Location = new System.Drawing.Point(96, 39);
            this._guiBand1FreqStart.Maximum = new decimal(new int[] {
            18000,
            0,
            0,
            0});
            this._guiBand1FreqStart.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this._guiBand1FreqStart.Name = "_guiBand1FreqStart";
            this._guiBand1FreqStart.Size = new System.Drawing.Size(97, 36);
            this._guiBand1FreqStart.TabIndex = 9;
            this._guiBand1FreqStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._guiBand1FreqStart.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this._guiBand1FreqStart.ValueChanged += new System.EventHandler(this.GuiSignalParam_ValueChanged);
            // 
            // _guiNumberOfChannels
            // 
            this._guiNumberOfChannels.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiNumberOfChannels.Location = new System.Drawing.Point(168, 24);
            this._guiNumberOfChannels.Margin = new System.Windows.Forms.Padding(2);
            this._guiNumberOfChannels.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this._guiNumberOfChannels.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._guiNumberOfChannels.Name = "_guiNumberOfChannels";
            this._guiNumberOfChannels.Size = new System.Drawing.Size(72, 41);
            this._guiNumberOfChannels.TabIndex = 6;
            this._guiNumberOfChannels.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._guiNumberOfChannels.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this._guiNumberOfChannels.ValueChanged += new System.EventHandler(this.GuiSignalParam_ValueChanged);
            // 
            // _guiFileParam
            // 
            this._guiFileParam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._guiFileParam.Controls.Add(this._guiWriteDataType);
            this._guiFileParam.Controls.Add(this._guiBrowseFolder);
            this._guiFileParam.Controls.Add(this._guiDestinationFolder);
            this._guiFileParam.Controls.Add(this._guiFileLength);
            this._guiFileParam.Controls.Add(label16);
            this._guiFileParam.Controls.Add(label17);
            this._guiFileParam.Controls.Add(label18);
            this._guiFileParam.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiFileParam.Location = new System.Drawing.Point(7, 509);
            this._guiFileParam.Name = "_guiFileParam";
            this._guiFileParam.Size = new System.Drawing.Size(760, 121);
            this._guiFileParam.TabIndex = 7;
            this._guiFileParam.TabStop = false;
            this._guiFileParam.Text = "File Param";
            // 
            // _guiWriteDataType
            // 
            this._guiWriteDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._guiWriteDataType.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiWriteDataType.FormattingEnabled = true;
            this._guiWriteDataType.Items.AddRange(new object[] {
            "Double",
            "Float"});
            this._guiWriteDataType.Location = new System.Drawing.Point(345, 76);
            this._guiWriteDataType.Name = "_guiWriteDataType";
            this._guiWriteDataType.Size = new System.Drawing.Size(121, 36);
            this._guiWriteDataType.TabIndex = 11;
            this._guiWriteDataType.SelectedIndexChanged += new System.EventHandler(this.GuiWriteDataType_SelectedIndexChanged);
            // 
            // _guiBrowseFolder
            // 
            this._guiBrowseFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this._guiBrowseFolder.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiBrowseFolder.Location = new System.Drawing.Point(712, 25);
            this._guiBrowseFolder.Name = "_guiBrowseFolder";
            this._guiBrowseFolder.Size = new System.Drawing.Size(36, 25);
            this._guiBrowseFolder.TabIndex = 9;
            this._guiBrowseFolder.Text = "...";
            this._guiBrowseFolder.UseVisualStyleBackColor = false;
            this._guiBrowseFolder.Click += new System.EventHandler(this.GuiBrowseFolder_Click);
            // 
            // _guiDestinationFolder
            // 
            this._guiDestinationFolder.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiDestinationFolder.Location = new System.Drawing.Point(168, 25);
            this._guiDestinationFolder.Multiline = true;
            this._guiDestinationFolder.Name = "_guiDestinationFolder";
            this._guiDestinationFolder.Size = new System.Drawing.Size(536, 41);
            this._guiDestinationFolder.TabIndex = 8;
            this._guiDestinationFolder.Text = "D:\\Data";
            // 
            // _guiFileLength
            // 
            this._guiFileLength.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiFileLength.Location = new System.Drawing.Point(168, 79);
            this._guiFileLength.Margin = new System.Windows.Forms.Padding(2);
            this._guiFileLength.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this._guiFileLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._guiFileLength.Name = "_guiFileLength";
            this._guiFileLength.Size = new System.Drawing.Size(92, 41);
            this._guiFileLength.TabIndex = 6;
            this._guiFileLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._guiFileLength.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // _guiProgressValue
            // 
            this._guiProgressValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._guiProgressValue.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiProgressValue.Location = new System.Drawing.Point(337, 640);
            this._guiProgressValue.Name = "_guiProgressValue";
            this._guiProgressValue.ReadOnly = true;
            this._guiProgressValue.Size = new System.Drawing.Size(70, 36);
            this._guiProgressValue.TabIndex = 12;
            this._guiProgressValue.TabStop = false;
            this._guiProgressValue.Text = "0 %";
            this._guiProgressValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _guiProgressBar
            // 
            this._guiProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._guiProgressBar.Location = new System.Drawing.Point(7, 636);
            this._guiProgressBar.Name = "_guiProgressBar";
            this._guiProgressBar.Size = new System.Drawing.Size(324, 33);
            this._guiProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this._guiProgressBar.TabIndex = 11;
            // 
            // _guiStop
            // 
            this._guiStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._guiStop.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiStop.Location = new System.Drawing.Point(666, 636);
            this._guiStop.Name = "_guiStop";
            this._guiStop.Size = new System.Drawing.Size(101, 33);
            this._guiStop.TabIndex = 9;
            this._guiStop.Text = "Stop";
            this._guiStop.UseVisualStyleBackColor = true;
            this._guiStop.Click += new System.EventHandler(this.GuiStop_Click);
            // 
            // _guiStart
            // 
            this._guiStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._guiStart.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiStart.Location = new System.Drawing.Point(543, 636);
            this._guiStart.Name = "_guiStart";
            this._guiStart.Size = new System.Drawing.Size(101, 33);
            this._guiStart.TabIndex = 10;
            this._guiStart.Text = "Start";
            this._guiStart.UseVisualStyleBackColor = true;
            this._guiStart.Click += new System.EventHandler(this.GuiStart_Click);
            // 
            // _bgWorker
            // 
            this._bgWorker.WorkerReportsProgress = true;
            this._bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgWorker_DoWork);
            this._bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BgWorker_ProgressChanged);
            this._bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgWorker_RunWorkerCompleted);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(784, 681);
            this.Controls.Add(_guiSignalDisplay);
            this.Controls.Add(this._guiProgressValue);
            this.Controls.Add(this._guiProgressBar);
            this.Controls.Add(this._guiStop);
            this.Controls.Add(this._guiStart);
            this.Controls.Add(this._guiFileParam);
            this.Controls.Add(this._guiSignalParam);
            this.Controls.Add(this.labelTitle);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "MainForm";
            this.Text = "Spectrum File Write Example";
            this.Load += new System.EventHandler(this.MainForm_Load);
            _guiSignalDisplay.ResumeLayout(false);
            this._guiSignalParam.ResumeLayout(false);
            this._guiSignalParam.PerformLayout();
            this._guiSignalBand2Info.ResumeLayout(false);
            this._guiSignalBand2Info.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._guiBand2FreqStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiBand2NumOfLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiBand2FreqStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiBand2FreqStart)).EndInit();
            this._guiSignalBand1Info.ResumeLayout(false);
            this._guiSignalBand1Info.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._guiBand1FreqStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiBand1NumOfLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiBand1FreqStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiBand1FreqStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiNumberOfChannels)).EndInit();
            this._guiFileParam.ResumeLayout(false);
            this._guiFileParam.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._guiFileLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.GroupBox _guiSignalParam;
        private System.Windows.Forms.NumericUpDown _guiNumberOfChannels;
        private System.Windows.Forms.NumericUpDown _guiBand1FreqStop;
        private System.Windows.Forms.NumericUpDown _guiBand1NumOfLines;
        private System.Windows.Forms.NumericUpDown _guiBand1FreqStep;
        private System.Windows.Forms.NumericUpDown _guiBand1FreqStart;
        private System.Windows.Forms.NumericUpDown _guiBand2FreqStop;
        private System.Windows.Forms.NumericUpDown _guiBand2NumOfLines;
        private System.Windows.Forms.NumericUpDown _guiBand2FreqStep;
        private System.Windows.Forms.NumericUpDown _guiBand2FreqStart;
        private System.Windows.Forms.GroupBox _guiSignalBand2Info;
        private System.Windows.Forms.GroupBox _guiSignalBand1Info;
        private System.Windows.Forms.GroupBox _guiFileParam;
        private System.Windows.Forms.Button _guiBrowseFolder;
        private System.Windows.Forms.TextBox _guiDestinationFolder;
        private System.Windows.Forms.NumericUpDown _guiFileLength;
        private System.Windows.Forms.CheckBox _guiBand2Enabled;
        private System.Windows.Forms.TextBox _guiProgressValue;
        private System.Windows.Forms.ProgressBar _guiProgressBar;
        private System.Windows.Forms.Button _guiStop;
        private System.Windows.Forms.Button _guiStart;
        private SeeSharpTools.JY.GUI.EasyChartX _guiSpectrumChart;
        private System.ComponentModel.BackgroundWorker _bgWorker;
        private System.Windows.Forms.FolderBrowserDialog _guiFolderBrowserDialog;
        private System.Windows.Forms.ComboBox _guiWriteDataType;
    }
}

