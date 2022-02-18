using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using SeeSharpTools.JXI.Numerics;


namespace SeeSharpTools.JXI.SignalProcessing.Conditioning.AdvancedFilters
{
    /// <summary>
    /// 滤波器计算静态类。
    /// </summary>
    public class EasyFIR<T>
    {
        /// <summary>
        /// 有限冲击响应滤波器
        /// </summary>
        public static void FIR(T[] X, double[] coefficients, out T[] output)
        {
            FIR<T> staticFIR = new FIR<T>(coefficients);
            output = new T[X.Length];
            staticFIR.Process(X, output);
        }

        /// <summary>
        /// 降采样滤波器
        /// </summary>
        public static void DownSampleFIR(T[] X, double[] coefficients, int decimateRate, out T[] output)
        {
            MultiRateFIR<T> staticFIR = new MultiRateFIR<T>(coefficients, 1, decimateRate);
            int length = X.Length / decimateRate;
            output = new T[length];
            staticFIR.Process(X, output, out length);
        }

        /// <summary>
        /// 升采样滤波器
        /// </summary>
        public static void UpSampleFIR(T[] X, double[] coefficients, int interpolationRate, out T[] output)
        {
            MultiRateFIR<T> staticFIR = new MultiRateFIR<T>(coefficients, interpolationRate, 1);
            int length = X.Length * interpolationRate;
            output = new T[length];
            staticFIR.Process(X, output, out length);
        }

        /// <summary>
        /// 任意变比滤波器
        /// </summary>
        public static void ArbitraryFIR(T[] X, double[] coefficients, int interpolationRate, int decimateRate, out T[] output)
        {
            MultiRateFIR<T> staticFIR = new MultiRateFIR<T>(coefficients, interpolationRate, decimateRate);
            int length = X.Length / decimateRate * interpolationRate;
            output = new T[length];
            staticFIR.Process(X, output, out length);
        }
    }

    /// <summary>
    /// FIR 滤波器
    /// </summary>
    public class FIR<T>
    {
        /// <summary>
        /// FIR 滤波器内部状态
        /// </summary>
        private T[] _status;

        /// <summary>
        /// FIR 滤波器执行参数
        /// </summary>
        private FIRCore.FilterHandle _handle;

        /// <summary>
        /// 滤波器执行精度
        /// </summary>
        private IPPCaller.IppDataType _precision;

        /// <summary>
        /// 滤波器作用域
        /// </summary>
        private IPPCaller.IppFilterType _domain;

        /// <summary>
        /// 初始化实数域滤波器
        /// </summary>
        public FIR(double[] coefficients)
        {
            _status = new T[coefficients.Length - 1];

            if (_status is float[]) { _precision = IPPCaller.IppDataType.ipp32f; }
            else if (_status is double[]) { _precision = IPPCaller.IppDataType.ipp64f; }
            else if (_status is Complex32[]) { _precision = IPPCaller.IppDataType.ipp32fc; }
            else if (_status is Complex[]) { _precision = IPPCaller.IppDataType.ipp64fc; }
            else { throw new ArgumentException("Filter type not supported"); }

            _domain = IPPCaller.IppFilterType.REAL;

            // 初始化 Filter Core。
            FIRCore.GetInstance().GetFIRDescriptor(coefficients, _precision, out _handle);
        }

        /// <summary>
        /// 初始化复数域滤波器
        /// </summary>
        public FIR(Complex[] coefficients)
        {
            _status = new T[coefficients.Length - 1];

            if (_status is float[]) { _precision = IPPCaller.IppDataType.ipp32f; }
            else if (_status is double[]) { _precision = IPPCaller.IppDataType.ipp64f; }
            else if (_status is Complex32[]) { _precision = IPPCaller.IppDataType.ipp32fc; }
            else if (_status is Complex[]) { _precision = IPPCaller.IppDataType.ipp64fc; }
            else { throw new ArgumentException("Filter type not supported"); }

            _domain = IPPCaller.IppFilterType.COMPLEX;

            // 初始化 Filter Core。
            FIRCore.GetInstance().GetFIRDescriptor(coefficients, _precision, out _handle);
        }

        /// <summary>
        /// 重置滤波器
        /// </summary>
        public void Reset()
        {
            _status = new T[_status.Length];
        }

        /// <summary>
        /// 执行滤波器
        /// </summary>
        public void Process(T[] input, T[] output)
        {
            int length = input.Length;
            if (input is short[])
            {
                // 输入数据类型转换
                short[] source_s16 = input as short[];
                short[] initState = _status as short[];
                short[] desination = output as short[];
                // 执行对应数据类型的滤波器，float
                FIRCore.ProcessFIR(source_s16, desination, length, initState, _handle);
            }
            else if (input is float[])
            {
                // 输入数据类型转换
                float[] source_f32 = input as float[];
                float[] initState = _status as float[];
                float[] desination = output as float[];
                // 执行对应数据类型的滤波器，float
                FIRCore.ProcessFIR(source_f32, desination, length, initState, _handle);
            }
            else if (input is double[])
            {
                // 输入数据类型转换
                double[] source_f64 = input as double[];
                double[] initState = _status as double[];
                double[] desination = output as double[];
                // 执行对应数据类型的滤波器，double
                FIRCore.ProcessFIR(source_f64, desination, length, initState, _handle);
            }
            else if (input is Complex32[])
            {
                // 输入数据类型转换
                Complex32[] source_fc32 = input as Complex32[];
                Complex32[] initState = _status as Complex32[];
                Complex32[] desination = output as Complex32[];
                // 执行对应数据类型的滤波器，Complex
                if (_domain == IPPCaller.IppFilterType.REAL)
                {
                    FIRCore.ProcessFIR_Real(source_fc32, desination, length, initState, _handle);
                }
                else if (_domain == IPPCaller.IppFilterType.COMPLEX)
                {
                    FIRCore.ProcessFIR(source_fc32, desination, length, initState, _handle);
                }
            }
            else if (input is Complex[])
            {
                // 输入数据类型转换
                Complex[] source_fc64 = input as Complex[];
                Complex[] initState = _status as Complex[];
                Complex[] desination = output as Complex[];
                // 执行对应数据类型的滤波器，Complex
                FIRCore.ProcessFIR(source_fc64, desination, length, initState, _handle);
            }

            // 保留滤波器历史状态
            if (input.Length >= _status.Length)
            {
                // 输入数据长度大于滤波器长度
                Array.Copy(input, input.Length - _status.Length, _status, 0, _status.Length);
            }
            else
            {
                // 输入数据长度小于滤波器长度
                Array.Copy(_status, input.Length, _status, 0, _status.Length - input.Length);
                Array.Copy(input, 0, _status, _status.Length - input.Length, input.Length);
            }
        }
    }

    /// <summary>
    /// MultiRateFIR
    /// </summary>
    public class MultiRateFIR<T>
    {
        /// <summary>
        /// MultiRateFIR 滤波器内部状态
        /// </summary>
        private T[] _status;

        /// <summary>
        /// FIR 滤波器执行参数
        /// </summary>
        private FIRCore.FilterHandle _handle;

        /// <summary>
        /// 滤波器执行精度
        /// </summary>
        private IPPCaller.IppDataType _precision;

        /// <summary>
        /// 滤波器作用域
        /// </summary>
        private IPPCaller.IppFilterType _domain;

        /// <summary>
        /// 升采样倍数
        /// </summary>
        private int _upSampleFactor;

        /// <summary>
        /// 降采样倍数
        /// </summary>
        private int _downSampleFactor;

        /// <summary>
        /// 降采样保留历史数组
        /// </summary>
        private T[] _history;

        /// <summary>
        /// 降采样保留历史数组实际长度
        /// </summary>
        private int _historyLength;

        /// <summary>
        /// 初始化实数域滤波器
        /// </summary>
        public MultiRateFIR(double[] coefficients, int upsampleFactor, int downsampleFactor)
        {
            _status = new T[(coefficients.Length + upsampleFactor - 1) / upsampleFactor];

            if (_status is float[]) { _precision = IPPCaller.IppDataType.ipp32f; }
            else if (_status is double[]) { _precision = IPPCaller.IppDataType.ipp64f; }
            else if (_status is Complex32[]) { _precision = IPPCaller.IppDataType.ipp32fc; }
            else if (_status is Complex[]) { _precision = IPPCaller.IppDataType.ipp64fc; }
            else { throw new ArgumentException("Filter type not supported"); }

            _domain = IPPCaller.IppFilterType.REAL;
            _upSampleFactor = upsampleFactor;
            _downSampleFactor = downsampleFactor;
            _history = new T[downsampleFactor];
            _historyLength = 0;

            // 初始化 Filter Core。
            FIRCore.GetInstance().GetFIRDescriptor(coefficients, _precision, _upSampleFactor, _downSampleFactor, out _handle);
        }

        /// <summary>
        /// 初始化复数域滤波器
        /// </summary>
        public MultiRateFIR(Complex[] coefficients, int upsampleFactor, int downsampleFactor)
        {
            _status = new T[(coefficients.Length + upsampleFactor - 1) / upsampleFactor];

            if (_status is float[]) { _precision = IPPCaller.IppDataType.ipp32f; }
            else if (_status is double[]) { _precision = IPPCaller.IppDataType.ipp64f; }
            else if (_status is Complex32[]) { _precision = IPPCaller.IppDataType.ipp32fc; }
            else if (_status is Complex[]) { _precision = IPPCaller.IppDataType.ipp64fc; }
            else { throw new ArgumentException("Filter type not supported"); }

            _domain = IPPCaller.IppFilterType.COMPLEX;
            _upSampleFactor = upsampleFactor;
            _downSampleFactor = downsampleFactor;
            _history = new T[downsampleFactor];
            _historyLength = 0;

            // 初始化 Filter Core。
            FIRCore.GetInstance().GetFIRDescriptor(coefficients, _precision, _upSampleFactor, _downSampleFactor, out _handle);
        }

        /// <summary>
        /// 重置滤波器
        /// </summary>
        public void Reset()
        {
            _status = new T[_status.Length];
            _history = new T[_downSampleFactor];
            _historyLength = 0;
        }

        /// <summary>
        /// 执行滤波器
        /// </summary>
        public void Process(T[] input, T[] output, out int outputLength)
        {
            int totalLength = input.Length + _historyLength;
            int newHistoryLength = totalLength % _downSampleFactor;

            // 需要满足滤波数据长度是降采样率的整数倍，多余的数据保留到history和下一次进行拼接。
            T[] source = new T[totalLength - newHistoryLength];
            Array.Copy(_history, 0, source, 0, _historyLength);
            Array.Copy(input, 0, source, _historyLength, source.Length - _historyLength);
            Array.Copy(input, input.Length - newHistoryLength, _history, 0, newHistoryLength);
            _historyLength = newHistoryLength;

            ProcessInner(source, output, out outputLength);
        }

        /// <summary>
        /// 快速降采样滤波，当输入数据长度始终为降采样率的整备数，无需复制history数组。
        /// </summary>
        public void Process_Fast(T[] input, T[] output, out int outputLength)
        {
            if (_historyLength != 0) { throw new InvalidOperationException("History data length is not empty."); }
            ProcessInner(input, output, out outputLength);
        }

        /// <summary>
        /// 执行滤波器。
        /// </summary>
        private void ProcessInner(T[] input, T[] output, out int outputLength)
        {
            if (input.Length % _downSampleFactor != 0) { throw new ArgumentException("Iutput array size is invalid."); }
            int iterations = input.Length / _downSampleFactor;
            outputLength = iterations * _upSampleFactor;

            if (output.Length < outputLength) { throw new ArgumentException("Output array size is less than requrired"); }

            if (input is short[])
            {
                // 输入数据类型转换
                short[] source_s16 = input as short[];
                short[] initState = _status as short[];
                short[] desination = output as short[];
                // 执行对应数据类型的滤波器，float
                FIRCore.ProcessMultiRateFIR(source_s16, desination, iterations, initState, _handle);
            }
            else if (input is float[])
            {
                // 输入数据类型转换
                float[] source_f32 = input as float[];
                float[] initState = _status as float[];
                float[] desination = output as float[];
                // 执行对应数据类型的滤波器，float
                FIRCore.ProcessMultiRateFIR(source_f32, desination, iterations, initState, _handle);
            }
            else if (input is double[])
            {
                // 输入数据类型转换
                double[] source_f64 = input as double[];
                double[] initState = _status as double[];
                double[] desination = output as double[];
                // 执行对应数据类型的滤波器，double
                FIRCore.ProcessMultiRateFIR(source_f64, desination, iterations, initState, _handle);
            }
            else if (input is Complex32[])
            {
                // 输入数据类型转换
                Complex32[] source_fc32 = input as Complex32[];
                Complex32[] initState = _status as Complex32[];
                Complex32[] desination = output as Complex32[];
                // 执行对应数据类型的滤波器，Complex
                if (_domain == IPPCaller.IppFilterType.REAL)
                {
                    FIRCore.ProcessMultiRateFIR_Real(source_fc32, desination, iterations, initState, _handle);
                }
                else if (_domain == IPPCaller.IppFilterType.COMPLEX)
                {
                    FIRCore.ProcessMultiRateFIR(source_fc32, desination, iterations, initState, _handle);
                }
            }
            else if (input is Complex[])
            {
                // 输入数据类型转换
                Complex[] source_fc64 = input as Complex[];
                Complex[] initState = _status as Complex[];
                Complex[] desination = output as Complex[];
                // 执行对应数据类型的滤波器，Complex
                FIRCore.ProcessMultiRateFIR(source_fc64, desination, iterations, initState, _handle);
            }

            // 保留滤波器历史状态
            if (input.Length >= _status.Length)
            {
                // 输入数据长度大于滤波器长度
                Array.Copy(input, input.Length - _status.Length, _status, 0, _status.Length);
            }
            else
            {
                // 输入数据长度小于滤波器长度
                Array.Copy(_status, input.Length, _status, 0, _status.Length - input.Length);
                Array.Copy(input, 0, _status, _status.Length - input.Length, input.Length);
            }

        }
    }

    /// <summary>
    /// Fitler Core
    /// </summary>
    internal class FIRCore : IDisposable
    {
        #region ---- 内部数据结构 ----

        /// <summary>
        /// Filter Worker 定义
        /// </summary>
        private struct FilterWorker
        {
            public IntPtr BufferHandle;     // 运行缓存
            public int WorkBufSize;         // 运行缓存大小  
        }

        /// <summary>
        /// Descriptor Config Settings, Descriptor Key
        /// </summary>
        private struct FIRConfig
        {
            public IPPCaller.IppDataType Precision;           // 数据类型 
            public IPPCaller.IppFilterType Domain;            // 滤波器类型 
            public int UpSampleFactor;              // 升采样倍数
            public int DownSampleFactor;            // 降采样倍数
            public double[] RealCoefficients;       // Filter 系数
            public Complex[] ComplexCoefficients;   // Filter 系数
            public FilterHandle Handle;             // 执行句柄
        }

        /// <summary>
        /// FIR Working Parameters
        /// </summary>
        internal struct FilterHandle
        {
            public IntPtr FilterSpec;               // IPP Filter 设置
            public int WorkBufSize;                 // 运行缓存大小
        }


        #endregion

        #region ---- 构造和引用管理 ----

        /// <summary>
        /// FIR Filter 唯一实例，用非null表示已经初始化过了。
        /// </summary>
        private static FIRCore _onlyInstance = null;

        /// <summary>
        /// Filter Thread 个数，最多同时工作的线程数。
        /// </summary>
        private static int _numberOfFilterWorkers = 8;

        /// <summary>
        /// 最多访问线程锁，保证最多N个 worker 
        /// </summary>
        private static SemaphoreSlim _filterWorkerLock;

        /// <summary>
        /// 所有可用的 Worker 参数
        /// </summary>
        private static ConcurrentQueue<FilterWorker> _filterWorkers;

        /// <summary>
        /// All initialzied DFTI Descriptor
        /// </summary>
        private readonly List<FIRConfig> _filterDescriptors;

        /// <summary>
        /// Construct, Creat the descriptor table
        /// </summary>
        private FIRCore()
        {
            // 建立滤波器描述列表，存储所有滤波器信息。
            _filterDescriptors = new List<FIRConfig>();

            // 建立滤波器 worker 队列，控制滤波器执行线程。
            _filterWorkerLock = _filterWorkerLock ?? new SemaphoreSlim(_numberOfFilterWorkers);
            _filterWorkers = _filterWorkers ?? new ConcurrentQueue<FilterWorker>(new FilterWorker[_numberOfFilterWorkers]); // 新建 worker 塞满队列。
        }

        /// <summary>
        /// Release all the descriptors
        /// </summary>
        public void Dispose()
        {
            IPPCaller.IppStatus error;

            foreach (FIRConfig descriptor in _filterDescriptors)
            {
                IPPCaller.ippsFree(descriptor.Handle.FilterSpec);
            }
            _filterDescriptors.Clear();

            FilterWorker worker;
            while (_filterWorkers.TryDequeue(out worker))
            {
                IPPCaller.ippsFree(worker.BufferHandle);
            }
        }

        /// <summary>
        /// 析构
        /// </summary>
        ~FIRCore()
        {
            Dispose();
        }

        /// <summary>
        /// Get the only DFTI Instance。
        /// </summary>
        internal static FIRCore GetInstance()
        {
            return _onlyInstance ?? (_onlyInstance = new FIRCore());
        }

        /// <summary>
        /// Get Filter Descriptor
        /// </summary>
        internal int GetFIRDescriptor(double[] filterCoefficients, IPPCaller.IppDataType dataType, out FilterHandle handle)
        {
            IPPCaller.IppStatus error = IPPCaller.IppStatus.ippStsNoErr;

            lock (_filterDescriptors)
            {
                //存在该Key则直接返回
                foreach (FIRConfig config in _filterDescriptors)
                {
                    if (FilterExist(config, filterCoefficients, dataType, 1, 1))
                    {
                        handle = config.Handle;
                        return (int)error;
                    }
                }

                //不存在，则创建并配置，保存后返回
                FIRConfig descriptor = new FIRConfig();
                descriptor.Precision = dataType;
                descriptor.Domain = IPPCaller.IppFilterType.REAL;
                descriptor.UpSampleFactor = 1;
                descriptor.DownSampleFactor = 1;
                descriptor.ComplexCoefficients = null;

                // 复制Filter系数。
                int filterLength = filterCoefficients.Length;
                double[] filterCoef = new double[filterLength];
                Vector.ArrayCopy(filterCoefficients, filterCoef);
                descriptor.RealCoefficients = filterCoef;

                // 计算 Buffer Size。 对于complex float 有特殊算法                
                int sizeFIRSpec = 0, sizeFIRWorkBuf = 0;
                switch (descriptor.Precision)
                {
                    case IPPCaller.IppDataType.ipp32f:
                    case IPPCaller.IppDataType.ipp64f:
                    case IPPCaller.IppDataType.ipp64fc:
                        error = IPPCaller.ippsFIRSRGetSize(filterLength, dataType, ref sizeFIRSpec, ref sizeFIRWorkBuf);
                        break;
                    case IPPCaller.IppDataType.ipp32fc:
                        error = IPPCaller.ippsFIRSRGetSize32f_32fc(filterLength, ref sizeFIRSpec, ref sizeFIRWorkBuf);
                        break;
                    case IPPCaller.IppDataType.ipp16s:
                        error = IPPCaller.ippsFIRSRGetSize(filterLength, IPPCaller.IppDataType.ipp32f, ref sizeFIRSpec, ref sizeFIRWorkBuf);
                        break;
                    case IPPCaller.IppDataType.ipp16sc:
                        error = IPPCaller.ippsFIRSRGetSize(filterLength, IPPCaller.IppDataType.ipp32fc, ref sizeFIRSpec, ref sizeFIRWorkBuf);
                        break;
                    default:
                        break;
                }

                // 创建 Filter Spec 缓存.
                descriptor.Handle.FilterSpec = IPPCaller.ippsMalloc_8u(sizeFIRSpec);
                descriptor.Handle.WorkBufSize = sizeFIRWorkBuf;
                IntPtr pFIRSpec = descriptor.Handle.FilterSpec;

                // 初始化 Filter
                switch (dataType)
                {
                    case IPPCaller.IppDataType.ipp32f:
                        {
                            float[] floatCoefficients = Vector.ConvertToFloat(filterCoef);
                            error = IPPCaller.ippsFIRSRInit_32f(floatCoefficients, filterLength, IPPCaller.IppAlgType.ippAlgAuto, pFIRSpec);
                            break;
                        }
                    case IPPCaller.IppDataType.ipp64f:
                        {
                            error = IPPCaller.ippsFIRSRInit_64f(filterCoef, filterLength, IPPCaller.IppAlgType.ippAlgAuto, pFIRSpec);
                            break;
                        }
                    case IPPCaller.IppDataType.ipp32fc:
                        {
                            float[] floatCoefficients = Vector.ConvertToFloat(filterCoef);
                            error = IPPCaller.ippsFIRSRInit32f_32fc(floatCoefficients, filterLength, IPPCaller.IppAlgType.ippAlgAuto, pFIRSpec);
                            break;
                        }
                    case IPPCaller.IppDataType.ipp64fc:
                        {
                            double[] image = new double[filterLength];
                            Complex[] complexCoefficients = Vector.RealImageToComplex(filterCoef, image);
                            GCHandle complex_GC = GCHandle.Alloc(complexCoefficients, GCHandleType.Pinned);
                            IntPtr complex_Address = complex_GC.AddrOfPinnedObject();
                            error = IPPCaller.ippsFIRSRInit_64fc(complex_Address, filterLength, IPPCaller.IppAlgType.ippAlgAuto, pFIRSpec);
                            complex_GC.Free();
                            break;
                        }
                    case IPPCaller.IppDataType.ipp16s:
                        {
                            float[] floatCoefficients = Vector.ConvertToFloat(filterCoef);
                            error = IPPCaller.ippsFIRSRInit_32f(floatCoefficients, filterLength, IPPCaller.IppAlgType.ippAlgAuto, pFIRSpec);
                            break;
                        }
                    case IPPCaller.IppDataType.ipp16sc:
                        {
                            float[] real = Vector.ConvertToFloat(filterCoef);
                            float[] image = new float[filterLength];
                            Complex32[] complexCoefficients = Vector.RealImageToComplex(real, image);
                            GCHandle complex_GC = GCHandle.Alloc(complexCoefficients, GCHandleType.Pinned);
                            IntPtr complex_Address = complex_GC.AddrOfPinnedObject();
                            error = IPPCaller.ippsFIRSRInit_32fc(complex_Address, filterLength, IPPCaller.IppAlgType.ippAlgAuto, pFIRSpec);
                            complex_GC.Free();
                            break;
                        }
                    default:
                        break;
                }

                //保存描述符并返回
                if (error == 0) { _filterDescriptors.Add(descriptor); }

                handle = descriptor.Handle;
                return (int)error;
            }
        }

        /// <summary>
        /// Get Filter Descriptor
        /// </summary>
        internal int GetFIRDescriptor(Complex[] filterCoefficients, IPPCaller.IppDataType dataType, out FilterHandle handle)
        {
            IPPCaller.IppStatus error = IPPCaller.IppStatus.ippStsNoErr;

            lock (_filterDescriptors)
            {
                //存在该Key则直接返回
                foreach (FIRConfig filter in _filterDescriptors)
                {
                    if (FilterExist(filter, filterCoefficients, dataType, 1, 1))
                    {
                        handle = filter.Handle;
                        return (int)error;
                    }
                }

                //不存在，则创建并配置，保存后返回
                FIRConfig descriptor = new FIRConfig();
                descriptor.Precision = dataType;
                descriptor.Domain = IPPCaller.IppFilterType.COMPLEX;
                descriptor.UpSampleFactor = 1;
                descriptor.DownSampleFactor = 1;
                descriptor.RealCoefficients = null;

                // 复制Filter系数。
                int filterLength = filterCoefficients.Length;
                Complex[] filterCoef = new Complex[filterLength];
                Vector.ArrayCopy(filterCoefficients, filterCoef);
                descriptor.ComplexCoefficients = filterCoef;

                // 计算 Buffer Size。 只有  ipp32fc ipp64fc  有效。    
                int sizeFIRSpec = 0, sizeFIRWorkBuf = 0;
                switch (descriptor.Precision)
                {
                    case IPPCaller.IppDataType.ipp32fc:
                    case IPPCaller.IppDataType.ipp64fc:
                        error = IPPCaller.ippsFIRSRGetSize(filterLength, dataType, ref sizeFIRSpec, ref sizeFIRWorkBuf);
                        break;
                    case IPPCaller.IppDataType.ipp16sc:
                        error = IPPCaller.ippsFIRSRGetSize(filterLength, IPPCaller.IppDataType.ipp32fc, ref sizeFIRSpec, ref sizeFIRWorkBuf);
                        break;
                    case IPPCaller.IppDataType.ipp32f:
                    case IPPCaller.IppDataType.ipp64f:
                    case IPPCaller.IppDataType.ipp16s:
                    default:
                        break;
                }

                // 创建 Filter Spec 缓存.
                descriptor.Handle.FilterSpec = IPPCaller.ippsMalloc_8u(sizeFIRSpec);
                descriptor.Handle.WorkBufSize = sizeFIRWorkBuf;
                IntPtr pFIRSpec = descriptor.Handle.FilterSpec;

                // 初始化 Filter
                switch (dataType)
                {
                    case IPPCaller.IppDataType.ipp16sc:
                    case IPPCaller.IppDataType.ipp32fc:
                        {
                            Complex32[] floatCoefficients = Vector.ConvertToComplex32(filterCoef);
                            GCHandle complex_GC = GCHandle.Alloc(floatCoefficients, GCHandleType.Pinned);
                            IntPtr complex_Address = complex_GC.AddrOfPinnedObject();
                            error = IPPCaller.ippsFIRSRInit_32fc(complex_Address, filterLength, IPPCaller.IppAlgType.ippAlgAuto, pFIRSpec);
                            complex_GC.Free();
                            break;
                        }
                    case IPPCaller.IppDataType.ipp64fc:
                        {
                            GCHandle complex_GC = GCHandle.Alloc(filterCoef, GCHandleType.Pinned);
                            IntPtr complex_Address = complex_GC.AddrOfPinnedObject();
                            error = IPPCaller.ippsFIRSRInit_64fc(complex_Address, filterLength, IPPCaller.IppAlgType.ippAlgAuto, pFIRSpec);
                            complex_GC.Free();
                            break;
                        }
                    case IPPCaller.IppDataType.ipp32f:
                    case IPPCaller.IppDataType.ipp64f:
                    case IPPCaller.IppDataType.ipp16s:
                    default:
                        break;
                }

                //保存描述符并返回
                if (error == 0) { _filterDescriptors.Add(descriptor); }

                handle = descriptor.Handle;
                return (int)error;
            }
        }

        /// <summary>
        /// Get Filter Descriptor
        /// </summary>
        internal int GetFIRDescriptor(double[] filterCoefficients, IPPCaller.IppDataType dataType, int upFactor, int downFactor, out FilterHandle handle)
        {
            IPPCaller.IppStatus error = IPPCaller.IppStatus.ippStsNoErr;

            lock (_filterDescriptors)
            {
                //存在该Key则直接返回
                foreach (FIRConfig config in _filterDescriptors)
                {
                    if (FilterExist(config, filterCoefficients, dataType, upFactor, downFactor))
                    {
                        handle = config.Handle;
                        return (int)error;
                    }
                }

                //不存在，则创建并配置，保存后返回
                FIRConfig descriptor = new FIRConfig();
                descriptor.Precision = dataType;
                descriptor.Domain = IPPCaller.IppFilterType.REAL;
                descriptor.UpSampleFactor = upFactor;
                descriptor.DownSampleFactor = downFactor;
                descriptor.ComplexCoefficients = null;

                // 复制Filter系数。
                int filterLength = filterCoefficients.Length;
                double[] filterCoef = new double[filterLength];
                Vector.ArrayCopy(filterCoefficients, filterCoef);
                descriptor.RealCoefficients = filterCoef;
                // 定义Scaled Filter 系数。对应升采样滤波器，需要补足插零带来的增益损失。
                double[] ScaledFilterCoef = new double[filterLength];
                Vector.ArrayCopy(filterCoef, ScaledFilterCoef);
                Vector.ArrayScale(ScaledFilterCoef, upFactor);

                // 计算 Buffer Size。 对于complex float 有特殊算法                
                int sizeFIRSpec = 0, sizeFIRWorkBuf = 0;
                switch (descriptor.Precision)
                {
                    case IPPCaller.IppDataType.ipp32f:
                    case IPPCaller.IppDataType.ipp64f:
                    case IPPCaller.IppDataType.ipp64fc:
                        error = IPPCaller.ippsFIRMRGetSize(filterLength, upFactor, downFactor, dataType, ref sizeFIRSpec, ref sizeFIRWorkBuf);
                        break;
                    case IPPCaller.IppDataType.ipp32fc:
                        error = IPPCaller.ippsFIRMRGetSize32f_32fc(filterLength, upFactor, downFactor, ref sizeFIRSpec, ref sizeFIRWorkBuf);
                        break;
                    case IPPCaller.IppDataType.ipp16s:
                        error = IPPCaller.ippsFIRMRGetSize(filterLength, upFactor, downFactor, IPPCaller.IppDataType.ipp32f, ref sizeFIRSpec, ref sizeFIRWorkBuf);
                        break;
                    case IPPCaller.IppDataType.ipp16sc:
                        error = IPPCaller.ippsFIRMRGetSize(filterLength, upFactor, downFactor, IPPCaller.IppDataType.ipp32fc, ref sizeFIRSpec, ref sizeFIRWorkBuf);
                        break;
                    default:
                        break;
                }

                // 创建 Filter Spec 缓存.
                descriptor.Handle.FilterSpec = IPPCaller.ippsMalloc_8u(sizeFIRSpec);
                descriptor.Handle.WorkBufSize = sizeFIRWorkBuf;
                IntPtr pFIRSpec = descriptor.Handle.FilterSpec;

                // 初始化 Filter
                switch (dataType)
                {
                    case IPPCaller.IppDataType.ipp32f:
                        {
                            float[] floatCoefficients = Vector.ConvertToFloat(ScaledFilterCoef);
                            error = IPPCaller.ippsFIRMRInit_32f(floatCoefficients, filterLength, upFactor, 0, downFactor, 0, pFIRSpec);
                            break;
                        }
                    case IPPCaller.IppDataType.ipp64f:
                        {
                            error = IPPCaller.ippsFIRMRInit_64f(ScaledFilterCoef, filterLength, upFactor, 0, downFactor, 0, pFIRSpec);
                            break;
                        }
                    case IPPCaller.IppDataType.ipp32fc:
                        {
                            float[] floatCoefficients = Vector.ConvertToFloat(ScaledFilterCoef);
                            error = IPPCaller.ippsFIRMRInit32f_32fc(floatCoefficients, filterLength, upFactor, 0, downFactor, 0, pFIRSpec);
                            break;
                        }
                    case IPPCaller.IppDataType.ipp64fc:
                        {
                            double[] image = new double[filterLength];
                            Complex[] complexCoefficients = Vector.RealImageToComplex(ScaledFilterCoef, image);
                            GCHandle complex_GC = GCHandle.Alloc(complexCoefficients, GCHandleType.Pinned);
                            IntPtr complex_Address = complex_GC.AddrOfPinnedObject();
                            error = IPPCaller.ippsFIRMRInit_64fc(complex_Address, filterLength, upFactor, 0, downFactor, 0, pFIRSpec);
                            complex_GC.Free();
                            break;
                        }
                    case IPPCaller.IppDataType.ipp16s:
                        {
                            float[] floatCoefficients = Vector.ConvertToFloat(ScaledFilterCoef);
                            error = IPPCaller.ippsFIRMRInit_32f(floatCoefficients, filterLength, upFactor, 0, downFactor, 0, pFIRSpec);
                            break;
                        }
                    case IPPCaller.IppDataType.ipp16sc:
                        {
                            float[] real = Vector.ConvertToFloat(ScaledFilterCoef);
                            float[] image = new float[filterLength];
                            Complex32[] complexCoefficients = Vector.RealImageToComplex(real, image);
                            GCHandle complex_GC = GCHandle.Alloc(complexCoefficients, GCHandleType.Pinned);
                            IntPtr complex_Address = complex_GC.AddrOfPinnedObject();
                            error = IPPCaller.ippsFIRMRInit_32fc(complex_Address, filterLength, upFactor, 0, downFactor, 0, pFIRSpec);
                            complex_GC.Free();
                            break;
                        }
                    default:
                        break;
                }

                //保存描述符并返回
                if (error == 0) { _filterDescriptors.Add(descriptor); }

                handle = descriptor.Handle;
                return (int)error;
            }
        }

        /// <summary>
        /// Get Filter Descriptor
        /// </summary>
        internal int GetFIRDescriptor(Complex[] filterCoefficients, IPPCaller.IppDataType dataType, int upFactor, int downFactor, out FilterHandle handle)
        {
            IPPCaller.IppStatus error = IPPCaller.IppStatus.ippStsNoErr;

            lock (_filterDescriptors)
            {
                //存在该Key则直接返回
                foreach (FIRConfig filter in _filterDescriptors)
                {
                    if (FilterExist(filter, filterCoefficients, dataType, upFactor, downFactor))
                    {
                        handle = filter.Handle;
                        return (int)error;
                    }
                }

                //不存在，则创建并配置，保存后返回
                FIRConfig descriptor = new FIRConfig();
                descriptor.Precision = dataType;
                descriptor.Domain = IPPCaller.IppFilterType.COMPLEX;
                descriptor.UpSampleFactor = upFactor;
                descriptor.DownSampleFactor = downFactor;
                descriptor.RealCoefficients = null;

                // 复制Filter系数。
                int filterLength = filterCoefficients.Length;
                Complex[] filterCoef = new Complex[filterLength];
                Vector.ArrayCopy(filterCoefficients, filterCoef);
                descriptor.ComplexCoefficients = filterCoef;
                // 定义Scaled Filter 系数。对应升采样滤波器，需要补足插零带来的增益损失。
                Complex[] ScaledFilterCoef = new Complex[filterLength];
                Vector.ArrayCopy(filterCoef, ScaledFilterCoef);
                Vector.ArrayScale(ScaledFilterCoef, upFactor);


                // 计算 Buffer Size。 只有  ipp32fc ipp64fc  有效。    
                int sizeFIRSpec = 0, sizeFIRWorkBuf = 0;
                switch (descriptor.Precision)
                {
                    case IPPCaller.IppDataType.ipp32fc:
                    case IPPCaller.IppDataType.ipp64fc:
                        error = IPPCaller.ippsFIRMRGetSize(filterLength, upFactor, downFactor, dataType, ref sizeFIRSpec, ref sizeFIRWorkBuf);
                        break;
                    case IPPCaller.IppDataType.ipp16sc:
                        error = IPPCaller.ippsFIRMRGetSize(filterLength, upFactor, downFactor, IPPCaller.IppDataType.ipp32fc, ref sizeFIRSpec, ref sizeFIRWorkBuf);
                        break;
                    case IPPCaller.IppDataType.ipp32f:
                    case IPPCaller.IppDataType.ipp64f:
                    case IPPCaller.IppDataType.ipp16s:
                    default:
                        break;
                }

                // 创建 Filter Spec 缓存.
                descriptor.Handle.FilterSpec = IPPCaller.ippsMalloc_8u(sizeFIRSpec);
                descriptor.Handle.WorkBufSize = sizeFIRWorkBuf;
                IntPtr pFIRSpec = descriptor.Handle.FilterSpec;

                // 初始化 Filter
                switch (dataType)
                {
                    case IPPCaller.IppDataType.ipp16sc:
                    case IPPCaller.IppDataType.ipp32fc:
                        {
                            Complex32[] floatCoefficients = Vector.ConvertToComplex32(ScaledFilterCoef);
                            GCHandle complex_GC = GCHandle.Alloc(floatCoefficients, GCHandleType.Pinned);
                            IntPtr complex_Address = complex_GC.AddrOfPinnedObject();
                            error = IPPCaller.ippsFIRMRInit_32fc(complex_Address, filterLength, upFactor, 0, downFactor, 0, pFIRSpec);
                            complex_GC.Free();
                            break;
                        }
                    case IPPCaller.IppDataType.ipp64fc:
                        {
                            GCHandle complex_GC = GCHandle.Alloc(ScaledFilterCoef, GCHandleType.Pinned);
                            IntPtr complex_Address = complex_GC.AddrOfPinnedObject();
                            error = IPPCaller.ippsFIRMRInit_64fc(complex_Address, filterLength, upFactor, 0, downFactor, 0, pFIRSpec);
                            complex_GC.Free();
                            break;
                        }
                    case IPPCaller.IppDataType.ipp32f:
                    case IPPCaller.IppDataType.ipp64f:
                    case IPPCaller.IppDataType.ipp16s:
                    default:
                        break;
                }

                //保存描述符并返回
                if (error == 0) { _filterDescriptors.Add(descriptor); }

                handle = descriptor.Handle;
                return (int)error;
            }
        }

        #endregion

        #region---- 私有方法 ----

        private static bool FilterExist(FIRConfig filter, double[] coefficients, IPPCaller.IppDataType pecision, int upFactor, int downFactor)
        {
            return (filter.Precision == pecision) &&
                    (filter.Domain == IPPCaller.IppFilterType.REAL) &&
                    (filter.UpSampleFactor == upFactor) &&
                    (filter.DownSampleFactor == downFactor) &&
                    Vector.ArrayEqual(filter.RealCoefficients, coefficients);
        }

        private static bool FilterExist(FIRConfig filter, Complex[] coefficients, IPPCaller.IppDataType pecision, int upFactor, int downFactor)
        {
            return (filter.Precision == pecision) &&
                    (filter.Domain == IPPCaller.IppFilterType.COMPLEX) &&
                    (filter.UpSampleFactor == upFactor) &&
                    (filter.DownSampleFactor == downFactor) &&
                    Vector.ArrayEqual(filter.ComplexCoefficients, coefficients);
        }

        private static bool GetWorker(out FilterWorker workBuffer, int bufferSize)
        {
            if (_filterWorkers.TryDequeue(out workBuffer))
            {
                // 如果获取的 worker 不合用，重新生成一个。
                if (workBuffer.WorkBufSize < bufferSize)
                {
                    IPPCaller.ippsFree(workBuffer.BufferHandle);
                    workBuffer.BufferHandle = IPPCaller.ippsMalloc_8u(bufferSize);
                    workBuffer.WorkBufSize = bufferSize;
                }
                return true;
            }
            return false;
        }

        private static void ReturnWorker(FilterWorker workBuffer)
        {
            _filterWorkers.Enqueue(workBuffer);
        }

        #endregion

        #region---- FIR ----

        /// <summary>
        /// 执行滤波
        /// </summary>
        internal static void ProcessFIR(short[] pSrc, short[] pDst, int length, short[] pDlySrc, FilterHandle descriptor)
        {
            // 从 Filter 线程池中获取一个 Filter 线程。多线程保护，最多 N 个同时执行。
            _filterWorkerLock.Wait();
            try
            {
                FilterWorker workBuffer; // 定义 Filter worker

                // 获取一个 worker , Dequeue 一定成功。 
                // 多线程保护，不会有两个线程获取同一个 worker (即同时 Dequeue)。
                if (GetWorker(out workBuffer, descriptor.WorkBufSize))
                {
                    // 执行滤波。
                    IPPCaller.ippsFIRSR_16s(pSrc, pDst, length, descriptor.FilterSpec, pDlySrc, pDlySrc, workBuffer.BufferHandle);

                    // worker 使用完毕，归还。
                    ReturnWorker(workBuffer);
                }
            }
            finally
            {
                // 释放 Filter 线程。
                _filterWorkerLock.Release();
            }
        }

        /// <summary>
        /// 执行滤波
        /// </summary>
        internal static void ProcessFIR(float[] pSrc, float[] pDst, int length, float[] pDlySrc, FilterHandle descriptor)
        {
            // 从 Filter 线程池中获取一个 Filter 线程。多线程保护，最多 N 个同时执行。
            _filterWorkerLock.Wait();
            try
            {
                FilterWorker workBuffer; // 定义 Filter worker

                // 获取一个 worker , Dequeue 一定成功。 
                // 多线程保护，不会有两个线程获取同一个 worker (即同时 Dequeue)。
                if (GetWorker(out workBuffer, descriptor.WorkBufSize))
                {
                    // 执行滤波。
                    IPPCaller.ippsFIRSR_32f(pSrc, pDst, length, descriptor.FilterSpec, pDlySrc, pDlySrc, workBuffer.BufferHandle);

                    // worker 使用完毕，归还。
                    ReturnWorker(workBuffer);
                }
            }
            finally
            {
                // 释放 Filter 线程。
                _filterWorkerLock.Release();
            }
        }

        /// <summary>
        /// 执行滤波
        /// </summary>
        internal static void ProcessFIR(double[] pSrc, double[] pDst, int length, double[] pDlySrc, FilterHandle descriptor)
        {
            // 从 Filter 线程池中获取一个 Filter 线程。多线程保护，最多 N 个同时执行。
            _filterWorkerLock.Wait();
            try
            {
                FilterWorker workBuffer; // 定义 Filter worker

                // 获取一个 worker , Dequeue 一定成功。 
                // 多线程保护，不会有两个线程获取同一个 worker (即同时 Dequeue)。
                if (GetWorker(out workBuffer, descriptor.WorkBufSize))
                {
                    // 执行滤波。
                    IPPCaller.ippsFIRSR_64f(pSrc, pDst, length, descriptor.FilterSpec, pDlySrc, pDlySrc, workBuffer.BufferHandle);

                    // worker 使用完毕，归还。
                    ReturnWorker(workBuffer);
                }
            }
            finally
            {
                // 释放 Filter 线程。
                _filterWorkerLock.Release();
            }
        }

        /// <summary>
        /// 执行滤波
        /// </summary>
        internal static void ProcessFIR(Complex[] pSrc, Complex[] pDst, int length, Complex[] pDlySrc, FilterHandle descriptor)
        {
            // 从 Filter 线程池中获取一个 Filter 线程。多线程保护，最多 N 个同时执行。
            _filterWorkerLock.Wait();
            try
            {
                FilterWorker workBuffer; // 定义 Filter worker

                // 获取一个 worker , Dequeue 一定成功。 
                // 多线程保护，不会有两个线程获取同一个 worker (即同时 Dequeue)。
                if (GetWorker(out workBuffer, descriptor.WorkBufSize))
                {
                    GCHandle source_GC = GCHandle.Alloc(pSrc, GCHandleType.Pinned);
                    IntPtr sourece_Address = source_GC.AddrOfPinnedObject();
                    GCHandle destination_GC = GCHandle.Alloc(pDst, GCHandleType.Pinned);
                    IntPtr destination_Address = destination_GC.AddrOfPinnedObject();
                    GCHandle history_GC = GCHandle.Alloc(pDlySrc, GCHandleType.Pinned);
                    IntPtr history_Address = history_GC.AddrOfPinnedObject();

                    // 执行滤波。
                    IPPCaller.ippsFIRSR_64fc(sourece_Address, destination_Address, length, descriptor.FilterSpec, history_Address, history_Address, workBuffer.BufferHandle);

                    // worker 使用完毕，归还。
                    ReturnWorker(workBuffer);

                    source_GC.Free();
                    destination_GC.Free();
                    history_GC.Free();
                }
            }
            finally
            {
                // 释放 Filter 线程。
                _filterWorkerLock.Release();
            }
        }

        /// <summary>
        /// 执行滤波
        /// </summary>
        internal static void ProcessFIR(Complex32[] pSrc, Complex32[] pDst, int length, Complex32[] pDlySrc, FilterHandle descriptor)
        {
            // 从 Filter 线程池中获取一个 Filter 线程。多线程保护，最多 N 个同时执行。
            _filterWorkerLock.Wait();
            try
            {
                FilterWorker workBuffer; // 定义 Filter worker

                // 获取一个 worker , Dequeue 一定成功。 
                // 多线程保护，不会有两个线程获取同一个 worker (即同时 Dequeue)。
                if (GetWorker(out workBuffer, descriptor.WorkBufSize))
                {
                    GCHandle source_GC = GCHandle.Alloc(pSrc, GCHandleType.Pinned);
                    IntPtr sourece_Address = source_GC.AddrOfPinnedObject();
                    GCHandle destination_GC = GCHandle.Alloc(pDst, GCHandleType.Pinned);
                    IntPtr destination_Address = destination_GC.AddrOfPinnedObject();
                    GCHandle history_GC = GCHandle.Alloc(pDlySrc, GCHandleType.Pinned);
                    IntPtr history_Address = history_GC.AddrOfPinnedObject();

                    // 执行滤波。
                    IPPCaller.ippsFIRSR_32fc(sourece_Address, destination_Address, length, descriptor.FilterSpec, history_Address, history_Address, workBuffer.BufferHandle);

                    // worker 使用完毕，归还。
                    ReturnWorker(workBuffer);

                    source_GC.Free();
                    destination_GC.Free();
                    history_GC.Free();
                }
            }
            finally
            {
                // 释放 Filter 线程。
                _filterWorkerLock.Release();
            }
        }

        /// <summary>
        /// 执行滤波
        /// </summary>
        internal static void ProcessFIR_Real(Complex32[] pSrc, Complex32[] pDst, int length, Complex32[] pDlySrc, FilterHandle descriptor)
        {
            // 从 Filter 线程池中获取一个 Filter 线程。多线程保护，最多 N 个同时执行。
            _filterWorkerLock.Wait();
            try
            {
                FilterWorker workBuffer; // 定义 Filter worker

                // 获取一个 worker , Dequeue 一定成功。 
                // 多线程保护，不会有两个线程获取同一个 worker (即同时 Dequeue)。
                if (GetWorker(out workBuffer, descriptor.WorkBufSize))
                {
                    GCHandle source_GC = GCHandle.Alloc(pSrc, GCHandleType.Pinned);
                    IntPtr sourece_Address = source_GC.AddrOfPinnedObject();
                    GCHandle destination_GC = GCHandle.Alloc(pDst, GCHandleType.Pinned);
                    IntPtr destination_Address = destination_GC.AddrOfPinnedObject();
                    GCHandle history_GC = GCHandle.Alloc(pDlySrc, GCHandleType.Pinned);
                    IntPtr history_Address = history_GC.AddrOfPinnedObject();

                    // 执行滤波。
                    IPPCaller.ippsFIRSR32f_32fc(sourece_Address, destination_Address, length, descriptor.FilterSpec, history_Address, history_Address, workBuffer.BufferHandle);

                    // worker 使用完毕，归还。
                    ReturnWorker(workBuffer);

                    source_GC.Free();
                    destination_GC.Free();
                    history_GC.Free();
                }
            }
            finally
            {
                // 释放 Filter 线程。
                _filterWorkerLock.Release();
            }
        }

        #endregion

        #region---- Multi Rate FIR ----

        /// <summary>
        /// 执行滤波
        /// </summary>
        internal static void ProcessMultiRateFIR(short[] pSrc, short[] pDst, int numIters, short[] pDlySrc, FilterHandle descriptor)
        {
            // 从 Filter 线程池中获取一个 Filter 线程。多线程保护，最多 N 个同时执行。
            _filterWorkerLock.Wait();
            try
            {
                FilterWorker workBuffer; // 定义 Filter worker

                // 获取一个 worker , Dequeue 一定成功。 
                // 多线程保护，不会有两个线程获取同一个 worker (即同时 Dequeue)。
                if (GetWorker(out workBuffer, descriptor.WorkBufSize))
                {
                    // 执行滤波。
                    IPPCaller.ippsFIRMR_16s(pSrc, pDst, numIters, descriptor.FilterSpec, pDlySrc, pDlySrc, workBuffer.BufferHandle);

                    // worker 使用完毕，归还。
                    ReturnWorker(workBuffer);
                }
            }
            finally
            {
                // 释放 Filter 线程。
                _filterWorkerLock.Release();
            }
        }

        /// <summary>
        /// 执行滤波
        /// </summary>
        internal static void ProcessMultiRateFIR(float[] pSrc, float[] pDst, int numIters, float[] pDlySrc, FilterHandle descriptor)
        {
            // 从 Filter 线程池中获取一个 Filter 线程。多线程保护，最多 N 个同时执行。
            _filterWorkerLock.Wait();
            try
            {
                FilterWorker workBuffer; // 定义 Filter worker

                // 获取一个 worker , Dequeue 一定成功。 
                // 多线程保护，不会有两个线程获取同一个 worker (即同时 Dequeue)。
                if (GetWorker(out workBuffer, descriptor.WorkBufSize))
                {
                    // 执行滤波。
                    IPPCaller.ippsFIRMR_32f(pSrc, pDst, numIters, descriptor.FilterSpec, pDlySrc, pDlySrc, workBuffer.BufferHandle);

                    // worker 使用完毕，归还。
                    ReturnWorker(workBuffer);
                }
            }
            finally
            {
                // 释放 Filter 线程。
                _filterWorkerLock.Release();
            }
        }

        /// <summary>
        /// 执行滤波
        /// </summary>
        internal static void ProcessMultiRateFIR(double[] pSrc, double[] pDst, int numIters, double[] pDlySrc, FilterHandle descriptor)
        {
            // 从 Filter 线程池中获取一个 Filter 线程。多线程保护，最多 N 个同时执行。
            _filterWorkerLock.Wait();
            try
            {
                FilterWorker workBuffer; // 定义 Filter worker

                // 获取一个 worker , Dequeue 一定成功。 
                // 多线程保护，不会有两个线程获取同一个 worker (即同时 Dequeue)。
                if (GetWorker(out workBuffer, descriptor.WorkBufSize))
                {
                    // 执行滤波。
                    IPPCaller.ippsFIRMR_64f(pSrc, pDst, numIters, descriptor.FilterSpec, pDlySrc, pDlySrc, workBuffer.BufferHandle);

                    // worker 使用完毕，归还。
                    ReturnWorker(workBuffer);
                }
            }
            finally
            {
                // 释放 Filter 线程。
                _filterWorkerLock.Release();
            }
        }

        /// <summary>
        /// 执行滤波
        /// </summary>
        internal static void ProcessMultiRateFIR(Complex[] pSrc, Complex[] pDst, int numIters, Complex[] pDlySrc, FilterHandle descriptor)
        {
            // 从 Filter 线程池中获取一个 Filter 线程。多线程保护，最多 N 个同时执行。
            _filterWorkerLock.Wait();
            try
            {
                FilterWorker workBuffer; // 定义 Filter worker

                // 获取一个 worker , Dequeue 一定成功。 
                // 多线程保护，不会有两个线程获取同一个 worker (即同时 Dequeue)。
                if (GetWorker(out workBuffer, descriptor.WorkBufSize))
                {
                    GCHandle source_GC = GCHandle.Alloc(pSrc, GCHandleType.Pinned);
                    IntPtr sourece_Address = source_GC.AddrOfPinnedObject();
                    GCHandle destination_GC = GCHandle.Alloc(pDst, GCHandleType.Pinned);
                    IntPtr destination_Address = destination_GC.AddrOfPinnedObject();
                    GCHandle history_GC = GCHandle.Alloc(pDlySrc, GCHandleType.Pinned);
                    IntPtr history_Address = history_GC.AddrOfPinnedObject();

                    // 执行滤波。
                    IPPCaller.ippsFIRMR_64fc(sourece_Address, destination_Address, numIters, descriptor.FilterSpec, history_Address, history_Address, workBuffer.BufferHandle);

                    // worker 使用完毕，归还。
                    ReturnWorker(workBuffer);

                    source_GC.Free();
                    destination_GC.Free();
                    history_GC.Free();
                }
            }
            finally
            {
                // 释放 Filter 线程。
                _filterWorkerLock.Release();
            }
        }

        /// <summary>
        /// 执行滤波
        /// </summary>
        internal static void ProcessMultiRateFIR(Complex32[] pSrc, Complex32[] pDst, int numIters, Complex32[] pDlySrc, FilterHandle descriptor)
        {
            // 从 Filter 线程池中获取一个 Filter 线程。多线程保护，最多 N 个同时执行。
            _filterWorkerLock.Wait();
            try
            {
                FilterWorker workBuffer; // 定义 Filter worker

                // 获取一个 worker , Dequeue 一定成功。 
                // 多线程保护，不会有两个线程获取同一个 worker (即同时 Dequeue)。
                if (GetWorker(out workBuffer, descriptor.WorkBufSize))
                {
                    GCHandle source_GC = GCHandle.Alloc(pSrc, GCHandleType.Pinned);
                    IntPtr sourece_Address = source_GC.AddrOfPinnedObject();
                    GCHandle destination_GC = GCHandle.Alloc(pDst, GCHandleType.Pinned);
                    IntPtr destination_Address = destination_GC.AddrOfPinnedObject();
                    GCHandle history_GC = GCHandle.Alloc(pDlySrc, GCHandleType.Pinned);
                    IntPtr history_Address = history_GC.AddrOfPinnedObject();

                    // 执行滤波。
                    IPPCaller.ippsFIRMR_32fc(sourece_Address, destination_Address, numIters, descriptor.FilterSpec, history_Address, history_Address, workBuffer.BufferHandle);

                    // worker 使用完毕，归还。
                    ReturnWorker(workBuffer);

                    source_GC.Free();
                    destination_GC.Free();
                    history_GC.Free();
                }
            }
            finally
            {
                // 释放 Filter 线程。
                _filterWorkerLock.Release();
            }
        }

        /// <summary>
        /// 执行滤波
        /// </summary>
        internal static void ProcessMultiRateFIR_Real(Complex32[] pSrc, Complex32[] pDst, int numIters, Complex32[] pDlySrc, FilterHandle descriptor)
        {
            // 从 Filter 线程池中获取一个 Filter 线程。多线程保护，最多 N 个同时执行。
            _filterWorkerLock.Wait();
            try
            {
                FilterWorker workBuffer; // 定义 Filter worker

                // 获取一个 worker , Dequeue 一定成功。 
                // 多线程保护，不会有两个线程获取同一个 worker (即同时 Dequeue)。
                if (GetWorker(out workBuffer, descriptor.WorkBufSize))
                {
                    GCHandle source_GC = GCHandle.Alloc(pSrc, GCHandleType.Pinned);
                    IntPtr sourece_Address = source_GC.AddrOfPinnedObject();
                    GCHandle destination_GC = GCHandle.Alloc(pDst, GCHandleType.Pinned);
                    IntPtr destination_Address = destination_GC.AddrOfPinnedObject();
                    GCHandle history_GC = GCHandle.Alloc(pDlySrc, GCHandleType.Pinned);
                    IntPtr history_Address = history_GC.AddrOfPinnedObject();

                    // 执行滤波。
                    IPPCaller.ippsFIRMR32f_32fc(sourece_Address, destination_Address, numIters, descriptor.FilterSpec, history_Address, history_Address, workBuffer.BufferHandle);

                    // worker 使用完毕，归还。
                    ReturnWorker(workBuffer);

                    source_GC.Free();
                    destination_GC.Free();
                    history_GC.Free();
                }
            }
            finally
            {
                // 释放 Filter 线程。
                _filterWorkerLock.Release();
            }
        }

        #endregion
    }

    /// <summary>
    /// 互相关
    /// </summary>
    public class CrossCorrelation
    {
        /// <summary>
        /// Double互相关
        /// </summary>
        public static void ComputeCrossCorrelation(double[] src1, double[] src2, out double[] destination)
        {
            int length = src1.Length + src2.Length - 1;
            destination = new double[length];
            int bufferSize;
            IPPCaller.ippsCrossCorrNormGetBufferSize(src1.Length, src2.Length, length, (1 - src1.Length), IPPCaller.IppDataType.ipp64f, 0, out bufferSize);
            IntPtr workBuffer = IPPCaller.ippsMalloc_8u(bufferSize);
            IPPCaller.ippsCrossCorrNorm_64f(src1, src1.Length, src2, src2.Length, destination, destination.Length, (1 - src1.Length), 0, workBuffer);
            IPPCaller.ippsFree(workBuffer);
        }

        /// <summary>
        /// float互相关
        /// </summary>
        public static void ComputeCrossCorrelation(float[] src1, float[] src2, out float[] destination)
        {
            int length = src1.Length + src2.Length - 1;
            destination = new float[length];
            int bufferSize;
            IPPCaller.ippsCrossCorrNormGetBufferSize(src1.Length, src2.Length, length, (1 - src1.Length), IPPCaller.IppDataType.ipp32f, 0, out bufferSize);
            IntPtr workBuffer = IPPCaller.ippsMalloc_8u(bufferSize);
            IPPCaller.ippsCrossCorrNorm_32f(src1, src1.Length, src2, src2.Length, destination, destination.Length, (1 - src1.Length), 0, workBuffer);
            IPPCaller.ippsFree(workBuffer);
        }

        /// <summary>
        /// Complex互相关
        /// </summary>
        public static void ComputeCrossCorrelation(Complex[] src1, Complex[] src2, out Complex[] destination)
        {
            int length = src1.Length + src2.Length - 1;
            destination = new Complex[length];
            int bufferSize;
            IPPCaller.ippsCrossCorrNormGetBufferSize(src1.Length, src2.Length, length, (1 - src1.Length), IPPCaller.IppDataType.ipp64fc, 0, out bufferSize);

            GCHandle src1_GC = GCHandle.Alloc(src1, GCHandleType.Pinned);
            IntPtr src1_Address = src1_GC.AddrOfPinnedObject();

            GCHandle src2_GC = GCHandle.Alloc(src2, GCHandleType.Pinned);
            IntPtr src2_Address = src2_GC.AddrOfPinnedObject();

            GCHandle dst_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr dst_Address = dst_GC.AddrOfPinnedObject();

            IntPtr workBuffer = IPPCaller.ippsMalloc_8u(bufferSize);
            IPPCaller.ippsCrossCorrNorm_64fc(src1_Address, src1.Length, src2_Address, src2.Length, dst_Address, destination.Length, (1 - src1.Length), 0, workBuffer);
            IPPCaller.ippsFree(workBuffer);

            src1_GC.Free();
            src2_GC.Free();
            dst_GC.Free();
        }

        /// <summary>
        /// Complex32互相关
        /// </summary>
        public static void ComputeCrossCorrelation(Complex32[] src1, Complex32[] src2, out Complex32[] destination)
        {
            int length = src1.Length + src2.Length - 1;
            destination = new Complex32[length];
            int bufferSize;
            IPPCaller.ippsCrossCorrNormGetBufferSize(src1.Length, src2.Length, length, (1 - src1.Length), IPPCaller.IppDataType.ipp32fc, 0, out bufferSize);

            GCHandle src1_GC = GCHandle.Alloc(src1, GCHandleType.Pinned);
            IntPtr src1_Address = src1_GC.AddrOfPinnedObject();

            GCHandle src2_GC = GCHandle.Alloc(src2, GCHandleType.Pinned);
            IntPtr src2_Address = src2_GC.AddrOfPinnedObject();

            GCHandle dst_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr dst_Address = dst_GC.AddrOfPinnedObject();

            IntPtr workBuffer = IPPCaller.ippsMalloc_8u(bufferSize);
            IPPCaller.ippsCrossCorrNorm_32fc(src1_Address, src1.Length, src2_Address, src2.Length, dst_Address, destination.Length, (1 - src1.Length), 0, workBuffer);
            IPPCaller.ippsFree(workBuffer);

            src1_GC.Free();
            src2_GC.Free();
            dst_GC.Free();
        }
    }

    public class FrequencyShift<T>
    {
        private double _frequency;
        public double Frequency
        {
            set { _frequency = value; }
            get { return _frequency; }
        }

        private double _phase;
        private double Phase
        {
            set { _phase = value; }
            get { return _phase; }
        }

        public FrequencyShift(double frequency = 0, double phase = 0)
        {
            _frequency = frequency;
            _phase = phase;
        }

        public void Process(T[] input, T[] output, double frequency)
        {
            _frequency = frequency;
            this.Process(input, output);
        }

        public void Process(T[] input, T[] output)
        {
            if (input is Complex[])
            {
                Complex[] source_fc64 = input as Complex[];
                Complex[] desination = output as Complex[];
                Vector.ArrayCopy(source_fc64, desination);
            }
            else if (input is Complex32[])
            {
                Complex32[] source_fc64 = input as Complex32[];
                Complex32[] desination = output as Complex32[];
                Vector.ArrayCopy(source_fc64, desination);
            }
            this.Process(output, _frequency);
        }

        public void Process(T[] inout, double frequency)
        {
            _frequency = frequency;
            this.Process(inout);
        }

        public void Process(T[] inout)
        {
            if (inout is Complex[])
            {
                Complex[] source_fc64 = inout as Complex[];
                Complex[] frequency = Vector.ToneInit(source_fc64.Length, _frequency, ref _phase);
                Vector.ArrayMulti(source_fc64, frequency);
            }
            else if (inout is Complex32[])
            {
                Complex32[] source_fc64 = inout as Complex32[];
                float tempPhase = (float)_phase;
                Complex32[] frequency = Vector.ToneInit(source_fc64.Length, (float)_frequency, ref tempPhase);
                _phase = (double)tempPhase;
                Vector.ArrayMulti(source_fc64, frequency);
            }
        }
    }

    internal class IPPCaller
    {
        #region---- IPP DLL Caller ----
#if dspLinux
        private const string IPPCoreFilePath = @"ipps.dll";
#else
        private const string IPPCoreFilePath = @"jxiIPP.dll";
#endif
        const CallingConvention IPPCallingConvertion = CallingConvention.Winapi;

        #region Memory

        // Malloc
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IntPtr ippsMalloc_8u(int len);

        // Free
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern void ippsFree(IntPtr ptr);

        #endregion

        #region Get Single Rate FIR Size

        // filter config by same coefficient type
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRSRGetSize(int tapsLen, IppDataType tapsType, ref int pSpecSize, ref int pBufSize);

        // complex filter config by real coefficients
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRSRGetSize32f_32fc(int tapsLen, ref int pSpecSize, ref int pBufSize);

        #endregion

        #region Init Single Rate FIR

        // float filter config
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRSRInit_32f(float[] pTaps, int tapsLen, IppAlgType algType, IntPtr pSpec);

        // double filter config
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRSRInit_64f(double[] pTaps, int tapsLen, IppAlgType algType, IntPtr pSpec);

        // Complex32 filter config
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRSRInit_32fc(IntPtr pTaps, int tapsLen, IppAlgType algType, IntPtr pSpec);

        // Complex filter config
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRSRInit_64fc(IntPtr pTaps, int tapsLen, IppAlgType algType, IntPtr pSpec);

        // Ccomplex32 filter config with float coefficients
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRSRInit32f_32fc(float[] pTaps, int tapsLen, IppAlgType algType, IntPtr pSpec);

        #endregion

        #region Do Single Rate FIR

        // I16 filter with float filter config coefficients
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRSR_16s(short[] pSrc, short[] pDst, int numIters, IntPtr pSpec, short[] pDlySrc, short[] pDlyDst, IntPtr pBuf);

        // float filter
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRSR_32f(float[] pSrc, float[] pDst, int numIters, IntPtr pSpec, float[] pDlySrc, float[] pDlyDst, IntPtr pBuf);

        // double filter
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRSR_64f(double[] pSrc, double[] pDst, int numIters, IntPtr pSpec, double[] pDlySrc, double[] pDlyDst, IntPtr pBuf);

        // I16 complex filter with float complex filter config coefficients
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRSR_16sc(IntPtr pSrc, IntPtr pDst, int numIters, IntPtr pSpec, IntPtr pDlySrc, IntPtr pDlyDst, IntPtr pBuf);

        // Complex32 filter
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRSR_32fc(IntPtr pSrc, IntPtr pDst, int numIters, IntPtr pSpec, IntPtr pDlySrc, IntPtr pDlyDst, IntPtr pBuf);

        // Complex filter
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRSR_64fc(IntPtr pSrc, IntPtr pDst, int numIters, IntPtr pSpec, IntPtr pDlySrc, IntPtr pDlyDst, IntPtr pBuf);

        // Complex32 filter with real config coefficients
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRSR32f_32fc(IntPtr pSrc, IntPtr pDst, int numIters, IntPtr pSpec, IntPtr pDlySrc, IntPtr pDlyDst, IntPtr pBuf);

        #endregion

        #region Get Multi Rate FIR Size

        // filter config by same coefficient type
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRMRGetSize(int tapsLen, int upFactor, int downFactor, IppDataType tapsType, ref int pSpecSize, ref int pBufSize);

        // complex filter config by real coefficients
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRMRGetSize32f_32fc(int tapsLen, int upFactor, int downFactor, ref int pSpecSize, ref int pBufSize);

        #endregion

        #region Init Multi Rate FIR

        // float filter config
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRMRInit_32f(float[] pTaps, int tapsLen, int upFactor, int upPhase, int downFactor, int downPhase, IntPtr pSpec);

        // double filter config
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRMRInit_64f(double[] pTaps, int tapsLen, int upFactor, int upPhase, int downFactor, int downPhase, IntPtr pSpec);

        // Complex32 filter config
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRMRInit_32fc(IntPtr pTaps, int tapsLen, int upFactor, int upPhase, int downFactor, int downPhase, IntPtr pSpec);

        // Complex filter config
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRMRInit_64fc(IntPtr pTaps, int tapsLen, int upFactor, int upPhase, int downFactor, int downPhase, IntPtr pSpec);

        // Complex32 filter config with float coefficients
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRMRInit32f_32fc(float[] pTaps, int tapsLen, int upFactor, int upPhase, int downFactor, int downPhase, IntPtr pSpec);

        #endregion

        #region Do Multi Rate FIR

        // I16 filter with float filter config coefficients
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRMR_16s(short[] pSrc, short[] pDst, int numIters, IntPtr pSpec, short[] pDlySrc, short[] pDlyDst, IntPtr pBuf);

        // float filter
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRMR_32f(float[] pSrc, float[] pDst, int numIters, IntPtr pSpec, float[] pDlySrc, float[] pDlyDst, IntPtr pBuf);

        // double fitler
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRMR_64f(double[] pSrc, double[] pDst, int numIters, IntPtr pSpec, double[] pDlySrc, double[] pDlyDst, IntPtr pBuf);

        // I16 complex filter with float complex filter config coefficients
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRMR_16sc(IntPtr pSrc, IntPtr pDst, int numIters, IntPtr pSpec, IntPtr pDlySrc, IntPtr pDlyDst, IntPtr pBuf);

        // Complex32 filter
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRMR_32fc(IntPtr pSrc, IntPtr pDst, int numIters, IntPtr pSpec, IntPtr pDlySrc, IntPtr pDlyDst, IntPtr pBuf);

        // Complex filter
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRMR_64fc(IntPtr pSrc, IntPtr pDst, int numIters, IntPtr pSpec, IntPtr pDlySrc, IntPtr pDlyDst, IntPtr pBuf);

        // Complex32 filter with real config coefficients
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsFIRMR32f_32fc(IntPtr pSrc, IntPtr pDst, int numIters, IntPtr pSpec, IntPtr pDlySrc, IntPtr pDlyDst, IntPtr pBuf);

        #endregion

        #region CrossCorrelation

        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsCrossCorrNormGetBufferSize(int src1Len, int src2Len, int dstLen, int lowLag, IppDataType dataType, int algType, out int pBufferSize);

        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsCrossCorrNorm_32f(float[] pSrc1, int src1Len, float[] pSrc2,int src2Len, float[] pDst, int dstLen, int lowLag, int algType, IntPtr pBuffer);
        
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsCrossCorrNorm_64f(double[] pSrc1, int src1Len, double[] pSrc2,int src2Len, double[] pDst, int dstLen, int lowLag, int algType, IntPtr pBuffer);
        
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsCrossCorrNorm_32fc(IntPtr pSrc1, int src1Len, IntPtr pSrc2, int src2Len, IntPtr pDst, int dstLen, int lowLag, int algType, IntPtr pBuffer);
       
        [DllImport(IPPCoreFilePath, CallingConvention = IPPCallingConvertion, SetLastError = false)]
        internal static extern IppStatus ippsCrossCorrNorm_64fc(IntPtr pSrc1, int src1Len, IntPtr pSrc2, int src2Len, IntPtr pDst, int dstLen, int lowLag, int algType, IntPtr pBuffer);

        #endregion

        #region Enum

        /// <summary>
        /// Error Status
        /// </summary>
        internal enum IppStatus
        {

            /* /////////////////////////////////////////////////////////////////////////////
            //        The following enumerator defines a status of Intel(R) IPP operations
            //                     negative value means error
            */
            /* errors */
            ippStsCpuNotSupportedErr = -9999,  /* The target CPU is not supported. */
            ippStsInplaceModeNotSupportedErr = -9998,  /* The inplace operation is currently not supported. */

            /* ippCrypto specific statuses - any changes MUST be done in both repositories -
               IPP & ippCrypto - don't use sts range 1000-2000 - reserved for Crypto */

            ippStsIIRIIRLengthErr = -234, /* Vector length for IIRIIR function is less than 3*(IIR order) */
            ippStsWarpTransformTypeErr = -233, /* The warp transform type is illegal */
            ippStsExceededSizeErr = -232, /* Requested size exceeded the maximum supported ROI size */
            ippStsWarpDirectionErr = -231, /* The warp transform direction is illegal */
            ippStsFilterTypeErr = -230, /* The filter type is incorrect or not supported */
            ippStsNormErr = -229, /* The norm is incorrect or not supported */
            ippStsAlgTypeErr = -228, /* Algorithm type is not supported. */
            ippStsMisalignedOffsetErr = -227, /* The offset is not aligned with an element. */
            ippStsBorderErr = -225, /* Illegal value for border type.*/
            ippStsDitherTypeErr = -224,/* Dithering type is not supported. */
            ippStsUnknownStatusCodeErr = -216, /* Unknown status code. */
            ippStsLzoBrokenStreamErr = -214, /* LZO safe decompression function cannot decode LZO stream.*/
            ippStsRoundModeNotSupportedErr = -213, /* Rounding mode is not supported. */
            ippStsDecimateFractionErr = -212, /* Fraction in Decimate is not supported. */
            ippStsWeightErr = -211, /* Incorrect value for weight. */
            ippStsQualityIndexErr = -210, /* Cannot calculate the quality index for an image filled with a constant. */
            ippStsIIRPassbandRippleErr = -209, /* Ripple in passband for Chebyshev1 design is less than zero, equal to zero, or greater than 29. */
            ippStsFilterFrequencyErr = -208, /* Cutoff frequency of filter is less than zero, equal to zero, or greater than 0.5. */
            ippStsIIRGenOrderErr = -206, /* Order of the IIR filter for design is less than 1, or greater than 12. */
            ippStsConvergeErr = -205, /* The algorithm does not converge. */
            ippStsSizeMatchMatrixErr = -204, /* The sizes of the source matrices are unsuitable. */
            ippStsCountMatrixErr = -203, /* Count value is less than or equal to zero. */
            ippStsRoiShiftMatrixErr = -202, /* RoiShift value is negative or not divisible by the size of the data type. */
            ippStsSrcDataErr = -200, /* The source buffer contains unsupported data.*/
            ippStsSingularErr = -195, /* Matrix is singular. */
            ippStsSparseErr = -194, /* Positions of taps are not in ascending order, or are negative, or repetitive. */
            ippStsRegExpOptionsErr = -190, /* RegExp: Options for the pattern are incorrect. */
            ippStsRegExpErr = -189, /* RegExp: The structure pRegExpState contains incorrect data. */
            ippStsRegExpMatchLimitErr = -188, /* RegExp: The match limit is exhausted. */
            ippStsRegExpQuantifierErr = -187, /* RegExp: Incorrect quantifier. */
            ippStsRegExpGroupingErr = -186, /* RegExp: Incorrect grouping. */
            ippStsRegExpBackRefErr = -185, /* RegExp: Incorrect back reference. */
            ippStsRegExpChClassErr = -184, /* RegExp: Incorrect character class. */
            ippStsRegExpMetaChErr = -183, /* RegExp: Incorrect metacharacter. */
            ippStsStrideMatrixErr = -182, /* Stride value is not positive or not divisible by the size of the data type. */
            ippStsNoiseRangeErr = -125, /* Noise value for Wiener Filter is out of range. */
            ippStsNotEvenStepErr = -108, /* Step value is not pixel multiple. */
            ippStsHistoNofLevelsErr = -107, /* Number of levels for histogram is less than 2. */
            ippStsLUTNofLevelsErr = -106, /* Number of levels for LUT is less than 2. */
            ippStsChannelOrderErr = -60, /* Incorrect order of the destination channels. */
            ippStsDataTypeErr = -59, /* Data type is incorrect or not supported. */
            ippStsQuadErr = -58, /* The quadrangle is nonconvex or degenerates into triangle, line, or point */
            ippStsRectErr = -57, /* Size of the rectangle region is less than, or equal to 1. */
            ippStsCoeffErr = -56, /* Incorrect values for transformation coefficients. */
            ippStsNoiseValErr = -55, /* Incorrect value for noise amplitude for dithering. */
            ippStsDitherLevelsErr = -54, /* Number of dithering levels is out of range. */
            ippStsNumChannelsErr = -53, /* Number of channels is incorrect, or not supported. */
            ippStsCOIErr = -52, /* COI is out of range. */
            ippStsDivisorErr = -51, /* Divisor is equal to zero, function is aborted. */
            ippStsAlphaTypeErr = -50, /* Illegal type of image compositing operation. */
            ippStsGammaRangeErr = -49, /* Gamma range bounds is less than, or equal to zero. */
            ippStsChannelErr = -47, /* Illegal channel number. */
            ippStsToneMagnErr = -46, /* Tone magnitude is less than, or equal to zero. */
            ippStsToneFreqErr = -45, /* Tone frequency is negative, or greater than, or equal to 0.5. */
            ippStsTonePhaseErr = -44, /* Tone phase is negative, or greater than, or equal to 2*PI. */
            ippStsTrnglMagnErr = -43, /* Triangle magnitude is less than, or equal to zero. */
            ippStsTrnglFreqErr = -42, /* Triangle frequency is negative, or greater than, or equal to 0.5. */
            ippStsTrnglPhaseErr = -41, /* Triangle phase is negative, or greater than, or equal to 2*PI. */
            ippStsTrnglAsymErr = -40, /* Triangle asymmetry is less than -PI, or greater than, or equal to PI. */
            ippStsHugeWinErr = -39, /* Kaiser window is too big. */
            ippStsJaehneErr = -38, /* Magnitude value is negative. */
            ippStsStrideErr = -37, /* Stride value is less than the length of the row. */
            ippStsEpsValErr = -36, /* Negative epsilon value. */
            ippStsWtOffsetErr = -35, /* Invalid offset value for wavelet filter. */
            ippStsAnchorErr = -34, /* Anchor point is outside the mask. */
            ippStsMaskSizeErr = -33, /* Invalid mask size. */
            ippStsShiftErr = -32, /* Shift value is less than zero. */
            ippStsSampleFactorErr = -31, /* Sampling factor is less than,or equal to zero. */
            ippStsSamplePhaseErr = -30, /* Phase value is out of range: 0 <= phase < factor. */
            ippStsFIRMRFactorErr = -29, /* MR FIR sampling factor is less than, or equal to zero. */
            ippStsFIRMRPhaseErr = -28, /* MR FIR sampling phase is negative, or greater than, or equal to the sampling factor. */
            ippStsRelFreqErr = -27, /* Relative frequency value is out of range. */
            ippStsFIRLenErr = -26, /* Length of a FIR filter is less than, or equal to zero. */
            ippStsIIROrderErr = -25, /* Order of an IIR filter is not valid. */
            ippStsResizeFactorErr = -24, /* Resize factor(s) is less than, or equal to zero. */
            ippStsInterpolationErr = -23, /* Invalid interpolation mode. */
            ippStsMirrorFlipErr = -22, /* Invalid flip mode. */
            ippStsMoment00ZeroErr = -21, /* Moment value M(0,0) is too small to continue calculations. */
            ippStsThreshNegLevelErr = -20, /* Negative value of the level in the threshold operation. */
            ippStsThresholdErr = -19, /* Invalid threshold bounds. */
            ippStsFftFlagErr = -18, /* Invalid value for the FFT flag parameter. */
            ippStsFftOrderErr = -17, /* Invalid value for the FFT order parameter. */
            ippStsStepErr = -16, /* Step value is not valid. */


            /* start of common with ippCrypto part - any changes MUST be done in both repositories - IPP & ippCrypto */
            ippStsLoadDynErr = -221, /* Error when loading the dynamic library. */
            ippStsLengthErr = -15, /* Incorrect value for string length. */
            ippStsNotSupportedModeErr = -14, /* The requested mode is currently not supported. */
            ippStsContextMatchErr = -13, /* Context parameter does not match the operation. */
            ippStsScaleRangeErr = -12, /* Scale bounds are out of range. */
            ippStsOutOfRangeErr = -11, /* Argument is out of range, or point is outside the image. */
            ippStsDivByZeroErr = -10, /* An attempt to divide by zero. */
            ippStsMemAllocErr = -9, /* Memory allocated for the operation is not enough.*/
            ippStsNullPtrErr = -8, /* Null pointer error. */
            ippStsRangeErr = -7, /* Incorrect values for bounds: the lower bound is greater than the upper bound. */
            ippStsSizeErr = -6, /* Incorrect value for data size. */
            ippStsBadArgErr = -5, /* Incorrect arg/param of the function. */
            ippStsNoMemErr = -4, /* Not enough memory for the operation. */
            ippStsErr = -2, /* Unknown/unspecified error */
            /* no errors */
            ippStsNoErr = 0, /* No errors. */
            /* warnings  */
            ippStsNoOperation = 1, /* No operation has been executed. */
            ippStsDivByZero = 2, /* Zero value(s) for the divisor in the Div function. */
            ippStsWaterfall = 43, /* Cannot load required library, waterfall is used. */
            /* end of common with ippCrypto part */


            ippStsSqrtNegArg = 3, /* Negative value(s) for the argument in the Sqrt function. */
            ippStsInvZero = 4, /* INF result. Zero value was met by InvThresh with zero level. */
            ippStsEvenMedianMaskSize = 5, /* Even size of the Median Filter mask was replaced with the odd one. */
            ippStsLnZeroArg = 7, /* Zero value(s) for the argument in the Ln function. */
            ippStsLnNegArg = 8, /* Negative value(s) for the argument in the Ln function. */
            ippStsNanArg = 9, /* Argument value is not a number. */
            ippStsOverflow = 12, /* Overflow in the operation. */
            ippStsUnderflow = 17, /* Underflow in the operation. */
            ippStsSingularity = 18, /* Singularity in the operation. */
            ippStsDomain = 19, /* Argument is out of the function domain.*/
            ippStsNonIntelCpu = 20, /* The target CPU is not Genuine Intel. */
            ippStsCpuMismatch = 21, /* Cannot set the library for the given CPU. */
            ippStsOvermuchStrings = 26, /* Number of destination strings is more than expected. */
            ippStsOverlongString = 27, /* Length of one of the destination strings is more than expected. */
            ippStsAffineQuadChanged = 28, /* 4th vertex of destination quad is not equal to customer's one. */
            ippStsWrongIntersectROI = 29, /* ROI has no intersection with the source or destination ROI. No operation. */
            ippStsWrongIntersectQuad = 30, /* Quadrangle has no intersection with the source or destination ROI. No operation. */
            ippStsSrcSizeLessExpected = 32, /* DC: Size of the source buffer is less than the expected one.*/
            ippStsDstSizeLessExpected = 33, /* DC: Size of the destination buffer is less than the expected one. */
            ippStsDoubleSize = 35, /* Width or height of image is odd. */
            ippStsNotSupportedCpu = 36, /* The CPU is not supported. */
            ippStsUnknownCacheSize = 37, /* The CPU is supported, but the size of the cache is unknown. */
            ippStsSymKernelExpected = 38, /* The Kernel is not symmetric. */
            ippStsEvenMedianWeight = 39, /* Even weight of the Weighted Median Filter is replaced with the odd one. */
            ippStsWrongIntersectVOI = 40, /* VOI has no intersection with the source or destination volume. No operation. */
            ippStsNoAntialiasing = 46, /* The mode does not support antialiasing. */
            ippStsRepetitiveSrcData = 47, /* DC: The source data is too repetitive.*/
            ippStsSizeWrn = 48, /* The size does not allow to perform full operation. */
            ippStsFeatureNotSupported = 49, /* Current CPU doesn't support at least 1 of the desired features. */
            ippStsUnknownFeature = 50, /* At least one of the desired features is unknown. */
            ippStsFeaturesCombination = 51, /* Wrong combination of features. */
            ippStsAccurateModeNotSupported = 52,/* Accurate mode is not supported. */
        }

        [Flags]
        internal enum IppAlgType
        {
            ippAlgAuto = 0x00000000,
            ippAlgDirect = 0x00000001,
            ippAlgFFT = 0x00000002,
            ippAlgMask = 0x000000FF
        }

        [Flags]
        internal enum IppsNormOp
        {
            ippsNormNone = 0x00000000,// default
            ippsNormA = 0x00000100,// biased normalization
            ippsNormB = 0x00000200,// unbiased normalization
            ippsNormMask = 0x0000FF00,
        }

        /// <summary>
        /// 滤波器数据类型，精度
        /// </summary>
        internal enum IppDataType
        {
            ippUndef = -1,
            ipp1u = 0,
            ipp8u = 1,
            ipp8uc = 2,
            ipp8s = 3,
            ipp8sc = 4,
            ipp16u = 5,
            ipp16uc = 6,
            ipp16s = 7,
            ipp16sc = 8,
            ipp32u = 9,
            ipp32uc = 10,
            ipp32s = 11,
            ipp32sc = 12,
            ipp32f = 13,
            ipp32fc = 14,
            ipp64u = 15,
            ipp64uc = 16,
            ipp64s = 17,
            ipp64sc = 18,
            ipp64f = 19,
            ipp64fc = 20
        }

        /// <summary>
        /// 滤波器系数类型，作用域
        /// </summary>
        internal enum IppFilterType
        {
            // yym_debug: 和MKL FFT的数据类型保持一致
            COMPLEX = 32,
            REAL = 33,
        }

        #endregion

        #endregion
    }
}
