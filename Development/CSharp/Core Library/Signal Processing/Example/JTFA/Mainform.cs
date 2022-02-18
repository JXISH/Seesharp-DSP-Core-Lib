/*************************************************
  * JTFA example
  * Input signal can be real signal or complex signal
  * The size of the window slide dt=FrequencyBins/4
  * The abscissa of the light map is the frequency and the ordinate is the time.
  * Rainbow color: red represents the strongest, black (or blue) represents the weakest
  * Black and white: white represents the strongest, black represents the weakest
  * ***********************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SeeSharpTools.JXI.SignalProcessing.JTFA;
using SeeSharpTools.JY.ArrayUtility;
using System.Numerics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using SeeSharpTools.JY.GUI;
using SeeSharpTools.JXI.SignalProcessing.Generation;
using SeeSharpTools.JXI.SignalProcessing.Window;

namespace JTFA
{
    public partial class Mainform : Form
    {

        #region Private Field
        GeneralJTFATask _task;
        #endregion

        #region Constructor
        public Mainform()
        {
            InitializeComponent();
            comboBoxColorType.SelectedIndex = 3;
            _task = new GeneralJTFATask();
        }
        #endregion

        #region Event Handler
        /// <summary>
        /// Generate and analyze
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAnalysis_Click(object sender, EventArgs e)
        {
            _task.SampleRate = (double)MaxFrequency.Value;//Sampling rate
            _task.FrequencyBins = (int)SpectralLineCount.Value;//Window size, is each FFT point
            _task.WindowType = (WindowType)Enum.Parse(typeof(WindowType), WindowTypes.Text);//Window type 
            _task.ColorTable = (GeneralJTFATask.ColorTableType)comboBoxColorType.SelectedIndex;
            var noise = new double[(int)SampleCount.Value];
            Generation.UniformWhiteNoise(ref noise, (double)NoiseAmplitude.Value );//Noise
           
            if (SignalType.SelectedItem.ToString()  =="Real")//Input signal is real
            {              
                double[,] spec = null;
                //Generating an input signal
                var sin100Hz = new double[(int)SampleCount.Value];
                double w = 2 * Math.PI * (double)MaxFrequency.Value; //angular speed
                double T = (int)SampleCount.Value / (double)MaxFrequency.Value; //total time
                double t = 0;
                //Linear frequency modulation
                for (int i = 0; i < sin100Hz.Length; i++)
                {
                    t+= 1 / (double)MaxFrequency.Value;
                    //线性调频 a*sin( 积分(w*t/T))
                    sin100Hz[i] = (double)DCOffset.Value + (double)Amplitude.Value * Math.Sin(w / 2 / T * t * t);
                }
                ArrayCalculation.Add(sin100Hz, noise, ref sin100Hz);
                _task.GetJTFA(sin100Hz, ref spec);//Get JTFA
                //Convert to DB
                double[,] specDB = new double[spec.GetLength(0),spec.GetLength(1)];
                for (int i = 0; i < specDB.GetLength(0); i++)
                {
                    for (int j = 0; j < specDB.GetLength(1); j++)
                    {
                        specDB[i, j] = 10 * Math.Log10(spec[specDB.GetLength(0)-1-i, j]);
                    }
                }
                //Get the intensity map
                Bitmap  myImage1 = new Bitmap(spec.GetLength(1), spec.GetLength(0));
                _task.GetImage(specDB, ref myImage1);
                //Resize intensity map 
                //pictureBox_frequency_time.Width = spec.GetLength(1);
                //pictureBox_frequency_time.Height = spec.GetLength(0);
               var myImage = new Bitmap(myImage1, new Size(pictureBox_frequency_time.Width, pictureBox_frequency_time.Height));
                //var myImage = new Bitmap(myImage1);     
                //Display                             
                pictureBox_frequency_time.Image = myImage;
                easyChart1.Plot(sin100Hz, 0, 1.0 / (double)MaxFrequency.Value);//Input waveform
                labeldf.Text = _task.JTFAInfomation.df.ToString();//Spectral interval
                labeldt.Text = _task.JTFAInfomation.dt.ToString();//Time interval
                labelf0.Text = _task.JTFAInfomation.f0.ToString();//Start frequency
                WaterfallPlot(easyChartXWaterfall, spec, _task.JTFAInfomation.df, (double)0.002);
            }
            else//Input signal is plural
            {
                double[,] spec = null;
                //Generating an input signal
                double sampleRate = _task.SampleRate, frequency = (double)Frequency.Value;
                var signal = new Complex[(int)SampleCount.Value];
                var sin100Hz = new double[(int)SampleCount.Value];
                var cos100Hz = new double[(int)SampleCount.Value];
                double w = 2 * Math.PI * (double)MaxFrequency.Value; //angular speed
                double T = (int)SampleCount.Value / (double)MaxFrequency.Value; //total time
                double t = 0;
                //Linear frequency modulation
                for (int i = 0; i < sin100Hz.Length; i++)
                {
                    t += 1 / (double)MaxFrequency.Value;
                    sin100Hz[i] = (double)DCOffset.Value + (double)Amplitude.Value * Math.Sin(w / T * t * t - w * t);
                    cos100Hz[i] = (double)DCOffset.Value + (double)Amplitude.Value * Math.Cos(w / T * t * t - w * t);
                }
                ArrayCalculation.Add(sin100Hz, noise, ref sin100Hz);
                ArrayCalculation.Add(cos100Hz, noise, ref cos100Hz);
                for (int i = 0; i < sin100Hz.Length; i++)
                {
                    signal[i] = new Complex(cos100Hz[i], sin100Hz[i]);
                }
                //Get JTFA
                _task.GetJTFA(signal, ref spec);
                //Convert to DB form
                double[,] specDB = new double[spec.GetLength(0), spec.GetLength(1)];
                for (int i = 0; i < specDB.GetLength(0); i++)
                {
                    for (int j = 0; j < specDB.GetLength(1); j++)
                    {
                        specDB[i, j] = 10 * Math.Log10(spec[specDB.GetLength(0)-1-i, j]);
                    }
                }
                //Get the intensity map
                Bitmap myImage1 = new Bitmap(spec.GetLength(1), spec.GetLength(0));
                _task.GetImage(specDB, ref myImage1);
                //Resize intensity map   
                var myImage = new Bitmap(myImage1, new Size(pictureBox_frequency_time.Width, pictureBox_frequency_time.Height));
                //Display                             
                pictureBox_frequency_time.Image = myImage;
                labeldf.Text = _task.JTFAInfomation.df.ToString();//Spectral interval
                labeldt.Text = _task.JTFAInfomation.dt.ToString();//Time interval
                labelf0.Text = _task.JTFAInfomation.f0.ToString();//Start frequency
                var dispWave = new double[2, (int)SampleCount.Value];
                for (int i = 0; i < (int)SampleCount.Value; i++)
                {
                    dispWave[0, i] = cos100Hz[i];
                    dispWave[1, i] = sin100Hz[i];
                }
                easyChart1.Plot(dispWave, 0, 1.0 / (double)MaxFrequency.Value);         
                WaterfallPlot(easyChartXWaterfall, spec, _task.JTFAInfomation.df, (double)0.2);
            }

        }

        /// <summary>
        /// Mainform Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mainform_Load(object sender, EventArgs e)
        {
            var windowTypes = Enum.GetNames(typeof(WindowType));
            foreach (var item in windowTypes)
            {
                WindowTypes.Items.Add(item);//Add window type
            }
            WindowTypes.SelectedIndex = 3;// window
            SignalType.SelectedIndex = 0;//Real number
        }
        #endregion

        #region Methods
        /// <summary>
        /// WaterfallPlot
        /// </summary>
        /// <param name="Chart">EasyChartX</param>
        /// <param name="data">spectrum</param>
        /// <param name="dX">df</param>
        /// <param name="YStep"></param>
        void WaterfallPlot(EasyChartX Chart, double[,] data, double dX, double YStep)
        {
            int signalLength = data.GetLength(1);
            int numOfSignals = Math.Max(2, data.GetLength(0)); // must bigger than 2 or xStep goes wrong
            int xRange = (int)(signalLength * 1.5); //waterfall extented length in X direction
            double xStep = (xRange - signalLength) / (numOfSignals - 1);
            double[] yMax = new double[xRange]; //max for maskering off the behind
            double[,] waterfallData = new double[numOfSignals, xRange];

            for (int i = 0; i < xRange; i++)
            {
                yMax[i] = double.NegativeInfinity;
            }

            int dataStartIndex = 0;
            double y = 0;
            for (int i = 0; i < numOfSignals; i++)
            {
                int dataIndex = 0;
                for (int j = 0; j < xRange; j++)
                {
                    dataStartIndex = (int)Math.Round(xStep * i);
                    //if out side data range, set -inf (for hidding)
                    if (j < dataStartIndex || j >= (dataStartIndex + signalLength))
                    {
                        waterfallData[i, j] = double.NaN;
                    }
                    //Inside data range, compare with yMax, if bigger then keep, else -inf
                    else
                    {
                        y = data[i, dataIndex] + i * YStep;
                        if (y > yMax[j])
                        {
                            yMax[j] = y;
                            waterfallData[i, j] = y;
                        }
                        else
                        {
                            waterfallData[i, j] = yMax[j];
                        }
                        dataIndex++;
                    }
                }
            }

            Chart.Miscellaneous.CheckInfinity = true;
            Chart.Miscellaneous.MaxSeriesCount = numOfSignals;
            Chart.Plot(waterfallData, 0, dX);
            for (int i = 0; i < numOfSignals; i++)
            {
                Chart.Series[i].Color = Color.FromArgb(0, 0, 128);
            }
        }
        #endregion
    }
}
