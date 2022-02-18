using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeeSharpTools.JY.DSP.Fundamental;
using System;
using SeeSharpTools.JX.DataViewer;
using SeeSharpTools.JXI.SignalProcessing.Conditioning.EasyFilters;

namespace SeeSharpTools.JXI.SignalProcessing.Measurement.Tests
{
    [TestClass()]
    public class ThirdOctaveAnalysisTests
    {
        [TestMethod()]
        public void AnalyzeTest_001()
        {
            double duration = 2; //seconds;
            double sampleRate = 51200; //you cannot change this
            int dataLength = (int)(duration * sampleRate);
            double[] sin1 = new double[dataLength];
             JY.DSP.Fundamental.Generation.SineWave(ref sin1, 1.414, 0, (int)Math.Round(duration * 1000));
            //JY.DSP.Fundamental.Generation.SineWave(ref sin1, 1.414, 0, 50, sampleRate);
            ThirdOctaveAnalysis analysis = new ThirdOctaveAnalysis();
            analysis.AverageMode = TimeAveragingMode.Fast;
            analysis.WeightingFilterType = WeightingType.AWeighting;
            var result = analysis.Analyze(sin1, sampleRate);
            double[] octaveLevels = new double[result.ThirdOctaveLevels.Length];
            for (int i = 0; i < octaveLevels.Length; i++)
            {
                octaveLevels[i] = 20 * Math.Log10(result.ThirdOctaveLevels[i]) + 100;
            }
            AnalogWaveformChart.Plot("Mode=Fast and  WeightingFilterType=AWeighting", octaveLevels);

            analysis.AverageMode = TimeAveragingMode.Fast;
            analysis.WeightingFilterType = WeightingType.None;
            result = analysis.Analyze(sin1, sampleRate);
            octaveLevels = new double[result.ThirdOctaveLevels.Length];
            for (int i = 0; i < octaveLevels.Length; i++)
            {
                octaveLevels[i] = 20 * Math.Log10(result.ThirdOctaveLevels[i]) + 100;
            }
            AnalogWaveformChart.Plot("Mode=Fast and  WeightingFilterType=None", octaveLevels);

            analysis.AverageMode = TimeAveragingMode.Impulsive;
            analysis.WeightingFilterType = WeightingType.AWeighting;
            result = analysis.Analyze(sin1, sampleRate);
            octaveLevels = new double[result.ThirdOctaveLevels.Length];
            for (int i = 0; i < octaveLevels.Length; i++)
            {
                octaveLevels[i] = 20 * Math.Log10(result.ThirdOctaveLevels[i]) + 100;
            }
            AnalogWaveformChart.Plot("Mode=Impulsive and  WeightingFilterType=AWeighting", octaveLevels);

            analysis.AverageMode = TimeAveragingMode.Impulsive;
            analysis.WeightingFilterType = WeightingType.None;
            result = analysis.Analyze(sin1, sampleRate);
            octaveLevels = new double[result.ThirdOctaveLevels.Length];
            for (int i = 0; i < octaveLevels.Length; i++)
            {
                octaveLevels[i] = 20 * Math.Log10(result.ThirdOctaveLevels[i]) + 100;
            }
            AnalogWaveformChart.Plot("Mode=Impulsive and  WeightingFilterType=None", octaveLevels);

            analysis.AverageMode = TimeAveragingMode.Slow;
            analysis.WeightingFilterType = WeightingType.AWeighting;
            result = analysis.Analyze(sin1, sampleRate);
            octaveLevels = new double[result.ThirdOctaveLevels.Length];
            for (int i = 0; i < octaveLevels.Length; i++)
            {
                octaveLevels[i] = 20 * Math.Log10(result.ThirdOctaveLevels[i]) + 100;
            }
            AnalogWaveformChart.Plot("Mode=Slow and  WeightingFilterType=AWeighting", octaveLevels);

            analysis.AverageMode = TimeAveragingMode.Slow;
            analysis.WeightingFilterType = WeightingType.None;
            result = analysis.Analyze(sin1, sampleRate);
            octaveLevels = new double[result.ThirdOctaveLevels.Length];
            for (int i = 0; i < octaveLevels.Length; i++)
            {
                octaveLevels[i] = 20 * Math.Log10(result.ThirdOctaveLevels[i]) + 100;
            }
            AnalogWaveformChart.Plot("Mode=Slow and  WeightingFilterType=None", octaveLevels);
        }

        [TestMethod()]
        public void AnalyzeTest_002()
        {
            /*****************************
             * 产生50Hz的正弦信号
             * 做1/3倍频程分析
             * 时间平均方式为Fast
             * 加权滤波器的类型为A加权
             * 中心频率为50即第4频段（从0开始）的能量最高
             * ******************************/
            double duration = 2; //seconds;
            double sampleRate = 51200; //you cannot change this
            int dataLength = (int)(duration * sampleRate);
            double[] sin1 = new double[dataLength];
            JY.DSP.Fundamental.Generation.SineWave(ref sin1, 1.414, 0, 50, sampleRate);
            ThirdOctaveAnalysis analysis = new ThirdOctaveAnalysis();
            analysis.AverageMode = TimeAveragingMode.Fast;
            analysis.WeightingFilterType = WeightingType.AWeighting;
            var result = analysis.Analyze(sin1, sampleRate);
            double[] octaveLevels = new double[result.ThirdOctaveLevels.Length];
            for (int i = 0; i < octaveLevels.Length; i++)
            {
                octaveLevels[i] = 20 * Math.Log10(result.ThirdOctaveLevels[i]) + 100;
            }
            AnalogWaveformChart.Plot("Mode=Fast and  WeightingFilterType=AWeighting", octaveLevels);          
        }

        [TestMethod()]
        public void AnalyzeTest_003()
        {
            /*****************************
             * 产生50Hz的正弦信号
             * 做1/3倍频程分析
             * 时间平均方式为Fast
             * 加权滤波器的类型为无加权
             * 中心频率为50即第4频段（从0开始）的能量最高
             * ******************************/
            double duration = 2; //seconds;
            double sampleRate = 51200; //you cannot change this
            int dataLength = (int)(duration * sampleRate);
            double[] sin1 = new double[dataLength];
            JY.DSP.Fundamental.Generation.SineWave(ref sin1, 1.414, 0, 50, sampleRate);
            ThirdOctaveAnalysis analysis = new ThirdOctaveAnalysis();
            analysis.AverageMode = TimeAveragingMode.Fast;
            analysis.WeightingFilterType = WeightingType.None;
            var result = analysis.Analyze(sin1, sampleRate);
            double[] octaveLevels = new double[result.ThirdOctaveLevels.Length];
            result = analysis.Analyze(sin1, sampleRate);
            octaveLevels = new double[result.ThirdOctaveLevels.Length];
            for (int i = 0; i < octaveLevels.Length; i++)
            {
                octaveLevels[i] = 20 * Math.Log10(result.ThirdOctaveLevels[i]) + 100;
            }
            AnalogWaveformChart.Plot("Mode=Fast and  WeightingFilterType=None", octaveLevels);           
        }

        [TestMethod()]
        public void AnalyzeTest_004()
        {
            /*****************************
             * 产生50Hz的正弦信号
             * 做1/3倍频程分析
             * 时间平均方式为Impulsive
             * 加权滤波器的类型为A加权
             * 中心频率为50即第4频段（从0开始）的能量最高
             * ******************************/
            double duration = 2; //seconds;
            double sampleRate = 51200; //you cannot change this
            int dataLength = (int)(duration * sampleRate);
            double[] sin1 = new double[dataLength];
            JY.DSP.Fundamental.Generation.SineWave(ref sin1, 1.414, 0, 50, sampleRate);
            ThirdOctaveAnalysis analysis = new ThirdOctaveAnalysis();
            analysis.AverageMode = TimeAveragingMode.Impulsive;
            analysis.WeightingFilterType = WeightingType.AWeighting;
            var result = analysis.Analyze(sin1, sampleRate);
            double[] octaveLevels = new double[result.ThirdOctaveLevels.Length];
            result = analysis.Analyze(sin1, sampleRate);
            octaveLevels = new double[result.ThirdOctaveLevels.Length];
            for (int i = 0; i < octaveLevels.Length; i++)
            {
                octaveLevels[i] = 20 * Math.Log10(result.ThirdOctaveLevels[i]) + 100;
            }
            AnalogWaveformChart.Plot("Mode=Fast and  WeightingFilterType=None", octaveLevels);
        }

        [TestMethod()]
        public void AnalyzeTest_005()
        {
            /*****************************
             * 产生50Hz的正弦信号
             * 做1/3倍频程分析
             * 时间平均方式为Impulsive
             * 加权滤波器的类型为无加权
             * 中心频率为50即第4频段（从0开始）的能量最高
             * ******************************/
            double duration = 2; //seconds;
            double sampleRate = 51200; //you cannot change this
            int dataLength = (int)(duration * sampleRate);
            double[] sin1 = new double[dataLength];
            JY.DSP.Fundamental.Generation.SineWave(ref sin1, 1.414, 0, 50, sampleRate);
            ThirdOctaveAnalysis analysis = new ThirdOctaveAnalysis();
            analysis.AverageMode = TimeAveragingMode.Impulsive;
            analysis.WeightingFilterType = WeightingType.None ;
            var result = analysis.Analyze(sin1, sampleRate);
            double[] octaveLevels = new double[result.ThirdOctaveLevels.Length];
            result = analysis.Analyze(sin1, sampleRate);
            octaveLevels = new double[result.ThirdOctaveLevels.Length];
            for (int i = 0; i < octaveLevels.Length; i++)
            {
                octaveLevels[i] = 20 * Math.Log10(result.ThirdOctaveLevels[i]) + 100;
            }
            AnalogWaveformChart.Plot("Mode=Fast and  WeightingFilterType=None", octaveLevels);
        }

        [TestMethod()]
        public void AnalyzeTest_006()
        {
            /*****************************
             * 产生50Hz的正弦信号
             * 做1/3倍频程分析
             * 时间平均方式为Slow
             * 加权滤波器的类型为A加权
             * 中心频率为50即第4频段（从0开始）的能量最高
             * ******************************/
            double duration = 2; //seconds;
            double sampleRate = 51200; //you cannot change this
            int dataLength = (int)(duration * sampleRate);
            double[] sin1 = new double[dataLength];
            JY.DSP.Fundamental.Generation.SineWave(ref sin1, 1.414, 0, 50, sampleRate);
            ThirdOctaveAnalysis analysis = new ThirdOctaveAnalysis();
            analysis.AverageMode = TimeAveragingMode.Slow ;
            analysis.WeightingFilterType = WeightingType.AWeighting;
            var result = analysis.Analyze(sin1, sampleRate);
            double[] octaveLevels = new double[result.ThirdOctaveLevels.Length];
            result = analysis.Analyze(sin1, sampleRate);
            octaveLevels = new double[result.ThirdOctaveLevels.Length];
            for (int i = 0; i < octaveLevels.Length; i++)
            {
                octaveLevels[i] = 20 * Math.Log10(result.ThirdOctaveLevels[i]) + 100;
            }
            AnalogWaveformChart.Plot("Mode=Fast and  WeightingFilterType=None", octaveLevels);
        }

        [TestMethod()]
        public void AnalyzeTest_007()
        {
            /*****************************
             * 产生50Hz的正弦信号
             * 做1/3倍频程分析
             * 时间平均方式为Slow
             * 加权滤波器的类型为无加权
             * 中心频率为50即第4频段（从0开始）的能量最高
             * ******************************/
            double duration = 2; //seconds;
            double sampleRate = 51200; //you cannot change this
            int dataLength = (int)(duration * sampleRate);
            double[] sin1 = new double[dataLength];
            JY.DSP.Fundamental.Generation.SineWave(ref sin1, 1.414, 0, 50, sampleRate);
            ThirdOctaveAnalysis analysis = new ThirdOctaveAnalysis();
            analysis.AverageMode = TimeAveragingMode.Slow;
            analysis.WeightingFilterType = WeightingType.None;
            var result = analysis.Analyze(sin1, sampleRate);
            double[] octaveLevels = new double[result.ThirdOctaveLevels.Length];
            result = analysis.Analyze(sin1, sampleRate);
            octaveLevels = new double[result.ThirdOctaveLevels.Length];
            for (int i = 0; i < octaveLevels.Length; i++)
            {
                octaveLevels[i] = 20 * Math.Log10(result.ThirdOctaveLevels[i]) + 100;
            }
            AnalogWaveformChart.Plot("Mode=Fast and  WeightingFilterType=None", octaveLevels);
        }

        [TestMethod()]
        public void AnalyzeTest_008()
        {
            /*****************************
             * 产生均匀噪声
             * 做1/3倍频程分析
             * 中心频率增加能量增加
             * ******************************/
            double duration = 2; //seconds;
            double sampleRate = 51200; //you cannot change this
            int dataLength = (int)(duration * sampleRate);
            double[] sin1 = new double[dataLength];
            JY.DSP.Fundamental.Generation.UniformWhiteNoise(ref sin1, 1.414);
            ThirdOctaveAnalysis analysis = new ThirdOctaveAnalysis();
            analysis.AverageMode = TimeAveragingMode.Fast;
            analysis.WeightingFilterType = WeightingType.AWeighting;
            var result = analysis.Analyze(sin1, sampleRate);
            double[] octaveLevels = new double[result.ThirdOctaveLevels.Length];
            for (int i = 0; i < octaveLevels.Length; i++)
            {
                octaveLevels[i] = 20 * Math.Log10(result.ThirdOctaveLevels[i]) + 100;
            }
            AnalogWaveformChart.Plot("Mode=Fast and  WeightingFilterType=AWeighting", octaveLevels);         
        }
    }
}