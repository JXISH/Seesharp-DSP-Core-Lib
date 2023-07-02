using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using SeeSharpTools.JXI.SignalProcessing.GeneralSpectrum;
using System.Reflection;
using SeeSharpTools.JXI.Exception;
using SeeSharpTools.JXI.MKL;
using System.Drawing;
using SeeSharpTools.JXI.SignalProcessing.Window;

/*******************************************************
 * 时频联合分析
 * FrequencyBins：FFT点数，也是窗大小
 * WindowType：窗类型
 * ColorTable：强度图类型
 * 每次滑动窗的距离为FrequencyBins/4
 * *****************************************************/
namespace SeeSharpTools.JXI.SignalProcessing.JTFA
{
    /// <summary>
    /// <para>Joint Time-Frequency Analysis</para>
    /// <para>The step size of the sliding window is 1/4*FrequencyBins</para>
    /// <para>Chinese Simplified: 时频联合分析。</para>
    /// <para>窗类型默认汉明窗，窗滑动的步径为1/4*FrequencyBins</para>
    /// </summary>
    public class GeneralJTFATask
    {
        #region-------------------Construct------------------
        /// <summary>
        /// 构造函数
        /// </summary>
        public GeneralJTFATask()
        {
            FrequencyBins = 512;
            ColorTable = ColorTableType.Rainbow;
            WindowType = WindowType.Hamming ;
            SampleRate = 0;
            JTFAInfomation = new JTFAInformation();
        }

        /// <summary>
        /// 构造函数 
        /// </summary>
        static GeneralJTFATask()
        {

        }
        #endregion

        #region --------------Public Fileds-------------------
        /// <summary>
        /// <para>Frequency bins，default 512</para>
        /// <para>Chinese Simplified：FFT谱线数，默认512，推荐使用2的整数幂</para>
        /// </summary>
        public int FrequencyBins { get; set; }

        /// <summary>
        /// <para>The type of ColorTable</para>
        /// <para>Chinese Simplified：强度图颜色类型</para>
        /// </summary>
        public ColorTableType ColorTable { get; set; }

        /// <summary>
        /// <para>The type of FFTWindow</para>
        /// <para>Chinese Simplified：窗类型</para>
        /// </summary>
        public WindowType WindowType { get; set; }

        /// <summary>
        /// <para>Sample rate</para>
        /// <para>Chinese Simplified：采样率</para>
        /// </summary>
        public double SampleRate { get; set; }

        /// <summary>
        /// <para>The information of spectrum （read only）</para>
        /// <para>Chinese Simplified: 频谱输出信息(只读)。</para>
        /// </summary>
        public JTFAInformation JTFAInfomation { get; }
        #endregion

        #region--------------Private Fileds------------------
        /// <summary>
        /// definition of rainbow color table 定义色表
        /// </summary>
        #region Rainbow Color Table R[], G[], B[] Constant        
        private int[] colorTableR = new int[] { 0, 128, 126, 123, 121, 118, 116, 113, 111, 108, 106,
                103, 101, 98, 96, 93, 91, 88, 86, 83, 81, 78, 76, 73,
                71, 68, 66, 63, 60, 58, 55, 53, 50, 48, 45, 43, 40,
                38, 35, 33, 30, 28, 25, 23, 20, 18, 15, 13, 10, 8, 5,
                3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 3, 8, 13, 18, 23, 28, 33, 38, 43,
                48, 53, 58, 63, 68, 73, 78, 83, 88, 93, 98, 103, 108, 113,
                118, 123, 128, 133, 138, 143, 148, 153, 158, 163, 168, 173,
                178, 183, 188, 193, 198, 203, 208, 213, 218, 223, 228, 233,
                238, 243, 248, 253, 255, 255, 255, 255, 255, 255, 255, 255,
                255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
                255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
                255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
                255, 255, 255, 255, 255, 255, 255 };
        private int[] colorTableG = new int[] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,6,11,16,21,
                26,31,36,41,46,51,56,61,66,71,76,81,86,91,96,101,106,111,116,121,
                126,131,136,141,146,151,156,161,166,171,176,181,186,191,196,201,206,
                211,216,221,226,231,236,241,246,251,255,255,255,255,255,255,255,255,
                255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,
                255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,
                255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,
                255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,
                255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,
                255,255,255,255,255,255,255,255,255,251,246,241,236,231,226,221,216,
                211,206,201,196,191,186,181,176,171,166,161,156,151,146,141,136,131,
                126,121,116,111,106,101,96,91,86,81,76,71,66,61,56,51,46,41,36,31,26,
                21,16,11,6,0};
        private int[] colorTableB = new int[] {0,255,255,255,255,255,255,255,255,255,255,255,255,255,
                255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,
                255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,
                255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,
                255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,
                255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,
                255,255,255,255,252,247,242,237,232,227,222,217,212,207,202,197,192,
                187,182,177,172,167,162,157,152,147,142,137,132,127,122,117,112,107,
                102,97,92,87,82,77,72,67,62,57,52,47,42,37,32,27,22,17,12,7,2,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
        #endregion // Rainbow Color Table R[],G[],B[] Constant

        /// <summary>
        /// 实数JTFA，遗留数据
        /// </summary>
        double[] _subWaveform;

        /// <summary>
        /// 复数JTFA，遗留数据
        /// </summary>
        Complex[] _subWaveformComplex;
        #endregion

        #region --------------Public Methods------------------
        /// <summary>
        /// <para>Reset</para>
        /// <para>Chinese Simplified：清空保留的波形</para>
        /// </summary>
        public void Reset()
        {
            _subWaveform = null;
            _subWaveformComplex=null;
        }

        /// <summary>
        /// <para>Joint Time-Frequency Analysis</para>
        /// <para>Chinese Simplified：获取时频信号</para>
        /// </summary>
        /// <param name="waveform">
        /// <para>The input waveform</para>
        /// <para>Chinese Simplified：输入数据</para>
        /// </param>
        /// <param name="spectrum">
        /// <para>The output spectrum</para>
        /// <para> row represents frequency, column represents time</para>
        /// <para>Chinese Simplified：输出频谱</para>
        /// <para>行代表频率，列代表时间</para>
        /// </param>
        public void GetJTFA(double[] waveform, ref double[,] spectrum/*, out double df,ref double [] peaks,ref double[] peaksFreq*/)
        {

            if (waveform.Length < FrequencyBins)
            {
                throw new JXIParamException("输入波形长度必须大于等于FrequencyBins");
            }
            double[] data = new double[1];
            if (_subWaveform == null )//上次计算没有遗留数据
            {
                //保存FrequencyBins / 4*3到FrequencyBins之间的数据，留着下次运算
                int count = (waveform.Length - FrequencyBins) / (FrequencyBins / 4) + 1;
                _subWaveform = new double[waveform.Length - (FrequencyBins / 4 * count)];
                Array.Copy(waveform, waveform.Length - _subWaveform.Length, _subWaveform, 0, _subWaveform.Length);

                //判断这次计算的数据是多大
                int number = (waveform.Length - FrequencyBins) % (FrequencyBins / 4);
                if (number != 0)
                {
                    data = new double[waveform.Length - number];
                }
                else
                {             
                    data = new double[waveform.Length];
                }
                Array.Copy(waveform, data, data.Length);
            }
            else//上次计算有遗留数据
            {
                //上次计算遗留的数据与数据数据拼接起来
                double[] dataTem = new double[_subWaveform.Length + waveform.Length];
                Array.Copy(_subWaveform, 0, dataTem, 0, _subWaveform.Length);
                Array.Copy(waveform, 0, dataTem, _subWaveform.Length, waveform.Length);

                //保存FrequencyBins / 4*3到FrequencyBins之间的数据，留着下次运算
                int count = (dataTem.Length - FrequencyBins) / (FrequencyBins / 4) + 1;
                _subWaveform = new double[dataTem.Length - (FrequencyBins / 4 * count)];
                Array.Copy(dataTem, dataTem.Length - _subWaveform.Length, _subWaveform, 0, _subWaveform.Length);
                //判断这次计算的数据是多大
                int number = (dataTem.Length - FrequencyBins) % (FrequencyBins / 4);
                if (number != 0)
                {                    
                    data = new double[dataTem.Length - number];
                }
                else
                {
                    data = new double[dataTem.Length];                  
                }
                Array.Copy(dataTem, data, data.Length);
            }
            int dataLength = data.Length;
            int countOfFFT = (dataLength - FrequencyBins) /( FrequencyBins /4) + 1;//需要计算的FFT次数
            if (spectrum == null || spectrum.GetLength(0) != countOfFFT || spectrum.GetLength(1) != FrequencyBins / 2)
            {
                //throw new JXIParamException("spectrum的长度设置不对");
                spectrum = new double[countOfFFT, FrequencyBins / 2];
            }

            double[] subData = new double[FrequencyBins];//每次FFT计算的数据长度   
            //FFT运算  
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.SampleRate = SampleRate;
            _task.InputDataType = InputDataType.Real;
            _task.WindowType = WindowType;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = subData.Length / 2;
            _task.Unit.Type = SpectrumOutputUnit.V2;
            _task.Unit.IsPSD = false;
            for (int i = 0; i < countOfFFT; i++)
            {
                var a = FrequencyBins / 4;
                Array.Copy(data, a * i , subData, 0, FrequencyBins);
                double[] spec = new double[subData.Length / 2];
                _task.GetSpectrum(subData, ref spec);
                ReplaceArraySubset(spec, ref spectrum, i);//将spec放到spectrum的指定行
            }
            JTFAInfomation.df = _task.SpectralInfomation.FreqDelta;
            JTFAInfomation.f0 = _task.SpectralInfomation.FreqStart;
            JTFAInfomation.FFTSize = _task.SpectralInfomation.FFTSize;
            JTFAInfomation.dt = FrequencyBins / 4 / SampleRate;
        }

        /// <summary>
        /// <para>Joint Time-Frequency Analysis</para>
        /// <para>Chinese Simplified：获取时频信号</para>
        /// </summary>
        /// <param name="data">
        /// <para>The input waveform</para>
        /// <para>Chinese Simplified：输入数据</para>
        /// </param>
        /// <param name="spectrum">
        /// <para>The output spectrum</para>
        /// <para> row represents frequency, column represents time</para>
        /// <para>Chinese Simplified：输出频谱</para>
        /// <para>行代表频率，列代表时间</para>
        /// <param name="df">
        /// <para>The interval of frequency bins</para>
        /// <para>Chinese Simplified：谱线间隔</para>
        /// </param>
        public void GetJTFA(Complex[] waveform, ref double[,] spectrum/*, out double df,ref double [] peaks,ref double[] peaksFreq*/)
        {
            if (waveform.Length<FrequencyBins)
            {
                throw new JXIParamException("输入波形长度必须大于等于FrequencyBins");
            }
            Complex[] data = new Complex[1];
            if (_subWaveformComplex == null)//上次计算没有遗留数据
            {

                //保存FrequencyBins / 4*3到FrequencyBins之间的数据，留着下次运算
                int count = (waveform.Length - FrequencyBins) / (FrequencyBins / 4) + 1;
                _subWaveformComplex = new Complex[waveform.Length - (FrequencyBins / 4 * count)];
                Array.Copy(waveform, waveform.Length - _subWaveformComplex.Length, _subWaveformComplex, 0, _subWaveformComplex.Length);
                //判断这次计算的数据是多大
                int number = (waveform.Length - FrequencyBins) % (FrequencyBins / 4);
                if (number != 0)
                {
                    data = new Complex[waveform.Length - number];
                }
                else
                {
                    data = new Complex[waveform.Length];
                }
                Array.Copy(waveform, data, data.Length);
            }
            else//上次计算有遗留数据
            {
                //上次计算遗留的数据与数据数据拼接起来
                Complex[] dataTem = new Complex[_subWaveformComplex.Length + waveform.Length];
                Array.Copy(_subWaveformComplex, 0, dataTem, 0, _subWaveformComplex.Length);
                Array.Copy(waveform, 0, dataTem, _subWaveformComplex.Length, waveform.Length);

                //保存FrequencyBins / 4*3到FrequencyBins之间的数据，留着下次运算
                int count = (dataTem.Length - FrequencyBins) / (FrequencyBins / 4) + 1;
                _subWaveformComplex = new Complex[dataTem.Length - (FrequencyBins / 4 * count)];
                Array.Copy(dataTem, dataTem.Length - _subWaveformComplex.Length, _subWaveformComplex, 0, _subWaveformComplex.Length);

                //判断这次计算的数据是多大
                int number = (dataTem.Length - FrequencyBins) % (FrequencyBins / 4);
                if (number != 0)
                {
                    data = new Complex[dataTem.Length - number];
                }
                else
                {
                    data = new Complex[dataTem.Length];
                }
                Array.Copy(dataTem, data, data.Length);
            }
            int dataLength = data.Length;
            int countOfFFT = (dataLength - FrequencyBins) / (FrequencyBins /4) + 1;//需要计算的FFT次数
            if (spectrum == null || spectrum.GetLength(0) != countOfFFT || spectrum.GetLength(1) != FrequencyBins)
            {
                //throw new JXIParamException("spectrum的长度设置不对");
                spectrum = new double[countOfFFT, FrequencyBins];
            }
  

            Complex[] subData = new Complex[FrequencyBins];//每次FFT计算的数据长度     
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            _task.SampleRate = SampleRate;
            _task.InputDataType = InputDataType.Complex;
            _task.WindowType = WindowType;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = subData.Length;
            _task.Unit.Type = SpectrumOutputUnit.V2;
            _task.Unit.IsPSD = false;
            for (int i = 0; i < countOfFFT; i++)
            {
                Array.Copy(data, (FrequencyBins / 4* i), subData, 0, FrequencyBins);
                double[] spec = new double[subData.Length];
                _task.GetSpectrum(subData, ref spec);
                ReplaceArraySubset(spec, ref spectrum, i);//将spec放到spectrum的指定行
            }
            JTFAInfomation.df = _task.SpectralInfomation.FreqDelta;
            JTFAInfomation.f0 = _task.SpectralInfomation.FreqStart;
            JTFAInfomation.FFTSize = _task.SpectralInfomation.FFTSize;
            JTFAInfomation.dt = FrequencyBins / 4 / SampleRate;
        }

        /// <summary>
        /// <para>Get image</para>
        /// <para>Chinese Simplified：获取强度图</para>
        /// </summary>
        /// <param name="spectrum">
        /// <para>The input spectrum</para>
        /// <para>Chinese Simplified：时频信号</para>
        /// </param>
        /// <param name="BitMap">
        /// <para>The output Bitmap</para>
        /// <para>Chinese Simplified：强度图</para>
        /// </param>
        public void GetImage(double[,] spectrum,ref Bitmap BitMap )
        {
            ushort[,] imageData = new ushort[spectrum.GetLength(0), spectrum.GetLength(1)];
            SpectrumScale(spectrum, ref imageData);         
            colorDataToBitmap(imageData,ref BitMap);
        }

        /// <summary>
        /// <para>Get image</para>
        /// <para>Chinese Simplified：获取强度图</para>
        /// </summary>
        /// <param name="spectrum">
        /// <para>The input spectrum</para>
        /// <para>Chinese Simplified：时频信号</para>
        /// </param>
        /// <param name="min">
        /// <para>The min value of the amplitude spectrum indicated by image</para>
        /// <para>Chinese Simplified：强度图表示的幅度谱的最小值</para>
        /// </param>
        /// <param name="max">
        /// <para>The max value of the amplitude spectrum indicated by image</para>
        /// <para>Chinese Simplified：强度图表示的幅度谱的最大值</para>
        /// </param>
        /// <param name="BitMap">
        /// <para>The output Bitmap </para>
        /// <para>Chinese Simplified：强度图,大小等于spectrum</para>
        public void GetImage(double[,] spectrum, double min, double max, ref Bitmap BitMap)
        {
            ushort[,] imageData = new ushort[spectrum.GetLength(0), spectrum.GetLength(1)];
            SpectrumScale(spectrum, ref imageData, min, max);
            colorDataToBitmap(imageData, ref BitMap);
        }

        #endregion

        #region--------------Private Methods-----------------
        /// <summary>
        /// 将源数组放到目标数组的指定行中
        /// </summary>
        /// <param name="subData">源数组</param>
        /// <param name="data">目标数组</param>
        /// <param name="index">目标数组的列</param>
        private void  ReplaceArraySubset(double [] subData, ref double [,] data,int index)
        {
            for (int i = 0; i < subData.Length ; i++)
            {
                data[index,i] = subData[i];
            }
        }

        /// <summary>
        /// FFT之后的数值调整到0~255
        /// </summary>
        /// <param name="spectrumData"></param>
        /// <param name="colorData"></param>
        private void SpectrumScale(double[,] spectrumData, ref ushort[,] colorData)
        {
            if (spectrumData.Length != colorData.Length)
            {
                colorData = new ushort[spectrumData.GetLength(0), spectrumData.GetLength(1)];
            }
            //var colorData = new ushort[spectrumData.GetLength(0), spectrumData.GetLength(1)];
            var colorDataT = new ushort[spectrumData.GetLength(1), spectrumData.GetLength(0)];
            var colorDataTemp = new ushort[14, spectrumData.GetLength(0)];
            double max;
            double min;
            FindMaxAndMin(spectrumData, out max, out min);
            double unitValue = (max - min) / 256;
            for (int i = 0; i < spectrumData.GetLength(0); i++)
            {

                for (int j = 0; j < spectrumData.GetLength(1); j++)
                {
                    if (spectrumData[i, j] >= max - 0.5 * unitValue)
                    {
                        colorData[i, j] = 255;
                    }
                    else if (spectrumData[i, j] <= min)
                    {
                        colorData[i, j] = 0;
                    }
                    else
                    {
                        colorData[i, j] = (ushort)Math.Round((spectrumData[i, j] - min) / unitValue);
                    }
                }
            }
        }

        /// <summary>
        /// FFT之后的数值调整到0~255
        /// </summary>
        /// <param name="spectrumData"></param>
        /// <param name="colorData"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        private void SpectrumScale(double[,] spectrumData, ref ushort[,] colorData, double min,double max)
        {
            if (spectrumData.Length != colorData.Length)
            {
                colorData = new ushort[spectrumData.GetLength(0), spectrumData.GetLength(1)];
            }
            double unitValue = (max - min) / 256;
            for (int i = 0; i < spectrumData.GetLength(0); i++)
            {
                for (int j = 0; j < spectrumData.GetLength(1); j++)
                {
                    if (spectrumData[i, j] >= max - 0.5 * unitValue)
                    {
                        colorData[i, j] = 255;
                    }
                    else if (spectrumData[i, j] <= min)
                    {
                        colorData[i, j] = 0;
                    }
                    else
                    {
                        colorData[i, j] = (ushort)Math.Round((spectrumData[i, j] - min) / unitValue);
                    }
                }
            }
        }

        /// <summary>
        /// 将频谱值转变为Bitmap
        /// </summary>
        /// <param name="colorData"></param>
        /// <param name="BitMap"></param>
        private void colorDataToBitmap(ushort[,] colorData, ref Bitmap BitMap)
        {
            if (BitMap.Width!= colorData.GetLength(1) || BitMap.Height != colorData.GetLength(0))
            {
                BitMap = new Bitmap(colorData.GetLength(1), colorData.GetLength(0));
            }
            double widthColorData = colorData.GetLength(1);//列
            double hightColorData = colorData.GetLength(0);//行
            if (ColorTable == ColorTableType.BlackWrite)//黑白色
            {
                for (int i = 0; i < hightColorData; i++)//行
                {
                    for (int j = 0; j < widthColorData; j++)//列
                    {
                        var RIndex = colorData[i, j];
                        var GIndex = colorData[i, j];
                        var BIndex = colorData[i, j];
                        var rgbColor = Color.FromArgb(RIndex,
                                                      GIndex,
                                                      BIndex);
                        BitMap.SetPixel(j, i, rgbColor);
                    }
                }
            }
            else if (ColorTable == ColorTableType.BlueGreenRed)
            {
                int blueCenter = 0;
                int blueRange = 90;
                int greenCenter = 127;
                int greenRange = 90;
                int redCenter = 255;
                int redRange = 90;
                for (int i = 0; i < hightColorData; i++)//行
                {
                    for (int j = 0; j < widthColorData; j++)//列
                    {
                        var RIndex = (int)Math.Round(255 * (1 - Math.Min((double)redRange, Math.Abs(colorData[i, j] - redCenter)) / redRange));
                        var GIndex = (int)Math.Round(255 * (1 - Math.Min((double)greenRange, Math.Abs(colorData[i, j] - greenCenter)) / greenRange));
                        var BIndex = (int)Math.Round(255 * (1 - Math.Min((double)blueRange, Math.Abs(colorData[i, j] - blueCenter)) / blueRange));
                        var rgbColor = Color.FromArgb(RIndex,
                                                      GIndex,
                                                      BIndex);
                        BitMap.SetPixel(j, i, rgbColor);
                    }
                }
            }
            else if (ColorTable == ColorTableType.Rainbow)
            {
                int[,] controlColor = { { 0, 0, 0 }, { 128, 0, 255 }, { 0, 0, 255 }, { 0, 255, 255 }, { 0, 255, 0 }, { 255, 255, 0 }, { 255, 0, 0 } };
                int[] controlIndex = { 0, 1, 52, 102, 153, 205, 254 };
                int index = 0;
                double a1, a2;
                int[] rgb = new int[3];
                for (int i = 0; i < hightColorData; i++)//行
                {
                    for (int j = 0; j < widthColorData; j++)//列
                    {

                        if(colorData[i, j] == 0)
                        {
                            index = 0;
                            var rgbColor = Color.FromArgb(0, 0,0);
                            BitMap.SetPixel(j, i, rgbColor);
                        }
                        else if (colorData[i, j] == 255)
                        {
                            index = 0;
                            var rgbColor = Color.FromArgb(255, 255, 255);
                            BitMap.SetPixel(j, i, rgbColor);
                        }
                        else
                        {
                            for (int k = 1; k < controlIndex.Length; k++)
                            {
                                if(colorData[i, j]> controlIndex[k-1] && colorData[i, j] <= controlIndex[k])
                                {
                                    index = k;
                                }
                            }
                            a2 = (colorData[i, j] - controlIndex[index - 1]) / ((double)controlIndex[index] - controlIndex[index - 1]);
                            a1 = (controlIndex[index] - colorData[i, j]) / ((double)controlIndex[index] - controlIndex[index - 1]);
                            for (int m = 0; m < 3; m++)
                            {
                                rgb[m] = (int)Math.Round(controlColor[index - 1, m] * a1 + controlColor[index, m] * a2);
                            }
                            var rgbColor = Color.FromArgb(rgb[0], rgb[1], rgb[2]);
                            BitMap.SetPixel(j, i, rgbColor);
                        }                       
                    }
                }
            }
            else if (ColorTable == ColorTableType.BalancedRainbow)
            {
                int[,] controlColor = new int[,] { { 0, 0, 0 }, { 90, 0, 180 }, { 0, 0, 255 }, { 0, 128, 128 }, { 0, 200, 0 }, { 200, 200, 0 }, { 255, 0, 0 } };
                int[] controlIndex = new int[] { 0, 1, 52, 102, 153, 205, 254 };
                double a1, a2;
                int[] rgb = new int[3];
                int index = 0;
                for (int i = 0; i < hightColorData; i++)//行
                {
                    for (int j = 0; j < widthColorData; j++)//列
                    {

                        if (colorData[i, j] == 0)
                        {
                            index = 0;
                            var rgbColor = Color.FromArgb(0, 0, 0);
                            BitMap.SetPixel(j, i, rgbColor);
                        }
                        else if (colorData[i, j] == 255)
                        {
                            index = 0;
                            var rgbColor = Color.FromArgb(255, 255, 255);
                            BitMap.SetPixel(j, i, rgbColor);
                        }
                        else
                        {
                            for (int k = 1; k < controlIndex.Length; k++)
                            {
                                if (colorData[i, j] > controlIndex[k - 1] && colorData[i, j] <= controlIndex[k])
                                {
                                    index = k;
                                }
                            }
                            a2 = (colorData[i, j] - controlIndex[index - 1]) / ((double)controlIndex[index] - controlIndex[index - 1]);
                            a1 = (controlIndex[index] - colorData[i, j]) / ((double)controlIndex[index] - controlIndex[index - 1]);
                            for (int m = 0; m < 3; m++)
                            {
                                rgb[m] = (int)Math.Round(controlColor[index - 1, m] * a1 + controlColor[index, m] * a2);
                            }
                            var rgbColor = Color.FromArgb(rgb[0], rgb[1], rgb[2]);
                            BitMap.SetPixel(j, i, rgbColor);
                        }

                    }
                }
            }
            else if (ColorTable == ColorTableType.Fire)
            {
                int[,] controlColor= new int[,] { { 0, 0, 0 }, { 90, 0, 0 }, { 200, 40, 0 }, { 255, 128, 0 }, { 255, 255, 0 }, { 255, 255, 255 } };
                int[] controlIndex = new int[] { 0, 50, 150, 200, 230, 255 };
                double a1, a2;
                int[] rgb = new int[3];
                int index = 0;
                for (int i = 0; i < hightColorData; i++)//行
                {
                    for (int j = 0; j < widthColorData; j++)//列
                    {

                        if (colorData[i, j] == 0)
                        {
                            index = 0;
                            var rgbColor = Color.FromArgb(0, 0, 0);
                            BitMap.SetPixel(j, i, rgbColor);
                        }
                        else
                        {
                            for (int k = 1; k < controlIndex.Length; k++)
                            {
                                if (colorData[i, j] > controlIndex[k - 1] && colorData[i, j] <= controlIndex[k])
                                {
                                    index = k;
                                }
                            }
                            a2 = (colorData[i, j] - controlIndex[index - 1]) / ((double)controlIndex[index] - controlIndex[index - 1]);
                            a1 = (controlIndex[index] - colorData[i, j]) / ((double)controlIndex[index] - controlIndex[index - 1]);
                            for (int m = 0; m < 3; m++)
                            {
                                rgb[m] = (int)Math.Round(controlColor[index - 1, m] * a1 + controlColor[index, m] * a2);
                            }
                            var rgbColor = Color.FromArgb(rgb[0], rgb[1], rgb[2]);
                            BitMap.SetPixel(j, i, rgbColor);
                        }
                    }
                }
            }
            else if (ColorTable == ColorTableType.BlueSpirit)
            {
                int[,] controlColor = new int[,] { { 0, 0, 0 }, { 64, 0, 128 }, { 0, 0, 255 }, { 255, 255, 255 } };
                int[] controlIndex = new int[] { 0, 100, 200, 255 };
                double a1, a2;
                int[] rgb = new int[3];
                int index = 0;
                for (int i = 0; i < hightColorData; i++)//行
                {
                    for (int j = 0; j < widthColorData; j++)//列
                    {

                        if (colorData[i, j] == 0)
                        {
                            index = 0;
                            var rgbColor = Color.FromArgb(0, 0, 0);
                            BitMap.SetPixel(j, i, rgbColor);
                        }
                        else
                        {
                            for (int k = 1; k < controlIndex.Length; k++)
                            {
                                if (colorData[i, j] > controlIndex[k - 1] && colorData[i, j] <= controlIndex[k])
                                {
                                    index = k;
                                }
                            }
                            a2 = (colorData[i, j] - controlIndex[index - 1]) / ((double)controlIndex[index] - controlIndex[index - 1]);
                            a1 = (controlIndex[index] - colorData[i, j]) / ((double)controlIndex[index] - controlIndex[index - 1]);
                            for (int m = 0; m < 3; m++)
                            {
                                rgb[m] = (int)Math.Round(controlColor[index - 1, m] * a1 + controlColor[index, m] * a2);
                            }
                            var rgbColor = Color.FromArgb(rgb[0], rgb[1], rgb[2]);
                            BitMap.SetPixel(j, i, rgbColor);
                        }
                    }
                }
            }
            else if (ColorTable == ColorTableType.BlueFairy)
            {
                int[,] controlColor = new int[,] { { 0, 0, 0 }, { 38, 242, 241 }, { 168, 93, 222 }, { 240, 255, 250 } };
                int[] controlIndex = new int[] { 0, 120, 200, 255 };
                double a1, a2;
                int index = 0;
                int[] rgb = new int[3];
                for (int i = 0; i < hightColorData; i++)//行
                {
                    for (int j = 0; j < widthColorData; j++)//列
                    {

                        if (colorData[i, j] == 0)
                        {
                            index = 0;
                            var rgbColor = Color.FromArgb(0, 0, 0);
                            BitMap.SetPixel(j, i, rgbColor);
                        }
                        else
                        {
                            for (int k = 1; k < controlIndex.Length; k++)
                            {
                                if (colorData[i, j] > controlIndex[k - 1] && colorData[i, j] <= controlIndex[k])
                                {
                                    index = k;
                                }
                            }
                            a2 = (colorData[i, j] - controlIndex[index - 1]) / ((double)controlIndex[index] - controlIndex[index - 1]);
                            a1 = (controlIndex[index] - colorData[i, j]) / ((double)controlIndex[index] - controlIndex[index - 1]);
                            for (int m = 0; m < 3; m++)
                            {
                                rgb[m] = (int)Math.Round(controlColor[index - 1, m] * a1 + controlColor[index, m] * a2);
                            }
                            var rgbColor = Color.FromArgb(rgb[0], rgb[1], rgb[2]);
                            BitMap.SetPixel(j, i, rgbColor);
                        }

                    }
                }
            }
            else if (ColorTable == ColorTableType.BlueOrange_30Hue)
            {
                int[,] controlColor = new int[,] { { 0, 127, 255 }, { 255, 127, 0 } };
                int[] controlIndex = new int[] { 0, 255 };
                double a1, a2;
                int[] rgb = new int[3];
                int index = 0;
                for (int i = 0; i < hightColorData; i++)//行
                {
                    for (int j = 0; j < widthColorData; j++)//列
                    {

                        if (colorData[i, j] == 0)
                        {
                            index = 0;
                            var rgbColor = Color.FromArgb(0, 0, 0);
                            BitMap.SetPixel(j, i, rgbColor);
                        }
                        else
                        {
                            for (int k = 1; k < controlIndex.Length; k++)
                            {
                                if (colorData[i, j] > controlIndex[k - 1] && colorData[i, j] <= controlIndex[k])
                                {
                                    index = k;
                                }
                            }
                            a2 = (colorData[i, j] - controlIndex[index - 1]) / ((double)controlIndex[index] - controlIndex[index - 1]);
                            a1 = (controlIndex[index] - colorData[i, j]) / ((double)controlIndex[index] - controlIndex[index - 1]);
                            for (int m = 0; m < 3; m++)
                            {
                                rgb[m] = (int)Math.Round(controlColor[index - 1, m] * a1 + controlColor[index, m] * a2);
                            }
                            var rgbColor = Color.FromArgb(rgb[0], rgb[1], rgb[2]);
                            BitMap.SetPixel(j, i, rgbColor);
                        }

                    }
                }
            }
            else
            {
                throw new JXIParamException("not support this ColorTableType");
            }
        }
      
        /// <summary>
        /// 查找最大值和最小值
        /// </summary>
        /// <param name="spectrumData"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        private void FindMaxAndMin(double[,] spectrumData,out double max,out double min)
        {
            double tempMax = double.MinValue;
            double tempMin = double .MaxValue ;
            for (int i = 0; i < spectrumData.GetLength(0); i++)
            {
                for (int j = 0; j < spectrumData.GetLength(1); j++)
                {
                    if(spectrumData[i,j]> tempMax)
                    {
                        tempMax = spectrumData[i, j];
                    }
                    if (spectrumData[i, j] < tempMin)
                    {
                        tempMin= spectrumData[i, j];
                    }
                }
            }
            max = tempMax;
            min = tempMin;
        }
        #endregion

        #region ---------------------枚举---------------------
        /// <summary>
        /// <para>The type of ColorTable，support Rainbow only</para>
        /// <para>Chinese Simplified：ColorTable的类型 ，暂不支持柔和的彩虹色</para>
        /// </summary>
        public enum ColorTableType
        {
            /// <summary>
            /// <para>BlackWrite</para>
            /// <para>Chinese Simplified：黑白色</para>
            /// </summary>
            BlackWrite,

            /// <summary>
            /// <para>BlueGreenRed </para>
            /// <para>Chinese Simplified：BlueGreenRed</para>
            /// </summary>
            BlueGreenRed,

            /// <summary>
            /// <para>Rainbow </para>
            /// <para>Chinese Simplified：彩虹色</para>
            /// </summary>
            Rainbow,

            /// <summary>
            /// <para> BalancedRainbow</para>
            /// <para>Chinese Simplified：BalancedRainbow</para>
            /// </summary>
            BalancedRainbow,

            /// <summary>
            /// <para>Fire </para>
            /// <para>Chinese Simplified：Fire</para>
            /// </summary>
            Fire,

            /// <summary>
            /// <para>BlueSpirit </para>
            /// <para>Chinese Simplified：BlueSpirit</para>
            /// </summary>
            BlueSpirit,

            /// <summary>
            /// <para> BlueFairy</para>
            /// <para>Chinese Simplified：BlueFairy</para>
            /// </summary>
            BlueFairy,

            /// <summary>
            /// <para> BlueOrange_30Hue</para>
            /// <para>Chinese Simplified：BlueOrange_30Hue</para>
            /// </summary>
            BlueOrange_30Hue
        }

        /// <summary>
        /// <para>Chinese Simplified：频谱计算相关信息</para>
        /// </summary>
        public class JTFAInformation
        {
            /// <summary>
            /// 默认构造函数
            /// </summary>
            public JTFAInformation()
            {
                f0 = 0;
                df = 0;
                FFTSize = 0;
                dt = 0;
            }

            /// <summary>
            /// 频谱起始频率f0
            /// </summary>
            public double f0 { get; internal set; }

            /// <summary>
            /// 频谱谱线的频率间隔df
            /// </summary>
            public double df { get; internal set; }

            /// <summary>
            /// 每次计算FFT时的长度，即每次应输入的时域波形长度。
            /// </summary>
            public int FFTSize { get; internal set; }

            /// <summary>
            /// JTFA时间间隔
            /// </summary>
            public double dt { get; internal set; }
        }
        #endregion
    }
}
