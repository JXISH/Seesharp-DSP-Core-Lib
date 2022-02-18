/*******************************************
  * FIRFilter example
  * Generate filter coefficients using Matlab
  * Select a different Filter type to read the corresponding .mat file
  * View time domain waveforms and spectra before and after Filter
  * Note:
  * 1. Select whether to use Reset() to clear the Filter status register and Filter points according to your needs.
  * 2. Automatically reset() when the filter unit length changes.
  * *****************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Data.Matlab;
using SeeSharpTools.JXI.SignalProcessing.Conditioning.EasyFilters;
using SeeSharpTools.JXI.SignalProcessing.GeneralSpectrum;
using SeeSharpTools.JXI.SignalProcessing.Generation;
using SeeSharpTools.JXI.SignalProcessing.Window;
using SeeSharpTools.JY.ArrayUtility;
using System.Numerics;


namespace FIRFlter_Winform
{
    public partial class Mainform : Form
    {
        #region Private Field
        /// <summary>
        /// Filter coefficient
        /// </summary>
        private double[] FilterCoe;

        /// <summary>
        /// Coefficient file storage path
        /// </summary>
        private string path;

        /// <summary>
        /// FIRFilter task
        /// </summary>
        private FIRFilter _fir = new FIRFilter();
        #endregion

        #region Constructor
        public Mainform()
        {
            InitializeComponent();
            path = Environment.CurrentDirectory;
            path = path.Substring(0, path.Length - 10);
        }
        #endregion

        #region Event Handler
        /// <summary>
        /// Loading interface
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mainform_Load(object sender, EventArgs e)
        {
            radioButtonLPF.Checked = true;//Default low pass
            //Hide
            labelFilterNum3.Visible = false;
            labelFilterNum3Value.Visible = false;
            labelFilterNum4.Visible = false;
            labelFilterNum4Value.Visible = false;
            //Filter parameter
            labelFilterNum1.Text = "fpass：";
            labelFilterNum1Value.Text = "0.03";
            labelFilterNum2.Text = "fstop：";
            labelFilterNum2Value.Text = "0.07";
        }

        /// <summary>
        /// Start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            //Generate time domain waveforms based on the selected filter
            int length = 2000;
            double[] sinWaveTemp = new double[length];
            double[] sinWave = new double[length];
            double[] noise = new double[length];//Noise
            double sampleRate = 100;//Sampling rate
            labelSampleRate.Text = sampleRate.ToString();

            double Amp = 1;//Amplitude
            Generation.UniformWhiteNoise(ref noise, Amp * 0.05);//Generate noise
            Generation.SineWave(ref sinWaveTemp, Amp, 0, sampleRate * 0.01, sampleRate);//Low frequency signal
            Generation.SineWave(ref sinWave, Amp, 0, sampleRate * 0.1, sampleRate);//High frequency signal
            ArrayCalculation.Add(sinWave, sinWaveTemp, ref sinWave);//Dual frequency signal
            ArrayCalculation.Add(sinWave, noise, ref sinWave);//Plus noise
            labelFrequency.Text = "1, 10";

            //Clear the filter status register. If you need to use the same configuration of the filter to continuously calculate multiple times, you don't need reset.
            _fir.Reset();//Reset            
            double[] sinWaveFiltered = new double[length];
            _fir.Filter(sinWave, ref sinWaveFiltered);//Filter
            labelFilteredPoints.Text = _fir.FilteredPointns.ToString();//Number of points through the filter
            //Calculated spectrum
            double[] spectrumSinWave = new double[length / 2];
            double[] spectrumFiltered = new double[length / 2];
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.SampleRate = sampleRate;//Sampling rate
            _task.InputDataType = InputDataType.Real;//Input type
            _task.WindowType = WindowType.Hamming;//Window type
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;//Average mode
            _task.Output.NumberOfLines = length / 2;//Output spectrum number
            _task.Unit.Type = SpectrumOutputUnit.V; //Unit
            _task.Unit.IsPSD = false;//Whether the density spectrum, pay attention to true, unit must be V2
            _task.GetSpectrum(sinWaveFiltered, ref spectrumFiltered);
            _task.GetSpectrum(sinWave, ref spectrumSinWave);
            //Drawing
            easyChartWaveform.Plot(sinWave, 0, 1 / sampleRate);//Time domain signal before filter
            easyChartWaveformFilter.Plot(sinWaveFiltered, 0, 1 / sampleRate);//Time domain signal after filtering
            easyChartSpectrum.Plot(spectrumSinWave, _task.SpectralInfomation.FreqStart, _task.SpectralInfomation.FreqDelta);//Frequency domain signal before filter
            easyChartSpectrumFilter.Plot(spectrumFiltered, _task.SpectralInfomation.FreqStart, _task.SpectralInfomation.FreqDelta);//Filtered frequency domain signal
        }
        /// <summary>
        /// LPF low pass
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonLPF_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonLPF.Checked)
            {
                //Read the data corresponding to num in the .mat file
                Matrix<double> m = MatlabReader.Read<double>(path + @"\lpf_0p03_0p07_40db.mat", "Num");
                //Create a filter coefficient array
                FilterCoe = new double[m.ColumnCount];
                //Get filter coefficient
                var tmp = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)m).Values;
                //Torture array
                Array.Copy(tmp, FilterCoe, m.ColumnCount);
                _fir.Coefficients = FilterCoe;//Filter coefficient
                //Filter parameter
                labelFilterNum1.Text = "fpass：";
                labelFilterNum1Value.Text = "0.03";
                labelFilterNum2.Text = "fstop：";
                labelFilterNum2Value.Text = "0.07";
                //Hide
                labelFilterNum3.Visible = false;
                labelFilterNum3Value.Visible = false;
                labelFilterNum4.Visible = false;
                labelFilterNum4Value.Visible = false;
                //Pre-filter time domain signal frequency
                labelFrequency.Text = "1, 10";
            }
        }

        /// <summary>
        /// HPF Qualcomm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonHPF_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonHPF.Checked)
            {
                //Read the data corresponding to num in the .mat file
                Matrix<double> m = MatlabReader.Read<double>(path + @"\hpf_0p03_0p07_40db.mat", "Num");
                //Create a filter coefficient array
                FilterCoe = new double[m.ColumnCount];
                //Get filter coefficient
                var tmp = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)m).Values;
                //Torture array
                Array.Copy(tmp, FilterCoe, m.ColumnCount);
                _fir.Coefficients = FilterCoe;//Filter coefficient
                //Filter parameter
                labelFilterNum1.Text = "fstop：";
                labelFilterNum1Value.Text = "0.03";
                labelFilterNum2.Text = "fpass：";
                labelFilterNum2Value.Text = "0.07";
                //Hide
                labelFilterNum3.Visible = false;
                labelFilterNum3Value.Visible = false;
                labelFilterNum4.Visible = false;
                labelFilterNum4Value.Visible = false;
                //Pre-filter time domain signal frequency
                labelFrequency.Text = "1, 10";
            }
        }

        /// <summary>
        /// BPF bandpass
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonBPF_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonBPF.Checked)
            {
                //Read the data corresponding to num in the .mat file
                Matrix<double> m = MatlabReader.Read<double>(path + @"\bpf_0p03_0p07_0p2_0p25_40db.mat", "Num");
                //Create a filter coefficient array
                FilterCoe = new double[m.ColumnCount];
                //Get filter coefficient
                var tmp = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)m).Values;
                //Torture array
                Array.Copy(tmp, FilterCoe, m.ColumnCount);
                _fir.Coefficients = FilterCoe;//Filter coefficient
                //Filter parameter
                labelFilterNum1.Text = "fstop1：";
                labelFilterNum1Value.Text = "0.03";
                labelFilterNum2.Text = "fpass1：";
                labelFilterNum2Value.Text = "0.07";
                labelFilterNum3.Text = "fpass2：";
                labelFilterNum3Value.Text = "0.2";
                labelFilterNum4.Text = "fstop2：";
                labelFilterNum4Value.Text = "0.25";
                //Not hidden
                labelFilterNum3.Visible = true;
                labelFilterNum3Value.Visible = true;
                labelFilterNum4.Visible = true;
                labelFilterNum4Value.Visible = true;
                //Pre-filter time domain signal frequency
                labelFrequency.Text = "1, 10";
            }
        }

        /// <summary>
        /// BSP band stop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonBSF_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonBSF.Checked)
            {
                //Read the data corresponding to num in the .mat file
                Matrix<double> m = MatlabReader.Read<double>(path + @"\bsf_0p03_0p07_0p2_0p25_40db.mat", "Num");
                //Create a filter coefficient array
                FilterCoe = new double[m.ColumnCount];
                //Get filter coefficient
                var tmp = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)m).Values;
                //Torture array
                Array.Copy(tmp, FilterCoe, m.ColumnCount);
                _fir.Coefficients = FilterCoe;//Filter coefficient
                //Filter parameter
                labelFilterNum1.Text = "fpass1：";
                labelFilterNum1Value.Text = "0.03";
                labelFilterNum2.Text = "fstop1：";
                labelFilterNum2Value.Text = "0.07";
                labelFilterNum3.Text = "fstop2：";
                labelFilterNum3Value.Text = "0.2";
                labelFilterNum4.Text = "fpass2：";
                labelFilterNum4Value.Text = "0.25";
                //Not hidden
                labelFilterNum3.Visible = true;
                labelFilterNum3Value.Visible = true;
                labelFilterNum4.Visible = true;
                labelFilterNum4Value.Visible = true;
                //Pre-filter time domain signal frequency
                labelFrequency.Text = "1, 10";
            }
        }
        #endregion

        #region Methods
        #endregion
    }
}
