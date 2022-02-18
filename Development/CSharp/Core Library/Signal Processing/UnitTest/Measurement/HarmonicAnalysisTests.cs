using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeeSharpTools.JY.ArrayUtility;
using SeeSharpTools.JY.DSP.Fundamental;
using System;
using SeeSharpTools.JX.DataViewer;
using SeeSharpTools.JY.File;
using System.Linq;
using System.Numerics;
using SeeSharpTools.JXI.SignalProcessing.Measurement;

namespace SeeSharpTools.JXI.SignalProcessing.Measurement.Tests
{
    [TestClass()]
    public class HarmonicAnalysisTests
    {
        [TestMethod()]
        public void THDAnalysisTest_001_Sine()
        {
            /*************************************
             * THD分析
             * 基波频率为500
             * ************************************/
            var samplingRate = 100000;
            var signalFrequency = 500;
            var noiseLevel = 0.2;
            var noise = new double[4000];
            var signal = new double[4000];
            JY.DSP.Fundamental.Generation.SineWave(ref signal, 1, 50, (double)signalFrequency, (double)samplingRate);
            ArrayCalculation.AddOffset(ref signal, 2);
            var dt = 1 / (double)samplingRate;
            JY.DSP.Fundamental.Generation.UniformWhiteNoise(ref noise, noiseLevel);
            ArrayCalculation.Add(signal, noise, ref signal);
            AnalogWaveformChart.Plot("raw signal", signal);
            double fundamentalFreq;
            double THD;
            double[] componentsLevel = new double[0];
            HarmonicAnalysis.THDAnalysis (signal, dt, out fundamentalFreq, out THD, ref componentsLevel);
            Console.WriteLine("fundamentalFrequency={0}", fundamentalFreq);
            Console.WriteLine("THD={0}", THD);
            AnalogWaveformChart.Plot("Analysis", componentsLevel);
        }

        [TestMethod()]
        public void THDAnalysisTest_002()
        {
            /*************************************
             * THD分析
             * 基波频率为60
             * ************************************/
            var samplingRate = 1000;
            var signalFrequency = 60;
            var noiseLevel = 0.2;
            var noise = new double[1000];
            var signal = new double[1000];
            JY.DSP.Fundamental.Generation.SineWave(ref signal, 1, 50, (double)signalFrequency, (double)samplingRate);
            ArrayCalculation.AddOffset(ref signal, 2);
            var dt = 1 / (double)samplingRate;
            JY.DSP.Fundamental.Generation.UniformWhiteNoise(ref noise, noiseLevel);
            ArrayCalculation.Add(signal, noise, ref signal);
            AnalogWaveformChart.Plot("raw signal", signal);
            double fundamentalFreq;
            double THD;
            double[] componentsLevel = new double[0];
            HarmonicAnalysis.THDAnalysis(signal, dt, out fundamentalFreq, out THD, ref componentsLevel);
            Console.WriteLine("fundamentalFrequency={0}", fundamentalFreq);
            Console.WriteLine("THD={0}", THD);
            AnalogWaveformChart.Plot("Analysis", componentsLevel);
        }

        [TestMethod()]
        public void THDAnalysisTest_003_NoisySquareFile()
        {
            /*************************************
             * SINAD分析
             * 读取csv文件分析 有噪声方波谐波分量
             * ************************************/

            //load waveform
            double[] signal = ReadTestCSV(@"Square_Noise0.001.csv");
            double dt = 1 / (double)51200;
            //THD analysis
            double fundamentalFreq;
            double[] componentsLevel = new double[0];
            double THD;
            HarmonicAnalysis.THDAnalysis(signal, dt, out fundamentalFreq, out THD, ref componentsLevel, 100);

            //显示结果
            int compareOrder = 8;
            double[] expectedComponents = new double[8] { 0.000002, 1.273288, 0.000012, 0.424502 , 0.000018 , 0.254806 , 0.000026 , 0.182114 };
            string harmonicsResult = "Components (amplitude): ";
            string expectedHarmonicsResult = "Expected: ";
            double[] error = new double[compareOrder];
            for (int i = 0; i < compareOrder; i++)
            {
                harmonicsResult += componentsLevel[i].ToString("N6") + "    ";
                expectedHarmonicsResult += expectedComponents[i].ToString("N6") + "    ";
                error[i] = Math.Abs((componentsLevel[i] - expectedComponents[i]) / expectedComponents[1]);
            }
            //分析通过与否
            double result = error.Max();
            //report of the test
            Console.WriteLine("fundamentalFrequency=" + fundamentalFreq.ToString("N3"));
            Console.WriteLine("THD = " + THD.ToString());
            Console.WriteLine(harmonicsResult);
            Console.WriteLine(expectedHarmonicsResult);
            Console.WriteLine("Max Error = " + result.ToString("N6"));
            bool pass = result < 0.7e-4;
            Assert.IsTrue(pass);
        }

        [TestMethod()]
        public void THDAnalysisTest_004_NoisySineFile()
        {
            /*************************************
             * SINAD分析
             * 读取csv文件分析 有噪声方波谐波分量
             * ************************************/

            //load waveform
            double[] signal = ReadTestCSV(@"Sine_Noise0.001.csv");
            double dt = 1 / (double)51200;
            double offset = 2;
            for (int i = 0; i < signal.Length; i++)
            {
                signal[i] += offset;
            }
            //THD analysis
            double fundamentalFreq;
            double[] componentsLevel = new double[0];
            double THD;
            HarmonicAnalysis.THDAnalysis(signal, dt, out fundamentalFreq, out THD, ref componentsLevel);

            //显示结果
            int compareOrder = 8;
            double expectedDC = offset;
            string harmonicsResult = "Components (amplitude): ";
            double error = Math.Abs((componentsLevel[0] - expectedDC) / expectedDC);
            for (int i = 0; i < compareOrder; i++)
            {
                harmonicsResult += componentsLevel[i].ToString("N6") + "    ";
            }
            //分析通过与否
            double result = error;
            //report of the test
            Console.WriteLine("fundamentalFrequency=" + fundamentalFreq.ToString("N3"));
            Console.WriteLine("THD = " + THD.ToString());
            Console.WriteLine(harmonicsResult);
            Console.WriteLine("DC Error = " + result.ToString("N6"));
            bool pass = result < 0.7e-4;
            Assert.IsTrue(pass);
        }

        [TestMethod()]
        public void ToneAnalysisTest_001()
        {
            /*************************************
             * Tone分析 计算信号基波的幅度、相位、频率
             * 基波频率为50
             * ************************************/
            var samplingRate = 1000;
            var signalFrequency = 20;
            var noiseLevel = 0.2;
            var noise = new double[1000];
            var signal = new double[1000];
            JY.DSP.Fundamental.Generation.SineWave(ref signal, 4, 50, (double)signalFrequency, (double)samplingRate);
            ArrayCalculation.AddOffset(ref signal, 2);
            var dt = 1 / (double)samplingRate;
            JY.DSP.Fundamental.Generation.UniformWhiteNoise(ref noise, noiseLevel);
            ArrayCalculation.Add(signal, noise, ref signal);
            AnalogWaveformChart.Plot("raw signal", signal);
            double fundamentalFreq;
            double phase;
            double amplitude;
            HarmonicAnalysis.ToneAnalysis(signal, dt, out fundamentalFreq,out amplitude,out phase);
            Console.WriteLine("fundamentalFrequency={0}", fundamentalFreq);
            Console.WriteLine("amplitude={0}", amplitude);
            Console.WriteLine("phase={0}", phase);
        }

        [TestMethod()]
        public void ToneAnalysisTest_Complex_001()
        {
            /*************************************
             * Tone分析 计算信号基波的幅度、相位、频率
             * 基波频率为50
             * ************************************/
            var samplingRate = 1000;
            var signalFrequency = 20;
            var noiseLevel = 0.2;
            int length = 1000;
            var noise = new double[length];
            var signalI = new double[length];
            var signalQ = new double[length];
            double phaseInnitail = 10;
            double amp = 4;
            double offset = 8;
            //cos
            JY.DSP.Fundamental.Generation.SineWave(ref signalI, amp, 90+ phaseInnitail, (double)signalFrequency, (double)samplingRate);
            //sin
            JY.DSP.Fundamental.Generation.SineWave(ref signalQ, amp, 0 + phaseInnitail, (double)signalFrequency, (double)samplingRate);
            var dt = 1 / (double)samplingRate;
            JY.DSP.Fundamental.Generation.UniformWhiteNoise(ref noise, noiseLevel);
            ArrayCalculation.Add(signalI, noise, ref signalI);
            ArrayCalculation.Add(signalQ, noise, ref signalQ);
            ArrayCalculation.AddOffset(ref signalI, offset);
            ArrayCalculation.AddOffset(ref signalQ, offset);
            //AnalogWaveformChart.Plot("signal I", signalI);
            //AnalogWaveformChart.Plot("signal Q", signalQ);

            Complex[] signal = new Complex[length];
            for (int i = 0; i < signal.Length; i++)
            {
                signal[i] = new Complex(signalI[i], signalQ[i]);
            }

            double fundamentalFreq;
            double phase;
            double amplitude;
            HarmonicAnalysis.ToneAnalysis(signal, dt, out fundamentalFreq, out amplitude, out phase);
            phase = phase * 180 / Math.PI;
            Console.WriteLine("fundamentalFrequency={0}", fundamentalFreq);
            Console.WriteLine("amplitude={0}", amplitude);
            Console.WriteLine("phase={0}", phase);
        }

        [TestMethod()]
        public void SINADAnalysisTest_002()
        {
            /*************************************
             * SINAD分析
             * 基波频率为60
             * ************************************/
            var samplingRate = 1000;
            var signalFrequency = 60;
            var noiseLevel = 0.2;
            var noise = new double[1000];
            var signal = new double[1000];
            JY.DSP.Fundamental.Generation.SineWave(ref signal, 1, 50, (double)signalFrequency, (double)samplingRate);
            ArrayCalculation.AddOffset(ref signal, 2);
            var dt = 1 / (double)samplingRate;
            JY.DSP.Fundamental.Generation.UniformWhiteNoise(ref noise, noiseLevel);
            ArrayCalculation.Add(signal, noise, ref signal);
            AnalogWaveformChart.Plot("raw signal", signal);
            double fundamentalFreq;
            double SINAD;
            double[] componentsLevel = new double[0];
            HarmonicAnalysis.SINADAnalysis( signal, dt, out fundamentalFreq, out SINAD, ref componentsLevel);
            Console.WriteLine("fundamentalFrequency={0}", fundamentalFreq);
            Console.WriteLine("SINAD={0}", SINAD);
            AnalogWaveformChart.Plot("Analysis", componentsLevel);
        }

        [TestMethod()]
        public void SINADAnalysisTest_003_NoisySineFile()
        {
            /*************************************
             * SINAD分析
             * 读取csv文件分析
             * ************************************/

            //load waveform
            double[] signal = ReadTestCSV(@"Sine_Noise0.001.csv");
            double dt = 1 / (double)51200;
            //SINAD analysis
            double fundamentalFreq;
            double THD;
            double[] componentsLevel = new double[0];
            //HarmonicAnalysis.THDAnalysis(signal, dt, out fundamentalFreq, out THD, ref componentsLevel);

            double SINAD;
            HarmonicAnalysis.SINADAnalysis(signal, dt, out fundamentalFreq, out SINAD, ref componentsLevel);

            //分析通过与否
            double result = 10*Math.Log10(SINAD);
            double expectedResult = 61.695;
            //report of the test
            Console.WriteLine("fundamentalFrequency=");
            Console.WriteLine("SINAD (dB) = "+ result.ToString("N3") + "expected:" + expectedResult.ToString("N3"));
            Console.WriteLine("error (dB) = " + (result - expectedResult).ToString("N3"));
            bool pass = Math.Abs((result - expectedResult)/expectedResult) < 3e-3;
            Assert.IsTrue(pass);
        }

        [TestMethod()]
        public void SINADAnalysisTest_004_NoisySquareFile()
        {
            /*************************************
             * SINAD分析
             * 读取csv文件分析
             * ************************************/

            //load waveform
            double[] signal = ReadTestCSV(@"Square_Noise0.001.csv");
            double dt = 1 / (double)51200;
            //SINAD analysis
            double fundamentalFreq;
            double[] componentsLevel = new double[0];
            double SINAD;
            HarmonicAnalysis.SINADAnalysis(signal, dt, out fundamentalFreq, out SINAD, ref componentsLevel);

            //分析通过与否
            double result = 10 * Math.Log10(SINAD);
            double expectedResult = 7.227;
            //report of the test
            Console.WriteLine("fundamentalFrequency=");
            Console.WriteLine("SINAD (dB) = " + result.ToString("N3") + "expected:" + expectedResult.ToString("N3"));
            Console.WriteLine("error (dB) = " + (result - expectedResult).ToString("N3"));
            bool pass = Math.Abs((result - expectedResult) / expectedResult) < 3e-3;
            Assert.IsTrue(pass);
        }

        private double[] ReadTestCSV(string fileName)
        {
            string basefolder = this.GetType().Assembly.Location;
            basefolder = basefolder.Substring(0, basefolder.LastIndexOf(@"\"));
            basefolder = basefolder.Substring(0, basefolder.LastIndexOf(@"\"));
            basefolder = basefolder.Substring(0, basefolder.LastIndexOf(@"\"));
            string filepath = basefolder + @"\" + fileName;
            double[,] rawData = CsvHandler.Read<double>(filepath);
            int size = rawData.GetLength(0);
            double[] signal = new double[size];
            for (int i = 0; i < size; i++)
            {
                signal[i] = rawData[i, 0];
            }
            return (signal);
        }
    }
}