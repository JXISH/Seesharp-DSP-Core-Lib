using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeeSharpTools.JXI.Mathematics.ProbabilityStatistics;
using SeeSharpTools.JXI.SignalProcessing.Generation;
using SeeSharpTools.JX.DataViewer;

namespace SeeSharpTools.JX.DSP.Generation.Tests
{
    [TestClass]
    public class GenerationUnitTest
    {
        [TestMethod]
        public void GaussianWhiteNoise_001()
        {
            /***********************************
             * 产生服从标准正态分布的高斯白噪声
             * 标准差：1
             * *********************************/
            int Length = 10000;
            var noise = new double[Length];
            int[] histgram=new  int[500];
            double[] histgramTemp = new double[histgram.Length];
            double[] intervals = new double[500];
            JXI.SignalProcessing.Generation.Generation.GaussianWhiteNoise(ref noise, 1);
            ProbabilityStatistics.Histogram(noise, ref histgram, ref intervals);
            for (int i = 0; i < histgram .Length ; i++)
            {
                histgramTemp[i] = (double)histgram[i];
            }
            AnalogWaveformChart.Plot("histgram", histgramTemp);
            AnalogWaveformChart.Plot("intervals", intervals);
            AnalogWaveformChart.Plot("noise", noise);
        }
    }
}
