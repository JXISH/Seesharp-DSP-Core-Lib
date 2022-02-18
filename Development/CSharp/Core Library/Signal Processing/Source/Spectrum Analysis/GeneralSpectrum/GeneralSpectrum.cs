using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using SeeSharpTools.JXI.Exception;
using SeeSharpTools.JXI.MKL;
using SeeSharpTools.JXI.SignalProcessing.Window;

namespace SeeSharpTools.JXI.SignalProcessing.GeneralSpectrum
{

    /// <summary>
    /// <para>Computes spectrum of _demoniator time-domain signal.</para>
    /// <para>Chinese Simplified: 计算信号的频谱。</para>
    /// </summary>
    public class GeneralSpectrumTask
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataType">输入数据类型</param>
        /// <param name="sampleRate">采样率</param>
        public GeneralSpectrumTask(InputDataType dataType, double sampleRate)
        {

            //modeChangeFlag = false;
            SpectralInfomation = new SpectralInformation();
            Unit = new SpectrumUnitInfo();
            Unit.Impedance = 50;
            _adjustedSampleRate = 10;
            WindowPara = 0;
            // Committed = false;
            InputDataType = dataType;
            _amplitudeAverageAux = new SpectrumAverageAux();
            Average = new SpectrumAverage(this);
            _dftiDescMgr = DFTIDescMgr.GetInstance();
            Output = new SpectrumOutput(this);
            SampleRate = sampleRate;
            _ENBW = 1;
            //WindowPara = 0;
            ////Committed = false;
            //InputDataType = dataType;
            //Output = new SpectrumOutput(this);
            //SampleRate = sampleRate;
            //Unit = new SpectrumUnitInfo();
            //Unit.Impedance = 50;
            //_amplitudeAverageAux = new SpectrumAverageAux();
            //Average = new SpectrumAverage(this);
            //SpectralInfomation = new SpectralInformation();
            //_dftiDescMgr = DFTIDescMgr.GetInstance();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public GeneralSpectrumTask()
        {
            //modeChangeFlag = false;
            SpectralInfomation = new SpectralInformation();
            Unit = new SpectrumUnitInfo();
            Unit.Impedance = 50;
            _adjustedSampleRate = 10;
            WindowPara = 0;
           // Committed = false;
            InputDataType = InputDataType.Real;
            _amplitudeAverageAux = new SpectrumAverageAux();
            Average = new SpectrumAverage(this);
            _dftiDescMgr = DFTIDescMgr.GetInstance();
            Output = new SpectrumOutput(this);
            SampleRate = 0;
            _ENBW = 1;
        }

        /// <summary>
        /// 构造函数 
        /// </summary>
        static GeneralSpectrumTask()
        {
        }

        #region --------------Private: 私有字段/属性-------------

        private DFTIDescMgr _dftiDescMgr;

        /// <summary>
        /// 任务是否已经提交
        /// </summary>
        //private bool Committed;

        //private SpectrumAverageAux _amplitudeAverageAux;
        internal SpectrumAverageAux _amplitudeAverageAux;

        /// <summary>
        /// Sqrt 2
        /// </summary>
        private const double Sqrt2 = 1.4142135623730950488016887242097;

        private double[] _averagedSpectrum;


        private double _ENBW;
        #endregion

        #region --------------Public: 公共属性-------------------
        /// <summary>
        /// <para>The Type of input data，default Real</para>
        /// <para>Chinese Simplified:  输入数据类型，默认为实数。</para>
        /// </summary>
        public InputDataType InputDataType { get; set; }

        private double _adjustedSampleRate;
        /// <summary>
        /// <para>Sample rate</para>
        /// <para>Chinese Simplified: 采样率。</para>
        /// </summary>
        public double SampleRate
        {
            get
            {
                return _adjustedSampleRate;
            }
            set
            {
                _adjustedSampleRate = value;
                var isRealType = InputDataType.Real == InputDataType;
                //实数FFT运算的点数=谱线数*2，复数FFT运算的点数=谱线数
                SpectralInfomation.FFTSize = isRealType ? 2 * Output.NumberOfLines : Output.NumberOfLines;
                //实数的df=Fs/FFTSize,复数的df=Fs/谱线数
                SpectralInfomation.FreqDelta = isRealType ? (_adjustedSampleRate / SpectralInfomation.FFTSize) : (_adjustedSampleRate / (int)Output.NumberOfLines);
                SpectralInfomation.FreqStart = isRealType ? 0 : -_adjustedSampleRate / SpectralInfomation.FFTSize * Math.Floor(SpectralInfomation.FFTSize/2.0);               
            }

        }

        /// <summary>
        /// <para>The Type of FFT window</para>
        /// <para>Chinese Simplified: 频谱加窗的类型。</para>
        /// </summary>
        public WindowType WindowType { get; set; }

        /// <summary>
        /// <para>The parameter of FFT window</para>
        /// <para>Chinese Simplified: 对于特定的窗类型(如Kaiser、Gaussian等)，可设定窗系数，暂不可用。</para>
        /// </summary>
        public double WindowPara { get; }

        /// <summary>
        /// <para>The parameter of spectrum</para>
        /// <para>Chinese Simplified: 频谱输出参数。</para>
        /// </summary>
        public SpectrumOutput Output { get; }

        /// <summary>
        /// <para>The parameter of spectrum unit</para>
        /// <para>Chinese Simplified: 频谱单位参数。</para>
        /// </summary>
        public SpectrumUnitInfo Unit { get; }

        /// <summary>
        /// <para>The parameter of spectrum average</para>
        /// <para>Chinese Simplified: 频谱平均参数。</para>
        /// </summary>
        public SpectrumAverage Average { get; }

        /// <summary>
        /// <para>The information of spectrum （read only）</para>
        /// <para>Chinese Simplified: 频谱输出信息(只读)。</para>
        /// </summary>
        public SpectralInformation SpectralInfomation { get; }
        #endregion

        #region --------------Public: 公共方法-------------------

        
        /// <summary>
        /// <para>Reset the FFTCount</para>
        /// <para>Chinese Simplified: 重置FFTCount属性值，若配置了频谱平均，则频谱平均历史也会被清空，即重新开始平均。</para>
        /// </summary>
        public void Reset()
        {
           // Committed = false;
            SpectralInfomation.FFTCount = 0;
            _amplitudeAverageAux.Reset();
        }


        /// <summary>
        /// <para>Get spectrum</para>
        /// <para>Chinese Simplified: 输入时域波形并获取频谱</para>
        /// </summary>
        /// <param name="data">
        /// <para>Waveform of input</para>
        /// <para>Chinese Simplified:时域数据（实信号）</para>
        /// </param>
        /// <param name="spectrum">
        /// <para>Spectrum of output</para>
        /// <para>Chinese Simplified:输出的频谱存放数组</para>
        /// </param>
        public void GetSpectrum(double[] data, ref double[] spectrum)
        {
            //if(Unit.IsPSD && Unit.Type!= SpectrumOutputUnit.V2)
            //{
            //    throw new JXIParamException("if IsPSD=true,the unit of output must be SpectrumOutputUnit.V2");
            //}
            #region 原Commit（）功能
            if (Output.Type == SpectrumOutputType.ByRBW) //ByRBW 暂时未实现
            {
                throw new NotImplementedException();
            }
            if (Output.NumberOfLines <= 0) //谱线数应大于0
            {
                throw new JXIParamException("NumberOfLines must be greater than 0.");
            }
            //提交时如果没有对应类型的描述符则生成
            var dftiType = DFTType.Double1DRealInComplexOut;
            _dftiDescMgr.GetDFTDesc(2 * (int)Output.NumberOfLines, dftiType); //生成对应的描述符（如果没有）
            _averagedSpectrum = new double[Output.NumberOfLines];
            #endregion 
            if (InputDataType == InputDataType.Complex)
            {
                throw new JXIParamException("Can not call this method when InputDataType is Complex.");
            }
            if (spectrum.Length < Output.NumberOfLines)
            {
                throw new JXIParamException("spectrum buffer overflow.");
            }
            //if (Committed == false)
            //{
            //    throw new JXIDSPInnerException("Commit First.");
            //}
            //var tempData = data; 
            //if (data.Length < SpectralInfomation.FFTSize)
            //{
                var tempData = new double[SpectralInfomation.FFTSize];
                CBLASNative.cblas_dcopy(Math.Min(SpectralInfomation.FFTSize, data.Length), data, 1, tempData, 1);
            //}

            //生成窗函数的数据
            var windowData = new double[SpectralInfomation.FFTSize];
            var xOutCTmp = new Complex[Output.NumberOfLines + 1];
            double cg, enbw;
            SeeSharpTools.JXI.SignalProcessing.Window.Window.GetWindow(WindowType, ref windowData, out cg, out enbw);
            _ENBW = enbw;
            CBLASNative.cblas_dscal(windowData.Length, 1 / cg, windowData, 1); //窗系数归一化
            VMLNative.vdMul(windowData.Length, windowData, tempData, tempData);//data通过窗函数
            BasicFFT.RealFFT(tempData, ref xOutCTmp);//实数信号的FFT计算
            SpectralInfomation.FFTCount++;
            VMLNative.vzAbs(Output.NumberOfLines, xOutCTmp, spectrum);//计算向量元素的绝对值

            double scale = Sqrt2 / SpectralInfomation.FFTSize; //双边到单边有一个二倍关系,输出为Vrms(有效电压)要除以根号2

            //fftcnt次的频谱做平均
            CBLASNative.cblas_dscal(Output.NumberOfLines, scale, spectrum, 1);//用标量计算向量的乘积

            spectrum[0] = spectrum[0] / Sqrt2; //零频无2倍关系，RMS（root meam square 均方根值即有效值）不用除以根号2


            //Unit Conversion(单位转换)
            var unitSettings = new UnitConvSetting(Unit.Type, PeakScaling.Rms, Unit.Impedance, Unit.IsPSD);
            Spectrum.UnitConversion(spectrum, SpectralInfomation.FreqDelta, SpectrumType.Amplitude,
                                    unitSettings, enbw);
            if (Average.Mode != SpectrumAverageMode.NoAveraging)
            {
                _amplitudeAverageAux.AverageSpectrum(spectrum);
            }
        }

        /// <summary>
        /// <para>Get spectrum</para>
        /// <para>Chinese Simplified: 输入时域波形并获取频谱</para>
        /// </summary>
        /// <param name="data">
        /// <para>Waveform of input</para>
        /// <para>Chinese Simplified:时域数据（复信号）</para>
        /// </param>
        /// <param name="spectrum">
        /// <para>Spectrum of output</para>
        /// <para>Chinese Simplified:输出的频谱存放数组</para>
        /// </param>
        public void GetSpectrum(Complex[] data, ref double[] spectrum)
        {
            //if (Unit.IsPSD && Unit.Type != SpectrumOutputUnit.V2)
            //{
            //    throw new JXIParamException("if IsPSD=true,the unit of output must be SpectrumOutputUnit.V2");
            //}
            #region 原Commit（）功能
            if (Output.Type == SpectrumOutputType.ByRBW) //ByRBW 暂时未实现
            {
                throw new NotImplementedException();
            }
            if (Output.NumberOfLines <= 0) //谱线数应大于0
            {
                throw new JXIParamException("NumberOfLines must be greater than 0.");
            }
            //提交时如果没有对应类型的描述符则生成
            var dftiType = DFTType.Double1DComplexInComplexOut;
            _dftiDescMgr.GetDFTDesc(2 * (int)Output.NumberOfLines, dftiType); //生成对应的描述符（如果没有）
            _averagedSpectrum = new double[Output.NumberOfLines];
            #endregion 
            if (InputDataType == InputDataType.Real)
            {
                throw new JXIParamException("Can not call this method when InputDataType is Real.");
            }
            if (spectrum.Length < Output.NumberOfLines)
            {
                throw new JXIParamException("spectrum buffer overflow.");
            }
            //if (Committed == false)
            //{
            //    throw new JXIDSPInnerException("Commit First.");
            //}
            //var tempData = data;
            //if (data.Length < SpectralInfomation.FFTSize)
            //{
            var tempData = new Complex[SpectralInfomation.FFTSize];
            //CBLASNative.cblas_zcopy(data.Length, data, 1, tempData, 1);

            var gchInput = GCHandle.Alloc(data, GCHandleType.Pinned);//钉住data，它的内存地址固定，而不被垃圾回收掉，然后最后我们自己管理，自己释放内存
            var inputPtr = gchInput.AddrOfPinnedObject();
            var gchData = GCHandle.Alloc(tempData, GCHandleType.Pinned);
            var dataPtr = gchData.AddrOfPinnedObject();

            CBLASNative.cblas_dcopy(Math.Min(SpectralInfomation.FFTSize, data.Length) * 2, inputPtr, 1, dataPtr, 1);
            gchInput.Free();
            //}

            //生成窗函数的数据
            var windowData = new double[SpectralInfomation.FFTSize];
            var windowDataC = new double[SpectralInfomation.FFTSize * 2];

            var gchWindow = GCHandle.Alloc(windowDataC, GCHandleType.Pinned);
            var windowPtr = gchWindow.AddrOfPinnedObject();



            var xOutCTmp = new Complex[Output.NumberOfLines];
            double cg, enbw;
            SeeSharpTools.JXI.SignalProcessing.Window.Window.GetWindow(WindowType, ref windowData, out cg, out enbw);
            _ENBW = enbw;
            CBLASNative.cblas_dscal(windowData.Length, 1 / cg, windowData, 1); //窗系数归一化
            CBLASNative.cblas_dcopy(windowData.Length, windowData, 1, windowPtr, 2);
            CBLASNative.cblas_dcopy(windowData.Length, windowData, 1, windowPtr + sizeof(double), 2);//实部、虚部都要加窗
            VMLNative.vdMul(windowData.Length * 2, windowDataC, dataPtr, dataPtr);
            gchWindow.Free();
            gchData.Free();

            BasicFFT.ComplexFFT(tempData, ref xOutCTmp);
            SpectralInfomation.FFTCount++;
            VMLNative.vzAbs(Output.NumberOfLines, xOutCTmp, spectrum);

            double scale = 1.0 / SpectralInfomation.FFTSize; //复数输出为Vrms，不需要除以根号2

            //fftcnt次的频谱做平均
            CBLASNative.cblas_dscal(Output.NumberOfLines, scale, spectrum, 1);
            //spectrum[0] = spectrum[0] * Sqrt2; //零频RMS不用除以根号2

            //Unit Conversion
            var unitSettings = new UnitConvSetting(Unit.Type, PeakScaling.Rms, Unit.Impedance, Unit.IsPSD);
            Spectrum.UnitConversion(spectrum, SpectralInfomation.FreqDelta, SpectrumType.Amplitude,
                                    unitSettings, enbw);

            //var tempSpec = new double[spectrum.Length / 2 + 1];
            //CBLASNative.cblas_dcopy(Output.NumberOfLines / 2 + 1, spectrum, 1, tempSpec, 1);
            var gchSpec = GCHandle.Alloc(spectrum, GCHandleType.Pinned);
            var specPtr = gchSpec.AddrOfPinnedObject();
            if (Output.NumberOfLines % 2 == 0)
            {
                var tempSpec = new double[spectrum.Length / 2 ];
                CBLASNative.cblas_dcopy(Output.NumberOfLines / 2 , spectrum, 1, tempSpec, 1);

                CBLASNative.cblas_dcopy(Output.NumberOfLines / 2 ,
                                        specPtr + sizeof(double) * (Output.NumberOfLines / 2 ), 1, spectrum, 1);
                CBLASNative.cblas_dcopy(Output.NumberOfLines / 2 ,
                                        tempSpec, 1, specPtr + sizeof(double) * (Output.NumberOfLines / 2), 1);
            }
            else
            {
                var tempSpec = new double[spectrum.Length / 2 + 1];
                CBLASNative.cblas_dcopy(Output.NumberOfLines / 2 + 1, spectrum, 1, tempSpec, 1);
                CBLASNative.cblas_dcopy(Output.NumberOfLines / 2,
                                    specPtr + sizeof(double) * (Output.NumberOfLines / 2 + 1), 1, spectrum, 1);
                CBLASNative.cblas_dcopy(Output.NumberOfLines / 2 + 1,
                                        tempSpec, 1, specPtr + sizeof(double) * (Output.NumberOfLines / 2), 1);
            }
            gchSpec.Free();
            if (Average.Mode != SpectrumAverageMode.NoAveraging)
            {
                _amplitudeAverageAux.AverageSpectrum(spectrum);
            }
        }

        /// <summary>
        /// <para>Get spectrum</para>
        /// <para>Chinese Simplified: 输入时域波形并获取频谱</para>
        /// </summary>
        /// <param name="data">
        /// <para>Waveform of input</para>
        /// <para>Chinese Simplified:时域数据（实信号）</para>
        /// </param>
        /// <param name="amplitude">
        /// <para>amplitudet</para>
        /// <para>Chinese Simplified:输出的幅度谱存放数组</para>
        /// </param>
        /// <param name="phase">
        /// <para>phase</para>
        /// <para>Chinese Simplified:输出的相位谱存放数组</para>
        /// </param>
        public void GetSpectrum(double[] data, ref double[] amplitude, ref double[] phase)
        {
            //if (Unit.IsPSD && Unit.Type != SpectrumOutputUnit.V2)
            //{
            //    throw new JXIParamException("if IsPSD=true,the unit of output must be SpectrumOutputUnit.V2");
            //}
            #region 原Commit（）功能
            if (Output.Type == SpectrumOutputType.ByRBW) //ByRBW 暂时未实现
            {
                throw new NotImplementedException();
            }
            if (Output.NumberOfLines <= 0) //谱线数应大于0
            {
                throw new JXIParamException("NumberOfLines must be greater than 0.");
            }
            //提交时如果没有对应类型的描述符则生成
            var dftiType = DFTType.Double1DRealInComplexOut;
            _dftiDescMgr.GetDFTDesc(2 * (int)Output.NumberOfLines, dftiType); //生成对应的描述符（如果没有）
            _averagedSpectrum = new double[Output.NumberOfLines];
            #endregion 
            if (InputDataType == InputDataType.Complex)
            {
                throw new JXIParamException("Can not call this method when InputDataType is Complex.");
            }
            if (amplitude.Length < Output.NumberOfLines || phase.Length < Output.NumberOfLines)
            {
                throw new JXIParamException("buffer overflow.");
            }
            //if (Committed == false)
            //{
            //    throw new JXIDSPInnerException("Commit First.");
            //}
            if (Unit.Type != SpectrumOutputUnit.V)
            {
                throw new JXIParamException("This Method Only Support Unit as V");
            }
            //var tempData = data;
            //if (data.Length < SpectralInfomation.FFTSize)
            //{
                var tempData = new double[SpectralInfomation.FFTSize];
                CBLASNative.cblas_dcopy(Math.Min (SpectralInfomation.FFTSize, data.Length), data, 1, tempData, 1);
            //}

            //生成窗函数的数据
            var windowData = new double[SpectralInfomation.FFTSize];
            var xOutCTmp = new Complex[Output.NumberOfLines + 1];
            double cg, enbw;
            SeeSharpTools.JXI.SignalProcessing.Window.Window.GetWindow(WindowType, ref windowData, out cg, out enbw);
            _ENBW = enbw;
            CBLASNative.cblas_dscal(windowData.Length, 1 / cg, windowData, 1); //窗系数归一化
            VMLNative.vdMul(windowData.Length, windowData, tempData, tempData);
            BasicFFT.RealFFT(tempData, ref xOutCTmp);
            SpectralInfomation.FFTCount++;
            VMLNative.vzAbs(Output.NumberOfLines, xOutCTmp, amplitude);

            var xOutRe = new double[Output.NumberOfLines];
            var xOutIm = new double[Output.NumberOfLines];
            var gc = GCHandle.Alloc(xOutCTmp, GCHandleType.Pinned);
            var ptr = gc.AddrOfPinnedObject();
            CBLASNative.cblas_dcopy(Output.NumberOfLines, ptr, 2, xOutRe, 1);
            CBLASNative.cblas_dcopy(Output.NumberOfLines, ptr + sizeof(double), 2, xOutIm, 1);
            VMLNative.vdAtan2(Output.NumberOfLines, xOutIm, xOutRe,  phase); //计算相位
            gc.Free();

            double scale = Sqrt2 / SpectralInfomation.FFTSize; //双边到单边有一个二倍关系,输出为Vrms要除以根号2

            //fftcnt次的频谱做平均
            CBLASNative.cblas_dscal(Output.NumberOfLines, scale, amplitude, 1);

            amplitude[0] = amplitude[0] / Sqrt2; //零频无2倍关系，RMS不用除以根号2

            if (Average.Mode != SpectrumAverageMode.NoAveraging)
            {
                _amplitudeAverageAux.AverageSpectrum(amplitude);
            }
        }

        /// <summary>
        /// <para>Get spectrum</para>
        /// <para>Chinese Simplified: 输入时域波形并获取频谱</para>
        /// </summary>
        /// <param name="data">
        /// <para>Waveform of input</para>
        /// <para>Chinese Simplified:时域数据（复信号）</para>
        /// </param>
        /// <param name="amplitude">
        /// <para>amplitudet</para>
        /// <para>Chinese Simplified:输出的幅度谱存放数组</para>
        /// </param>
        /// <param name="phase">
        /// <para>phase</para>
        /// <para>Chinese Simplified:输出的相位谱存放数组</para>
        /// </param>
        public void GetSpectrum(Complex[] data, ref double[] amplitude, ref double[] phase)
        {
            //if (Unit.IsPSD && Unit.Type != SpectrumOutputUnit.V2)
            //{
            //    throw new JXIParamException("if IsPSD=true,the unit of output must be SpectrumOutputUnit.V2");
            //}
            #region 原Commit（）功能
            if (Output.Type == SpectrumOutputType.ByRBW) //ByRBW 暂时未实现
            {
                throw new NotImplementedException();
            }
            if (Output.NumberOfLines <= 0) //谱线数应大于0
            {
                throw new JXIParamException("NumberOfLines must be greater than 0.");
            }
            //提交时如果没有对应类型的描述符则生成
             var   dftiType = DFTType.Double1DComplexInComplexOut;
            _dftiDescMgr.GetDFTDesc(2 * (int)Output.NumberOfLines, dftiType); //生成对应的描述符（如果没有）
            _averagedSpectrum = new double[Output.NumberOfLines];
            #endregion 
            if (InputDataType == InputDataType.Real)
            {
                throw new JXIParamException("Can not call this method when InputDataType is Real.");
            }
            if (amplitude.Length < Output.NumberOfLines)
            {
                throw new JXIParamException("spectrum buffer overflow.");
            }
            //if (Committed == false)
            //{
            //    throw new JXIDSPInnerException("Commit First.");
            //}
            //var tempData = data;
            //if (data.Length < SpectralInfomation.FFTSize)
            //{
                var tempData = new Complex[SpectralInfomation.FFTSize];
            //CBLASNative.cblas_zcopy(data.Length, data, 1, tempData, 1);

            var gchInput = GCHandle.Alloc(data, GCHandleType.Pinned);
            var inputPtr = gchInput.AddrOfPinnedObject();
            var gchData = GCHandle.Alloc(tempData, GCHandleType.Pinned);
            var dataPtr = gchData.AddrOfPinnedObject();

            CBLASNative.cblas_dcopy(Math.Min(SpectralInfomation.FFTSize, data.Length) * 2, inputPtr, 1, dataPtr, 1);
            gchInput.Free();
            //}

            //生成窗函数的数据
            var windowData = new double[SpectralInfomation.FFTSize];
            var windowDataC = new double[SpectralInfomation.FFTSize * 2];

            var gchWindow = GCHandle.Alloc(windowDataC, GCHandleType.Pinned);
            var windowPtr = gchWindow.AddrOfPinnedObject();

            var xOutCTmp = new Complex[Output.NumberOfLines];
            double cg, enbw;
            SeeSharpTools.JXI.SignalProcessing.Window.Window.GetWindow(WindowType, ref windowData, out cg, out enbw);
            _ENBW = enbw;
            CBLASNative.cblas_dscal(windowData.Length, 1 / cg, windowData, 1); //窗系数归一化
            CBLASNative.cblas_dcopy(windowData.Length, windowData, 1, windowPtr, 2);
            CBLASNative.cblas_dcopy(windowData.Length, windowData, 1, windowPtr + sizeof(double), 2);
            VMLNative.vdMul(windowData.Length * 2, windowDataC, dataPtr, dataPtr);
            gchWindow.Free();
            gchData.Free();

            BasicFFT.ComplexFFT(tempData, ref xOutCTmp);
            SpectralInfomation.FFTCount++;
            VMLNative.vzAbs(Output.NumberOfLines, xOutCTmp, amplitude);

            var xOutRe = new double[Output.NumberOfLines];
            var xOutIm = new double[Output.NumberOfLines];
            var gc = GCHandle.Alloc(xOutCTmp, GCHandleType.Pinned);
            var ptr = gc.AddrOfPinnedObject();
            CBLASNative.cblas_dcopy(Output.NumberOfLines, ptr, 2, xOutRe, 1);
            CBLASNative.cblas_dcopy(Output.NumberOfLines, ptr + sizeof(double), 2, xOutIm, 1);

            VMLNative.vdAtan2(Output.NumberOfLines, xOutIm, xOutRe, phase); //计算相位
            gc.Free();

            double scale = 1.0 / (Sqrt2 * SpectralInfomation.FFTSize); //输出为Vrms要除以根号2

            //fftcnt次的频谱做平均
            CBLASNative.cblas_dscal(Output.NumberOfLines, scale, amplitude, 1);
            amplitude[0] = amplitude[0] * Sqrt2; //零频RMS不用除以根号2

            //var tempSpec = new double[amplitude.Length / 2 + 1];
            //CBLASNative.cblas_dcopy(Output.NumberOfLines / 2 + 1, amplitude, 1, tempSpec, 1);
            var gchSpec = GCHandle.Alloc(amplitude, GCHandleType.Pinned);
            var specPtr = gchSpec.AddrOfPinnedObject();

            if (Output.NumberOfLines % 2 == 0)
            {
                var tempSpec = new double[amplitude.Length / 2];
                CBLASNative.cblas_dcopy(Output.NumberOfLines / 2, amplitude, 1, tempSpec, 1);

                CBLASNative.cblas_dcopy(Output.NumberOfLines / 2,
                                        specPtr + sizeof(double) * (Output.NumberOfLines / 2), 1, amplitude, 1);
                CBLASNative.cblas_dcopy(Output.NumberOfLines / 2,
                                        tempSpec, 1, specPtr + sizeof(double) * (Output.NumberOfLines / 2), 1);
            }
            else
            {
                var tempSpec = new double[amplitude.Length / 2 + 1];
                CBLASNative.cblas_dcopy(Output.NumberOfLines / 2 + 1, amplitude, 1, tempSpec, 1);
                CBLASNative.cblas_dcopy(Output.NumberOfLines / 2,
                                    specPtr + sizeof(double) * (Output.NumberOfLines / 2 + 1), 1, amplitude, 1);
                CBLASNative.cblas_dcopy(Output.NumberOfLines / 2 + 1,
                                        tempSpec, 1, specPtr + sizeof(double) * (Output.NumberOfLines / 2), 1);
            }
            gchSpec.Free();

            //var tempPhase = tempSpec;
            //CBLASNative.cblas_dcopy(Output.NumberOfLines / 2 + 1, phase, 1, tempPhase, 1);
            var gchPhase = GCHandle.Alloc(phase, GCHandleType.Pinned);
            var phasePtr = gchPhase.AddrOfPinnedObject();
            //CBLASNative.cblas_dcopy(Output.NumberOfLines / 2 - 1,
            //                        phasePtr + sizeof(double) * (Output.NumberOfLines / 2 + 1), 1, phase, 1);
            //CBLASNative.cblas_dcopy(Output.NumberOfLines / 2 + 1,
            //                        tempPhase, 1, phasePtr + sizeof(double) * (Output.NumberOfLines / 2 - 1), 1);
            if (Output.NumberOfLines % 2 == 0)
            {
                var tempPhase = new double[phase.Length / 2];
                CBLASNative.cblas_dcopy(Output.NumberOfLines / 2, phase, 1, tempPhase, 1);

                CBLASNative.cblas_dcopy(Output.NumberOfLines / 2,
                                        phasePtr + sizeof(double) * (Output.NumberOfLines / 2), 1, phase, 1);
                CBLASNative.cblas_dcopy(Output.NumberOfLines / 2,
                                        tempPhase, 1, phasePtr + sizeof(double) * (Output.NumberOfLines / 2), 1);
            }
            else
            {
                var tempPhase = new double[phase.Length / 2 + 1];
                CBLASNative.cblas_dcopy(Output.NumberOfLines / 2 + 1, phase, 1, tempPhase, 1);
                CBLASNative.cblas_dcopy(Output.NumberOfLines / 2,
                                    phasePtr + sizeof(double) * (Output.NumberOfLines / 2 + 1), 1, phase, 1);
                CBLASNative.cblas_dcopy(Output.NumberOfLines / 2 + 1,
                                        tempPhase, 1, phasePtr + sizeof(double) * (Output.NumberOfLines / 2), 1);
            }
            gchPhase.Free();

            if (Average.Mode != SpectrumAverageMode.NoAveraging)
            {
                _amplitudeAverageAux.AverageSpectrum(amplitude);
            }
        }

        #region --------------频谱分析相关-----------------
        /// <summary>
        /// <para>Find peak</para>
        /// <para>Chinese Simplified:查找最大的峰值</para>
        /// </summary>
        /// <param name="spectrum">
        /// <para>Spectrum of input</para>
        /// <para>Chinese Simplified:输入频谱</para>
        /// </param>
        /// <returns>
        /// <para>Peak</para>
        /// <para>Chinese Simplified:输入频谱的最大值所对应的幅值、检索频率、加权频率</para>
        /// </returns>
        public Peak FindPeak(double[] spectrum)
        {
            if (spectrum.Length == 0)
            {
                throw new JXIParamException("输入数组不能为空");
            }
            Peak spectrumPeak = new Peak();//新建一个Peak对象
            int i, startIndex, endIndex;
            double powerInBand = 0;//加权频率时使用，频带内的幅值之和
            double powerMltIndex = 0;//加权频率时使用，频带内的幅值乘以频率之和
            double maxValue = spectrum.Max();//频谱的最大值
            int maxValueIndex = Array.FindIndex<double>(spectrum, s => s == maxValue);//频谱最大值的频率检索值
            //频率加权平均，取最大值前后3根谱线做加权平均
            startIndex = maxValueIndex - 3;
            spectrumPeak.PeakValue = maxValue;//最大值
            //modified by fwang @20190924 for fix peak freq near DC.
            if (startIndex < 0) //startIndex越界处理
            {
                startIndex = 0;
                spectrumPeak.PeakIndexFreq = maxValueIndex * SpectralInfomation.FreqDelta;
                spectrumPeak.PeakFrequency = maxValueIndex * SpectralInfomation.FreqDelta;
            }
            else
            {
                endIndex = startIndex + 7;
                if (endIndex > spectrum.Length - 1)//endIndex越界处理
                {
                    endIndex = spectrum.Length - 1;
                }
                double[] tempSpectrum = new double[endIndex - startIndex];//定义临时数组存放最大值以及前后3根谱线的幅值
                Array.Copy(spectrum, startIndex, tempSpectrum, 0, endIndex - startIndex);
                Spectrum.UnitConversion(ref tempSpectrum, Unit.Type);//加权平均时，单位必须是线性的，如果是db需要转换
                for (i = 0; i < tempSpectrum.Length; i++)
                {
                    powerInBand += tempSpectrum[i];
                    powerMltIndex += tempSpectrum[i] * (i + startIndex);
                }
                spectrumPeak.PeakIndexFreq = SpectralInfomation.FreqStart + maxValueIndex * SpectralInfomation.FreqDelta;//最大值的检索频率
                spectrumPeak.PeakFrequency = SpectralInfomation.FreqStart + powerMltIndex / powerInBand * SpectralInfomation.FreqDelta;//最大值的加权频率
            }
            return spectrumPeak;
        }

        /// <summary>
        /// <para>Find peak in range of [freqStart，freqStop]</para>
        /// <para>Chinese Simplified:查找[freqStart，freqStop]频段内的最大峰值</para>
        /// </summary>
        /// <param name="spectrum">
        /// <para>Spectrum of input</para>
        /// <para>Chinese Simplified:输入频谱</para>
        /// </param>
        /// <param name="freqStart">
        /// <para>Frequency of start</para>
        /// <para>Chinese Simplified:检索的开始频率</para>
        /// </param>
        /// <param name="freqStop">
        /// <para>Frequency of stopt</para>
        /// <para>Chinese Simplified:检索的结束频率</para>
        /// </param>
        /// <returns>
        /// <para>Peak</para>
        /// <para>Chinese Simplified:输入频谱的最大值所对应的幅值、检索频率、加权频率</para>
        /// </returns>
        public Peak FindPeak(double[] spectrum, double freqStart, double freqStop)
        {
            if (spectrum.Length == 0)
            {
                throw new JXIParamException("输入数组不能为空");
            }
            int freqStartIndex = (int)((freqStart - SpectralInfomation.FreqStart) / SpectralInfomation.FreqDelta);//开始检索
            int freqStopIndex  = (int)((freqStop  - SpectralInfomation.FreqStart) / SpectralInfomation.FreqDelta);//结束检索
            double[] tempSpectrumFreqStartTofreqStop = new double[freqStopIndex- freqStartIndex+1];//定义数组，保存需要检索的频段对应的幅值
            Array.Copy(spectrum, freqStartIndex, tempSpectrumFreqStartTofreqStop, 0, freqStopIndex - freqStartIndex+1);
            Peak spectrumPeak = new Peak();//新建一个Peak对象
            int i, startIndex, endIndex;
            double powerInBand = 0;//加权频率时使用，频带内的幅值之和
            double powerMltIndex = 0;//加权频率时使用，频带内的幅值乘以频率之和
            double maxValue = tempSpectrumFreqStartTofreqStop.Max();//频谱的最大值
            int maxValueIndex = Array.FindIndex<double>(tempSpectrumFreqStartTofreqStop, s => s == maxValue);//频谱最大值的频率检索值
            //频率加权平均，取最大值前后3根谱线做加权平均
            startIndex = maxValueIndex - 3;
            if (startIndex < 0) //startIndex越界处理
            {
                startIndex = 0;
            }
            endIndex = startIndex + 7;
            if (endIndex > tempSpectrumFreqStartTofreqStop.Length - 1)//endIndex越界处理
            {
                endIndex = tempSpectrumFreqStartTofreqStop.Length - 1;
            }
            double[] tempSpectrum = new double[endIndex - startIndex+1];//定义临时数组存放最大值以及前后3根谱线的幅值
            Array.Copy(tempSpectrumFreqStartTofreqStop, startIndex, tempSpectrum, 0, endIndex - startIndex+1);
            Spectrum.UnitConversion(ref tempSpectrum, Unit.Type);//加权平均时，单位必须是线性的，如果是db需要转换
            for (i = 0; i < tempSpectrum.Length; i++)
            {
                powerInBand += tempSpectrum[i];
                powerMltIndex += tempSpectrum[i] * ( i + startIndex+ freqStartIndex);
            }
            spectrumPeak.PeakValue = maxValue;//最大值
            spectrumPeak.PeakIndexFreq = SpectralInfomation.FreqStart + (maxValueIndex+ freqStartIndex) * SpectralInfomation.FreqDelta;//最大值的检索频率
            spectrumPeak.PeakFrequency = SpectralInfomation.FreqStart + powerMltIndex / powerInBand * SpectralInfomation.FreqDelta;//最大值的加权频率
            return spectrumPeak;
        }

        /// <summary>
        /// <para>Find peaks which above the threshold</para>
        /// <para>Chinese Simplified:检索频谱大于阈值的所有峰值</para>
        /// </summary>
        /// <param name="spectrum">
        /// <para>Spectrum of input</para>
        /// <para>Chinese Simplified:输入频谱</para>
        /// </param>
        /// <param name="threshold">
        /// <para>threshold</para>
        /// <para>Chinese Simplified:阈值</para>
        /// </param>
        /// <returns>
        /// <para>Peaks</para>
        /// <para>Chinese Simplified:大于阈值的所有峰值所对应的幅值、检索频率、加权频率</para>
        /// </returns>
        public Peak [] FindPeak(double[] spectrum,double threshold)
        {
            if (spectrum.Length==0)//判断数组是否为空
            {
                return null;
            }
            List<Peak> spectrumPeak=new List<Peak>();//不知道具体有几个峰值，所以定义链表
            Peak spectrumPeakOverthreshold = new Peak();//新建一个Peak对象
            int greaterThanThresholdIndex=0;//大于阈值的检索值
            int lessThanThresholdIndex=0;//小于阈值的检索值
            double greaterThanThresholdFreq=0.0;//大于阈值的频率
            double lessThanThresholdFreq=0.0;//小于阈值的频率
            int lastGreaterThanThresholdInde=0;//最后一个大于阈值的检索值
            int startIndex = 0;//检索开始值
            int stopIndex = spectrum.Length-1;//检索结束值

            //查找最后大于阈值的值
            if ((lastGreaterThanThresholdInde = Array.FindLastIndex(spectrum, s => s >= threshold) )< 0)
            {
                return null;
            }

            //查找峰值
            for (int i = startIndex; i <= stopIndex; )
            {
                if ((greaterThanThresholdIndex = Array.FindIndex<double>(spectrum, startIndex, s => s >= threshold)) >= 0)//从startIndex开始查找大于阈值的值，返回检索值
                {
                    greaterThanThresholdFreq = greaterThanThresholdIndex * SpectralInfomation.FreqDelta + SpectralInfomation.FreqStart;//大于阈值的检索频率
                    if ((lessThanThresholdIndex = Array.FindIndex<double>(spectrum, greaterThanThresholdIndex, s => s < threshold)) >= 0)//从greaterThanThresholdIndex开始查找小于阈值的值，返回检索值
                    {
                        lessThanThresholdFreq = lessThanThresholdIndex * SpectralInfomation.FreqDelta + SpectralInfomation.FreqStart;//小于阈值的检索频率
                        spectrumPeakOverthreshold = FindPeak(spectrum, greaterThanThresholdFreq, lessThanThresholdFreq);//查找指定频段的最大值
                        spectrumPeak.Add(spectrumPeakOverthreshold);//添加元素到链表
                        startIndex = lessThanThresholdIndex + 1;//检索开始值等于小于阈值的检索+1
                        i = startIndex;
                        if ((lastGreaterThanThresholdInde - lessThanThresholdIndex) == -1)//如果最后一个大于阈值的检索值-小于阈值的检索=-1，即当前为最后一个峰值
                        {
                            return spectrumPeak.ToArray();//将链表转成数组
                        }
                    }
                    else//没有找到小于阈值的值，则检索到数组最后一个元素
                    {
                        lessThanThresholdFreq = (spectrum.Length - 1) * SpectralInfomation.FreqDelta + SpectralInfomation.FreqStart;//检索结束频率
                        spectrumPeakOverthreshold = FindPeak(spectrum, greaterThanThresholdFreq, lessThanThresholdFreq);//检索指定频段内的最大值
                        spectrumPeak.Add(spectrumPeakOverthreshold);//添加到链表中
                        return spectrumPeak.ToArray();//将链表转成数组
                    }
                }
                else//没有大于阈值的值
                {
                    return null;
                }
            }
            
            return spectrumPeak.ToArray();
        }

        /// <summary>
        /// <para>Find peaks which above the threshold in range of [freqStart，freqStop]</para>
        /// <para>Chinese Simplified:检索[freqStart，freqStop]频段内大于阈值的所有峰值</para>
        /// </summary>
        /// <param name="spectrum">
        /// <para>Spectrum of input</para>
        /// <para>Chinese Simplified:输入频谱</para>
        /// </param>
        /// <param name="threshold">
        /// <para>threshold</para>
        /// <para>Chinese Simplified:阈值</para>
        /// </param>
        /// <param name="freqStart">
        /// <para>Frequency of start</para>
        /// <para>Chinese Simplified:检索的开始频率</para>
        /// </param>
        /// <param name="freqStop">
        /// <para>Frequency of stopt</para>
        /// <para>Chinese Simplified:检索的结束频率</para>
        /// </param>
        /// <returns>
        /// <para>Peaks</para>
        /// <para>Chinese Simplified:[freqStart，freqStop]频段内大于阈值的所有峰值所对应的幅值、检索频率、加权频率</para>
        /// </returns>
        public Peak[] FindPeak(double[] spectrum, double threshold, double freqStart, double freqStop)
        {
            if (spectrum.Length == 0)//判断数组是否为空
            {
                return null;
            }
            List<Peak> spectrumPeak = new List<Peak>();//不知道具体有几个峰值，所以定义链表
            Peak spectrumPeakOverthreshold = new Peak();//新建一个Peak对象
            int greaterThanThresholdIndex = 0;//大于阈值的检索值
            int lessThanThresholdIndex = 0;//小于阈值的检索值
            double greaterThanThresholdFreq = 0.0;//大于阈值的频率
            double lessThanThresholdFreq = 0.0;//小于阈值的频率
            int lastGreaterThanThresholdInde = 0;//最后一个大于阈值的检索值
            int startIndex = (int)((freqStart - SpectralInfomation.FreqStart) / SpectralInfomation.FreqDelta);//检索开始值
            int stopIndex = (int)((freqStop - SpectralInfomation.FreqStart) / SpectralInfomation.FreqDelta);//检索结束值

            //查找最后大于阈值的值
            if ((lastGreaterThanThresholdInde = Array.FindLastIndex(spectrum, s => s >= threshold)) < 0)
            {
                return null;
            }

            //查找峰值
            for (int i = startIndex; i <= stopIndex;)
            {
                if ((greaterThanThresholdIndex = Array.FindIndex<double>(spectrum, startIndex, s => s >= threshold)) >= 0)//从startIndex开始查找大于阈值的值，返回检索值
                {
                    greaterThanThresholdFreq = greaterThanThresholdIndex * SpectralInfomation.FreqDelta + SpectralInfomation.FreqStart;//大于阈值的检索频率
                    if ((lessThanThresholdIndex = Array.FindIndex<double>(spectrum, greaterThanThresholdIndex, s => s < threshold)) >= 0)//从greaterThanThresholdIndex开始查找小于阈值的值，返回检索值
                    {
                        lessThanThresholdFreq = lessThanThresholdIndex * SpectralInfomation.FreqDelta + SpectralInfomation.FreqStart;//小于阈值的检索频率
                        spectrumPeakOverthreshold = FindPeak(spectrum, greaterThanThresholdFreq, lessThanThresholdFreq);//查找指定频段的最大值
                        spectrumPeak.Add(spectrumPeakOverthreshold);//添加元素到链表
                        startIndex = lessThanThresholdIndex + 1;//检索开始值等于小于阈值的检索+1
                        i = startIndex;
                        if ((lastGreaterThanThresholdInde - lessThanThresholdIndex) == -1)//如果最后一个大于阈值的检索值-小于阈值的检索=-1，即当前为最后一个峰值
                        {
                            return spectrumPeak.ToArray();//将链表转成数组
                        }
                    }
                    else//没有找到小于阈值的值，则检索到数组最后一个元素
                    {
                        lessThanThresholdFreq = (spectrum.Length - 1) * SpectralInfomation.FreqDelta + SpectralInfomation.FreqStart;//检索结束频率
                        spectrumPeakOverthreshold = FindPeak(spectrum, greaterThanThresholdFreq, lessThanThresholdFreq);//检索指定频段内的最大值
                        spectrumPeak.Add(spectrumPeakOverthreshold);//添加到链表中
                        return spectrumPeak.ToArray();//将链表转成数组
                    }
                }
                else//没有大于阈值的值
                {
                    return null;
                }
            }

            return spectrumPeak.ToArray();
        }

        /// <summary>
        /// <para>Measure power in band</para>
        /// <para>Chinese Simplified:测量带内功率</para>
        /// </summary>
        /// <param name="spectrum">
        /// <para>spectrum</para>
        /// <para>Chinese Simplified:频谱</para>
        /// </param>
        /// <param name="centerFreq">
        /// <para>Center frequency</para>
        /// <para>Chinese Simplified:中心频率</para>
        /// </param>
        /// <param name="bandwidth">
        /// <para>Bandwidth</para>
        /// <para>Chinese Simplified:带宽</para>
        /// </param>
        /// <returns>
        /// <para>Power in band</para>
        /// <para>Chinese Simplified:带内功率</para>
        /// </returns>
        public double MeasurePowerInBand(double[] spectrum, double centerFreq, double bandwidth)
        {
            var spec = new double[spectrum.Length];
            Array.Copy(spectrum, spec, spectrum.Length);
            //判断参数的合理性
            if (InputDataType== InputDataType.Complex &&(centerFreq - bandwidth / 2 < -SampleRate / 2 || centerFreq + bandwidth / 2 > SampleRate / 2 || bandwidth<=0)||
                InputDataType == InputDataType.Real && (centerFreq - bandwidth / 2 < 0 || centerFreq + bandwidth / 2 > SampleRate / 2 || bandwidth <= 0)
                )
            {
                throw new JXIParamException("param error");
            }
            double PowerInBand = 0;
            double PowerInBandTem = 0;
            //开始频率
            double fStart = centerFreq - bandwidth / 2 < SpectralInfomation.FreqStart ? SpectralInfomation.FreqStart : centerFreq - bandwidth / 2;
            //结束频率
            double fStop = centerFreq + bandwidth / 2;
            //开始检索
            int fStartIndex = (int)Math.Floor((fStart - SpectralInfomation.FreqStart) / SpectralInfomation.FreqDelta);
            //结束检索
            int fStopIndex = (int)Math.Floor((fStop - SpectralInfomation.FreqStart) / SpectralInfomation.FreqDelta);
            //是否为功率谱
            if (!Unit.IsPSD)
            {
                Spectrum.UnitConversionToV2(ref spec, Unit);//转换单位为V2
                //谱密度计算
                var scale = 1.0 / (_ENBW * SpectralInfomation.FreqDelta);
                CBLASNative.cblas_dscal(spec.Length, scale, spec, 1);
            }
            //计算带内功率
            for (int i = fStartIndex; i < fStopIndex; i++)
            {
                PowerInBandTem += spec[i] * SpectralInfomation.FreqDelta;
            }
            //判断带内是否刚好包括整数个df
            if (((fStart - SpectralInfomation.FreqStart) % SpectralInfomation.FreqDelta) != 0   )
            {
                if (fStartIndex >= 1)
                {
                    PowerInBandTem += spec[fStartIndex - 1] * ((fStart - SpectralInfomation.FreqStart) % SpectralInfomation.FreqDelta);
                }
            }

            if (((fStop - SpectralInfomation.FreqStart) % SpectralInfomation.FreqDelta) != 0   && fStopIndex != (spectrum.Length-1))
            {
                if (fStopIndex + 1 <= spectrum.Length - 1)
                {
                    PowerInBandTem += spec[fStopIndex + 1] * ((fStop - SpectralInfomation.FreqStart) % SpectralInfomation.FreqDelta);
                }

            }
            //将单位转换为用户设置的单位
            PowerInBand = Spectrum.UnitConversionV2ToOther(PowerInBandTem, Unit);
            return PowerInBand;
        }
        #endregion

        #endregion
    }
}
