using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#region ------------------------------------Description----------------------------------------
/*************************************************************************************************
 * This class provides the methods to get the amplitude, period and _phase of waveform(_status)
 * 
 * Data 
 * includes waveform[] which is the main square wave to be analysised,
 * waveformRef[] is _demoniator reference for _phase analysis only
 * 
 * Logic
 * Init method set all results to be Nan
 * When you call the Get... methods, it calls analysis when the required result is NaN
 * You MUST SetWaveform be Get results, otherwise the results are not predictable.  
 * Note, this algorithm works for squarewave only, not sine wave
 * Rev 0.1 JXSH designed structure and amplitude and _phase analysis 
 * Rev 0.5 JXXY added pulse-width analysis in the period analysis, and reviewed by JXSH 2017-Mar 16
 ***************************************************************************************************/
#endregion

namespace SeeSharpTools.JXI.SignalProcessing.Measurement
{
    /// <summary>
    /// <para>SquarewaveMeasurements Class</para>
    /// <para>Chinese Simplified：方波测量类</para>
    /// </summary>
    public static class SquarewaveMeasurements
    {
        #region----------------------------Construct-------------------------------------
        /// <summary>
        /// 构造函数 
        /// </summary>
        static SquarewaveMeasurements()
        {
 
        }
        #endregion

        #region ---------------------------Public Methods----------------------------------------
        /// <summary>
        /// <para>Get Level Histogram</para>
        /// <para>Chinese Simplified：获取方波幅值直方图</para>
        /// </summary>
        /// <param name="inputWaveform">
        /// <para>The input waveform</para>
        /// <para>Chinese Simplified：输入波形</para>
        /// </param>
        /// <param name="histogramX">
        /// <para>The X coordinate of the histogram</para>
        /// <para>Chinese Simplified：直方图X坐标</para>
        /// </param>
        /// <param name="histogramY">
        /// <para>The Y coordinate of the histogram</para>
        /// <para>Chinese Simplified：直方图Y坐标</para>
        /// </param>
        /// <param name="histogramConfig">
        /// <para>Config of histogram </para>
        /// <para>Chinese Simplified：直方图配置</para>
        /// </param>
        /// <returns>
        /// <para>return true if this method is called, else false</para>
        /// <para>Chinese Simplified：如果调用这个方法之前已经设置过波形返回值为true，否则为false</para>
        /// </returns>
        public static bool GetLevelHistogram(double[] inputWaveform, ref double[] histogramX, ref double[] histogramY, Histogram histogramConfig)
        {
            if (inputWaveform == null)
            {
                return false;
            }
            int intervals = 50;
            double intervalTopScale = 0.99;
            int intervalIndex = 0;
            double minValue = double.IsNaN(histogramConfig.LowerLimit) ? inputWaveform.Min() : histogramConfig.LowerLimit;
            double maxValue = double.IsNaN(histogramConfig.UpperLimit) ? inputWaveform.Max() : histogramConfig.UpperLimit;
            double waveformPeakPeak = maxValue - minValue;
            int groupNums = double.IsNaN(histogramConfig.Interval) ? intervals : (int)(waveformPeakPeak / histogramConfig.Interval);
            int[] intervalCounter = new int[groupNums];
            double[] intervalIntegrator = new double[groupNums];
            for (int i = 0; i < inputWaveform.Length; i++)
            {
                intervalIndex = (int)((inputWaveform[i] - minValue) / waveformPeakPeak * groupNums * intervalTopScale);
                if (intervalIndex >= 0 && intervalIndex < groupNums)
                {
                    intervalIntegrator[intervalIndex] += inputWaveform[i];
                    intervalCounter[intervalIndex]++;
                }
            }

            if (histogramY == null || histogramY.Length != groupNums)
            {
                histogramY = new double[groupNums];
            }
            if (histogramX == null || histogramX.Length != groupNums)
            {
                histogramX = new double[groupNums];
            }

            for (int levelCount_i = 0; levelCount_i < groupNums; levelCount_i++)
            {
                histogramY[levelCount_i] = intervalCounter[levelCount_i];
                histogramX[levelCount_i] = (double)levelCount_i / intervals * waveformPeakPeak;
            }
            return true;
        }

        /// <summary>
        /// <para></para>
        /// <para>Chinese Simplified：获取方波占空比直方图</para>
        /// </summary>
        /// <param name="inputWaveform">
        /// <para>The input waveform</para>
        /// <para>Chinese Simplified：输入波形</para>
        /// </param>
        /// <param name="histogramX">
        /// <para>The X coordinate of the histogram</para>
        /// <para>Chinese Simplified：直方图X坐标</para>
        /// </param>
        /// <param name="histogramY">
        /// <para>The Y coordinate of the histogram</para>
        /// <para>Chinese Simplified：直方图Y坐标</para>
        /// </param>
        /// <param name="histogramConfig">
        /// <para>Config of histogram </para>
        /// <para>Chinese Simplified：直方图配置</para>
        /// </param>
        /// <returns>
        /// <para>return true if this method is called, else false</para>
        /// <para>Chinese Simplified：如果调用这个方法之前已经设置过波形返回值为true，否则为false</para>
        /// </returns>
        /// <returns>如果调用这个方法之前已经设置过波形返回值为true，否则为false</returns>
        public static bool GetDutyCycleHistogram(double[] inputWaveform, ref double[] histogramX, ref double[] histogramY, Histogram histogramConfig)
        {
            if (inputWaveform == null)
            {
                return false;
            }
            int intervals = 50;
            // make sure amplitude analysis is done
            double highLevel;
            double lowLevel;
            AmplitudeAnalysis(inputWaveform, out highLevel, out lowLevel);
            // use high low levels to analysis the period
            double amplitude = highLevel - lowLevel;
            double stateTollerance = 0.1 * amplitude;
            double highTreshold = highLevel - stateTollerance;
            double lowThreshold = lowLevel + stateTollerance;
            bool enteringHigh = false;
            bool enteredLow = false;
            bool enteringLow = false;
            bool enteredHigh = false;
            int lastFallingEdge = 0;
            List<double> dutyCycle = new List<double>(10);
            int[] intervalCounter;
            int intervalIndex = 0;
            int edgeCount = 0;
            int lastEdge = 0;
            double periodIntegrator = 0;
            for (int i = 1; i < inputWaveform.Length; i++)
            {
                enteredLow = enteredLow ||
                    (inputWaveform[i - 1] > lowThreshold && inputWaveform[i] <= lowThreshold);//处于低电压段
                enteringHigh = inputWaveform[i - 1] < highTreshold && inputWaveform[i] >= highTreshold;//上升沿
                if (enteringHigh && enteredLow)
                {
                    enteredLow = false;
                    edgeCount++;//上升沿计数
                    if (edgeCount > 1)
                    {
                        periodIntegrator += i - lastEdge;//计算所有完整周期内所包含的点数之和
                        dutyCycle.Add((double)(lastFallingEdge - lastEdge) / (i - lastEdge));//计算该周期的占空比 
                    }
                    lastEdge = i;//记录该次上升沿所对应的索引
                }
                //捕获下降沿
                enteredHigh = enteredHigh || (inputWaveform[i - 1] < highTreshold && inputWaveform[i] >= highTreshold);//处于高电压段
                enteringLow = inputWaveform[i - 1] > lowThreshold && inputWaveform[i] <= lowThreshold;//下降沿
                if (enteringLow && enteredHigh)
                {
                    enteredHigh = false;
                    lastFallingEdge = i;
                }
            }
            double minValue = double.IsNaN(histogramConfig.LowerLimit) ? dutyCycle.Min() : histogramConfig.LowerLimit;
            double maxValue = double.IsNaN(histogramConfig.UpperLimit) ? dutyCycle.Max() : histogramConfig.UpperLimit;
            double detaDuty = maxValue - minValue;
            int groupNums = double.IsNaN(histogramConfig.Interval) ? intervals : (int)(detaDuty / histogramConfig.Interval);
            intervalCounter = new int[groupNums];
            for (int i = 0; i < dutyCycle.Count; i++)
            {
                intervalIndex = (int)((dutyCycle.ElementAt(i) - minValue) / detaDuty * groupNums);
                if (intervalIndex >= 0 && intervalIndex < groupNums)
                {
                    intervalCounter[intervalIndex]++;
                }
            }

            if (histogramY == null || histogramY.Length != groupNums)//避免在长度相同的情况下创建数组，增加Gc的工作量
            {
                histogramY = new double[groupNums];
            }
            if (histogramX == null || histogramX.Length != groupNums)
            {
                histogramX = new double[groupNums];
            }

            for (int dutyCycle_i = 0; dutyCycle_i < groupNums; dutyCycle_i++)
            {
                histogramY[dutyCycle_i] = intervalCounter[dutyCycle_i];
                histogramX[dutyCycle_i] = (double)dutyCycle_i / groupNums * detaDuty;
            }
            return true;
        }

        /// <summary>
        /// Amplitude Analysis
        /// <para>Chinese Simplified：幅度分析</para>
        /// </summary>
        /// <param name="waveform">
        /// <para>The input waveform</para>
        /// <para>Chinese Simplified：输入波形</para>
        /// </param>
        /// <param name="highLevel">
        /// <para>High level</para>
        /// <para>Chinese Simplified：高电平</para>
        /// </param>
        /// <param name="lowLevel">
        /// <para>Low level</para>
        /// <para>Chinese Simplified：低电平
        /// </para>
        /// </param>
        public static void AmplitudeAnalysis(double[] waveform, out double highLevel, out double lowLevel)
        {
            //devide the absolute max to min by 'intervals'
            //count the waveform levels within each interval
            //find the high level in the peak count from above
            //find the low level in the peak cout from bellow
            //get high and low levels by calculating the average of each peak interval;
            int intervals = 50;
            double intervalTopScale = 0.99;
            double[] intervalIntegrator = new double[intervals];
            int[] intervalCounter = new int[intervals];
            double waveformMax = waveform.Max();
            double waveformMin = waveform.Min();
            double waveformPeakPeak = waveformMax - waveformMin;
            int intervalIndex = 0;

            for (int i = 0; i < waveform.Length; i++)
            {
                intervalIndex = (int)((waveform[i] - waveformMin) / waveformPeakPeak * intervals * intervalTopScale);
                intervalIntegrator[intervalIndex] += waveform[i];
                intervalCounter[intervalIndex]++;
            }

            //find the peak interval
            int peakIndex = intervals - 1;
            while (intervalCounter[peakIndex] < waveform.Length / intervals
            || intervalCounter[peakIndex - 1] >= intervalCounter[peakIndex])
            {
                peakIndex--;
            }
            highLevel = intervalIntegrator[peakIndex] / intervalCounter[peakIndex];
            //find the valley interval
            int valleyIndex = 0;
            while (intervalCounter[valleyIndex] < waveform.Length / intervals
                || intervalCounter[valleyIndex + 1] >= intervalCounter[valleyIndex])
            {
                valleyIndex++;
            }
            lowLevel = intervalIntegrator[valleyIndex] / intervalCounter[valleyIndex];
        }

        /// <summary>
        /// Period Analysis
        /// <para>Chinese Simplified：周期分析</para>
        /// </summary>
        /// <param name="waveform">
        /// <para>The input waveform</para>
        /// <para>Chinese Simplified：输入波形</para>
        /// </param>
        /// <param name="period">
        /// <para>period</para>
        /// <para>Chinese Simplified：周期</para>
        /// </param>
        /// <param name="dutyCycleAvg">
        /// <para>Averaged dutycycle</para>
        /// <para>Chinese Simplified：平均后的占空比</para>
        /// </param>
        /// <param name="pulseCount">
        /// <para>Count of pulse</para>
        /// <para>Chinese Simplified：脉冲个数</para>
        /// </param>
        /// <param name="pulseMaxWidth">
        /// <para>Max Width of pulse</para>
        /// <para>Chinese Simplified：脉冲宽度最大值</para>
        /// </param>
        /// <param name="pulseMinWidth">
        /// <para>Min Width of pulse</para>
        /// <para>Chinese Simplified：脉冲宽度最小值</para>
        /// </param>
        public static void PeriodAnalysis(double[] waveform, out double period, out double dutyCycleAvg,
                                          out double pulseCount, out double pulseMaxWidth, out double pulseMinWidth)
        {
            double highLevel;
            double lowLevel;
            int intervals = 50;
            // make sure amplitude analysis is done
            AmplitudeAnalysis(waveform, out highLevel, out lowLevel);
            // use high low levels to analysis the period
            double amplitude = highLevel - lowLevel;
            double stateTollerance = 0.1 * amplitude;
            double highTreshold = highLevel - stateTollerance;
            double lowThreshold = lowLevel + stateTollerance;
            bool enteringHigh = false;
            bool enteredLow = false;
            bool enteringLow = false;
            bool enteredHigh = false;
            int lastFallingEdge = 0;
            double dutyCycle = 0;
            int[] intervalCounter = new int[intervals];
            int intervalIndex = 0;
            double[] intervalIntegrator = new double[intervals];
            double pulseMaxWidth2 = 0;
            double pulseMinWidth2 = 100000000;
            int edgeCount = 0;
            int lastEdge = 0;
            int periodCount = 0;
            double periodIntegrator = 0;
            for (int i = 1; i < waveform.Length; i++)
            {

                enteredLow = enteredLow ||
                    (waveform[i - 1] > lowThreshold && waveform[i] <= lowThreshold);//处于低电压段
                enteringHigh = waveform[i - 1] < highTreshold && waveform[i] >= highTreshold;//上升沿
                if (enteringHigh && enteredLow)
                {
                    enteredLow = false;
                    edgeCount++;//上升沿计数
                    if (edgeCount > 1)
                    {
                        periodCount++;//周期数计数
                        periodIntegrator += i - lastEdge;//计算所有完整周期内所包含的点数之和
                        if ((lastFallingEdge - lastEdge) > pulseMaxWidth2)//获取最大脉宽
                        {
                            pulseMaxWidth2 = lastFallingEdge - lastEdge;
                        }
                        if ((lastFallingEdge - lastEdge) < pulseMinWidth2)//获取最小脉宽
                        {
                            pulseMinWidth2 = lastFallingEdge - lastEdge;
                        }
                        dutyCycle = (double)(lastFallingEdge - lastEdge) / (i - lastEdge);//计算该周期的占空比
                        intervalIndex = (int)((dutyCycle - 0.0) / (1 - 0.0) * intervals);//
                        intervalIntegrator[intervalIndex] += dutyCycle;
                        intervalCounter[intervalIndex]++;//
                    }
                    lastEdge = i;//记录该次上升沿所对应的索引
                }
                //  捕获下降沿
                enteredHigh = enteredHigh || (waveform[i - 1] < highTreshold && waveform[i] >= highTreshold);//处于高电压段
                enteringLow = waveform[i - 1] > lowThreshold && waveform[i] <= lowThreshold;//下降沿
                if (enteringLow && enteredHigh)
                {
                    enteredHigh = false;
                    lastFallingEdge = i;
                }
            }
            period = periodIntegrator / periodCount;
            pulseMaxWidth = pulseMaxWidth2;
            pulseMinWidth = pulseMinWidth2;
            pulseCount = (double)periodCount + 1;//由以上算法可知，计算出来的periodCount比实际的周期数要少1个，故在此做加1
            double dutyCycleValue = 0;
            int maxIndex = 0;
            double dutyCycleMax = 0;
            double dutyCycleMin = 100;
            for (int i = 0; i < intervals; i++)
            {
                dutyCycleValue = intervalIntegrator[i] / intervalCounter[i];//当出现0/0时，计算结果是非数字
                if (!double.IsNaN(dutyCycleValue))
                {
                    if (dutyCycleValue > dutyCycleMax)
                    {
                        dutyCycleMax = dutyCycleValue;
                        maxIndex = i;
                    }

                    if (dutyCycleValue < dutyCycleMin)
                    {
                        dutyCycleMin = dutyCycleValue;
                    }
                }
            }
            dutyCycleAvg = intervalIntegrator[maxIndex] / intervalCounter[maxIndex];//占空比的平均值
            //for (int dutyCycle_i = 0; dutyCycle_i < intervals; dutyCycle_i++)
            //{
            //    _dutyCycleCount[dutyCycle_i] = intervalCounter[dutyCycle_i];
            //}
        }

        /// <summary>
        /// <para>Phase Analysis</para>
        /// <para>Chinese Simplified：相位分析</para>
        /// </summary>
        /// <param name="waveform">
        /// <para>The input waveform</para>
        /// <para>Chinese Simplified：输入波形</para>
        /// </param>
        /// <param name="waveformRef">
        /// <para>The reference waveform</para>
        /// <para>Chinese Simplified：参考波形</para>
        /// </param>
        /// <param name="phase">
        /// <para>phase</para>
        /// <para>Chinese Simplified：输入波形相对于参考波形的相位</para> 
        /// </param>
        public static void PhaseAnalysis(double[] waveform, double[] waveformRef,out double phase)
        {
            double productSum = 0;
            double maxProductSum = 0;
            double offsetOfMax = 0;
            phase = 0;
            //make sure amplitude and period are _calculated
            double period;
            double dutyCycle;
            double pulseCount;
            double pulseMaxWidth;
            double pulseMinWidth;
            PeriodAnalysis(waveform,out period,out dutyCycle, out  pulseCount, out  pulseMaxWidth, out  pulseMinWidth); 
            if (Math.Min(waveform.Length, waveformRef.Length) > 2 * period)
            {
                int compareLength = Math.Min((int)(waveform.Length - period), waveformRef.Length);
                for (int indexOffset = 0; indexOffset < period; indexOffset++)//当输入的波形长度很大时，计算量会很大
                {
                    productSum = 0;
                    for (int i = 0; i < compareLength; i++)
                    {
                        productSum += waveform[i] * waveformRef[i + indexOffset];
                    }
                    if (productSum > maxProductSum)
                    {
                        maxProductSum = productSum;
                        offsetOfMax = indexOffset;
                    }
                }
                phase = 360 * offsetOfMax / period;
            }

        }


        /// <summary>
        /// <para>Time Analysis</para>
        /// <para>Chinese Simplified：计算电平归一化后0.1~0.9的上升下降的抽样点数,risingSamples为上升的抽样点数，fallingSamples为下降的抽样点数</para>
        /// </summary>
        /// <param name="waveform"></param>
        /// <para>The input waveform</para>
        /// <para>Chinese Simplified：输入波形</para>
        /// <param name="risingSamples"></param>
        /// <para>Rising Time</para>
        /// <para>Chinese Simplified：上升时间</para>
        /// <param name="fallingSamples"></param>
        /// <para>FallingTime</para>
        /// <para>Chinese Simplified：下降时间</para>
        public static void TimeAnalysis(double[] waveform, out double risingSamples, out double fallingSamples)
        {
            double _risingSamples = 0;
            double _fallingSamples = 0;
            double highLevel;
            double lowLevel;
            AmplitudeAnalysis(waveform, out highLevel, out lowLevel);
            //以0.2-0.8计算上升下降时间
            double _highLevel = (highLevel - lowLevel) * 0.9;
            double _lowLevel= (highLevel - lowLevel) * 0.1;
            bool _flagRisingCounter = false;//开始上升时间计数标志
            bool _flagFallingCounter = false;//开始下降时间计数标志
            int _counterRising = 0;//上升沿个数
            int _counterFalling = 0;//下降沿个数
            int _samplesRising = 0;//上升抽样点数
            int _samplesFalling = 0;//下降抽样点数
            for (int i = 1; i < waveform.Length; i++)
            {
                //上升时间
                if (waveform[i - 1] < _lowLevel && waveform[i] >= _lowLevel)
                {
                    _flagRisingCounter = true;
                    _counterRising++;
                }
                if (waveform[i - 1] <= _highLevel && waveform[i] > _highLevel)
                {
                    _flagRisingCounter = false;
                }
                if (_flagRisingCounter)//上升
                {
                    _samplesRising++;
                }

                //下降时间
                if (waveform[i - 1] > _highLevel && waveform[i] <= _highLevel)
                {
                    _flagFallingCounter = true;
                    _counterFalling++;
                }
                if (waveform[i - 1] >= _lowLevel && waveform[i] < _lowLevel)
                {
                    _flagFallingCounter = false ;
                }
                if (_flagFallingCounter)
                {
                    _samplesFalling++;
                }
            }
            //求平均
            _risingSamples = _samplesRising / _counterRising;
            _fallingSamples = _samplesFalling / _counterFalling;

            risingSamples = _risingSamples;
            fallingSamples = _fallingSamples;
        }

        #endregion

        #region----------------------SquarewaveMeasurements需要用到的结构体和枚举类定义---------
        /// <summary>
        /// <para>Config of histogram</para>
        /// <para>Chinese Simplified：统计的参数设置</para>
        /// </summary>
        public class Histogram
        {
            /// <summary>
            /// <para>LowerLimit</para>
            /// <para>Chinese Simplified：统计范围的下限</para>
            /// </summary>
            public double LowerLimit  { set; get; }

            /// <summary>
            /// <para>Interval</para>
            /// <para>Chinese Simplified：组距</para>
            /// </summary>
            public double Interval { set; get; }

            /// <summary>
            /// <para>UpperLimit</para>
            /// <para>Chinese Simplified：统计范围的上限</para>
            /// </summary>
            public double UpperLimit  { set; get; }

            /// <summary>
            /// <para></para>
            /// <para>Chinese Simplified：构造函数</para>
            /// </summary>
            public Histogram(double lowerLimit=double.NaN,double interval=double.NaN,double upperLimit=double.NaN)
            {
                LowerLimit = lowerLimit;
                Interval = interval;
                UpperLimit = upperLimit;
            }
        }
        #endregion      

    }
}
