using System;
using System.Windows.Forms;
using System.Diagnostics;
using SeeSharpTools.JXI.Mathematics.ProbabilityStatistics;

namespace ProbabilityStatisticsExample
{
    /// <summary>
    /// 版权所有(C)2017，上海聚星仪器有限公司
    /// Copyright (C) 2017, Shanghai Juxing Instrument Co., Ltd.
    /// 内容摘要: 本Demo为统计分析类库中均值计算的Demo
    ///           产生一组随机数并计算其众数
    /// Abstract: This demo is a demo for the mean calculation in the statistical analysis class library.
    ///            Generate a set of random numbers and calculate their mode
    /// Completion date: September 17, 2017
    ///  Verison 1.0
    /// Author: Liu Yuhui, JXI 
    /// </summary>	
    public partial class MeanForm : Form
    {
        #region private Fields              
        /// <summary>
        /// 待计算的数据
        /// Data to be calculated
        /// </summary>
        private double[] data;
               
        #endregion

        #region Construct
        public MeanForm()
        {
            InitializeComponent();
            MeanEvent(null,null);
            EfficiencyEvent(null, null);
        }
        #endregion

        #region Event Handler
        /// <summary>
        /// 设置改变
        /// setting change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingChangedEvent(object sender, EventArgs e)
        {            
            GetMean();
        }

        /// <summary>
        /// 计算各均值
        /// Calculate the mean
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeanEvent(object sender, EventArgs e)
        {
            data = new double[(int)nudLength.Value];
            RandomSequence(ref data, 0, 1);
            easyChart1.Plot(data);
            GetMean();
        }

        /// <summary>
        /// 运行改方法指定次数计算消失时间
        /// Run the change method to specify the number of times to calculate the disappearance time
        /// </summary>
        private void EfficiencyEvent(object sender, EventArgs e)
        {
            int loopTimes = (int)nudTestTimes.Value;
            double mean;
            Stopwatch stopwatch = new Stopwatch();
            switch (cboxMeanType.SelectedIndex)
            {
                case 0:
                    stopwatch.Start();
                    for (int i = 0; i < loopTimes; i++)
                    {
                        mean = ProbabilityStatistics.Mean(data);
                    }
                    stopwatch.Stop();
                    break;
                case 1:
                    stopwatch.Start();
                    for (int i = 0; i < loopTimes; i++)
                    {
                        mean = ProbabilityStatistics.GeometricMean(data);
                    }
                    stopwatch.Stop();
                    break;
                case 2:
                    stopwatch.Start();
                    for (int i = 0; i < loopTimes; i++)
                    {
                        mean = ProbabilityStatistics.HarmonicMean(data);
                    }
                    stopwatch.Stop();
                    break;
                case 3:
                    stopwatch.Start();
                    for (int i = 0; i < loopTimes; i++)
                    {
                        mean = ProbabilityStatistics.TrimmedMean(data, (int)nudTrimedPercent.Value);
                    }
                    stopwatch.Stop();
                    break;
                default:
                    stopwatch.Start();
                    for (int i = 0; i < loopTimes; i++)
                    {
                        mean = ProbabilityStatistics.HarmonicMean(data);
                    }
                    stopwatch.Stop();
                    break;
            }
            txtboxTimeElapsed.Text = stopwatch.ElapsedMilliseconds.ToString() + "ms";
        }
        #endregion

        #region Methods
        /// <summary>
        /// double RandomSequence Generation
        /// </summary>
        /// <param name="length"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void RandomSequence(ref double[] sequence, double min, double max)
        {
            if (sequence.Length < 1)
            {
                throw new ArgumentException();
            }
            int length = sequence.Length;
            Random r = new Random();
            for (int i = 0; i < length; i++)
            {
                sequence[i] = r.NextDouble() * (max - min) + min;
            }
        }

        /// <summary>
        /// 计算平均值并显示到界面
        /// Calculate the average and display it to the Winform
        /// </summary>
        public void GetMean()
        {
            double mean;
            switch (cboxMeanType.SelectedIndex)
            {
                case 0:
                    mean = ProbabilityStatistics.Mean(data);
                    break;
                case 1:
                    mean = ProbabilityStatistics.GeometricMean(data);
                    break;
                case 2:
                    mean = ProbabilityStatistics.HarmonicMean(data);
                    break;
                case 3:
                    mean = ProbabilityStatistics.TrimmedMean(data, (int)nudTrimedPercent.Value);
                    break;
                default:
                    mean = ProbabilityStatistics.Mean(data);
                    break;
            }
            txtboxMean.Text = mean.ToString();
            EfficiencyEvent(null, null);
        }
        #endregion
    }
}
