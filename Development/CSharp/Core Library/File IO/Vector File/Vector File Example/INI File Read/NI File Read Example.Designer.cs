namespace IniFileReadExample
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelTitle = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._guiReadFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._guiFilePath = new System.Windows.Forms.TextBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this._guiDataGridView = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._guiDeleteINI = new System.Windows.Forms.Button();
            this._guiAddINI = new System.Windows.Forms.Button();
            this._guiValue = new System.Windows.Forms.TextBox();
            this._guiKey = new System.Windows.Forms.TextBox();
            this._guiSection = new System.Windows.Forms.TextBox();
            this._guiFileBrowseDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._guiDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.labelTitle);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(917, 569);
            this.splitContainer1.SplitterDistance = 77;
            this.splitContainer1.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.BackColor = System.Drawing.Color.DodgerBlue;
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitle.Font = new System.Drawing.Font("Cambria", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(917, 77);
            this.labelTitle.TabIndex = 10;
            this.labelTitle.Text = "Read INI File Example";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._guiReadFile);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this._guiFilePath);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(917, 488);
            this.splitContainer2.SplitterDistance = 81;
            this.splitContainer2.TabIndex = 0;
            // 
            // _guiReadFile
            // 
            this._guiReadFile.Font = new System.Drawing.Font("思源宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._guiReadFile.Location = new System.Drawing.Point(814, 13);
            this._guiReadFile.Name = "_guiReadFile";
            this._guiReadFile.Size = new System.Drawing.Size(100, 41);
            this._guiReadFile.TabIndex = 2;
            this._guiReadFile.Text = "选择文件";
            this._guiReadFile.UseVisualStyleBackColor = true;
            this._guiReadFile.Click += new System.EventHandler(this.GuiReadFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("思源宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "File:";
            // 
            // _guiFilePath
            // 
            this._guiFilePath.Font = new System.Drawing.Font("思源宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._guiFilePath.Location = new System.Drawing.Point(59, 11);
            this._guiFilePath.Multiline = true;
            this._guiFilePath.Name = "_guiFilePath";
            this._guiFilePath.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._guiFilePath.Size = new System.Drawing.Size(740, 47);
            this._guiFilePath.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this._guiDataGridView);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.label4);
            this.splitContainer3.Panel2.Controls.Add(this.label3);
            this.splitContainer3.Panel2.Controls.Add(this.label2);
            this.splitContainer3.Panel2.Controls.Add(this._guiDeleteINI);
            this.splitContainer3.Panel2.Controls.Add(this._guiAddINI);
            this.splitContainer3.Panel2.Controls.Add(this._guiValue);
            this.splitContainer3.Panel2.Controls.Add(this._guiKey);
            this.splitContainer3.Panel2.Controls.Add(this._guiSection);
            this.splitContainer3.Size = new System.Drawing.Size(917, 403);
            this.splitContainer3.SplitterDistance = 337;
            this.splitContainer3.TabIndex = 0;
            // 
            // _guiDataGridView
            // 
            this._guiDataGridView.AllowUserToAddRows = false;
            this._guiDataGridView.AllowUserToDeleteRows = false;
            this._guiDataGridView.AllowUserToResizeColumns = false;
            this._guiDataGridView.AllowUserToResizeRows = false;
            this._guiDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._guiDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._guiDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._guiDataGridView.Cursor = System.Windows.Forms.Cursors.Default;
            this._guiDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this._guiDataGridView.Location = new System.Drawing.Point(3, 0);
            this._guiDataGridView.Name = "_guiDataGridView";
            this._guiDataGridView.RowHeadersVisible = false;
            this._guiDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this._guiDataGridView.RowTemplate.Height = 23;
            this._guiDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._guiDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this._guiDataGridView.Size = new System.Drawing.Size(902, 334);
            this._guiDataGridView.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("思源宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(6, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 20);
            this.label4.TabIndex = 20;
            this.label4.Text = "Section";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("思源宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(229, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 20);
            this.label3.TabIndex = 19;
            this.label3.Text = "Key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("思源宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(406, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 20);
            this.label2.TabIndex = 18;
            this.label2.Text = "Value";
            // 
            // _guiDeleteINI
            // 
            this._guiDeleteINI.Font = new System.Drawing.Font("思源宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._guiDeleteINI.Location = new System.Drawing.Point(754, 9);
            this._guiDeleteINI.Name = "_guiDeleteINI";
            this._guiDeleteINI.Size = new System.Drawing.Size(107, 41);
            this._guiDeleteINI.TabIndex = 17;
            this._guiDeleteINI.Text = "删除";
            this._guiDeleteINI.UseVisualStyleBackColor = true;
            this._guiDeleteINI.Click += new System.EventHandler(this.GuiDeleteINI_Click);
            // 
            // _guiAddINI
            // 
            this._guiAddINI.Font = new System.Drawing.Font("思源宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._guiAddINI.Location = new System.Drawing.Point(612, 9);
            this._guiAddINI.Name = "_guiAddINI";
            this._guiAddINI.Size = new System.Drawing.Size(118, 41);
            this._guiAddINI.TabIndex = 16;
            this._guiAddINI.Text = "增加";
            this._guiAddINI.UseVisualStyleBackColor = true;
            this._guiAddINI.Click += new System.EventHandler(this._GuiAddINI_Click);
            // 
            // _guiValue
            // 
            this._guiValue.Font = new System.Drawing.Font("思源宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._guiValue.Location = new System.Drawing.Point(465, 20);
            this._guiValue.Multiline = true;
            this._guiValue.Name = "_guiValue";
            this._guiValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._guiValue.Size = new System.Drawing.Size(123, 30);
            this._guiValue.TabIndex = 15;
            // 
            // _guiKey
            // 
            this._guiKey.Font = new System.Drawing.Font("思源宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._guiKey.Location = new System.Drawing.Point(277, 20);
            this._guiKey.Multiline = true;
            this._guiKey.Name = "_guiKey";
            this._guiKey.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._guiKey.Size = new System.Drawing.Size(123, 30);
            this._guiKey.TabIndex = 14;
            // 
            // _guiSection
            // 
            this._guiSection.Font = new System.Drawing.Font("思源宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._guiSection.Location = new System.Drawing.Point(78, 20);
            this._guiSection.Multiline = true;
            this._guiSection.Name = "_guiSection";
            this._guiSection.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._guiSection.Size = new System.Drawing.Size(123, 30);
            this._guiSection.TabIndex = 13;
            // 
            // _guiFileBrowseDialog
            // 
            this._guiFileBrowseDialog.FileName = "CC";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 569);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "Read INI File Example";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._guiDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button _guiReadFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _guiFilePath;
        private System.Windows.Forms.OpenFileDialog _guiFileBrowseDialog;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataGridView _guiDataGridView;
        private System.Windows.Forms.TextBox _guiSection;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button _guiDeleteINI;
        private System.Windows.Forms.Button _guiAddINI;
        private System.Windows.Forms.TextBox _guiValue;
        private System.Windows.Forms.TextBox _guiKey;
    }
}

