/*******************************************
  * GaussianWhiteNoise example
  * Generate Gaussian White Noise (subject to normal distribution)
  * Using the histogram to do statistics, you can see that the Noise obeys the normal distribution.
  * *****************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SeeSharpTools.JXI.SignalProcessing.Generation;
using SeeSharpTools.JXI.SignalProcessing.GeneralSpectrum;
using SeeSharpTools.JXI.SignalProcessing.Window;
using SeeSharpTools.JY.ArrayUtility;
using System.Numerics;


namespace GaussianWhiteNoise_Winform
{
    public partial class MainForm : Form
    {
        #region Private Field       
        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handler
        /// <summary>
        /// Generate button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            //Get parameters
            double stdev =(double)numericUpDownStdev.Value;
            int length = (int)numericUpDownLength.Value;//Data length
            int numberOfIntervals=(int)numericUpDownNumber.Value;
            //Generate noise
            double[] noise = new double[length];
            Generation.GaussianWhiteNoise(ref noise, stdev);
            //Get a histogram
            double[] intervals = new double[numberOfIntervals];//Median value of each bar
            double[] histogram = new double[numberOfIntervals];//Bar chart
            Histogram(noise, numberOfIntervals,out histogram, out intervals);
            //Calculated power spectrum
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = 1000;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = (int)_task.SampleRate / 2;
            _task.Unit.Type = SpectrumOutputUnit.V2;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
            double[] spec = new double[(int)_task.SampleRate / 2];
            _task.GetSpectrum(noise, ref spec);
            //Display
            easyChartNoise.Plot(noise);
            easyChartHistogram.Plot(histogram, intervals[0],(intervals[1]- intervals[0]));
            easyChartSpec.Plot(spec,_task.SpectralInfomation .FreqStart ,_task.SpectralInfomation.FreqDelta);
        }
        #endregion

        #region Methods

        /// <summary>
        /// Histogram statistical analysis (statistical input data appears in the specified interval)        
        /// </summary>
        /// <param name="nums">Input data (the input array length contains at least one element)</param>
        /// <param name="numberOfIntervals">Specify the number of intervals (the number of intervals is at least 1)</param>
        /// <param name="intervals">Interval intermediate value</param>
        /// <param name="histgram">Discrete histogram of nums</param>
        /// <param name="intervalType">Designated interval opening and closing method</param>
        private void Histogram(double[] nums, int numberOfIntervals, out double[] histgram, out double[] intervals, IntervalType intervalType = IntervalType.LeftClosed)
        {
            #region Local variable definition

            /// <summary>
            /// Interval width
            /// </summary>
            double _delta;

            /// <summary>
            /// Interval half width
            /// </summary>
            double _deltaHalf;

            /// <summary>
            /// Maximum value of input data
            /// </summary>
            double _dataMax;

            /// <summary>
            /// Minimum value of input data
            /// </summary>
            double _dataMin;

            #endregion

            #region Input parameter condition check, variable initialization
            //The input array length contains at least one element
            //The number of intervals is at least 1
            if (nums == null || nums.Length == 0 || numberOfIntervals <= 0)
            {
                throw new ArgumentException() { Source = "Unexpected Input Parameter for Hstogram Creating" };
            }

            //Histogram and interval array initialization            
            intervals = new double[numberOfIntervals];
            histgram = new double[numberOfIntervals];
            #endregion

            #region Interval segmentation

            //Calculate the minimum value of input data
            _dataMax = nums.Max();
            _dataMin = nums.Min();

            //Calculate interval width
            _delta = (_dataMax - _dataMin) / numberOfIntervals;
            _deltaHalf = _delta / 2;

            //Segmentation interval
            for (int i = 0; i < numberOfIntervals; i++)
            {
                intervals[i] = _dataMin + _deltaHalf + _delta * i;
            }
            #endregion

            #region Histogram statistics

            //The number of data counts falling within a specific interval
            int count = 0;

            if (intervalType == IntervalType.LeftClosed)
            {
                //Left closed right open interval mode statistics
                for (int i = 0; i < numberOfIntervals; i++) //The outer loop realizes an interval statistics every cycle
                {
                    count = 0;//Count value initialization

                    for (int j = 0; j < nums.Length; j++)
                    {
                        if (nums[j] >= (intervals[i] - _deltaHalf) && nums[j] < (intervals[i] + _deltaHalf))
                        {
                            count++;
                        }
                    }
                    histgram[i] = count;//The interval count value is written into the corresponding position of the histogram to complete the ith interval count statistics.
                }

                //Last interval right boundary data statistics
                for (int j = 0; j < nums.Length; j++)
                {
                    if (nums[j] == _dataMax)
                    {
                        histgram[numberOfIntervals - 1]++;
                    }
                }
            }
            else
            {
                //Left open right closed interval mode statistics
                for (int i = 0; i < numberOfIntervals; i++)  //The outer loop realizes an interval statistics every cycle
                {
                    count = 0;//Count value initialization

                    for (int j = 0; j < nums.Length; j++)
                    {
                        if (nums[j] > (intervals[i] - _deltaHalf) && nums[j] <= (intervals[i] + _deltaHalf))
                        {
                            count++;
                        }
                    }
                    histgram[i] = count;//The interval count value is written into the corresponding position of the histogram to complete the ith interval count statistics.
                }

                //The first interval left boundary data statistics
                for (int j = 0; j < nums.Length; j++)
                {
                    if (nums[j] == _dataMin)
                    {
                        histgram[0]++;
                    }
                }
            }
            #endregion
        }

        private enum IntervalType
        {
            //
            // Summary:
            //     Except that the rightmost interval is a closed interval, all other intervals are left closed right open form.
            LeftClosed = 0,
            //
            // Summary:
            //     Except that the leftmost interval is a closed interval, all other intervals are left open right closed interval
            RightClosed = 1
        }
        #endregion
    }
}
