using System;
using System.Windows.Forms;
using SeeSharpTools.JXI.Mathematics.ProbabilityStatistics;
using System.Diagnostics;

namespace SeesharpTools.JXI.Mathematics.example
{
    /// <summary>
    /// 版权所有(C)2017，上海聚星仪器有限公司
    ///  Copyright (C) 2017, Shanghai Juxing Instrument Co., Ltd.
    /// 内容摘要: 计算数组A和数组B的均方差；
    /// Abstract: Calculate the mean square error of array A and array B;
    /// Completion date: September 17, 2017
    /// Version: 1.0
    /// Author: Liu Yuhui, JXI
    /// </summary>
    public partial class MeanSquaredErrorForm : Form
    {
        public MeanSquaredErrorForm()
        {           
            InitializeComponent();
            buttonCalculate_Click(null, null);
            buttonEfficiencyTesting_Click(null,null);
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            //产生待计算的随机数组A和B
           // Generate random arrays A and B to be calculated
            double reslut = 0;
            Random ran = new Random();
            double[] dDataA = new double[(int)nudLength.Value];
            double[] dDataB = new double[dDataA.Length];
            double[,] displayData = new double[2, dDataA.Length];
            for (int i = 0; i < dDataA.Length; i++)
            {
                displayData[0,i] = dDataA[i] = ran.NextDouble();
                displayData[1, i] = dDataB[i] = ran.NextDouble();
            }
            
            reslut = ProbabilityStatistics.MeanSquaredError(dDataA, dDataB);

            dataGridView1.Rows.Clear();
            for (int i = 0; i < dDataA.Length; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = dDataA[i];
                dataGridView1.Rows[i].Cells[1].Value = dDataB[i];
            }
            textBoxReslut.Text = reslut.ToString("f5");
        }

        /// <summary>
        /// 测试算法效率
        /// Test algorithm efficiency (benchmark)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEfficiencyTesting_Click(object sender, EventArgs e)
        {
            Stopwatch time = new Stopwatch();
            Random ran = new Random();            
            double[] dDataA = new double[(int)nudLength.Value];
            double[] dDataB = new double[(int)nudLength.Value];
            for (int i = 0; i < dDataA.Length; i++)
            {
                dDataA[i] = ran.NextDouble();
                dDataB[i] = ran.NextDouble();
            }
            int tickCount = Environment.TickCount;
            time.Start();
            for (int i = 0; i < nudTestTimes.Value; i++)
            {
                ProbabilityStatistics.MeanSquaredError(dDataA, dDataB);
            }  
            time.Stop();
            textBoxEfficiencyTesting.Text = time.ElapsedMilliseconds.ToString();
        }

       
    }
}
