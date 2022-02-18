using System;
using System.Windows.Forms;
using SeeSharpTools.JXI.Mathematics.ProbabilityStatistics;
using System.Diagnostics;
using SeeSharpTools.JY.File;
namespace example
{
    /// <summary>
    /// 版权所有(C)2017，上海聚星仪器有限公司
    /// Copyright (C) 2017, Shanghai Juxing Instrument Co., Ltd.
    /// 内容摘要: 本Demo为统计分析类库中众数计算的Demo
    ///           产生一组随机数并计算其众数
    ///Abstract: This demo is a demo for the majority calculation in the statistical analysis class library.
    ///          Generate a set of random numbers and calculate their mode
    /// Completion date: September 17, 2017
    /// Version: 1.0
    /// Author: Liu Yuhui , JXI    
    /// </summary>	
    public partial class ModeForm : Form
    {
        double[] arr; 

        public ModeForm()
        {
            InitializeComponent();
            ModeEvent(null, null);
        }
        
        public void ModeEvent(object sender, EventArgs e)
        {
            arr =  new double[(int)nudLength.Value];

            Random r = new Random();
           
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = r.Next(1,100);
            }
            Array.Sort(arr);

            System.Text.StringBuilder str = new System.Text.StringBuilder();
            
            dataGridView1.Rows.Clear();

            for (int i = 0; i < arr.Length; i++)
            {
                dataGridView1.Rows.Add(arr[i]);
            }
            
            double[] M = ProbabilityStatistics.Mode(arr);
            //textBox1.Text = "有" + M.Length + "个众数    ";
            textBox1.Text = "Number of Modes = " + M.Length;

            for (int i = 0; i < M.Length; i++)
            {
                textBox1.Text += M[i] + " ";
            }
            textBox1.Text += "\n";

            EfficiencyEvent(null, null);
        }

        /// <summary>
        /// 运行该方法指定次数计算消耗时间
        /// Loop the method to benchmark
        /// </summary>
        private void EfficiencyEvent(object sender, EventArgs e)
        {
            int loopTimes = (int)nudTestTimes.Value;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < loopTimes; i++)
            {
                ProbabilityStatistics.Median(arr);
            }
            stopwatch.Stop();
            txtboxTimeElapsed.Text = stopwatch.ElapsedMilliseconds.ToString() + "ms";
        }

    }
}
