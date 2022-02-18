namespace FixFreqStreamFileExampleWriteComplex
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
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            this.labelTitle = new System.Windows.Forms.Label();
            this._guiSignalParam = new System.Windows.Forms.GroupBox();
            this._guiLevel = new System.Windows.Forms.NumericUpDown();
            this._guiSampleRate = new System.Windows.Forms.NumericUpDown();
            this._guiNumberOfChannels = new System.Windows.Forms.NumericUpDown();
            this._guiFileParam = new System.Windows.Forms.GroupBox();
            this._guiDisableBuffering = new System.Windows.Forms.CheckBox();
            this._guiBrowseFolder = new System.Windows.Forms.Button();
            this._guiDestinationFolder = new System.Windows.Forms.TextBox();
            this._guiFileLength = new System.Windows.Forms.NumericUpDown();
            this._guiFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this._guiStart = new System.Windows.Forms.Button();
            this._guiStop = new System.Windows.Forms.Button();
            this._guiProgressBar = new System.Windows.Forms.ProgressBar();
            this._guiProgressValue = new System.Windows.Forms.TextBox();
            this._bgWorker = new System.ComponentModel.BackgroundWorker();
            label3 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            this._guiSignalParam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._guiLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiSampleRate)).BeginInit();
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
            label1.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(477, 29);
            label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(59, 28);
            label1.TabIndex = 10;
            label1.Text = "MHz";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new System.Drawing.Point(279, 29);
            label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(135, 28);
            label2.TabIndex = 9;
            label2.Text = "SampleRate";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label4.Location = new System.Drawing.Point(554, 29);
            label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(68, 28);
            label4.TabIndex = 9;
            label4.Text = "Level";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label5.Location = new System.Drawing.Point(708, 29);
            label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(60, 28);
            label5.TabIndex = 10;
            label5.Text = "dBm";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label10.Location = new System.Drawing.Point(12, 84);
            label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(127, 28);
            label10.TabIndex = 7;
            label10.Text = "File Length";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label6.Location = new System.Drawing.Point(12, 31);
            label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(131, 28);
            label6.TabIndex = 7;
            label6.Text = "Folder Path";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label7.Location = new System.Drawing.Point(264, 84);
            label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(45, 28);
            label7.TabIndex = 10;
            label7.Text = "sec";
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
            this.labelTitle.TabIndex = 4;
            this.labelTitle.Text = "FixFrequencyStream File Write Example (Complex)";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _guiSignalParam
            // 
            this._guiSignalParam.Controls.Add(this._guiLevel);
            this._guiSignalParam.Controls.Add(label5);
            this._guiSignalParam.Controls.Add(this._guiSampleRate);
            this._guiSignalParam.Controls.Add(label4);
            this._guiSignalParam.Controls.Add(label1);
            this._guiSignalParam.Controls.Add(label2);
            this._guiSignalParam.Controls.Add(this._guiNumberOfChannels);
            this._guiSignalParam.Controls.Add(label3);
            this._guiSignalParam.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiSignalParam.Location = new System.Drawing.Point(12, 87);
            this._guiSignalParam.Name = "_guiSignalParam";
            this._guiSignalParam.Size = new System.Drawing.Size(760, 68);
            this._guiSignalParam.TabIndex = 5;
            this._guiSignalParam.TabStop = false;
            this._guiSignalParam.Text = "Signal Param";
            // 
            // _guiLevel
            // 
            this._guiLevel.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiLevel.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._guiLevel.Location = new System.Drawing.Point(604, 24);
            this._guiLevel.Margin = new System.Windows.Forms.Padding(2);
            this._guiLevel.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this._guiLevel.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this._guiLevel.Name = "_guiLevel";
            this._guiLevel.Size = new System.Drawing.Size(100, 41);
            this._guiLevel.TabIndex = 8;
            this._guiLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _guiSampleRate
            // 
            this._guiSampleRate.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiSampleRate.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._guiSampleRate.Location = new System.Drawing.Point(373, 24);
            this._guiSampleRate.Margin = new System.Windows.Forms.Padding(2);
            this._guiSampleRate.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this._guiSampleRate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this._guiSampleRate.Name = "_guiSampleRate";
            this._guiSampleRate.Size = new System.Drawing.Size(100, 41);
            this._guiSampleRate.TabIndex = 8;
            this._guiSampleRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._guiSampleRate.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
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
            // 
            // _guiFileParam
            // 
            this._guiFileParam.Controls.Add(this._guiDisableBuffering);
            this._guiFileParam.Controls.Add(this._guiBrowseFolder);
            this._guiFileParam.Controls.Add(this._guiDestinationFolder);
            this._guiFileParam.Controls.Add(this._guiFileLength);
            this._guiFileParam.Controls.Add(label6);
            this._guiFileParam.Controls.Add(label7);
            this._guiFileParam.Controls.Add(label10);
            this._guiFileParam.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiFileParam.Location = new System.Drawing.Point(12, 161);
            this._guiFileParam.Name = "_guiFileParam";
            this._guiFileParam.Size = new System.Drawing.Size(760, 121);
            this._guiFileParam.TabIndex = 5;
            this._guiFileParam.TabStop = false;
            this._guiFileParam.Text = "File Param";
            // 
            // _guiDisableBuffering
            // 
            this._guiDisableBuffering.AutoSize = true;
            this._guiDisableBuffering.Checked = true;
            this._guiDisableBuffering.CheckState = System.Windows.Forms.CheckState.Checked;
            this._guiDisableBuffering.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiDisableBuffering.Location = new System.Drawing.Point(373, 83);
            this._guiDisableBuffering.Name = "_guiDisableBuffering";
            this._guiDisableBuffering.Size = new System.Drawing.Size(216, 32);
            this._guiDisableBuffering.TabIndex = 11;
            this._guiDisableBuffering.Text = "Disable Buffering";
            this._guiDisableBuffering.UseVisualStyleBackColor = true;
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
            this._guiFileLength.DecimalPlaces = 1;
            this._guiFileLength.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiFileLength.Location = new System.Drawing.Point(168, 79);
            this._guiFileLength.Margin = new System.Windows.Forms.Padding(2);
            this._guiFileLength.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this._guiFileLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
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
            // _guiStart
            // 
            this._guiStart.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiStart.Location = new System.Drawing.Point(548, 298);
            this._guiStart.Name = "_guiStart";
            this._guiStart.Size = new System.Drawing.Size(101, 33);
            this._guiStart.TabIndex = 6;
            this._guiStart.Text = "Start";
            this._guiStart.UseVisualStyleBackColor = true;
            this._guiStart.Click += new System.EventHandler(this.GuiStart_Click);
            // 
            // _guiStop
            // 
            this._guiStop.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiStop.Location = new System.Drawing.Point(671, 298);
            this._guiStop.Name = "_guiStop";
            this._guiStop.Size = new System.Drawing.Size(101, 33);
            this._guiStop.TabIndex = 6;
            this._guiStop.Text = "Stop";
            this._guiStop.UseVisualStyleBackColor = true;
            this._guiStop.Click += new System.EventHandler(this.GuiStop_Click);
            // 
            // _guiProgressBar
            // 
            this._guiProgressBar.Location = new System.Drawing.Point(12, 298);
            this._guiProgressBar.Name = "_guiProgressBar";
            this._guiProgressBar.Size = new System.Drawing.Size(324, 33);
            this._guiProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this._guiProgressBar.TabIndex = 7;
            // 
            // _guiProgressValue
            // 
            this._guiProgressValue.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiProgressValue.Location = new System.Drawing.Point(342, 302);
            this._guiProgressValue.Name = "_guiProgressValue";
            this._guiProgressValue.ReadOnly = true;
            this._guiProgressValue.Size = new System.Drawing.Size(70, 36);
            this._guiProgressValue.TabIndex = 8;
            this._guiProgressValue.TabStop = false;
            this._guiProgressValue.Text = "0 %";
            this._guiProgressValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.ClientSize = new System.Drawing.Size(784, 345);
            this.Controls.Add(this._guiProgressValue);
            this.Controls.Add(this._guiProgressBar);
            this.Controls.Add(this._guiStop);
            this.Controls.Add(this._guiStart);
            this.Controls.Add(this._guiFileParam);
            this.Controls.Add(this._guiSignalParam);
            this.Controls.Add(this.labelTitle);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generation FixFreqStream Complex";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this._guiSignalParam.ResumeLayout(false);
            this._guiSignalParam.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._guiLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiSampleRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiNumberOfChannels)).EndInit();
            this._guiFileParam.ResumeLayout(false);
            this._guiFileParam.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._guiFileLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.NumericUpDown _guiNumberOfChannels;
        private System.Windows.Forms.NumericUpDown _guiSampleRate;
        private System.Windows.Forms.NumericUpDown _guiLevel;
        private System.Windows.Forms.NumericUpDown _guiFileLength;
        private System.Windows.Forms.Button _guiBrowseFolder;
        private System.Windows.Forms.TextBox _guiDestinationFolder;
        private System.Windows.Forms.FolderBrowserDialog _guiFolderBrowserDialog;
        private System.Windows.Forms.GroupBox _guiSignalParam;
        private System.Windows.Forms.GroupBox _guiFileParam;
        private System.Windows.Forms.CheckBox _guiDisableBuffering;
        private System.Windows.Forms.Button _guiStart;
        private System.Windows.Forms.Button _guiStop;
        private System.Windows.Forms.ProgressBar _guiProgressBar;
        private System.Windows.Forms.TextBox _guiProgressValue;
        private System.ComponentModel.BackgroundWorker _bgWorker;
    }
}

