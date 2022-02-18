using SeeSharpTools.JXI.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SeeSharpTools.JXI.RFCommunications.Modem
{
    /// <summary>
    /// 模拟解调，AM PM FM
    /// </summary>
    public class AnalogDemodulation
    {
        /// <summary>
        /// AM解调静态方法
        /// </summary>
        /// <param name="modulatedSignal">输入基带波形</param>
        /// <param name="message">输出实数解调波形，与基带等长度</param>
        public static void AMDemodulate(Complex[] modulatedSignal, ref double[] message)
        {
            //如果输入合理，则进行解调
            if(modulatedSignal!=null && modulatedSignal.Length>0)
            {
                //如果输出变量定义不匹配，则重新定义
                if(message==null || message.Length!=modulatedSignal.Length)
                {
                    message=new double[modulatedSignal.Length];
                }
                for (int i = 0; i < modulatedSignal.Length; i++)
                {
                    message[i] = modulatedSignal[i].Magnitude;
                }
            }
            else
            {
                throw new ArgumentException("improper input mudulatedSignal, no data");
            }
        }
        /// <summary>
        /// PM解调静态方法
        /// </summary>
        /// <param name="modulatedSignal">输入基带波形</param>
        /// <param name="message">输出实数解调波形，与基带等长度</param>
        /// <param name="carrier">归一化载波频率，rad/sample</param>
        /// <param name="carrierCorrection">是否进行载波修正，如不修正载波频率=0</param>
        /// <param name="wrappedOutput">是否约束输出在0-2pi</param>
        /// <exception cref="ArgumentException">输入不合理抛异常</exception>
        public static void PMDemodulate(Complex[] modulatedSignal, ref double[] message, ref double carrier, bool carrierCorrection=true, bool wrappedOutput=false)
        {
            //如果输入合理，则进行解调
            if (modulatedSignal != null && modulatedSignal.Length > 0)
            {
                //如果输出变量定义不匹配，则重新定义
                if (message == null || message.Length != modulatedSignal.Length)
                {
                    message = new double[modulatedSignal.Length];
                }
                //get phase
                double _phase = 0;
                double _phaseOld = 0;
                double _dPhase = 0;
                for (int i = 0; i < modulatedSignal.Length; i++)
                {
                    _phase = modulatedSignal[i].Phase;
                    //unwrapping
                    if (i > 0)
                    {
                        _dPhase = _phase - _phaseOld;
                        if (Math.Abs(_dPhase) > Math.PI)
                        {
                            message[i] = _phase - 2 * Math.PI * Math.Round(_dPhase / 2 / Math.PI);
                        }
                        else
                        {
                            message[i] = _phase;
                        }
                    }
                    else
                    {
                        message[i] = _phase;
                    }
                    _phaseOld = message[i];
                }
                //如果需要，消除载波
                if (carrierCorrection)
                {
                    //detrend usefull for digital demodulations
                    double[] _dataY = new double[message.Length];
                    double _slop;
                    double _inte;
                    double[] _dataX = new double[message.Length];
                    for (int i = 0; i < message.Length; i++)
                    {
                        _dataX[i] = i;
                    }
                    EasyCurveFitting.LinearFit(_dataX, message, _dataX, ref _dataY, out _slop, out _inte);
                    carrier = _slop;
                    for (int i = 0; i < message.Length; i++)
                    {
                        message[i] -= _dataY[i];
                    }
                }
                else
                {
                    carrier = 0;
                }
                //如果需要，将输出约束在0-2Pi范围
                if(wrappedOutput)
                {
                    for (int i = 0; i < message.Length; i++)
                    {
                        message[i] = message[i] - 2 * Math.PI * Math.Floor(message[i] / 2 / Math.PI);
                    }
                }

            }
            else
            {
                throw new ArgumentException("improper input mudulatedSignal, no data");
            }
        }
        /// <summary>
        /// FM解调静态方法，采用前后2点相位差计算调频
        /// </summary>
        /// <param name="modulatedSignal">输入基带波形，至少3点</param>
        /// <param name="message">输出实数解调波形，与基带等长度，单位rad/sample</param>
        /// <exception cref="ArgumentException"></exception>
        public static void FMDemodulate(Complex[] modulatedSignal, ref double[] message)
        {
            //如果输入合理，则进行解调
            if (modulatedSignal != null && modulatedSignal.Length > 2)
            {
                //如果输出变量定义不匹配，则重新定义
                if (message == null || message.Length != modulatedSignal.Length)
                {
                    message = new double[modulatedSignal.Length];
                }
                //先解调相
                double[] _pmMessage = new double[modulatedSignal.Length];
                double carrier = 0;
                PMDemodulate(modulatedSignal, ref _pmMessage, ref carrier, false, false);
                int loopEnd = modulatedSignal.Length - 1;
                for (int i = 1; i < loopEnd; i++)
                {
                    message[i] = (_pmMessage[i + 1] - _pmMessage[i - 1]) * 0.5;
                }
                message[0] = message[1]; //padding 1st point
                message[modulatedSignal.Length - 1] = message[modulatedSignal.Length - 2]; //padding last point

            }
            else
            {
                throw new ArgumentException("improper input mudulatedSignal, no data");
            }
        }
    }
}
