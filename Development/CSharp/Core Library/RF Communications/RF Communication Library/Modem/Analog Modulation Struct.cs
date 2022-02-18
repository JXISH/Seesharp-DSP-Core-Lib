using SeeSharpTools.JXI.SignalProcessing.Generation;
using System;

namespace SeeSharpTools.JXI.RFCommunications.Modem
{

    /// <summary>
    /// 标准信号类型。
    /// </summary>
    public enum StandardSignalType { CW, AM, FM, }

    #region------------------------- 调制信号生成的参数类 -------------------------

    /// <summary>
    /// 标准信号的参数。
    /// </summary>
    public class StandardSignalGenParam
    {

        #region ------------------- 公共属性 -------------------

        /// <summary>
        /// 信号类型。
        /// </summary>
        public StandardSignalType Type { get; set; }

        /// <summary>
        /// 模拟调制参数，当信号类型为AM/FM/PM时设置。
        /// </summary>
        public AnalogModulationParam AnalogModulation { get; set; }

        #endregion

        #region ------------------- 构造函数 -------------------

        /// <summary>
        /// 构造函数。
        /// </summary>
        public StandardSignalGenParam()
        {
            // 设置公共属性的默认值。
            this.Type = StandardSignalType.CW;
            this.AnalogModulation = new AnalogModulationParam();
        }

        #endregion

        #region------------------------- 公共方法 -------------------------

        /// <summary>
        /// 复制对象内容。
        /// </summary>
        /// <param name="source"></param>
        public void CopyFrom(StandardSignalGenParam source)
        {
            this.Type = source.Type;
            this.AnalogModulation.CopyFrom(source.AnalogModulation);
        }

        #endregion

    }

    /// <summary>
    /// 模拟调制（AM/FM/PM）的调制参数。
    /// </summary>
    public class AnalogModulationParam
    {

        #region------------------------- 公共属性 -------------------------

        /// <summary>
        /// 信源参数。
        /// </summary>
        public AnalogModMessageParam Message { get; }

        /// <summary>
        /// AM（调幅）的调制参数。
        /// </summary>
        public AmplitudeModulationParam AM { get; }

        /// <summary>
        /// FM（调频）的调制参数。
        /// </summary>
        public FrequencyModulationParam FM { get; }

        #endregion

        #region ------------------- 构造函数 -------------------

        /// <summary>
        /// 构造函数。
        /// </summary>
        public AnalogModulationParam()
        {
            //设置公共属性的默认值。
            this.Message = new AnalogModMessageParam();
            this.Message.Type = FunctionGenSignalType.Sine;
            this.Message.Frequency = 1000;

            this.AM = new AmplitudeModulationParam();
            this.AM.Depth = 50;

            this.FM = new FrequencyModulationParam();
            this.FM.Deviation = 25000;
        }

        #endregion

        #region------------------------- 公共方法 -------------------------

        /// <summary>
        /// 复制对象内容。
        /// </summary>
        /// <param name="source"></param>
        public void CopyFrom(AnalogModulationParam source)
        {
            this.Message.CopyFrom(source.Message);
            this.AM.CopyFrom(source.AM);
            this.FM.CopyFrom(source.FM);
        }

        #endregion

    }

    /// <summary>
    /// 模拟调制的信源参数。
    /// </summary>
    public class AnalogModMessageParam
    {
        #region ------------------- 公共事件 -------------------

        /// <summary>
        /// 参数值改变时触发的事件。
        /// </summary>
        public event EventHandler ParamChanged;

        #endregion

        #region------------------------- 公共属性 -------------------------

        private FunctionGenSignalType _type;
        /// <summary>
        /// 波形类型。
        /// </summary>
        public FunctionGenSignalType Type
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

        private double _frequency;
        /// <summary>
        /// 波形频率。
        /// </summary>
        public double Frequency
        {
            get { return _frequency; }
            set
            {
                //State catching,only perform operation when value changes.
                if (_frequency != value)
                {
                    //update private filed and invoke event.
                    _frequency = value;
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
        public void CopyFrom(AnalogModMessageParam source)
        {
            this.Type = source.Type;
            this.Frequency = source.Frequency;
        }

        #endregion

    }

    /// <summary>
    /// AM（调幅）的调制参数。
    /// </summary>
    public class AmplitudeModulationParam
    {
        #region ------------------- 公共事件 -------------------

        /// <summary>
        /// 参数值改变时触发的事件。
        /// </summary>
        public event EventHandler ParamChanged;

        #endregion

        #region------------------------- 公共属性 -------------------------

        private double _depth;
        /// <summary>
        /// 调制深度，%。
        /// </summary>
        public double Depth
        {
            get { return _depth; }
            set
            {
                //State catching,only perform operation when value changes.
                if (_depth != value)
                {
                    //update private filed and invoke event.
                    _depth = value;
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
        public void CopyFrom(AmplitudeModulationParam source)
        {
            this.Depth = source.Depth;
        }

        #endregion

    }

    /// <summary>
    /// FM（调频）的调制参数。
    /// </summary>
    public class FrequencyModulationParam
    {
        #region ------------------- 公共事件 -------------------

        /// <summary>
        /// 参数值改变时触发的事件。
        /// </summary>
        public event EventHandler ParamChanged;

        #endregion

        #region------------------------- 公共属性 -------------------------

        private double _deviation;
        /// <summary>
        /// 调频最大频偏，Hz。
        /// </summary>
        public double Deviation
        {
            get { return _deviation; }
            set
            {
                //State catching,only perform operation when value changes.
                if (_deviation != value)
                {
                    //update private filed and invoke event.
                    _deviation = value;
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
        public void CopyFrom(FrequencyModulationParam source)
        {
            this.Deviation = source.Deviation;
        }

        #endregion

    }

    #endregion
}
