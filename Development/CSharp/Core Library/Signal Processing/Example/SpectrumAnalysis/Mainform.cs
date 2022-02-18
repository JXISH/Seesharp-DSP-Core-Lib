/*************************************************
  * spectrum example
  * Input signal can be Real number signal or complex signal
  * ***********************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Numerics;
using SeeSharpTools.JY.ArrayUtility;
using SeeSharpTools.JXI.SignalProcessing.Generation;
using SeeSharpTools.JXI.SignalProcessing.GeneralSpectrum;
using SeeSharpTools.JXI.SignalProcessing.Window;

namespace PowerSpectrum_Demo
{
    public partial class Mainform : Form
    {
        #region Private Field
        /// <summary>
        /// SpectrumTask
        /// </summary>
        private GeneralSpectrumTask _task;

        /// <summary>
        /// Do you need to resubmit the configuration?
        /// </summary>
        private bool _needCommit;
        #endregion

        #region Constructor
        public Mainform()
        {
            InitializeComponent();
            _task = new GeneralSpectrumTask();
            _needCommit = true;
        }
        #endregion

        #region Event Handler
        /// <summary>
        /// Generate and analyze
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //Do you need to resubmit the configuration?
            if (_needCommit)
            {
                CommitConfig_Click(null, null);
            }

            var spec = new double[(int)SpectralLineCount.Value];
            //Noise
            var noise = new double[(int)SampleCount.Value];
            Generation.UniformWhiteNoise(ref noise, (double)Amplitude.Value * 0.05); 

            if (_task.InputDataType == InputDataType.Real)//Input signal is real
            {
                //Generating an input signal
                var sin100Hz = new double[(int) SampleCount.Value];
                Generation.SineWave(ref sin100Hz, (double)Amplitude.Value, 0, (double)Frequency.Value,
                    (double)SampleRate.Value);              
                for (int i = 0; i < sin100Hz.Length; i++)//Plus DC
                {
                    sin100Hz[i] += (double) DCOffset.Value;
                }
                ArrayCalculation.Add(sin100Hz, noise, ref sin100Hz);//Plus noise
                var st = DateTime.Now;
                _task.GetSpectrum(sin100Hz, ref spec);//Acquisition spectrum
                var sp = DateTime.Now - st;
                ElapsedTime.Text = sp.TotalMilliseconds.ToString("F3");
                Peak a = _task.FindPeak(spec);//Find peak
                var powerInband = _task.MeasurePowerInBand(spec,_task.SampleRate / 4, _task.SampleRate / 2);//In-band power UnitV2
                //Display
                labelPeak.Text = a.PeakValue.ToString("f3");
                labelPowerInBand.Text = powerInband.ToString("f3");
                easyChart1.Plot(sin100Hz,0, 1.0/(double)SampleRate.Value);
            }
            else
            {
                //Generating complex signals
                double sampleRate = _task.SampleRate, frequency = (double)Frequency.Value;
                var signal = new Complex[(int)SampleCount.Value];
                var sin100Hz = new double[(int)SampleCount.Value];
                Generation.SineWave(ref sin100Hz, (double)Amplitude.Value, 0, frequency, sampleRate);
                var cos100Hz = new double[(int)SampleCount.Value];
                Generation.SineWave(ref cos100Hz, (double)Amplitude.Value, 90, frequency, sampleRate);
                ArrayCalculation.Add(sin100Hz, noise, ref sin100Hz);
                ArrayCalculation.Add(cos100Hz, noise, ref cos100Hz);
                for (int i = 0; i < sin100Hz.Length; i++)
                {
                    sin100Hz[i] += (double)DCOffset.Value;
                }
                for (int i = 0; i < sin100Hz.Length; i++)
                {
                    cos100Hz[i] += (double)DCOffset.Value;
                }
                for (int i = 0; i < sin100Hz.Length; i++)
                {
                    signal[i] = new Complex(cos100Hz[i], sin100Hz[i]);
                }
                var st = DateTime.Now;
                _task.GetSpectrum(signal, ref spec);//Calculated spectrum
                var sp = DateTime.Now - st;
                ElapsedTime.Text = sp.TotalMilliseconds.ToString("F3");
                var dispWave = new double[2, (int) SampleCount.Value];
                for (int i = 0; i < (int) SampleCount.Value; i++)
                {
                    dispWave[0, i] = cos100Hz[i];
                    dispWave[1, i] = sin100Hz[i];
                }
                easyChart1.Plot(dispWave, 0, 1.0/(double)SampleRate.Value);
                Peak a = _task.FindPeak(spec);//Find peak
                var powerInband = _task.MeasurePowerInBand(spec, 0, sampleRate);//In-band power UnitV2
                //Display
                labelPeak.Text = a.PeakValue.ToString("f3");//Peak
                labelPowerInBand.Text = powerInband.ToString("f3");//In-band power UnitV2
            }
            easyChart2.Plot(spec, _task.SpectralInfomation.FreqStart, _task.SpectralInfomation.FreqDelta);
            //Display
            FFTSize.Text = _task.SpectralInfomation.FFTSize.ToString();//FFT size
            FFTCount.Text = _task.SpectralInfomation.FFTCount.ToString();//FFT points
            f0.Text = _task.SpectralInfomation.FreqStart.ToString("F2");//Start frequency
            df.Text = _task.SpectralInfomation.FreqDelta.ToString("F2");//Spectral interval
        }

        /// <summary>
        /// Form1 Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            var specUnits = Enum.GetNames(typeof(SpectrumOutputUnit));//Unit
            var windowTypes = Enum.GetNames(typeof(WindowType));//Window type
            var signalTypes = Enum.GetNames(typeof(InputDataType));//Type of data
            var averageModes = Enum.GetNames(typeof(SpectrumAverageMode));//Average mode
            var weightTypes = Enum.GetNames(typeof(SpectrumWeightingType));//Weighted type
            foreach (var item in specUnits)
            {
                SpectrumUnits.Items.Add(item);//Add Unit
            }
            SpectrumUnits.SelectedIndex = 0;

            foreach (var item in windowTypes)
            {
                WindowTypes.Items.Add(item);//Add window
            }
            WindowTypes.SelectedIndex = 0;

            foreach (var item in signalTypes)
            {
                SignalType.Items.Add(item);//Add type of data
            }
            SignalType.SelectedIndex = 0;

            foreach (var item in averageModes)
            {
                AverageMode.Items.Add(item);//Add average mode
            }
            AverageMode.SelectedIndex = 0;

            foreach (var item in weightTypes)
            {
                WeightType.Items.Add(item);//Add weighted type
            }
            WeightType.SelectedIndex = 0;//Rectangular window
        }

        /// <summary>
        /// Reconfiguration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetConfig_Click(object sender, EventArgs e)
        {
            _task.Reset();//Reset
            //Display
            FFTSize.Text = _task.SpectralInfomation.FFTSize.ToString();//FFT size
            FFTCount.Text = _task.SpectralInfomation.FFTCount.ToString();//FFT points
            f0.Text = _task.SpectralInfomation.FreqStart.ToString("F2");//Start frequency
            df.Text = _task.SpectralInfomation.FreqDelta.ToString("F2");//Spectral interval

            CommitConfig.Enabled = true;
            ResetConfig.Enabled = false;
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;

            _needCommit = true;//Need to resubmit configuration
        }

        /// <summary>
        /// Submit configuration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommitConfig_Click(object sender, EventArgs e)
        {
            _task.InputDataType = (InputDataType)Enum.Parse(typeof(InputDataType), SignalType.Text);//Input type
            _task.SampleRate = (double)SampleRate.Value;//Sampling rate
            _task.WindowType = (WindowType)Enum.Parse(typeof(WindowType), WindowTypes.Text);//Window type

            _task.Average.Mode = (SpectrumAverageMode)Enum.Parse(typeof(SpectrumAverageMode), AverageMode.Text);//Average mode
            _task.Average.WeightingType = (SpectrumWeightingType)Enum.Parse(typeof(SpectrumWeightingType), WeightType.Text);//Weighted type
            _task.Average.Size = (double)AverageSize.Value;//Average size

            _task.Output.NumberOfLines = (int)SpectralLineCount.Value;//Output spectrum number

            _task.Unit.Type = (SpectrumOutputUnit)Enum.Parse(typeof(SpectrumOutputUnit), SpectrumUnits.Text);//Unit
            _task.Unit.Impedance = 50;//Resistance
            _task.Unit.IsPSD = false;//Whether the density spectrum, pay attention to true, unit must be V2
            //Display
            FFTSize.Text = _task.SpectralInfomation.FFTSize.ToString();//FFT size
            FFTCount.Text = _task.SpectralInfomation.FFTCount.ToString();//FFT points
            f0.Text = _task.SpectralInfomation.FreqStart.ToString("F2");//Start frequency
            df.Text = _task.SpectralInfomation.FreqDelta.ToString("F2");//Spectral interval

            CommitConfig.Enabled = false;
            ResetConfig.Enabled = true;
        }

        /// <summary>
        /// Sampling points change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SampleCount_ValueChanged(object sender, EventArgs e)
        {
            var tp = (InputDataType) Enum.Parse(typeof(InputDataType), SignalType.Text);
            SpectralLineCount.Value = tp== InputDataType.Real?(int)SampleCount.Value/2://Determine whether it is real number, the number of lines changes according to type of data
            ((int) SampleCount.Value);
            _needCommit = true;//Need to resubmit configuration
        }

        /// <summary>
        /// Enter type of data to change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignalType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tp = (InputDataType)Enum.Parse(typeof(InputDataType), SignalType.Text);
            SpectralLineCount.Value = tp == InputDataType.Real ? (int)SampleCount.Value / 2 ://Determine whether it is real number, the number of lines changes according to type of data
            ((int)SampleCount.Value);

            _needCommit = true;//Need to resubmit configuration
        }

        /// <summary>
        /// Average mode change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AverageMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            _needCommit = true;//Need to resubmit configuration
        }

        /// <summary>
        /// Weighted type change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeightType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _needCommit = true;//Need to resubmit configuration
        }

        /// <summary>
        /// Average size change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AverageSize_ValueChanged(object sender, EventArgs e)
        {
            _needCommit = true;//Need to resubmit configuration
        }
        #endregion

        #region Methods
        #endregion
    }
}

