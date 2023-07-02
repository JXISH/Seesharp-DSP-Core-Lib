using SeeSharpTools.JY.GUI;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using SeeSharpTools.JXI.Numerics;
using SeeSharpTools.JXI.SignalProcessing.Transform;
using SeeSharpTools.JXI.Mathematics.LinearAlgebra.Matrix;
using SeeSharpTools.JXI.SignalProcessing.Window;
using SeeSharpTools.JXI.SignalProcessing.GeneralSpectrum;
using System.Globalization;
using System.Security.Policy;
using SeeSharpTools.JXI.SignalProcessing.JTFA;
using System.ComponentModel.Design.Serialization;
using SeeSharpTools.JXI.SignalProcessing.Generation;
using System.Web.Compilation;
using System.Xml.Schema;

namespace GaborTransformExample
{
    public partial class FormGaborTransformExample : Form
    {
        GaborTransformer Transformer;
        int signalLength;
        double[] waveform;
        bool conjugateRemovedCoeff;
        double noiseSigma;
        public FormGaborTransformExample()
        {
            InitializeComponent();
        }
        
        // Load initial Gabor Transform parameters
        private void Form1_Load(object sender, EventArgs e)
        {
            signalLength = 4096;
            waveform = new double[signalLength];
            Transformer = new GaborTransformer();
            Transformer.WindowType = WindowType.Hanning;
            Transformer.WindowLength = 1024;
            Transformer.FrequencyBins = 1024;
            Transformer.TimeStep = 64;
            for (int i = 0; i < signalLength; i++)
            {
                waveform[i] = Math.Sin(Math.Pow((i) / (double)100, 2));
            }
        }

        // Preform Gabor Transform and Expansion
        private void easyButton1_Click(object sender, EventArgs e)
        {
            // generate noise
            double[] noise = new double[signalLength];
            double[] _waveform = new double[signalLength];
            double mean = 0;
            conjugateRemovedCoeff = (!radioButtonFullSpectrum.Checked) || (radioButtonHalfSpectrum.Checked);
            noiseSigma = (double)numericUpDownNoiseRMS.Value;
            NoiseGenerator.GaussNiose(noise, noiseSigma, mean);

            // add noise to waveform
            for (int i = 0; i < signalLength; i++)
            {
                _waveform[i] = waveform[i] + noise[i];
            }
            easyChartXInputWaveform.Plot(_waveform);

            // Gabor Transform
            int M = signalLength / Transformer.TimeStep + Transformer.WindowLength / Transformer.TimeStep + 1;
            Complex[][] Coeff = new Complex[M][];
            Transformer.Padding = GaborTransformer.PaddingType.zeros;
            Transformer.GetGaborTransform(_waveform, Coeff, conjugateRemovedCoeff);
            double[,] powerSpectrum = GaborTransformer.ConvertToMagnitudeSpectrum(Coeff);
            WaterfallPlot(easyChartXInputTFWaterfall, powerSpectrum, Transformer.TimeStep, 0.0005);
            // Noise elimiation through mask
            SpectrogramMask(ref Coeff);

            //Gabor Expansion
            double[,] powerSpectrumFiltered = GaborTransformer.ConvertToMagnitudeSpectrum(Coeff);
            WaterfallPlot(easyChartXFilteredTFWaterfall, powerSpectrumFiltered, Transformer.TimeStep, 0.0005);
            double[] waveformOut = new double[signalLength];
            Transformer.GetGaborExpansion(Coeff, waveformOut, conjugateRemovedCoeff);
            easyChartXFilteredWaveform.Plot(waveformOut);
        }

        // Filter Noise Through a Custom subtraction based algorithm
        void SpectrogramMask(ref Complex[][] Coeff)
        {
            double magMax = getMaxMagitude(Coeff);
            for (int i = 0; i < Coeff.Length - 1; i++)
            {
                getFilteredSlice(ref Coeff[i], Coeff[i + 1], magMax);
            }
        }

        // Filter noise in one time slice
        void getFilteredSlice(ref Complex[] CoeffSlice, Complex[] NextCoeffSlice, double magMax)
        {
            Complex[] _tmpCoeff = new Complex[CoeffSlice.Length];
            double[] _PwrCoeff = Vector.GetComplexPower(CoeffSlice);
            double[] _PwrNextCoeff = Vector.GetComplexPower(NextCoeffSlice);
            Array.Copy(CoeffSlice, _tmpCoeff, CoeffSlice.Length);
            double threshold = 0.000015;
            List<int> indices = new List<int>();
            for (int i = 0;i < CoeffSlice.Length; i++)
            {
                if (_PwrCoeff[i] - _PwrNextCoeff[i] < threshold && (_PwrCoeff[i] - _PwrNextCoeff[i] > -threshold))
                {
                    CoeffSlice[i] = 0;
                }
                else
                {
                    indices.Add(i);
                }
            }
            foreach (int i in indices)
            {
                for (int j = Math.Max(0, i - 5); j < Math.Min(CoeffSlice.Length, i + 6); j++)
                {
                    CoeffSlice[j] = _tmpCoeff[j];
                }
            }
        }

        // Auxiliary function
        double getMaxMagitude(Complex[][] Coeff)
        {
            double res = 0;
            for (int i = 0; i < Coeff.Length; i++)
            {
                for (int j = 0; j < Coeff[i].Length; j++)
                {
                    if (Coeff[i][j].Magnitude > res)
                    {
                        res = Coeff[i][j].Magnitude;
                    }
                }
            }
            return res;
        }

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

    }
}
