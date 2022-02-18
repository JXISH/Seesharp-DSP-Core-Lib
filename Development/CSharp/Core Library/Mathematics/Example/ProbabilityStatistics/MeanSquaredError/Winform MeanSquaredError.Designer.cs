namespace SeesharpTools.JXI.Mathematics.example
{
    partial class MeanSquaredErrorForm
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
            this.textBoxReslut = new System.Windows.Forms.TextBox();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.buttonEfficiencyTesting = new System.Windows.Forms.Button();
            this.textBoxEfficiencyTesting = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.OriginalDataA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OriginalDataB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nudTestTimes = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nudLength = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTestTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxReslut
            // 
            this.textBoxReslut.Font = new System.Drawing.Font("宋体", 9F);
            this.textBoxReslut.Location = new System.Drawing.Point(43, 172);
            this.textBoxReslut.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxReslut.Name = "textBoxReslut";
            this.textBoxReslut.Size = new System.Drawing.Size(199, 25);
            this.textBoxReslut.TabIndex = 17;
            // 
            // buttonCalculate
            // 
            this.buttonCalculate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCalculate.Location = new System.Drawing.Point(43, 230);
            this.buttonCalculate.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(199, 36);
            this.buttonCalculate.TabIndex = 12;
            this.buttonCalculate.Text = "GenerateAndAnalyze";
            this.buttonCalculate.UseVisualStyleBackColor = false;
            this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);
            // 
            // buttonEfficiencyTesting
            // 
            this.buttonEfficiencyTesting.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonEfficiencyTesting.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonEfficiencyTesting.Location = new System.Drawing.Point(45, 503);
            this.buttonEfficiencyTesting.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEfficiencyTesting.Name = "buttonEfficiencyTesting";
            this.buttonEfficiencyTesting.Size = new System.Drawing.Size(199, 36);
            this.buttonEfficiencyTesting.TabIndex = 20;
            this.buttonEfficiencyTesting.Text = "Test";
            this.buttonEfficiencyTesting.UseVisualStyleBackColor = false;
            this.buttonEfficiencyTesting.Click += new System.EventHandler(this.buttonEfficiencyTesting_Click);
            // 
            // textBoxEfficiencyTesting
            // 
            this.textBoxEfficiencyTesting.Font = new System.Drawing.Font("宋体", 9F);
            this.textBoxEfficiencyTesting.Location = new System.Drawing.Point(45, 435);
            this.textBoxEfficiencyTesting.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxEfficiencyTesting.Name = "textBoxEfficiencyTesting";
            this.textBoxEfficiencyTesting.Size = new System.Drawing.Size(199, 25);
            this.textBoxEfficiencyTesting.TabIndex = 21;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OriginalDataA,
            this.OriginalDataB});
            this.dataGridView1.Location = new System.Drawing.Point(283, 77);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(503, 492);
            this.dataGridView1.TabIndex = 22;
            // 
            // OriginalDataA
            // 
            this.OriginalDataA.HeaderText = "RawData A(random)";
            this.OriginalDataA.Name = "OriginalDataA";
            this.OriginalDataA.Width = 170;
            // 
            // OriginalDataB
            // 
            this.OriginalDataB.HeaderText = "RawData B(random)";
            this.OriginalDataB.Name = "OriginalDataB";
            this.OriginalDataB.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.OriginalDataB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OriginalDataB.Width = 170;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F);
            this.label4.Location = new System.Drawing.Point(42, 153);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "MSE Calculation:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F);
            this.label6.Location = new System.Drawing.Point(42, 416);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "Efficiency:ms";
            // 
            // nudTestTimes
            // 
            this.nudTestTimes.Location = new System.Drawing.Point(45, 362);
            this.nudTestTimes.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudTestTimes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTestTimes.Name = "nudTestTimes";
            this.nudTestTimes.Size = new System.Drawing.Size(199, 25);
            this.nudTestTimes.TabIndex = 23;
            this.nudTestTimes.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F);
            this.label1.Location = new System.Drawing.Point(42, 344);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "LoopTimes";
            // 
            // nudLength
            // 
            this.nudLength.Location = new System.Drawing.Point(45, 104);
            this.nudLength.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudLength.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudLength.Name = "nudLength";
            this.nudLength.Size = new System.Drawing.Size(199, 25);
            this.nudLength.TabIndex = 23;
            this.nudLength.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudLength.ValueChanged += new System.EventHandler(this.buttonCalculate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F);
            this.label2.Location = new System.Drawing.Point(42, 86);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "RawDataLenth";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(76, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(636, 44);
            this.label5.TabIndex = 24;
            this.label5.Text = "MeanSquaredError Calculation";
            // 
            // MeanSquaredErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(845, 614);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudLength);
            this.Controls.Add(this.nudTestTimes);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBoxEfficiencyTesting);
            this.Controls.Add(this.buttonEfficiencyTesting);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxReslut);
            this.Controls.Add(this.buttonCalculate);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MeanSquaredErrorForm";
            this.Text = "MeanSquareErrorCalculationDemo";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTestTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxReslut;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.Button buttonEfficiencyTesting;
        private System.Windows.Forms.TextBox textBoxEfficiencyTesting;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudTestTimes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudLength;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn OriginalDataA;
        private System.Windows.Forms.DataGridViewTextBoxColumn OriginalDataB;
        private System.Windows.Forms.Label label5;
    }
}

