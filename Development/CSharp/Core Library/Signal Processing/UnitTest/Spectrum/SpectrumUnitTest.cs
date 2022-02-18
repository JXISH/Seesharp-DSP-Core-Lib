using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeeSharpTools.JXI.SignalProcessing.GeneralSpectrum;
using SeeSharpTools.JY.ArrayUtility;
using SeeSharpTools.JX.DataViewer;
using SeeSharpTools.JY.File;
using SeeSharpTools.JXI.SignalProcessing.Generation;
using SeeSharpTools.JXI.SignalProcessing.Window;

namespace SeeSharpTools.JXI.SignalProcessing.GeneralSpectrumTest
{
    [TestClass]
    public class SpectrumUnitTest
    {
        //[TestMethod]
        //public void GeneralSpectrumTask_WindowFunction()
        //{
        //    /**********************************
        //     * 使用统一的窗生成函数生成各个窗的数据
        //     * ********************************/
        //    int length = 43467;
        //    double[] windowCoe = new double[5];
        //    //var window = WindowType.Hanning;
        //    //var window = WindowType.Hamming;
        //    //var window = WindowType.Blackman;
        //    //var window = WindowType.Blackman_Harris;
        //    //var window = WindowType.Exact_Blackman;
        //    //var window = WindowType.Flat_Top ;
        //    //var window = WindowType.Four_Term_B_Harris;
        //    var window = WindowType.Seven_Term_B_Harris ;
        //    double[] windowdata1 = new double[length];
        //    double[] windowdata2 = new double[length];
        //    double CG1 = 0;
        //    double CG2 = 0;
        //    double ENBW1 = 0;
        //    double ENBW2 = 0;
        //    Window.GetWindow(window, ref windowdata1, out CG1, out ENBW1);
        //    Window.GeneralizedWindowCoe(window, ref windowCoe);
        //    Window.GeneralizedCosineWindow(windowCoe, ref windowdata2, out CG2, out ENBW2);
        //    Assert.IsTrue(Math.Abs(CG2 - CG1) / CG1 < 0.001);
        //    Assert.IsTrue(Math.Abs(ENBW2 - ENBW1) / ENBW1 < 0.001);
        //    for (int i = 0; i < length; i++)
        //    {
        //        var err = Math.Abs(windowdata2[i] - windowdata1[i]) / windowdata1[i];
        //        if (windowdata2[i] != 0 && windowdata1[i] != 0)
        //        {
        //            Assert.IsTrue(err < 0.002);
        //        }
        //    }
        //}

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_PSD_001()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * ********************************/
            int Length = 1000;
            double sampleRate = 2000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] specNone = new double[Length / 2];
            double[] specHanning = new double[Length / 2];
            double[] specHamming = new double[Length / 2];
            double[] specBlackman = new double[Length / 2];
            double[] specBlackman_Harris = new double[Length / 2];
            double[] specExact_Blackman = new double[Length / 2];
           // double[] specFlat_Top = new double[Length / 2];
            double[] specFour_Term_B_Harris = new double[Length / 2];
           // double[] specSeven_Term_B_Harris = new double[Length / 2];
            double[,] spec = new double[4,Length / 2];

            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();

            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.V2     ;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = true;
            //   _task.Commit();
            _task.GetSpectrum(sin, ref specNone);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            // VISN_Plot.PlotData("Spectrum1",fStart,df,spec);

            _task.WindowType = WindowType.Hanning ;
            _task.GetSpectrum(sin, ref specHanning);


            _task.WindowType = WindowType.Hamming ;
            _task.GetSpectrum(sin, ref specHamming);

            _task.WindowType = WindowType.Blackman ;
            _task.GetSpectrum(sin, ref specBlackman);

            _task.WindowType = WindowType.Blackman_Harris ;
            _task.GetSpectrum(sin, ref specBlackman_Harris);


            _task.WindowType = WindowType.Exact_Blackman ;
            _task.GetSpectrum(sin, ref specExact_Blackman);

           // _task.WindowType = WindowType.Flat_Top ;
           // _task.GetSpectrum(sin, ref specFlat_Top);

            _task.WindowType = WindowType.Four_Term_B_Harris ;
            _task.GetSpectrum(sin, ref specFour_Term_B_Harris);

           // _task.WindowType = WindowType.Seven_Term_B_Harris;
           // _task.GetSpectrum(sin, ref specSeven_Term_B_Harris);

            InsetArray(ref spec, specNone, 0);
            InsetArray(ref spec, specHanning, 1);
            InsetArray(ref spec, specHamming, 2);
            InsetArray(ref spec, specBlackman_Harris, 3);
            //InsetArray(ref spec, specExact_Blackman, 4);
            //InsetArray(ref spec, specExact_Blackman, 5);
            //InsetArray(ref spec, specFlat_Top, 6);
            //InsetArray(ref spec, specFour_Term_B_Harris, 7);
            //InsetArray(ref spec, specSeven_Term_B_Harris, 8);
            //AnalogWaveformChart.Plot("Spectrum1", fStart, df, spec);
            //Peak[] a = new Peak[100];
            //a = _task.FindPeak(specNone, 0.6, 0, 150);
            //Assert.IsTrue(Math.Abs((2 - a[0].PeakValue) / 2) < 0.01);//直流分量的幅值
            //Assert.IsTrue(Math.Abs((1 / Math.Sqrt(2) - a[1].PeakValue) / (1 / Math.Sqrt(2))) < 0.01); //正弦信号的有效值
            //Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.01);//正弦信号的频率
        }

        private void InsetArray(ref double[,] array2D, double[] array, int index)
        {
            for (int i = 0; i < Math.Min(array.Length, array2D.GetLength(1)); i++)
            {
                array2D[index, i] = array[i];
            }
        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_001()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，不加窗，不平均，输出频谱单位为V
             * 验证峰值的有效值是不是2V和1/sqrt（2）=0.707V
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving ;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length/2;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false ;
         //   _task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
           // VISN_Plot.PlotData("Spectrum1",fStart,df,spec);
            //AnalogWaveformChart.Plot("Spectrum1",fStart, df, spec);
            Peak[] a = new Peak[100];
            a=_task.FindPeak(spec, 0.6, 0, 150);
            Assert.IsTrue(Math.Abs((2 - a[0].PeakValue) / 2)< 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((1/Math.Sqrt(2) - a[1].PeakValue)/ (1 / Math.Sqrt(2)))  < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency)/10) < 0.01);//正弦信号的频率

        }


        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_002()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，Blackman窗，不平均，输出频谱单位为V
             * 验证峰值的有效值是不是2V和1/sqrt（2）=0.707V
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length/2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.Blackman;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.Exponential;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length/2;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
           // _task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //VISN_Plot.PlotData("Spectrum1",fStart,df,spec);
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, 0.6, 0, 150);
            Assert.IsTrue(Math.Abs((2 - a[0].PeakValue) / 2) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((1 / Math.Sqrt(2) - a[1].PeakValue) / (1 / Math.Sqrt(2))) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.05);//正弦信号的频率

        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_003()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，Blackman_Harris窗，不平均，输出频谱单位为V
             * 验证峰值的有效值是不是2V和1/sqrt（2）=0.707V
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.Blackman_Harris ;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.Exponential;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
           // _task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, 0.6, 0, 150);
            Assert.IsTrue(Math.Abs((2 - a[0].PeakValue) / 2) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((1 / Math.Sqrt(2) - a[1].PeakValue) / (1 / Math.Sqrt(2))) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.05);//正弦信号的频率

        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_004()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，Exact_Blackman窗，不平均，输出频谱单位为V
             * 验证峰值的有效值是不是2V和1/sqrt（2）=0.707V
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.Exact_Blackman ;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.Exponential;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
            //_task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, 0.6, 0, 150);
            Assert.IsTrue(Math.Abs((2 - a[0].PeakValue) / 2) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((1 / Math.Sqrt(2) - a[1].PeakValue) / (1 / Math.Sqrt(2))) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.05);//正弦信号的频率

        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_005()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，Flat_Top窗，不平均，输出频谱单位为V
             * 验证峰值的有效值是不是2V和1/sqrt（2）=0.707V
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.01);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.Flat_Top_90D ;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.Exponential;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
            //_task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, 0.6, 0, 150);
           // Assert.IsTrue(Math.Abs((2 - a[0].PeakValue) / 2) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((1 / Math.Sqrt(2) - a[1].PeakValue) / (1 / Math.Sqrt(2))) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.05);//正弦信号的频率

        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_006()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，Four_Term_B_Harris窗，不平均，输出频谱单位为V
             * 验证峰值的有效值是不是2V和1/sqrt（2）=0.707V
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.Four_Term_B_Harris ;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.Exponential;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
            //_task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, 0.6, 0, 150);
            Assert.IsTrue(Math.Abs((2 - a[0].PeakValue) / 2) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((1 / Math.Sqrt(2) - a[1].PeakValue) / (1 / Math.Sqrt(2))) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.05);//正弦信号的频率

        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_007()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，Hamming窗，不平均，输出频谱单位为V
             * 验证峰值的有效值是不是2V和1/sqrt（2）=0.707V
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.Hamming ;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.Exponential;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
            //_task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
           // VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, 0.6, 0, 150);
            Assert.IsTrue(Math.Abs((2 - a[0].PeakValue) / 2) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((1 / Math.Sqrt(2) - a[1].PeakValue) / (1 / Math.Sqrt(2))) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.05);//正弦信号的频率

        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_008()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，Hanning窗，不平均，输出频谱单位为V
             * 验证峰值的有效值是不是2V和1/sqrt（2）=0.707V
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.Hanning ;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.Exponential;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
            //_task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, 0.6, 0, 150);
            Assert.IsTrue(Math.Abs((2 - a[0].PeakValue) / 2) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((1 / Math.Sqrt(2) - a[1].PeakValue) / (1 / Math.Sqrt(2))) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.05);//正弦信号的频率

        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_009()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，不加窗，平均方式PeakHold，输出频谱单位为V
             * 验证峰值的有效值是不是2V和1/sqrt（2）=0.707V
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.PeakHold;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
            //_task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
           // VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, 0.6, 0, 150);
            Assert.IsTrue(Math.Abs((2 - a[0].PeakValue) / 2) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((1 / Math.Sqrt(2) - a[1].PeakValue) / (1 / Math.Sqrt(2))) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.01);//正弦信号的频率

        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_010()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，不加窗
             * 平均方式RMSAveraging，加权方式LinearMoving,权设定平均次数=10
             * 输出频谱单位为V
             * 验证峰值的有效值是不是2V和1/sqrt（2）=0.707V
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.RMSAveraging ;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving ;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
           // _task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
           // VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, 0.6, 0, 150);
            Assert.IsTrue(Math.Abs((2 - a[0].PeakValue) / 2) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((1 / Math.Sqrt(2) - a[1].PeakValue) / (1 / Math.Sqrt(2))) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.01);//正弦信号的频率

        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_011()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，不加窗
             * 平均方式RMSAveraging，加权方式LinearContinuous
             * 输出频谱单位为V
             * 验证峰值的有效值是不是2V和1/sqrt（2）=0.707V
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.RMSAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearContinuous  ;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
           // _task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, 0.6, 0, 150);
            Assert.IsTrue(Math.Abs((2 - a[0].PeakValue) / 2) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((1 / Math.Sqrt(2) - a[1].PeakValue) / (1 / Math.Sqrt(2))) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.01);//正弦信号的频率

        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_012()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，不加窗
             * 平均方式RMSAveraging，加权方式Exponential,加权设定指数衰减系数=1
             * 输出频谱单位为V
             * 验证峰值的有效值是不是2V和1/sqrt（2）=0.707V
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.RMSAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.Exponential;
            _task.Average.Size = 1;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
            //_task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, 0.6, 0, 150);
            Assert.IsTrue(Math.Abs((2 - a[0].PeakValue) / 2) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((1 / Math.Sqrt(2) - a[1].PeakValue) / (1 / Math.Sqrt(2))) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.01);//正弦信号的频率

        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_013()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，不加窗，不平均，输出频谱单位为V2
             * 验证峰值的有效值是不是4和1/2=0.5
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.V2 ;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
            //_task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, 0.4, 0, 150);
            Assert.IsTrue(Math.Abs((4 - a[0].PeakValue) / 4) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((0.5- a[1].PeakValue) / 0.5) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.01);//正弦信号的频率

        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_014()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，不加窗，不平均，输出频谱单位为dBV
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.dBV ;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
           // _task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            double f0Value = 20 * Math.Log10  (2);
            double f10Value = 20 * Math.Log10(1/Math.Sqrt(2));
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, f10Value*1.1, 0, 150);
            Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.01);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_015()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，不加窗，不平均，输出频谱单位为dBmV
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.dBmV ;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
           // _task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            double f0Value = 20 * Math.Log10(2*1000);
            double f10Value = 20 * Math.Log10(1 / Math.Sqrt(2)*1000);
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, f10Value / 1.1, 0, 150);
            Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.01);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_016()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，不加窗，不平均，输出频谱单位为dBV
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.dBuV ;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
           // _task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            double f0Value = 20 * Math.Log10(2*1000000);
            double f10Value = 20 * Math.Log10(1 / Math.Sqrt(2)*1000000);
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, f10Value /1.1, 0, 150);
            Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.01);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_017()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，不加窗，不平均，输出频谱单位为W
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.W ;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
            //_task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            double f0Value = Math.Pow(2, 2) / _task.Unit.Impedance;
            double f10Value = 0.5 / _task.Unit.Impedance;
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, f10Value / 1.1, 0, 150);
            Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.01);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_018()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，不加窗，不平均，输出频谱单位为dBW
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.dBW ;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
          //  _task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            double f0Value =10*Math.Log10 ( Math.Pow(2, 2) / _task.Unit.Impedance);
            double f10Value = 10 * Math.Log10(0.5 / _task.Unit.Impedance);
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, f10Value * 1.1, 0, 150);
            Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.01);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_019()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，不加窗，不平均，输出频谱单位为dBm
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.dBm ;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
           // _task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            double f0Value = 10 * Math.Log10(Math.Pow(2, 2) / _task.Unit.Impedance*1000);
            double f10Value = 10 * Math.Log10(0.5 / _task.Unit.Impedance*1000);
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, f10Value / 1.1, 0, 150);
            Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.01);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_020()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，不加窗，不平均，输出密度谱单位为V
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = true ;
           // _task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
           // VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            double f0Value = 2/df;
            double f10Value = 1 / Math.Sqrt(2) / df;
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, 0.6, 0, 150);
            Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.01);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_021()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，不加窗，不平均，输出密度谱单位为V2
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.V2;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = true;
            //_task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
           // VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            double f0Value = Math.Pow ( 2,2)/df;
            double f10Value = Math.Pow(1 / Math.Sqrt(2),2)/df ;
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, f10Value/1.1, 0, 150);
            Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.01);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_022()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，不加窗，不平均，输出密度谱单位为dBV
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.dBV ;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = true;
            //_task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            double f0Value = 20 * Math.Log10(2)/df;
            double f10Value = 20 * Math.Log10(1 / Math.Sqrt(2) )/ df;
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, f10Value*1.1, 0, 150);
            Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.05);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_023()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，不加窗，不平均，输出密度谱单位为dBmV
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.dBmV;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = true;
           // _task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            double f0Value = 20 * Math.Log10(2*1000) / df;
            double f10Value = 20 * Math.Log10(1 / Math.Sqrt(2)*1000) / df;
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, f10Value / 1.1, 0, 150);
            Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.01);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_024()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，不加窗，不平均，输出密度谱单位为dBuV
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.dBuV;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = true;
           // _task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            double f0Value = 20 * Math.Log10(2 * 1000000) / df;
            double f10Value = 20 * Math.Log10(1 / Math.Sqrt(2) * 1000000) / df;
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, f10Value / 1.1, 0, 150);
            Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.01);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_025()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，不加窗，不平均，输出密度谱单位为W
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.W;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = true;
           // _task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            double f0Value = Math.Pow (2,2)/ _task.Unit.Impedance / df;
            double f10Value = 0.5 / _task.Unit.Impedance/ df;
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, f10Value / 1.1, 0, 150);
            Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.01);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_026()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，不加窗，不平均，输出密度谱单位为dBW
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.dBW ;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = true;
            //_task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            double f0Value = 10 * Math.Log10(Math.Pow(2, 2) / _task.Unit.Impedance) / df;
            double f10Value = 10 * Math.Log10(0.5 / _task.Unit.Impedance) / df;
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, f10Value *1.1, 0, 150);
            Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.01);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_027()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，不加窗，不平均，输出密度谱单位为dBW
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.dBm;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = true;
            //_task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //VISN_Plot.PlotData("Spectrum1", fStart, df, spec);
            double f0Value = 10 * Math.Log10(Math.Pow(2, 2) / _task.Unit.Impedance*1000) / df;
            double f10Value = 10 * Math.Log10(0.5 / _task.Unit.Impedance*1000) / df;
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, f10Value /1.1, 0, 150);
            Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.01);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_028()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，Blackman窗，不平均，输出密度谱单位为V
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.Blackman ;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = true;
          //  _task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //VISN_Plot.PlotData("Spectrum2", fStart, df, spec);
            double f0Value = 2 / 1.72676 / df;
            double f10Value = 1 / Math.Sqrt(2) / 1.72676 / df;
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, f10Value/1.1, 0, 150);
            //Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.01);//直流分量的幅值
           // Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.05);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_029()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，Blackman_Harris窗，不平均，输出密度谱单位为V
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.Blackman_Harris ;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = true;
          //  _task.Commit();
            _task.GetSpectrum(sin, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //VISN_Plot.PlotData("Spectrum3", fStart, df, spec);
            double f0Value = 2/ 1.708538/df;
            double f10Value = 1 / Math.Sqrt(2) / 1.708538 / df;
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, f10Value/1.1, 0, 150);
            //Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.01);//直流分量的幅值
           // Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.05);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_ComplexFFT_001()
        {
            /**********************************
             * 产生复数信号，频率为10Hz，直流分量为2V，幅值1Vpp
             * 复数FFT，不加窗，不平均，输出频谱单位为V
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double DCOffset = 2;
            double Amp = 1;
            double frequency = 50;
            double[] sin = new double[Length];
            double[] cos = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length ];
            Generation.Generation.SineWave(ref sin, Amp, 0, frequency, sampleRate);
            Generation.Generation.SineWave(ref cos, Amp, 90, frequency, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.05);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.Add(cos, noise, ref cos);
            ArrayCalculation.AddOffset(ref sin, DCOffset);
            ArrayCalculation.AddOffset(ref cos, DCOffset);
            var signal = new Complex[Length];
            for (int i = 0; i < Length; i++)
            {
                signal[i] = new Complex(cos[i], sin[i]);
            }

            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Complex;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
           // _task.Commit();
            _task.GetSpectrum(signal, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            //AnalogWaveformChart.Plot("Spectrum3", fStart, df, spec);
            double f0Value = Math.Sqrt (Math.Pow(DCOffset,2)*2) ;
            double f10Value = 1/Math.Sqrt(Math.Pow(Amp,2));
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, 0.4, -15, 150);
            Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((frequency - a[1].PeakFrequency) / frequency) < 0.01);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_ComplexFFT_002()
        {
            /**********************************
             * 产生复数信号，频率为10Hz，直流分量为2V，幅值1Vpp
             * 复数FFT，Blackman窗，不平均，输出频谱单位为V
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double DCOffset = 2;
            double Amp = 1;
            double frequency = 10;
            double[] sin = new double[Length];
            double[] cos = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length];
            Generation.Generation.SineWave(ref sin, Amp, 0, frequency, sampleRate);
            Generation.Generation.SineWave(ref cos, Amp, 90, frequency, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.05);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.Add(cos, noise, ref cos);
            ArrayCalculation.AddOffset(ref sin, DCOffset);
            ArrayCalculation.AddOffset(ref cos, DCOffset);
            var signal = new Complex[Length];
            for (int i = 0; i < Length; i++)
            {
                signal[i] = new Complex(cos[i], sin[i]);
            }

            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Complex;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.Blackman;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length-3;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
           // _task.Commit();
            _task.GetSpectrum(signal, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            // VISN_Plot.PlotData("Spectrum3", fStart, df, spec);
            double f0Value = Math.Sqrt(Math.Pow(DCOffset, 2) * 2);
            double f10Value = 1 / Math.Sqrt(Math.Pow(Amp, 2));
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, f10Value / 1.1, -15, 150);
            Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((frequency - a[1].PeakFrequency) / frequency) < 0.04);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_ComplexFFT_003()
        {
            /**********************************
             * 产生复数信号，频率为10Hz，直流分量为2V，幅值1Vpp
             * 复数FFT，Blackman_Harris窗，不平均，输出频谱单位为V
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double DCOffset = 2;
            double Amp = 1;
            double frequency = 10;
            double[] sin = new double[Length];
            double[] cos = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length];
            Generation.Generation.SineWave(ref sin, Amp, 0, frequency, sampleRate);
            Generation.Generation.SineWave(ref cos, Amp, 90, frequency, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.05);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.Add(cos, noise, ref cos);
            ArrayCalculation.AddOffset(ref sin, DCOffset);
            ArrayCalculation.AddOffset(ref cos, DCOffset);
            var signal = new Complex[Length];
            for (int i = 0; i < Length; i++)
            {
                signal[i] = new Complex(cos[i], sin[i]);
            }

            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Complex;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.Blackman_Harris ;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
           // _task.Commit();
            _task.GetSpectrum(signal, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            // VISN_Plot.PlotData("Spectrum3", fStart, df, spec);
            double f0Value = Math.Sqrt(Math.Pow(DCOffset, 2) * 2);
            double f10Value = 1 / Math.Sqrt(Math.Pow(Amp, 2));
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, f10Value / 1.1, -15, 150);
            Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.04);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_ComplexFFT_004()
        {
            /**********************************
             * 产生复数信号，频率为10Hz，直流分量为2V，幅值1Vpp
             * 复数FFT，Exact_Blackman窗，不平均，输出频谱单位为V
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double DCOffset = 2;
            double Amp = 1;
            double frequency = 10;
            double[] sin = new double[Length];
            double[] cos = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length];
            Generation.Generation.SineWave(ref sin, Amp, 0, frequency, sampleRate);
            Generation.Generation.SineWave(ref cos, Amp, 90, frequency, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.05);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.Add(cos, noise, ref cos);
            ArrayCalculation.AddOffset(ref sin, DCOffset);
            ArrayCalculation.AddOffset(ref cos, DCOffset);
            var signal = new Complex[Length];
            for (int i = 0; i < Length; i++)
            {
                signal[i] = new Complex(cos[i], sin[i]);
            }

            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Complex;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.Exact_Blackman ;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
           // _task.Commit();
            _task.GetSpectrum(signal, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            // VISN_Plot.PlotData("Spectrum3", fStart, df, spec);
            double f0Value = Math.Sqrt(Math.Pow(DCOffset, 2) * 2);
            double f10Value = 1 / Math.Sqrt(Math.Pow(Amp, 2));
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, f10Value / 1.1, -15, 150);
            Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.01);//直流分量的幅值
            Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.01); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.04);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_ComplexFFT_005()
        {
            /**********************************
             * 产生复数信号，频率为10Hz，直流分量为2V，幅值1Vpp
             * 复数FFT，Flat_Top窗，不平均，输出频谱单位为V
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double DCOffset = 2;
            double Amp = 1;
            double frequency = 10;
            double[] sin = new double[Length];
            double[] cos = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length];
            Generation.Generation.SineWave(ref sin, Amp, 0, frequency, sampleRate);
            Generation.Generation.SineWave(ref cos, Amp, 90, frequency, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.05);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.Add(cos, noise, ref cos);
            ArrayCalculation.AddOffset(ref sin, DCOffset);
            ArrayCalculation.AddOffset(ref cos, DCOffset); ;
            var signal = new Complex[Length];
            for (int i = 0; i < Length; i++)
            {
                signal[i] = new Complex(cos[i], sin[i]);
            }

            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Complex;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.Flat_Top_90D;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
           // _task.Commit();
            _task.GetSpectrum(signal, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            // VISN_Plot.PlotData("Spectrum3", fStart, df, spec);
            double f0Value = Math.Sqrt(Math.Pow(DCOffset, 2) * 2);
            double f10Value = 1 / Math.Sqrt(Math.Pow(Amp, 2));
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, f10Value / 1.1, -15, 150);
            Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.04);//直流分量的幅值
            Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.04); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.04);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_ComplexFFT_006()
        {
            /**********************************
             * 产生复数信号，频率为10Hz，直流分量为2V，幅值1Vpp
             * 复数FFT，Four_Term_B_Harris窗，不平均，输出频谱单位为V
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double DCOffset = 2;
            double Amp = 1;
            double frequency = 10;
            double[] sin = new double[Length];
            double[] cos = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length];
            Generation.Generation.SineWave(ref sin, Amp, 0, frequency, sampleRate);
            Generation.Generation.SineWave(ref cos, Amp, 90, frequency, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.05);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.Add(cos, noise, ref cos);
            ArrayCalculation.AddOffset(ref sin, DCOffset);
            ArrayCalculation.AddOffset(ref cos, DCOffset);
            var signal = new Complex[Length];
            for (int i = 0; i < Length; i++)
            {
                signal[i] = new Complex(cos[i], sin[i]);
            }

            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Complex;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.Four_Term_B_Harris;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
          //  _task.Commit();
            _task.GetSpectrum(signal, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            // VISN_Plot.PlotData("Spectrum3", fStart, df, spec);
            double f0Value = Math.Sqrt(Math.Pow(DCOffset, 2) * 2);
            double f10Value = 1 / Math.Sqrt(Math.Pow(Amp, 2));
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, f10Value / 1.1, -15, 150);
            Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.04);//直流分量的幅值
            Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.04); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.05);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_ComplexFFT_007()
        {
            /**********************************
             * 产生复数信号，频率为10Hz，直流分量为2V，幅值1Vpp
             * 复数FFT，Hamming窗，不平均，输出频谱单位为V
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double DCOffset = 2;
            double Amp = 1;
            double frequency = 10;
            double[] sin = new double[Length];
            double[] cos = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length];
            Generation.Generation.SineWave(ref sin, Amp, 0, frequency, sampleRate);
            Generation.Generation.SineWave(ref cos, Amp, 90, frequency, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.05);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.Add(cos, noise, ref cos);
            ArrayCalculation.AddOffset(ref sin, DCOffset);
            ArrayCalculation.AddOffset(ref cos, DCOffset); ;
            var signal = new Complex[Length];
            for (int i = 0; i < Length; i++)
            {
                signal[i] = new Complex(cos[i], sin[i]);
            }

            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Complex;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.Hamming;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
           // _task.Commit();
            _task.GetSpectrum(signal, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            // VISN_Plot.PlotData("Spectrum3", fStart, df, spec);
            double f0Value = Math.Sqrt(Math.Pow(DCOffset, 2) * 2);
            double f10Value = 1 / Math.Sqrt(Math.Pow(Amp, 2));
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, f10Value / 1.1, -15, 150);
            Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.04);//直流分量的幅值
            Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.04); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.05);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_ComplexFFT_008()
        {
            /**********************************
             * 产生复数信号，频率为10Hz，直流分量为2V，幅值1Vpp
             * 复数FFT，Hamming窗，不平均，输出频谱单位为V
             * 验证峰值的有效值
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double DCOffset = 2;
            double Amp = 1;
            double frequency = 10;
            double[] sin = new double[Length];
            double[] cos = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length];
            Generation.Generation.SineWave(ref sin, Amp, 0, frequency, sampleRate);
            Generation.Generation.SineWave(ref cos, Amp, 90, frequency, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.05);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.Add(cos, noise, ref cos);
            ArrayCalculation.AddOffset(ref sin, DCOffset);
            ArrayCalculation.AddOffset(ref cos, DCOffset);
            var signal = new Complex[Length];
            for (int i = 0; i < Length; i++)
            {
                signal[i] = new Complex(cos[i], sin[i]);
            }

            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Complex;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.Hanning;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
            //_task.Commit();
            _task.GetSpectrum(signal, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            // VISN_Plot.PlotData("Spectrum3", fStart, df, spec);
            double f0Value = Math.Sqrt(Math.Pow(DCOffset, 2) * 2);
            double f10Value = 1 / Math.Sqrt(Math.Pow(Amp, 2));
            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, f10Value / 1.1, -15, 150);
            Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.04);//直流分量的幅值
            Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.04); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.05);//正弦信号的频率
        }

        [TestMethod]
        public void GeneralSpectrumTask_ComplexFFT_FindPeak()
        {
            /**********************************
             * 产生复数信号，频率为10MHz，直流分量为0V，幅值0.316Vpp
             * 复数FFT，Hamming窗，不平均，输出频谱单位为V
             * 验证峰值的有效值
             * ********************************/
            int Length = 4096;
            double sampleRate = 800*1e6;
            double DCOffset = 0;
            double Amp = 0.316;
            double frequency = 10*1e6;
            double[] sin = new double[Length];
            double[] cos = new double[Length];
            double[] noise = new double[Length];
            double[] spec = new double[Length];
            Generation.Generation.SineWave(ref sin, Amp, 0, frequency, sampleRate);
            Generation.Generation.SineWave(ref cos, Amp, 90, frequency, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.01);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.Add(cos, noise, ref cos);
            ArrayCalculation.AddOffset(ref sin, DCOffset);
            ArrayCalculation.AddOffset(ref cos, DCOffset);
            var signal = new Complex[Length];
            for (int i = 0; i < Length; i++)
            {
                signal[i] = new Complex(cos[i], sin[i]);
            }

            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Complex;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.Hanning;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = Length;
            _task.Unit.Type = SpectrumOutputUnit.dBm;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
            //_task.Commit();
            _task.GetSpectrum(signal, ref spec);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            // VISN_Plot.PlotData("Spectrum3", fStart, df, spec);
            double f0Value = Math.Sqrt(Math.Pow(DCOffset, 2) * 2);
            double f10Value = 1 / Math.Sqrt(Math.Pow(Amp, 2));
            var b = _task.FindPeak(spec);

            Peak[] a = new Peak[100];
            a = _task.FindPeak(spec, b.PeakValue*0.5);
            Assert.IsTrue(Math.Abs((f0Value - a[0].PeakValue) / f0Value) < 0.04);//直流分量的幅值
            Assert.IsTrue(Math.Abs((f10Value - a[1].PeakValue) / f10Value) < 0.04); //正弦信号的有效值
            Assert.IsTrue(Math.Abs((10 - a[1].PeakFrequency) / 10) < 0.05);//正弦信号的频率
        }


        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_PowerInBand()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，不加窗，不平均
             * 比较不同单位以及功率谱 测量出来的功率
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] noise = new double[Length];
            double[] spec1 = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
            _task.GetSpectrum(sin, ref spec1);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            var powerInband1 = _task.MeasurePowerInBand(spec1, _task.SampleRate / 4, _task.SampleRate / 2);
            Console.WriteLine("V :{0}", powerInband1.ToString());

            double[] spec2= new double[Length / 2];
            _task.Unit.Type = SpectrumOutputUnit.V2;
            _task.GetSpectrum(sin, ref spec2);
            var powerInband = spec2[0] + spec2[(int)(10 / df)];
            var powerInband2 = _task.MeasurePowerInBand(spec2, _task.SampleRate / 4, _task.SampleRate / 2);
            Console.WriteLine("V2 :{0}", powerInband2.ToString());
            Assert.IsTrue(Math.Abs(powerInband - powerInband2) / powerInband < 0.01);

            double[] spec3 = new double[Length / 2];
            _task.Unit.Type = SpectrumOutputUnit.dBmV;
            _task.GetSpectrum(sin, ref spec3);
            var powerInband3 = _task.MeasurePowerInBand(spec3, _task.SampleRate / 4, _task.SampleRate / 2);
            Console.WriteLine("dBmV :{0}", powerInband3.ToString());
            Assert.IsTrue(Math.Abs(20*Math.Log10(Math.Sqrt(powerInband)*1000) - powerInband3) / powerInband < 0.01);

            double[] spec4= new double[Length / 2];
            _task.Unit.Type = SpectrumOutputUnit.dBuV;
            _task.GetSpectrum(sin, ref spec4);
            var powerInband4= _task.MeasurePowerInBand(spec4, _task.SampleRate / 4, _task.SampleRate / 2);
            Console.WriteLine("dBuV :{0}", powerInband4.ToString());
            Assert.IsTrue(Math.Abs(20 * Math.Log10(Math.Sqrt(powerInband) * 1000000) - powerInband4) / powerInband < 0.01);

            double[] spec5 = new double[Length / 2];
            _task.Unit.Type = SpectrumOutputUnit.W;
            _task.GetSpectrum(sin, ref spec5);
            var powerInband5 = _task.MeasurePowerInBand(spec5, _task.SampleRate / 4, _task.SampleRate / 2);
            Console.WriteLine("W :{0}", powerInband5.ToString());
            Assert.IsTrue(Math.Abs(powerInband/_task.Unit.Impedance - powerInband5) / powerInband < 0.01);

            double[] spec6 = new double[Length / 2];
            _task.Unit.Type = SpectrumOutputUnit.dBW;
            _task.GetSpectrum(sin, ref spec6);
            var powerInband6 = _task.MeasurePowerInBand(spec6, _task.SampleRate / 4, _task.SampleRate / 2);
            Console.WriteLine("dBW :{0}", powerInband6.ToString());
            Assert.IsTrue(Math.Abs(10 * Math.Log10(powerInband / _task.Unit.Impedance) - powerInband6) / powerInband < 0.01);

            double[] spec7 = new double[Length / 2];
            _task.Unit.Type = SpectrumOutputUnit.dBm;
            _task.GetSpectrum(sin, ref spec7);
            var powerInband7 = _task.MeasurePowerInBand(spec7, _task.SampleRate / 4, _task.SampleRate / 2);
            Console.WriteLine("dBm :{0}", powerInband7.ToString());
            Assert.IsTrue(Math.Abs(10 * Math.Log10(powerInband / _task.Unit.Impedance*1000) - powerInband7) / powerInband < 0.01);

            double[] spec8 = new double[Length / 2];
            _task.Unit.IsPSD = true;
            _task.Unit.Type = SpectrumOutputUnit.V2;
            _task.GetSpectrum(sin, ref spec8);
            var powerInband8 = _task.MeasurePowerInBand(spec8, _task.SampleRate / 4, _task.SampleRate / 2);
            Console.WriteLine("IsPSD :{0}", powerInband8.ToString());
            Assert.IsTrue(Math.Abs(powerInband - powerInband8) / powerInband < 0.01);
        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_PowerInBand_002()
        {
            /**********************************
             * 产生有2V的直流分量10Hz，1Vpp的正弦波，并加上噪声
             * 实数FFT，不加窗，不平均
             * 测量各个分量的功率
             * ********************************/
            int Length = 1000;
            double sampleRate = 1000;
            double[] sin = new double[Length];
            double[] sin1 = new double[Length];
            double[] noise = new double[Length];
            double[] spec1 = new double[Length / 2];
            Generation.Generation.SineWave(ref sin, 1, 0, 10, sampleRate);
            Generation.Generation.SineWave(ref sin1, 1, 0, 50, sampleRate);
            Generation.Generation.UniformWhiteNoise(ref noise, 0.1);
            ArrayCalculation.Add(sin, noise, ref sin);
            ArrayCalculation.AddOffset(ref sin, 2);
            ArrayCalculation.Add(sin, sin1, ref sin);
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Output.NumberOfLines = Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.V2;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
            _task.GetSpectrum(sin, ref spec1);
            double df = _task.SpectralInfomation.FreqDelta;
            double count = _task.SpectralInfomation.FFTCount;
            double fftSize = _task.SpectralInfomation.FFTSize;
            double fStart = _task.SpectralInfomation.FreqStart;
            var powerInband1 = _task.MeasurePowerInBand(spec1, _task.SampleRate / 4, _task.SampleRate / 2);
            Console.WriteLine("all band :{0}", powerInband1.ToString());

            var powerInband2 = _task.MeasurePowerInBand(spec1, 4,8);
            Console.WriteLine("DC :{0}", powerInband2.ToString());
            Assert.IsTrue(Math.Abs(powerInband2 - spec1[0]) / spec1[0] < 0.01);

            var powerInband3 = _task.MeasurePowerInBand(spec1, 10, 4);
            Console.WriteLine("10 :{0}", powerInband3.ToString());
            Assert.IsTrue(Math.Abs(powerInband3 - spec1[10]) / spec1[0] < 0.01);

            var powerInband4 = _task.MeasurePowerInBand(spec1, 50, 4);
            Console.WriteLine("50 :{0}", powerInband4.ToString());
            Assert.IsTrue(Math.Abs(powerInband4 - spec1[50]) / spec1[0] < 0.01);
        }

        [TestMethod]
        public void GeneralSpectrumTask_RealFFT_003_PowerSpectrum()
        {
            string outputPath = "";
            //load waveform
            double[] signal = ReadTestCSV(@"Square_Noise001.csv");
            double sampleRate = 51200;
            double dt = 1 / sampleRate;
            int sianglLength = signal.Length;
            double[] spec1 = new double[sianglLength / 2];
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = sampleRate;
            _task.WindowType = WindowType.Hanning;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Output.NumberOfLines = sianglLength / 2;
            _task.Unit.Type = SpectrumOutputUnit.V2;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;
            _task.GetSpectrum(signal, ref spec1);

            outputPath = GetOutputPath(@"Sqr8192Hanning.csv");
            CsvHandler.WriteData<double>(outputPath, spec1, WriteMode.OverLap);
            Assert.IsTrue(true);
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

        private string GetOutputPath(string fileName)
        {
            string basefolder = this.GetType().Assembly.Location;
            basefolder = basefolder.Substring(0, basefolder.LastIndexOf(@"\"));
            string filepath = basefolder + @"\Data\" + fileName;
            return filepath;
        }
    }

    //[TestClass]
    //public class WindowFunctionUnitTest
    //{
    //    [TestMethod]
    //    public void GeneralSpectrumTask_WindowFunction_Hanning()
    //    {
    //        /**********************************
    //         * 使用统一的窗生成函数生成各个窗的数据
    //         * ********************************/
    //        int length = 10;
    //        double[] windowCoe = new double[5];
    //        var window = WindowType.Hanning;
    //        double[] windowdata = new double[length];
    //        double[] windowdata2 = new double[length + 1];
    //        double CG1 = 0;
    //        double ENBW1 = 0;
    //        double CG2 = 0;
    //        double ENBW2 = 0;
    //        Window.GetWindow(window, ref windowdata, out CG1, out ENBW1, Window.WindowProperty.SymmetricWindow);
    //        Window.GetWindow(window, ref windowdata2, out CG1, out ENBW1);
    //        Get_ENBW_And_CG(window, out ENBW2, out CG2);
    //        var error = Math.Abs(CG2 - CG1) / CG1;
    //        Assert.IsTrue(error < 0.001);
    //        error = Math.Abs(ENBW2 - ENBW1) / ENBW1;
    //        Assert.IsTrue(error < 0.001);
    //        ////AnalogWaveformChart.Plot("Hanning1", windowdata);
    //    }

    //    private void Get_ENBW_And_CG(WindowType window, out double ENBW, out double CG)
    //    {
    //        ENBW = 0;
    //        CG = 0;
    //        switch (window)
    //        {
    //            //Hanning
    //            case WindowType.Hanning:
    //                ENBW = 1.5;
    //                CG = 0.5;
    //                break;
    //            //hamming
    //            case WindowType.Hamming:
    //                ENBW = 1.36283;
    //                CG = 0.54;
    //                break;
    //            //Blackman_Harris
    //            case WindowType.Blackman_Harris:
    //                ENBW = 1.708538;
    //                CG = 0.42323;
    //                break;
    //            //Exact_Blackman
    //            case WindowType.Exact_Blackman:
    //                ENBW = 1.69369;
    //                CG = 0.42659;
    //                break;
    //            //Blackman
    //            case WindowType.Blackman:
    //                CG = 0.42;
    //                ENBW = 1.72676;
    //                break;
    //            //Flat_Top
    //            //case WindowType.Flat_Top:
    //            //    ENBW = 3.77;
    //            //    CG = 0.22;
    //            //    break;
    //            //Four_Term_B_Harris
    //            case WindowType.Four_Term_B_Harris:
    //                ENBW = 2.00435;
    //                CG = 0.35875;
    //                break;
    //            //Seven_Term_B_Harris
    //            //case WindowType.Seven_Term_B_Harris:
    //            //    ENBW = 2.63191;
    //            //    CG = 0.27105;
    //            //    break;
    //            case WindowType.Four_Term_Nuttal:
    //                ENBW = 2.0212;
    //                CG = 0.355768;
    //                break;
    //            case WindowType.Four_Term_Blackman_Nuttal:
    //                ENBW = 1.97611;
    //                CG = 3.635819267707608e-001;
    //                break;
    //            case WindowType.Five_Term_Least_Sidelobe:
    //                ENBW = 2.21535;
    //                CG = 3.232153788877343e-001;
    //                break;
    //            case WindowType.Six_Term_Least_Sidelobe:
    //                ENBW = 2.43390;
    //                CG = 2.935578950102797e-001;
    //                break;
    //            case WindowType.Three_Term_Nuttal:
    //                ENBW = 1.7721;
    //                CG = 0.40897;
    //                break;
    //            case WindowType.Three_Term_Blackman_Nuttal:
    //                ENBW = 1.70371;
    //                CG = 4.243800934609435e-001;
    //                break;
    //            case WindowType.Seven_Term_Least_Sidelobe:
    //                ENBW = 2.63025;
    //                CG = 2.712203605850388e-001;
    //                break;
    //            //Flat_Top_90D
    //            case WindowType.Flat_Top_90D:
    //                ENBW = 3.8832;
    //                CG = 0.209783021421;
    //                break;
    //            case WindowType.Flat_Top_95:
    //                ENBW = 3.8112;
    //                CG = 0.213640903311;
    //                break;
    //            case WindowType.Six_Term_Flat_Top:
    //                ENBW = 4.2186;
    //                CG = 0.192240452512;
    //                break;
    //            case WindowType.Seven_Term_Flat_Top:
    //                ENBW = 4.5386;
    //                CG = 0.178153071078;
    //                break;
    //        }
    //    }

    //    [TestMethod]
    //    public void GeneralSpectrumTask_WindowFunction_Hamming()
    //    {
    //        /**********************************
    //         * 使用统一的窗生成函数生成各个窗的数据
    //         * ********************************/
    //        int length = 11;
    //        var window = WindowType.Hamming;
    //        double[] windowdata = new double[length];
    //        double CG1 = 0;
    //        double ENBW1 = 0;
    //        double CG2 = 0;
    //        double ENBW2 = 0;
    //        Window.GetWindow(window, ref windowdata, out CG1, out ENBW1);
    //        Get_ENBW_And_CG(window, out ENBW2, out CG2);
    //        var error = Math.Abs(CG2 - CG1) / CG1;
    //        Assert.IsTrue(error < 0.001);
    //        error = Math.Abs(ENBW2 - ENBW1) / ENBW1;
    //        Assert.IsTrue(error < 0.001);
    //    }

    //    [TestMethod]
    //    public void GeneralSpectrumTask_WindowFunction_Blackman()
    //    {
    //        /**********************************
    //         * 使用统一的窗生成函数生成各个窗的数据
    //         * ********************************/
    //        int length = 1000;
    //        var window = WindowType.Blackman;
    //        double[] windowdata = new double[length];
    //        double CG1 = 0;
    //        double ENBW1 = 0;
    //        double CG2 = 0;
    //        double ENBW2 = 0;
    //        Window.GetWindow(window, ref windowdata, out CG1, out ENBW1);
    //        Get_ENBW_And_CG(window, out ENBW2, out CG2);
    //        var error = Math.Abs(CG2 - CG1) / CG1;
    //        Assert.IsTrue(error < 0.001);
    //        error = Math.Abs(ENBW2 - ENBW1) / ENBW1;
    //        Assert.IsTrue(error < 0.001);
    //    }

    //    [TestMethod]
    //    public void GeneralSpectrumTask_WindowFunction_Blackman_Harris()
    //    {
    //        /**********************************
    //         * 使用统一的窗生成函数生成各个窗的数据
    //         * ********************************/
    //        int length = 1000;
    //        var window = WindowType.Blackman_Harris;
    //        double[] windowdata = new double[length];
    //        double CG1 = 0;
    //        double ENBW1 = 0;
    //        double CG2 = 0;
    //        double ENBW2 = 0;
    //        Window.GetWindow(window, ref windowdata, out CG1, out ENBW1);
    //        Get_ENBW_And_CG(window, out ENBW2, out CG2);
    //        var error = Math.Abs(CG2 - CG1) / CG1;
    //        Assert.IsTrue(error < 0.001);
    //        error = Math.Abs(ENBW2 - ENBW1) / ENBW1;
    //        Assert.IsTrue(error < 0.001);
    //    }

    //    [TestMethod]
    //    public void GeneralSpectrumTask_WindowFunction_Exact_Blackman()
    //    {
    //        /**********************************
    //         * 使用统一的窗生成函数生成各个窗的数据
    //         * ********************************/
    //        int length = 1000;
    //        var window = WindowType.Exact_Blackman;
    //        double[] windowdata = new double[length];
    //        double CG1 = 0;
    //        double ENBW1 = 0;
    //        double CG2 = 0;
    //        double ENBW2 = 0;
    //        Window.GetWindow(window, ref windowdata, out CG1, out ENBW1);
    //        Get_ENBW_And_CG(window, out ENBW2, out CG2);
    //        var error = Math.Abs(CG2 - CG1) / CG1;
    //        Assert.IsTrue(error < 0.001);
    //        error = Math.Abs(ENBW2 - ENBW1) / ENBW1;
    //        Assert.IsTrue(error < 0.001);
    //    }

    //    [TestMethod]
    //    public void GeneralSpectrumTask_WindowFunction_Flat_Top_90D()
    //    {
    //        /**********************************
    //         * 使用统一的窗生成函数生成各个窗的数据
    //         * ********************************/
    //        int length = 1000;
    //        var window = WindowType.Flat_Top_90D;
    //        double[] windowdata = new double[length];
    //        double CG1 = 0;
    //        double ENBW1 = 0;
    //        double CG2 = 0;
    //        double ENBW2 = 0;
    //        Window.GetWindow(window, ref windowdata, out CG1, out ENBW1);
    //        Get_ENBW_And_CG(window, out ENBW2, out CG2);
    //        var error = Math.Abs(CG2 - CG1) / CG1;
    //        Assert.IsTrue(error < 0.001);
    //        error = Math.Abs(ENBW2 - ENBW1) / ENBW1;
    //        Assert.IsTrue(error < 0.001);
    //    }

    //    [TestMethod]
    //    public void GeneralSpectrumTask_WindowFunction_Flat_Top_95()
    //    {
    //        /**********************************
    //         * 使用统一的窗生成函数生成各个窗的数据
    //         * ********************************/
    //        int length = 1000;
    //        var window = WindowType.Flat_Top_95;
    //        double[] windowdata = new double[length];
    //        double CG1 = 0;
    //        double ENBW1 = 0;
    //        double CG2 = 0;
    //        double ENBW2 = 0;
    //        Window.GetWindow(window, ref windowdata, out CG1, out ENBW1);
    //        Get_ENBW_And_CG(window, out ENBW2, out CG2);
    //        var error = Math.Abs(CG2 - CG1) / CG1;
    //        Assert.IsTrue(error < 0.001);
    //        error = Math.Abs(ENBW2 - ENBW1) / ENBW1;
    //        Assert.IsTrue(error < 0.001);
    //    }

    //    [TestMethod]
    //    public void GeneralSpectrumTask_WindowFunction_Six_Term_Flat_Top()
    //    {
    //        /**********************************
    //         * 使用统一的窗生成函数生成各个窗的数据
    //         * ********************************/
    //        int length = 1000;
    //        var window = WindowType.Six_Term_Flat_Top;
    //        double[] windowdata = new double[length];
    //        double CG1 = 0;
    //        double ENBW1 = 0;
    //        double CG2 = 0;
    //        double ENBW2 = 0;
    //        Window.GetWindow(window, ref windowdata, out CG1, out ENBW1);
    //        Get_ENBW_And_CG(window, out ENBW2, out CG2);
    //        var error = Math.Abs(CG2 - CG1) / CG1;
    //        Assert.IsTrue(error < 0.001);
    //        error = Math.Abs(ENBW2 - ENBW1) / ENBW1;
    //        Assert.IsTrue(error < 0.001);
    //    }

    //    [TestMethod]
    //    public void GeneralSpectrumTask_WindowFunction_Seven_Term_Flat_Top()
    //    {
    //        /**********************************
    //         * 使用统一的窗生成函数生成各个窗的数据
    //         * ********************************/
    //        int length = 1000;
    //        var window = WindowType.Seven_Term_Flat_Top;
    //        double[] windowdata = new double[length];
    //        double CG1 = 0;
    //        double ENBW1 = 0;
    //        double CG2 = 0;
    //        double ENBW2 = 0;
    //        Window.GetWindow(window, ref windowdata, out CG1, out ENBW1);
    //        Get_ENBW_And_CG(window, out ENBW2, out CG2);
    //        var error = Math.Abs(CG2 - CG1) / CG1;
    //        Assert.IsTrue(error < 0.001);
    //        error = Math.Abs(ENBW2 - ENBW1) / ENBW1;
    //        Assert.IsTrue(error < 0.001);
    //    }

    //    [TestMethod]
    //    public void GeneralSpectrumTask_WindowFunction_Four_Term_B_Harris()
    //    {
    //        /**********************************
    //         * 使用统一的窗生成函数生成各个窗的数据
    //         * ********************************/
    //        int length = 1000;
    //        var window = WindowType.Four_Term_B_Harris;
    //        double[] windowdata = new double[length];
    //        double CG1 = 0;
    //        double ENBW1 = 0;
    //        double CG2 = 0;
    //        double ENBW2 = 0;
    //        Window.GetWindow(window, ref windowdata, out CG1, out ENBW1);
    //        Get_ENBW_And_CG(window, out ENBW2, out CG2);
    //        var error = Math.Abs(CG2 - CG1) / CG1;
    //        Assert.IsTrue(error < 0.001);
    //        error = Math.Abs(ENBW2 - ENBW1) / ENBW1;
    //        Assert.IsTrue(error < 0.001);
    //    }


    //    [TestMethod]
    //    public void GeneralSpectrumTask_WindowFunction_Four_Term_Blackman_Nuttal()
    //    {
    //        /**********************************
    //         * 使用统一的窗生成函数生成各个窗的数据
    //         * ********************************/
    //        int length = 1000;
    //        var window = WindowType.Four_Term_Blackman_Nuttal;
    //        double[] windowdata = new double[length];
    //        double CG1 = 0;
    //        double ENBW1 = 0;
    //        double CG2 = 0;
    //        double ENBW2 = 0;
    //        Window.GetWindow(window, ref windowdata, out CG1, out ENBW1);
    //        Get_ENBW_And_CG(window, out ENBW2, out CG2);
    //        var error = Math.Abs(CG2 - CG1) / CG1;
    //        Assert.IsTrue(error < 0.001);
    //        error = Math.Abs(ENBW2 - ENBW1) / ENBW1;
    //        Assert.IsTrue(error < 0.001);
    //    }

    //    [TestMethod]
    //    public void GeneralSpectrumTask_WindowFunction_Five_Term_least_Sidelobe()
    //    {
    //        /**********************************
    //         * 使用统一的窗生成函数生成各个窗的数据
    //         * ********************************/
    //        int length = 1000;
    //        var window = WindowType.Five_Term_Least_Sidelobe;
    //        double[] windowdata = new double[length];
    //        double CG1 = 0;
    //        double ENBW1 = 0;
    //        double CG2 = 0;
    //        double ENBW2 = 0;
    //        Window.GetWindow(window, ref windowdata, out CG1, out ENBW1);
    //        Get_ENBW_And_CG(window, out ENBW2, out CG2);
    //        var error = Math.Abs(CG2 - CG1) / CG1;
    //        Assert.IsTrue(error < 0.001);
    //        error = Math.Abs(ENBW2 - ENBW1) / ENBW1;
    //        Assert.IsTrue(error < 0.001);
    //    }

    //    [TestMethod]
    //    public void GeneralSpectrumTask_WindowFunction_Six_Term_least_Sidelobe()
    //    {
    //        /**********************************
    //         * 使用统一的窗生成函数生成各个窗的数据
    //         * ********************************/
    //        int length = 1000;
    //        var window = WindowType.Six_Term_Least_Sidelobe;
    //        double[] windowdata = new double[length];
    //        double CG1 = 0;
    //        double ENBW1 = 0;
    //        double CG2 = 0;
    //        double ENBW2 = 0;
    //        Window.GetWindow(window, ref windowdata, out CG1, out ENBW1);
    //        Get_ENBW_And_CG(window, out ENBW2, out CG2);
    //        var error = Math.Abs(CG2 - CG1) / CG1;
    //        Assert.IsTrue(error < 0.001);
    //        error = Math.Abs(ENBW2 - ENBW1) / ENBW1;
    //        Assert.IsTrue(error < 0.001);
    //    }

    //    [TestMethod]
    //    public void GeneralSpectrumTask_WindowFunction_Three_Term_Nuttal()
    //    {
    //        /**********************************
    //         * 使用统一的窗生成函数生成各个窗的数据
    //         * ********************************/
    //        int length = 1000;
    //        var window = WindowType.Three_Term_Nuttal;
    //        double[] windowdata = new double[length];
    //        double CG1 = 0;
    //        double ENBW1 = 0;
    //        double CG2 = 0;
    //        double ENBW2 = 0;
    //        Window.GetWindow(window, ref windowdata, out CG1, out ENBW1);
    //        Get_ENBW_And_CG(window, out ENBW2, out CG2);
    //        var error = Math.Abs(CG2 - CG1) / CG1;
    //        Assert.IsTrue(error < 0.001);
    //        error = Math.Abs(ENBW2 - ENBW1) / ENBW1;
    //        Assert.IsTrue(error < 0.001);
    //    }

    //    [TestMethod]
    //    public void GeneralSpectrumTask_WindowFunction_Three_Term_Blackman_Nuttal()
    //    {
    //        /**********************************
    //         * 使用统一的窗生成函数生成各个窗的数据
    //         * ********************************/
    //        int length = 1000;
    //        var window = WindowType.Three_Term_Blackman_Nuttal;
    //        double[] windowdata = new double[length];
    //        double CG1 = 0;
    //        double ENBW1 = 0;
    //        double CG2 = 0;
    //        double ENBW2 = 0;
    //        Window.GetWindow(window, ref windowdata, out CG1, out ENBW1);
    //        Get_ENBW_And_CG(window, out ENBW2, out CG2);
    //        var error = Math.Abs(CG2 - CG1) / CG1;
    //        Assert.IsTrue(error < 0.001);
    //        error = Math.Abs(ENBW2 - ENBW1) / ENBW1;
    //        Assert.IsTrue(error < 0.001); ;
    //    }

    //    [TestMethod]
    //    public void GeneralSpectrumTask_WindowFunction_Seven_Term_least_Sidelobe()
    //    {
    //        /**********************************
    //         * 使用统一的窗生成函数生成各个窗的数据
    //         * ********************************/
    //        int length = 1000;
    //        var window = WindowType.Seven_Term_Least_Sidelobe;
    //        double[] windowdata = new double[length];
    //        double CG1 = 0;
    //        double ENBW1 = 0;
    //        double CG2 = 0;
    //        double ENBW2 = 0;
    //        Window.GetWindow(window, ref windowdata, out CG1, out ENBW1);
    //        Get_ENBW_And_CG(window, out ENBW2, out CG2);
    //        var error = Math.Abs(CG2 - CG1) / CG1;
    //        Assert.IsTrue(error < 0.001);
    //        error = Math.Abs(ENBW2 - ENBW1) / ENBW1;
    //        Assert.IsTrue(error < 0.001);
    //    }

    //}
}
