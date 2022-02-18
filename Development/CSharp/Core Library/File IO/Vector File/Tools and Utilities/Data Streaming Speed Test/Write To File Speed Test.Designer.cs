namespace UtilityWin32WriteBlockSizeTest
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
            System.Windows.Forms.GroupBox _guiSelectedChRecFolderSetting;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            this._guiBrowseFolder = new System.Windows.Forms.Button();
            this._guiSelectedChRecordFolder = new System.Windows.Forms.TextBox();
            this._guiFileView = new System.Windows.Forms.DataGridView();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BlockSize = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.FileFolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelTitle = new System.Windows.Forms.Label();
            this._guiNumberOfFiles = new System.Windows.Forms.NumericUpDown();
            this._guiStop = new System.Windows.Forms.Button();
            this._guiStart = new System.Windows.Forms.Button();
            this._guiSpeedResultView = new System.Windows.Forms.DataGridView();
            this.FileCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Speed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._guiFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this._guiTotalSpeed = new System.Windows.Forms.TextBox();
            this._guiTimerForStatus = new System.Windows.Forms.Timer(this.components);
            this._guiStatus = new System.Windows.Forms.StatusStrip();
            this._guiToolProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this._guiWriteLength = new System.Windows.Forms.NumericUpDown();
            this._guiDisableBuffering = new System.Windows.Forms.CheckBox();
            this.group = new System.Windows.Forms.GroupBox();
            this._guiWriteMode = new System.Windows.Forms.ComboBox();
            _guiSelectedChRecFolderSetting = new System.Windows.Forms.GroupBox();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            _guiSelectedChRecFolderSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._guiFileView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiNumberOfFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiSpeedResultView)).BeginInit();
            this._guiStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._guiWriteLength)).BeginInit();
            this.group.SuspendLayout();
            this.SuspendLayout();
            // 
            // _guiSelectedChRecFolderSetting
            // 
            _guiSelectedChRecFolderSetting.Anchor = System.Windows.Forms.AnchorStyles.None;
            _guiSelectedChRecFolderSetting.Controls.Add(this._guiBrowseFolder);
            _guiSelectedChRecFolderSetting.Controls.Add(this._guiSelectedChRecordFolder);
            _guiSelectedChRecFolderSetting.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            _guiSelectedChRecFolderSetting.Location = new System.Drawing.Point(583, 316);
            _guiSelectedChRecFolderSetting.Name = "_guiSelectedChRecFolderSetting";
            _guiSelectedChRecFolderSetting.Size = new System.Drawing.Size(354, 88);
            _guiSelectedChRecFolderSetting.TabIndex = 7;
            _guiSelectedChRecFolderSetting.TabStop = false;
            _guiSelectedChRecFolderSetting.Text = "当前选中通道的数据存储目录";
            // 
            // _guiBrowseFolder
            // 
            this._guiBrowseFolder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._guiBrowseFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this._guiBrowseFolder.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiBrowseFolder.Location = new System.Drawing.Point(299, 44);
            this._guiBrowseFolder.Name = "_guiBrowseFolder";
            this._guiBrowseFolder.Size = new System.Drawing.Size(36, 25);
            this._guiBrowseFolder.TabIndex = 4;
            this._guiBrowseFolder.Text = "...";
            this._guiBrowseFolder.UseVisualStyleBackColor = false;
            this._guiBrowseFolder.Click += new System.EventHandler(this.GuiBrowseFolder_Click);
            // 
            // _guiSelectedChRecordFolder
            // 
            this._guiSelectedChRecordFolder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._guiSelectedChRecordFolder.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiSelectedChRecordFolder.Location = new System.Drawing.Point(5, 31);
            this._guiSelectedChRecordFolder.Multiline = true;
            this._guiSelectedChRecordFolder.Name = "_guiSelectedChRecordFolder";
            this._guiSelectedChRecordFolder.Size = new System.Drawing.Size(275, 38);
            this._guiSelectedChRecordFolder.TabIndex = 3;
            this._guiSelectedChRecordFolder.Text = "D:\\Data";
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Cambria", 10.5F);
            label2.Location = new System.Drawing.Point(29, 84);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(82, 16);
            label2.TabIndex = 25;
            label2.Text = "FileNumber";
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.SystemColors.Control;
            label1.Font = new System.Drawing.Font("Cambria", 10.5F);
            label1.Location = new System.Drawing.Point(591, 456);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(75, 16);
            label1.TabIndex = 25;
            label1.Text = "TotalSpeed";
            // 
            // label3
            // 
            label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label3.AutoSize = true;
            label3.BackColor = System.Drawing.SystemColors.Control;
            label3.Font = new System.Drawing.Font("Cambria", 10.5F);
            label3.Location = new System.Drawing.Point(825, 456);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(60, 16);
            label3.TabIndex = 25;
            label3.Text = "Mbyte/S";
            // 
            // label4
            // 
            label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Cambria", 10.5F);
            label4.Location = new System.Drawing.Point(29, 135);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(105, 16);
            label4.TabIndex = 25;
            label4.Text = "NumberOfBlock";
            // 
            // label6
            // 
            label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Cambria", 10.5F);
            label6.Location = new System.Drawing.Point(29, 38);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(41, 16);
            label6.TabIndex = 25;
            label6.Text = "Mode";
            // 
            // _guiFileView
            // 
            this._guiFileView.AllowUserToAddRows = false;
            this._guiFileView.AllowUserToDeleteRows = false;
            this._guiFileView.AllowUserToResizeColumns = false;
            this._guiFileView.AllowUserToResizeRows = false;
            this._guiFileView.Anchor = System.Windows.Forms.AnchorStyles.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._guiFileView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this._guiFileView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._guiFileView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Count,
            this.BlockSize,
            this.FileFolder});
            this._guiFileView.Location = new System.Drawing.Point(2, 87);
            this._guiFileView.MultiSelect = false;
            this._guiFileView.Name = "_guiFileView";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._guiFileView.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this._guiFileView.RowHeadersVisible = false;
            this._guiFileView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this._guiFileView.RowTemplate.Height = 27;
            this._guiFileView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._guiFileView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._guiFileView.Size = new System.Drawing.Size(578, 248);
            this._guiFileView.TabIndex = 8;
            this._guiFileView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.GuiFileView_CellValueChanged);
            // 
            // Count
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Cambria", 10.8F);
            this.Count.DefaultCellStyle = dataGridViewCellStyle11;
            this.Count.HeaderText = "";
            this.Count.Name = "Count";
            this.Count.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Count.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Count.Width = 40;
            // 
            // BlockSize
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Cambria", 10.8F);
            this.BlockSize.DefaultCellStyle = dataGridViewCellStyle12;
            this.BlockSize.HeaderText = "BlockSize";
            this.BlockSize.Items.AddRange(new object[] {
            "4194304",
            "8388608",
            "16777216",
            "33554432",
            "67108864"});
            this.BlockSize.Name = "BlockSize";
            this.BlockSize.Width = 120;
            // 
            // FileFolder
            // 
            this.FileFolder.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileFolder.DefaultCellStyle = dataGridViewCellStyle13;
            this.FileFolder.HeaderText = "FileFolder";
            this.FileFolder.Name = "FileFolder";
            this.FileFolder.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelTitle.BackColor = System.Drawing.Color.DodgerBlue;
            this.labelTitle.Font = new System.Drawing.Font("Cambria", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(-78, 1);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(1094, 83);
            this.labelTitle.TabIndex = 9;
            this.labelTitle.Text = "Raid Speed Test";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _guiNumberOfFiles
            // 
            this._guiNumberOfFiles.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._guiNumberOfFiles.Font = new System.Drawing.Font("Cambria", 15F);
            this._guiNumberOfFiles.Location = new System.Drawing.Point(135, 76);
            this._guiNumberOfFiles.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this._guiNumberOfFiles.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._guiNumberOfFiles.Name = "_guiNumberOfFiles";
            this._guiNumberOfFiles.Size = new System.Drawing.Size(180, 31);
            this._guiNumberOfFiles.TabIndex = 26;
            this._guiNumberOfFiles.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._guiNumberOfFiles.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._guiNumberOfFiles.ValueChanged += new System.EventHandler(this.GuiNumberOfChannels_ValueChanged);
            // 
            // _guiStop
            // 
            this._guiStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._guiStop.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiStop.Location = new System.Drawing.Point(778, 538);
            this._guiStop.Name = "_guiStop";
            this._guiStop.Size = new System.Drawing.Size(101, 33);
            this._guiStop.TabIndex = 27;
            this._guiStop.Text = "Stop";
            this._guiStop.UseVisualStyleBackColor = true;
            this._guiStop.Click += new System.EventHandler(this.GuiStop_Click);
            // 
            // _guiStart
            // 
            this._guiStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._guiStart.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiStart.Location = new System.Drawing.Point(594, 538);
            this._guiStart.Name = "_guiStart";
            this._guiStart.Size = new System.Drawing.Size(101, 33);
            this._guiStart.TabIndex = 28;
            this._guiStart.Text = "Start";
            this._guiStart.UseVisualStyleBackColor = true;
            this._guiStart.Click += new System.EventHandler(this.GuiStart_Click);
            // 
            // _guiSpeedResultView
            // 
            this._guiSpeedResultView.AllowUserToAddRows = false;
            this._guiSpeedResultView.AllowUserToDeleteRows = false;
            this._guiSpeedResultView.AllowUserToResizeColumns = false;
            this._guiSpeedResultView.AllowUserToResizeRows = false;
            this._guiSpeedResultView.Anchor = System.Windows.Forms.AnchorStyles.None;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._guiSpeedResultView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this._guiSpeedResultView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._guiSpeedResultView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileCount,
            this.Speed});
            this._guiSpeedResultView.Location = new System.Drawing.Point(12, 341);
            this._guiSpeedResultView.MultiSelect = false;
            this._guiSpeedResultView.Name = "_guiSpeedResultView";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._guiSpeedResultView.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this._guiSpeedResultView.RowHeadersVisible = false;
            this._guiSpeedResultView.RowHeadersWidth = 70;
            this._guiSpeedResultView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this._guiSpeedResultView.RowTemplate.Height = 27;
            this._guiSpeedResultView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._guiSpeedResultView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._guiSpeedResultView.Size = new System.Drawing.Size(567, 279);
            this._guiSpeedResultView.TabIndex = 29;
            // 
            // FileCount
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Cambria", 10.8F);
            this.FileCount.DefaultCellStyle = dataGridViewCellStyle16;
            this.FileCount.HeaderText = "";
            this.FileCount.Name = "FileCount";
            this.FileCount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FileCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FileCount.Width = 40;
            // 
            // Speed
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Speed.DefaultCellStyle = dataGridViewCellStyle17;
            this.Speed.HeaderText = "Speed(M)";
            this.Speed.Name = "Speed";
            this.Speed.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Speed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Speed.Width = 200;
            // 
            // _guiTotalSpeed
            // 
            this._guiTotalSpeed.Font = new System.Drawing.Font("Cambria", 12F);
            this._guiTotalSpeed.Location = new System.Drawing.Point(683, 451);
            this._guiTotalSpeed.Name = "_guiTotalSpeed";
            this._guiTotalSpeed.Size = new System.Drawing.Size(124, 26);
            this._guiTotalSpeed.TabIndex = 30;
            this._guiTotalSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _guiTimerForStatus
            // 
            this._guiTimerForStatus.Interval = 200;
            this._guiTimerForStatus.Tick += new System.EventHandler(this.GuiTimerForStatus_Tick);
            // 
            // _guiStatus
            // 
            this._guiStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._guiToolProgressBar});
            this._guiStatus.Location = new System.Drawing.Point(0, 623);
            this._guiStatus.Name = "_guiStatus";
            this._guiStatus.Size = new System.Drawing.Size(945, 22);
            this._guiStatus.TabIndex = 31;
            this._guiStatus.Text = "statusStrip1";
            // 
            // _guiToolProgressBar
            // 
            this._guiToolProgressBar.Name = "_guiToolProgressBar";
            this._guiToolProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // _guiWriteLength
            // 
            this._guiWriteLength.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._guiWriteLength.Font = new System.Drawing.Font("Cambria", 15F);
            this._guiWriteLength.Location = new System.Drawing.Point(135, 126);
            this._guiWriteLength.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this._guiWriteLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._guiWriteLength.Name = "_guiWriteLength";
            this._guiWriteLength.Size = new System.Drawing.Size(180, 31);
            this._guiWriteLength.TabIndex = 26;
            this._guiWriteLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._guiWriteLength.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._guiWriteLength.ValueChanged += new System.EventHandler(this.GuiNumberOfChannels_ValueChanged);
            // 
            // _guiDisableBuffering
            // 
            this._guiDisableBuffering.AutoSize = true;
            this._guiDisableBuffering.Checked = true;
            this._guiDisableBuffering.CheckState = System.Windows.Forms.CheckState.Checked;
            this._guiDisableBuffering.Font = new System.Drawing.Font("Cambria", 10.5F);
            this._guiDisableBuffering.Location = new System.Drawing.Point(32, 175);
            this._guiDisableBuffering.Name = "_guiDisableBuffering";
            this._guiDisableBuffering.Size = new System.Drawing.Size(129, 20);
            this._guiDisableBuffering.TabIndex = 33;
            this._guiDisableBuffering.Text = "DisableBuffering";
            this._guiDisableBuffering.UseVisualStyleBackColor = true;
            // 
            // group
            // 
            this.group.Controls.Add(this._guiWriteMode);
            this.group.Controls.Add(this._guiWriteLength);
            this.group.Controls.Add(this._guiDisableBuffering);
            this.group.Controls.Add(label6);
            this.group.Controls.Add(label2);
            this.group.Controls.Add(label4);
            this.group.Controls.Add(this._guiNumberOfFiles);
            this.group.Font = new System.Drawing.Font("Cambria", 10.5F);
            this.group.Location = new System.Drawing.Point(584, 87);
            this.group.Name = "group";
            this.group.Size = new System.Drawing.Size(354, 212);
            this.group.TabIndex = 34;
            this.group.TabStop = false;
            this.group.Text = "ParamSetting";
            // 
            // _guiWriteMode
            // 
            this._guiWriteMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._guiWriteMode.Font = new System.Drawing.Font("Cambria", 15F);
            this._guiWriteMode.Items.AddRange(new object[] {
            "并行写入",
            "串行写入"});
            this._guiWriteMode.Location = new System.Drawing.Point(135, 29);
            this._guiWriteMode.Margin = new System.Windows.Forms.Padding(0);
            this._guiWriteMode.Name = "_guiWriteMode";
            this._guiWriteMode.Size = new System.Drawing.Size(178, 31);
            this._guiWriteMode.TabIndex = 34;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(945, 645);
            this.Controls.Add(this.group);
            this.Controls.Add(this._guiStatus);
            this.Controls.Add(this._guiTotalSpeed);
            this.Controls.Add(this._guiSpeedResultView);
            this.Controls.Add(this._guiStop);
            this.Controls.Add(this._guiStart);
            this.Controls.Add(label3);
            this.Controls.Add(label1);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this._guiFileView);
            this.Controls.Add(_guiSelectedChRecFolderSetting);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Write To File Speed Test";
            this.Load += new System.EventHandler(this.MainForm_Load);
            _guiSelectedChRecFolderSetting.ResumeLayout(false);
            _guiSelectedChRecFolderSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._guiFileView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiNumberOfFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiSpeedResultView)).EndInit();
            this._guiStatus.ResumeLayout(false);
            this._guiStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._guiWriteLength)).EndInit();
            this.group.ResumeLayout(false);
            this.group.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView _guiFileView;
        private System.Windows.Forms.Button _guiBrowseFolder;
        private System.Windows.Forms.TextBox _guiSelectedChRecordFolder;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.NumericUpDown _guiNumberOfFiles;
        private System.Windows.Forms.Button _guiStop;
        private System.Windows.Forms.Button _guiStart;
        private System.Windows.Forms.DataGridView _guiSpeedResultView;
        private System.Windows.Forms.FolderBrowserDialog _guiFolderBrowserDialog;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Speed;
        private System.Windows.Forms.TextBox _guiTotalSpeed;
        private System.Windows.Forms.Timer _guiTimerForStatus;
        private System.Windows.Forms.StatusStrip _guiStatus;
        private System.Windows.Forms.ToolStripProgressBar _guiToolProgressBar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.DataGridViewComboBoxColumn BlockSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileFolder;
        private System.Windows.Forms.NumericUpDown _guiWriteLength;
        private System.Windows.Forms.CheckBox _guiDisableBuffering;
        private System.Windows.Forms.GroupBox group;
        private System.Windows.Forms.ComboBox _guiWriteMode;
    }
}

