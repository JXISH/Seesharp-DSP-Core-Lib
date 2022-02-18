using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeeSharpTools.JXI.Mathematics.ProbabilityStatistics
{
    /// <summary>
    /// point-by-point计算最大最小值、平均数
    /// 将来如果要增加stdev, histogram等功能，可以设立属性flag打开或关闭某些统计，以优化CPU占用
    /// 2022-02-01 来自VST\Digital Signal Processing\Math Utility
    /// </summary>
    public class PointByPointStatistics
    {
        #region------------------------- 公共属性 -------------------------

        private double _value;
        /// <summary>
        /// 当前数值。
        /// </summary>
        public double Value { get { return (_count > 0) ? _value : double.NaN; } }

        private int _count;
        /// <summary>
        /// 已更新的测量值次数。
        /// </summary>
        public int Count { get { return _count; } }

        private int _historyLength;
        /// <summary>
        /// 历史数据长度。
        /// </summary>
        public int HistoryLength
        {
            get { return _historyLength; }
            set
            {
                if (value > 1)
                {
                    if (value != _historyLength)
                    {
                        // 重新设置历史数据的数值长度。
                        _historyLength = value;

                        // 更新历史数据列表容量，若新设置的长度比现有长度小，则先删除多余的数据点。
                        if (value < _historyValues.Capacity) { _historyValues.RemoveRange(0, _historyValues.Capacity - value); }
                        _historyValues.Capacity = value;
                    }
                }
                else { throw new ArgumentOutOfRangeException("History length must be greater than 0"); }
            }
        }

        private List<double> _historyValues;
        /// <summary>
        /// 历史数据列表。
        /// </summary>
        public List<double> HistoryValues { get { return _historyValues; } }

        private double _maxValue;
        /// <summary>
        /// 自实例化或Reset()以来的最大值，注：并非滑动的历史数据最大值。
        /// </summary>
        public double MaxValue
        {
            get
            {
                if (_count > 0) { return _maxValue; }
                else { return double.NaN; }
            }
        }

        private double _minValue;
        /// <summary>
        /// 自实例化或Reset()以来的最小值，注：并非滑动的历史数据最小值。
        /// </summary>
        public double MinValue
        {
            get
            {
                if (_count > 0) { return _minValue; }
                else { return double.NaN; }
            }
        }

        /// <summary>
        /// 历史数据的平均值（滑动）。
        /// </summary>
        public double MeanValue
        {
            get
            {
                if (_count > 0)
                {
                    // 计算平均值，待完善，应根据数据类型分别处理（Log数值应转换为线性数值再求和平均）。
                    double sum = 0;
                    foreach (var value in _historyValues) { sum += value; }
                    return sum / _historyValues.Count;
                }
                else { return double.NaN; }
            }
        }

        /// <summary>
        /// 历史数据的中值（滑动）。
        /// </summary>
        public double MedianValue
        {
            get
            {
                if (_count > 0)
                {
                    // 排序后获取中值。
                    double[] sortedArray = _historyValues.ToArray();

                    if (_historyValues.Count % 2 == 0)
                    {
                        // 若历史数据长度为偶数，取中间两个值的平均，待完善，应根据数据类型分别处理（Log数值应转换为线性数值再求和平均）。
                        int index = _historyValues.Count / 2;
                        return (sortedArray[index] + sortedArray[index - 1]) / 2;
                    }
                    else
                    {
                        // 若历史数据长度为奇数，直接取中值。
                        return sortedArray[(_historyValues.Count - 1) / 2];
                    }
                }
                else { return double.NaN; }
            }
        }

        #endregion

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        /// 构造函数。
        /// </summary>
        public PointByPointStatistics() : this(1) {; }

        /// <summary>
        /// 构造函数。
        /// </summary>
        public PointByPointStatistics(int historyLength)
        {
            // 检查输入参数有效。
            if (historyLength <= 0) { throw new ArgumentOutOfRangeException("History length must be greater than 0"); }

            // 创建列表。
            _historyLength = historyLength;
            _historyValues = new List<double>(_historyLength);

            //设置初始值。
            _count = 0;
            _maxValue = double.MinValue;
            _minValue = double.MaxValue;
        }

        #endregion

        #region------------------------- 公共方法 -------------------------

        /// <summary>
        /// 将新的数值添加到历史队列中，若队列已满，则移除最早的数据点。
        /// </summary>
        /// <param name="newValue"></param>
        public void Enqueue(double newValue)
        {
            // 保留输入数值至当前值，并更新最大值和最小值。
            _value = newValue;
            if (_count <= 0)
            {
                _maxValue = newValue;
                _minValue = newValue;
            }
            else
            {
                _maxValue = Math.Max(_maxValue, newValue);
                _minValue = Math.Min(_minValue, newValue);
            }

            // 将当前数值存入历史数据。若列表未满，直接添加，若列表已满，则先移除最老的数据点再添加。
            if (_count >= _historyLength) { _historyValues.RemoveAt(0); }
            _historyValues.Add(newValue);

            // 更新计数器。
            _count++;
        }

        /// <summary>
        /// 复制对象的数据（深复制）。
        /// </summary>
        /// <param name="source"></param>
        public void CopyFrom(PointByPointStatistics source)
        {
            // 复制属性。
            _count = source.Count;
            _value = source.Value;
            _maxValue = source.MaxValue;
            _minValue = source.MinValue;
            _historyLength = source.HistoryLength;

            // 清空自身列表，复制数据。
            _historyValues.Clear();
            _historyValues.Capacity = source.HistoryLength;
            foreach (var value in source.HistoryValues) { _historyValues.Add(value); }

        }

        /// <summary>
        /// 清空历史数据。
        /// </summary>
        public void Reset()
        {
            // 清空历史数据。
            _historyValues.Clear();

            // 清除标记。
            _count = 0;
            _value = double.NaN;
            _maxValue = double.MinValue;
            _minValue = double.MaxValue;
        }

        #endregion

    }
}
