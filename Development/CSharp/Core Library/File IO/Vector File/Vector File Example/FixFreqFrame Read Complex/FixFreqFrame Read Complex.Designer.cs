namespace FixFreqFrameFileExampleReadComplex
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
            System.Windows.Forms.Label label6;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            SeeSharpTools.JY.GUI.EasyChartXSeries easyChartXSeries1 = new SeeSharpTools.JY.GUI.EasyChartXSeries();
            this.labelTitle = new System.Windows.Forms.Label();
            this._guiFileSelectionParam = new System.Windows.Forms.GroupBox();
            this._guiBrowseFile = new System.Windows.Forms.Button();
            this._guiFilePath = new System.Windows.Forms.TextBox();
            this._guiSplitterMain = new System.Windows.Forms.SplitContainer();
            this._guiReadControlSetting = new System.Windows.Forms.GroupBox();
            this._guiStop = new System.Windows.Forms.Button();
            this._guiReadPosValue = new System.Windows.Forms.TextBox();
            this._guiStart = new System.Windows.Forms.Button();
            this._guiReadPositionBar = new System.Windows.Forms.TrackBar();
            this._guiSplitterForData = new System.Windows.Forms.SplitContainer();
            this._guiFileInfoView = new System.Windows.Forms.DataGridView();
            this.PropertyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PropertyValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._guiSpectrumChart = new SeeSharpTools.JY.GUI.EasyChartX();
            this._guiFileBrowseDialog = new System.Windows.Forms.OpenFileDialog();
            this._bgWorker = new System.ComponentModel.BackgroundWorker();
            label6 = new System.Windows.Forms.Label();
            this._guiFileSelectionParam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._guiSplitterMain)).BeginInit();
            this._guiSplitterMain.Panel1.SuspendLayout();
            this._guiSplitterMain.Panel2.SuspendLayout();
            this._guiSplitterMain.SuspendLayout();
            this._guiReadControlSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._guiReadPositionBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiSplitterForData)).BeginInit();
            this._guiSplitterForData.Panel1.SuspendLayout();
            this._guiSplitterForData.Panel2.SuspendLayout();
            this._guiSplitterForData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._guiFileInfoView)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label6.Location = new System.Drawing.Point(12, 31);
            label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(69, 19);
            label6.TabIndex = 7;
            label6.Text = "File Path";
            // 
            // labelTitle
            // 
            this.labelTitle.BackColor = System.Drawing.Color.DodgerBlue;
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTitle.Font = new System.Drawing.Font("Cambria", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(984, 71);
            this.labelTitle.TabIndex = 5;
            this.labelTitle.Text = "FixFrequencyFrame File Read Example (Complex)";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _guiFileSelectionParam
            // 
            this._guiFileSelectionParam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._guiFileSelectionParam.Controls.Add(this._guiBrowseFile);
            this._guiFileSelectionParam.Controls.Add(this._guiFilePath);
            this._guiFileSelectionParam.Controls.Add(label6);
            this._guiFileSelectionParam.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiFileSelectionParam.Location = new System.Drawing.Point(7, 13);
            this._guiFileSelectionParam.Name = "_guiFileSelectionParam";
            this._guiFileSelectionParam.Size = new System.Drawing.Size(960, 77);
            this._guiFileSelectionParam.TabIndex = 6;
            this._guiFileSelectionParam.TabStop = false;
            this._guiFileSelectionParam.Text = "File Selection";
            // 
            // _guiBrowseFile
            // 
            this._guiBrowseFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._guiBrowseFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this._guiBrowseFile.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiBrowseFile.Location = new System.Drawing.Point(912, 25);
            this._guiBrowseFile.Name = "_guiBrowseFile";
            this._guiBrowseFile.Size = new System.Drawing.Size(36, 25);
            this._guiBrowseFile.TabIndex = 9;
            this._guiBrowseFile.Text = "...";
            this._guiBrowseFile.UseVisualStyleBackColor = false;
            this._guiBrowseFile.Click += new System.EventHandler(this.GuiBrowseFile_Click);
            // 
            // _guiFilePath
            // 
            this._guiFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._guiFilePath.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiFilePath.Location = new System.Drawing.Point(86, 25);
            this._guiFilePath.Multiline = true;
            this._guiFilePath.Name = "_guiFilePath";
            this._guiFilePath.ReadOnly = true;
            this._guiFilePath.Size = new System.Drawing.Size(818, 41);
            this._guiFilePath.TabIndex = 8;
            this._guiFilePath.TabStop = false;
            // 
            // _guiSplitterMain
            // 
            this._guiSplitterMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._guiSplitterMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this._guiSplitterMain.IsSplitterFixed = true;
            this._guiSplitterMain.Location = new System.Drawing.Point(0, 71);
            this._guiSplitterMain.Name = "_guiSplitterMain";
            this._guiSplitterMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _guiSplitterMain.Panel1
            // 
            this._guiSplitterMain.Panel1.Controls.Add(this._guiReadControlSetting);
            this._guiSplitterMain.Panel1.Controls.Add(this._guiFileSelectionParam);
            // 
            // _guiSplitterMain.Panel2
            // 
            this._guiSplitterMain.Panel2.Controls.Add(this._guiSplitterForData);
            this._guiSplitterMain.Size = new System.Drawing.Size(984, 490);
            this._guiSplitterMain.SplitterDistance = 174;
            this._guiSplitterMain.TabIndex = 7;
            // 
            // _guiReadControlSetting
            // 
            this._guiReadControlSetting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._guiReadControlSetting.Controls.Add(this._guiStop);
            this._guiReadControlSetting.Controls.Add(this._guiReadPosValue);
            this._guiReadControlSetting.Controls.Add(this._guiStart);
            this._guiReadControlSetting.Controls.Add(this._guiReadPositionBar);
            this._guiReadControlSetting.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiReadControlSetting.Location = new System.Drawing.Point(7, 96);
            this._guiReadControlSetting.Name = "_guiReadControlSetting";
            this._guiReadControlSetting.Size = new System.Drawing.Size(960, 69);
            this._guiReadControlSetting.TabIndex = 7;
            this._guiReadControlSetting.TabStop = false;
            this._guiReadControlSetting.Text = "Read Control";
            // 
            // _guiStop
            // 
            this._guiStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._guiStop.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiStop.Location = new System.Drawing.Point(888, 21);
            this._guiStop.Name = "_guiStop";
            this._guiStop.Size = new System.Drawing.Size(60, 33);
            this._guiStop.TabIndex = 8;
            this._guiStop.Text = "Stop";
            this._guiStop.UseVisualStyleBackColor = true;
            this._guiStop.Click += new System.EventHandler(this.GuiStop_Click);
            // 
            // _guiReadPosValue
            // 
            this._guiReadPosValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._guiReadPosValue.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiReadPosValue.Location = new System.Drawing.Point(697, 25);
            this._guiReadPosValue.Name = "_guiReadPosValue";
            this._guiReadPosValue.ReadOnly = true;
            this._guiReadPosValue.Size = new System.Drawing.Size(104, 26);
            this._guiReadPosValue.TabIndex = 9;
            this._guiReadPosValue.TabStop = false;
            this._guiReadPosValue.Text = "1";
            this._guiReadPosValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _guiStart
            // 
            this._guiStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._guiStart.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiStart.Location = new System.Drawing.Point(817, 21);
            this._guiStart.Name = "_guiStart";
            this._guiStart.Size = new System.Drawing.Size(60, 33);
            this._guiStart.TabIndex = 9;
            this._guiStart.Text = "Start";
            this._guiStart.UseVisualStyleBackColor = true;
            this._guiStart.Click += new System.EventHandler(this.GuiStart_Click);
            // 
            // _guiReadPositionBar
            // 
            this._guiReadPositionBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._guiReadPositionBar.Location = new System.Drawing.Point(6, 20);
            this._guiReadPositionBar.Maximum = 100;
            this._guiReadPositionBar.Name = "_guiReadPositionBar";
            this._guiReadPositionBar.Size = new System.Drawing.Size(685, 45);
            this._guiReadPositionBar.SmallChange = 5;
            this._guiReadPositionBar.TabIndex = 1;
            this._guiReadPositionBar.TickFrequency = 10;
            this._guiReadPositionBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this._guiReadPositionBar.ValueChanged += new System.EventHandler(this.GuiReadPositionBar_ValueChanged);
            // 
            // _guiSplitterForData
            // 
            this._guiSplitterForData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._guiSplitterForData.Dock = System.Windows.Forms.DockStyle.Fill;
            this._guiSplitterForData.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this._guiSplitterForData.Location = new System.Drawing.Point(0, 0);
            this._guiSplitterForData.Name = "_guiSplitterForData";
            // 
            // _guiSplitterForData.Panel1
            // 
            this._guiSplitterForData.Panel1.Controls.Add(this._guiFileInfoView);
            // 
            // _guiSplitterForData.Panel2
            // 
            this._guiSplitterForData.Panel2.Controls.Add(this._guiSpectrumChart);
            this._guiSplitterForData.Size = new System.Drawing.Size(984, 312);
            this._guiSplitterForData.SplitterDistance = 273;
            this._guiSplitterForData.TabIndex = 0;
            // 
            // _guiFileInfoView
            // 
            this._guiFileInfoView.AllowUserToAddRows = false;
            this._guiFileInfoView.AllowUserToDeleteRows = false;
            this._guiFileInfoView.AllowUserToResizeRows = false;
            this._guiFileInfoView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._guiFileInfoView.ColumnHeadersVisible = false;
            this._guiFileInfoView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PropertyName,
            this.PropertyValue});
            this._guiFileInfoView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._guiFileInfoView.Location = new System.Drawing.Point(0, 0);
            this._guiFileInfoView.MultiSelect = false;
            this._guiFileInfoView.Name = "_guiFileInfoView";
            this._guiFileInfoView.ReadOnly = true;
            this._guiFileInfoView.RowHeadersVisible = false;
            this._guiFileInfoView.RowTemplate.Height = 23;
            this._guiFileInfoView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._guiFileInfoView.Size = new System.Drawing.Size(271, 310);
            this._guiFileInfoView.TabIndex = 0;
            // 
            // PropertyName
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PropertyName.DefaultCellStyle = dataGridViewCellStyle1;
            this.PropertyName.FillWeight = 50F;
            this.PropertyName.HeaderText = "";
            this.PropertyName.Name = "PropertyName";
            this.PropertyName.ReadOnly = true;
            this.PropertyName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PropertyValue
            // 
            this.PropertyValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PropertyValue.DefaultCellStyle = dataGridViewCellStyle2;
            this.PropertyValue.HeaderText = "";
            this.PropertyValue.Name = "PropertyValue";
            this.PropertyValue.ReadOnly = true;
            this.PropertyValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // _guiSpectrumChart
            // 
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
            this._guiSpectrumChart.AxisY.Minimum = -100D;
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
            this._guiSpectrumChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this._guiSpectrumChart.GradientStyle = SeeSharpTools.JY.GUI.EasyChartX.ChartGradientStyle.None;
            this._guiSpectrumChart.LegendBackColor = System.Drawing.Color.Transparent;
            this._guiSpectrumChart.LegendFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this._guiSpectrumChart.LegendForeColor = System.Drawing.Color.Black;
            this._guiSpectrumChart.LegendVisible = false;
            easyChartXSeries1.Color = System.Drawing.Color.Red;
            easyChartXSeries1.Marker = SeeSharpTools.JY.GUI.EasyChartXSeries.MarkerType.None;
            easyChartXSeries1.Name = "Series1";
            easyChartXSeries1.Type = SeeSharpTools.JY.GUI.EasyChartXSeries.LineType.FastLine;
            easyChartXSeries1.Visible = true;
            easyChartXSeries1.Width = SeeSharpTools.JY.GUI.EasyChartXSeries.LineWidth.Thin;
            easyChartXSeries1.XPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            easyChartXSeries1.YPlotAxis = SeeSharpTools.JY.GUI.EasyChartXAxis.PlotAxis.Primary;
            this._guiSpectrumChart.LineSeries.Add(easyChartXSeries1);
            this._guiSpectrumChart.Location = new System.Drawing.Point(0, 0);
            this._guiSpectrumChart.Margin = new System.Windows.Forms.Padding(5);
            this._guiSpectrumChart.Miscellaneous.CheckInfinity = false;
            this._guiSpectrumChart.Miscellaneous.CheckNaN = false;
            this._guiSpectrumChart.Miscellaneous.CheckNegtiveOrZero = false;
            this._guiSpectrumChart.Miscellaneous.DirectionChartCount = 3;
            this._guiSpectrumChart.Miscellaneous.Fitting = SeeSharpTools.JY.GUI.EasyChartX.FitType.Range;
            this._guiSpectrumChart.Miscellaneous.MarkerSize = 3;
            this._guiSpectrumChart.Miscellaneous.MaxSeriesCount = 32;
            this._guiSpectrumChart.Miscellaneous.MaxSeriesPointCount = 4000;
            this._guiSpectrumChart.Miscellaneous.ShowFunctionMenu = true;
            this._guiSpectrumChart.Miscellaneous.SplitLayoutColumnInterval = 0F;
            this._guiSpectrumChart.Miscellaneous.SplitLayoutDirection = SeeSharpTools.JY.GUI.EasyChartXUtility.LayoutDirection.LeftToRight;
            this._guiSpectrumChart.Miscellaneous.SplitLayoutRowInterval = 0F;
            this._guiSpectrumChart.Miscellaneous.SplitViewAutoLayout = true;
            this._guiSpectrumChart.Name = "_guiSpectrumChart";
            this._guiSpectrumChart.SeriesCount = 1;
            this._guiSpectrumChart.Size = new System.Drawing.Size(705, 310);
            this._guiSpectrumChart.SplitView = false;
            this._guiSpectrumChart.TabIndex = 2;
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
            // _guiFileBrowseDialog
            // 
            this._guiFileBrowseDialog.Filter = "IQ Files (*.IQ)|*.IQ";
            // 
            // _bgWorker
            // 
            this._bgWorker.WorkerReportsProgress = true;
            this._bgWorker.WorkerSupportsCancellation = true;
            this._bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgWorker_DoWork);
            this._bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BgWorker_ProgressChanged);
            this._bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgWorker_RunWorkerCompleted);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this._guiSplitterMain);
            this.Controls.Add(this.labelTitle);
            this.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FixFreqFrame Read Complex";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this._guiFileSelectionParam.ResumeLayout(false);
            this._guiFileSelectionParam.PerformLayout();
            this._guiSplitterMain.Panel1.ResumeLayout(false);
            this._guiSplitterMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._guiSplitterMain)).EndInit();
            this._guiSplitterMain.ResumeLayout(false);
            this._guiReadControlSetting.ResumeLayout(false);
            this._guiReadControlSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._guiReadPositionBar)).EndInit();
            this._guiSplitterForData.Panel1.ResumeLayout(false);
            this._guiSplitterForData.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._guiSplitterForData)).EndInit();
            this._guiSplitterForData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._guiFileInfoView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.GroupBox _guiFileSelectionParam;
        private System.Windows.Forms.Button _guiBrowseFile;
        private System.Windows.Forms.TextBox _guiFilePath;
        private System.Windows.Forms.SplitContainer _guiSplitterMain;
        private System.Windows.Forms.GroupBox _guiReadControlSetting;
        private System.Windows.Forms.TrackBar _guiReadPositionBar;
        private System.Windows.Forms.TextBox _guiReadPosValue;
        private System.Windows.Forms.Button _guiStop;
        private System.Windows.Forms.Button _guiStart;
        private System.Windows.Forms.SplitContainer _guiSplitterForData;
        private SeeSharpTools.JY.GUI.EasyChartX _guiSpectrumChart;
        private System.Windows.Forms.OpenFileDialog _guiFileBrowseDialog;
        private System.Windows.Forms.DataGridView _guiFileInfoView;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropertyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropertyValue;
        private System.ComponentModel.BackgroundWorker _bgWorker;
    }
}

