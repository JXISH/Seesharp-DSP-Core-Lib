namespace SeeSharpTools.JX.DSP.Math_LinearFittingExample
{
    partial class LinearFittingFrom
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownA = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownB = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownNoise = new System.Windows.Forms.NumericUpDown();
            this.Result = new System.Windows.Forms.Label();
            this.btnGenrate = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownDataLength = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.btnFit = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBoxConfig = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNoise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDataLength)).BeginInit();
            this.groupBoxConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea2.CursorX.IsUserSelectionEnabled = true;
            chartArea2.CursorY.IsUserSelectionEnabled = true;
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(0, 119);
            this.chart1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chart1.Name = "chart1";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series3.Legend = "Legend1";
            series3.Name = "OriData";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend1";
            series4.Name = "FitData";
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            this.chart1.Size = new System.Drawing.Size(889, 571);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Y = aX + b + Noise";
            // 
            // numericUpDownA
            // 
            this.numericUpDownA.DecimalPlaces = 2;
            this.numericUpDownA.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownA.Location = new System.Drawing.Point(140, 74);
            this.numericUpDownA.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownA.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownA.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDownA.Name = "numericUpDownA";
            this.numericUpDownA.Size = new System.Drawing.Size(120, 30);
            this.numericUpDownA.TabIndex = 2;
            this.numericUpDownA.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownB
            // 
            this.numericUpDownB.DecimalPlaces = 2;
            this.numericUpDownB.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownB.Location = new System.Drawing.Point(140, 129);
            this.numericUpDownB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownB.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownB.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDownB.Name = "numericUpDownB";
            this.numericUpDownB.Size = new System.Drawing.Size(120, 30);
            this.numericUpDownB.TabIndex = 3;
            this.numericUpDownB.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "a";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "b";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Noise";
            // 
            // numericUpDownNoise
            // 
            this.numericUpDownNoise.DecimalPlaces = 2;
            this.numericUpDownNoise.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownNoise.Location = new System.Drawing.Point(140, 184);
            this.numericUpDownNoise.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownNoise.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownNoise.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDownNoise.Name = "numericUpDownNoise";
            this.numericUpDownNoise.Size = new System.Drawing.Size(120, 30);
            this.numericUpDownNoise.TabIndex = 7;
            this.numericUpDownNoise.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // Result
            // 
            this.Result.BackColor = System.Drawing.Color.White;
            this.Result.Location = new System.Drawing.Point(97, 672);
            this.Result.Name = "Result";
            this.Result.Size = new System.Drawing.Size(631, 25);
            this.Result.TabIndex = 8;
            this.Result.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnGenrate
            // 
            this.btnGenrate.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGenrate.Location = new System.Drawing.Point(996, 510);
            this.btnGenrate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGenrate.Name = "btnGenrate";
            this.btnGenrate.Size = new System.Drawing.Size(119, 48);
            this.btnGenrate.TabIndex = 10;
            this.btnGenrate.Text = "Generate";
            this.btnGenrate.UseVisualStyleBackColor = true;
            this.btnGenrate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(0, 678);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(889, 34);
            this.label6.TabIndex = 11;
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDownDataLength
            // 
            this.numericUpDownDataLength.Location = new System.Drawing.Point(140, 239);
            this.numericUpDownDataLength.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownDataLength.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownDataLength.Name = "numericUpDownDataLength";
            this.numericUpDownDataLength.Size = new System.Drawing.Size(120, 30);
            this.numericUpDownDataLength.TabIndex = 13;
            this.numericUpDownDataLength.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 248);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "dataLength";
            // 
            // btnFit
            // 
            this.btnFit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFit.Location = new System.Drawing.Point(996, 589);
            this.btnFit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnFit.Name = "btnFit";
            this.btnFit.Size = new System.Drawing.Size(119, 48);
            this.btnFit.TabIndex = 14;
            this.btnFit.Text = "Fit";
            this.btnFit.UseVisualStyleBackColor = true;
            this.btnFit.Click += new System.EventHandler(this.btnFit_Click);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(448, 39);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(340, 59);
            this.label7.TabIndex = 15;
            this.label7.Text = "Linear Fitting";
            // 
            // groupBoxConfig
            // 
            this.groupBoxConfig.Controls.Add(this.label1);
            this.groupBoxConfig.Controls.Add(this.label2);
            this.groupBoxConfig.Controls.Add(this.numericUpDownA);
            this.groupBoxConfig.Controls.Add(this.label5);
            this.groupBoxConfig.Controls.Add(this.numericUpDownDataLength);
            this.groupBoxConfig.Controls.Add(this.label3);
            this.groupBoxConfig.Controls.Add(this.label4);
            this.groupBoxConfig.Controls.Add(this.numericUpDownB);
            this.groupBoxConfig.Controls.Add(this.numericUpDownNoise);
            this.groupBoxConfig.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxConfig.Location = new System.Drawing.Point(940, 119);
            this.groupBoxConfig.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxConfig.Name = "groupBoxConfig";
            this.groupBoxConfig.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxConfig.Size = new System.Drawing.Size(267, 299);
            this.groupBoxConfig.TabIndex = 16;
            this.groupBoxConfig.TabStop = false;
            this.groupBoxConfig.Text = "GenerateRawData";
            // 
            // LinearFittingFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1237, 741);
            this.Controls.Add(this.groupBoxConfig);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnFit);
            this.Controls.Add(this.btnGenrate);
            this.Controls.Add(this.Result);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.label6);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "LinearFittingFrom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LinearFittingFrom";
            this.Load += new System.EventHandler(this.btnGenerate_Click);
            this.Shown += new System.EventHandler(this.btnFit_Click);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNoise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDataLength)).EndInit();
            this.groupBoxConfig.ResumeLayout(false);
            this.groupBoxConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownA;
        private System.Windows.Forms.NumericUpDown numericUpDownB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownNoise;
        private System.Windows.Forms.Label Result;
        private System.Windows.Forms.Button btnGenrate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownDataLength;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnFit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBoxConfig;
    }
}

