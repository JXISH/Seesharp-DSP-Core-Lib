using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using SeeSharpTools.JXI.MKL;
using SeeSharpTools.JXI.Exception;

namespace SeeSharpTools.JXI.SignalProcessing.GeneralSpectrum
{
    internal class SpectrumAverageAux
    {
        public SpectrumAverageAux()
        {
            _averageCount = 0;
            _averagingMode = SpectrumAverageMode.NoAveraging;
            _averagingSize = 10;
            _spectrumList = new List<double[]>();
            _vectorList = new List<Complex[]>();
            _weightingType = SpectrumWeightingType.LinearMoving;
            _linearAveragingMode = LinearAveragingMode.MovingAveraging;
            _dataUnit = SpectrumAveragingDataUnit.Linear;
        }

        #region ---------------私有字段定义-----------------

        /// <summary>
        /// 用于缓存平均后的频谱
        /// </summary>
        private double[] _averagedSpectrum;

        /// <summary>
        /// 已平均的次数
        /// </summary>
        private int _averageCount;

        /// <summary>
        /// 频谱链表，用于存储平均的多个频谱
        /// </summary>
        private List<double[]> _spectrumList;

        private List<Complex[]> _vectorList;
        
        /// <summary>
        /// 平均模式
        /// </summary>
        private SpectrumAverageMode _averagingMode;

        /// <summary>
        /// 平均的最大次数
        /// </summary>
        private double _averagingSize;

        /// <summary>
        /// 平均的加权方式
        /// </summary>
        private SpectrumWeightingType _weightingType;

        /// <summary>
        /// 线性平均的方式
        /// </summary>
        private LinearAveragingMode _linearAveragingMode;

        /// <summary>
        /// 数据的单位
        /// </summary>
        private SpectrumAveragingDataUnit _dataUnit;

        #endregion

        #region ---------------公共属性定义-----------------

        /// <summary>
        /// 频谱平均模式，无平均/矢量/RMS/PeakHold
        /// </summary>
        public SpectrumAverageMode Mode
        {
            get
            {
                return _averagingMode;
            }
            set
            {
                var toReset = _averagingMode != value;
                _averagingMode = value;
                if (toReset)
                {
                    Reset();
                }
            }
        }

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
                if (_averagingSize <= 0)
                {
                    throw new JXIParamException("Size must greater than 0!");
                }
                var toReset = _averagingSize != value;
                _averagingSize = value;
                if (!toReset) return;
                //清理多余的
                while (_spectrumList.Count > _averagingSize)
                {
                    _spectrumList.RemoveAt(0);
                }
            }
        }

        /// <summary>
        /// 加权类型
        /// </summary>
        public SpectrumWeightingType WeightingType
        {
            get { return _weightingType; }
            set
            {
                var toReset = _weightingType != value;
                _weightingType = value;
                if (toReset)
                {
                    Reset();
                }
            }
        }

        /// <summary>
        /// the spectral data is linear or logarithm.
        /// </summary>
        public SpectrumAveragingDataUnit DataUnit
        {
            get { return _dataUnit; }
            set
            {
                var toReset = _dataUnit != value;
                _dataUnit = value;
                if (toReset)
                {
                    Reset();
                }
                _dataUnit = value;
                Reset();
            }
        }
        
        #endregion

        /// <summary>
        /// Reset the spectrum averaging, set the average count to zero.
        /// </summary>
        public void Reset()
        {
            if (_averagedSpectrum != null)
            {
                for (int i = 0; i < _averagedSpectrum.Length; i++)
                {
                    _averagedSpectrum[i] = 0;
                }
            }
            _averageCount = 0; //重置平均次数
            _spectrumList?.Clear(); //清空用于平均的多个频谱
        }

        /// <summary>
        /// 频谱平均
        /// </summary>
        /// <param name="averagedSpectrum">平均后的频谱</param>
        /// <param name="averageCount">已平均的次数</param>
        /// <param name="newSpectrum">新输入的频谱</param>
        public void AverageSpectrum(double[] newSpectrum, out int averageCount)
        {
            AverageSpectrum(newSpectrum);
            averageCount = this._averageCount;
        }
        /// <summary>
        /// 频谱平均
        /// </summary>
        /// <param name="newSpectrum"></param>
        public void AverageSpectrum(double[] newSpectrum)
        {
            if (_averagingMode == SpectrumAverageMode.VectorAveraging)
            {
                throw new JXIParamException("VectorAveraging cannot call this method.");
            }
            bool retFlag = false;
            //add new spectrum to spectrum list. RMS平均才需要用链表保存每一帧频谱
            if (_averagedSpectrum == null || _averagedSpectrum.Length != newSpectrum.Length)
            {
                Reset();
                _averagedSpectrum = new double[newSpectrum.Length];
                CBLASNative.cblas_dcopy(newSpectrum.Length, newSpectrum, 1, _averagedSpectrum, 1);
                _averageCount++;
                retFlag = true;
            }
            if (Mode == SpectrumAverageMode.RMSAveraging)
            {
                var newSpectrumToAdd = new double[newSpectrum.Length];
                _spectrumList.Add(newSpectrumToAdd);
                CBLASNative.cblas_dcopy(newSpectrum.Length, newSpectrum, 1, newSpectrumToAdd, 1);
            }
            if (retFlag)
            {
                return;
            }
            if (Mode != SpectrumAverageMode.NoAveraging && _averageCount == 0)
            {
                CBLASNative.cblas_dcopy(newSpectrum.Length, newSpectrum, 1, _averagedSpectrum, 1);
                _averageCount++;
                return;
            }

            if (_dataUnit == SpectrumAveragingDataUnit.dB)
            {
                //what if the spectrum is in the unit of dB
                throw new NotImplementedException();
            }
            //average the spectrum in the unit of linear according to the averaging mode.
            switch (_averagingMode)
            {
                case SpectrumAverageMode.NoAveraging:
                    {
                        //Buffer.BlockCopy(newSpectrum, 0, _averagedSpectrum, 0, newSpectrum.Length * sizeof(double));
                        break;
                    }
                case SpectrumAverageMode.RMSAveraging:
                    {
                        if (_weightingType == SpectrumWeightingType.LinearMoving)
                        {
                            if (_averageCount < _averagingSize)
                            {
                                //(averagedSpectrum * _averageCount + newSpectrum) / (_averageCount + 1);
                                CBLASNative.cblas_dscal(_averagedSpectrum.Length, _averageCount, _averagedSpectrum, 1);
                                VMLNative.vdAdd(_averagedSpectrum.Length, _averagedSpectrum, newSpectrum,
                                    _averagedSpectrum);
                                CBLASNative.cblas_dscal(_averagedSpectrum.Length, 1.0/(_averageCount + 1),
                                    _averagedSpectrum, 1);
                                _averageCount++;
                            }
                            else
                            {
                                //(averagedSpectrum * _averageCount - _spectrumList[0] + newSpectrum) / (_averageCount + 1);
                                CBLASNative.cblas_dscal(_averagedSpectrum.Length, _averageCount, _averagedSpectrum, 1);
                                VMLNative.vdSub(_averagedSpectrum.Length, _averagedSpectrum, _spectrumList[0], _averagedSpectrum);
                                VMLNative.vdAdd(_averagedSpectrum.Length, _averagedSpectrum, newSpectrum, _averagedSpectrum);
                                CBLASNative.cblas_dscal(_averagedSpectrum.Length, 1.0 / _averageCount,
                                    _averagedSpectrum, 1);
                                _spectrumList.RemoveAt(0); //Remove First
                            }
                        }
                        else if (_weightingType == SpectrumWeightingType.LinearContinuous)
                        {
                            //(averagedSpectrum * _averageCount + newSpectrum) / (_averageCount + 1);
                            CBLASNative.cblas_dscal(_averagedSpectrum.Length, _averageCount, _averagedSpectrum, 1);
                            VMLNative.vdAdd(_averagedSpectrum.Length, _averagedSpectrum, newSpectrum,
                                _averagedSpectrum);
                            CBLASNative.cblas_dscal(_averagedSpectrum.Length, 1.0 / (_averageCount + 1),
                                _averagedSpectrum, 1);

                        }
                        else  //weightingType == SpectrumWeightingType.Exponential.
                        {
                            throw new NotImplementedException();
                            //double a1, a2 = 0.5;
                            //if (averagesSoFar <= averagingSize)
                            //{
                            //    a1 = (averagesSoFar - 1) / averagesSoFar;
                            //    a2 = 1 / averagesSoFar;
                            //}
                            //else
                            //{
                            //    a1 = (averagingSize - 1) / averagingSize;
                            //    a2 = 1 / averagingSize;
                            //}
                            //for (int i = 0; i < newSpectrum.Length; i++)
                            //{
                            //    averagedSpectrum[i] = a1 * averagedSpectrum[i] + a2 * newSpectrum[i];
                            //}
                            //averagesSoFar++;
                        }
                        break;
                    }
                case SpectrumAverageMode.PeakHold:
                    {
                        for (int i = 0; i < newSpectrum.Length; i++)
                        {
                            if (newSpectrum[i] > _averagedSpectrum[i])
                            {
                                _averagedSpectrum[i] = newSpectrum[i];
                            }
                        }
                        _averageCount++;
                        break;
                    }
            }
            Buffer.BlockCopy(_averagedSpectrum, 0, newSpectrum, 0, newSpectrum.Length * sizeof(double));
        }

        private void VectorAveraging(ref Complex[] averagedSpectrum, Complex[] newSpectrum)
        {
            if (_averagingMode != SpectrumAverageMode.VectorAveraging)
            {
                throw new JXIParamException("Only VectorAveraging Mode could call this method.");
            }
            if (averagedSpectrum.Length != newSpectrum.Length)
            {
                averagedSpectrum = new Complex[newSpectrum.Length];
                newSpectrum.CopyTo(averagedSpectrum, 0);
                Reset();
                return;
            }
            //add new spectrum to spectrum list. RMS平均才需要用链表保存每一帧频谱
            if (Mode == SpectrumAverageMode.RMSAveraging)
            {
                var newSpectrumToAdd = new Complex[newSpectrum.Length];
                _vectorList.Add(newSpectrumToAdd);
                Buffer.BlockCopy(newSpectrum, 0, newSpectrumToAdd, 0, 
                                 newSpectrum.Length * Marshal.SizeOf(typeof(Complex)));
            }
            if (_averageCount == 0)
            {
                Buffer.BlockCopy(newSpectrum, 0, averagedSpectrum, 0, 
                                 newSpectrum.Length * Marshal.SizeOf(typeof(Complex)));
                _averageCount++;
                return;
            }

            if (_dataUnit == SpectrumAveragingDataUnit.dB)
            {
                //what if the spectrum is in the unit of dB
            }
            //average the spectrum in the unit of linear according to the averaging mode.
            switch (_averagingMode)
            {
                case SpectrumAverageMode.NoAveraging:
                    {
                        Buffer.BlockCopy(newSpectrum, 0, averagedSpectrum, 0, 
                                         newSpectrum.Length * Marshal.SizeOf(typeof(Complex)));
                        break;
                    }
                case SpectrumAverageMode.RMSAveraging:
                {
                    var averagedSpectrumGcHandle = GCHandle.Alloc(averagedSpectrum, GCHandleType.Pinned);
                    var averagedSpectrumPtr = averagedSpectrumGcHandle.AddrOfPinnedObject();
                    var newSpectrumGcHandle = GCHandle.Alloc(newSpectrum, GCHandleType.Pinned);
                    var newSpectrumPtr = newSpectrumGcHandle.AddrOfPinnedObject();
                    if (_weightingType == SpectrumWeightingType.LinearMoving)
                    {

                        if (_averageCount < _averagingSize)
                        {
                            //(averagedSpectrum * _averageCount + newSpectrum) / (_averageCount + 1);
                            CBLASNative.cblas_dscal(averagedSpectrum.Length, _averageCount*2, averagedSpectrumPtr, 1);
                            VMLNative.vdAdd(averagedSpectrum.Length*2, averagedSpectrumPtr, newSpectrumPtr,
                                averagedSpectrumPtr);
                            CBLASNative.cblas_dscal(averagedSpectrum.Length*2, 1.0 / (_averageCount + 1),
                                averagedSpectrumPtr, 1);
                        }
                        else
                        {
                            //(averagedSpectrum * _averageCount - _spectrumList[0] + newSpectrum) / (_averageCount + 1);
                            var vectorList0GcHandle = GCHandle.Alloc(_vectorList[0], GCHandleType.Pinned);
                            var vectorList0Ptr = vectorList0GcHandle.AddrOfPinnedObject();
                            CBLASNative.cblas_dscal(averagedSpectrum.Length*2, _averageCount, averagedSpectrumPtr, 1);
                            VMLNative.vdSub(averagedSpectrum.Length*2, averagedSpectrumPtr, vectorList0Ptr,
                                averagedSpectrumPtr);
                            VMLNative.vdAdd(averagedSpectrum.Length*2, averagedSpectrumPtr, newSpectrumPtr,
                                averagedSpectrumPtr);
                            CBLASNative.cblas_dscal(averagedSpectrum.Length*2, 1.0 / (_averageCount + 1), averagedSpectrumPtr, 1);
                            _vectorList.RemoveAt(0); //Remove First
                            vectorList0GcHandle.Free();
                        }
                        _averageCount++;
                    }
                    else if (_weightingType == SpectrumWeightingType.LinearContinuous)
                    {
                        //(averagedSpectrum * _averageCount + newSpectrum) / (_averageCount + 1);
                        CBLASNative.cblas_dscal(averagedSpectrum.Length * 2, _averageCount, averagedSpectrumPtr, 1);
                        VMLNative.vdAdd(averagedSpectrum.Length * 2, averagedSpectrumPtr, newSpectrumPtr, averagedSpectrumPtr);
                        CBLASNative.cblas_dscal(averagedSpectrum.Length * 2, 1.0 / (_averageCount + 1), averagedSpectrumPtr, 1);

                    }
                    else //weightingType == SpectrumWeightingType.Exponential.
                    {
                        throw new NotImplementedException();
                        //double a1, a2 = 0.5;
                        //if (averagesSoFar <= averagingSize)
                        //{
                        //    a1 = (averagesSoFar - 1) / averagesSoFar;
                        //    a2 = 1 / averagesSoFar;
                        //}
                        //else
                        //{
                        //    a1 = (averagingSize - 1) / averagingSize;
                        //    a2 = 1 / averagingSize;
                        //}
                        //for (int i = 0; i < newSpectrum.Length; i++)
                        //{
                        //    averagedSpectrum[i] = a1 * averagedSpectrum[i] + a2 * newSpectrum[i];
                        //}
                        //averagesSoFar++;
                    }
                    averagedSpectrumGcHandle.Free();
                    newSpectrumGcHandle.Free();
                    break;
                }
                case SpectrumAverageMode.PeakHold:
                {
                    var averagedAmp = new double[averagedSpectrum.Length];
                    var newAmp = new double[averagedSpectrum.Length];
                    VMLNative.vzAbs(averagedSpectrum.Length, averagedSpectrum, averagedAmp);
                    VMLNative.vzAbs(newSpectrum.Length, newSpectrum, newAmp);
                    for (int i = 0; i < newSpectrum.Length; i++)
                    {
                        if (newAmp[i] > averagedAmp[i])
                        {
                            averagedSpectrum[i] = newSpectrum[i];
                        }
                    }
                    _averageCount++;
                    break;
                }
            }
        }

    }

    #region Relevant Enumeration

    public enum LinearAveragingMode
    {
        OneShot,
        AutoRestartOneShot,
        MovingAveraging,
        Continuous
    }

    #endregion
}
