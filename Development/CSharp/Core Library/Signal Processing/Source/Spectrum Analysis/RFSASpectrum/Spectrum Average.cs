using System;
using System.Collections.Generic;
using System.Numerics;
using SeeSharpTools.JXI.Numerics;
using SeeSharpTools.JXI.Mathematics;

namespace SeeSharpTools.JXI.SignalProcessing.SpectrumAnalysis.RFSASpectrum
{
    public class SpectrumAverage
    {
        #region ---------------私有字段定义-----------------

        // 计算平均时，锁定历史数据不被意外更新。
        private object _lockPrivateData;

        // 用于缓存平均后的频谱
        private double[] _averagedPowerSpectrum;
        private float[] _averagedPowerSpectrum_float;

        // 频谱链表，用于存储平均的多个频谱
        private List<double[]> _listPowerSpectrum;
        private List<float[]> _listPowerSpectrum_float;

        private dataPrecision _precision;
        private int _spectrumSize;
        public enum dataPrecision
        {
            SINGLE = 35,
            DOUBLE = 36,
        }

        #endregion

        #region ---------------公共属性定义-----------------

        // 已平均的次数
        private int _averageCount;
        public int AverageCount
        {
            get { return _averageCount; }
        }

        private SpectrumAverageMode _averagingMode;
        /// <summary>
        /// 频谱平均模式，无平均/RMS/PeakHold
        /// </summary>
        public SpectrumAverageMode Mode
        {
            get
            {
                return _averagingMode;
            }
            set
            {
                if (_averagingMode != value)
                {
                    _averagingMode = value;
                    Reset();
                }
            }
        }

        private double _averagingSize;
        /// <summary>
        /// 平均次数
        /// </summary>
        public double Size
        {
            get
            {
                return _averagingSize;
            }
            set
            {
                double temp = Math.Max(1.0, value);
                if (_averagingSize != temp)
                {
                    _averagingSize = temp;
                    Reset();
                }
            }
        }

        private SpectrumWeightingType _weightingType;
        /// <summary>
        /// 加权类型
        /// </summary>
        public SpectrumWeightingType WeightingType
        {
            get { return _weightingType; }
            set
            {
                if (_weightingType != value)
                {
                    _weightingType = value;
                    Reset();
                }
            }
        }

        private SpectrumWeightingMode _weightingMode;
        /// <summary>
        /// 线性平均的方式
        /// </summary>
        public SpectrumWeightingMode WeightingMode
        {
            get { return _weightingMode; }
            set
            {
                if (_weightingMode != value)
                {
                    _weightingMode = value;
                    Reset();
                }
            }
        }

        private SpectralInfo _spectralInfo;
        /// <summary>
        /// 频谱信息
        /// </summary>
        public SpectralInfo SpectralInfo
        {
            get { return _spectralInfo; }
        }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public SpectrumAverage()
        {
            _lockPrivateData = new object();

            _averagingMode = SpectrumAverageMode.NoAveraging;
            _averagingSize = 10;
            _weightingType = SpectrumWeightingType.LinearMoving;
            _weightingMode = SpectrumWeightingMode.Continuous;
            _spectralInfo = new SpectralInfo();

            ResetAnverageSettings(0, dataPrecision.DOUBLE, new SpectralInfo());
        }

        /// <summary>
        /// 重置平均
        /// </summary>
        public void Reset()
        {
            lock (_lockPrivateData)
            {
                _averageCount = 0;//重置平均次数

                int averagingSizeInt = (int)Math.Ceiling(_averagingSize);
                if (_precision == dataPrecision.DOUBLE)
                {
                    if (_listPowerSpectrum.Count > averagingSizeInt)
                    {
                        _listPowerSpectrum.RemoveRange(averagingSizeInt, _listPowerSpectrum.Count - averagingSizeInt); // 删除超过 _averagingSize 的队列元素
                    }
                }
                else if (_precision == dataPrecision.SINGLE)
                {
                    if (_listPowerSpectrum_float.Count > averagingSizeInt)
                    {
                        _listPowerSpectrum_float.RemoveRange(averagingSizeInt, _listPowerSpectrum_float.Count - averagingSizeInt); // 删除超过 _averagingSize 的队列元素
                    }
                }
            }
        }

        /// <summary>
        /// Reset the spectrum averaging, set the average count to zero.
        /// </summary>
        private void ResetAnverageSettings(int spectrumSize, dataPrecision precision, SpectralInfo spectralInfo)
        {
            _averageCount = 0; //重置平均次数
            _precision = precision;
            _spectrumSize = spectrumSize;

            _spectralInfo.CopyFrom(spectralInfo); // 重置频谱信息

            if (_precision == dataPrecision.DOUBLE)
            {
                _averagedPowerSpectrum = new double[_spectrumSize];
                if (_listPowerSpectrum == null) { _listPowerSpectrum = new List<double[]>(); }
                _listPowerSpectrum.Clear(); //清空用于平均的多个频谱

                if (_listPowerSpectrum_float != null)
                {
                    _listPowerSpectrum_float.Clear();
                    _listPowerSpectrum_float = null;
                }
                _averagedPowerSpectrum_float = null;
            }

            else if (_precision == dataPrecision.SINGLE)
            {
                _averagedPowerSpectrum_float = new float[_spectrumSize];
                if (_listPowerSpectrum_float == null) { _listPowerSpectrum_float = new List<float[]>(); }
                _listPowerSpectrum_float.Clear(); //清空用于平均的多个频谱

                if (_listPowerSpectrum != null)
                {
                    _listPowerSpectrum.Clear();
                    _listPowerSpectrum = null;
                }
                _averagedPowerSpectrum = null;
            }
        }

        /// <summary>
        /// 获取内部double类型数据
        /// 内部使用，不改变值
        /// </summary>
        internal double[] GetAverageSpectrumRef()
        {
            double[] data = null;
            if (_precision == dataPrecision.DOUBLE)
            {
                return _averagedPowerSpectrum;
            }
            else if (_precision == dataPrecision.SINGLE)
            {
                if (true)
                {
                    data = Vector.ConvertToDouble(_averagedPowerSpectrum_float);
                }
                else
                {
                    #region 保留历史代码
                    data = new double[_spectrumSize];
                    Array.Copy(_averagedPowerSpectrum_float, data, _spectrumSize);
                    #endregion
                }
            }
            return data;
        }

        /// <summary>
        /// 频谱平均
        /// </summary>
        public void AverageSpectrum(double[] sourceSpectrum, double[] averagedSpectrum, SpectralInfo spectralInfo)
        {
            if (averagedSpectrum.Length < sourceSpectrum.Length) { throw (new ArgumentException("Averaged Spectrum Size Unvalid.")); }
            Array.Copy(sourceSpectrum, averagedSpectrum, sourceSpectrum.Length);
            AverageSpectrum(averagedSpectrum, spectralInfo);
        }

        /// <summary>
        /// 频谱平均
        /// </summary>
        public void AverageSpectrum(float[] sourceSpectrum, float[] averagedSpectrum, SpectralInfo spectralInfo)
        {
            if (averagedSpectrum.Length < sourceSpectrum.Length) { throw (new ArgumentException("Averaged Spectrum Size Unvalid.")); }
            Array.Copy(sourceSpectrum, averagedSpectrum, sourceSpectrum.Length);
            AverageSpectrum(averagedSpectrum, spectralInfo);
        }

        /// <summary>
        /// 频谱平均
        /// </summary>
        public void AverageSpectrum(double[] newPowerSpectrum, SpectralInfo spectralInfo)
        {
            lock (_lockPrivateData)
            {
                if (_spectrumSize != newPowerSpectrum.Length || _precision != dataPrecision.DOUBLE || (!_spectralInfo.Equals(spectralInfo))) // 数组长度不同，重置
                {
                    ResetAnverageSettings(newPowerSpectrum.Length, dataPrecision.DOUBLE, spectralInfo);
                }

                AverageSpectrumCore(newPowerSpectrum, _averagedPowerSpectrum,  _listPowerSpectrum);
            }
        }

        /// <summary>
        /// 频谱平均
        /// </summary>
        public void AverageSpectrum(float[] newPowerSpectrum, SpectralInfo spectralInfo)
        {
            lock (_lockPrivateData)
            {
                if (_spectrumSize != newPowerSpectrum.Length || _precision != dataPrecision.SINGLE || (!_spectralInfo.Equals(spectralInfo))) // 数组长度不同，重置
                {
                    ResetAnverageSettings(newPowerSpectrum.Length, dataPrecision.SINGLE, spectralInfo);
                }

                AverageSpectrumCore(newPowerSpectrum, _averagedPowerSpectrum_float, _listPowerSpectrum_float);
            }
        }

        /// <summary>
        /// 频谱平均
        /// </summary>
        private void AverageSpectrumCore<T>(T[] newPowerSpectrum, T[] averagedData, List<T[]> historyData)
        {
            #region 完成1组平均
            if (_averageCount >= _averagingSize)
            {
                #region OneShot完成，返回
                if (_weightingMode == SpectrumWeightingMode.OneShot && _averagingMode != SpectrumAverageMode.NoAveraging) 
                {
                    Vector.ArrayCopy(averagedData, newPowerSpectrum);
                    return;
                }
                #endregion

                #region AutoRestartOneShot，重置平均
                if (_weightingMode == SpectrumWeightingMode.AutoRestartOneShot) 
                {
                    _averageCount = 0;                                          //重置平均次数
                }
                #endregion
            }
            #endregion

            #region 根据设置计算平均
            if (_averagingMode == SpectrumAverageMode.NoAveraging)
            {
                _averageCount = 1;
                Vector.ArrayCopy(newPowerSpectrum, averagedData);
                return;
            }
            else if (_averagingMode == SpectrumAverageMode.RMSAveraging)
            {
                _averageCount = RMSAverage(newPowerSpectrum, averagedData, _averageCount, _averagingSize, _weightingType, historyData);
            }
            else if (_averagingMode == SpectrumAverageMode.PeakHold)
            {
                _averageCount = PeakHold(newPowerSpectrum, averagedData, _averageCount);
            }
            else if (_averagingMode == SpectrumAverageMode.NegativePeak)
            {
                _averageCount = NegativePeak(newPowerSpectrum, averagedData, _averageCount);
            }
            #endregion

            Vector.ArrayCopy(averagedData, newPowerSpectrum);
        }

        private static int RMSAverage<T>(T[] newData, T[] averagedData, int averageCount, double averagingSize, SpectrumWeightingType weightingType, List<T[]> historyData)
        {
            if (weightingType == SpectrumWeightingType.LinearMoving)
            {
                averageCount = MovingAverage(newData, averagedData, averageCount, averagingSize, historyData);
            }
            else if (weightingType == SpectrumWeightingType.LinearContinuous)
            {
                averageCount = ContinuousAverage(newData, averagedData, averageCount);
            }
            else if (weightingType == SpectrumWeightingType.Exponential)
            {
                averageCount = ExponentialAverage(newData, averagedData, averageCount, averagingSize);
            }
            return averageCount;
        }

        private static int PeakHold<T>(T[] power, T[] averagedSpectrum, int averageCount)
        {
            if (averageCount > 0)
            {
                Vector.ArrayMax(averagedSpectrum, power);
            }
            else
            {
                Vector.ArrayCopy(power, averagedSpectrum);
            }
            averageCount++;
            return averageCount;
        }

        private static int NegativePeak<T>(T[] power, T[] averagedSpectrum, int averageCount)
        {
            if (averageCount > 0)
            {
                Vector.ArrayMin(averagedSpectrum, power);
            }
            else
            {
                Vector.ArrayCopy(power, averagedSpectrum);
            }
            averageCount++;
            return averageCount;
        }

        private static int MovingAverage<T>(T[] newData, T[] averagedData, int averageCount, double averagingSize, List<T[]> historyData)
        {
            if (averageCount < averagingSize)
            {
                T[] newSpectrumToAdd;
                if (averageCount >= historyData.Count)
                {
                    newSpectrumToAdd = new T[newData.Length];                   // 历史数据队列不够长，创建新历史数据
                }
                else
                {
                    newSpectrumToAdd = historyData[0];                          // 历史数据队列够长，使用旧历史数据内存。仅发生在Reset后，不改变平均长度才会进入
                    historyData.RemoveAt(0);
                }
                Vector.ArrayCopy(newData, newSpectrumToAdd);                        // 复制当前数据
                historyData.Add(newSpectrumToAdd);                                  // 添加到队列最后

                averageCount++;                                                     // 按照公式计算平均值
                Vector.ArraySub(newData, averagedData);                             // averagedSpectrum  + ( newSpectrum -averagedSpectrum ) / _averageCount;
                Vector.ArrayScale(newData, 1.0 / averageCount);
                Vector.ArrayAdd(averagedData, newData);
            }
            else
            {
                T[] oldestSpectrum = historyData[0];
                historyData.RemoveAt(0);                                             // 从队列中删除最旧的数据
                Vector.ArrayCopy(newData, oldestSpectrum);                           // 将当前值复制到临时数组中  
                historyData.Add(oldestSpectrum);                                     // 将当前值添加到队列最后。临时数组内存没有被释放，因此节省重开内存开销。

                if (true)
                {
                    Vector.ArrayCopy(historyData[0], averagedData);                   // 按照公式计算平均值
                    for (int i = 1; i < historyData.Count; i++)
                    {
                        Vector.ArrayAdd(averagedData, historyData[i]);
                    }
                    Vector.ArrayScale(averagedData, 1.0 / averageCount);
                }
                else
                {
                    #region 保留旧版本代码
                    Vector.ArraySub(oldestSpectrum, newData);                          // 按照公式计算平均值
                    Vector.ArrayScale(oldestSpectrum, -1.0 / averageCount);            // averagedSpectrum - ( _spectrumList[0] - newSpectrum ) / _averageCount;
                    Vector.ArrayAdd(averagedData, oldestSpectrum);
                    #endregion
                }
            }

            return averageCount;
        }

        private static int ContinuousAverage<T>(T[] newData, T[] averageData, int averageCount)
        {
            // averagedSpectrum + ( newSpectrum - averagedSpectrum ) / _averageCount;
            averageCount++;
            Vector.ArraySub(newData, averageData);
            Vector.ArrayScale(newData, 1.0 / averageCount);
            Vector.ArrayAdd(averageData, newData);
            return averageCount;
        }

        private static int ExponentialAverage<T>(T[] newData, T[] averagedData, int averageCount, double averagingSize)
        {
            if (averageCount < averagingSize)
            {
                averageCount++;
            }
            // averagedSpectrum + ( newSpectrum - averagedSpectrum ) / _averageCount;
            Vector.ArraySub(newData, averagedData);
            Vector.ArrayScale(newData, 1.0 / averageCount);
            Vector.ArrayAdd(averagedData, newData);
            return averageCount;
        }
    }

    #region ---- 枚举定义 ----

    /// <summary>
    /// 频谱平均方式定义
    /// </summary>
    public enum SpectrumAverageMode
    {
        /// <summary>
        /// 无平均
        /// </summary>
        NoAveraging,

        /// <summary>
        /// RMS平均
        /// </summary>
        RMSAveraging,

        /// <summary>
        /// 峰值保持
        /// </summary>
        PeakHold,

        /// <summary>
        /// 负峰值
        /// </summary>
        NegativePeak,
    }

    /// <summary>
    /// 设定平均的加权方式
    /// </summary>
    public enum SpectrumWeightingType
    {
        /// <summary>
        /// 线性滑动
        /// </summary>
        LinearMoving,

        /// <summary>
        /// 线性连续
        /// </summary>
        LinearContinuous,

        /// <summary>
        /// 指数平均
        /// </summary>
        Exponential
    }

    /// <summary>
    /// 平均加权次数
    /// </summary>
    public enum SpectrumWeightingMode
    {
        /// <summary>
        /// 只计算一次
        /// </summary>
        OneShot,

        /// <summary>
        /// 完成一次重新开始
        /// </summary>
        AutoRestartOneShot,

        /// <summary>
        /// 连续计算
        /// </summary>
        Continuous
    }

    #endregion
}
