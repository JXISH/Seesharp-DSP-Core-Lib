using System;
using System.Windows.Forms;
using SeeSharpTools.JXI.Mathematics.ProbabilityStatistics;

namespace Winform_Statistics
{
    /// <summary>
    /// 版权所有(C)2017，上海聚星仪器有限公司
    /// Copyright (C) 2017, Shanghai Juxing Instrument Co., Ltd.
    /// 内容摘要: 本Demo为统计分析类库的Demo
    ///           产生一组随机数并计算其均值，中值，众数，标准差，方差，均方根
    /// Abstract: This demo is a demo of the statistical analysis class library.
    ///           Generate a set of random numbers and calculate their mean, median, mode, standard deviation, variance,and root mean square
    /// Completion date: September 17, 2017
    /// Version: 1.0
    /// Author: Liu Yuhui , JXI    
    /// </summary>	
    public partial class MultiFunctionForm : Form
    {
        #region Private Fields
        double[] data;        
        #endregion

        #region Constructor
        public MultiFunctionForm()
        {
            InitializeComponent();            
            btnGenerate_Click(null, null);
            cboxMeanType.SelectedIndex = 0;
        }
        #endregion

        #region Events Handle
        /// <summary>
        /// 产生一组随机数并计算结果
        /// Generate a set of random numbers and calculate the result
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Random ran = new Random();
            data = new double[(int)nudLength.Value];            

            for (int i = 0; i < (int)nudLength.Value; i++)
            {
                data[i] = ran.Next(1, 100);
            }
            easyChart1.Plot(data);//显示数据     show data

            GetMean();//计算均值                 calculate Mean
            GetMode();//计算众数                 calculate Mode
            txtboxMedian.Text = ProbabilityStatistics.Median(data).ToString();//计算中值                                     calculate Median
            txtboxRMS.Text = ProbabilityStatistics.RootMeanSquare(data).ToString();//计算均方根                              calculate RootMeanSquare
            txtboxStandardDeviation.Text = ProbabilityStatistics.StandardDeviation(data).ToString();//计算标准差             calculate standard deviation
            txtboxVariance.Text = ProbabilityStatistics.Variance(data).ToString();//计算方差                                 calculate variance

        }

        /// <summary>
        /// 基于之前产生的随机数数组计算均值
        /// Calculate the mean based on the previously generated array of random numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboxMeanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMean();//计算均值 calculate Mean
        }
        #endregion

        #region Methods

        /// <summary>
        /// 计算平均值并显示到界面
        /// Calculate the Mean and display it to the Winform
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
        }

        /// <summary>
        /// 计算众数
        /// calculate Mode
        /// </summary>
        public void GetMode()
        {
            txtboxMode.Text = "";
            double[] mode = ProbabilityStatistics.Mode(data);

            for (int i = 0; i < mode.Length; i++)
            {
                txtboxMode.Text += mode[i] + "; ";
            }
            txtboxMode.Text += "\n";
        }
        #endregion
    }
}
