using System;
using System.Windows.Forms;
using SeeSharpTools.JXI.Mathematics.ProbabilityStatistics;
using SeeSharpTools.JY.DSP.Fundamental;
using SeeSharpTools.JY.ArrayUtility;
using System.Diagnostics;

namespace Demo_for_HistgramCreating
{
    /// <summary>
    /// 版权所有(C)2017，上海聚星仪器有限公司
    /// Copyright (C) 2017, Shanghai Juxing Instrument Co., Ltd.
    /// 内容摘要: 本Demo为统计分析类库中直方图分析方法CreateHstogram的Demo
    /// Abstract: This demo is the demo of the histogram analysis, using method CreateHstogram in the statistical analysis class library.
    /// 通过产生一个指定长度的正弦波进行指定数据区间上进行直方图统计分析 
    /// Perform a histogram statistical analysis on a specified data interval by generating a sine wave of a specified length
    /// Completion date: September 17, 2017
    ///  V1.0
     /// Author: Liu Yuhui , JXI
    /// </summary>	
    public partial class HistogramForm : Form
    {
        #region Private Fields

        /// <summary>
        /// Data to be analyzed
        /// 待分析data
        /// </summary>
        double[] dataIn;

        /// <summary>
        /// Result, Discrete histogram
        /// 分析得到的离散直方图
        /// </summary>
        int[] histgram;

        /// <summary>
        /// Interval median value of the discrete histogram obtained by analysis
        /// 分析得到的离散直方图的区间中值
        /// </summary>
        double[] intervals;

        /// <summary>
        /// Specify the number of intervals for a histogram statistical analysis
        /// 指定一个直方图统计分析的区间数
        /// </summary>
        int nbrOfIntervals = 8;

        /// <summary>
        /// Input data length
        /// 输入数据长度
        /// </summary>
        int dataLength;

        #endregion

        #region Constructor
        public HistogramForm()
        {
            InitializeComponent();
            HistogramEvent(null, null);
            EfficiencyEvent(null, null);
        }
        #endregion

        #region Events Handle

        /// <summary>
        /// Add a random number based on the existing data to generate new data and perform histogram analysis display.
        /// 在已有的data基础上加入一个随机数产生新的数据并进行直方图分析显示
        /// </summary>
        private void HistogramEvent(object sender, EventArgs e)
        {
            nbrOfIntervals = (int)nudNbrOfIntervals.Value;  //读取前面板控件值   Read front panel control values
            dataLength = (int)nudDataLength.Value;          //读取前面板控件值   Read front panel control values
            dataIn = new double[dataLength];                //初始化数据数组长度 Initialize the length of the data array           
            Generation.SineWave(ref dataIn,1,0,5);          //产生一个正弦信号   Generate a sinusoidal signal
            histgram = new int[nbrOfIntervals];
            intervals = new double[nbrOfIntervals];

            easyChart1.Plot(dataIn);                        //时域信号显示                                Time domain signal display
            ProbabilityStatistics.Histogram(dataIn,ref histgram, ref intervals);//分析数据直方图          data histogram analysis
            chart1.Series["Series1"].Points.DataBindXY(intervals, histgram);//直方图显示                  Histogram display
        }

        /// <summary>
        /// Run the method by loop times to benchmark
        /// 运行该方法loopTimes指定次数,计算消耗时间
        /// </summary>
        private void EfficiencyEvent(object sender, EventArgs e)
        {
            int loopTimes = (int)nudTestTimes.Value;
            Stopwatch stopwatch = new Stopwatch();
            dataIn = new double[dataLength];                    //初始化数据数组长度     Initialize the length of the data array     
            Generation.SineWave(ref dataIn, 1, 0, 5);           //产生一个正弦信号       Generate a sinusoidal signal
            stopwatch.Start();
            for (int i = 0; i < loopTimes; i++)
            {
                ProbabilityStatistics.Histogram(dataIn, ref histgram, ref intervals);//分析数据直方图     Analytical data histogram
            }
            stopwatch.Stop();
            txtboxTimeElapsed.Text = stopwatch.ElapsedMilliseconds.ToString() + "ms";
        }
        #endregion        
    }    
}

