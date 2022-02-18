using System;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using SeeSharpTools.JXI.MKL;
using SeeSharpTools.JXI.Exception;
using SeeSharpTools.JXI.SignalProcessing.Window;

namespace SeeSharpTools.JXI.SignalProcessing.GeneralSpectrum
{
    /// <summary>
    /// <para>Computes spectrum of _demoniator time-domain signal.</para>
    /// <para>Chinese Simplified: 计算信号的频谱。</para>
    /// </summary>
    internal static class Spectrum
    {
        #region -----------Private Var & Initializtion------------

        /// <summary>
        /// Maximiun Spectral Line Count
        /// </summary>
        private const int MaxSpectralLine = 65536;

        /// <summary>
        /// Sqrt 2
        /// </summary>
        private const double Sqrt2 = 1.4142135623730950488016887242097;

        #endregion

        #region ---------------Public: Power Spectrum-------------

        /// <summary>
        /// <para>Computes the power spectrum of input time-domain signal.</para>
        /// <para>Chinese Simplified: 计算输入信号的功率频谱。</para>
        /// </summary>
        /// <param name="x">
        /// <para>input time-domain signal.</para>
        /// <para>Chinese Simplified: 输入的时域波形。</para>
        /// </param>
        /// <param name="samplingRate">
        /// <para>sampling rate of the input time-domain signal, in samples per second.</para>
        /// <para>Chinese Simplified: 输入信号的采样率，以S/s为单位。</para>
        /// </param>
        /// <param name="spectrum">
        /// <para>output sequence containing the power spectrum.</para>
        /// <para>Chinese Simplified: 输出功率谱。</para>
        /// </param>
        /// <param name="df">
        /// <para>the frequency resolution of the spectrum,  in hertz.</para>
        /// <para>Chinese Simplified: 功率谱的频谱间隔，以Hz为单位。</para>
        /// </param>
        /// <param name="unit">
        /// <para>unit of the output power spectrum</para>
        /// <para>Chinese Simplified: 设置功率谱的单位。</para>
        /// </param>
        /// <param name="windowType">
        /// <para>the time-domain window to apply to the time signal.</para>
        /// <para>Chinese Simplified: 窗类型。</para>
        /// </param>
        /// <param name="windowPara">
        /// <para>parameter for _demoniator Kaiser/Gaussian/Dolph-Chebyshev window, If window is any other window, this parameter is ignored</para>
        /// <para>Chinese Simplified: 窗调整系数，仅用于Kaiser/Gaussian/Dolph-Chebyshev窗。</para>
        /// </param>
        /// <param name="PSD">
        /// <para>specifies whether the output power spectrum is converted to power spectral density.</para>
        /// <para>Chinese Simplified: 输出的频谱是否为功率谱密度。</para>
        /// </param>
        public static void PowerSpectrum(double[] x, double samplingRate, ref double[] spectrum, out double df,
            SpectrumOutputUnit unit = SpectrumOutputUnit.V2, WindowType windowType = WindowType.Hanning,
            double windowPara = double.NaN, bool PSD = false)
        {
            int spectralLines = spectrum.Length; //谱线数是输出数组的大小
            SpectralInfo spectralInfo = new SpectralInfo();
            AdvanceRealFFT(x, spectralLines, windowType, spectrum, ref spectralInfo);
            double scale = 1.0 / spectralInfo.FFTSize;
            CBLASNative.cblas_dscal(spectralLines, scale, spectrum, 1);
            df = 0.5 * samplingRate/ spectralInfo.spectralLines; //计算频率间隔
            //获取ENBW
            double[] windowdata = new double[x.Length];
            double CG = 0;
            double ENBW = 0;
            Window.Window.GetWindow(windowType, ref windowdata,out CG, out ENBW);
            //Unit Conversion
            UnitConvSetting unitSettings = new UnitConvSetting(unit, PeakScaling.Rms, 50.00, PSD);
            UnitConversion(spectrum, df, SpectrumType.Amplitude, unitSettings, ENBW);
        }

        /// <summary>
        /// <para>Computes the power spectrum of input time-domain signal.</para>
        /// <para>Chinese Simplified: 计算输入信号的功率频谱。</para>
        /// </summary>
        /// <param name="x">
        /// <para>input time-domain signal.</para>
        /// <para>Chinese Simplified: 输入的时域波形。</para>
        /// </param>
        /// <param name="samplingRate">
        /// <para>sampling rate of the input time-domain signal, in samples per second.</para>
        /// <para>Chinese Simplified: 输入信号的采样率，以S/s为单位。</para>
        /// </param>
        /// <param name="spectrum">
        /// <para>output sequence containing the power spectrum.</para>
        /// <para>Chinese Simplified: 输出功率谱。</para>
        /// </param>
        /// <param name="df">
        /// <para>the frequency resolution of the spectrum,  in hertz.</para>
        /// <para>Chinese Simplified: 功率谱的频谱间隔，以Hz为单位。</para>
        /// </param>
        /// <param name="unitSettings">
        /// <para>unit settings of the output power spectrum</para>
        /// <para>Chinese Simplified: 设置功率谱的单位。</para>
        /// </param>
        /// <param name="windowType">
        /// <para>the time-domain window to apply to the time signal.</para>
        /// <para>Chinese Simplified: 窗类型。</para>
        /// </param>
        /// <param name="windowPara">
        /// <para>parameter for _demoniator Kaiser/Gaussian/Dolph-Chebyshev window, If window is any other window, this parameter is ignored</para>
        /// <para>Chinese Simplified: 窗调整系数，仅用于Kaiser/Gaussian/Dolph-Chebyshev窗。</para>
        /// </param>
        public static void PowerSpectrum(double[] x, double samplingRate, ref double[] spectrum, out double df,
            UnitConvSetting unitSettings, WindowType windowType, double windowPara)
        {
            int spectralLines = spectrum.Length; //谱线数是输出数组的大小
            SpectralInfo spectralInfo = new SpectralInfo();
            AdvanceRealFFT(x, spectralLines, windowType, spectrum, ref spectralInfo);

            double scale = 1.0 / spectralInfo.FFTSize;
            CBLASNative.cblas_dscal(spectralLines, scale, spectrum, 1);

            df = 0.5 * samplingRate / spectralInfo.spectralLines; //计算频率间隔
            //获取ENBW
            double[] windowdata = new double[x.Length];
            double CG = 0;
            double ENBW = 0;
            Window.Window.GetWindow(windowType, ref windowdata, out CG, out ENBW);
            //Unit Conversion
            UnitConversion(spectrum, df, SpectrumType.Amplitude, unitSettings,ENBW);
        }
        #endregion

        #region ------------Public: PeakSpectrumAnalysis------------
        /// <summary>
        /// Get the fundamental frequency and array of harmonic power.
        /// </summary>
        /// <param name="timewaveform">the waveform of input signal assuming in voltage</param>
        /// <param name="dt">sampling interval of timewaveform (_status)</param>
        /// <param name="peakFreq">the _calculated peak tone frequency</param>
        /// <param name="peakAmp">the _calculated peak tone voltage peak amplitude, which is 1.414*RMS</param>
        /// i.e. peakSignal=peakAmp*sin(2*pi*peakFreq*t)
        public static void PeakSpectrumAnalysis(double[] timewaveform, double dt, out double peakFreq, out double peakAmp)
        {
            double[] spectrum = new double[timewaveform.Length / 2];
            double df;
            var spectUnit = SpectrumOutputUnit.V2; //this V^2 unit relates to power in band calculation, don't change
            var winType = WindowType.Hanning;  //relates to ENBW, must change in pair
            double ENBW = 1.5000; //ENBW for winType Hanning.ENBW = 1.500

            double approxFreq = -1;
            double searchRange = 0;

            double maxValue = 0;
            int maxValueIndex = 0;
            int i, approxFreqIndex, startIndex, endIndex;
            double powerInBand = 0;
            double powerMltIndex = 0;

            Spectrum.PowerSpectrum(timewaveform, 1 / dt, ref spectrum, out df, spectUnit, winType);
            if (approxFreq < 0)
            {
                startIndex = 0;
                endIndex = spectrum.Length;
            }
            else
            {
                approxFreqIndex = (int)(approxFreq / df);
                endIndex = (int)(searchRange / 200 / dt); // start earch with approx. Freq - 1/2 range
                startIndex = approxFreqIndex - endIndex;  //start search index
                endIndex = (int)(searchRange / 100 / dt); //search length in indexes
                if (startIndex < 0) startIndex = 0;  //start index protection
                if (startIndex > spectrum.Length - 2) startIndex = spectrum.Length - 2;  //start index protection
                if (endIndex < 1) endIndex = 1; //protect search range;
            };
            //Search spectrum from [i1] to i1+i2-1;
            maxValue = -1; //power spectrum can not be less than 0;

            maxValue = spectrum.Max();
            maxValueIndex = Array.FindIndex<double>(spectrum, s => s == maxValue);

            startIndex = maxValueIndex - 3;
            if (startIndex < 0) startIndex = 0;

            endIndex = startIndex + 7;

            if (endIndex > spectrum.Length - 1) endIndex = spectrum.Length - 1;

            for (i = startIndex; i < endIndex; i++)
            {
                powerInBand += spectrum[i];
                powerMltIndex += spectrum[i] * i;
            }
            peakFreq = powerMltIndex / powerInBand * df;     //Given the estimated frequency and power, the exact frequency can be _calculated
            peakAmp = Math.Sqrt(powerInBand / ENBW * 2); //convert V^2 to V peak amplitude                        //Refer this formula to  ITU Handbook
        }

        #endregion

        #region ---------------Internal: UnitConversion------------

        /// <summary>
        /// 频谱单位转换函数
        /// </summary>
        /// <param name="spectrum">输入频谱，单位转换后返回的序列也保存在里面</param>
        /// <param name="spectrumType">输入频谱类型，功率谱或者幅度谱</param>
        /// <param name="df">频谱间隔</param>
        /// <param name="unitSetting">单位转换设置</param>
        /// <param name="equivalentNoiseBw">计算频谱时，加窗所用窗函数的等效噪声带宽</param>
        /// <returns></returns>
        internal static void UnitConversion(double[] spectrum, double df, SpectrumType spectrumType,
            UnitConvSetting unitSetting, double equivalentNoiseBw)
        {
            double scale = 1.0;
            int freq0Idx = 0, N = spectrum.Length;
            //VMLNative.vdSqr(N, spectrum, spectrum);

            if (unitSetting.PeakScaling == PeakScaling.Peak) //峰峰值要乘以2
            {
                switch (spectrumType)
                {
                    case SpectrumType.Amplitude: // Sqrt2
                        scale *= Sqrt2;
                        break;
                    case SpectrumType.Power: // 2
                        scale *= 2;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(spectrumType), spectrumType, null);
                }
                CBLASNative.cblas_dscal(N, scale, spectrum, 1);
                spectrum[0] /= scale; //零频不用
            }

            //根据设置的转换单位进行转换
            switch (unitSetting.Unit)
            {
                case SpectrumOutputUnit.V:
                    {
                        if (SpectrumType.Power == spectrumType)
                        {
                            VMLNative.vdSqrt(N, spectrum, spectrum);
                        }
                        if (unitSetting.PSD)//功率谱
                        {
                            //计算V2，平方
                            VMLNative.vdSqr(N, spectrum, spectrum);
                            //谱密度计算
                            scale = 1.0 / (equivalentNoiseBw * df);
                            CBLASNative.cblas_dscal(N, scale, spectrum, 1);
                            //计算V，开方
                            VMLNative.vdSqrt(N, spectrum, spectrum);

                        }
                        break;
                    }
                case SpectrumOutputUnit.dBV:
                    {
                        if (SpectrumType.Power == spectrumType)
                        {
                            VMLNative.vdSqrt(N, spectrum, spectrum);
                        }
                        if (unitSetting.PSD)//功率谱
                        {
                            //计算V2，平方
                            VMLNative.vdSqr(N, spectrum, spectrum);
                            //谱密度计算
                            scale = 1.0 / (equivalentNoiseBw * df);
                            CBLASNative.cblas_dscal(N, scale, spectrum, 1);
                            //计算V，开方
                            VMLNative.vdSqrt(N, spectrum, spectrum);
                        }
                        //lg
                        VMLNative.vdLog10(N, spectrum, spectrum);
                        scale = 20;
                        //20*lg
                        CBLASNative.cblas_dscal(N, scale, spectrum, 1);

                        break;
                    }
                case SpectrumOutputUnit.dBmV:
                    {
                        if (SpectrumType.Power == spectrumType)
                        {
                            VMLNative.vdSqrt(N, spectrum, spectrum);
                        }
                        if (unitSetting.PSD)//功率谱
                        {
                            //计算V2，平方
                            VMLNative.vdSqr(N, spectrum, spectrum);
                            //谱密度计算
                            scale = 1.0 / (equivalentNoiseBw * df);
                            CBLASNative.cblas_dscal(N, scale, spectrum, 1);
                            //计算V，开方
                            VMLNative.vdSqrt(N, spectrum, spectrum);
                        }
                        CBLASNative.cblas_dscal(N, 1e3, spectrum, 1);  //V To mV                
                        VMLNative.vdLog10(N, spectrum, spectrum);    //To Lg
                        scale = 20;

                        CBLASNative.cblas_dscal(N, scale, spectrum, 1); //To 20*Lg
                        break;
                    }
                case SpectrumOutputUnit.dBuV:
                    {
                        if (SpectrumType.Power == spectrumType)
                        {
                            VMLNative.vdSqrt(N, spectrum, spectrum);
                        }
                        if (unitSetting.PSD)//功率谱
                        {
                            //计算V2，平方
                            VMLNative.vdSqr(N, spectrum, spectrum);
                            //谱密度计算
                            scale = 1.0 / (equivalentNoiseBw * df);
                            CBLASNative.cblas_dscal(N, scale, spectrum, 1);
                            //计算V，开方
                            VMLNative.vdSqrt(N, spectrum, spectrum);
                        }
                        CBLASNative.cblas_dscal(N, 1e6, spectrum, 1); //V To uV                
                        VMLNative.vdLog10(N, spectrum, spectrum);//To Lg
                        scale = 20;
                        CBLASNative.cblas_dscal(N, scale, spectrum, 1);//To 20*Lg
                        break;
                    }
                case SpectrumOutputUnit.V2:
                    {
                        if (SpectrumType.Amplitude == spectrumType)
                        {
                            //计算V2，平方
                            VMLNative.vdSqr(N, spectrum, spectrum);
                            if (unitSetting.PSD)//功率谱
                            {
                                //谱密度计算
                                scale = 1.0 / (equivalentNoiseBw * df);
                                CBLASNative.cblas_dscal(N, scale, spectrum, 1);
                            }
                        }
                        break;
                    }
                case SpectrumOutputUnit.W:
                case SpectrumOutputUnit.dBW:
                case SpectrumOutputUnit.dBm:
                    {
                        if (SpectrumType.Amplitude == spectrumType)
                        {
                            //计算V2，平方
                            VMLNative.vdSqr(N, spectrum, spectrum);
                            if (unitSetting.PSD)//功率谱
                            {
                                //谱密度计算
                                scale = 1.0 / (equivalentNoiseBw * df);
                                CBLASNative.cblas_dscal(N, scale, spectrum, 1);
                            }
                        }
                        scale = 1.0 / unitSetting.Impedance;            //1/R
                        //VMLNative.vdSqr(N, spectrum, spectrum);         //V^2
                        CBLASNative.cblas_dscal(N, scale, spectrum, 1); //W = V^2/R

                        if (unitSetting.Unit == SpectrumOutputUnit.dBW)   //dBW = 20lgW
                        {
                            //lg
                            VMLNative.vdLog10(N, spectrum, spectrum);
                            scale = 10;
                            //10*lg
                            CBLASNative.cblas_dscal(N, scale, spectrum, 1);
                        }
                        else if (unitSetting.Unit == SpectrumOutputUnit.dBm) // dBm = 10lg(W/1mW)
                        {
                            CBLASNative.cblas_dscal(N, 1e3, spectrum, 1); // W/1mW
                                                              //lg
                            VMLNative.vdLog10(N, spectrum, spectrum);
                            scale = 10;
                            //10*lg
                            CBLASNative.cblas_dscal(N, scale, spectrum, 1);
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            //if (!unitSetting.PSD)
            //{
            //    return;
            //}
            ////谱密度计算
            //scale = 1.0 / (equivalentNoiseBw * df);
            //CBLASNative.cblas_dscal(N, scale, spectrum, 1);
        }

        /// <summary>
        /// 将非线性单位转变成线性单位
        /// </summary>
        /// <param name="spectrum"></param>
        /// <param name="unit"></param>
        internal static void UnitConversion(ref double[] spectrum,SpectrumOutputUnit unit)
        {
          switch (unit)
            {
                case SpectrumOutputUnit.dBW:
                case SpectrumOutputUnit.dBm:
                    {
                        double scale = 1.0/10;
                        CBLASNative.cblas_dscal(spectrum.Length, scale, spectrum, 1);
                        for (int i = 0; i < spectrum.Length; i++)
                        {
                            spectrum[i] = Math.Pow(10.0, spectrum[i]);
                        }
                        break;
                    }
                case SpectrumOutputUnit.dBuV:
                case SpectrumOutputUnit.dBmV:
                case SpectrumOutputUnit.dBV:
                    {
                        double scale = 1.0 / 20;
                        CBLASNative.cblas_dscal(spectrum.Length, scale, spectrum, 1);
                        for (int i = 0; i < spectrum.Length; i++)
                        {
                            spectrum[i] = Math.Pow(10.0, spectrum[i]);
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// 将单位转变为V2
        /// </summary>
        /// <param name="spectrum"></param>
        /// <param name="unit"></param>
        internal static void UnitConversionToV2(ref double[] spectrum, SpectrumUnitInfo unit)
        {
            int N = spectrum.Length;
            double scale=0.0;
            switch (unit.Type)
            {
                case SpectrumOutputUnit.V2:
                    break;
                case SpectrumOutputUnit.dBuV:
                    scale = 1.0 / 20;
                    CBLASNative.cblas_dscal(spectrum.Length, scale, spectrum, 1);
                    for (int i = 0; i < spectrum.Length; i++)
                    {
                        spectrum[i] = Math.Pow(10.0, spectrum[i])/1000000;
                    }
                    VMLNative.vdSqr(N, spectrum, spectrum);
                    break;
                case SpectrumOutputUnit.dBmV:
                    scale = 1.0 / 20;
                    CBLASNative.cblas_dscal(spectrum.Length, scale, spectrum, 1);
                    for (int i = 0; i < spectrum.Length; i++)
                    {
                        spectrum[i] = Math.Pow(10.0, spectrum[i]) / 1000;
                    }
                    VMLNative.vdSqr(N, spectrum, spectrum);
                    break;
                case SpectrumOutputUnit.dBV:
                    scale = 1.0 / 20;
                    CBLASNative.cblas_dscal(spectrum.Length, scale, spectrum, 1);
                    for (int i = 0; i < spectrum.Length; i++)
                    {
                        spectrum[i] = Math.Pow(10.0, spectrum[i]);
                    }
                    VMLNative.vdSqr(N, spectrum, spectrum);
                    break;
                case SpectrumOutputUnit.V:
                     VMLNative.vdSqr(N, spectrum, spectrum);
                    break;
                case SpectrumOutputUnit.W:
                    CBLASNative.cblas_dscal(spectrum.Length, unit.Impedance, spectrum, 1);
                    break;
                case SpectrumOutputUnit.dBW:
                    scale = 1.0 / 10;
                    CBLASNative.cblas_dscal(spectrum.Length, scale, spectrum, 1);
                    for (int i = 0; i < spectrum.Length; i++)
                    {
                        spectrum[i] = Math.Pow(10.0, spectrum[i]) ;
                    }
                    CBLASNative.cblas_dscal(spectrum.Length, unit.Impedance, spectrum, 1);
                    break;
                case SpectrumOutputUnit.dBm:
                    {
                        scale = 1.0 / 10;
                        CBLASNative.cblas_dscal(spectrum.Length, scale, spectrum, 1);
                        for (int i = 0; i < spectrum.Length; i++)
                        {
                            spectrum[i] = Math.Pow(10.0, spectrum[i])/1000;
                        }
                        CBLASNative.cblas_dscal(spectrum.Length, unit.Impedance, spectrum, 1);
                        break;
                    }               
            }
        }


        /// <summary>
        /// 将单位V2转变为其他单位
        /// </summary>
        /// <param name="data"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        internal static double UnitConversionV2ToOther( double data, SpectrumUnitInfo unit)
        {
            double PowerInBand = 0;
            double PowerInBandTem = 0;
            switch (unit.Type)
            {
                case SpectrumOutputUnit.V2:
                    PowerInBandTem = data;
                    break;
                case SpectrumOutputUnit.dBuV:
                    PowerInBandTem = 20 * Math.Log10(Math.Sqrt(data) * 1000000);
                    break;
                case SpectrumOutputUnit.dBmV:
                    PowerInBandTem = 20*Math.Log10(Math.Sqrt(data)*1000);
                    break;
                case SpectrumOutputUnit.dBV:
                    PowerInBandTem = 20 * Math.Log10(Math.Sqrt(data));
                    break;
                case SpectrumOutputUnit.V:
                    PowerInBandTem = Math.Sqrt(data);
                    break;
                case SpectrumOutputUnit.W:
                    PowerInBandTem = data/ unit.Impedance;
                    break;
                case SpectrumOutputUnit.dBW:
                    PowerInBandTem = 10 * Math.Log10(data / unit.Impedance);
                    break;
                case SpectrumOutputUnit.dBm:
                    PowerInBandTem = 10 * Math.Log10(data / unit.Impedance*1000);
                    break;
            }
            PowerInBand = PowerInBandTem;
            return PowerInBand;
        }
        #endregion

        #region  ---------------Internal: Adance FFT----------------
        /// <summary>
        /// Advance Real FFT
        /// </summary>
        /// <param name="xIn">time domain data</param>
        /// <param name="spectralLines">spectralLines</param>
        /// <param name="windowType">window type</param>
        /// <param name="xOut">spectral out data</param>
        /// <param name="spectralInfo">spectral info</param>
        internal static void AdvanceRealFFT(double[] xIn, int spectralLines, WindowType windowType,
                                     double[] xOut, ref SpectralInfo spectralInfo)
        {
            int n = xIn.Length, windowsize = 0, fftcnt = 0; //做FFT的次数
            int fftsize = 0; //做FFT点数
            double cg = 0, enbw = 0, scale = 0.0;
            double[] xInTmp = null;
            double[] windowData = null;
            Complex[] xOutCTmp = null;

            //输入的线数超过最大支持的线数则使用最大支持线数
            if (spectralLines > MaxSpectralLine)
            {
                spectralLines = MaxSpectralLine;
            }

            //输入的点数小于线数，则窗长度为N，先加窗再补零到2*spectralLines再做FFT
            if (n <= 2 * spectralLines)
            {
                windowsize = n;
                fftcnt = 1;
            }
            else
            {
                windowsize = 2 * spectralLines;
                fftcnt = n / (2 * spectralLines);
            }

            fftsize = 2 * spectralLines; //不管N与2*spectralLines的关系是怎么样，FFT的点数都应该为 2*spectralLines

            xInTmp = new double[fftsize];
            xOutCTmp = new Complex[fftsize / 2 + 1];

            if (n < (2 * spectralLines))
            {
                //memset(x_in + N, 0, (fftsize - N) * sizeof(double)); //补零至spectralLines
                for (int i = n; i < fftsize; i++)
                {
                    xInTmp[i] = 0;
                }
            }
            //memset(xOut, 0, spectralLines * sizeof(double));
            //生成窗函数的数据
            windowData = new double[windowsize];
            Window.Window.GetWindow(windowType, ref windowData, out cg, out enbw);
            CBLASNative.cblas_dscal(windowsize, 1 / cg, windowData, 1); //窗系数归一化
            CBLASNative.cblas_dscal(xOut.Length, 0, xOut, 1); //将xOut清零
            GCHandle gch = GCHandle.Alloc(xIn, GCHandleType.Pinned);
            var xInPtr = gch.AddrOfPinnedObject();
            for (int i = 0; i < fftcnt; i++)
            {
                //拷贝数据到临时内存中
                //memcpy(x_in, _inputWaveform + i * windowsize, fftsize * sizeof(double));
                /*TIME_DOMAIN_WINDOWS(windowType, x_in, &CG, &ENBW, windowsize);*//*(double*)(xIn + i * windowsize)*/
                VMLNative.vdMul(windowsize, windowData, xInPtr + i * fftsize * sizeof(double), xInTmp);
                BasicFFT.RealFFT(xInTmp, ref xOutCTmp);

                //计算FFT结果复数的模,复用x_in做中间存储
                VMLNative.vzAbs(fftsize / 2 + 1, xOutCTmp, xInTmp);

                //每次计算结果累加起来
                VMLNative.vdAdd(spectralLines, xInTmp, xOut, xOut);
            }

            scale = 2 * (1.0 / fftcnt) / Sqrt2; //双边到单边有一个二倍关系,输出为Vrms要除以根号2

            //fftcnt次的频谱做平均
            CBLASNative.cblas_dscal(spectralLines, scale, xOut, 1);

            xOut[0] = xOut[0] / Sqrt2; //上一步零频上多除了根号2，这里乘回来（Rms在零频上不用除根号2，单双边到单边还是要乘2 ?）

            spectralInfo.spectralLines = spectralLines;
            spectralInfo.FFTSize = fftsize;
            spectralInfo.windowSize = windowsize;
            spectralInfo.windowType = windowType;
        }


        /// <summary>
        /// 计算时域信号的复数频谱，包含幅度谱和相位谱信息
        /// </summary>
        /// <param name="waveform">时域波形数据</param>
        /// <param name="windowType">窗类型</param>
        /// <param name="spectrum">计算后的复数频谱数据</param>
        internal static void AdvanceComplexFFT(double[] waveform, WindowType windowType, ref Complex[] spectrum)
        {
            if (waveform == null || spectrum == null || spectrum.Length < (waveform.Length / 2 + 1))
            {
                throw new JXIUserBufferException("length is null!");
            }

            int n = waveform.Length, windowsize = waveform.Length; //做FFT的次数
            int fftsize = windowsize; //做FFT点数
            double cg = 0, enbw = 0;
            double[] xInTmp = null;
            double[] windowData = null;

            xInTmp = new double[fftsize];

            GCHandle gchXIn = GCHandle.Alloc(waveform, GCHandleType.Pinned);
            var xInPtr = gchXIn.AddrOfPinnedObject();
            GCHandle gchXout = GCHandle.Alloc(spectrum, GCHandleType.Pinned);
            var xOutPtr = gchXout.AddrOfPinnedObject();

            try
            {
                //生成窗函数的数据
                windowData = new double[windowsize];
                Window.Window.GetWindow(windowType, ref windowData, out cg, out enbw);
                CBLASNative.cblas_dscal(windowsize, 1 / cg, windowData, 1); //窗系数归一化
                CBLASNative.cblas_dscal(spectrum.Length, 0, xOutPtr, 1); //将xOut清零
                /*TIME_DOMAIN_WINDOWS(windowType, x_in, &CG, &ENBW, windowsize);*//*(double*)(xIn + i * windowsize)*/
                VMLNative.vdMul(windowsize, windowData, xInPtr, xInTmp);
                BasicFFT.RealFFT(xInTmp, ref spectrum);
            }
            finally
            {
                gchXIn.Free();
                gchXout.Free();
            }

        }

        /// <summary>
        /// 计算复数信号的复数频谱，包含幅度谱和相位谱信息
        /// </summary>
        /// <param name="waveform">时域波形数据</param>
        /// <param name="windowType">窗类型</param>
        /// <param name="spectrum">计算后的复数频谱数据</param>
        internal static void AdvanceComplexFFT(Complex[] waveform, WindowType windowType, ref Complex[] spectrum)
        {
            if (waveform == null || spectrum == null || spectrum.Length < waveform.Length)
            {
                throw new JXIUserBufferException("length is null!");
            }

            int n = waveform.Length, windowsize = waveform.Length; //做FFT的次数
            int fftsize = windowsize; //做FFT点数
            double cg = 0, enbw = 0;
            Complex[] xTmp = null;
            double[] windowData = null;
            double[] windowDataC = null;

            xTmp = new Complex[fftsize];

            GCHandle gchXIn = GCHandle.Alloc(waveform, GCHandleType.Pinned);
            var xInPtr = gchXIn.AddrOfPinnedObject();
            GCHandle gchXout = GCHandle.Alloc(spectrum, GCHandleType.Pinned);
            var xOutPtr = gchXout.AddrOfPinnedObject();
            var gchData = GCHandle.Alloc(xTmp, GCHandleType.Pinned);
            var dataPtr = gchData.AddrOfPinnedObject();

            CBLASNative.cblas_dcopy( waveform.Length* 2, xInPtr, 1, dataPtr, 1);
            try
            {
                //生成窗函数的数据
                windowData = new double[windowsize];
                windowDataC = new double[windowsize*2];

                var gchWindow = GCHandle.Alloc(windowDataC, GCHandleType.Pinned);
                var windowPtr = gchWindow.AddrOfPinnedObject();

                Window.Window.GetWindow(windowType, ref windowData, out cg, out enbw);

                CBLASNative.cblas_dscal(windowData.Length, 1 / cg, windowData, 1); //窗系数归一化
                CBLASNative.cblas_dscal(spectrum.Length, 0, xOutPtr, 1); //将xOut清零
                CBLASNative.cblas_dcopy(windowData.Length, windowData, 1, windowPtr, 2);//复制real窗函数变成complex的窗函数
                CBLASNative.cblas_dcopy(windowData.Length, windowData, 1, windowPtr + sizeof(double), 2);
                VMLNative.vdMul(windowData.Length * 2, windowDataC, dataPtr, dataPtr);//将信号通过窗函数
                BasicFFT.ComplexFFT(xTmp, ref spectrum);//复数fft
            }
            finally
            {
                gchXIn.Free();
                gchXout.Free();
                gchData.Free();
            }

        }

        #endregion
    }
}
