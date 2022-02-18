namespace SeeSharpTools.JX.DSP.Math_PolynomialFittingForm
{
    partial class PolynomialFittingForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown_a0 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_a1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_a2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_a3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_noise = new System.Windows.Forms.NumericUpDown();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownDataLength = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.btnFit = new System.Windows.Forms.Button();
            this.Result = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBoxConfig = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_a0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_a1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_a2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_a3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_noise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDataLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBoxConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "a0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "a1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "a2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 270);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "a3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 325);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "Noise";
            // 
            // numericUpDown_a0
            // 
            this.numericUpDown_a0.DecimalPlaces = 2;
            this.numericUpDown_a0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown_a0.Location = new System.Drawing.Point(164, 105);
            this.numericUpDown_a0.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_a0.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_a0.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDown_a0.Name = "numericUpDown_a0";
            this.numericUpDown_a0.Size = new System.Drawing.Size(107, 30);
            this.numericUpDown_a0.TabIndex = 6;
            this.numericUpDown_a0.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numericUpDown_a1
            // 
            this.numericUpDown_a1.DecimalPlaces = 2;
            this.numericUpDown_a1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown_a1.Location = new System.Drawing.Point(164, 158);
            this.numericUpDown_a1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_a1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_a1.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDown_a1.Name = "numericUpDown_a1";
            this.numericUpDown_a1.Size = new System.Drawing.Size(107, 30);
            this.numericUpDown_a1.TabIndex = 7;
            this.numericUpDown_a1.Value = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            // 
            // numericUpDown_a2
            // 
            this.numericUpDown_a2.DecimalPlaces = 2;
            this.numericUpDown_a2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown_a2.Location = new System.Drawing.Point(164, 210);
            this.numericUpDown_a2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_a2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_a2.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDown_a2.Name = "numericUpDown_a2";
            this.numericUpDown_a2.Size = new System.Drawing.Size(107, 30);
            this.numericUpDown_a2.TabIndex = 8;
            this.numericUpDown_a2.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // numericUpDown_a3
            // 
            this.numericUpDown_a3.DecimalPlaces = 2;
            this.numericUpDown_a3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown_a3.Location = new System.Drawing.Point(164, 262);
            this.numericUpDown_a3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_a3.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_a3.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDown_a3.Name = "numericUpDown_a3";
            this.numericUpDown_a3.Size = new System.Drawing.Size(107, 30);
            this.numericUpDown_a3.TabIndex = 9;
            this.numericUpDown_a3.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // numericUpDown_noise
            // 
            this.numericUpDown_noise.DecimalPlaces = 2;
            this.numericUpDown_noise.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown_noise.Location = new System.Drawing.Point(164, 315);
            this.numericUpDown_noise.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown_noise.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_noise.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDown_noise.Name = "numericUpDown_noise";
            this.numericUpDown_noise.Size = new System.Drawing.Size(107, 30);
            this.numericUpDown_noise.TabIndex = 10;
            this.numericUpDown_noise.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGenerate.Location = new System.Drawing.Point(996, 625);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(113, 46);
            this.btnGenerate.TabIndex = 11;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 51);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 0, 17, 0);
            this.label6.Size = new System.Drawing.Size(416, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Y = a0 + a1*X + a2 X^2 + a3*X^3 + Noise";
            // 
            // numericUpDownDataLength
            // 
            this.numericUpDownDataLength.Location = new System.Drawing.Point(164, 368);
            this.numericUpDownDataLength.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownDataLength.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownDataLength.Name = "numericUpDownDataLength";
            this.numericUpDownDataLength.Size = new System.Drawing.Size(107, 30);
            this.numericUpDownDataLength.TabIndex = 16;
            this.numericUpDownDataLength.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 380);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 20);
            this.label8.TabIndex = 15;
            this.label8.Text = "dataLength";
            // 
            // btnFit
            // 
            this.btnFit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFit.Location = new System.Drawing.Point(1219, 625);
            this.btnFit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnFit.Name = "btnFit";
            this.btnFit.Size = new System.Drawing.Size(113, 46);
            this.btnFit.TabIndex = 17;
            this.btnFit.Text = "Fit";
            this.btnFit.UseVisualStyleBackColor = true;
            this.btnFit.Click += new System.EventHandler(this.btnFit_Click);
            // 
            // Result
            // 
            this.Result.BackColor = System.Drawing.Color.White;
            this.Result.Location = new System.Drawing.Point(89, 670);
            this.Result.Name = "Result";
            this.Result.Size = new System.Drawing.Size(625, 25);
            this.Result.TabIndex = 18;
            this.Result.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(0, 679);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(889, 34);
            this.label7.TabIndex = 19;
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chart1
            // 
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorY.IsUserSelectionEnabled = true;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 116);
            this.chart1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Legend = "Legend1";
            series1.Name = "OriData";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.EmptyPointStyle.Color = System.Drawing.Color.Gray;
            series2.Legend = "Legend1";
            series2.Name = "FitData";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(889, 571);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(266, 41);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(694, 59);
            this.label9.TabIndex = 20;
            this.label9.Text = "Polynomial Fitting";
            // 
            // groupBoxConfig
            // 
            this.groupBoxConfig.Controls.Add(this.label6);
            this.groupBoxConfig.Controls.Add(this.label1);
            this.groupBoxConfig.Controls.Add(this.label2);
            this.groupBoxConfig.Controls.Add(this.label3);
            this.groupBoxConfig.Controls.Add(this.numericUpDownDataLength);
            this.groupBoxConfig.Controls.Add(this.label4);
            this.groupBoxConfig.Controls.Add(this.label8);
            this.groupBoxConfig.Controls.Add(this.numericUpDown_noise);
            this.groupBoxConfig.Controls.Add(this.label5);
            this.groupBoxConfig.Controls.Add(this.numericUpDown_a3);
            this.groupBoxConfig.Controls.Add(this.numericUpDown_a0);
            this.groupBoxConfig.Controls.Add(this.numericUpDown_a2);
            this.groupBoxConfig.Controls.Add(this.numericUpDown_a1);
            this.groupBoxConfig.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxConfig.Location = new System.Drawing.Point(952, 116);
            this.groupBoxConfig.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxConfig.Name = "groupBoxConfig";
            this.groupBoxConfig.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxConfig.Size = new System.Drawing.Size(460, 436);
            this.groupBoxConfig.TabIndex = 21;
            this.groupBoxConfig.TabStop = false;
            this.groupBoxConfig.Text = "GenerateRawData";
            // 
            // PolynomialFittingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1428, 740);
            this.Controls.Add(this.groupBoxConfig);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.Result);
            this.Controls.Add(this.btnFit);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.label7);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "PolynomialFittingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PolynomialFittingForm";
            this.Load += new System.EventHandler(this.btnGenerate_Click);
            this.Shown += new System.EventHandler(this.btnFit_Click);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_a0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_a1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_a2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_a3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_noise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDataLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBoxConfig.ResumeLayout(false);
            this.groupBoxConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown_a0;
        private System.Windows.Forms.NumericUpDown numericUpDown_a1;
        private System.Windows.Forms.NumericUpDown numericUpDown_a2;
        private System.Windows.Forms.NumericUpDown numericUpDown_a3;
        private System.Windows.Forms.NumericUpDown numericUpDown_noise;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownDataLength;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnFit;
        private System.Windows.Forms.Label Result;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBoxConfig;
    }
}

