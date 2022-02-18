using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeeSharpTools.JXI.SignalProcessing.Conditioning.EasyFilters;
using SeeSharpTools.JX.DataViewer;
using SeeSharpTools.JY.ArrayUtility;
using SeeSharpTools.JY.DSP.Fundamental;
using MathNet.Numerics.Data.Matlab;
using MathNet.Numerics.LinearAlgebra;



namespace SeeSharpTools.JX.SignalProcessing.ConditioningTest
{
    [TestClass]
    public class IIRFilterTest
    {
        [TestMethod]
        public void IIR_LPF_Test_001()
        {
            /*************************
             * LPF
             * passFreq=50Hz
             * stopFreq=100Hz
             * 带外衰减40db
             * 采样频率1000Hz
             * *************************/
            double[] sinWave1 = new double[1000];
            double[] sinWave2 = new double[1000];
            Generation.SineWave(ref sinWave1, 1, 0, 10, 1000);
            Generation.SineWave(ref sinWave2, 1, 0, 120, 1000);
            ArrayCalculation.Add(sinWave1, sinWave2, ref sinWave1);
            sinWave1[0] = 0.5;
            double[] sinWaveIir = new double[1000];
            double[] numerator = new double[] { 1, 8, 28, 56, 70, 56, 28, 8, 1 };
            double[] demoniatorr = new double[] { 1,- 6.14833128577259 ,  16.7106178322332, - 26.1940863217919  , 25.8773677909964 ,- 16.4863518823100   ,6.61072693847043, - 1.52457047404531 ,  0.154752511303108 };
            double k = 4.887073568907229e-07;
            var iir = new IIRFilter();
            iir.SetCoefficients(numerator, demoniatorr);
            iir.Filter(sinWave1, ref sinWaveIir);
            ArrayCalculation.MultiplyScale(ref sinWaveIir, k);
            AnalogWaveformChart.Plot("IIR_LPF1", sinWaveIir);
            AnalogWaveformChart.Plot("IIR_LPF2", sinWave1);
        }

        [TestMethod]
        public void IIR_LPF_Test_002()
        {
            /*************************
             * LPF
             * passFreq=50Hz
             * stopFreq=100Hz
             * 带外衰减40db
             * 采样频率1000Hz
             * SOS
             * *************************/
            double[] sinWave1 = new double[1000];
            double[] sinWave2 = new double[1000];
            Generation.SineWave(ref sinWave1, 1, 0, 10, 1000);
            Generation.SineWave(ref sinWave2, 1, 0, 350, 1000);
            ArrayCalculation.Add(sinWave1, sinWave2, ref sinWave1);
            string path = Environment.CurrentDirectory;
            path = path.Substring(0, path.LastIndexOf(@"\"));
            path = path.Substring(0, path.LastIndexOf(@"\"));
            path = path.Substring(0, path.LastIndexOf(@"\"));
            //清空系数矩阵
            double [,] SOS = null;
            double [] G = null;

            //读.mat文件
            Matrix<double> MATLAB_SOS = MatlabReader.Read<double>(path+ @"\SeeSharpTools.JXI.SignalProcessing\lpf_0p2_0p3_40db_SOS.mat", "SOS");
            //创建滤波器系数数组
            SOS = new double[MATLAB_SOS.RowCount,MATLAB_SOS.ColumnCount];
            //获取滤波器系数
            var tmp_SOS = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)MATLAB_SOS).Values;
            //拷问数组
            for (int i = 0; i < SOS.GetLength(0); i++)
            {
                for (int j = 0; j < SOS.GetLength(1); j++)
                {
                    SOS[i, j] = tmp_SOS[i + j * (SOS.GetLength(0))];
                }
            }

            //读.mat文件
            Matrix<double> MATLAB_G = MatlabReader.Read<double>(path + @"\SeeSharpTools.JXI.SignalProcessing\lpf_0p2_0p3_40db_SOS.mat", "G");
            //创建滤波器系数数组
            G = new double[ MATLAB_G.RowCount];
            //获取滤波器系数
            var tmp_G = ((MathNet.Numerics.LinearAlgebra.Double.DenseMatrix)MATLAB_G).Values;
            //拷问数组
            Array.Copy(tmp_G, G, MATLAB_G.RowCount);

            double[] sinWaveIir = new double[1000];
            var iir = new IIRFilter();
            iir.IsSOS = true;
            iir.SetCoefficients(SOS, G);
            iir.Filter(sinWave1, ref sinWaveIir);
            AnalogWaveformChart.Plot("IIR_After", sinWaveIir);
            AnalogWaveformChart.Plot("IIR_Before", sinWave1);
        }

        [TestMethod]
        public void IIR_LPF_Test_003()
        {
            /*************************
             
             * *************************/
            double[] sinWave1 = new double[1000];
            double[] sinWave2 = new double[1000];
            int freqLines = 500;
            double df = 1000 / 2 / freqLines;
            Generation.SquareWave (ref sinWave1,1,25,5);
            for (int i = 0; i <sinWave1.Length; i++)
            {
                if (sinWave1[i] < 0.5)
                {

                    sinWave1[i] = 1;
                }
                else
                {
                    sinWave1[i] = 0;
                }
            }
            Generation.UniformWhiteNoise (ref sinWave2,0.01);
            ArrayCalculation.Add(sinWave1, sinWave2, ref sinWave1);

            double[] sinWaveIir = new double[1000];
            double[] numerator = new double[] { +2.3236928574050486E-1, +2.3236928574050486E-1 };
            double[] demoniatorr = new double[] { +1.0000000000000000E+0, -5.3526142851899028E-1 };
            double k = +1.0000000000000000E+0;
            var iir = new IIRFilter();
            iir.SetCoefficients(numerator, demoniatorr);
            iir.Filter(sinWave1, ref sinWaveIir);
            ArrayCalculation.MultiplyScale(ref sinWaveIir, k);

            //double[] bodeMag = new double[freqLines];
            //int numOfAverage = (int)Math.Floor((decimal)(sinWave1.Length / 2 / freqLines));
            //AverageParam Average = new AverageParam();
            //Average.Mode = AverageMode.RMS;
            //Average.Number = numOfAverage;
            //bodeMag = FrequencyResponseFunction.GetMagenitude(sinWave1, sinWaveIir, true, Average);
            //AnalogWaveformChart.Plot("BodeMagnitude", 0, 1 / df, bodeMag);

            AnalogWaveformChart.Plot("IIR_After", sinWaveIir);
            AnalogWaveformChart.Plot("IIR_Before", sinWave1);
        }
    }
}
