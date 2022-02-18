namespace WavFileGeneratorTestPanel
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
            System.Windows.Forms.Label labelChannelTitle;
            System.Windows.Forms.Label labelFreqTitle;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            this._guiSelectFolder = new System.Windows.Forms.Button();
            this.labelFilePath = new System.Windows.Forms.Label();
            this._guiRecorderFolder = new System.Windows.Forms.TextBox();
            this._guiChannelSelection = new System.Windows.Forms.ComboBox();
            this._guiChannelOneFrequency = new System.Windows.Forms.NumericUpDown();
            this._guiChannelTwoFrequency = new System.Windows.Forms.NumericUpDown();
            this._guiFileTimeLength = new System.Windows.Forms.NumericUpDown();
            this._guiAudioSampleRate = new System.Windows.Forms.ComboBox();
            this._guiGenerate = new System.Windows.Forms.Button();
            this._guiRecFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this._guiParaSetting = new System.Windows.Forms.GroupBox();
            labelChannelTitle = new System.Windows.Forms.Label();
            labelFreqTitle = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._guiChannelOneFrequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiChannelTwoFrequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiFileTimeLength)).BeginInit();
            this._guiParaSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelChannelTitle
            // 
            labelChannelTitle.AutoSize = true;
            labelChannelTitle.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelChannelTitle.Location = new System.Drawing.Point(22, 27);
            labelChannelTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            labelChannelTitle.Name = "labelChannelTitle";
            labelChannelTitle.Size = new System.Drawing.Size(58, 17);
            labelChannelTitle.TabIndex = 15;
            labelChannelTitle.Text = "Channel";
            // 
            // labelFreqTitle
            // 
            labelFreqTitle.AutoSize = true;
            labelFreqTitle.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelFreqTitle.Location = new System.Drawing.Point(22, 87);
            labelFreqTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            labelFreqTitle.Name = "labelFreqTitle";
            labelFreqTitle.Size = new System.Drawing.Size(187, 17);
            labelFreqTitle.TabIndex = 18;
            labelFreqTitle.Text = "ChannelOneFrequency(kHz)";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(22, 146);
            label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(191, 17);
            label1.TabIndex = 18;
            label1.Text = "ChannelTwoFrequency(kHz)";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label4.Location = new System.Drawing.Point(22, 205);
            label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(144, 17);
            label4.TabIndex = 18;
            label4.Text = "FileTimeLength（s）";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label5.Location = new System.Drawing.Point(22, 264);
            label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(132, 17);
            label5.TabIndex = 15;
            label5.Text = "SamleRate（Sa/s）";
            // 
            // _guiSelectFolder
            // 
            this._guiSelectFolder.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiSelectFolder.Location = new System.Drawing.Point(460, 12);
            this._guiSelectFolder.Margin = new System.Windows.Forms.Padding(2);
            this._guiSelectFolder.Name = "_guiSelectFolder";
            this._guiSelectFolder.Size = new System.Drawing.Size(94, 28);
            this._guiSelectFolder.TabIndex = 13;
            this._guiSelectFolder.Text = "SelectPath";
            this._guiSelectFolder.UseVisualStyleBackColor = true;
            this._guiSelectFolder.Click += new System.EventHandler(this._guiSelectFolder_Click);
            // 
            // labelFilePath
            // 
            this.labelFilePath.AutoSize = true;
            this.labelFilePath.Font = new System.Drawing.Font("Cambria", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFilePath.Location = new System.Drawing.Point(6, 14);
            this.labelFilePath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelFilePath.Name = "labelFilePath";
            this.labelFilePath.Size = new System.Drawing.Size(83, 22);
            this.labelFilePath.TabIndex = 12;
            this.labelFilePath.Text = "SavePath";
            // 
            // _guiRecorderFolder
            // 
            this._guiRecorderFolder.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiRecorderFolder.Location = new System.Drawing.Point(90, 11);
            this._guiRecorderFolder.Margin = new System.Windows.Forms.Padding(2);
            this._guiRecorderFolder.Multiline = true;
            this._guiRecorderFolder.Name = "_guiRecorderFolder";
            this._guiRecorderFolder.Size = new System.Drawing.Size(316, 30);
            this._guiRecorderFolder.TabIndex = 11;
            this._guiRecorderFolder.Text = "D:\\Data";
            // 
            // _guiChannelSelection
            // 
            this._guiChannelSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._guiChannelSelection.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiChannelSelection.FormattingEnabled = true;
            this._guiChannelSelection.Items.AddRange(new object[] {
            "1",
            "2"});
            this._guiChannelSelection.Location = new System.Drawing.Point(22, 52);
            this._guiChannelSelection.Margin = new System.Windows.Forms.Padding(2);
            this._guiChannelSelection.Name = "_guiChannelSelection";
            this._guiChannelSelection.Size = new System.Drawing.Size(173, 27);
            this._guiChannelSelection.TabIndex = 14;
            this._guiChannelSelection.SelectedIndexChanged += new System.EventHandler(this._guiChannelSelection_SelectedIndexChanged);
            // 
            // _guiChannelOneFrequency
            // 
            this._guiChannelOneFrequency.DecimalPlaces = 3;
            this._guiChannelOneFrequency.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiChannelOneFrequency.Location = new System.Drawing.Point(22, 112);
            this._guiChannelOneFrequency.Margin = new System.Windows.Forms.Padding(2);
            this._guiChannelOneFrequency.Maximum = new decimal(new int[] {
            40000,
            0,
            0,
            0});
            this._guiChannelOneFrequency.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this._guiChannelOneFrequency.Name = "_guiChannelOneFrequency";
            this._guiChannelOneFrequency.Size = new System.Drawing.Size(173, 26);
            this._guiChannelOneFrequency.TabIndex = 16;
            this._guiChannelOneFrequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._guiChannelOneFrequency.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // _guiChannelTwoFrequency
            // 
            this._guiChannelTwoFrequency.DecimalPlaces = 3;
            this._guiChannelTwoFrequency.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiChannelTwoFrequency.Location = new System.Drawing.Point(22, 171);
            this._guiChannelTwoFrequency.Margin = new System.Windows.Forms.Padding(2);
            this._guiChannelTwoFrequency.Maximum = new decimal(new int[] {
            40000,
            0,
            0,
            0});
            this._guiChannelTwoFrequency.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this._guiChannelTwoFrequency.Name = "_guiChannelTwoFrequency";
            this._guiChannelTwoFrequency.Size = new System.Drawing.Size(173, 26);
            this._guiChannelTwoFrequency.TabIndex = 16;
            this._guiChannelTwoFrequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._guiChannelTwoFrequency.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // _guiFileTimeLength
            // 
            this._guiFileTimeLength.DecimalPlaces = 3;
            this._guiFileTimeLength.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiFileTimeLength.Location = new System.Drawing.Point(22, 230);
            this._guiFileTimeLength.Margin = new System.Windows.Forms.Padding(2);
            this._guiFileTimeLength.Maximum = new decimal(new int[] {
            40000,
            0,
            0,
            0});
            this._guiFileTimeLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this._guiFileTimeLength.Name = "_guiFileTimeLength";
            this._guiFileTimeLength.Size = new System.Drawing.Size(173, 26);
            this._guiFileTimeLength.TabIndex = 16;
            this._guiFileTimeLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._guiFileTimeLength.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // _guiAudioSampleRate
            // 
            this._guiAudioSampleRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._guiAudioSampleRate.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._guiAudioSampleRate.FormattingEnabled = true;
            this._guiAudioSampleRate.Items.AddRange(new object[] {
            "6000",
            "8000",
            "11025",
            "16000",
            "22050",
            "32000",
            "32075",
            "44100",
            "48000",
            "96000"});
            this._guiAudioSampleRate.Location = new System.Drawing.Point(22, 289);
            this._guiAudioSampleRate.Margin = new System.Windows.Forms.Padding(2);
            this._guiAudioSampleRate.Name = "_guiAudioSampleRate";
            this._guiAudioSampleRate.Size = new System.Drawing.Size(173, 27);
            this._guiAudioSampleRate.TabIndex = 14;
            // 
            // _guiGenerate
            // 
            this._guiGenerate.Font = new System.Drawing.Font("Cambria", 13.8F);
            this._guiGenerate.Location = new System.Drawing.Point(360, 137);
            this._guiGenerate.Name = "_guiGenerate";
            this._guiGenerate.Size = new System.Drawing.Size(115, 41);
            this._guiGenerate.TabIndex = 19;
            this._guiGenerate.Text = "Generate";
            this._guiGenerate.UseVisualStyleBackColor = true;
            this._guiGenerate.Click += new System.EventHandler(this.GuiGenerate_Click);
            // 
            // _guiParaSetting
            // 
            this._guiParaSetting.Controls.Add(this._guiChannelSelection);
            this._guiParaSetting.Controls.Add(this._guiAudioSampleRate);
            this._guiParaSetting.Controls.Add(this._guiGenerate);
            this._guiParaSetting.Controls.Add(labelChannelTitle);
            this._guiParaSetting.Controls.Add(label5);
            this._guiParaSetting.Controls.Add(labelFreqTitle);
            this._guiParaSetting.Controls.Add(this._guiChannelTwoFrequency);
            this._guiParaSetting.Controls.Add(label1);
            this._guiParaSetting.Controls.Add(this._guiFileTimeLength);
            this._guiParaSetting.Controls.Add(label4);
            this._guiParaSetting.Controls.Add(this._guiChannelOneFrequency);
            this._guiParaSetting.Font = new System.Drawing.Font("Cambria", 13.8F);
            this._guiParaSetting.Location = new System.Drawing.Point(3, 53);
            this._guiParaSetting.Name = "_guiParaSetting";
            this._guiParaSetting.Size = new System.Drawing.Size(551, 324);
            this._guiParaSetting.TabIndex = 22;
            this._guiParaSetting.TabStop = false;
            this._guiParaSetting.Text = "ParaSetting";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 380);
            this.Controls.Add(this._guiParaSetting);
            this.Controls.Add(this._guiSelectFolder);
            this.Controls.Add(this.labelFilePath);
            this.Controls.Add(this._guiRecorderFolder);
            this.Name = "MainForm";
            this.Text = "Wav File Generator Test Panel";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._guiChannelOneFrequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiChannelTwoFrequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._guiFileTimeLength)).EndInit();
            this._guiParaSetting.ResumeLayout(false);
            this._guiParaSetting.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _guiSelectFolder;
        private System.Windows.Forms.Label labelFilePath;
        private System.Windows.Forms.TextBox _guiRecorderFolder;
        private System.Windows.Forms.ComboBox _guiChannelSelection;
        private System.Windows.Forms.NumericUpDown _guiChannelOneFrequency;
        private System.Windows.Forms.NumericUpDown _guiChannelTwoFrequency;
        private System.Windows.Forms.NumericUpDown _guiFileTimeLength;
        private System.Windows.Forms.ComboBox _guiAudioSampleRate;
        private System.Windows.Forms.Button _guiGenerate;
        private System.Windows.Forms.FolderBrowserDialog _guiRecFolderDialog;
        private System.Windows.Forms.GroupBox _guiParaSetting;
    }
}

