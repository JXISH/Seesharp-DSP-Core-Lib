using System;
using System.Linq;
using SeeSharpTools.JXI.SignalProcessing.GeneralSpectrum;
using System.Reflection;
using System.Numerics;
using SeeSharpTools.JXI.SignalProcessing.Window;

namespace SeeSharpTools.JXI.SignalProcessing.Measurement
{
    /// <summary>
    /// <para>HarmonicAnalysis Class</para>
    /// <para>Chinese Simplified：谐波分析类</para>
    /// </summary>
    public static class HarmonicAnalysis
    {
        #region----------------------------Construct-------------------------------------
        /// <summary>
        /// 构造函数 调License Manager
        /// </summary>
        static HarmonicAnalysis()
        {

        }
        #endregion

        #region-------------------------Public Methods----------------------
        /// <summary>
        /// <para>Calculates the THD and level of all components of the input signal.</para>
        /// <para>THD in value, not %</para>
        /// <para>component levels in voltage peak which is 1.414*rms</para>
        /// <para>component[0]=DC level; [1]=fundamental frequency level; ...</para>
        /// <para>if the specified highest harmonic is higher than nyquest freq, </para>
        /// <para>the exceeded frequency components will be 0; </para>
        /// <para>Chinese Simplified：计算输入信号总谐波失真以及谐波分量</para>
        /// <para>THD是值，而不是百分比</para>
        /// <para>component levels为有效值的1.414倍（单位V2）</para>
        /// <para>component[0]为直流分量的功率; [1]为基波的功率; ...</para>
        /// <para>如果指定的最高谐波高于奈奎斯特频率，则超过频率分量将为0;</para>
        /// </summary>
        /// <param name="timewaveform">
        /// <para>the waveform of input signal assuming in voltage</para>
        /// <para>Chinese Simplified：输入信号（单位为V）</para>
        /// </param>
        /// <param name="dt">
        /// <para>sampling interval of timewaveform (_status)</para>
        /// <para>Chinese Simplified：时域波形的抽样间隔</para>
        /// </param>
        /// <param name="detectedFundamentalFreq">
        /// <para>the _calculated peak tone frequency in the range of search</para>
        /// <para>Chinese Simplified：基波频率</para>
        /// </param>
        /// <param name="THD">
        /// <para>total harmonic distortion in scale = sqrt(sum(harmonics power) / fundamental power)</para>
        /// <para>Chinese Simplified：总谐波失真</para>
        /// </param>
        /// <param name="componentsLevel">
        /// <para>in voltage peak which is 1.414*rms, [0]for DC [1] for fundamental</para>
        /// <para>Chinese Simplified：谐波分量</para>
        /// </param>
        /// <param name="highestHarmonic">
        /// <para>the highest order to analysis, if too high, the exceeded harmonics level will be 0</para>
        /// <para>Chinese Simplified：最高谐波</para>
        /// </param>
        public static void THDAnalysis(double[] timewaveform, double dt, out double detectedFundamentalFreq,
                                       out double THD, ref double[] componentsLevel, int highestHarmonic = 10)
        {
            double[] spectrum = new double[timewaveform.Length / 2];
            double df;
            var spectUnit = SpectrumOutputUnit.V2; //this V^2 unit relates to power in band calculation, don't change
            var winType = WindowType.Four_Term_Nuttal;  //relates to ENBW, must change in pair
            //四阶窗函数，有2*4-1=7个bins，为了保险起见，防止信号泄露，再加2个bins
            int indexCnt = 9;
            double CG = 0;
            double ENBW = 0;
            double[] windowdata = new double[timewaveform.Length];
            Window.Window.GetWindow(winType, ref windowdata, out CG, out ENBW);
            // double ENBW = 1.500; //ENBW for winType Hanning.ENBW = 1.500 Hamming 1.362826

            double maxValue = 0;
            int maxValueIndex = 0;
            int i, approxFreqIndex, startIndex = 0, endIndex = spectrum.Length;
            double powerInBand = 0;
            double powerMltIndex = 0;
            double powerTotalHarmonic = 0;
            double spectrumDV = 0;
            if (componentsLevel == null || componentsLevel.Length != highestHarmonic)
            {
                componentsLevel = new double[highestHarmonic + 1 > 2 ? highestHarmonic + 1 : 2];//componentsLevel的长度比highestHarmonic大1，但是不能小于2
            }
            Spectrum.PowerSpectrum(timewaveform, 1 / dt, ref spectrum, out df, spectUnit, winType);
            //滤除直流
            for (int j = 0; j < indexCnt/2; j++)
            {
                spectrumDV = spectrum[j];
                spectrum[j] = 0;
            }
            //****************************************
            //Search peak

            //Search spectrum from [i1] to i1+i2-1;
            maxValue = -1; //power spectrum can not be less than 0;
            maxValue = spectrum.Max();
            maxValueIndex = Array.FindIndex(spectrum, s => s == maxValue);


            //Search peak ends
            //****************************************
            //Peak analysis
            startIndex = maxValueIndex - indexCnt / 2;
            if (startIndex < 0) startIndex = 0;

            endIndex = startIndex + indexCnt;

            if (endIndex > spectrum.Length - 1) endIndex = spectrum.Length - 1;

            for (i = startIndex; i < endIndex; i++)
            {
                powerInBand += spectrum[i];
                powerMltIndex += spectrum[i] * i;
            }
            //Given the estimated frequency and power, the exact frequency can be _calculated
            detectedFundamentalFreq = powerMltIndex / powerInBand * df;

            componentsLevel[0] = spectrumDV / ENBW;  //DC in V^2
            componentsLevel[1] = powerInBand / ENBW; //unit V^2 for amplitude  //Refer this formula to  ITU Handbook
                                                     //Peak analysis ends
                                                     //****************************************
                                                     //Power calculation for THD

            powerTotalHarmonic = 0;
            for (i = 2; i <= highestHarmonic; i++)
            {
                approxFreqIndex = (int)Math.Round(detectedFundamentalFreq / df * i - 2);
                if (approxFreqIndex < 0) approxFreqIndex = 0;

                powerInBand = 0;
                for (startIndex = 1; startIndex < 5; startIndex++)
                {
                    if (approxFreqIndex + startIndex < spectrum.Length)
                    {
                        powerInBand += spectrum[approxFreqIndex + startIndex];
                    }
                }
                componentsLevel[i] = powerInBand / ENBW;
                powerTotalHarmonic += componentsLevel[i];
            }
            THD = powerTotalHarmonic / componentsLevel[1];
            THD = Math.Sqrt(THD);
            //Power calculation ends
            //****************************************
            // transfer components level from V^2 to V peak amplitude;
            for (i = 1; i < highestHarmonic + 1; i++)
            {
                componentsLevel[i] = Math.Sqrt(componentsLevel[i] * 2); //AC has conjugate  part so to *2 for the energy
            }
            componentsLevel[0] = Math.Sqrt(componentsLevel[0]); //DC has no conjugate part
            //transfer ends
            //****************************************
        }

        /// <summary>
        /// <para>Calculates amplitude  phase and frequency of fundamental wave(sin)</para>
        /// <para>Chinese Simplified：计算信号基波(sin)的频率、相位、幅值</para>
        /// </summary>
        /// <param name="timewaveform">
        /// <para>the waveform of input signal assuming in voltage</para>
        /// <para>Chinese Simplified：输入信号（单位为V）</para>
        ///  </param>
        /// <param name="dt">
        /// <para>sampling interval of timewaveform (_status)</para>
        /// <para>Chinese Simplified：时域波形的抽样间隔</para>
        /// </param>
        /// <param name="detectedFundamentalFreq">
        /// <para>the _calculated peak tone frequency in the range of search</para>
        /// <para>Chinese Simplified：基波频率</para>
        /// </param>
        /// <param name="amplitude">
        /// <para> The amplitude of  fundamental wave</para>
        /// <para>Chinese Simplified：基波幅值</para>
        /// /param>
        /// <param name="phase">
        /// <para>The phase of  fundamental wave</para>
        /// <para>Chinese Simplified：基波相位</para>
        /// <param name="initialGuess">
        /// <para>Initial guess for the tone frequency, unit in Hz</para>
        /// <para>Chinese Simplified：预估信号的频率，单位Hz</para>
        /// <param name="searchRange">
        /// <para>Peak search range near the initialGuess</para>
        /// <para>Chinese Simplified：在预估频率附近搜索峰值的范围</para>
        /// </param>
        public static void ToneAnalysis(double[] timewaveform, double dt, out double detectedFundamentalFreq,
                                        out double amplitude, out double phase, double initialGuess=0, double searchRange = 0.05)
        {

            //double initialGuess = 0;
            //double searchRange = 0.05;
            var result=SingleToneAnalysis(timewaveform, 1/dt, initialGuess, searchRange);
            detectedFundamentalFreq = result.Frequency;
            amplitude = result.Amplitude;
            phase = result.Phase;
        }


        /// <summary>
        /// <para>Calculates amplitude  phase and frequency of fundamental wave(cos)</para>
        /// <para>Chinese Simplified：计算信号基波(cos)的频率、相位、幅值</para>
        /// </summary>
        /// <param name="timewaveform">
        /// <para>the waveform of input signal assuming in voltage</para>
        /// <para>Chinese Simplified：输入信号（单位为V）</para>
        ///  </param>
        /// <param name="dt">
        /// <para>sampling interval of timewaveform (_status)</para>
        /// <para>Chinese Simplified：时域波形的抽样间隔</para>
        /// </param>
        /// <param name="detectedFundamentalFreq">
        /// <para>the _calculated peak tone frequency in the range of search</para>
        /// <para>Chinese Simplified：基波频率</para>
        /// </param>
        /// <param name="amplitude">
        /// <para> The amplitude of  fundamental wave</para>
        /// <para>Chinese Simplified：基波幅值</para>
        /// /param>
        /// <param name="phase">
        /// <para>The phase of  fundamental wave</para>
        /// <para>Chinese Simplified：基波相位</para>
        /// <param name="initialGuess">
        /// <para>Initial guess for the tone frequency, unit in Hz</para>
        /// <para>Chinese Simplified：预估信号的频率，单位Hz</para>
        /// <param name="searchRange">
        /// <para>Peak search range near the initialGuess</para>
        /// <para>Chinese Simplified：在预估频率附近搜索峰值的范围</para>
        /// </param>
        public static void ToneAnalysis(Complex[] timewaveform, double dt, out double detectedFundamentalFreq,
                                        out double amplitude, out double phase, double initialGuess = 0, double searchRange = 0.05)
        {

            //double initialGuess = 0;
            //double searchRange = 0.05;
            var result = SingleToneAnalysis(timewaveform, 1 / dt, initialGuess, searchRange);
            detectedFundamentalFreq = result.Frequency;
            amplitude = result.Amplitude;
            phase = result.Phase;
        }

        /// <summary>
        /// <para>Calculates SINAD:Signal-to-noise and distortion ratio </para>
        /// <para>Chinese Simplified：计算信纳比SINAD</para>
        /// </summary>
        /// <param name="timewaveform">
        /// <para>the waveform of input signal assuming in voltage</para>
        /// <para>Chinese Simplified：输入信号（单位为V）</para>
        ///  </param>
        /// <param name="dt">
        /// <para>sampling interval of timewaveform (_status)</para>
        /// <para>Chinese Simplified：时域波形的抽样间隔</para>
        /// </param>
        /// <param name="detectedFundamentalFreq">
        /// <para>the _calculated peak tone frequency in the range of search</para>
        /// <para>Chinese Simplified：基波频率</para>
        /// </param>
        /// <param name="SINAD">
        /// <para>SINAD:Signal-to-noise and distortion ratio in dB </para>
        /// <para>Chinese Simplified：信纳比,单位DB</para>
        /// </param>
        /// <param name="componentsLevel">
        /// <para>in voltage peak which is 1.414*rms, [0]for DC [1] for fundamental</para>
        /// <para>Chinese Simplified：谐波分量</para>
        /// </param>
        /// <param name="highestHarmonic">
        /// <para>the highest order to analysis, if too high, the exceeded harmonics level will be 0</para>
        /// <para>Chinese Simplified：最高谐波</para>
        public static void SINADAnalysis(double[] timewaveform, double dt, out double detectedFundamentalFreq,
                                          out double SINAD, ref double[] componentsLevel, int highestHarmonic = 10)
        {
            //analysis body
            double THD;
            THDAnalysis(timewaveform, dt, out detectedFundamentalFreq, out THD, ref componentsLevel, highestHarmonic);

            //计算信号扣除直流分量的RMS（均方根）
            double std1 = StandardDeviation(timewaveform);

            //去除主信号
            var result = SingleToneAnalysis(timewaveform, 1/dt);
            double[] signalNoFundamentalFreq = new double[timewaveform.Length];
            for (int i = 0; i < timewaveform.Length; i++)
            {
                signalNoFundamentalFreq[i] = timewaveform[i] - result.Amplitude* Math.Sin(2 * Math.PI * result.Frequency * dt*i + result.Phase);
            }

            //计算信号扣除直流分量的RMS（均方根）
            double std2 = StandardDeviation(signalNoFundamentalFreq);

            SINAD = 20 * Math.Log10(std1 / std2);         
        }

        #endregion

        #region-------------------------Private Methods----------------------
        /// <summary>
        /// 计算数组所有元素之和
        /// </summary>
        /// <param name="data">输入数据</param>
        /// <returns>数组所有元素之和</returns>
        private static double Sum(double[] data)
        {
            double sum = 0.0;
            for (int i = 0; i < data.Length; i++)
            {
                sum += data[i];
            }
            return sum;
        }

        /// <summary>
        /// Perform substraction on two phase value, unit in radian. Thre result will be wrapped to range [-Pi, Pi] (*)
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns>The difference between p1 and p2. </returns>
        static private double _PhaseSub(double p1, double p2)
        {
            double diff = (p1 - p2) / Math.PI / 2.0;
            diff -= Math.Round(diff);       // Round() performs 'round to even'. Thus both -0.5 and +0.5 might show in result.
            return diff * Math.PI * 2.0;
        }

        /// <summary>
        /// Refine the peak estimation based on three cosequent peak spectrum values. 
        /// Ref: X.Ming, D.Kang “Corrections for frequency, amplitude and phase in a fast fourier transform of 
        /// a harmonic signal” Mechanical Systems and Signal Processing V. 10, 2, March 1996, Pages 211-221
        /// </summary>
        /// <param name="threeFingers">three spectrum</param>
        /// <returns>Tone information. Frequency unit in 'bins'. </returns>
        private static ToneInfo FFTPeakCorrection(Complex[] threeFingers)
        {
            ToneInfo toneInfo = new ToneInfo();
            double a, b, c;
            double delta;

            a = threeFingers[0].Magnitude;
            b = threeFingers[1].Magnitude;
            c = threeFingers[2].Magnitude;

            if (a == c)
            {
                delta = 0;
                toneInfo.Frequency = 1;
                toneInfo.Amplitude = b;
            }
            else
            {
                double sub_peak = a > c ? a : c;
                delta = (b - 2.0 * sub_peak) / (b + sub_peak) * (a > c ? 1 : -1);
                toneInfo.Frequency = 1 + delta;
                toneInfo.Amplitude = b * (1 - delta * delta) / Sinc(delta);
            }

            // Linear interpolate phase value
            double p0, p1;
            p0 = threeFingers[1].Phase;
            p1 = threeFingers[a > c ? 0 : 2].Phase;

            toneInfo.Phase = _PhaseSub(p0 + delta * _PhaseSub(p1, p0), 0);

            return toneInfo;
        }

        // ********************************************************************************
        /// <summary>
        /// Single Tone Analysis
        /// </summary>
        /// <param name="timewaveform">Waveform in time space</param>
        /// <param name="Fs">Sampling frequency, unit in Hz</param>
        /// <param name="initialGuess">Initial guess for the tone frequency, unit in Hz</param>
        /// <param name="searchRange">Peak search range near the initialGuess.</param>
        /// <returns>Tone information of the signal. Contains amplitude, frequency and phase.</returns>
        /// <created>Wei Jin,2019/11/29</created>
        /// <changed>Wei Jin,2019/11/29</changed>
        // ********************************************************************************
        private static ToneInfo SingleToneAnalysis(double[] timewaveform, double Fs = 1.0, double initialGuess = 0, double searchRange = 0.05)
        {
            ToneInfo toneInfo;
            int i;

            int fftSize = timewaveform.Length;
            Complex[] spectrum = new Complex[fftSize];

            Spectrum.AdvanceComplexFFT(timewaveform, WindowType.Hanning, ref spectrum);

            // Use the center position as time '0' reference, which makes the spectrum of the window function pure real number 
            for (i = 0; i < (fftSize + 1) / 4; i++)
            {
                spectrum[i * 2 + 1] = -spectrum[i * 2 + 1];
            }

            // Null DC components
            for (i = 0; i < 2; i++)
                spectrum[i] = 0;

            // Establish tone search range
            int searchStart = 2;
            int searchEnd = fftSize / 2 - 2;

            if (initialGuess > 0 && initialGuess < Fs / 2.0)
            {
                searchStart = Math.Max(searchStart, (int)((initialGuess / Fs - searchRange / 2) * fftSize));
                searchEnd = Math.Min(searchEnd, (int)((initialGuess / Fs + searchRange / 2) * fftSize));
            }

            // Gross search for the peak tone
            double peakVal = 0;
            int peakPos = 0;
            for (i = searchStart; i < searchEnd; i++)
            {
                if (peakVal < spectrum[i].Magnitude)
                {
                    peakVal = spectrum[i].Magnitude;
                    peakPos = i;
                }
            }

            // Refine peak result for the first round
            Complex[] threeFingers = new Complex[3];
            Array.Copy(spectrum, peakPos - 1, threeFingers, 0, 3);
            toneInfo = FFTPeakCorrection(threeFingers);
            toneInfo.Frequency += peakPos - 1;

            // Remove aliasing around DC and Fs/2
            // The Fourier transform of the hanning window has the following form
            //      h(z)    = sinc(z) / (z * z - 1)
            //              = sin(pi * z) / (pi * z * (z * z - 1))
            // Note: the negative frequency component has conjugate phase
            // Question(wjin): Why use add instead of sub?            
            double x_offset;
            for (i = 0; i < 3; i++)
            {
                x_offset = peakPos - 1 + i + toneInfo.Frequency;
                threeFingers[i] += Sinc(x_offset) / (x_offset * x_offset - 1.0)
                    * Complex.FromPolarCoordinates(toneInfo.Amplitude, -toneInfo.Phase);

                x_offset -= fftSize;
                threeFingers[i] += Sinc(x_offset) / (x_offset * x_offset - 1.0)
                    * Complex.FromPolarCoordinates(toneInfo.Amplitude, -toneInfo.Phase);
            }

            // Refine peak result for the second round
            toneInfo = FFTPeakCorrection(threeFingers);
            toneInfo.Frequency += peakPos - 1;

            // Correct the results for output.
            toneInfo.Amplitude *= 2.0 / fftSize;
            toneInfo.Phase = _PhaseSub(toneInfo.Phase, toneInfo.Frequency * Math.PI);   // Change the reference position to the begining of the singal
            toneInfo.Phase = _PhaseSub(toneInfo.Phase, -0.5 * Math.PI);                 // Change Cos phase to Sin phase
            toneInfo.Frequency *= Fs / fftSize;                                         // Convert frequency unit from 'bins' to engineering units.

            return toneInfo;
        }



        // ********************************************************************************
        /// <summary>
        /// Single Tone Analysis
        /// </summary>
        /// <param name="timewaveform">Waveform in time space</param>
        /// <param name="Fs">Sampling frequency, unit in Hz</param>
        /// <param name="initialGuess">Initial guess for the tone frequency, unit in Hz</param>
        /// <param name="searchRange">Peak search range near the initialGuess.</param>
        /// <returns>Tone information of the signal. Contains amplitude, frequency and phase.</returns>
        /// <created>Xiexiao Xie 2021/09/15</created>
        /// <changed>Xiexiao Xie 2021/09/15</changed>
        // ********************************************************************************
        private static ToneInfo SingleToneAnalysis(Complex[] timewaveform, double Fs = 1.0, double initialGuess = 0, double searchRange = 0.05)
        {
            ToneInfo toneInfo;
            int i;

            int fftSize = timewaveform.Length;
            Complex[] spectrum = new Complex[fftSize];

            Spectrum.AdvanceComplexFFT(timewaveform, WindowType.Hanning, ref spectrum);

            // Use the center position as time '0' reference, which makes the spectrum of the window function pure real number 
            for (i = 0; i < (fftSize + 1) / 4; i++)
            {
                spectrum[i * 2 + 1] = -spectrum[i * 2 + 1];
            }

            // Null DC components
            for (i = 0; i < 2; i++)
                spectrum[i] = 0;

            // Establish tone search range
            int searchStart = 2;
            int searchEnd = fftSize / 2 - 2;

            if (initialGuess > 0 && initialGuess < Fs / 2.0)
            {
                searchStart = Math.Max(searchStart, (int)((initialGuess / Fs - searchRange / 2) * fftSize));
                searchEnd = Math.Min(searchEnd, (int)((initialGuess / Fs + searchRange / 2) * fftSize));
            }

            // Gross search for the peak tone
            double peakVal = 0;
            int peakPos = 0;
            for (i = searchStart; i < searchEnd; i++)
            {
                if (peakVal < spectrum[i].Magnitude)
                {
                    peakVal = spectrum[i].Magnitude;
                    peakPos = i;
                }
            }

            // Refine peak result for the first round
            Complex[] threeFingers = new Complex[3];
            Array.Copy(spectrum, peakPos - 1, threeFingers, 0, 3);
            toneInfo = FFTPeakCorrection(threeFingers);
            toneInfo.Frequency += peakPos - 1;

            // Remove aliasing around DC and Fs/2
            // The Fourier transform of the hanning window has the following form
            //      h(z)    = sinc(z) / (z * z - 1)
            //              = sin(pi * z) / (pi * z * (z * z - 1))
            // Note: the negative frequency component has conjugate phase
            // Question(wjin): Why use add instead of sub?            
            //double x_offset;
            //for (i = 0; i < 3; i++)
            //{
            //    x_offset = peakPos - 1 + i + toneInfo.Frequency;
            //    threeFingers[i] += Sinc(x_offset) / (x_offset * x_offset - 1.0)
            //        * Complex.FromPolarCoordinates(toneInfo.Amplitude, -toneInfo.Phase);

            //    x_offset -= fftSize;
            //    threeFingers[i] += Sinc(x_offset) / (x_offset * x_offset - 1.0)
            //        * Complex.FromPolarCoordinates(toneInfo.Amplitude, -toneInfo.Phase);
            //}

            // Refine peak result for the second round
            toneInfo = FFTPeakCorrection(threeFingers);
            toneInfo.Frequency += peakPos - 1;

            // Correct the results for output.
            toneInfo.Amplitude *= 1.0 / fftSize;
            toneInfo.Phase = _PhaseSub(toneInfo.Phase, toneInfo.Frequency * Math.PI);   // Change the reference position to the begining of the singal
            toneInfo.Frequency *= Fs / fftSize;                                         // Convert frequency unit from 'bins' to engineering units.

            return toneInfo;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static double Sinc(double x)
        {
            double phase = Math.PI * x;
            if (0 == phase)
            {
                return 1;
            }
            else
            {
                return Math.Sin(phase) / (phase);
            }
        }

        /// <summary>
        /// 计算算术平均值,（x1+x2+...+xn）/n
        /// </summary>
        /// <param name="nums">待计算数组</param>
        /// <returns>算术平均值</returns>
        private static double Mean(double[] nums)
        {
            #region 参数列表条件判断
            //输入数组不能为空
            if (nums == null || nums.Length == 0)
            {
                throw new ArgumentException("Parameter Input Illegal");
            }
            #endregion
            double sum = 0;
            double avg = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
            }
            avg = sum / nums.Length;
            return avg;
        }

        /// <summary>
        /// 计算方差
        /// </summary>
        /// <param name="nums">计算方差的数组</param>
        /// <returns></returns>
        private static double Variance(double[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                throw new ArgumentException("Parameter Input Illegal");
            }
            double _mean = Mean(nums);
            double sum = 0;
            int length = nums.Length;
            for (int i = 0; i < length; i++)
            {
                sum += Math.Pow((nums[i] - _mean), 2);
            }
            double _variance = sum / length; // 方差
            return _variance;
        }

        /// <summary>
        /// 计算标准差
        /// </summary>
        /// <param name="nums">计算标准差的数组</param>
        /// <returns></returns>
        private static double StandardDeviation(double[] nums)
        {
            double _variance = Variance(nums); // 方差
            double _standardDeviation = Math.Pow(_variance, 1 / 2.0);//标准差
            return _standardDeviation;
        }
        #endregion

    }

    /// <summary>
    /// Tone Analysis Result
    /// </summary>
    internal class ToneInfo
    {
        /// <summary>
        /// Tone frequency
        /// </summary>
        public double Frequency;
        /// <summary>
        /// Tone amplitude
        /// </summary>
        public double Amplitude;
        /// <summary>
        /// Tone phase
        /// </summary>
        public double Phase;
    }
}
