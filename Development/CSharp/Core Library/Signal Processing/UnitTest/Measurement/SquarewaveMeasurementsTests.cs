using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeeSharpTools.JY.ArrayUtility;
using System;
using System.IO;
using System.Windows.Forms;
using SeeSharpTools.JY.DSP.Fundamental;
using SeeSharpTools.JX.DataViewer;

namespace SeeSharpTools.JXI.SignalProcessing.Measurement.Tests
{
    [TestClass()]
    public class SquarewaveMeasurementsTests
    {
        private double[,] ReadFromCsvFile(string filePath)  //返回一个double array
        {
            int numberOfRows;
            int numberOfColumns;
            int numHeadLine = 1;
            if (filePath == null) return null;

            // Read all lines and get number of rows and columns.
            string[] allLines = File.ReadAllLines(filePath);
            numberOfRows = allLines.Length;
            double[] rowData = Array.ConvertAll(allLines[numHeadLine].Split(','), double.Parse);
            numberOfColumns = rowData.Length;

            // Parse data by lines.
            double[,] dataArray = new double[numberOfColumns, numberOfRows];
            for (int i = numHeadLine; i < numberOfRows; i++)
            {
                rowData = Array.ConvertAll(allLines[i].Split(','), double.Parse);
                ArrayManipulation.ReplaceArraySubset(rowData, ref dataArray, i, ArrayManipulation.IndexType.column);
            }
            return dataArray;
        }

        //[TestMethod()]
        //public void AnalyzeTest_001()
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    openFileDialog.Filter = "CSV文件|*.csv";
        //    openFileDialog.RestoreDirectory = true;
        //    openFileDialog.FilterIndex = 1;
        //    if (openFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        var waveforms = ReadFromCsvFile(openFileDialog.FileName);
        //        SquarewaveMeasurements waveformMeas = new SquarewaveMeasurements();
        //        //将二维数组的第0行波形数据取出来，作为参考波形，用于求解相位
        //        double[] waveformTmp = new double[waveforms.GetLength(1)];
        //        ArrayManipulation.GetArraySubset(waveforms, 0, ref waveformTmp, ArrayManipulation.IndexType.row);
        //        //第一步
        //        waveformMeas.SetRefWaveform(waveformTmp);//写入参考波形，
        //        for (int i = 0; i < waveforms.GetLength(0); i++)
        //        {
        //            //二维数组的某行取出，放置于waveformTmp
        //            ArrayManipulation.GetArraySubset(waveforms, i, ref waveformTmp, ArrayManipulation.IndexType.row);
        //            //第二步
        //            waveformMeas.SetWaveform(waveformTmp);//写入要处理的波形，
        //            //第三步，该方波分析类中还分析了占空比、占空比统计直方图、最大高电平脉宽、最小低电平脉宽、脉冲个数、方波电平统计直方图，
        //            var highLevel = waveformMeas.GetHighStateLevel();//获取高电平
        //            var lowLevel = waveformMeas.GetLowStateLevel();//获取低电平
        //            var period = waveformMeas.GetPeriod();//获取方波的周期
        //            var phase = waveformMeas.GetPhase();//获取该段波形的相对于参考波形的相位
        //            double[] levelHistogramX = new double[0];
        //            double[] levelHistogramY = new double[0];
        //            waveformMeas.GetLevelHistogram(ref levelHistogramX, ref levelHistogramY, new SquarewaveMeasurements.Histogram());
        //            for (int j = 0; j < levelHistogramX.Length; j++)
        //            {
        //                Console.WriteLine("{0}:{1}", levelHistogramX[j], levelHistogramY[j]);
        //            }
        //            Console.WriteLine("highLevel:{0}", highLevel);
        //            Console.WriteLine("lowLevel:{0}", lowLevel);
        //            Console.WriteLine("period:{0}", period);
        //            Console.WriteLine("phase:{0}", phase);
        //        }
        //    }
        //}

        [TestMethod()]
        public void AnalyzeTest_002()
        {
            /**************************
             * 产生频率为20Hz，采样率为1000Hz，幅值为2V，占空比为50%的方波
             * 测量方波的高电平、低电平、周期、相位
             * ***************************/

            double[] waveformTmp = new double[1000];
            double[] waveformRef = new double[1000];
            JY.DSP.Fundamental.Generation.SquareWave(ref waveformTmp, 2, 50, 20, 1000);
            JY.DSP.Fundamental.Generation.SquareWave(ref waveformRef, 2, 50, 20, 1000);
            double highLevel;
            double lowLevel;
            double period;
            double dutycycle;
            double phase;
            double pulseCount;
            double pulseMaxLength;
            double pulseMinLength;
            SquarewaveMeasurements.AmplitudeAnalysis(waveformTmp, out highLevel, out lowLevel);
            SquarewaveMeasurements.PeriodAnalysis(waveformTmp, out period, out dutycycle,
                                   out pulseCount, out pulseMaxLength, out pulseMinLength);
            SquarewaveMeasurements.PhaseAnalysis(waveformTmp, waveformRef, out phase);
            double[] levelHistogramX = new double[0];
            double[] levelHistogramY = new double[0];
            SquarewaveMeasurements.GetLevelHistogram(waveformTmp,ref levelHistogramX, ref levelHistogramY, new SquarewaveMeasurements.Histogram());
            for (int j = 0; j < levelHistogramX.Length; j++)
            {
                Console.WriteLine("{0}:{1}", levelHistogramX[j], levelHistogramY[j]);
            }
            Console.WriteLine("highLevel:{0}", highLevel);
            Console.WriteLine("lowLevel:{0}", lowLevel);
            Console.WriteLine("period:{0}", period);
            Console.WriteLine("phase:{0}", phase);

        }

        [TestMethod()]
        public void AnalyzeTest_003()
        {
            /******************************
             * 上升下降时间
             * ***************************/
            int cycle = 3;
            double _realRisingSamples = 173;
            double _realFallingSamples = 277;
            double _holdOnSamples = 452;
            double[] _rising = new double[(int)_realRisingSamples+1];
            double[] _falling = new double[(int)_realFallingSamples+1];
            double[] _holdOnHigh = new double[(int)_holdOnSamples];
            double[] _holdOnLow= new double[(int)_holdOnSamples];
            SeeSharpTools.JY.DSP.Fundamental.Generation.Ramp(ref _rising, 0, 1 / _realRisingSamples);
            SeeSharpTools.JY.DSP.Fundamental.Generation.Ramp(ref _falling, 1, -1 / _realFallingSamples);
            for (int i = 0; i < (int)_holdOnSamples; i++)
            {
                _holdOnHigh[i] = 1;
                _holdOnLow[i] = 0;
            }
            int length =(int)(_realRisingSamples+1 + _realFallingSamples+1 + 2 * _holdOnSamples);
            double[] waveform = new double[length * cycle];
            for (int i = 0; i < cycle; i++)
            {     
                //上升      
                Array.Copy(_rising, 0, waveform, i * length, _rising.Length);
                //高电平
                Array.Copy(_holdOnHigh, 0, waveform, i * length + _rising.Length, _holdOnHigh.Length);
                //下降
                Array.Copy(_falling, 0, waveform, i * length + _rising.Length+ _holdOnHigh.Length, _falling.Length);
                //低电平
                Array.Copy(_holdOnLow, 0, waveform, i * length + _rising.Length + _holdOnHigh.Length+ _falling.Length, _holdOnLow.Length);
            }
           AnalogWaveformChart.Plot("waveform", waveform);

            double risingSamples = 0;
            double fallingSamples = 0;
            SquarewaveMeasurements.TimeAnalysis(waveform, out risingSamples, out fallingSamples);
            Console.WriteLine ("真实上升时间{0}，测量上升时间{1}", Math.Ceiling( 0.8* _realRisingSamples)- Math.Ceiling(0.2 * _realRisingSamples), risingSamples);
            Console.WriteLine ("真实下降时间{0}，测量下降时间{1}", Math.Ceiling(0.8 * _realFallingSamples) - Math.Ceiling(0.2 * _realFallingSamples), fallingSamples);
        }
    }
}