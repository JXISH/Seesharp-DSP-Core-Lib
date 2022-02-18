namespace ProbabilityStatisticsExample
{
    partial class MeanForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.easyChart1 = new SeeSharpTools.JY.GUI.EasyChart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboxMeanType = new System.Windows.Forms.ComboBox();
            this.txtboxMean = new System.Windows.Forms.TextBox();
            this.nudTrimedPercent = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtboxTimeElapsed = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudLength = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nudTestTimes = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrimedPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTestTimes)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(325, 98);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 15);
            this.label2.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(264, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(372, 44);
            this.label5.TabIndex = 9;
            this.label5.Text = "Mean Calculation";
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
            this.easyChart1.Location = new System.Drawing.Point(49, 109);
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
            this.easyChart1.Size = new System.Drawing.Size(587, 412);
            this.easyChart1.TabIndex = 17;
            this.easyChart1.XAxisLogarithmic = false;
            this.easyChart1.YAutoEnable = true;
            this.easyChart1.YAxisLogarithmic = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboxMeanType);
            this.groupBox1.Controls.Add(this.txtboxMean);
            this.groupBox1.Controls.Add(this.nudTrimedPercent);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Location = new System.Drawing.Point(642, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 204);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mean";
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
            this.cboxMeanType.Text = "ArithmeticMean";
            this.cboxMeanType.SelectedIndexChanged += new System.EventHandler(this.SettingChangedEvent);
            // 
            // txtboxMean
            // 
            this.txtboxMean.Location = new System.Drawing.Point(30, 156);
            this.txtboxMean.Name = "txtboxMean";
            this.txtboxMean.Size = new System.Drawing.Size(179, 25);
            this.txtboxMean.TabIndex = 13;
            this.txtboxMean.Text = " ";
            // 
            // nudTrimedPercent
            // 
            this.nudTrimedPercent.Location = new System.Drawing.Point(29, 96);
            this.nudTrimedPercent.Name = "nudTrimedPercent";
            this.nudTrimedPercent.Size = new System.Drawing.Size(179, 25);
            this.nudTrimedPercent.TabIndex = 1;
            this.nudTrimedPercent.ValueChanged += new System.EventHandler(this.SettingChangedEvent);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 138);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 15);
            this.label7.TabIndex = 3;
            this.label7.Text = "Mean";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(26, 78);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(119, 15);
            this.label10.TabIndex = 3;
            this.label10.Text = "TrimmedPercent";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(27, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 15);
            this.label11.TabIndex = 3;
            this.label11.Text = "MeanType";
            // 
            // txtboxTimeElapsed
            // 
            this.txtboxTimeElapsed.Location = new System.Drawing.Point(31, 104);
            this.txtboxTimeElapsed.Margin = new System.Windows.Forms.Padding(4);
            this.txtboxTimeElapsed.Name = "txtboxTimeElapsed";
            this.txtboxTimeElapsed.Size = new System.Drawing.Size(178, 25);
            this.txtboxTimeElapsed.TabIndex = 15;
            this.txtboxTimeElapsed.TextChanged += new System.EventHandler(this.EfficiencyEvent);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(28, 85);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 15);
            this.label9.TabIndex = 16;
            this.label9.Text = "Efficiency/ms";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(113, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(279, 15);
            this.label1.TabIndex = 20;
            this.label1.Text = "Random Data Length(To be analyzed)";
            // 
            // nudLength
            // 
            this.nudLength.Location = new System.Drawing.Point(398, 96);
            this.nudLength.Maximum = new decimal(new int[] {
            10000,
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
            this.nudLength.TabIndex = 19;
            this.nudLength.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudLength.ValueChanged += new System.EventHandler(this.MeanEvent);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nudTestTimes);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtboxTimeElapsed);
            this.groupBox2.Location = new System.Drawing.Point(642, 317);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(241, 156);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Efficiency";
            // 
            // nudTestTimes
            // 
            this.nudTestTimes.Location = new System.Drawing.Point(31, 43);
            this.nudTestTimes.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudTestTimes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTestTimes.Name = "nudTestTimes";
            this.nudTestTimes.Size = new System.Drawing.Size(178, 25);
            this.nudTestTimes.TabIndex = 17;
            this.nudTestTimes.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudTestTimes.ValueChanged += new System.EventHandler(this.EfficiencyEvent);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "LoopTimes";
            // 
            // MeanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(922, 553);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudLength);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.easyChart1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MeanForm";
            this.Text = "MeanCalculationDemo";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrimedPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTestTimes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private SeeSharpTools.JY.GUI.EasyChart easyChart1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboxMeanType;
        private System.Windows.Forms.TextBox txtboxMean;
        private System.Windows.Forms.NumericUpDown nudTrimedPercent;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtboxTimeElapsed;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudLength;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown nudTestTimes;
        private System.Windows.Forms.Label label3;
    }
}

