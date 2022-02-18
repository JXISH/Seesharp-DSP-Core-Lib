/// 内容摘要: 概率统计类库覆盖测试
/// 概率统计类库包括均值、中位数、众数、方差、均方差、标准差、均方根的计算及一维数组的直方图统计
/// 完成日期: 2017年9月6日
/// 版    本:Version 1
/// 作    者:刘玉辉
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Windows.Forms;
using SeeSharpTools.JY.DSP.Fundamental;
using SeeSharpTools.JXI.Mathematics.ProbabilityStatistics;
using SeeSharpTools.JXI.Exception;


//正常运行计算结果是否正确
//给入空数组是否报错
//两个数组不等长是否报错
//只输入一个点
//拟合两次

//输入一个数计算结果是否正确

namespace SeeSharpTools.JXI.Mathematics.ProbabilityStatisticsUnitTest
{
    [TestClass]
    public class MeanUnitTest
    {
        [TestMethod]
        public void Mean_001()
        {
            /**********************************
             * 均值
             * 考察计算结果是否正确
             * *******************************/

            double[] x = new double[] { 1, 2, 3, 4, 5 };
            double aver;

            aver = 0;
            aver = ProbabilityStatistics.ProbabilityStatistics.Mean(x);
            Assert.IsTrue(aver == 3);

            aver = 0;
            aver = ProbabilityStatistics.ProbabilityStatistics.GeometricMean(x);
            Assert.IsTrue(Math.Abs(aver - 2.605) < 0.001);

            aver = 0;
            aver = ProbabilityStatistics.ProbabilityStatistics.HarmonicMean(x);
            Assert.IsTrue(Math.Abs(aver - 2.19) < 0.01);

            aver = 0;
            aver = ProbabilityStatistics.ProbabilityStatistics.TrimmedMean(x, 40);
            Assert.IsTrue(Math.Abs(aver - 3) < 0.01);
        }

        [TestMethod]
        public void Mean_002()
        {
            /**********************************
             * 均值
             * 考察输入为null是否报错
             * *******************************/

            double[] x = null;
            double aver;
            try
            {
                aver = ProbabilityStatistics.ProbabilityStatistics.Mean(x);               
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                                
            }
            try
            {
                aver = ProbabilityStatistics.ProbabilityStatistics.GeometricMean(x);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                
            }
            try
            {
                aver = ProbabilityStatistics.ProbabilityStatistics.HarmonicMean(x);               
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                
            }
            try
            {               
                aver = ProbabilityStatistics.ProbabilityStatistics.TrimmedMean(x, 40);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                
            }
            
        }

        [TestMethod]
        public void Mean_003()
        {
            /**********************************
             * 均值
             * 考察输入为空是否报错
             * *******************************/

            double[] x = new double[] { };
            double aver;
            try
            {
                aver = ProbabilityStatistics.ProbabilityStatistics.Mean(x);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }
            try
            {
                aver = ProbabilityStatistics.ProbabilityStatistics.GeometricMean(x);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }
            try
            {
                aver = ProbabilityStatistics.ProbabilityStatistics.HarmonicMean(x);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }
            try
            {
                aver = ProbabilityStatistics.ProbabilityStatistics.TrimmedMean(x, 40);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }

        }

        [TestMethod]
        public void Mean_004()
        {
            /**********************************
             * 均值
             * 考察切尾百分比过小是否报错
             * *******************************/

            double[] x = new double[] { 1, 2, 3, 4, 5 }; 
            double aver;
           
            try
            {
                aver = ProbabilityStatistics.ProbabilityStatistics.TrimmedMean(x, -10);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }

        }

        [TestMethod]
        public void Mean_005()
        {
            /**********************************
             * 均值
             * 考察切尾百分比过大是否报错
             * *******************************/

            double[] x = new double[] { 1, 2, 3, 4, 5 };
            double aver;

            try
            {
                aver = ProbabilityStatistics.ProbabilityStatistics.TrimmedMean(x,110);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }

        }

        [TestMethod]
        public void Mean_006()
        {
            /**********************************
             * 均值
             * 输入序列为一个元素考察计算结果是否正确
             * *******************************/

            double[] x = new double[] { 1 };
            double aver;

            aver = 0;
            aver = ProbabilityStatistics.ProbabilityStatistics.Mean(x);
            Assert.IsTrue(aver == 1);

            aver = 0;
            aver = ProbabilityStatistics.ProbabilityStatistics.GeometricMean(x);
            Assert.IsTrue(Math.Abs(aver - 1) < 0.001);

            aver = 0;
            aver = ProbabilityStatistics.ProbabilityStatistics.HarmonicMean(x);
            Assert.IsTrue(Math.Abs(aver - 1) < 0.01);

            aver = 0;
            aver = ProbabilityStatistics.ProbabilityStatistics.TrimmedMean(x, 40);
            Assert.IsTrue(Math.Abs(aver - 1) < 0.01);
        }
    }

    [TestClass]
    public class ModeUnitTest
    {
        [TestMethod]
        public void Mode_001()
        {
            /**********************************
             * 考察计算结果是否正确
             * *******************************/

            double[] x = { 1, 2, 3, 4, 3 };
            double[] result;

            result = new double[] { };
            result = ProbabilityStatistics.ProbabilityStatistics.Mode(x);

            if (result.Length != 1)
            {
                Assert.Fail();
            }
            else if (result[0]!=3)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Mode_005()
        {
            /**********************************
             * 考察计算结果是否正确(两个众数的情况)
             * *******************************/

            double[] x = { 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 2, 3};
            double[] result;

            result = new double[] { };        
                     
            result = ProbabilityStatistics.ProbabilityStatistics.Mode(x);
                        
            if (result.Length != 2)
            {
                Assert.Fail();
            }
            else if (result[0] != 2 || result[1] != 3)
            {
                Assert.Fail();
            }          
        }

        [TestMethod]
        public void Mode_006()
        {
            /**********************************
             * 考察计算结果是否正确(没有众数的情况)
             * *******************************/

            double[] x = { 1, 2, 3, 4, 1, 2, 3, 4 };
            double[] result;

            result = new double[] { };
            result = ProbabilityStatistics.ProbabilityStatistics.Mode(x);

            if (result.Length != 0)
            {
                Assert.Fail();
            }           
        }

        [TestMethod]
        public void Mode_002()
        {
            /**********************************
             * 考察输入为null是否报错
             * *******************************/

            double[] x = null;
            double[] result;
            
            try
            {
                result = ProbabilityStatistics.ProbabilityStatistics.Mode(x);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }           
        }

        [TestMethod]
        public void Mode_003()
        {
            /**********************************
             * 考察输入为空是否报错
             * *******************************/

            double[] x = new double[] { };
            double[] result;

            try
            {
                result = ProbabilityStatistics.ProbabilityStatistics.Mode(x);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }
        }

        [TestMethod]
        public void Mode_004()
        {
            /**********************************
             * 输入序列为一个元素考察计算结果是否正确
             * *******************************/

            double[] x = { 3 };
            double[] result;

            result = new double[] { };
            result = ProbabilityStatistics.ProbabilityStatistics.Mode(x);

            if (result.Length != 0)
            {
                Assert.Fail();
            }           
        }
    }

    [TestClass]
    public class MedianUnitTest
    {
        [TestMethod]
        public void Median_001()
        {
            /**********************************
             * 考察计算结果是否正确
             * *******************************/

            double[] x = { 1, 2, 3, 4, 5 };
            double result;

            result = 0;
            result = ProbabilityStatistics.ProbabilityStatistics.Median(x);

            Assert.AreEqual(3,result);

            x = new double[]{ 1, 2, 3, 4 };
            result = 0;
            result = ProbabilityStatistics.ProbabilityStatistics.Median(x);

            Assert.AreEqual(2.5, result);

        }

        [TestMethod]
        public void Median_002()
        {
            /**********************************
             * 考察输入为null是否报错
             * *******************************/

            double[] x = null;
            double result;

            try
            {
                result = ProbabilityStatistics.ProbabilityStatistics.Median(x);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }
        }

        [TestMethod]
        public void Median_003()
        {
            /**********************************
             * 考察输入为空是否报错
             * *******************************/

            double[] x = new double[] { };
            double result;

            try
            {
                result = ProbabilityStatistics.ProbabilityStatistics.Median(x);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }
        }

        [TestMethod]
        public void Median_004()
        {
            /**********************************
             * 输入序列为一个元素考察计算结果是否正确
             * *******************************/

            double[] x = { 3 };
            double result;

            result = 0;
            result = ProbabilityStatistics.ProbabilityStatistics.Median(x);

            Assert.AreEqual(3, result);
            
        }
    }

    [TestClass]
    public class MeanSquareErrorUnitTest
    {
        
        [TestMethod]
        public void MeanSquareError_001()
        {
            /**********************************
             * 考察计算结果是否正确
             * *******************************/

            double[] x = { 1, 2, 3, 4, 5 };
            double[] y = { 1.1, 1.9, 3.2, 4.1, 5 };
            double result;

            result = 0;
            result = ProbabilityStatistics.ProbabilityStatistics.MeanSquaredError(x, y); 

            Assert.AreEqual(0.014, result,0.0000001);
        }

        [TestMethod]
        public void MeanSquareError_002()
        {
            /**********************************
             * 考察输入为null是否报错
             * *******************************/

            double[] x = { 1, 2, 3, 4, 5 };
            double[] y = { 1.1, 1.9, 3.2, 4.1, 5 };
            double result;

            try
            {
                result = ProbabilityStatistics.ProbabilityStatistics.MeanSquaredError(null, y);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }

            try
            {
                result = ProbabilityStatistics.ProbabilityStatistics.MeanSquaredError(x, null);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }
        }

        [TestMethod]
        public void MeanSquareError_003()
        {
            /**********************************
             * 考察输入为空是否报错
             * *******************************/

            double[] x = { 1, 2, 3, 4, 5 };
            double[] y = { 1.1, 1.9, 3.2, 4.1, 5 };
            double result;

            try
            {
                result = ProbabilityStatistics.ProbabilityStatistics.MeanSquaredError(new double[] { }, y);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }

            try
            {
                result = ProbabilityStatistics.ProbabilityStatistics.MeanSquaredError(x, new double[] { });
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }
        }

        [TestMethod]
        public void MeanSquareError_004()
        {
            /**********************************
             * 输入序列为一个元素考察计算结果是否正确
             * *******************************/

            double[] x = { 1};
            double[] y = { 1};
            double result;

            result = 0;
            result = ProbabilityStatistics.ProbabilityStatistics.MeanSquaredError(x, y);

            Assert.AreEqual(0, result);

        }

        [TestMethod]
        public void MeanSquareError_005()
        {
            /**********************************
             * 考察输入序列不登场的情况
             * *******************************/

            double[] x = { 1, 2, 3, 4 };
            double[] y = { 1.1, 1.9, 3.2, 4.1, 5 };
            double result;

            result = 0;
            try
            {
                result = ProbabilityStatistics.ProbabilityStatistics.MeanSquaredError(x, y);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }
        }

    }

    [TestClass]
    public class RootMeanSquareUnitTest
    {
        [TestMethod]
        public void RootMeanSquare_001()
        {
            /**********************************
             * 考察计算结果是否正确
             * *******************************/

            double[] x = { 1, 2, 3, 4, 5 };
            double result;

            result = 0;
            result = ProbabilityStatistics.ProbabilityStatistics.RootMeanSquare(x);
            double expected = Math.Pow(11, 0.5);

            Assert.AreEqual(expected, result, 0.0000001);           
        }

        [TestMethod]
        public void RootMeanSquare_002()
        {
            /**********************************
             * 考察输入为null是否报错
             * *******************************/

            double[] x = null;
            double result;

            try
            {
                result = ProbabilityStatistics.ProbabilityStatistics.RootMeanSquare(x);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }
        }

        [TestMethod]
        public void RootMeanSquare_003()
        {
            /**********************************
             * 考察输入为空是否报错
             * *******************************/

            double[] x = new double[] { };
            double result;

            try
            {
                result = ProbabilityStatistics.ProbabilityStatistics.RootMeanSquare(x);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }
        }

        [TestMethod]
        public void RootMeanSquare_004()
        {
            /**********************************
             * 输入序列为一个元素考察计算结果是否正确
             * *******************************/

            double[] x = { 3 };
            double result;

            result = 0;
            result = ProbabilityStatistics.ProbabilityStatistics.RootMeanSquare(x);

            Assert.AreEqual(3, result);

        }
    }

    [TestClass]
    public class VarianceUnitTest
    {
        [TestMethod]
        public void Variance_001()
        {
            /**********************************
             * 考察计算结果是否正确
             * *******************************/

            double[] x = { 1, 2, 3, 4, 5 };
            double result;

            result = 0;
            result = ProbabilityStatistics.ProbabilityStatistics.Variance(x);
            double expected = 2;

            Assert.AreEqual(expected, result, 0.0000001);
        }

        [TestMethod]
        public void Variance_002()
        {
            /**********************************
             * 考察输入为null是否报错
             * *******************************/

            double[] x = null;
            double result;

            try
            {
                result = ProbabilityStatistics.ProbabilityStatistics.Variance(x);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }
        }

        [TestMethod]
        public void Variance_003()
        {
            /**********************************
             * 考察输入为空是否报错
             * *******************************/

            double[] x = new double[] { };
            double result;

            try
            {
                result = ProbabilityStatistics.ProbabilityStatistics.Variance(x);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }
        }

        [TestMethod]
        public void Variance_004()
        {
            /**********************************
             * 输入序列为一个元素考察计算结果是否正确
             * *******************************/

            double[] x = { 3 };
            double result;

            result = 0;
            result = ProbabilityStatistics.ProbabilityStatistics.Variance(x);

            Assert.AreEqual(0, result);

        }
    }

    [TestClass]
    public class StandardDeviationUnitTest
    {
        [TestMethod]
        public void StandardDeviation_001()
        {
            /**********************************
             * 考察计算结果是否正确
             * *******************************/

            double[] x = { 1, 2, 3, 4, 5 };
            double result;
            result = 0;
            result = ProbabilityStatistics.ProbabilityStatistics.StandardDeviation(x);
            double expected = Math.Pow(2,0.5);
            
            Assert.AreEqual(expected, result, 0.0000001);
        }

        [TestMethod]
        public void StandardDeviation_002()
        {
            /**********************************
             * 考察输入为null是否报错
             * *******************************/

            double[] x = null;
            double result;

            try
            {
                result = ProbabilityStatistics.ProbabilityStatistics.StandardDeviation(x);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }
        }

        [TestMethod]
        public void StandardDeviation_003()
        {
            /**********************************
             * 考察输入为空是否报错
             * *******************************/

            double[] x = new double[] { };
            double result;

            try
            {
                result = ProbabilityStatistics.ProbabilityStatistics.StandardDeviation(x);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }
        }

        [TestMethod]
        public void StandardDeviation_004()
        {
            /**********************************
             * 输入序列为一个元素考察计算结果是否正确
             * *******************************/

            double[] x = { 3 };
            double result;

            result = 0;
            result = ProbabilityStatistics.ProbabilityStatistics.StandardDeviation(x);

            Assert.AreEqual(0, result);

        }
    }

    [TestClass]
    public class HistgramUnitTest
    {
        [TestMethod()]        
        public void HstogramTest_001()
        {
            /**********************************
             * 考察计算结果是否正确
             * *******************************/

            int nbrOfIntervals = 8;
            double[] data = new double[] { 1, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7, 8, 8, 9 };
            int[] histgram = new int[nbrOfIntervals];
            double[] intervals = new double[nbrOfIntervals];
            double[] expectedHistgram = { 1, 2, 3, 3, 3, 3, 3, 3 };                 //LabVIEW计算所得
            double[] expectedIntervals = { 1.5, 2.5, 3.5, 4.5, 5.5, 6.5, 7.5, 8.5 };//LabVIEW计算所得
            ProbabilityStatistics.ProbabilityStatistics.Histogram(data, ref histgram, ref intervals);
            for (int i = 0; i < nbrOfIntervals; i++)
            {
                if (histgram[i] == expectedHistgram[i] && intervals[i] == expectedIntervals[i])
                {
                }
                else
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod()]       
        public void HstogramTest_002()
        {
            /**********************************
             * 考察输入为null是否报错
             * *******************************/
            int nbrOfIntervals = 8;
            double[] data = null;
            int[] histgram = new int[nbrOfIntervals];
            double[] intervals = new double[nbrOfIntervals];
            
            try
            {
                ProbabilityStatistics.ProbabilityStatistics.Histogram(data,ref histgram, ref intervals);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }   
        }

        [TestMethod()]
        public void HstogramTest_003()
        {
            /**********************************
             * 考察输入为空是否报错
             * *******************************/

            int nbrOfIntervals = 8;
            double[] data = new double[] { };
            int[] histgram = new int[nbrOfIntervals];
            double[] intervals = new double[nbrOfIntervals];

            try
            {
                ProbabilityStatistics.ProbabilityStatistics.Histogram(data, ref histgram, ref intervals);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }
        }
                
        [TestMethod()]
        public void HstogramTest_005()
        {
            double[] data = new double[] { 1 };
            int nbrOfIntervals = 4;
            int[] histgram = new int[nbrOfIntervals];
            double[] intervals = new double[nbrOfIntervals];
            double[] expectedHistgram = { 0, 0, 0,1 }; //LabVIEW计算所得
            double[] expectedIntervals = { 1, 1, 1, 1 };//LabVIEW计算所得

            ProbabilityStatistics.ProbabilityStatistics.Histogram(data,ref histgram, ref intervals);

            for (int i = 0; i < nbrOfIntervals; i++)
            {
                if (histgram[i] == expectedHistgram[i] && intervals[i] == expectedIntervals[i])
                {
                }
                else
                {
                    Assert.Fail();
                }
            }
        }
    } 
}
