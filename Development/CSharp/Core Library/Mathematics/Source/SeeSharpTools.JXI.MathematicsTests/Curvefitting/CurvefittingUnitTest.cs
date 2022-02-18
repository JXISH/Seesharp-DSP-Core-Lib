/// 内容摘要: 曲线拟合覆盖测试
/// 包括线性拟合、指数拟合以及多项式拟合覆盖测试
/// 完成日期: 2017年8月4日
/// 版    本:Version 1
/// 作    者:谢晓姣
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeeSharpTools.JXI.Mathematics;
using SeeSharpTools.JXI.Exception;

namespace SeeSharpTools.JXI.MathematicsTests.Curvefitting
{
    [TestClass]
    public class LinearFittingUnitTest
    {
        #region 线性拟合测试用例
        [TestMethod]
        public void LinearFitting_001()
        {
            /**********************************
             * 线性拟合
             * 输入等长数据
             * *******************************/
            try
            {
                double[] x = { 1, 2, 3, 4, 5 };
                double[] y = { 3, 5, 7, 9, 11 };
                double slop = 0;
                double interception = 0;
                double[] data = new double[5];
                EasyCurveFitting.LinearFit(x, y, x, ref data, out slop, out interception);
                Assert.IsTrue(slop == 2 & interception == 1);
            }
            catch (JXIParamException )
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void LinearFitting_002()
        {
            /**********************************
             * 线性拟合
             * x的数据长度大于y的数据长度
             * *******************************/
            try
            {
                double[] x = { 1, 2, 3, 4, 5, 6 };
                double[] y = { 3, 5, 7, 9, 11 };
                double slop = 0;
                double interception = 0;
                double[] data = new double[5];
                EasyCurveFitting.LinearFit(x, y, x, ref data, out slop, out interception);
                Assert.IsTrue(slop == 2 & interception == 1);
            }
            catch (JXIParamException )
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void LinearFitting_003()
        {
            /**********************************
             * 线性拟合
             * x的数据长度小于y的数据长度
             * *******************************/
            try
            {
                double[] x = { 1, 2, 3, 4, 5 };
                double[] y = { 3, 5, 7, 9, 11, 13 };
                double slop = 0;
                double interception = 0;
                double[] data = new double[5];
                EasyCurveFitting.LinearFit(x, y, x, ref data, out slop, out interception);
                Assert.IsTrue(slop == 2 & interception == 1);
            }
            catch (JXIParamException )
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void LinearFitting_004()
        {
            /**********************************
             * 线性拟合
             * y为空数组
             * *******************************/
            try
            {
                double[] x = { 1, 2, 3, 4, 5 };
                double[] y = { };
                double slop = 0;
                double interception = 0;
                double[] data = new double[5];
                EasyCurveFitting.LinearFit(x, y, x, ref data, out slop, out interception);
                Assert.Fail();
            }
            catch (JXIParamException ex)
            {
                Assert.AreEqual(ex.Message, "数组为空");
            }
        }

        [TestMethod]
        public void LinearFitting_005()
        {
            /**********************************
             * 线性拟合
             * x为空数组
             * *******************************/
            try
            {
                double[] x = { };
                double[] y = { 3, 5, 7, 9, 11 };
                double slop = 0;
                double interception = 0;
                double[] data = new double[5];
                EasyCurveFitting.LinearFit(x, y, x, ref data, out slop, out interception);
                Assert.Fail();
            }
            catch (JXIParamException ex)
            {
                Assert.AreEqual(ex.Message, "数组为空");
            }
        }

        [TestMethod]
        public void LinearFitting_006()
        {
            /**********************************
             * 线性拟合
             * 只输入一个点
             * *******************************/
            try
            {
                double[] x = { 1 };
                double[] y = { 3, 4 };
                double slop = 0;
                double interception = 0;
                double[] data = new double[5];
                EasyCurveFitting.LinearFit(x, y, x, ref data, out slop, out interception);
                Assert.Fail();
            }
            catch (JXIParamException ex)
            {
                Assert.AreEqual(ex.Message, "输入数据的长度至少为2");
            }
        }

        [TestMethod]
        public void LinearFitting_007()
        {
            /**********************************
             * 线性拟合
             * 数据等长，拟合两次
             * *******************************/
            try
            {
                double[] x = { 1, 2, 3, 4, 5 };
                double[] y = { 3, 5, 7, 9, 11 };
                double slop = 0;
                double interception = 0;
                double[] data = new double[5];
                EasyCurveFitting.LinearFit(x, y, x, ref data, out slop, out interception);
                Assert.IsTrue(slop == 2 & interception == 1);
                EasyCurveFitting.LinearFit(x, y, x, ref data, out slop, out interception);
                Assert.IsTrue(slop == 2 & interception == 1);
            }
            catch (JXIParamException )
            {
             Assert.Fail();    
            }
        }

        [TestMethod]
        public void LinearFitting_008()
        {
            /**********************************
             * 线性拟合
             * 数据不等长，拟合两次
             * *******************************/
            try
            {
                double[] x = { 1, 2, 3, 4, 5,6};
                double[] y = { 3, 5, 7, 9, 11 };
                double slop = 0;
                double interception = 0;
                double[] data = new double[5];
                EasyCurveFitting.LinearFit(x, y, x, ref data, out slop, out interception);
                Assert.IsTrue(slop == 2 & interception == 1);
                EasyCurveFitting.LinearFit(x, y, x, ref data, out slop, out interception);
                Assert.IsTrue(slop == 2 & interception == 1);
            }
            catch (JXIParamException )
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void LinearFitting_009()
        {
            /**********************************
             * 线性拟合
             * 数据不等长
             * 拟合两次
             * 创建拟合数组长度大于有效数据长度
             * *******************************/
            try
            {
                double[] x = { 1, 2, 3, 4, 5, 6 };
                double[] y = { 3, 5, 7, 9, 11 };
                double slop = 0;
                double interception = 0;
                double[] data = new double[6];
                EasyCurveFitting.LinearFit(x, y, x, ref data, out slop, out interception);
                Assert.IsTrue(slop == 2 & interception == 1);
                EasyCurveFitting.LinearFit(x, y, x, ref data, out slop, out interception);
                Assert.IsTrue(slop == 2 & interception == 1);
            }
            catch (JXIParamException )
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void LinearFitting_010()
        {
            /**********************************
             * 线性拟合
             * 数据不等长
             * 拟合两次
             * 创建拟合数组长度小于有效数据长度
             * *******************************/
            try
            {
                double[] x = { 1, 2, 3, 4, 5, 6 };
                double[] y = { 3, 5, 7, 9, 11 };
                double slop = 0;
                double interception = 0;
                double[] data = new double[4];
                EasyCurveFitting.LinearFit(x, y, x, ref data, out slop, out interception);
                Assert.IsTrue(slop == 2 & interception == 1);
                EasyCurveFitting.LinearFit(x, y, x, ref data, out slop, out interception);
                Assert.IsTrue(slop == 2 & interception == 1);
            }
            catch (JXIParamException )
            {
                Assert.Fail();
            }
        }

        #endregion
    }

    [TestClass]
    public class ExponentialFittingUnitTest
    {
        #region 指数拟合测试用例
        [TestMethod]
        public void ExponentialFitting_001()
        {
            /**********************************
             * 指数拟合
             * 输入等长数据
             * *******************************/
            try
            {
                double[] x = { 1, 2, 3, 4, 5 };
                double[] y = new double[5];
                for (int i = 0; i < 5; i++)
                {
                    y[i] = System.Math.Exp(1.7 * x[i]) * (2.6);
                }
                double amplitude = 0;
                double damping = 0;
                double offset = 0;
                double[] data = new double[5];
                EasyCurveFitting.ExponentialFit(x, y, x, ref data, out amplitude, out damping, out offset);
                Assert.IsTrue(System.Math.Abs(amplitude - 2.6) < 0.001 & System.Math.Abs(damping - 1.7) < 0.001);
            }
            catch (JXIParamException )
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ExponentialFitting_002()
        {
            /**********************************
             * 指数拟合
             * 输入不等长数据
             * *******************************/
            try
            {
                double[] x = { 1, 2, 3, 4 };
                double[] y = { 14.23226321849072, 77.906660123232228, 426.45695897974446, 2334.4029582910857, 12778.39898477775 };
                double amplitude = 0;
                double damping = 0;
                double offset = 0;
                double[] data = new double[5];
                EasyCurveFitting.ExponentialFit(x, y, x, ref data, out amplitude, out damping, out offset);
                Assert.IsTrue(System.Math.Abs(amplitude - 2.6) < 0.001 & System.Math.Abs(damping - 1.7) < 0.001);
            }
            catch (JXIParamException )
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ExponentialFitting_003()
        {
            /**********************************
             * 指数拟合
             * 输入空数组
             * *******************************/
            try
            {
                double[] x = { };
                double[] y = { 14.23226321849072, 77.906660123232228, 426.45695897974446, 2334.4029582910857, 12778.39898477775 };
                double amplitude = 0;
                double damping = 0;
                double offset = 0;
                double[] data = new double[5];
                EasyCurveFitting.ExponentialFit(x, y, x, ref data, out amplitude, out damping, out offset);
                Assert.Fail();
            }
            catch (JXIParamException ex)
            {
                Assert.AreEqual(ex.Message, "数组为空");
            }
        }

        [TestMethod]
        public void ExponentialFitting_004()
        {
            /**********************************
             * 指数拟合
             * 输入点数小于2
             * *******************************/
            try
            {
                double[] x = { 1 };
                double[] y = { 14.23226321849072, 77.906660123232228, 426.45695897974446, 2334.4029582910857, 12778.39898477775 };
                double amplitude = 0;
                double damping = 0;
                double offset = 0;
                double[] data = new double[5];
                EasyCurveFitting.ExponentialFit(x, y, x, ref data, out amplitude, out damping, out offset);
                Assert.Fail();
            }
            catch (JXIParamException ex)
            {
                Assert.AreEqual(ex.Message, "输入数据的长度至少为2");
            }
        }

        [TestMethod]
        public void ExponentialFitting_005()
        {
            /**********************************
             * 指数拟合
             * y数组中存在0值
             * *******************************/
            try
            {
                double[] x = { 1, 2, 3, 4, 5 };
                double[] y = { 0, 77.906660123232228, 426.45695897974446, 2334.4029582910857, 12778.39898477775 };
                double amplitude = 0;
                double damping = 0;
                double offset = 0;
                double[] data = new double[5];
                EasyCurveFitting.ExponentialFit(x, y, x, ref data, out amplitude, out damping, out offset);
            }
            catch (JXIParamException )
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public void ExponentialFitting_006()
        {
            /**********************************
             * 指数拟合
             * 输入等长数据
             * 拟合两次
             * *******************************/
            try
            {
                double[] x = { 1, 2, 3, 4, 5 };
                double[] y = new double[5];
                for (int i = 0; i < 5; i++)
                {
                    y[i] = System.Math.Exp(1.7 * x[i]) * 2.6;
                }
                double amplitude = 0;
                double damping = 0;
                double offset = 0;
                double[] data = new double[5];
                EasyCurveFitting.ExponentialFit(x, y, x, ref data, out amplitude, out damping, out offset);
                Assert.IsTrue(System.Math.Abs(amplitude - 2.6)/2.6 < 0.001 & System.Math.Abs(damping - 1.7)/ 1.7 < 0.001);
                EasyCurveFitting.ExponentialFit(x, y, x, ref data, out amplitude, out damping, out offset);
                Assert.IsTrue(System.Math.Abs(amplitude - 2.6) / 2.6 < 0.001 & System.Math.Abs(damping - 1.7)/ 1.7 < 0.001);
            }
            catch (JXIParamException )
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ExponentialFitting_007()
        {
            /**********************************
             * 指数拟合
             * 输入不等长数据
             * 拟合两次
             * *******************************/
            try
            {
                double[] x = { 1, 2, 3, 4, 5, 6 };
                double[] y = new double[5];
                for (int i = 0; i < 5; i++)
                {
                    y[i] = System.Math.Exp(1.7 * x[i]) * 2.6;
                }
                double amplitude = 0;
                double damping = 0;
                double offset = 0;
                double[] data = new double[5];
                EasyCurveFitting.ExponentialFit(x, y, x, ref data, out amplitude, out damping, out offset);
                Assert.IsTrue(System.Math.Abs(amplitude - 2.6) < 0.001 & System.Math.Abs(damping - 1.7) < 0.001);
                EasyCurveFitting.ExponentialFit(x, y, x, ref data, out amplitude, out damping, out offset);
                Assert.IsTrue(System.Math.Abs(amplitude - 2.6) < 0.001 & System.Math.Abs(damping - 1.7) < 0.001);
            }
            catch (JXIParamException )
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ExponentialFitting_008()
        {
            /**********************************
             * 指数拟合
             * 输入不等长数据
             * 拟合两次
             * 创建拟合数组长度大于有效数据长度
             * *******************************/
            try
            {
                double[] x = { 1, 2, 3, 4, 5, 6 };
                double[] y = new double[5];
                for (int i = 0; i < 5; i++)
                {
                    y[i] = System.Math.Exp(1.7 * x[i]) * 2.6;
                }
                double amplitude = 0;
                double damping = 0;
                double offset = 0;
                double[] data = new double[7];
                EasyCurveFitting.ExponentialFit(x, y, x, ref data, out amplitude, out damping, out offset);
                Assert.IsTrue(System.Math.Abs(amplitude - 2.6) < 0.001 & System.Math.Abs(damping - 1.7) < 0.001);
                EasyCurveFitting.ExponentialFit(x, y, x, ref data, out amplitude, out damping, out offset);
                Assert.IsTrue(System.Math.Abs(amplitude - 2.6) < 0.001 & System.Math.Abs(damping - 1.7) < 0.001);
            }
            catch (JXIParamException )
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ExponentialFitting_009()
        {
            /**********************************
             * 指数拟合
             * 输入不等长数据
             * 拟合两次
             * 创建拟合数组长度小于有效数据长度
             * *******************************/
            try
            {
                double[] x = { 1, 2, 3, 4, 5, 6 };
                double[] y = new double[5];
                for (int i = 0; i < 5; i++)
                {
                    y[i] = System.Math.Exp(1.7 * x[i]) * 2.6;
                }
                double amplitude = 0;
                double damping = 0;
                double offset = 0;
                double[] data = new double[4];
                EasyCurveFitting.ExponentialFit(x, y, x, ref data, out amplitude, out damping, out offset);
                Assert.IsTrue(System.Math.Abs(amplitude - 2.6) < 0.001 & System.Math.Abs(damping - 1.7) < 0.001);
                EasyCurveFitting.ExponentialFit(x, y, x, ref data, out amplitude, out damping, out offset);
                Assert.IsTrue(System.Math.Abs(amplitude - 2.6) < 0.001 & System.Math.Abs(damping - 1.7) < 0.001);
            }
            catch (JXIParamException )
            {
                Assert.Fail();
            }
        }
        #endregion
    }

    [TestClass]
    public class PolynomialFittingUnitTest
    { 
     #region 多项式拟合测试用例
        [TestMethod]
        public void PolynomialFitting_001()
        {
            /**********************************
             * 多项式拟合
             * 输入等长数据
             * *******************************/
            try
            {
                double[] x = { 1, 2, 3, 4, 5 };
                double[] y = new double[5];
                for (int i = 0; i < 5; i++)
                {
                    y[i] = 10 + -10 * x[i]  -5 * System.Math.Pow(x[i], 2) -4 * System.Math.Pow(x[i], 3);
                }
                double[] coe = new double[5];
                double[] data = new double[5];
                int order = 3;
                EasyCurveFitting.PolynomialFit(x, y, order, x, ref data, ref coe);
                Assert.IsTrue(System.Math.Abs(coe[0] - 10)/10 < 0.001);
                Assert.IsTrue(System.Math.Abs(coe[1] + 10) / 10 < 0.001);
                Assert.IsTrue(System.Math.Abs(coe[2] + 5) / 5 < 0.001);
                Assert.IsTrue(System.Math.Abs(coe[3] + 4) / 4 < 0.001);
            }
            catch (JXIParamException )
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void PolynomialFitting_002()
        {
            /**********************************
             * 多项式拟合
             * 输入不等长数据
             * *******************************/
            try
            {
                double[] x = { 1, 2, 3, 4, 5, 6 };
                double[] y = new double[5];
                for (int i = 0; i < 5; i++)
                {
                    y[i] = 1 + 2 * x[i] + 3 * System.Math.Pow(x[i], 2) + 4 * System.Math.Pow(x[i], 3);
                }
                double[] coe = new double[5];
                double[] data = new double[5];
                int order = 3;
                EasyCurveFitting.PolynomialFit(x, y, order, x, ref data, ref coe);
                for (int i = 0; i < order; i++)
                {
                    Assert.IsTrue(System.Math.Abs(coe[i] - 1 - i) < 0.001);
                }
            }
            catch (JXIParamException )
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void PolynomialFitting_003()
        {
            /**********************************
             * 多项式拟合
             * 阶数大于输入点数
             * *******************************/
            try
            {
                double[] x = { 1, 2 };
                double[] y = new double[2];
                for (int i = 0; i < 2; i++)
                {
                    y[i] = 1 + 2 * x[i] + 3 * System.Math.Pow(x[i], 2) + 4 * System.Math.Pow(x[i], 3);
                }
                double[] coe = new double[3];
                double[] data = new double[2];
                int order = 3;
                EasyCurveFitting.PolynomialFit(x, y, order, x, ref data, ref coe);
                Assert.Fail();
            }
            catch (JXIParamException ex)
            {
                Assert.AreEqual(ex.Message, "有效数据长度必须大于阶数");
            }
        }

        [TestMethod]
        public void PolynomialFitting_004()
        {
            /**********************************
             * 多项式拟合
             * 输入空数组
             * *******************************/
            try
            {
                double[] x = { 1, 2, 3, 4, 5 };
                double[] y = { };
                double[] coe = new double[5];
                double[] data = new double[5];
                int order = 3;
                EasyCurveFitting.PolynomialFit(x, y, order, x, ref data, ref coe);
            }
            catch (JXIParamException ex)
            {
                Assert.AreEqual(ex.Message, "数组为空");
            }
        }

        [TestMethod]
        public void PolynomialFitting_005()
        {
            /**********************************
             * 多项式拟合
             * 输入等长数据
             * 拟合两次
             * *******************************/
            try
            {
                double[] x = { 1, 2, 3, 4, 5 };
                double[] y = new double[5];
                for (int i = 0; i < 5; i++)
                {
                    y[i] = 1 + 2 * x[i] + 3 * System.Math.Pow(x[i], 2) + 4 * System.Math.Pow(x[i], 3);
                }
                double[] coe = new double[5];
                double[] data = new double[5];
                int order = 3;
                EasyCurveFitting.PolynomialFit(x, y, order, x, ref data, ref coe);
                for (int i = 0; i < order; i++)
                {
                    Assert.IsTrue(System.Math.Abs(coe[i] - 1 - i) < 0.001);
                }
                EasyCurveFitting.PolynomialFit(x, y, order, x, ref data, ref coe);
                for (int i = 0; i < order; i++)
                {
                    Assert.IsTrue(System.Math.Abs(coe[i] - 1 - i) < 0.001);
                }
            }
            catch (JXIParamException )
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void PolynomialFitting_006()
        {
            /**********************************
             * 多项式拟合
             * 输入不等长数据
             * 拟合两次
             * *******************************/
            try
            {
                double[] x = { 1, 2, 3, 4, 5 ,6};
                double[] y = new double[5];
                for (int i = 0; i < 5; i++)
                {
                    y[i] = 1 + 2 * x[i] + 3 * System.Math.Pow(x[i], 2) + 4 * System.Math.Pow(x[i], 3);
                }
                double[] coe = new double[5];
                double[] data = new double[5];
                int order = 3;
                EasyCurveFitting.PolynomialFit(x, y, order, x, ref data, ref coe);
                for (int i = 0; i < order; i++)
                {
                    Assert.IsTrue(System.Math.Abs(coe[i] - 1 - i) < 0.001);
                }
                EasyCurveFitting.PolynomialFit(x, y, order, x, ref data, ref coe);
                for (int i = 0; i < order; i++)
                {
                    Assert.IsTrue(System.Math.Abs(coe[i] - 1 - i) < 0.001);
                }
            }
            catch (JXIParamException )
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void PolynomialFitting_007()
        {
            /**********************************
             * 多项式拟合
             * 输入不等长数据
             * 拟合两次
             * 创建拟合数组长度大于有效数据长度
             * *******************************/
            try
            {
                double[] x = { 1, 2, 3, 4, 5, 6 };
                double[] y = new double[5];
                for (int i = 0; i < 5; i++)
                {
                    y[i] = 1 + 2 * x[i] + 3 * System.Math.Pow(x[i], 2) + 4 * System.Math.Pow(x[i], 3);
                }
                double[] coe = new double[7];
                double[] data = new double[6];
                int order = 3;
                EasyCurveFitting.PolynomialFit(x, y, order, x, ref data, ref coe);
                for (int i = 0; i < order; i++)
                {
                    Assert.IsTrue(System.Math.Abs(coe[i] - 1 - i) < 0.001);
                }
                EasyCurveFitting.PolynomialFit(x, y, order, x, ref data, ref coe);
                for (int i = 0; i < order; i++)
                {
                    Assert.IsTrue(System.Math.Abs(coe[i] - 1 - i) < 0.001);
                }
            }
            catch (JXIParamException )
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void PolynomialFitting_008()
        {
            /**********************************
             * 多项式拟合
             * 输入不等长数据
             * 拟合两次
             * 创建拟合数组长度小于有效数据长度
             * *******************************/
            try
            {
                double[] x = { 1, 2, 3, 4, 5, 6 };
                double[] y = new double[5];
                for (int i = 0; i < 5; i++)
                {
                    y[i] = 1 + 2 * x[i] + 3 * System.Math.Pow(x[i], 2) + 4 * System.Math.Pow(x[i], 3);
                }
                double[] coe = new double[7];
                double[] data = new double[4];
                int order = 3;
                EasyCurveFitting.PolynomialFit(x, y, order, x, ref data, ref coe);
                for (int i = 0; i < order; i++)
                {
                    Assert.IsTrue(System.Math.Abs(coe[i] - 1 - i) < 0.001);
                }
                EasyCurveFitting.PolynomialFit(x, y, order, x, ref data, ref coe);
                for (int i = 0; i < order; i++)
                {
                    Assert.IsTrue(System.Math.Abs(coe[i] - 1 - i) < 0.001);
                }
            }
            catch (JXIParamException )
            {
                Assert.Fail();
            }
        }
        #endregion
    }
}
