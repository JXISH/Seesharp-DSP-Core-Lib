using System;
using System.Numerics;
using SeeSharpTools.JXI.Numerics;
using SeeSharpTools.JXI.Mathematics;

namespace SeeSharpTools.JXI.SignalProcessing.SpectrumAnalysis.RFSASpectrum
{
    public class SpectrumFFT
    {
        #region ---- Complex ----

        /// <summary>
        /// 计算功率谱，输出频谱信息（info）
        /// </summary>
        public static void ComputePowerSpectrum(Complex[] IQdata, ref double[] spectrum, SpectrumSettings settings, SpectralInfo info)
        {
            // 使用内部IQ数组操作，避免修改输入数据。
            Complex[] processIQ = new Complex[Math.Min(IQdata.Length, settings.WindowSize)];
            Vector.ArrayCopy(IQdata, 0, processIQ, 0, 0);

            // 判断频谱点数是否满足要求
            if (spectrum.Length < settings.SpectrumLines)
            {
                spectrum = new double[settings.SpectrumLines];
            }

            ComputePowerSpectrum(processIQ, spectrum, settings, info);
        }

        /// <summary>
        /// （obsolete）计算功率谱，输出频谱信息（info）
        /// 不检查IQ和spectrum数组大小，计算过程中会覆盖IQ原始数据
        /// </summary>
        public static void ComputePowerSpectrum(Complex[] IQdata, double[] spectrum, SpectrumSettings settings, SpectralInfo info)
        {
            // 时域加窗
            FFTWindow.ProcessWindow(IQdata, settings.WindowType);
            // 细调频率
            FineTune(IQdata, settings.SampleRate, settings.frequencyShift);
            // IQ折叠，频率合一
            Complex[] compressData = DataFold(IQdata, settings.FFTSize);
            // 计算频谱
            ComputeFFT(compressData, spectrum, settings.FFTSize, settings.StartIndex, settings.SpectrumLines);
            // 更新频谱信息 （单位V2）
            info.Generate(settings, SpectrumUnit.V2, false);
        }

        /// <summary>
        /// 计算频谱，输出频谱信息（info）
        /// </summary>
        public static void ComputeComplexSpectrum(Complex[] IQdata, ref Complex[] fftResult, SpectrumSettings settings, SpectralInfo info, int delay = 0)
        {
            // 使用内部IQ数组操作，避免修改输入数据。
            Complex[] processIQ = new Complex[Math.Min(IQdata.Length, settings.WindowSize)];
            Vector.ArrayCopy(IQdata, 0, processIQ, 0, 0);

            // 判断频谱点数是否满足要求
            if (fftResult.Length < settings.SpectrumLines)
            {
                fftResult = new Complex[settings.SpectrumLines];
            }

            // 时域加窗
            FFTWindow.ProcessWindow(processIQ, settings.WindowType);
            // 细调频率
            FineTune(processIQ, settings.SampleRate, settings.frequencyShift);
            // 时域延迟delay长度
            TimeDelay(processIQ, delay);
            // IQ折叠，频率合一
            Complex[] compressData = DataFold(processIQ, settings.FFTSize);
            // 计算频谱
            ComplexFFT(compressData, fftResult, settings.FFTSize, settings.StartIndex, settings.SpectrumLines);
            // 更新频谱信息 （单位V）
            info.Generate(settings, SpectrumUnit.V, false);
        }

        #endregion

        #region ---- Complex32 ----

        /// <summary>
        /// 计算功率谱，输出频谱信息（info）
        /// </summary>
        public static void ComputePowerSpectrum(Complex32[] IQdata, ref float[] spectrum, SpectrumSettings settings, SpectralInfo info)
        {
            // 使用内部IQ数组操作，避免修改输入数据。
            Complex32[] processIQ = new Complex32[Math.Min(IQdata.Length, settings.WindowSize)];
            Vector.ArrayCopy(IQdata, 0, processIQ, 0, 0);

            // 判断频谱点数是否满足要求
            if (spectrum.Length < settings.SpectrumLines)
            {
                spectrum = new float[settings.SpectrumLines];
            }

            ComputePowerSpectrum(processIQ, spectrum, settings, info);
        }

        /// <summary>
        /// （obsolete）计算功率谱，输出频谱信息（info）
        /// 不检查IQ和spectrum数组大小，计算过程中会覆盖IQ原始数据
        /// </summary>
        public static void ComputePowerSpectrum(Complex32[] IQdata, float[] spectrum, SpectrumSettings settings, SpectralInfo info)
        {
            // 时域加窗
            FFTWindow.ProcessWindow(IQdata, settings.WindowType);
            // 细调频率
            FineTune(IQdata, settings.SampleRate, settings.frequencyShift);
            // IQ折叠，频率合一
            Complex32[] compressData = DataFold(IQdata, settings.FFTSize);
            // 计算频谱
            ComputeFFT(compressData, spectrum, settings.FFTSize, settings.StartIndex, settings.SpectrumLines);
            // 更新频谱信息 （单位V2）
            info.Generate(settings, SpectrumUnit.V2, false);
        }

        /// <summary>
        /// 计算频谱，输出频谱信息（info）
        /// </summary>
        public static void ComputeComplexSpectrum(Complex32[] IQdata, ref Complex32[] fftResult, SpectrumSettings settings, SpectralInfo info, int delay = 0)
        {
            // 使用内部IQ数组操作，避免修改输入数据。
            Complex32[] processIQ = new Complex32[Math.Min(IQdata.Length, settings.WindowSize)];
            Vector.ArrayCopy(IQdata, 0, processIQ, 0, 0);

            // 判断频谱点数是否满足要求
            if (fftResult.Length < settings.SpectrumLines)
            {
                fftResult = new Complex32[settings.SpectrumLines];
            }

            // 时域加窗
            FFTWindow.ProcessWindow(processIQ, settings.WindowType);
            // 细调频率
            FineTune(processIQ, settings.SampleRate, settings.frequencyShift);
            // 时域延迟delay长度
            TimeDelay(processIQ, delay);
            // IQ折叠，频率合一
            Complex32[] compressData = DataFold(processIQ, settings.FFTSize);
            // 计算频谱
            ComplexFFT(compressData, fftResult, settings.FFTSize, settings.StartIndex, settings.SpectrumLines);
            // 更新频谱信息 （单位V）
            info.Generate(settings, SpectrumUnit.V, false);
        }

        #endregion

        #region ---- Private ----

        private static void FineTune<T>(T[] IQdata, double sampleRate, double frequencyShift)
        {
            if (frequencyShift != 0)   // 用时域乘法实现频域卷积，对齐 FFT bin。
            {
                double freqNorminal = frequencyShift / sampleRate;
                T[] shiftTone =  Vector.ToneInit<T>(IQdata.Length, freqNorminal);
                Vector.ArrayMulti(IQdata, shiftTone);
            }
        }

        private static void TimeDelay<T>(T[] IQdata, int delay)
        {
            if (delay != 0)   // 时域时延对应频域相移。
            {
                Vector.ArrayRotate(IQdata, -delay);
            }
        }

        private static T[] DataFold<T>(T[] IQdata, int fftSize)
        {
            if (IQdata.Length == fftSize) { return IQdata; }

            // IQ长度大于FFT点数，折叠IQ数组
            // IQ长度小于FFT点数，补零
            T[] tempIQData = new T[fftSize];
            T[] compressIQData = new T[fftSize];
            for (int i = 0; i < IQdata.Length; i += fftSize)
            {
                // IQ长度小于FFT点数, ArrayCopy保证只复制有效的IQ数据
                Vector.ArrayCopy(IQdata, i, tempIQData, 0, fftSize);
                Vector.ArrayAdd(compressIQData, tempIQData);
            }
            return compressIQData;
        }

        private static void ComputeFFT(Complex[] IQdata, double[] spectrum, int fftSize, int startIndex, int spectrumLines)
        {
            Transform.DFT.ComputeForward(IQdata);                               // 计算FFT
            double[] FFTData = Vector.GetComplexPower(IQdata);        // 计算Power Spectrum

            RotateFFTPoints(FFTData, spectrum, fftSize, startIndex, spectrumLines); // 获取相应的spectrum谱线
        }

        private static void ComputeFFT(Complex32[] IQdata, float[] spectrum, int fftSize, int startIndex, int spectrumLines)
        {
            Transform.DFT.ComputeForward(IQdata);                               // 计算FFT
            float[] FFTData =  Vector.GetComplexPower(IQdata);        // 计算Power Spectrum

            RotateFFTPoints(FFTData, spectrum, fftSize, startIndex, spectrumLines); // 获取相应的spectrum谱线
        }

        private static void ComplexFFT(Complex[] IQdata, Complex[] fftResult, int fftSize, int startIndex, int spectrumLines)
        {
            Transform.DFT.ComputeForward(IQdata);                     // 计算FFT
            RotateFFTPoints(IQdata, fftResult, fftSize, startIndex, spectrumLines); // 获取相应的spectrum谱线
        }

        private static void ComplexFFT(Complex32[] IQdata, Complex32[] fftResult, int fftSize, int startIndex, int spectrumLines)
        {
            Transform.DFT.ComputeForward(IQdata);                     // 计算FFT
            RotateFFTPoints(IQdata, fftResult, fftSize, startIndex, spectrumLines); // 获取相应的spectrum谱线
        }

        private static void RotateFFTPoints<T>(T[] FFTData, T[] spectrum, int fftSize, int startIndex, int spectrumLines)
        {
            int rightLength = fftSize - startIndex;         // startIndex右侧频谱点数
            int leftLength = spectrumLines - rightLength;   // 回绕到0频后还需要需要的频谱点数
            if (leftLength > 0)                             // 左侧频谱点数大于0，并且右侧点数小于需要的频谱点数，产生频谱回绕   
            {

                Vector.ArrayCopy(FFTData, 0, spectrum, rightLength, leftLength);    // 填充回绕部分数据。
            }
            else                                            // 左侧频谱点数小于0，没有回绕
            {
                rightLength = spectrumLines;                // 右侧频谱点数大于等于实际频谱点数，需重置
            }
            Vector.ArrayCopy(FFTData, startIndex, spectrum, 0, rightLength);  // 填充非回绕部分数据
        }

        #endregion
    }
}