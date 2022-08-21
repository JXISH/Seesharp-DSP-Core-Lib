using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeeSharpTools.JY.GUI;
using SeeSharpTools.JXI.SignalProcessing.Generation;

namespace Winform_Noise_Generator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonGaussNoise_Click(object sender, EventArgs e)
        {
            //Get parameters
            double sigma = (double)numericUpDownSigma.Value;
            double mean = (double)numericUpDownMean.Value;
            double[] noise = new double[100];
            string noiseType = domainUpDownNoiseType.Text;
            int numberOfIntervals = (int)numericUpDownBarChartNum.Value;

            if(noiseType is "Gauss Noise")
            {
                NoiseGenerator.GaussNiose(noise, sigma, mean);
            }
            else
            {
                NoiseGenerator.WhiteNoise(noise, sigma, mean);
            }
            double[] intervals = new double[numberOfIntervals];//Median value of each bar
            double[] histogram = new double[numberOfIntervals];//Bar chart
            Histogram(noise, numberOfIntervals, out histogram, out intervals);

            //Display
            easyChartNoise.Plot(noise);//Noise
            for (int i = 0; i < intervals.Length; i++)
            {
                intervals[i] = Math.Round(intervals[i], 5);
            }
            //Console.WriteLine();
            //foreach (var item in intervals)
            //{
            //    Console.Write("{0} ", item);
            //}
            easyChartHistogram.Plot(histogram, intervals[0], (intervals[1] - intervals[0]));
            
        }

        private void easyChartGaussNoise_AxisViewChanged(object sender, SeeSharpTools.JY.GUI.EasyChartEvents.EasyChartViewEventArgs e)
        {

        }

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

        private void numericUpDownBarChartNum_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
