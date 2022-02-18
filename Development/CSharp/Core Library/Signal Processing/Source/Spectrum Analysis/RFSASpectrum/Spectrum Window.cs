using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using SeeSharpTools.JXI.Numerics;
using SeeSharpTools.JXI.Mathematics;

namespace SeeSharpTools.JXI.SignalProcessing.SpectrumAnalysis.RFSASpectrum
{
    /**************************************************
    * 窗函数
    * **********************************************/
    /// <summary>
    /// 窗函数
    /// </summary>
    public class FFTWindow:IDisposable
    {
        #region Const

        internal static int SINGLE = 35;
        internal static int DOUBLE = 36;
        internal static int COMPLEX = 32;
        internal static int REAL = 33;
        internal static int INPLACE = 43;
        internal static int NOT_INPLACE = 44;

        internal const int SINC_OVER_SAMPLE = 8;            // 对于Sinc窗IQ折叠次数，频谱线数合并根数
        internal const double SINC_BW_SCALE = 1.1;          // 调整Scale，修正Sinc函数的3dB带宽

        #endregion

        #region ---- 构造和引用管理 ----

        /// <summary>
        /// DFTI 唯一实例，用非null表示已经初始化过了。
        /// </summary>
        private static FFTWindow _onlyInstance = null;

        /// <summary>
        /// Descriptor Config Settings, Descriptor Key
        /// </summary>
        private struct FFTWindowConfig
        {
            public int WindowSize;
            public FFTWindowType WindowType;
            public double Parameter;
            public int Precision;
            public int Placement;
            public int Domain;
        }

        /// <summary>
        /// All initialzied DFTI Descriptor
        /// </summary>
        private readonly Dictionary<FFTWindowConfig, GCHandle> _windowHandleDict;

        /// <summary>
        /// Construct, Creat the descriptor table
        /// </summary>
        private FFTWindow()
        {
            _windowHandleDict = new Dictionary<FFTWindowConfig, GCHandle>();
        }

        /// <summary>
        /// Release all the descriptors
        /// </summary>
        public void Dispose()
        {
            foreach (var descriptor in _windowHandleDict)
            {
                descriptor.Value.Free();
            }
            _windowHandleDict.Clear();
        }

        ~FFTWindow()
        {
            Dispose();
        }

        /// <summary>
        /// Get DFT Descriptor
        /// </summary>
        internal int GetWindowHandle(int windowSize, FFTWindowType windowType,int precision,out IntPtr windowHandle, double parameter = SINC_OVER_SAMPLE)
        {
            int error = 0;

            FFTWindowConfig config;
            config.WindowSize = windowSize;
            config.WindowType = windowType;
            config.Parameter = parameter;
            config.Precision = precision;
            config.Placement = FFTWindow.INPLACE;
            config.Domain = FFTWindow.COMPLEX;

            lock (_windowHandleDict)
            {
                //存在该Key则直接返回
                if (_windowHandleDict.ContainsKey(config))
                {
                    windowHandle = _windowHandleDict[config].AddrOfPinnedObject();
                    return error;
                }

                GCHandle handle;

                //不存在，则创建并配置，保存后返回
                if (config.Precision == FFTWindow.SINGLE)
                {
                    float[] window = GetWindow_float(config.WindowType, config.WindowSize, config.Parameter);
                    float[] windowForComplex = new float[window.Length * 2];
                    for (int i = 0; i < window.Length; i++)
                    {
                        windowForComplex[i * 2] = window[i];
                        windowForComplex[i * 2 + 1] = window[i];
                    }

                    handle = GCHandle.Alloc(windowForComplex, GCHandleType.Pinned);
                }
                else
                {
                    double[] window = GetWindow(config.WindowType, config.WindowSize, config.Parameter);
                    double[] windowForComplex = new double[window.Length * 2];
                    for (int i = 0; i < window.Length; i++)
                    {
                        windowForComplex[i * 2] = window[i];
                        windowForComplex[i * 2 + 1] = window[i];
                    }

                    handle = GCHandle.Alloc(windowForComplex, GCHandleType.Pinned);
                }

                //保存描述符并返回
                if (error == 0) { _windowHandleDict.Add(config, handle); }

                windowHandle = handle.AddrOfPinnedObject();
                return error;
            }
        }

        /// <summary>
        /// Get the only DFTI Instance。
        /// </summary>
        internal static FFTWindow GetInstance()
        {
            return _onlyInstance ?? (_onlyInstance = new FFTWindow());
        }

        #endregion

        /// <summary>
        /// 信号加窗
        /// </summary>

        public static void ProcessWindow(Complex[] data, FFTWindowType windowType)
        {
            IntPtr windowHandle;
            int error = GetInstance().GetWindowHandle(data.Length, windowType, FFTWindow.DOUBLE,out windowHandle);

            Vector.ArrayMulti_double(data, windowHandle);
        }

        public static void ProcessWindow(Complex32[] data, FFTWindowType windowType)
        {
            IntPtr windowHandle;
            int error = GetInstance().GetWindowHandle(data.Length, windowType, FFTWindow.SINGLE, out windowHandle);

            Vector.ArrayMulti_float(data, windowHandle);
        }

        /// <summary>
        /// 获取窗数据
        /// </summary>
        private static double[] GetWindow(FFTWindowType windowType, int size, double parameter)
        {
            double cg, enbw;
            switch (GetWindowGroup(windowType))
            {
                case FFTWindowGroup.Cosine:
                    return GeneralizedCosineWindow(windowType, size, out cg, out enbw);
                case FFTWindowGroup.Sinc:
                    return GeneralizedSincWindow(windowType, size, out cg, out enbw, SINC_OVER_SAMPLE);
                default:
                    throw new ArgumentException("Unexpected Window Type");
            }
        }

        public static double[] GetWindow(FFTWindowType windowType, int size)
        {
            return GetWindow(windowType, size, 8);
        }

        private static float[] GetWindow_float(FFTWindowType windowType, int size, double parameter)
        {
            return Vector.ConvertToFloat(GetWindow(windowType, size, parameter));
        }

        /// <summary>
        /// 窗的参数
        /// </summary>
        public static double GetWindowENBW(FFTWindowType windowType)
        {
            double bw3dB, bw6dB, cg, enbw, mainLobeWidth;
            GeneralizedWindowCoef(windowType, out cg, out enbw, out bw3dB, out bw6dB,out mainLobeWidth);
            return enbw;
        }

        public static double GetWindowBW3dB(FFTWindowType windowType)
        {
            double bw3dB, bw6dB, cg, enbw, mainLobeWidth;
            GeneralizedWindowCoef(windowType, out cg, out enbw, out bw3dB, out bw6dB, out mainLobeWidth);
            return bw3dB;
        }

        public static double GetWindowBW6dB(FFTWindowType windowType)
        {
            double bw3dB, bw6dB, cg, enbw, mainLobeWidth;
            GeneralizedWindowCoef(windowType, out cg, out enbw, out bw3dB, out bw6dB, out mainLobeWidth);
            return bw6dB;
        }

        public static double GetWindowMainLobeWidth(FFTWindowType windowType)
        {
            double bw3dB, bw6dB, cg, enbw, mainLobeWidth;
            GeneralizedWindowCoef(windowType, out cg, out enbw, out bw3dB, out bw6dB, out mainLobeWidth);
            return mainLobeWidth;
        }

        /// <summary>
        /// 获取窗系数
        /// </summary>
        private static double[] GeneralizedWindowCoef(FFTWindowType windowType,
                                                      out double coherentGain, out double enbw,
                                                      out double bw3dB, out double bw6dB, out double mainLobeWidth)
        {
            // 常量查表法
            double[] windowCoe;

            switch (windowType)
            {
                // Rectangle
                case FFTWindowType.None:
                    windowCoe = new double[] { 1.0 };
                    enbw = 1;
                    bw3dB = 0.89;
                    bw6dB = 1.21;
                    break;
                // Hanning
                case FFTWindowType.Hanning:
                    windowCoe = new double[] { 0.5, -0.5 };
                    enbw = 1.5;
                    bw3dB = 1.4406;
                    bw6dB = 2;
                    break;
                // Hamming
                case FFTWindowType.Hamming:
                    windowCoe = new double[] { 0.54, -0.46 };
                    enbw = 1.362826000;
                    bw3dB = 1.303000000;
                    bw6dB = 1.820000000;
                    break;
                // Blackman Harris
                case FFTWindowType.Blackman_Harris:
                    windowCoe = new double[] { 0.42323, -0.49755, 0.07922 };
                    enbw = 1.708538000;
                    bw3dB = 1.623500000;
                    bw6dB = 2.270000000;
                    break;
                // Exact Blackman
                case FFTWindowType.Exact_Blackman:
                    windowCoe = new double[] { 7938.0 / 18608.0, -9240.0 / 18608.0, 1430.0 / 18608.0 };
                    enbw = 1.693699000;
                    bw3dB = 1.608700000;
                    bw6dB = 2.250000000;
                    break;
                // Blackman
                case FFTWindowType.Blackman:
                    windowCoe = new double[] { 0.42, -0.5, 0.08 };
                    enbw = 1.726757000;
                    bw3dB = 1.643700000;
                    bw6dB = 2.300000000;
                    break;
                // 4 B-Harris
                case FFTWindowType.Four_Term_B_Harris:
                    windowCoe = new double[] { 0.35875, -0.48829, 0.14128, -0.01168 };
                    enbw = 2.004353000;
                    bw3dB = 1.899400000;
                    bw6dB = 2.670000000;
                    break;
                case FFTWindowType.Four_Term_Blackman_Nuttal:
                    windowCoe = new double[] { 3.635819267707608e-001, -4.891774371450171e-001, 1.365995139786921e-001, -1.064112210553003e-002 };
                    enbw = 1.976111700;
                    bw3dB = 1.870000000;
                    bw6dB = 2.630000000;
                    break;
                case FFTWindowType.Five_Term_Least_Sidelobe:
                    windowCoe = new double[] { 3.232153788877343e-001, -4.714921439576260e-001, 1.755341299601972e-001, -2.849699010614994e-002, 1.261357088292677e-003 };
                    enbw = 2.215350783;
                    bw3dB = 2.094900000;
                    bw6dB = 2.950000000;
                    break;
                case FFTWindowType.Six_Term_Least_Sidelobe:
                    windowCoe = new double[] { 2.935578950102797e-001, -4.519357723474506e-001, 2.014164714263962e-001, -4.792610922105837e-002, 5.026196426859393e-003, -1.375555679558877e-004 };
                    enbw = 2.433904184;
                    bw3dB = 2.299046958;
                    bw6dB = 3.236208601;
                    break;
                // 7 B-Harris
                case FFTWindowType.Seven_Term_B_Harris:
                    windowCoe = new double[] { 2.712203605850388e-001, -4.334446123274422e-001, 2.180041228929303e-001, -6.578534329560609e-002, 1.076186730534183e-002, -7.700127105808265e-004, 1.368088305992921e-005 };
                    enbw = 2.631905000;
                    bw3dB = 2.484100000;
                    bw6dB = 3.500000000;
                    break;
                // Flat Top
                case FFTWindowType.Flat_Top_90D:
                    windowCoe = new double[] { 0.209783021421, -0.407525336544, 0.281175959705, -0.0924746634556, 0.00904101887418 };
                    enbw = 3.883167166;
                    bw3dB = 3.835862180;
                    bw6dB = 4.711923977;
                    break;
                case FFTWindowType.Flat_Top_95:
                    windowCoe = new double[] { 0.213640903311, -0.414108259879, 0.278698873916, -0.0860603241582, 0.00749163873597 };
                    enbw = 3.811213009;
                    bw3dB = 3.762901021;
                    bw6dB = 4.623043646;
                    break;
                case FFTWindowType.Six_Term_Flat_Top:
                    windowCoe = new double[] { 0.192240452512, -0.37631789481, 0.284144941765, -0.122407781678, 0.0236146057221, -0.00127432351161 };
                    enbw = 4.218610289;
                    bw3dB = 4.162121341;
                    bw6dB = 5.126945598;
                    break;
                case FFTWindowType.Seven_Term_Flat_Top:
                    windowCoe = new double[] { 0.178153071078, -0.350534041444, 0.281452647671, -0.144524263157, 0.0402333021358, -0.00494169539905, 0.000160979115026 };
                    enbw = 4.538604789;
                    bw3dB = 4.474340485;
                    bw6dB = 5.522132326;
                    break;
                 case FFTWindowType.Sinc:
                    windowCoe = new double[] { 1.0 };
                    enbw = 1.11193;
                    bw3dB = 1.05045;
                    bw6dB = 1.10393;
                    break;
                case FFTWindowType.Sinc_Hamming:
                    windowCoe = new double[] { 1.0 };
                    enbw = 1.00438;
                    bw3dB = 0.99971;
                    bw6dB = 1.09995;
                    break;
                default:
                    throw new ArgumentException("Unexpected Window Type");
            }
            coherentGain = windowCoe[0];
            mainLobeWidth = windowCoe.Length * 2;
            return windowCoe;
        }

        public static double[] GeneralizedCosineWindowCoefNormalized(FFTWindowType windowType)
        {
            if (GetWindowGroup(windowType) != FFTWindowGroup.Cosine) { throw new ArgumentException("Unexpected Window Type."); }

            double bw3dB, bw6dB, cg, enbw, mainLobeWidth;
            double[] windowCoe = GeneralizedWindowCoef(windowType, out cg, out enbw, out bw3dB, out bw6dB, out mainLobeWidth);
            for (int i = 0; i < windowCoe.Length; i++)
            {
                windowCoe[i] /= cg;
            }
            return windowCoe;
        }

        /// <summary>
        /// 生成余弦窗函数
        /// </summary>
        private static double[] GeneralizedCosineWindow(FFTWindowType windowType, int size, out double cg, out double enbw)
        {
            #region 判断窗类型
            if (GetWindowGroup (windowType) != FFTWindowGroup.Cosine) { throw new ArgumentException("Unexpected Window Type."); }
            #endregion

            #region 生成余弦窗(频率)系数
            double bw3dB, bw6dB, mainLobeWidth;
            double[] windowCoe = GeneralizedWindowCoef(windowType, out cg, out enbw, out bw3dB, out bw6dB, out mainLobeWidth);
            #endregion

            #region 生成余弦窗
            double[] windowdata = new double[size];

            double deltaT = 2.0 * Math.PI / windowdata.Length;
            double actualT = 0;
            double dcPower = 0;

            for (int i = 0; i < windowdata.Length; i++)
            {
                windowdata[i] = 0;
                actualT = deltaT * i;
                // 对于任意时刻t, 信号幅度等于所有频率在该时刻的幅度和
                for (int j = 0; j < windowCoe.Length; j++)
                {
                    windowdata[i] += windowCoe[j] * Math.Cos(actualT * j);  // 2*pi*freq*t, j表示频率
                }

                // 所有时刻幅度平均(求和), 即为DC
                dcPower += windowdata[i];
            }

            dcPower /= windowdata.Length;

            #endregion

            #region ---- 能量归一化 ----

            for (int i = 0; i < windowdata.Length; i++)
            {
                windowdata[i]  = windowdata[i] / dcPower;
            }

            #endregion

            #region ---- 任意窗 ENBW 定义 ----

            //for (int i = 0; i < size; i++)
            //{
            //    S1 += windowdata[i];
            //    S2 += Math.Pow(windowdata[i], 2);
            //}
            //enbw = windowdata.Length * S2 / Math.Pow(S1, 2);

            #endregion

            #region ---- 余弦窗 ENBW 求解
            //double S1 = 0;
            //double S2 = 0;
            //S1 = windowCoe[0];
            //S2 = 0;
            //for (int i = 0; i < windowCoe.Length; i++)
            //{
            //    S2 += windowCoe[i] * windowCoe[i] * 0.5;
            //}
            //enbw = S2 / (S1 * S1);

            #endregion

            return windowdata;
        }

        /// <summary>
        /// 生成Sinc窗函数
        /// </summary>
        private static double[] GeneralizedSincWindow(FFTWindowType windowType, int size, out double cg, out double enbw, int puleBins = 8)
        {
            #region 判断窗类型
            if (GetWindowGroup(windowType) != FFTWindowGroup.Sinc) { throw new ArgumentException("Unexpected Window Type."); }
            #endregion

            #region 生成余弦窗
            double[] cosineWindowData = GeneralizedCosineWindow((FFTWindowType)(windowType - FFTWindowType.Sinc), size, out cg, out enbw);
            double[] windowdata = new double[size];
            #endregion

            #region 生成Sinc窗
            double deltaT = puleBins * Math.PI / windowdata.Length* SINC_BW_SCALE;
            double offsetT = puleBins * Math.PI / (-2.0)* SINC_BW_SCALE;
            double dcPower = 0;
            double rmsPower = 0;
            double actualT = 0;

            for (int i = 0; i < windowdata.Length; i++)
            {               
                actualT = i * deltaT + offsetT;
                windowdata[i] = MathExtension.Sinc(actualT);
                windowdata[i] *= cosineWindowData[i];
                dcPower += windowdata[i];
                rmsPower += windowdata[i] * windowdata[i];
            }

            dcPower /= windowdata.Length;
            rmsPower /= windowdata.Length;
            #endregion

            #region ---- 能量归一化 ----

            for (int i = 0; i < windowdata.Length; i++)
            {
                windowdata[i] = windowdata[i] / dcPower / puleBins;
            }
            cg = 1;

            #endregion

            #region ---- 任意窗 ENBW 定义 ----

            enbw = rmsPower / (dcPower * dcPower); 

            #endregion

            return windowdata;
        }

        public static FFTWindowGroup GetWindowGroup(FFTWindowType windowType)
        {
            switch (windowType)
            {
                // 余弦窗
                case FFTWindowType.None:
                case FFTWindowType.Hanning:
                case FFTWindowType.Hamming:
                case FFTWindowType.Blackman_Harris:
                case FFTWindowType.Exact_Blackman:
                case FFTWindowType.Blackman:
                case FFTWindowType.Four_Term_B_Harris:
                case FFTWindowType.Four_Term_Blackman_Nuttal:
                case FFTWindowType.Five_Term_Least_Sidelobe:
                case FFTWindowType.Six_Term_Least_Sidelobe:
                case FFTWindowType.Seven_Term_B_Harris:
                case FFTWindowType.Flat_Top_90D:
                case FFTWindowType.Flat_Top_95:
                case FFTWindowType.Six_Term_Flat_Top:
                case FFTWindowType.Seven_Term_Flat_Top:
                    return FFTWindowGroup.Cosine;
                // Sinc窗
                case FFTWindowType.Sinc:
                case FFTWindowType.Sinc_Hamming:
                    return FFTWindowGroup.Sinc;
                // default
                default:
                    throw new ArgumentException("Unexpected Window Type");
            }
        }
    }

    /// <summary>
    /// <para>window type</para>
    /// <para>Chinese Simplified: 窗类型</para>
    /// </summary>
    public enum FFTWindowType : int //窗函数类型枚举
    {
        /// <summary>
        /// <para>Rectangle</para>
        /// <para>Chinese Simplified: 矩形窗</para>
        /// </summary>
        None,

        /// <summary>
        /// <para>Hanning (2-Term)</para>
        /// <para>Chinese Simplified: 汉宁窗</para>
        /// </summary>
        Hanning,

        /// <summary>
        /// <para>Hamming (2-Term)</para>
        /// <para>Chinese Simplified: 海明窗</para>
        /// </summary>
        Hamming,

        /// <summary>
        /// <para>Blackman Harris (3-Term)</para>
        /// <para>Chinese Simplified: 布莱克曼-哈里斯窗</para>
        /// </summary>
        Blackman_Harris,

        /// <summary>
        /// <para>Exact Blackman  (3-Term)</para>
        /// </summary>
        Exact_Blackman,

        /// <summary>
        /// <para>Blackman  (3-Term)</para>
        /// <para>Chinese Simplified: 布莱克曼窗</para>
        /// </summary>
        Blackman,

        /// <summary>
        /// <para>4 Term B-Harris</para>
        /// </summary>																																						
        Four_Term_B_Harris,

        /// <summary>
        /// 4_Term_Blackman_Nuttal(4-Term Minimum)
        /// </summary>
        Four_Term_Blackman_Nuttal,

        /// <summary>
        /// <para>5-Term Flat Top(HFT95), the highest sidelobe is −95.0dB, located at f = ±7.49 bins. </para>
        /// <para>Chinese Simplified: 平顶窗</para>	
        /// </summary>
        Flat_Top_95,

        /// <summary>
        /// <para>5-Term Flat Top(HFT90D),the highest sidelobe is −90.2dB, located at f = ±5.58 bins</para>
        /// <para>Chinese Simplified: 平顶窗</para>	
        /// </summary>
        Flat_Top_90D,

        /// <summary>
        /// 5_Term_Least_Sidelobe(Low Sidelobe)
        /// </summary>
        Five_Term_Least_Sidelobe,

        /// <summary>
        /// 6_Term_Flat_Top(HFT116D),the highest sidelobe is −116.8dB, located at f = ±7.52 bins
        /// </summary>
        Six_Term_Flat_Top,

        /// <summary>
        /// 6_Term_Least_Sidelobe
        /// </summary>
        Six_Term_Least_Sidelobe,

        /// <summary>
        /// 7_Term_Flat_Top(HFT144D),the highest sidelobe is −144.1dB, located at f = ±7.07 bins.
        /// </summary>
        Seven_Term_Flat_Top,

        /// <summary>
        /// 7_Term_Least_Sidelobe(7-Term B-Harris)
        /// </summary>
        Seven_Term_B_Harris,

        /// <summary>
        /// Sinc
        /// </summary>
        Sinc =256,

        /// <summary>
        /// SincHamming
        /// </summary>
        Sinc_Hamming =258,

        ///// <summary>
        ///// <para>Kaiser</para>
        ///// <para>Chinese Simplified: 凯塞窗</para>	
        ///// </summary>
        //Kaiser,
    }

    /// <summary>
    /// 窗分类
    /// </summary>
    public enum FFTWindowGroup
    {
        /// <summary>
        /// <para>Cosine</para>
        /// <para>Chinese Simplified: 余弦窗</para>
        /// </summary>
        Cosine,

        /// <summary>
        /// <para>Sinc</para>
        /// <para>Chinese Simplified: Sinc窗</para>
        /// </summary>
        Sinc,
    }
}
