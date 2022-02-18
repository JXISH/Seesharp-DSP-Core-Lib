using System;
using System.Linq;

namespace SeeSharpTools.JXI.RFCommunications.Modem
{

    /// <summary>
    /// 数字调制的信源参数。
    /// </summary>
    public class DigitalModMessageParam
    {
        #region ------------------- 公共事件 -------------------

        /// <summary>
        /// 参数值改变时触发的事件。
        /// </summary>
        public event EventHandler ParamChanged;

        #endregion

        #region------------------------- 公共属性 -------------------------

        private DigitalModMessageType _type;
        /// <summary>
        /// 信源类型。
        /// </summary>
        public DigitalModMessageType Type
        {
            get { return _type; }
            set
            {
                //State catching,only perform operation when value changes.
                if (_type != value)
                {
                    //update private filed and invoke event.
                    _type = value;
                    ParamChanged?.Invoke(this, null);
                }
            }
        }

        private int _prbsOrder;
        /// <summary>
        /// 当信源类型为PRBS时，PRBS的阶数。
        /// </summary>
        public int PrbsOrder
        {
            get { return _prbsOrder; }
            set
            {
                //State catching,only perform operation when value changes.
                if (_prbsOrder != value)
                {
                    //update private filed and invoke event.
                    _prbsOrder = value;
                    ParamChanged?.Invoke(this, null);
                }
            }
        }

        private int _prbsSeed;
        /// <summary>
        /// 当信源类型为PRBS时，PRBS生成所用的Seed。
        /// </summary>
        public int PrbsSeed
        {
            get { return _prbsSeed; }
            set
            {
                //State catching,only perform operation when value changes.
                if (_prbsSeed != value)
                {
                    //update private filed and invoke event.
                    _prbsSeed = value;
                    ParamChanged?.Invoke(this, null);
                }
            }
        }

        private byte[] _bitSequence;
        /// <summary>
        /// 当信源类型为UserDefinedBitSequence时的信源码流。
        /// </summary>
        public byte[] BitSequence
        {
            get { return _bitSequence; }
        }
        #endregion

        #region------------------------- 公共方法 -------------------------

        /// <summary>
        /// 设置信源码流，当信源类型为UserDefinedBitSequence时调用此方法。
        /// </summary>
        /// <param name="bitSequence"></param>
        public void SetBitSequency(byte[] bitSequence)
        {
            if (bitSequence == null || bitSequence.Length == 0) { throw new ArgumentNullException("Bit Sequence could not be empty."); }

            //如果_bitSequence的值发生改变，触发参数值改变事件
            if (!Enumerable.SequenceEqual(bitSequence, _bitSequence))
            {
                _bitSequence = new byte[bitSequence.Length];
                Array.Copy(bitSequence, _bitSequence, bitSequence.Length);
                ParamChanged?.Invoke(this, null);
            }
        }

        /// <summary>
        /// 复制对象内容。
        /// </summary>
        /// <param name="source"></param>
        public void CopyFrom(DigitalModMessageParam source)
        {
            this.Type = source.Type;
            this.PrbsOrder = source.PrbsOrder;
            this.PrbsSeed = source.PrbsSeed;
            _bitSequence = new byte[source.BitSequence.Length];
            Array.Copy(source.BitSequence, _bitSequence, source.BitSequence.Length);
        }

        #endregion

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        /// 构造函数。
        /// </summary>
        public DigitalModMessageParam()
        {
            this.Type = DigitalModMessageType.PRBS;
            this.PrbsOrder = 9;
            this.PrbsSeed = 0;
            _bitSequence = new byte[2] { 0, 1 };
        }

        #endregion

    }

    /// <summary>
    /// 数字调制的信源（码流）类型。
    /// </summary>
    public enum DigitalModMessageType
    {
        /// <summary>
        /// 伪随机序列（Pseudo-Random Binary Sequence）。
        /// </summary>
        PRBS,

        /// <summary>
        /// 用户自定义的一个序列（码流）。
        /// </summary>
        UserDefinedBitSequence
    }

    /// <summary>
    /// Pulse（脉冲调制）的调制参数。
    /// </summary>
    public class PulseModulationParam
    {
        #region ------------------- 公共事件 -------------------

        /// <summary>
        /// 参数值改变时触发的事件。
        /// </summary>
        public event EventHandler ParamChanged;

        #endregion

        #region------------------------- 公共属性 -------------------------

        private double _period;
        /// <summary>
        /// 脉冲信号的周期，以秒（second）为单位。
        /// </summary>
        public double Period
        {
            get { return _period; }
            set
            {
                //State catching,only perform operation when value changes.
                if (_period != value)
                {
                    //update private filed and invoke event.
                    _period = value;
                    ParamChanged?.Invoke(this, null);
                }
            }
        }

        private double _width;
        /// <summary>
        /// 脉冲信号的脉宽，即高电平的持续时间（不能大于脉冲信号的周期），以秒（second）为单位。
        /// </summary>
        public double Width
        {
            get { return _width; }
            set
            {
                //State catching,only perform operation when value changes
                if (_width != value)
                {
                    //update private filed and invoke event.
                    _width = value;
                    ParamChanged?.Invoke(this, null);
                }
            }
        }

        #endregion

        #region------------------------- 公共方法 -------------------------

        /// <summary>
        /// 复制对象内容。
        /// </summary>
        /// <param name="source"></param>
        public void CopyFrom(PulseModulationParam source)
        {
            this.Period = source.Period;
            this.Width = source.Width;
        }

        #endregion
    }

}
