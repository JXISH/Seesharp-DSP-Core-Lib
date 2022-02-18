/*******************************************
 * FrequencyResponseFunction 范例
 * 使用范例FIRFilter的Low pass filter
 * 利用Dual frequency signal（带Noise）以及通过Filter器的信号求得Filter器的频响
 * *****************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SeeSharpTools.JXI.SignalProcessing.Measurement;
using SeeSharpTools.JXI.SignalProcessing.Generation;
using SeeSharpTools.JXI.SignalProcessing.Conditioning.EasyFilters;
using SeeSharpTools.JY.ArrayUtility;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Data.Matlab;



namespace Winform_FrequencyResponseFunction
{
    public partial class MainForm : Form
    {
        #region Private Field
        /// <summary>
        /// FIRFilter task
        /// </summary>
        private FIRFilter _fir = new FIRFilter();
        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeComponent();
            double[] FilterCoe = new double[] { 0.00194150175769345, -0.00169793575520208, -0.00426956799325628, -0.00829350380976647, -0.0132303432212115, -0.0181544335557185, -0.0217585679131809, -0.0225134101549763, -0.0189478446411358, -0.00995128109977753, 0.00489205937905203, 0.0250974062726473, 0.0492006450550267, 0.0748863114260257, 0.0992878499995790, 0.119424371366942, 0.132695897659600, 0.137328720011526, 0.132695897659600, 0.119424371366942, 0.0992878499995790, 0.0748863114260257, 0.0492006450550267, 0.0250974062726473, 0.00489205937905203, -0.00995128109977753, -0.0189478446411358, -0.0225134101549763, -0.0217585679131809, -0.0181544335557185, -0.0132303432212115 - 0.00829350380976647, -0.00426956799325628, -0.00169793575520208, 0.00194150175769345 };
            _fir.Coefficients = FilterCoe;//Filter coefficient
        }
        #endregion

        #region Event Handler
        /// <summary>
        /// Start button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            int length = 1000;
            int freqLines = length/2;
            //Generating signal
            double[] noiseInput = new double[length];//Noise
            double sampleRate = 1000;//Sampling rate
            double Amp = 1;//Amplitude
            double[] noiseAddon = new double[length];
            double[] FIROutput = new double[length];       
            double[] bodeMag = new double[freqLines];
            double[] bodePhase = new double[freqLines];
            double[] coherent = new double[freqLines];

            int numOfAverage = 50;
            FrequencyResponseFunction FRFAnalysis = new FrequencyResponseFunction();
            FRFAnalysis.Average.Mode = AverageMode.RMS;
            FRFAnalysis.Average.Number = numOfAverage;
            FRFAnalysis.ResetAveraging = true;

            for (int i = 0; i < numOfAverage; i++)
            {
                Generation.UniformWhiteNoise(ref noiseAddon, Amp * 0.0001);//Generate noise
                Generation.UniformWhiteNoise(ref noiseInput, Amp * 0.01);//Generate noise
                _fir.Filter(noiseInput, ref FIROutput);//Filter
                ArrayCalculation.Add(FIROutput, noiseAddon, ref FIROutput);
                if (i == 0) FRFAnalysis.Reset();
                FRFAnalysis.Analyze(noiseInput, FIROutput);
                bodeMag = FRFAnalysis.GetMagenitude(true);
                bodePhase = FRFAnalysis.GetPhase(true);
                coherent = FRFAnalysis.GetCoherente();
            }
            bool averageDone = FRFAnalysis.AveragingDone;
            UnwrapPhase(ref bodePhase, bodePhase.Length );//截止频率（归一化）是0.3
            //Display
            easyChartMagenitude.Plot(bodeMag,0,1/ sampleRate);
            easyChartPhase.Plot(bodePhase,0, 1 / sampleRate);
            easyChartCoherent.Plot(coherent, 0, 1 / sampleRate);
        }

        #endregion

        #region Methods     

        /// <summary>
        /// <para>UnwrapPhase，phase is the input phase to unwrap, in radians.</para>
        /// </summary>
        /// <param name="phase">phase is the input phase to unwrap, in radians. </param>
        /// <param name="N">unwrapping length</param>
        public static void UnwrapPhase(ref double[] phase, int N)
        {
            int i = 0;
            for (i = 1; i < N; i++)
            {
                phase[i] = phase[i] - 2 * Math.PI * Math.Floor(((phase[i] - phase[i - 1]) / (2 * Math.PI)) + 0.5);//利用相位展开公式进行相位展开
            }
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
