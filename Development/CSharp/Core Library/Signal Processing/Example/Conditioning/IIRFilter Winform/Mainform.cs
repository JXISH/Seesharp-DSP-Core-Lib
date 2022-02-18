/*******************************************
  * IIRFilter example
  * Use Matlab to generate Filter coefficient
  * Select a different Filter type to read the corresponding .mat file
  * View time domain waveforms and spectra before and after Filter
  * Note:
  * 1. Select whether to use Reset() to clear the Filter status register and Filter points according to your needs.
  * 2. When the length of the Filter coefficient changes, Auto Reset()
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
using SeeSharpTools.JXI.SignalProcessing.Window;
using SeeSharpTools.JXI.SignalProcessing.Generation;
using SeeSharpTools.JY.ArrayUtility;
using System.Numerics;

namespace IIRFilter_Winform
{
    public partial class Mainform : Form
    {
        #region Private Field
        /// <summary>
        /// Filter coefficient numerator
        /// </summary>
        private double[] numerator;

        /// <summary>
        /// Filter coefficient denominator
        /// </summary>
        private double[] demoniator;

        /// <summary>
        /// Filter coefficient gain
        /// </summary>
        private double[] k;

        /// <summary>
        /// Filter coefficient
        /// </summary>
        private double[,] SOS = null;

        /// <summary>
        /// Filter coefficient gain
        /// </summary>
        private double[] G = null;

        /// <summary>
        /// Filter coefficient
        /// </summary>
        private string path;

        /// <summary>
        /// FIRFilter task
        /// </summary>
        private IIRFilter _IIR = new IIRFilter();
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
            Generation.UniformWhiteNoise(ref noise, Amp * 0.5);//Generate noise
            if (radioButtonBSF.Checked)
            {
                Generation.SineWave(ref sinWaveTemp, Amp, 0, sampleRate * 0.01, sampleRate);//Low frequency signal
                Generation.SineWave(ref sinWave, Amp, 0, sampleRate * 0.2, sampleRate);//High frequency signal
                ArrayCalculation.Add(sinWave, sinWaveTemp, ref sinWave);//Dual frequency signal
                ArrayCalculation.Add(sinWave, noise, ref sinWave);//Plus noise
                labelFrequency.Text = "1, 20";
            }
            else
            {
                Generation.SineWave(ref sinWaveTemp, Amp, 0, sampleRate * 0.01, sampleRate);//Low frequency signal
                Generation.SineWave(ref sinWave, Amp, 0, sampleRate * 0.1, sampleRate);//High frequency signal
                ArrayCalculation.Add(sinWave, sinWaveTemp, ref sinWave);//Dual frequency signal
                ArrayCalculation.Add(sinWave, noise, ref sinWave);//Plus noise
                labelFrequency.Text = "1, 10";
            }

            //Clear the filter status register. If you need to use the same configuration of the filter to continuously calculate multiple times, you don't need reset.
            //_IIR.Reset();//Reset            
            double[] sinWaveFiltered = new double[length];
            _IIR.Filter(sinWave, ref sinWaveFiltered);//Filter
            if (!checkBoxIsSOS.Checked)
            {
                ArrayCalculation.MultiplyScale(ref sinWaveFiltered, k[0]);//Scale with k
            }
            labelFilteredPoints.Text = _IIR.FilteredPointns.ToString();//Number of points through the filter
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
                labelFrequency.Text = "1, 10";
                if (checkBoxIsSOS.Checked)
                {
                    //Clear coefficient matrix
                    SOS = null;
                    G = null;
                    //Read the .mat file
                    Matrix<double> MATLAB_SOS = MatlabReader.Read<double>(path + @"\lpf_0p03_0p07_40db_SOS.mat", "SOS");
                    //Create a filter coefficient array
                    SOS = new double[MATLAB_SOS.RowCount, MATLAB_SOS.ColumnCount];
                    //Get filter coefficient
                    var tmp_SOS = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)MATLAB_SOS).Values;
                    //Torture array
                    for (int i = 0; i < SOS.GetLength(0); i++)
                    {
                        for (int j = 0; j < SOS.GetLength(1); j++)
                        {
                            SOS[i, j] = tmp_SOS[i + j * (SOS.GetLength(0))];
                        }
                    }
                    //Read the .mat file
                    Matrix<double> MATLAB_G = MatlabReader.Read<double>(path + @"\lpf_0p03_0p07_40db_SOS.mat", "G");
                    //Create a filter coefficient array
                    G = new double[MATLAB_G.RowCount];
                    //Get filter coefficient
                    var tmp_G = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)MATLAB_G).Values;
                    //Torture array
                    Array.Copy(tmp_G, G, MATLAB_G.RowCount);
                    _IIR.IsSOS = true;
                    _IIR.SetCoefficients(SOS, G);
                }
                else
                {
                    //Clear coefficient matrix
                    numerator = null;
                    demoniator = null;
                    k = null;

                    //Read the data in the .mat file corresponding to numerator
                    Matrix<double> MATLAB_numerator = MatlabReader.Read<double>(path + @"\lpf_0p03_0p07_40db.mat", "numerator");
                    //Create a filter coefficient array
                    numerator = new double[MATLAB_numerator.ColumnCount];
                    //Get filter coefficient
                    var tmp_numerator = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)MATLAB_numerator).Values;
                    //Torture array
                    Array.Copy(tmp_numerator, numerator, MATLAB_numerator.ColumnCount);

                    //Read the data in the .mat file demoniator
                    Matrix<double> MATLAB_demoniator = MatlabReader.Read<double>(path + @"\lpf_0p03_0p07_40db.mat", "demoniator");
                    //Create a filter coefficient array
                    demoniator = new double[MATLAB_demoniator.ColumnCount];
                    //Get filter coefficient
                    var tmp_demoniator = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)MATLAB_demoniator).Values;
                    //Torture array
                    Array.Copy(tmp_demoniator, demoniator, MATLAB_demoniator.ColumnCount);

                    //Read the data corresponding to k in the .mat file
                    Matrix<double> MATLAB_k = MatlabReader.Read<double>(path + @"\lpf_0p03_0p07_40db.mat", "k");
                    //Create a filter coefficient array
                    k = new double[MATLAB_k.ColumnCount];
                    //Get filter coefficient
                    var tmp_k = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)MATLAB_k).Values;
                    //Torture array
                    Array.Copy(tmp_k, k, MATLAB_k.ColumnCount);
                    _IIR.IsSOS = false;
                    //Set filter coefficient
                    _IIR.SetCoefficients(numerator, demoniator);
                }

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
                labelFrequency.Text = "1, 10";
                if (checkBoxIsSOS.Checked)
                {
                    //Clear coefficient matrix
                    SOS = null;
                    G = null;
                    //Read the .mat file
                    Matrix<double> MATLAB_SOS = MatlabReader.Read<double>(path + @"\hpf_0p03_0p07_40db_SOS.mat", "SOS");
                    //Create a filter coefficient array
                    SOS = new double[MATLAB_SOS.RowCount, MATLAB_SOS.ColumnCount];
                    //Get filter coefficient
                    var tmp_SOS = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)MATLAB_SOS).Values;
                    //Torture array
                    for (int i = 0; i < SOS.GetLength(0); i++)
                    {
                        for (int j = 0; j < SOS.GetLength(1); j++)
                        {
                            SOS[i, j] = tmp_SOS[i + j * (SOS.GetLength(0))];
                        }
                    }

                    //Read the .mat file
                    Matrix<double> MATLAB_G = MatlabReader.Read<double>(path + @"\hpf_0p03_0p07_40db_SOS.mat", "G");
                    //Create a filter coefficient array
                    G = new double[MATLAB_G.RowCount];
                    //Get filter coefficient
                    var tmp_G = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)MATLAB_G).Values;
                    //Torture array
                    Array.Copy(tmp_G, G, MATLAB_G.RowCount);
                    _IIR.IsSOS = true;
                    _IIR.SetCoefficients(SOS, G);
                }
                else
                {
                    //Clear coefficient matrix
                    numerator = null;
                    demoniator = null;
                    k = null;
                    //Read the data in the .mat file corresponding to numerator
                    Matrix<double> MATLAB_numerator = MatlabReader.Read<double>(path + @"\hpf_0p03_0p07_40db.mat", "numerator");
                    //Create a filter coefficient array
                    numerator = new double[MATLAB_numerator.ColumnCount];
                    //Get filter coefficient
                    var tmp_numerator = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)MATLAB_numerator).Values;
                    //Torture array
                    Array.Copy(tmp_numerator, numerator, MATLAB_numerator.ColumnCount);

                    //Read the data in the .mat file demoniator
                    Matrix<double> MATLAB_demoniator = MatlabReader.Read<double>(path + @"\hpf_0p03_0p07_40db.mat", "demoniator");
                    //Create a filter coefficient array
                    demoniator = new double[MATLAB_demoniator.ColumnCount];
                    //Get filter coefficient
                    var tmp_demoniator = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)MATLAB_demoniator).Values;
                    //Torture array
                    Array.Copy(tmp_demoniator, demoniator, MATLAB_demoniator.ColumnCount);

                    //Read the data corresponding to k in the .mat file
                    Matrix<double> MATLAB_k = MatlabReader.Read<double>(path + @"\hpf_0p03_0p07_40db.mat", "k");
                    //Create a filter coefficient array
                    k = new double[MATLAB_k.ColumnCount];
                    //Get filter coefficient
                    var tmp_k = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)MATLAB_k).Values;
                    //Torture array
                    Array.Copy(tmp_k, k, MATLAB_k.ColumnCount);

                    _IIR.IsSOS = false;
                    //Set filter coefficient
                    _IIR.SetCoefficients(numerator, demoniator);
                }
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
                labelFrequency.Text = "1, 10";
                if (checkBoxIsSOS.Checked)
                {
                    //Clear coefficient matrix
                    SOS = null;
                    G = null;
                    //Read the .mat file
                    Matrix<double> MATLAB_SOS = MatlabReader.Read<double>(path + @"\bpf_0p03_0p07_0p3_0p4_40db_SOS.mat", "SOS");
                    //Create a filter coefficient array
                    SOS = new double[MATLAB_SOS.RowCount, MATLAB_SOS.ColumnCount];
                    //Get filter coefficient
                    var tmp_SOS = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)MATLAB_SOS).Values;
                    //Torture array
                    for (int i = 0; i < SOS.GetLength(0); i++)
                    {
                        for (int j = 0; j < SOS.GetLength(1); j++)
                        {
                            SOS[i, j] = tmp_SOS[i + j * (SOS.GetLength(0))];
                        }
                    }

                    //Read the .mat file
                    Matrix<double> MATLAB_G = MatlabReader.Read<double>(path + @"\bpf_0p03_0p07_0p3_0p4_40db_SOS.mat", "G");
                    //Create a filter coefficient array
                    G = new double[MATLAB_G.RowCount];
                    //Get filter coefficient
                    var tmp_G = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)MATLAB_G).Values;
                    //Torture array
                    Array.Copy(tmp_G, G, MATLAB_G.RowCount);
                    _IIR.IsSOS = true;
                    _IIR.SetCoefficients(SOS, G);
                }
                else
                {
                    //Clear coefficient matrix
                    numerator = null;
                    demoniator = null;
                    k = null;
                    //Read the data in the .mat file corresponding to numerator
                    Matrix<double> MATLAB_numerator = MatlabReader.Read<double>(path + @"\bpf_0p03_0p07_0p3_0p4_40db.mat", "numerator");
                    //Create a filter coefficient array
                    numerator = new double[MATLAB_numerator.ColumnCount];
                    //Get filter coefficient
                    var tmp_numerator = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)MATLAB_numerator).Values;
                    //Torture array
                    Array.Copy(tmp_numerator, numerator, MATLAB_numerator.ColumnCount);

                    //Read the data in the .mat file demoniator
                    Matrix<double> MATLAB_demoniator = MatlabReader.Read<double>(path + @"\bpf_0p03_0p07_0p3_0p4_40db.mat", "demoniator");
                    //Create a filter coefficient array
                    demoniator = new double[MATLAB_demoniator.ColumnCount];
                    //Get filter coefficient
                    var tmp_demoniator = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)MATLAB_demoniator).Values;
                    //Torture array
                    Array.Copy(tmp_demoniator, demoniator, MATLAB_demoniator.ColumnCount);

                    //Read the data corresponding to k in the .mat file
                    Matrix<double> MATLAB_k = MatlabReader.Read<double>(path + @"\bpf_0p03_0p07_0p3_0p4_40db.mat", "k");
                    //Create a filter coefficient array
                    k = new double[MATLAB_k.ColumnCount];
                    //Get filter coefficient
                    var tmp_k = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)MATLAB_k).Values;
                    //Torture array
                    Array.Copy(tmp_k, k, MATLAB_k.ColumnCount);

                    _IIR.IsSOS = false;
                    //Set filter coefficient
                    _IIR.SetCoefficients(numerator, demoniator);
                }
                //Filter parameter
                labelFilterNum1.Text = "fstop1：";
                labelFilterNum1Value.Text = "0.03";
                labelFilterNum2.Text = "fpass1：";
                labelFilterNum2Value.Text = "0.07";
                labelFilterNum3.Text = "fpass2：";
                labelFilterNum3Value.Text = "0.3";
                labelFilterNum4.Text = "fstop2：";
                labelFilterNum4Value.Text = "0.4";
                //Not hidden
                labelFilterNum3.Visible = true;
                labelFilterNum3Value.Visible = true;
                labelFilterNum4.Visible = true;
                labelFilterNum4Value.Visible = true;
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
                labelFrequency.Text = "1, 20";
                if (checkBoxIsSOS.Checked)
                {
                    //Clear coefficient matrix
                    SOS = null;
                    G = null;
                    //Read the .mat file
                    Matrix<double> MATLAB_SOS = MatlabReader.Read<double>(path + @"\bsf_0p1_0p2_0p3_0p4_40db_SOS.mat", "SOS");
                    //Create a filter coefficient array
                    SOS = new double[MATLAB_SOS.RowCount, MATLAB_SOS.ColumnCount];
                    //Get filter coefficient
                    var tmp_SOS = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)MATLAB_SOS).Values;
                    //Torture array
                    for (int i = 0; i < SOS.GetLength(0); i++)
                    {
                        for (int j = 0; j < SOS.GetLength(1); j++)
                        {
                            SOS[i, j] = tmp_SOS[i + j * (SOS.GetLength(0))];
                        }
                    }

                    //Read the .mat file
                    Matrix<double> MATLAB_G = MatlabReader.Read<double>(path + @"\bsf_0p1_0p2_0p3_0p4_40db_SOS.mat", "G");
                    //Create a filter coefficient array
                    G = new double[MATLAB_G.RowCount];
                    //Get filter coefficient
                    var tmp_G = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)MATLAB_G).Values;
                    //Torture array
                    Array.Copy(tmp_G, G, MATLAB_G.RowCount);
                    _IIR.IsSOS = true;
                    _IIR.SetCoefficients(SOS, G);
                }
                else
                {
                    //Clear coefficient matrix
                    numerator = null;
                    demoniator = null;
                    k = null;
                    //Read the data in the .mat file corresponding to numerator
                    Matrix<double> MATLAB_numerator = MatlabReader.Read<double>(path + @"\bsf_0p1_0p2_0p3_0p4_40db.mat", "numerator");
                    //Create a filter coefficient array
                    numerator = new double[MATLAB_numerator.ColumnCount];
                    //Get filter coefficient
                    var tmp_numerator = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)MATLAB_numerator).Values;
                    //Torture array
                    Array.Copy(tmp_numerator, numerator, MATLAB_numerator.ColumnCount);

                    //Read the data in the .mat file demoniator
                    Matrix<double> MATLAB_demoniator = MatlabReader.Read<double>(path + @"\bsf_0p1_0p2_0p3_0p4_40db.mat", "demoniator");
                    //Create a filter coefficient array
                    demoniator = new double[MATLAB_demoniator.ColumnCount];
                    //Get filter coefficient
                    var tmp_demoniator = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)MATLAB_demoniator).Values;
                    //Torture array
                    Array.Copy(tmp_demoniator, demoniator, MATLAB_demoniator.ColumnCount);

                    //Read the data corresponding to k in the .mat file
                    Matrix<double> MATLAB_k = MatlabReader.Read<double>(path + @"\bsf_0p1_0p2_0p3_0p4_40db.mat", "k");
                    //Create a filter coefficient array
                    k = new double[MATLAB_k.ColumnCount];
                    //Get filter coefficient
                    var tmp_k = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)MATLAB_k).Values;
                    //Torture array
                    Array.Copy(tmp_k, k, MATLAB_k.ColumnCount);
                    _IIR.IsSOS = false;
                    //Set filter coefficient
                    _IIR.SetCoefficients(numerator, demoniator);
                }
                //Filter parameter
                labelFilterNum1.Text = "fpass1：";
                labelFilterNum1Value.Text = "0.1";
                labelFilterNum2.Text = "fstop1：";
                labelFilterNum2Value.Text = "0.2";
                labelFilterNum3.Text = "fstop2：";
                labelFilterNum3Value.Text = "0.3";
                labelFilterNum4.Text = "fpass2：";
                labelFilterNum4Value.Text = "0.4";
                //Not hidden
                labelFilterNum3.Visible = true;
                labelFilterNum3Value.Visible = true;
                labelFilterNum4.Visible = true;
                labelFilterNum4Value.Visible = true;
            }
        }

        /// <summary>
        /// checkBoxIsSOS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxIsSOS_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButtonLPF.Checked)
            {
                radioButtonLPF_CheckedChanged(sender, e);
            }
            else if (radioButtonHPF.Checked)
            {
                radioButtonHPF_CheckedChanged(sender, e);
            }
            else if (radioButtonBPF.Checked)
            {
                radioButtonBPF_CheckedChanged(sender, e);
            }
            else if (radioButtonBSF.Checked)
            {
                radioButtonBSF_CheckedChanged(sender, e);
            }

        }
        #endregion

        #region Methods
        #endregion
    }
}
