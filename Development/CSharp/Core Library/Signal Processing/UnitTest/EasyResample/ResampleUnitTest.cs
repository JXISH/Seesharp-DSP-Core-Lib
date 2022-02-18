using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeeSharpTools.JX.DataViewer;
using SeeSharpTools.JY.ArrayUtility;
using SeeSharpTools.JXI.SignalProcessing.Conditioning.EasyResample;
using System.Numerics;

namespace SeeSharpTools.JX.SignalProcessing.EasyResampleTest
{
    [TestClass]
    public class ResampleUnitTest
    {
        [TestMethod]
        public void Resample_Test_001()
        {
            /*****************************
             * 恒定采样率
             * 两个差0.5个点的信号
             * dt=2 
             * ***************************/
            double[] sinWave1 = new double[1000];
            double[] sinWave2 = new double[1000];
            JY.DSP.Fundamental.Generation.SineWave(ref sinWave1, 1, 0, 10, 1000);
            JY.DSP.Fundamental.Generation.SineWave(ref sinWave2, 1, 360.0 / 200, 10, 1000);
            double[,] rawWaveform = new double[1000, 2];
            ArrayManipulation.Connected_2D_Array(sinWave1, sinWave2, ref rawWaveform);
            double[,] rawWaveformT = new double[rawWaveform.GetLength(1), rawWaveform.GetLength(0)];
            ArrayManipulation.Transpose(rawWaveform, ref rawWaveformT);
            var syncWaveform = EasyResample.ResampleWaveform(sinWave1, 0, 2);
            var syncWaveform2 = EasyResample.ResampleWaveform(sinWave2, 0.5, 2);
            var syncWaveform3 = new double[syncWaveform.Length,2];
            var syncWaveform3T = new double[2,syncWaveform.Length];
            ArrayManipulation.Connected_2D_Array(syncWaveform, syncWaveform2, ref syncWaveform3);
            ArrayManipulation.Transpose(syncWaveform3, ref syncWaveform3T);
            AnalogWaveformChart.Plot("RawWaveform", rawWaveformT);
            AnalogWaveformChart.Plot("SyncWaveform", syncWaveform3T);
        }

        [TestMethod]
        public void Resample_Test_002()
        {
            /*****************************
             * 恒定采样率
             * 两个差0.5个点的信号
             * dt=1
             * ***************************/
            double[] sinWave1 = new double[1000];
            double[] sinWave2 = new double[1000];
            JY.DSP.Fundamental.Generation.SineWave(ref sinWave1, 1, 0, 10, 1000);
            JY.DSP.Fundamental.Generation.SineWave(ref sinWave2, 1, 360.0 / 200, 10, 1000);
            double[,] rawWaveform = new double[1000, 2];
            ArrayManipulation.Connected_2D_Array(sinWave1, sinWave2, ref rawWaveform);
            double[,] rawWaveformT = new double[rawWaveform.GetLength(1), rawWaveform.GetLength(0)];
            ArrayManipulation.Transpose(rawWaveform, ref rawWaveformT);
            var syncWaveform = EasyResample.ResampleWaveform(sinWave1, 0,1);
            var syncWaveform2 = EasyResample.ResampleWaveform(sinWave2, 0.5,1);
            var syncWaveform3 = new double[syncWaveform.Length, 2];
            var syncWaveform3T = new double[2, syncWaveform.Length];
            ArrayManipulation.Connected_2D_Array(syncWaveform, syncWaveform2, ref syncWaveform3);
            ArrayManipulation.Transpose(syncWaveform3, ref syncWaveform3T);
            AnalogWaveformChart.Plot("RawWaveform", rawWaveformT);
            AnalogWaveformChart.Plot("SyncWaveform", syncWaveform3T);

        }

        [TestMethod]
        public void Resample_Test_003()
        {
            /*****************************
             * 恒定采样率
             * dt=2
             * ***************************/
            double[] sinWave1 = new double[1000];
            double[] sinWave2 = new double[1000];
            JY.DSP.Fundamental.Generation.SineWave(ref sinWave1, 1, 0, 10, 1000);
            var syncWaveform = EasyResample.ResampleWaveform(sinWave1, 0, 1);
            var syncWaveform2 = EasyResample.ResampleWaveform(sinWave1, 0, 2);
            AnalogWaveformChart.Plot("syncWaveform", syncWaveform);
            AnalogWaveformChart.Plot("syncWaveform2", syncWaveform2);
        }

        [TestMethod]
        public void Resample_Test_004()
        {
            /*****************************
             * 改变采样率
             * dt=0.7
             * delay=0.5;
             * ***************************/
            double[] sinWave1 = new double[1000];
            double[] sinWave2 = new double[1000];
            JY.DSP.Fundamental.Generation.SineWave(ref sinWave1, 1, 0, 1, 100);
            var syncWaveform2 = EasyResample.ResampleWaveform(sinWave1, 0.5,0.7);
            AnalogWaveformChart.Plot("syncWaveform2", syncWaveform2);
        }

        [TestMethod]
        public void Resample_Test_005()
        {
            /*****************************
             * 改变采样率
             * dt=0.3
             * delay=0;
             * ***************************/
            double[] sinWave1 = new double[400];
            double[] sinWave2 = new double[400];
            JY.DSP.Fundamental.Generation.SineWave(ref sinWave1, 1, 0, 10, 1000);
            var syncWaveform2 = EasyResample.ResampleWaveform(sinWave1, 0, 0.3);
            AnalogWaveformChart.Plot("syncWaveform2", syncWaveform2);
        }

        [TestMethod]
        public void Resample_Test_006()
        {
            /*****************************
             * 改变采样率
             * dt=0.3
             * delay=0;
             * ***************************/
            double delay = 0;
            double dt = 0.1;
            double[] sinWave1 = new double[1000];
            double[] sinWave2 = new double[(int)((sinWave1.Length -2*85)/dt)];
            double sampleRate = 1000;
            double freq = 10;
            JY.DSP.Fundamental.Generation.SineWave(ref sinWave1, 1, 0, freq, sampleRate);
            JY.DSP.Fundamental.Generation.SineWave(ref sinWave2, 1, (85+ delay)* 360*freq/sampleRate, freq, sampleRate/dt);
            var syncWaveform2 = EasyResample.ResampleWaveform(sinWave1, delay, dt);
            double[,] rawWaveform = new double[sinWave2.Length , 2];
            ArrayManipulation.Connected_2D_Array(syncWaveform2, sinWave2, ref rawWaveform);
            double[,] rawWaveformT = new double[rawWaveform.GetLength(1), rawWaveform.GetLength(0)];
            ArrayManipulation.Transpose(rawWaveform, ref rawWaveformT);
            AnalogWaveformChart.Plot("syncWaveform2", rawWaveformT);
            double error = 0;
            for (int i = 0; i < sinWave2.Length ; i++)
            {
                error += (sinWave2[i] - syncWaveform2[i]);
                var error1 = (sinWave2[i] - syncWaveform2[i]);
                var a = sinWave2[i];
                var b = syncWaveform2[i];
                Assert.IsTrue(Math.Abs(error1 / a) < 1E-6 || Math.Abs(error1) < 1E-6);
            }
            var errorAverage = 10*Math.Log10( error / sinWave2.Length);
        }

        [TestMethod]
        public void Resample_Test_007()
        {
            /*****************************
             * 改变采样率
             * dt=2.5
             * delay=0;
             * ***************************/
            double dt = 2.5;
            double[] sinWave1 = new double[1000];
            double[] sinWave2 = new double[(int)((sinWave1.Length - 2 * 85) / dt)];
            double sampleRate = 1000;
            double freq = 10;
            JY.DSP.Fundamental.Generation.SineWave(ref sinWave1, 1, 0, freq, sampleRate);
            JY.DSP.Fundamental.Generation.SineWave(ref sinWave2, 1, 85 * 360 * freq / sampleRate, freq, sampleRate / dt);
            var syncWaveform2 = EasyResample.ResampleWaveform(sinWave1, 0, dt);
            double[,] rawWaveform = new double[sinWave2.Length, 2];
            ArrayManipulation.Connected_2D_Array(syncWaveform2, sinWave2, ref rawWaveform);
            double[,] rawWaveformT = new double[rawWaveform.GetLength(1), rawWaveform.GetLength(0)];
            ArrayManipulation.Transpose(rawWaveform, ref rawWaveformT);
            //AnalogWaveformChart.Plot("syncWaveform2", rawWaveformT);
            double error = 0;
            for (int i = 0; i < sinWave2.Length; i++)
            {
                error += (sinWave2[i] - syncWaveform2[i]);
                Assert.IsTrue(Math.Abs((sinWave2[i] - syncWaveform2[i]) / sinWave2[i]) < 1E-6 ||
                                        Math.Abs((sinWave2[i] - syncWaveform2[i]))<1E-6);

            }
            var errorAverage = 20 * Math.Log10(error / sinWave2.Length);
        }

        [TestMethod]
        public void Resample_Test_008()
        {
            /*****************************
             * 双频信号 新采样率不满足高频分量的奈奎斯特采样定理，没有滤除高频分量
             * 改变采样率
             * dt=100
             * delay=0;
             * ***************************/
            double dt = 100;
            double[] sinWave1 = new double[10000];
            double[] sinWave2 = new double[10000];
            double[] sinWave3 = new double[(int)((sinWave1.Length - 2 * 85) / dt)];
            double[] sinWave4 = new double[(int)((sinWave1.Length - 2 * 85) / dt)];
            double sampleRate = 1000;
            double freq = 1;
            double freqHigh = 8;
            JY.DSP.Fundamental.Generation.SineWave(ref sinWave1, 1, 0, freq, sampleRate);
            JY.DSP.Fundamental.Generation.SineWave(ref sinWave2, 1, 0, freqHigh, sampleRate);
            JY.DSP.Fundamental.Generation.SineWave(ref sinWave3, 1, 85 * 360 * freq / sampleRate, freq, sampleRate / dt);
            JY.DSP.Fundamental.Generation.SineWave(ref sinWave4, 1, 85 * 360 * freqHigh / sampleRate, freqHigh, sampleRate / dt);
            ArrayCalculation.Add(sinWave1, sinWave2, ref sinWave1);
            ArrayCalculation.Add(sinWave3, sinWave4, ref sinWave3);

            var rasampleWaveform2 = EasyResample.ResampleWaveform(sinWave1, 0, dt);
            double[,] rawWaveform = new double[sinWave3.Length, 2];
            ArrayManipulation.Connected_2D_Array(rasampleWaveform2, sinWave3, ref rawWaveform);
            double[,] rawWaveformT = new double[rawWaveform.GetLength(1), rawWaveform.GetLength(0)];
            ArrayManipulation.Transpose(rawWaveform, ref rawWaveformT);
            AnalogWaveformChart.Plot("syncWaveform2", rasampleWaveform2);
            AnalogWaveformChart.Plot("sinWave3", sinWave3);
            AnalogWaveformChart.Plot("sinWave1", sinWave1);
            AnalogWaveformChart.Plot("rawWaveformT", rawWaveformT);
            double error = 0;
            double errorA = 0;
            for (int i = 0; i < sinWave3.Length; i++)
            {
                error = (sinWave3[i] - rasampleWaveform2[i]);
                var a = sinWave3[i];
                var b = rasampleWaveform2[i];
                Assert.IsTrue(Math.Abs(error / a) < 1E-6 || Math.Abs(error)<1E-6);
                errorA += (sinWave3[i] - rasampleWaveform2[i]); ;
            }
            var errorAverage = 20 * Math.Log10(errorA / sinWave2.Length);
        }

        [TestMethod]
        public void Resample_Test_009()
        {
            /*****************************
             * 改变采样率
             * dt=0.3
             * delay=0;
             * ***************************/
            double delay = 0;
            double dt = 0.1;
            int length = 1000;
            double[] sinWave1 = new double[length];
            double[] sinWave2 = new double[(int)((sinWave1.Length - 2 * 85) / dt)];
            double[] cosWave1 = new double[length];
            double[] cosWave2 = new double[(int)((sinWave1.Length - 2 * 85) / dt)];
            Complex [] complex1= new Complex[length];

            double sampleRate = 1000;
            double freq = 10;
            JY.DSP.Fundamental.Generation.SineWave(ref sinWave1, 1, 0, freq, sampleRate);
            JY.DSP.Fundamental.Generation.SineWave(ref cosWave1, 1, 90, freq, sampleRate);
            JY.DSP.Fundamental.Generation.SineWave(ref sinWave2, 1, (85 + delay) * 360 * freq / sampleRate, freq, sampleRate / dt);
            JY.DSP.Fundamental.Generation.SineWave(ref cosWave2, 1, 90+(85 + delay) * 360 * freq / sampleRate, freq, sampleRate / dt);

            for (int i = 0; i < length; i++)
            {
                complex1[i] = new Complex(sinWave1[i], cosWave1[i]);
            }

            var reampleWaveform = EasyResample.ResampleWaveform(complex1, delay, dt);
            double errorReal = 0;
            double errorIm = 0;
            for (int i = 0; i < sinWave2.Length; i++)
            {
                errorReal += (sinWave2[i] - reampleWaveform[i].Real);
                errorIm += (cosWave2[i] - reampleWaveform[i].Imaginary);

                var error1 = (sinWave2[i] - reampleWaveform[i].Real);
                var a1 = sinWave2[i];
                Assert.IsTrue(Math.Abs(error1 / a1) < 1E-6 || Math.Abs(error1) < 1E-6);

                var error2 = (cosWave2[i] - reampleWaveform[i].Imaginary );
                var a2 = cosWave2[i];
                Assert.IsTrue(Math.Abs(error2 / a2) < 1E-6 || Math.Abs(error2) < 1E-6);
            }
            var errorAverageReal = 10 * Math.Log10(errorReal / sinWave2.Length);
            Console.Write("errorAverageReal:{0}", errorAverageReal);
            var errorAverageIm = 10 * Math.Log10(errorIm / cosWave2.Length);
            Console.Write("errorAverageReal:{0}", errorAverageIm);
        }
    }
}
