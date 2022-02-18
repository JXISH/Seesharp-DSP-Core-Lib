namespace Winform_Statistics
{
    partial class MultiFunctionForm
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
            this.easyChart1 = new SeeSharpTools.JY.GUI.EasyChart();
            this.nudTrimedPercent = new System.Windows.Forms.NumericUpDown();
            this.cboxMeanType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtboxMean = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.nudLength = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.txtboxMode = new System.Windows.Forms.TextBox();
            this.txtboxMedian = new System.Windows.Forms.TextBox();
            this.txtboxStandardDeviation = new System.Windows.Forms.TextBox();
            this.txtboxVariance = new System.Windows.Forms.TextBox();
            this.txtboxRMS = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrimedPercent)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).BeginInit();
            this.SuspendLayout();
            // 
            // easyChart1
            // 
            this.easyChart1.AxisYMax = double.NaN;
            this.easyChart1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.None;
            this.easyChart1.ChartAreaBackColor = System.Drawing.Color.Empty;
            this.easyChart1.EasyChartBackColor = System.Drawing.Color.White;
            this.easyChart1.FixAxisX = false;
            this.easyChart1.LegendBackColor = System.Drawing.Color.Transparent;
            this.easyChart1.LegendVisible = false;
            this.easyChart1.Location = new System.Drawing.Point(12, 11);
            this.easyChart1.MajorGridColor = System.Drawing.Color.Black;
            this.easyChart1.MajorGridEnabled = true;
            this.easyChart1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.easyChart1.MinorGridColor = System.Drawing.Color.Black;
            this.easyChart1.MinorGridEnabled = false;
            this.easyChart1.MinorGridType = SeeSharpTools.JY.GUI.EasyChart.GridStyle.Solid;
            this.easyChart1.Name = "easyChart1";
            this.easyChart1.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.DeepPink,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.Black};
            this.easyChart1.SeriesNames = new string[] {
        "Series1",
        "Series2",
        "Series3",
        "Series4",
        "Series5",
        "Series6",
        "Series7",
        "Series8",
        "Series9",
        "Series10",
        "Series11",
        "Series12",
        "Series13",
        "Series14",
        "Series15",
        "Series16",
        "Series17",
        "Series18",
        "Series19",
        "Series20",
        "Series21",
        "Series22",
        "Series23",
        "Series24",
        "Series25",
        "Series26",
        "Series27",
        "Series28",
        "Series29",
        "Series30",
        "Series31",
        "Series32"};
            this.easyChart1.Size = new System.Drawing.Size(603, 320);
            this.easyChart1.TabIndex = 0;
            this.easyChart1.XAxisLogarithmic = false;
            this.easyChart1.YAutoEnable = true;
            this.easyChart1.YAxisLogarithmic = false;
            // 
            // nudTrimedPercent
            // 
            this.nudTrimedPercent.Location = new System.Drawing.Point(29, 96);
            this.nudTrimedPercent.Name = "nudTrimedPercent";
            this.nudTrimedPercent.Size = new System.Drawing.Size(179, 25);
            this.nudTrimedPercent.TabIndex = 1;
            this.nudTrimedPercent.ValueChanged += new System.EventHandler(this.cboxMeanType_SelectedIndexChanged);
            // 
            // cboxMeanType
            // 
            this.cboxMeanType.FormattingEnabled = true;
            this.cboxMeanType.Items.AddRange(new object[] {
            "ArithmeticMean",
            "GeometricMean",
            "HarmonicMean",
            "TrimmedMean"});
            this.cboxMeanType.Location = new System.Drawing.Point(29, 38);
            this.cboxMeanType.Name = "cboxMeanType";
            this.cboxMeanType.Size = new System.Drawing.Size(179, 23);
            this.cboxMeanType.TabIndex = 2;
            this.cboxMeanType.SelectedIndexChanged += new System.EventHandler(this.cboxMeanType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "MeanType";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboxMeanType);
            this.groupBox1.Controls.Add(this.txtboxMean);
            this.groupBox1.Controls.Add(this.nudTrimedPercent);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(643, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 204);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "均值";
            // 
            // txtboxMean
            // 
            this.txtboxMean.Location = new System.Drawing.Point(30, 156);
            this.txtboxMean.Name = "txtboxMean";
            this.txtboxMean.Size = new System.Drawing.Size(179, 25);
            this.txtboxMean.TabIndex = 13;
            this.txtboxMean.Text = " ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Mean";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "TrimmedPercent";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(914, 240);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "Mode";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(914, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "Median";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(914, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 15);
            this.label6.TabIndex = 7;
            this.label6.Text = "StandardDeviation";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(914, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 15);
            this.label7.TabIndex = 8;
            this.label7.Text = "Variance";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(914, 175);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 15);
            this.label8.TabIndex = 9;
            this.label8.Text = "RootMeanSquare";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(672, 300);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(179, 31);
            this.btnGenerate.TabIndex = 10;
            this.btnGenerate.Text = "GenerateAndAnalyze";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // nudLength
            // 
            this.nudLength.Location = new System.Drawing.Point(672, 256);
            this.nudLength.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLength.Name = "nudLength";
            this.nudLength.Size = new System.Drawing.Size(179, 25);
            this.nudLength.TabIndex = 1;
            this.nudLength.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(670, 240);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 15);
            this.label9.TabIndex = 11;
            this.label9.Text = "DataLength";
            // 
            // txtboxMode
            // 
            this.txtboxMode.Location = new System.Drawing.Point(917, 258);
            this.txtboxMode.Multiline = true;
            this.txtboxMode.Name = "txtboxMode";
            this.txtboxMode.Size = new System.Drawing.Size(179, 73);
            this.txtboxMode.TabIndex = 12;
            // 
            // txtboxMedian
            // 
            this.txtboxMedian.Location = new System.Drawing.Point(917, 28);
            this.txtboxMedian.Name = "txtboxMedian";
            this.txtboxMedian.Size = new System.Drawing.Size(179, 25);
            this.txtboxMedian.TabIndex = 13;
            this.txtboxMedian.Text = " ";
            // 
            // txtboxStandardDeviation
            // 
            this.txtboxStandardDeviation.Location = new System.Drawing.Point(917, 83);
            this.txtboxStandardDeviation.Name = "txtboxStandardDeviation";
            this.txtboxStandardDeviation.Size = new System.Drawing.Size(179, 25);
            this.txtboxStandardDeviation.TabIndex = 13;
            this.txtboxStandardDeviation.Text = " ";
            // 
            // txtboxVariance
            // 
            this.txtboxVariance.Location = new System.Drawing.Point(917, 138);
            this.txtboxVariance.Name = "txtboxVariance";
            this.txtboxVariance.Size = new System.Drawing.Size(179, 25);
            this.txtboxVariance.TabIndex = 13;
            this.txtboxVariance.Text = " ";
            // 
            // txtboxRMS
            // 
            this.txtboxRMS.Location = new System.Drawing.Point(917, 193);
            this.txtboxRMS.Name = "txtboxRMS";
            this.txtboxRMS.Size = new System.Drawing.Size(179, 25);
            this.txtboxRMS.TabIndex = 13;
            this.txtboxRMS.Text = " ";
            // 
            // MultiFunctionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1133, 357);
            this.Controls.Add(this.txtboxRMS);
            this.Controls.Add(this.txtboxVariance);
            this.Controls.Add(this.txtboxStandardDeviation);
            this.Controls.Add(this.txtboxMedian);
            this.Controls.Add(this.txtboxMode);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.nudLength);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.easyChart1);
            this.Name = "MultiFunctionForm";
            this.Text = "ProbabilityStatisticsDemo";
            ((System.ComponentModel.ISupportInitialize)(this.nudTrimedPercent)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SeeSharpTools.JY.GUI.EasyChart easyChart1;
        private System.Windows.Forms.NumericUpDown nudTrimedPercent;
        private System.Windows.Forms.ComboBox cboxMeanType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudMean;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudMedian;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudStandardDeviation;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudVanriance;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudRootMeanSquare;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.NumericUpDown nudLength;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtboxMode;
        private System.Windows.Forms.TextBox txtboxMean;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtboxMedian;
        private System.Windows.Forms.TextBox txtboxStandardDeviation;
        private System.Windows.Forms.TextBox txtboxVariance;
        private System.Windows.Forms.TextBox txtboxRMS;
    }
}

