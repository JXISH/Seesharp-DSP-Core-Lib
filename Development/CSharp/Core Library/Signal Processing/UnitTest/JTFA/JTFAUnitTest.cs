using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeeSharpTools.JXI.SignalProcessing.Generation;
using SeeSharpTools.JXI.SignalProcessing.GeneralSpectrum;
using SeeSharpTools.JY.ArrayUtility;
using System.Numerics;
using System.Drawing;
using SeeSharpTools.JXI.SignalProcessing.JTFA;
using SeeSharpTools.JXI.SignalProcessing.Window;

namespace SeeSharpTools.JXI.SignalProcessing.GeneralJTFATaskTest
{
    [TestClass]
    public class JTFAUnitTest
    {
        [TestMethod]
        public void JTFA_RealFFT_001()
        {
            /*******************************
             * 输入信号为实数
             * （输入信号的长度-FrequencyBins）/(FrequencyBins/4)不是整数
             * FrequencyBins不是4的整倍数
             * 存在遗留数据的情况
             * ******************************/
            int Length = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double freqSin = 10;
            Generation.Generation.SineWave(ref sin, 1, 0, freqSin, 1000);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.05);
            ArrayCalculation.Add(sin, noise, ref sin);
            GeneralJTFATask task = new GeneralJTFATask();
            task.FrequencyBins = 50;
            task.SampleRate = 1000;
            double[,] JTFASpectrum = null;
            task.GetJTFA(sin, ref JTFASpectrum);
            var df = task.JTFAInfomation.df;
            var dt = task.JTFAInfomation.dt;
            var f0 = task.JTFAInfomation.f0;
            var FFTSize = task.JTFAInfomation.FFTSize;
        }

        [TestMethod]
        public void JTFA_RealFFT_002()
        {
            /*******************************
             * 输入信号为实数
             * （输入信号的长度-FrequencyBins）/(FrequencyBins/4)不是整数
             * FrequencyBins是4的整倍数
             * 存在遗留数据的情况
             * ******************************/
            int Length = 900;
            double[] sin = new double[Length];
            double freqSin = 10;
            JY.DSP.Fundamental.Generation.SineWave(ref sin, 1, 0, freqSin, 1000);
            GeneralJTFATask task = new GeneralJTFATask();
            task.ColorTable = GeneralJTFATask.ColorTableType.Rainbow;
            task.WindowType = WindowType.None;
            task.FrequencyBins = 500;
            task.SampleRate = 1000;
            double[,] JTFASpectrum = null;
            task.GetJTFA(sin, ref JTFASpectrum);
            double df = task.JTFAInfomation.df;
        }

        [TestMethod]
        public void JTFA_RealFFT_003()
        {
            /*******************************
             * 输入信号为实数
             * （输入信号的长度-FrequencyBins）/(FrequencyBins/4)不是整数
             * 存在数据遗留的情况
             * FrequencyBins不是4的整倍数
             * ******************************/
            int Length = 100;
            double[] sin = new double[Length];
            double freqSin = 10;
            JY.DSP.Fundamental.Generation.SineWave(ref sin, 1, 0, freqSin, 100);
            GeneralJTFATask task = new GeneralJTFATask();
            task.FrequencyBins = 50;
            task.SampleRate = 100;
            double[,] JTFASpectrum = null;
            task.GetJTFA(sin, ref JTFASpectrum);
            Bitmap image = new Bitmap(750, 250);
            task.GetImage(JTFASpectrum, ref image);
        }

        [TestMethod]
        public void JTFA_RealFFT_004()
        {
            /*******************************
             * 输入信号为实数
             * （输入信号的长度-FrequencyBins）/(FrequencyBins/4)不是整数
             * FrequencyBins不是4的整倍数
             * 整段进行JTFA与分段进行JTFA进行对比
             * ******************************/
            int Length = 200;
            double[] sin = new double[Length];
            double freqSin = 10;
            JY.DSP.Fundamental.Generation.SineWave(ref sin, 1, 0, freqSin, 100);
            GeneralJTFATask task = new GeneralJTFATask();
            task.FrequencyBins = 50;
            task.SampleRate = 100;
            double[,] JTFASpectrum=null;
            task.GetJTFA(sin, ref JTFASpectrum);
            task.Reset();
            //分段JTFA
            double[] sin1 = new double[80];
            double[] sin2 = new double[Length- sin1.Length];
            Array.Copy(sin, 0, sin1, 0, sin1.Length);
            Array.Copy(sin, sin1.Length , sin2, 0, sin2.Length);
            double[,] JTFASpectrum1 = null;
            double[,] JTFASpectrum2 = null;
            task.GetJTFA(sin1, ref JTFASpectrum1);
            task.GetJTFA(sin2, ref JTFASpectrum2);
            task.Reset();
            double[,] JTFASpectrum3 = new double [JTFASpectrum1.GetLength(0)+JTFASpectrum2.GetLength(0), task.FrequencyBins/2];
            double[,] error = new double[JTFASpectrum1.GetLength(0) + JTFASpectrum2.GetLength(0), task.FrequencyBins / 2];
            for (int i = 0; i < JTFASpectrum3.GetLength(0); i++)
            {
                for (int j = 0; j < JTFASpectrum3.GetLength(1); j++)
                {
                    if ( i < JTFASpectrum1.GetLength(0))
                    {
                        JTFASpectrum3[i, j] = JTFASpectrum1[i, j];
                    }
                    else
                    {
                        JTFASpectrum3[i, j] = JTFASpectrum2[i- JTFASpectrum1.GetLength(0), j];
                    }
                    error[i, j] = JTFASpectrum[i, j] - JTFASpectrum3[i, j];
                    Assert.IsTrue(Math.Abs(error[i, j])==0);
                }
            }

        }

        [TestMethod]
        public void JTFA_RealFFT_005()
        {
            /*******************************
             * 输入信号为实数
             * （输入信号的长度-FrequencyBins）*4/FrequencyBins不是整数
             * 存在遗留数据的情况
             * FrequencyBins不是4的整倍数
             * 查找峰值
             * ******************************/
            int Length = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double freqSin = 10;
            Generation.Generation.SineWave(ref sin, 1, 0, freqSin, 1000);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.05);
            ArrayCalculation.Add(sin, noise, ref sin);
            GeneralJTFATask task = new GeneralJTFATask();
            task.FrequencyBins = 505;
            task.SampleRate = 1000;
            double[,] JTFASpectrum = null;
            task.GetJTFA(sin, ref JTFASpectrum);
            var df = task.JTFAInfomation.df;
            var dt = task.JTFAInfomation.dt;
            var f0 = task.JTFAInfomation.f0;
            var FFTSize = task.JTFAInfomation.FFTSize;
        }

        [TestMethod]
        public void JTFA_RealFFT_006()
        {
            /*******************************
             * 输入信号为实数
             * （输入信号的长度-FrequencyBins）/(FrequencyBins/4)是整数
             * FrequencyBins不是4的整倍数
             * 整段进行JTFA与分段进行JTFA进行对比
             * ******************************/
            int Length = 194;
            double[] sin = new double[Length];
            double freqSin = 10;
            JY.DSP.Fundamental.Generation.SineWave(ref sin, 1, 0, freqSin, 100);
            GeneralJTFATask task = new GeneralJTFATask();
            task.FrequencyBins = 50;
            task.SampleRate = 100;
            double[,] JTFASpectrum = null;
            task.GetJTFA(sin, ref JTFASpectrum);
            task.Reset();
            //分段JTFA
            double[] sin1 = new double[74];
            double[] sin2 = new double[Length - sin1.Length];
            Array.Copy(sin, 0, sin1, 0, sin1.Length);
            Array.Copy(sin, sin1.Length, sin2, 0, sin2.Length);
            double[,] JTFASpectrum1 = null;
            double[,] JTFASpectrum2 = null;
            task.GetJTFA(sin1, ref JTFASpectrum1);
            task.GetJTFA(sin2, ref JTFASpectrum2);
            task.Reset();
            double[,] JTFASpectrum3 = new double[JTFASpectrum1.GetLength(0) + JTFASpectrum2.GetLength(0), task.FrequencyBins / 2];
            double[,] error = new double[JTFASpectrum1.GetLength(0) + JTFASpectrum2.GetLength(0), task.FrequencyBins / 2];
            for (int i = 0; i < JTFASpectrum3.GetLength(0); i++)
            {
                for (int j = 0; j < JTFASpectrum3.GetLength(1); j++)
                {
                    if (i < JTFASpectrum1.GetLength(0))
                    {
                        JTFASpectrum3[i, j] = JTFASpectrum1[i, j];
                    }
                    else
                    {
                        JTFASpectrum3[i, j] = JTFASpectrum2[i - JTFASpectrum1.GetLength(0), j];
                    }
                    error[i, j] = JTFASpectrum[i, j] - JTFASpectrum3[i, j];
                    Assert.IsTrue(Math.Abs(error[i, j])==0);
                }
            }
        }

        [TestMethod]
        public void JTFA_RealFFT_007()
        {
            /*******************************
             * 输入信号为实数
             * （输入信号的长度-FrequencyBins）/(FrequencyBins/4)是整数
             * FrequencyBins是4的整倍数
             * 整段进行JTFA与分段进行JTFA进行对比
             * ******************************/
            int Length = 195;
            double[] sin = new double[Length];
            double freqSin = 10;
            JY.DSP.Fundamental.Generation.SineWave(ref sin, 1, 0, freqSin, 100);
            GeneralJTFATask task = new GeneralJTFATask();
            task.FrequencyBins = 52;
            task.SampleRate = 100;
            double[,] JTFASpectrum = null;
            task.GetJTFA(sin, ref JTFASpectrum);
            task.Reset();
            //分段JTFA
            double[] sin1 = new double[78];
            double[] sin2 = new double[Length - sin1.Length];
            Array.Copy(sin, 0, sin1, 0, sin1.Length);
            Array.Copy(sin, sin1.Length, sin2, 0, sin2.Length);
            double[,] JTFASpectrum1 = null;
            double[,] JTFASpectrum2 = null;
            task.GetJTFA(sin1, ref JTFASpectrum1);
            task.GetJTFA(sin2, ref JTFASpectrum2);
            task.Reset();
            double[,] JTFASpectrum3 = new double[JTFASpectrum1.GetLength(0) + JTFASpectrum2.GetLength(0), task.FrequencyBins / 2];
            double[,] error = new double[JTFASpectrum1.GetLength(0) + JTFASpectrum2.GetLength(0), task.FrequencyBins / 2];
            for (int i = 0; i < JTFASpectrum3.GetLength(0); i++)
            {
                for (int j = 0; j < JTFASpectrum3.GetLength(1); j++)
                {
                    if (i < JTFASpectrum1.GetLength(0))
                    {
                        JTFASpectrum3[i, j] = JTFASpectrum1[i, j];
                    }
                    else
                    {
                        JTFASpectrum3[i, j] = JTFASpectrum2[i - JTFASpectrum1.GetLength(0), j];
                    }
                    error[i, j] = JTFASpectrum[i, j] - JTFASpectrum3[i, j];
                    Assert.IsTrue(Math.Abs(error[i, j] ) ==0);
                }
            }
        }

        [TestMethod]
        public void JTFA_RealFFT_008()
        {
            /*******************************
             * 输入信号为实数
             * （输入信号的长度-FrequencyBins）/(FrequencyBins/4)不是整数
             * FrequencyBins是4的整倍数
             * 整段进行JTFA与分段进行JTFA进行对比
             * ******************************/
            int Length = 200;
            double[] sin = new double[Length];
            double freqSin = 10;
            JY.DSP.Fundamental.Generation.SineWave(ref sin, 1, 0, freqSin, 100);
            GeneralJTFATask task = new GeneralJTFATask();
            task.FrequencyBins = 52;
            task.SampleRate = 100;
            double[,] JTFASpectrum = null;
            task.GetJTFA(sin, ref JTFASpectrum);
            task.Reset();
            //分段JTFA
            double[] sin1 = new double[79];
            double[] sin2 = new double[Length - sin1.Length];
            Array.Copy(sin, 0, sin1, 0, sin1.Length);
            Array.Copy(sin, sin1.Length, sin2, 0, sin2.Length);
            double[,] JTFASpectrum1 = null;
            double[,] JTFASpectrum2 = null;
            task.GetJTFA(sin1, ref JTFASpectrum1);
            task.GetJTFA(sin2, ref JTFASpectrum2);
            task.Reset();
            double[,] JTFASpectrum3 = new double[JTFASpectrum1.GetLength(0) + JTFASpectrum2.GetLength(0), task.FrequencyBins / 2];
            double[,] error = new double[JTFASpectrum1.GetLength(0) + JTFASpectrum2.GetLength(0), task.FrequencyBins / 2];
            for (int i = 0; i < JTFASpectrum3.GetLength(0); i++)
            {
                for (int j = 0; j < JTFASpectrum3.GetLength(1); j++)
                {
                    if (i < JTFASpectrum1.GetLength(0))
                    {
                        JTFASpectrum3[i, j] = JTFASpectrum1[i, j];
                    }
                    else
                    {
                        JTFASpectrum3[i, j] = JTFASpectrum2[i - JTFASpectrum1.GetLength(0), j];
                    }
                    error[i, j] = JTFASpectrum[i, j] - JTFASpectrum3[i, j];
                    Assert.IsTrue(Math.Abs(error[i, j]) == 0);
                }
            }
        }

        [TestMethod]
        public void JTFA_RealFFT_009()
        {
            /*******************************
             * 输入信号为实数
             * 输入数据很长 1M
             * （输入信号的长度-FrequencyBins）/(FrequencyBins/4)不是整数
             * FrequencyBin不是4的整倍数
             * 整段进行JTFA与分段进行JTFA进行对比
             * ******************************/
            int Length = 1000000;
            double[] sin = new double[Length];
            double freqSin = 10;
            JY.DSP.Fundamental.Generation.SineWave(ref sin, 1, 0, freqSin, 1000);
            GeneralJTFATask task = new GeneralJTFATask();
            task.FrequencyBins = 512;
            task.SampleRate = 1000;
            double[,] JTFASpectrum = null;
            task.GetJTFA(sin, ref JTFASpectrum);
            task.Reset();
            //分段JTFA
            double[] sin1 = new double[5612];
            double[] sin2 = new double[Length - sin1.Length];
            Array.Copy(sin, 0, sin1, 0, sin1.Length);
            Array.Copy(sin, sin1.Length, sin2, 0, sin2.Length);
            double[,] JTFASpectrum1 = null;
            double[,] JTFASpectrum2 = null;
            task.GetJTFA(sin1, ref JTFASpectrum1);
            task.GetJTFA(sin2, ref JTFASpectrum2);
            task.Reset();
            double[,] JTFASpectrum3 = new double[JTFASpectrum1.GetLength(0) + JTFASpectrum2.GetLength(0), task.FrequencyBins / 2];
            double[,] error = new double[JTFASpectrum1.GetLength(0) + JTFASpectrum2.GetLength(0), task.FrequencyBins / 2];
            for (int i = 0; i < JTFASpectrum3.GetLength(0); i++)
            {
                for (int j = 0; j < JTFASpectrum3.GetLength(1); j++)
                {
                    if (i < JTFASpectrum1.GetLength(0))
                    {
                        JTFASpectrum3[i, j] = JTFASpectrum1[i, j];
                    }
                    else
                    {
                        JTFASpectrum3[i, j] = JTFASpectrum2[i - JTFASpectrum1.GetLength(0), j];
                    }
                    error[i, j] = JTFASpectrum[i, j] - JTFASpectrum3[i, j];
                    Assert.IsTrue(Math.Abs(error[i, j]) == 0);
                }
            }
        }

        [TestMethod]
        public void JTFA_ComplexFFT_001()
        {
            /*******************************
             * 输入信号为复数
             * （输入信号的长度-FrequencyBins）/(FrequencyBins/4)是整数
             * 不存在补0的情况
             * 查找峰值
             * ******************************/
            int Length = 100;
            double[] sin = new double[Length];
            double[] cos = new double[Length];
            double freqSin = 10;
            JY.DSP.Fundamental.Generation.SineWave(ref sin, 1, 0, freqSin, 100);
            JY.DSP.Fundamental.Generation.SineWave(ref cos, 1, 90, freqSin, 100);
            Complex[] signal = new Complex[Length];
            for (int i = 0; i < Length; i++)
            {
                signal[i] = new Complex(cos[i], sin[i]);
            }
            GeneralJTFATask task = new GeneralJTFATask();
            task.FrequencyBins = 52;
            task.SampleRate = 100;
            double[,] JTFASpectrum = new double[task.FrequencyBins , 5];
            double df;
            double valueFreq = 1.0 / 2;
            task.GetJTFA(signal, ref JTFASpectrum);
            df = task.JTFAInfomation.df;
            var f0 = task.JTFAInfomation.f0;
            var a = (freqSin - f0) / df; 
        }

        [TestMethod]
        public void JTFA_ComplexFFT_002()
        {
            /*******************************
             * 输入信号为复数
             * （输入信号的长度-FrequencyBins）/(FrequencyBins/4)不是整数
             * 存在补0的情况
             * 查找峰值
             * ******************************/
            int Length = 1000;
            double[] sin = new double[Length];
            double[] cos = new double[Length];
            double freqSin = 10;
            JY.DSP.Fundamental.Generation.SineWave(ref sin, 1, 0, freqSin, 1000);
            JY.DSP.Fundamental.Generation.SineWave(ref cos, 1, 90, freqSin, 1000);
            Complex[] signal = new Complex[Length];
            for (int i = 0; i < Length; i++)
            {
                signal[i] = new Complex(cos[i], sin[i]);
            }
            GeneralJTFATask task = new GeneralJTFATask();
            task.SampleRate = 1000;
            double[,] JTFASpectrum = new double[task.FrequencyBins , 5];
            double df;
            double valueFreq = 1.0 / 2;
            task.GetJTFA(signal, ref JTFASpectrum);
            int FFTcount = (int)Math.Ceiling(((double)Length - task.FrequencyBins) / task.FrequencyBins * 4 + 1);
            //for (int i = 0; i < FFTcount - 1; i++)//由于补0，最后一次FFT的幅值是不对的
            //{
            //    double value = JTFASpectrum[(int)(freqSin / df + task.SampleRate / 4 - 1), i];
            //    Assert.IsTrue(Math.Abs(value - valueFreq) / valueFreq < 0.05);
            //}
        }

        [TestMethod]
        public void JTFA_ComplexFFT_003()
        {
            /*******************************
             * 输入信号为复数
             * （输入信号的长度-FrequencyBins）/(FrequencyBins/4)不是整数
             * FrequencyBins是4的整数倍
             * ******************************/
            int Length = 200;
            double[] sin = new double[Length];
            double[] cos = new double[Length];
            double freqSin = 10;
            JY.DSP.Fundamental.Generation.SineWave(ref sin, 1, 0, freqSin, 100);
            JY.DSP.Fundamental.Generation.SineWave(ref cos, 1, 90, freqSin, 100);
            Complex[] signal = new Complex[Length];
            for (int i = 0; i < Length; i++)
            {
                signal[i] = new Complex(cos[i], sin[i]);
            }
            GeneralJTFATask task = new GeneralJTFATask();
            task.FrequencyBins = 52;
            task.SampleRate = 100;
            double[,] JTFASpectrum=null;
            task.GetJTFA(signal, ref JTFASpectrum);
            task.Reset();
            Complex[] signal1 = new Complex[70];
            Complex[] signal2 = new Complex[Length- signal1.Length];
            for (int i = 0; i < Length; i++)
            {
                if (i < signal1.Length)
                {
                    signal1[i] = signal[i];
                }
                else
                {
                    signal2[i- signal1.Length] = signal[i];
                }
            }
            double[,] JTFASpectrum1 = null;
            double[,] JTFASpectrum2 = null;
            task.GetJTFA(signal1, ref JTFASpectrum1);
            task.GetJTFA(signal2, ref JTFASpectrum2);
            double[,] JTFASpectrum3 = new double[JTFASpectrum1.GetLength(0) + JTFASpectrum2.GetLength(0), task.FrequencyBins ];
            double[,] error = new double[JTFASpectrum1.GetLength(0) + JTFASpectrum2.GetLength(0), task.FrequencyBins];
            for (int i = 0; i < JTFASpectrum3.GetLength(0); i++)
            {
                for (int j = 0; j < JTFASpectrum3.GetLength(1); j++)
                {
                    if (i < JTFASpectrum1.GetLength(0))
                    {
                        JTFASpectrum3[i, j] = JTFASpectrum1[i, j];
                    }
                    else
                    {
                        JTFASpectrum3[i, j] = JTFASpectrum2[i - JTFASpectrum1.GetLength(0), j];
                    }
                    error[i, j] = JTFASpectrum[i, j] - JTFASpectrum3[i, j];
                    Assert.IsTrue(Math.Abs(error[i, j]) == 0);
                }
            }
        }

        [TestMethod]
        public void JTFA_ComplexFFT_004()
        {
            /*******************************
             * 输入信号为复数
             * （输入信号的长度-FrequencyBins）/(FrequencyBins/4)不是整数
             * FrequencyBins不是4的整数倍
             * ******************************/
            int Length = 199;
            double[] sin = new double[Length];
            double[] cos = new double[Length];
            double freqSin = 10;
            JY.DSP.Fundamental.Generation.SineWave(ref sin, 1, 0, freqSin, 100);
            JY.DSP.Fundamental.Generation.SineWave(ref cos, 1, 90, freqSin, 100);
            Complex[] signal = new Complex[Length];
            for (int i = 0; i < Length; i++)
            {
                signal[i] = new Complex(cos[i], sin[i]);
            }
            GeneralJTFATask task = new GeneralJTFATask();
            task.FrequencyBins = 50;
            task.SampleRate = 100;
            double[,] JTFASpectrum = null;
            task.GetJTFA(signal, ref JTFASpectrum);
            task.Reset();
            Complex[] signal1 = new Complex[70];
            Complex[] signal2 = new Complex[Length - signal1.Length];
            for (int i = 0; i < Length; i++)
            {
                if (i < signal1.Length)
                {
                    signal1[i] = signal[i];
                }
                else
                {
                    signal2[i - signal1.Length] = signal[i];
                }
            }
            double[,] JTFASpectrum1 = null;
            double[,] JTFASpectrum2 = null;
            task.GetJTFA(signal1, ref JTFASpectrum1);
            task.GetJTFA(signal2, ref JTFASpectrum2);
            double[,] JTFASpectrum3 = new double[JTFASpectrum1.GetLength(0) + JTFASpectrum2.GetLength(0), task.FrequencyBins];
            double[,] error = new double[JTFASpectrum1.GetLength(0) + JTFASpectrum2.GetLength(0), task.FrequencyBins];
            for (int i = 0; i < JTFASpectrum3.GetLength(0); i++)
            {
                for (int j = 0; j < JTFASpectrum3.GetLength(1); j++)
                {
                    if (i < JTFASpectrum1.GetLength(0))
                    {
                        JTFASpectrum3[i, j] = JTFASpectrum1[i, j];
                    }
                    else
                    {
                        JTFASpectrum3[i, j] = JTFASpectrum2[i - JTFASpectrum1.GetLength(0), j];
                    }
                    error[i, j] = JTFASpectrum[i, j] - JTFASpectrum3[i, j];
                    Assert.IsTrue(Math.Abs(error[i, j]) == 0);
                }
            }
        }

        [TestMethod]
        public void JTFA_ComplexFFT_005()
        {
            /*******************************
             * 输入信号为复数
             * （输入信号的长度-FrequencyBins）/(FrequencyBins/4)是整数
             * FrequencyBins是4的整数倍
             * ******************************/
            int Length = 195;
            double[] sin = new double[Length];
            double[] cos = new double[Length];
            double freqSin = 10;
            JY.DSP.Fundamental.Generation.SineWave(ref sin, 1, 0, freqSin, 100);
            JY.DSP.Fundamental.Generation.SineWave(ref cos, 1, 90, freqSin, 100);
            Complex[] signal = new Complex[Length];
            for (int i = 0; i < Length; i++)
            {
                signal[i] = new Complex(cos[i], sin[i]);
            }
            GeneralJTFATask task = new GeneralJTFATask();
            task.FrequencyBins = 52;
            task.SampleRate = 100;
            double[,] JTFASpectrum = null;
            task.GetJTFA(signal, ref JTFASpectrum);
            task.Reset();
            Complex[] signal1 = new Complex[78];
            Complex[] signal2 = new Complex[Length - signal1.Length];
            for (int i = 0; i < Length; i++)
            {
                if (i < signal1.Length)
                {
                    signal1[i] = signal[i];
                }
                else
                {
                    signal2[i - signal1.Length] = signal[i];
                }
            }
            double[,] JTFASpectrum1 = null;
            double[,] JTFASpectrum2 = null;
            task.GetJTFA(signal1, ref JTFASpectrum1);
            task.GetJTFA(signal2, ref JTFASpectrum2);
            double[,] JTFASpectrum3 = new double[JTFASpectrum1.GetLength(0) + JTFASpectrum2.GetLength(0), task.FrequencyBins];
            double[,] error = new double[JTFASpectrum1.GetLength(0) + JTFASpectrum2.GetLength(0), task.FrequencyBins];
            for (int i = 0; i < JTFASpectrum3.GetLength(0); i++)
            {
                for (int j = 0; j < JTFASpectrum3.GetLength(1); j++)
                {
                    if (i < JTFASpectrum1.GetLength(0))
                    {
                        JTFASpectrum3[i, j] = JTFASpectrum1[i, j];
                    }
                    else
                    {
                        JTFASpectrum3[i, j] = JTFASpectrum2[i - JTFASpectrum1.GetLength(0), j];
                    }
                    error[i, j] = JTFASpectrum[i, j] - JTFASpectrum3[i, j];
                    Assert.IsTrue(Math.Abs(error[i, j]) == 0);
                }
            }
        }

        [TestMethod]
        public void JTFA_ComplexFFT_006()
        {
            /*******************************
             * 输入信号为复数
             * （输入信号的长度-FrequencyBins）/(FrequencyBins/4)是整数
             * FrequencyBins不是4的整数倍
             * ******************************/
            int Length = 194;
            double[] sin = new double[Length];
            double[] cos = new double[Length];
            double freqSin = 10;
            JY.DSP.Fundamental.Generation.SineWave(ref sin, 1, 0, freqSin, 100);
            JY.DSP.Fundamental.Generation.SineWave(ref cos, 1, 90, freqSin, 100);
            Complex[] signal = new Complex[Length];
            for (int i = 0; i < Length; i++)
            {
                signal[i] = new Complex(cos[i], sin[i]);
            }
            GeneralJTFATask task = new GeneralJTFATask();
            task.FrequencyBins = 50;
            task.SampleRate = 100;
            double[,] JTFASpectrum = null;
            task.GetJTFA(signal, ref JTFASpectrum);
            task.Reset();
            Complex[] signal1 = new Complex[74];
            Complex[] signal2 = new Complex[Length - signal1.Length];
            for (int i = 0; i < Length; i++)
            {
                if (i < signal1.Length)
                {
                    signal1[i] = signal[i];
                }
                else
                {
                    signal2[i - signal1.Length] = signal[i];
                }
            }
            double[,] JTFASpectrum1 = null;
            double[,] JTFASpectrum2 = null;
            task.GetJTFA(signal1, ref JTFASpectrum1);
            task.GetJTFA(signal2, ref JTFASpectrum2);
            double[,] JTFASpectrum3 = new double[JTFASpectrum1.GetLength(0) + JTFASpectrum2.GetLength(0), task.FrequencyBins];
            double[,] error = new double[JTFASpectrum1.GetLength(0) + JTFASpectrum2.GetLength(0), task.FrequencyBins];
            for (int i = 0; i < JTFASpectrum3.GetLength(0); i++)
            {
                for (int j = 0; j < JTFASpectrum3.GetLength(1); j++)
                {
                    if (i < JTFASpectrum1.GetLength(0))
                    {
                        JTFASpectrum3[i, j] = JTFASpectrum1[i, j];
                    }
                    else
                    {
                        JTFASpectrum3[i, j] = JTFASpectrum2[i - JTFASpectrum1.GetLength(0), j];
                    }
                    error[i, j] = JTFASpectrum[i, j] - JTFASpectrum3[i, j];
                    Assert.IsTrue(Math.Abs(error[i, j]) == 0);
                }
            }
        }

        [TestMethod]
        public void JTFA_ComplexFFT_007()
        {
            /*******************************
             * 输入信号为复数
             * 数据长度为1M
             * （输入信号的长度-FrequencyBins）/(FrequencyBins/4)是整数
             * FrequencyBins不是4的整数倍
             * ******************************/
            int Length = 1000000;
            double[] sin = new double[Length];
            double[] cos = new double[Length];
            double freqSin = 10;
            JY.DSP.Fundamental.Generation.SineWave(ref sin, 1, 0, freqSin, 1000);
            JY.DSP.Fundamental.Generation.SineWave(ref cos, 1, 90, freqSin, 1000);
            Complex[] signal = new Complex[Length];
            for (int i = 0; i < Length; i++)
            {
                signal[i] = new Complex(cos[i], sin[i]);
            }
            GeneralJTFATask task = new GeneralJTFATask();
            task.FrequencyBins = 327;
            task.SampleRate = 1000;
            double[,] JTFASpectrum = null;
            task.GetJTFA(signal, ref JTFASpectrum);
            task.Reset();
            Complex[] signal1 = new Complex[657];
            Complex[] signal2 = new Complex[Length - signal1.Length];
            for (int i = 0; i < Length; i++)
            {
                if (i < signal1.Length)
                {
                    signal1[i] = signal[i];
                }
                else
                {
                    signal2[i - signal1.Length] = signal[i];
                }
            }
            double[,] JTFASpectrum1 = null;
            double[,] JTFASpectrum2 = null;
            task.GetJTFA(signal1, ref JTFASpectrum1);
            task.GetJTFA(signal2, ref JTFASpectrum2);
            double[,] JTFASpectrum3 = new double[JTFASpectrum1.GetLength(0) + JTFASpectrum2.GetLength(0), task.FrequencyBins];
            double[,] error = new double[JTFASpectrum1.GetLength(0) + JTFASpectrum2.GetLength(0), task.FrequencyBins];
            for (int i = 0; i < JTFASpectrum3.GetLength(0); i++)
            {
                for (int j = 0; j < JTFASpectrum3.GetLength(1); j++)
                {
                    if (i < JTFASpectrum1.GetLength(0))
                    {
                        JTFASpectrum3[i, j] = JTFASpectrum1[i, j];
                    }
                    else
                    {
                        JTFASpectrum3[i, j] = JTFASpectrum2[i - JTFASpectrum1.GetLength(0), j];
                    }
                    error[i, j] = JTFASpectrum[i, j] - JTFASpectrum3[i, j];
                    Assert.IsTrue(Math.Abs(error[i, j]) == 0);
                }
            }
        }
    }
}
