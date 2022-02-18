using System;
using System.Reflection;


namespace SeeSharpTools.JXI.SignalProcessing.Conditioning.EasyFilters
{
    /// <summary>
    ///<para> FIR Filter Class </para>
    ///<para> Simplified Chinese: FIR滤波器类</para>
    /// </summary>
    public class FIRFilter
    {
        #region----------------------------Construct-------------------------------------
        /// <summary>
        /// 构造函数 
        /// </summary>
        static FIRFilter()
        {

        }
        #endregion

        #region---------------Private Fileds-------------------------------
        private double[] _numerator;
        private double[] _status;
        private int _numOfStatus = 0;
        private ulong _filteredPointns = 0;
        #endregion

        #region-------------------Public Fileds----------------------------
        /// <summary>
        /// <para>filtered Pointns</para>
        /// <para>Simplified Chinese:通过滤波的点数</para>
        /// </summary>
        public ulong FilteredPointns
        {
            get
            {
                return _filteredPointns;
            }
        }
        #endregion

        #region--------------------------Public Methods--------------------
        /// <summary>
        /// <para>Clear status registor</para>
        /// <para>Simplified Chinese:清空状态寄存器</para>
        /// </summary>
        public void Reset()
        {
            _filteredPointns = 0;
            if (_status != null)
            {
                for (int i = 0; i < _status.Length; i++)
                {
                    _status[i] = 0;
                }
            }
        }

        /// <summary>
        /// <para>Filter coefficient </para>
        /// <para>Simplified Chinese:滤波器系数</para>
        /// </summary>
        public double[] Coefficients
        {
            set
            {
                var datalength = value.Length;//滤波器系数长度
                if (_numerator==null || _numerator.Length!= datalength)
                {
                    _numerator = new double[datalength];//分子
                }
                Buffer.BlockCopy(value, 0, _numerator, 0, sizeof(double) * datalength);
                _numOfStatus = datalength;//滤波器系数长度
                if (_status==null || _status.Length!=datalength)
                {
                    _status = new double[datalength];
                    Reset();
                }
                //_filteredPointns = 0;
            }
        }

        /// <summary>
        /// <para>This method is FIR filter direct form structure; </para>
        /// <para>where _numerator=numerators, </para>
        /// <para>they are sharing the same status registor _status;</para>
        /// <para>Simplified Chinese:使用直接型FIR滤波器</para>
        /// <para>所有输入信号共享一个状态寄存器</para>
        /// </summary>
        /// <param name="x">input signal Simplified Chinese:输入信号 </param>
        /// <param name="y">output signal same length as _inputWaveform Simplified Chinese:输入信号，长度与输入信号一致</param>
        public void Filter(double[] x, ref double[] y)
        {
            int i = 0;
            int j = 0;
            int k = 0;
            int bLength = _numerator.Length;
            double forwardSum = 0;

            for (i = 0; i < x.Length; i++)
            {
                forwardSum = 0;
                for (j = 0; j < _status.Length; j++) //s1.Length = max (_demoniator.Length, _numerator.Length) set in set _denominator/_numerator
                {
                    k = _status.Length - 1 - j; //in this loop, k goes from last _status to _status[0]
                    if (k > 0)
                    {
                        _status[k] = _status[k - 1];
                        forwardSum += _numerator[k] * _status[k];
                    }
                    else
                    {
                        _status[0] = x[i];
                        forwardSum += _numerator[0] * _status[0];
                    }
                }
                y[i] = forwardSum;
            }
            _filteredPointns += (ulong)x.Length;
        }
        #endregion
    }
}
