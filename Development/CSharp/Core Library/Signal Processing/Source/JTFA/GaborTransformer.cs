using SeeSharpTools.JXI.SignalProcessing.Transform;
using SeeSharpTools.JXI.SignalProcessing.Window;
using System;
using System.Numerics;
using SeeSharpTools.JXI.Numerics;
using SeeSharpTools.JXI.Mathematics.LinearAlgebra.Matrix;
using SeeSharpTools.JXI.MKL;


namespace SeeSharpTools.JXI.SignalProcessing.JTFA
{
    /// <summary>
    /// Gabor 变换
    /// </summary>
    public class GaborTransformer
    {
        /// <summary>
        /// Gabor频域点数
        /// </summary>
        public int FrequencyBins { get; set; }
        
        /// <summary>
        /// Gabor时域步进
        /// </summary>
        public int TimeStep { get; set; }

        /// <summary>
        /// Gabor窗函数种类
        /// </summary>
        public WindowType WindowType { get; set; }

        /// <summary>
        /// Gabor窗函数长度
        /// </summary>
        public int WindowLength { get; set; }

        /// <summary>
        /// Gabor填充类型
        /// </summary>
        public enum PaddingType { noPadding, zeros, history, wrap }

        /// <summary>
        /// Gabor填充
        /// </summary>
        public PaddingType Padding { get; set; }

        /// <summary>
        /// Gabor解析窗
        /// </summary>
        public double[] AnalysisWindow { get => _analysisWindow;}

        /// <summary>
        /// Gabor还原窗
        /// </summary>
        public double[] SynthesisWindow { get => _synthesisWindow;}

        /// <summary>
        /// Gabor实数填充数据
        /// </summary>
        private double[] _padding;
        
        /// <summary>
        /// Gabor复数填充数据
        /// </summary>
        private Complex[] _paddingC;

        /// <summary>
        /// 实数Gabor，遗留数据
        /// </summary>
        private double[] _unprocessedWaveformReal;

        /// <summary>
        /// 复数Gabor，遗留数据
        /// </summary>
        private Complex[] _unprocessedWaveformComplex;

        /// <summary>
        /// Gabor解析窗, 内部参数
        /// </summary>
        private double[] _analysisWindow;

        /// <summary>
        /// Gabor还原窗， 内部参数
        /// </summary>
        private double[] _synthesisWindow;

        /// <summary>
        /// Gabor Transformer 默认参数
        /// </summary>
        public GaborTransformer()
        {
            FrequencyBins = 512;
            TimeStep = 128;
            WindowType = WindowType.Hanning;
            WindowLength = 512;
            Padding = PaddingType.noPadding;
        }

        /// <summary>
        /// 根据窗类型生成解析与还原双窗函数
        /// </summary>
        /// <param name="windowType">窗类型</param>
        public void GenerateDualWindow(WindowType windowType) 
        {
            _analysisWindow = new double[WindowLength];
            _synthesisWindow = new double[WindowLength];
            double cg, enbw;
            WindowType = windowType;
            SeeSharpTools.JXI.SignalProcessing.Window.Window.GetWindow(WindowType, ref _analysisWindow, out cg, out enbw);
            CBLASNative.cblas_dscal(_analysisWindow.Length, 1 / cg, _analysisWindow, 1); //窗系数归一化
            //功率归一化
            VMLNative.vdSqr(_analysisWindow.Length, _analysisWindow, _analysisWindow);
            double windowdataSum = CBLASNative.cblas_dasum(_analysisWindow.Length, _analysisWindow, 1);
            CBLASNative.cblas_dscal(_analysisWindow.Length, 1 / Math.Sqrt(windowdataSum), _analysisWindow, 1);
            GetDualFunction(_analysisWindow, WindowLength, TimeStep, FrequencyBins, 0, _synthesisWindow);
        }

        /// <summary>
        /// 生成默认双窗函数
        /// </summary>
        /// <param name="signalLength">信号长度</param>
        /// <exception cref="System.Exception">当信号长度不足时不做Gabor变换</exception>
        private void GenerateDualWindowDefault(int signalLength)
        {
            if (signalLength< 512)
                throw new System.Exception("signal too short");
            WindowLength = 512;
            FrequencyBins = 512;
            TimeStep = 128;
            WindowType = WindowType.Hanning;
            GenerateDualWindow(WindowType.Hanning);
        }

        /// <summary>
        /// 复数Gabor变换
        /// </summary>
        /// <param name="waveform">输入波形</param>
        /// <param name="Coeff">输出时频域系数矩阵</param>
        public void GetGaborTransform(Complex[] waveform, Complex[][] Coeff)
        {
            #region ---- 判断 ----
            if (_analysisWindow == null || _synthesisWindow == null)
            {
                GenerateDualWindow(WindowType);
            }
            if (_padding == null)
            {
                GeneratePadding(waveform);
            }
            if (_unprocessedWaveformComplex == null)
            {
                _unprocessedWaveformComplex = new Complex[WindowLength];
            }
            #endregion
            Array.Copy(waveform, waveform.Length - WindowLength, _unprocessedWaveformComplex, 0, WindowLength);  //存储历史波形
            GetGaborTransform(waveform, _analysisWindow, TimeStep, FrequencyBins, _padding, ref Coeff);
        }

        /// <summary>
        /// 实数Gabor变换
        /// </summary>
        /// <param name="waveform">输入波形</param>
        /// <param name="Coeff">输出时频域系数矩阵</param>
        /// <param name="conjugateRemovedCoeff">全/半频谱选项</param>
        public void GetGaborTransform(double[] waveform, Complex[][] Coeff, bool conjugateRemovedCoeff = false)
        {
            #region ---- 判断 ----
            if (_analysisWindow == null || _synthesisWindow == null)
            {
                GenerateDualWindow(WindowType);
            }
            if (_padding == null)
            {
                GeneratePadding(waveform);
            }
            if (_unprocessedWaveformReal == null)
            {
                _unprocessedWaveformReal = new double[WindowLength];
            }
            #endregion
            Array.Copy(waveform, waveform.Length - WindowLength, _unprocessedWaveformReal, 0, WindowLength);  //存储历史波形
            GetGaborTransform(waveform, _analysisWindow, TimeStep, FrequencyBins, _padding, ref Coeff, conjugateRemovedCoeff); 
        }

        /// <summary>
        /// 实数Gabor展开
        /// </summary>
        /// <param name="Coeff">时频域系数矩阵</param>
        /// <param name="waveformOut">输出还原信号</param>
        /// <param name="conjugateRemovedCoeff"></param>
        public void GetGaborExpansion(Complex[][] Coeff, double[] waveformOut, bool conjugateRemovedCoeff = false)
        {
            GetGaborExpasion(Coeff, _synthesisWindow, TimeStep, FrequencyBins, waveformOut, conjugateRemovedCoeff);
        }

        /// <summary>
        /// 复数Gabor展开
        /// </summary>
        /// <param name="Coeff"></param>
        /// <param name="waveformOut"></param>
        public void GetGaborExpansion(Complex[][] Coeff, Complex[] waveformOut)
        {
            GetGaborExpasion(Coeff, _synthesisWindow, TimeStep, FrequencyBins, waveformOut);
            return; //Call Static
        }

        /// <summary>
        /// 输出功率谱
        /// </summary>
        /// <param name="Coeff">时频域系数矩阵</param>
        /// <returns>时频域功率谱</returns>
        public static double[,] ConvertToPowerSpectrum(Complex[][] Coeff)
        {
            double[,] powerSpectrum = new double[Coeff.Length, Coeff[0].Length];
            for (int i = 0; i < Coeff.Length; i++)
            {
                double[] rowPower = new double[Coeff[i].Length];
                Vector.GetComplexPower(Coeff[i], rowPower);
                for (int j = 0; j < Coeff[i].Length; j++)
                {
                    powerSpectrum[i, j] = rowPower[j];
                }
            }
            return powerSpectrum;
        }

        /// <summary>
        /// 输出幅度谱
        /// </summary>
        /// <param name="Coeff">时频域系数矩阵</param>
        /// <returns>时频域幅度谱</returns>
        public static double[,] ConvertToMagnitudeSpectrum(Complex[][] Coeff)
        {
            double[,] powerSpectrum = new double[Coeff.Length, Coeff[0].Length];
            for (int i = 0; i < Coeff.Length; i++)
            {
                double[] rowPower = new double[Coeff[i].Length];
                Vector.GetComplexPower(Coeff[i], rowPower);
                Vector.GetComplexMagnitude(Coeff[i], rowPower);
                //Vector.GetComplexMagnitude(Coeff[i], rowPower);
                for (int j = 0; j < Coeff[i].Length; j++)
                {
                    powerSpectrum[i, j] = rowPower[j];
                }
            }
            return powerSpectrum;
        }

        #region ----辅助函数----
        /// <summary>
        /// 根据填充类型生成实数数据填充
        /// </summary>
        /// <param name="waveform"></param>
        /// <exception cref="System.Exception"></exception>
        private void GeneratePadding(double[] waveform)
        {
            switch (Padding)
            {
                case PaddingType.noPadding:
                    {
                        throw new System.Exception("No Padding Type Selected");
                    }
                case PaddingType.zeros:
                    {
                        _padding = new double[2 * WindowLength];
                        for (int i = 0; i < 2 * WindowLength; i++)
                        {
                            _padding[i] = 0;
                        }
                        break;
                    }
                case PaddingType.wrap:
                    {
                        _padding = new double[2 * WindowLength];
                        for (int i = 0; i < WindowLength; i++)
                        {
                            _padding[i] = waveform[waveform.Length - WindowLength + i];
                            _padding[i + WindowLength] =  waveform[i];
                        }
                        break;
                    }
                case PaddingType.history:
                    {
                        _padding = new double[2 * WindowLength];
                        if (_unprocessedWaveformReal == null)
                        {
                            Padding = PaddingType.zeros;
                            GeneratePadding(waveform);
                            break;
                        }
                        for (int i = 0; i < WindowLength; i++)
                        {
                            _padding[i] = _unprocessedWaveformReal[i];
                            _padding[i + WindowLength] = 0;
                        }
                        break;
                    }
                default: throw new System.Exception("Padding Type Not Recognized");
            }
        }

        /// <summary>
        /// 根据填充类型生成复数数据填充
        /// </summary>
        /// <param name="waveform"></param>
        /// <exception cref="System.Exception"></exception>
        private void GeneratePadding(Complex[] waveform)
        {
            switch (Padding)
            {
                case PaddingType.noPadding:
                    {
                        throw new System.Exception("No Padding Type Selected");
                    }
                case PaddingType.zeros:
                    {
                        _paddingC = new Complex[2 * WindowLength];
                        for (int i = 0; i < 2 * WindowLength; i++)
                        {
                            _paddingC[i] = 0;
                        }
                        break;
                    }
                case PaddingType.wrap:
                    {
                        _paddingC = new Complex[WindowLength];
                        for (int i = 0; i < WindowLength; i++)
                        {
                            _paddingC[i] = waveform[waveform.Length - WindowLength + i];
                            _paddingC[i + WindowLength] = waveform[i];
                        }
                        break;
                    }
                case PaddingType.history:
                    {
                        _padding = new double[2 * WindowLength];
                        if (_unprocessedWaveformReal == null)
                        {
                            Padding = PaddingType.zeros;
                            GeneratePadding(waveform);
                            break;
                        }
                        for (int i = 0; i < WindowLength; i++)
                        {
                            _paddingC[i] = _unprocessedWaveformComplex[i];
                            _paddingC[i + WindowLength] = 0;
                        }
                        break;
                    }
                default: throw new System.Exception("Padding Type Not Recognized");
            }
        }

        /// <summary>
        /// 窗函数是否在信号之前
        /// </summary>
        /// <param name="windowPosition">窗函数位置</param>
        /// <param name="windowLength">窗函数长度</param>
        /// <returns>bool</returns>
        private static bool windowBeforeSignal(int windowPosition, int windowLength)
        {
            return windowPosition < windowLength;
        }

        /// <summary>
        /// 窗函数是否在信号之后
        /// </summary>
        /// <param name="windowPosition">窗函数位置</param>
        /// <param name="signalLength">信号长度</param>
        /// <returns>bool</returns>
        private static bool windowAfterSignal(int windowPosition, int signalLength)
        {
            return windowPosition >= signalLength;
        }

        /// <summary>
        /// 对时域信号截取加窗
        /// </summary>
        /// <param name="window">时域解析窗</param>
        /// <param name="dN">频域采样步进</param>
        /// <param name="windowLength">窗长度</param>
        /// <param name="windowPosition">窗位置</param>
        /// <param name="waveformIn">时域信号</param>
        /// <param name="padding">时域信号填充</param>
        /// <param name="waveformOut">输出：截取加窗后的时域信号</param>
        private static void applyAnalysisWindow(double[] window, int dN, int windowLength, int windowPosition, Complex[] waveformIn, double[] padding, ref Complex[] waveformOut)
        {
            Complex [] waveform = new Complex[windowLength];

            for (int i = 0; i < windowLength; i++)
            {
                if (windowBeforeSignal(windowPosition, windowLength))
                {
                    if (i >= windowLength - windowPosition)
                    {
                        waveform[i] = window[i] * waveformIn[i - (windowLength - windowPosition)];
                    }
                    else
                    {
                        waveform[i] = padding[windowPosition + i];
                    }

                }
                else if (windowAfterSignal(windowPosition, waveformOut.Length))
                {
                    if (i < waveformIn.Length + windowLength - windowPosition)
                    {
                        waveform[i] = window[i] * waveformIn[(windowPosition - windowLength) + i];
                    }
                    else
                    {
                        waveform[i] = padding[i - (waveformIn.Length + windowLength - windowPosition)];
                    }

                }
                else
                {
                    waveform[i] = window[i] * waveformIn[windowPosition - windowLength + i];
                }
            }

            int shift = (windowPosition) % windowLength;
            Complex[] _tmp = new Complex[windowLength];
            Array.Copy(waveform, _tmp, windowLength);
            Array.Copy(_tmp, 0, waveform, shift, windowLength - shift);
            Array.Copy(_tmp, windowLength - shift, waveform, 0, shift);
            if (dN != 1)
            {
                Complex[] _tmpFold = new Complex[windowLength / dN];
                for (int i = 0; i < windowLength / dN; i++)
                {
                    for (int j = 0; j < dN; j++)
                    {
                        _tmpFold[i] += waveform[j * (windowLength / dN) + i];
                    }
                }
                Array.Copy(_tmpFold, waveformOut,windowLength / dN);
            }
            else
            {
                Array.Copy(waveform, waveformOut, windowLength / dN);
            }
        }


        /// <summary>
        /// 对时域信号截取加窗
        /// </summary>
        /// <param name="window">时域解析窗</param>
        /// <param name="dN">频域采样步进</param>
        /// <param name="windowLength">窗长度</param>
        /// <param name="windowPosition">窗位置</param>
        /// <param name="waveformIn">时域信号</param>
        /// <param name="padding">时域信号填充</param>
        /// <param name="waveformOut">输出：截取加窗后的时域信号</param>
        private static void applyAnalysisWindow(double[] window, int dN, int windowLength, int windowPosition, double[] waveformIn, double[] padding, ref double[] waveformOut)
        {
            double[] waveform = new double[windowLength];

            for (int i = 0; i < windowLength; i++)
            {
                if (windowBeforeSignal(windowPosition, windowLength))
                {
                    if (i >= windowLength - windowPosition)
                    {
                        waveform[i] = window[i] * waveformIn[i - (windowLength - windowPosition)];
                    }
                    else
                    {
                        waveform[i] = padding[windowPosition + i];
                    }

                }
                else if (windowAfterSignal(windowPosition, waveformOut.Length))
                {
                    if (i < waveformIn.Length + windowLength - windowPosition)
                    {
                        waveform[i] = window[i] * waveformIn[(windowPosition - windowLength) + i];
                    }
                    else
                    {
                        waveform[i] = padding[windowLength - 1 + i - (waveformIn.Length + windowLength - windowPosition)];
                    }
                }
                else
                {
                    waveform[i] = window[i] * waveformIn[windowPosition - windowLength + i];
                }
            }

            int shift = (windowPosition) % windowLength;
            double[] _tmp = new double[windowLength];
            Array.Copy(waveform, _tmp, windowLength);
            Array.Copy(_tmp, 0, waveform, shift, windowLength - shift);
            Array.Copy(_tmp, windowLength - shift, waveform, 0, shift);
            if (dN != 1)
            {
                double[] _tmpFold = new double[windowLength / dN];
                for (int i = 0; i < windowLength / dN; i++)
                {
                    for (int j = 0; j < dN; j++)
                    {
                        _tmpFold[i] += waveform[j * (windowLength / dN) + i];
                    }
                }
                Array.Copy(_tmpFold, waveformOut, windowLength / dN);
            }
            else
            {
                Array.Copy(waveform, waveformOut, windowLength / dN);
            }
        }

        /// <summary>
        /// 将反FFT后的时域信号加窗并补齐
        /// </summary>
        /// <param name="window">时域还原窗</param>
        /// <param name="dN">频域采样步进</param>
        /// <param name="windowLength">窗长度</param>
        /// <param name="windowPosition">窗位置</param>
        /// <param name="waveformIn">反FFT后的时域信号</param>
        /// <param name="waveformOut">输出:加窗补齐后的时域信号</param>
        private static void ApplySynthesisWindow(double[] window, int dN, int windowLength, int windowPosition, Complex[] waveformIn, ref Complex[] waveformOut)
        {
            Complex[] _tmpExpand = new Complex[windowLength];
            if (dN != 1)
            {
                for (int i = 0; i < windowLength / dN; i++)
                {
                    for (int j = 0; j < dN; j++)
                    {
                        _tmpExpand[j * (windowLength / dN) + i] = waveformIn[i];
                    }
                }
            }
            else
            {
                _tmpExpand = waveformIn;
            }
            int shiftBack = windowLength - (windowPosition % windowLength);
            Complex[] _tmp = new Complex[windowLength];
            Array.Copy(_tmpExpand, _tmp, windowLength);
            Array.Copy(_tmp, 0, _tmpExpand, shiftBack, windowLength - shiftBack);
            Array.Copy(_tmp, windowLength - shiftBack, _tmpExpand, 0, shiftBack);

            for (int i = 0; i < windowLength; i++)
            {
                if (windowBeforeSignal(windowPosition, windowLength))
                {
                    if (i >= windowLength - windowPosition)
                    {
                        waveformOut[i + windowPosition - windowLength] = window[i] * _tmpExpand[i];
                    }
                }
                else if (windowAfterSignal(windowPosition, waveformOut.Length))
                {
                    if (i < waveformOut.Length + windowLength - windowPosition)
                    {
                        waveformOut[i + windowPosition - windowLength] = window[i] * _tmpExpand[i];
                    }
                }
                else
                {
                    waveformOut[i + windowPosition - windowLength] = window[i] * _tmpExpand[i];
                }

            }
        }


        /// <summary>
        /// 将反FFT后的时域信号加窗并补齐
        /// </summary>
        /// <param name="window">时域还原窗</param>
        /// <param name="dN">频域采样步进</param>
        /// <param name="windowLength">窗长度</param>
        /// <param name="windowPosition">窗位置</param>
        /// <param name="waveformIn">反FFT后的时域信号</param>
        /// <param name="waveformOut">输出:加窗补齐后的时域信号</param>
        private static void ApplySynthesisWindow(double[] window, int dN, int windowLength, int windowPosition, double[] waveformIn, ref double[] waveformOut)
        {
            double[] _tmpExpand = new double[windowLength];
            if (dN != 1)
            {
                for (int i = 0; i < windowLength / dN; i++)
                {
                    for (int j = 0; j < dN; j++)
                    {
                        _tmpExpand[j * (windowLength / dN) + i] = waveformIn[i];
                    }
                }
            }
            else
            {
                _tmpExpand = waveformIn;
            }
            int shiftBack = windowLength - (windowPosition % windowLength);
            double[] _tmp = new double[windowLength];
            Array.Copy(_tmpExpand, _tmp, windowLength);
            Array.Copy(_tmp, 0, _tmpExpand, shiftBack, windowLength - shiftBack);
            Array.Copy(_tmp, windowLength - shiftBack, _tmpExpand, 0, shiftBack);

            for (int i = 0; i < windowLength; i++)
            {
                if (windowBeforeSignal(windowPosition, windowLength))
                {
                    if (i >= windowLength - windowPosition)
                    {
                        waveformOut[i + windowPosition - windowLength] = window[i] * _tmpExpand[i];
                    }
                }
                else if (windowAfterSignal(windowPosition, waveformOut.Length))
                {
                    if (i < waveformOut.Length + windowLength - windowPosition)
                    {
                        waveformOut[i + windowPosition - windowLength] = window[i] * _tmpExpand[i];
                    }
                }
                else
                {
                    waveformOut[i + windowPosition - windowLength] = window[i] * _tmpExpand[i];
                }

            }
        }

        /// <summary>
        /// 与JXI.Mathematics中PseudoInverse对接
        /// </summary>
        /// <param name="H">输入矩阵(一维数组)</param>
        /// <param name="row">矩阵行数</param>
        /// <param name="col">矩阵列数</param>
        /// <param name="HT">输出：矩阵伪逆(一维数组)</param>
        private static void PseudoInverse(double[] H, int row, int col, double[] HT)
        {
            #region ---- 辅助函数 ----
            double[,] convertTo2D(double[] arr)
            {
                double[,] _array2D = new double[row, col];
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        _array2D[i, j] = H[i * col + j];
                    }
                }
                return _array2D;
            }

            void flattenFrom2D(double[,] array2D, double[] res)
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        res[j * row + i] = array2D[j, i];
                    }
                }
            }
            #endregion

            Matrix<double> _matrixH = new Matrix<double>(convertTo2D(H));
            Matrix<double> _matrixHT = Matrix<double>.PseudoInverse(_matrixH);
            flattenFrom2D(_matrixHT.MatrixArray, HT);
        }
        #endregion

        #region ----Static----
        /// <summary>
        /// 基于一个窗函数，生成对应的Gabor Dual Window Function
        /// </summary>
        /// <param name="window">输入窗函数</param>
        /// <param name="L">窗口宽度</param>
        /// <param name="dM">Gabor时间采样步长</param>
        /// <param name="N">频域采样点数</param>
        /// <param name="periodic">窗口周期性</param>
        /// <param name="gamma">输出：Gabor Dual Window函数</param>
        public static void GetDualFunction(double[] window, int L, int dM, int N, int periodic, double[] gamma)
        {
            #region ---- 判断 ----
            if (dM <= 0)
                throw (new System.Exception("dual function setting error: dM must be > 0")); // dual function setting error: dM must be > 0
            if (N <= 0 || ((N & (N - 1)) != 0))
                throw (new System.Exception("dual function setting error: N must be > 1 and a power of two")); // dual function setting error: N must be > 1 and a power of two
            if (L <= 0 || L % dM != 0 || L % N != 0)
                throw (new System.Exception("dual function setting error: L must be > 0 and divisible by both dM and N")); // dual function setting error: L must be > 0 and divisible by both dM and N
            if (dM > N)
                throw (new System.Exception("dual function setting error: N must be >= dM")); // dual function setting error: N must be >= dM
            #endregion

            #region ---- 准备 ----
            double[] a, H, HT;
            int i, k, M, dN, p, q, q0;
            int len_w, step_freq;

            dN = L / N;
            M = L / dM; // Number of time sampling points

            // Constructing auxiliary window a[]
            switch (periodic)
            {
                case 0: // Periodic window
                    len_w = L;
                    step_freq = dN;
                    break;
                case 1: // Non-periodic window
                    len_w = 2 * L - N;
                    step_freq = 2 * dN - 1;
                    break;
                default: // Window selection error
                    throw (new System.Exception("Window selection error"));
            }

            // Allocate memory
            a = new double[len_w];
            Array.Clear(a, 0, len_w);
            Array.Copy(window, a, L);

            H = new double[M * step_freq];
            HT = new double[M * step_freq];
            #endregion

            #region ---- 计算窗函数 ----
            // Computing dual function
            for (k = 0; k < dM; k++)
            {
                for (q = 0; q < step_freq; q++)
                {
                    q0 = q * N;
                    for (p = 0; p < M; p++)
                    {
                        i = k + q0 + p * dM;
                        while (i >= len_w)
                            i -= len_w;

                        if (step_freq > M)
                            H[q * M + p] = a[i];
                        else
                            H[p * step_freq + q] = a[i];
                    }
                }

                // Resolving H*x = u, where u = (1/dM, 0, 0, ..., 0)T
                if (step_freq > M)
                {
                    PseudoInverse(H, step_freq, M, HT);
                    for (p = 0; p < M; p++)
                        gamma[k + p * dM] = HT[p * step_freq] / dM;
                }
                else
                {
                    PseudoInverse(H, M, step_freq, HT);
                    for (p = 0; p < M; p++)
                        gamma[k + p * dM] = HT[p] / dM;
                }
            }
            #endregion
        }

        /// <summary>
        /// 复数Gabor Transform静态实现，将一个时域信号通过解析窗和时域采样后映射到时频域里
        /// </summary>
        /// <param name="waveform">输入波形</param>
        /// <param name="analysisWindow">时域解析窗</param>
        /// <param name="dM">时域采样步进</param>
        /// <param name="N">频域采样点数</param>
        /// <param name="padding">时域信号填充</param>
        /// <param name="Coeff">输出: 时频域谱</param>
        public static void GetGaborTransform(Complex[] waveform, double[] analysisWindow, int dM, int N, double[] padding, ref Complex[][] Coeff)
        {
            #region ---- 判断 ----
            if (analysisWindow.Length != padding.Length)
                throw (new System.Exception());
            #endregion
            #region ---- 准备 ----
            int signalLength = waveform.GetLength(0);
            int windowLength = analysisWindow.GetLength(0);
            int M = signalLength / dM;
            int dN = windowLength / N;
            #endregion
            for (int i = 0; i < M + windowLength / dM + 1; i++)
            {
                Complex[] rowC = new Complex[N];
                applyAnalysisWindow(analysisWindow, dN, windowLength, i * dM, waveform, padding, ref rowC);
                DFT.ComputeForward(rowC);
                Coeff[i] = rowC;
            }
        }

        /// <summary>
        /// 实数Gabor Transform静态实现，将一个时域信号通过解析窗和时域采样后映射到时频域里
        /// </summary>
        /// <param name="waveform">输入波形</param>
        /// <param name="analysisWindow">时域解析窗</param>
        /// <param name="dM">时域采样步进</param>
        /// <param name="N">频域采样点数</param>
        /// <param name="padding">时域信号填充</param>
        /// <param name="spectrum">输出: 时频域谱</param>
        public static void GetGaborTransform(double[] waveform, double[] analysisWindow, int dM, int N, double[] padding, ref Complex[][] spectrum, bool conjugateRemovedCoeff = false)
        {
            #region ---- 判断 ----
            if (2 * analysisWindow.Length != padding.Length)
                throw (new System.Exception());
            #endregion
            #region ---- 准备 ----
            int signalLength = waveform.GetLength(0);
            int windowLength = analysisWindow.GetLength(0);
            int M = signalLength / dM;
            int dN = windowLength / N;
            #endregion
            for (int i = 0; i < M + windowLength / dM + 1; i++)
            {
                double[] rowR = new double[N];
                Complex[] spectrumRow = new Complex[N / 2 + 1];
                
                applyAnalysisWindow(analysisWindow, dN, windowLength, i * dM, waveform, padding, ref rowR);
                Complex[] rowC = Vector.ConvertToComplex(rowR);
                DFT.ComputeForward(rowC);
                if (conjugateRemovedCoeff)
                {
                    Array.Copy(rowC, spectrumRow, N / 2 + 1);
                    spectrum[i] = spectrumRow;
                } 
                else
                {
                    spectrum[i] = rowC;
                }
            }
        }

        /// <summary>
        /// 将一个通过Gabor Transform生成的时频域谱通过对应的还原窗还原成时域信号
        /// </summary>
        /// <param name="spectrum">输入时频域谱</param>
        /// <param name="synthesisWindow">时域还原窗</param>
        /// <param name="dM">时域采样步进</param>
        /// <param name="N">频域采样点数</param>
        /// <param name="recoverdSignal">输出：还原时域信号</param>
        public static void GetGaborExpasion(Complex[][] spectrum, double[] synthesisWindow, int dM, int N, Complex[] recoverdSignal)
        {
            int row = spectrum.GetLength(0);
            int signalLength = recoverdSignal.GetLength(0);
            int windowLength = synthesisWindow.GetLength(0);
            int dN = windowLength / N;
            Complex[] recoverdSignalC = new Complex[signalLength];
            
            for (int i = 0; i < row; i++)
            {
                Complex[] _tmpWave = new Complex[signalLength];
                DFT.ComputeBackward(spectrum[i]);
                ApplySynthesisWindow(synthesisWindow, dN, windowLength, i * dM, spectrum[i], ref _tmpWave);
                for (int j = 0; j < signalLength; j++)
                {
                    recoverdSignalC[j] += dM * _tmpWave[j];
                }
            }

            Array.Copy(recoverdSignalC, recoverdSignal, signalLength);
        }


        /// <summary>
        /// 将一个通过Gabor Transform生成的时频域谱通过对应的还原窗还原成时域信号
        /// </summary>
        /// <param name="spectrum">输入时频域谱</param>
        /// <param name="synthesisWindow">时域还原窗</param>
        /// <param name="dM">时域采样步进</param>
        /// <param name="N">频域采样点数</param>
        /// <param name="recoverdSignal">输出：还原时域信号</param>
        public static void GetGaborExpasion(Complex[][] spectrum, double[] synthesisWindow, int dM, int N, double[] recoverdSignal, bool conjugateRemovedCoeff = false)
        {
            int row = spectrum.Length;
            int signalLength = recoverdSignal.Length;
            int windowLength = synthesisWindow.Length;

            int M = signalLength / dM;
            int dN = windowLength / N;
            double[] recoverdSignalR = new double[signalLength];
            Complex[] _tmpIFFTInC;
            for (int i = 0; i < row; i++)
            {
                if (conjugateRemovedCoeff)
                {
                    Complex[] recoveredSpectrumRow = new Complex[synthesisWindow.Length];
                    for ( int k = 0; k < spectrum[i].Length; k++)
                    {
                        recoveredSpectrumRow[k] = spectrum[i][k];
                        if (k > 0)
                        {
                            recoveredSpectrumRow[recoveredSpectrumRow.Length - k] = Complex.Conjugate(spectrum[i][k]);
                        }
                        
                    }
                    _tmpIFFTInC = recoveredSpectrumRow;
                }
                else
                {
                    _tmpIFFTInC = spectrum[i];
                }

                double[] _tmpWave = new double[signalLength];
                double[] _tmpIFFTOut = new double[windowLength];
                Complex[] _tmpIFFTOutC = new Complex[windowLength];
                DFT.ComputeBackward(_tmpIFFTInC, _tmpIFFTOutC);
                Vector.GetComplexReal(_tmpIFFTOutC, _tmpIFFTOut);
                ApplySynthesisWindow(synthesisWindow, dN, windowLength, i * dM, _tmpIFFTOut, ref _tmpWave);
                for (int j = 0; j < signalLength; j++)
                {
                    recoverdSignalR[j] += dM * _tmpWave[j];
                }
            }

            Array.Copy(recoverdSignalR, recoverdSignal, signalLength);
        }
        #endregion
    }
}
