using System;
using System.Linq;
using System.Reflection;
using SeeSharpTools.JXI.MKL;
using SeeSharpTools.JXI.Exception;

namespace SeeSharpTools.JXI.Mathematics
{
    /// <summary>
    /// CurveFitting Class
    /// </summary>
    public static class EasyCurveFitting
    {
        static EasyCurveFitting()
        {
            //利用反射获取版本号
            //Assembly fileAssembly = Assembly.GetExecutingAssembly();
            //var fileVersion = fileAssembly.GetName().Version.ToString();//获取版本号
            //var firstPointIndex = fileVersion.IndexOf('.');
            //var secondPointIndex = fileVersion.Substring(firstPointIndex + 1, fileVersion.Length - firstPointIndex - 1).IndexOf('.');
            //string strProduct = "SeeSharpTools.JXI.Mathematics";
            //string strVersion = fileVersion.Substring(0, firstPointIndex + 1 + secondPointIndex);//获取大版本号1.0
            //var status = LicenseManager.LicenseManager.GetActivationStatus(strProduct, strVersion);
            //if (status < 0)
            //{
            //    LicenseManager.ProductLicenseManager.GetActivationStatus(strProduct, strVersion);
            //}
        }
            
        /// <summary>
        /// Calculate the linear fit of a pair (intersetX, fittedY) based on known data sequence (x,y) using the Least Square method.
        /// </summary>
        /// <param name="x"> x sequence of the known data set (x,y).</param>
        /// <param name="y"> y sequence of the known data set (x,y).</param>
        /// <param name="interestX">x of interest.</param>
        /// <param name="fittedY">Fitted y value. </param>
        public static void LinearFit(double[] x, double[] y, double interestX, out double fittedY)
        {
            #region 验证输入数据与参数是否合法，非法则抛出异常
            //检查输入数据是否为空
            if (x.Length == 0 || y.Length == 0)
            {
                //第一种处理: x或y为空
                throw new JXIParamException("数组为空");
            }
                FittingCommon.Equilong(ref x, ref y);//确保x、y的长度一致
            //检查输入数据长度是否至少为2个点
            if (x.Length <= 1)
            {
                throw new JXIParamException("输入数据的长度至少为2");
            }
            #endregion

            #region 定义变量
            int _dataLenth = x.Length;//数据长度
            double slope = 0.0;//斜率
            double intercept = 0.0;//截距
            #endregion

            #region 最小二乘法求解
            // 第一步求均值
            double mx = 0.0;
            FittingCommon.SumOfArray(x, _dataLenth, out mx);
            mx /= _dataLenth;
            double my = 0.0;
            FittingCommon.SumOfArray(y, _dataLenth, out my);
            my /= _dataLenth;

            // 第二步求斜率和截距
            double covariance = 0.0;
            double variance = 0.0;
            for (int i = 0; i < _dataLenth; i++)
            {
                double diff = x[i] - mx;
                covariance += diff * (y[i] - my);
                variance += diff * diff;
            }
            var b = covariance / variance;
            intercept = my - b * mx;
            slope = b;
            #endregion

            #region 计算拟合后的数据
            fittedY = slope * interestX + intercept;
            #endregion
        }

        /// <summary>
        /// Calculate the linear fit of a pair (intersetX, fittedY) based on known data sequence (x,y) using the Least Square method.
        /// </summary>
        /// <param name="x"> x sequence of the known data set (x,y).</param>
        /// <param name="y"> y sequence of the known data set (x,y).</param>
        /// <param name="interestX">x of interest.</param>
        /// <param name="fittedY">Fitted y value. </param>
        /// <param name="slope"> Slope of the fitted model.</param>
        /// <param name="intercept"> Intercept of the fitted model.</param>
        public static void LinearFit(double[] x, double[] y, double interestX, out double fittedY, out double slope, out double intercept)
        {

            #region 验证输入数据与参数是否合法，非法则抛出异常
            //检查输入数据是否为空
            if (x.Length == 0 || y.Length == 0)
            {
                //第一种处理: x或y为空
                throw new JXIParamException("数组为空");
            }
            FittingCommon.Equilong(ref x, ref y);//确保x、y的长度一致
            //检查输入数据长度是否至少为2个点
            if (x.Length <= 1)
            {
                throw new JXIParamException("输入数据的长度至少为2");
            }
            #endregion

            #region 定义变量
            int _dataLenth = x.Length;//数据长度
            #endregion

            #region 最小二乘法求解
            // 第一步求均值
            double mx = 0.0;
            FittingCommon.SumOfArray(x, _dataLenth, out mx);
            mx /= _dataLenth;
            double my = 0.0;
            FittingCommon.SumOfArray(y, _dataLenth, out my);
            my /= _dataLenth;

            // 第二步求斜率和截距
            double covariance = 0.0;
            double variance = 0.0;
            for (int i = 0; i < _dataLenth; i++)
            {
                double diff = x[i] - mx;
                covariance += diff * (y[i] - my);
                variance += diff * diff;
            }
            var b = covariance / variance;
            intercept = my - b * mx;
            slope = b;
            #endregion

            #region 计算拟合后的数据
            fittedY = slope * interestX + intercept;
            #endregion
        }

        /// <summary>
        /// Calculate the linear fit of a data set (intersetX, fittedY) based on known data sequence (x,y) using the Least Square method.
        /// </summary>
        /// <param name="x"> x sequence of the known data set (x,y).</param>
        /// <param name="y"> y sequence of the known data set (x,y).</param>
        /// <param name="interestX">x sequence of interest.</param>
        /// <param name="fittedY">Fitted y sequence corresponding to the intersetX seqeuence. </param>
        public static void LinearFit(double[] x, double[] y, double[] interestX, ref double[] fittedY)
        {
            #region 验证输入数据与参数是否合法，非法则抛出异常
            //检查输入数据是否为空
            if (x.Length == 0 || y.Length == 0)
            {
                //第一种处理: x或y为空
                throw new JXIParamException("数组为空");
            }
            FittingCommon.Equilong(ref x, ref y);//确保x、y的长度一致
            FittingCommon.Equilong(ref interestX, ref fittedY);//确保interestX、fittedY的长度一致
            //检查输入数据长度是否至少为2个点
            if (x.Length <= 1)
            {
                throw new JXIParamException("输入数据的长度至少为2");
            }
            #endregion

            #region 定义变量
            int _dataLenth = x.Length;//数据长度
            double slope = 0.0;//斜率
            double intercept = 0.0;//截距
            #endregion

            #region 最小二乘法求解
            // 第一步求均值
            double mx = 0.0;
            FittingCommon.SumOfArray(x, _dataLenth, out mx);
            mx /= _dataLenth;
            double my = 0.0;
            FittingCommon.SumOfArray(y, _dataLenth, out my);
            my /= _dataLenth;

            // 第二步求斜率和截距
            double covariance = 0.0;
            double variance = 0.0;
            for (int i = 0; i < _dataLenth; i++)
            {
                double diff = x[i] - mx;
                covariance += diff * (y[i] - my);
                variance += diff * diff;
            }
            var b = covariance / variance;
            intercept = my - b * mx;
            slope = b;
            #endregion

            #region 计算拟合后的数据

            for (int i = 0; i < interestX.Length; i++)
            {
                fittedY[i] = slope * interestX[i] + intercept;
            }
            #endregion
        }

        /// <summary>
        /// Calculate the linear fit of a data set (intersetX, fittedY) based on known data sequence (x,y) using the Least Square method.
        /// </summary>
        /// <param name="x"> x sequence of the known data set (x,y).</param>
        /// <param name="y"> y sequence of the known data set (x,y).</param>
        /// <param name="interestX">x sequence of interest.</param>
        /// <param name="fittedY">Fitted y sequence corresponding to the intersetX sequence. </param>
        /// <param name="slope"> Slope of the fitted model.</param>
        /// <param name="intercept"> Intercept of the fitted model.</param>
        public static void LinearFit(double[] x, double[] y, double[] interestX, ref double[] fittedY, out double slope, out double intercept)
        {
            #region 验证输入数据与参数是否合法，非法则抛出异常
            //检查输入数据是否为空
            if (x.Length == 0 || y.Length == 0)
            {
                //第一种处理: x或y为空
                throw new JXIParamException("数组为空");
            }
            FittingCommon.Equilong(ref x, ref y);//确保x、y的长度一致
            FittingCommon.Equilong(ref interestX, ref fittedY);//确保interestX、fittedY的长度一致
            //检查输入数据长度是否至少为2个点
            if (x.Length <= 1)
            {
                throw new JXIParamException("输入数据的长度至少为2");
            }
            #endregion

            #region 定义变量
            int _dataLenth = x.Length;//数据长度
            #endregion

            #region 最小二乘法求解
            // 第一步求均值
            double mx = 0.0;
            FittingCommon.SumOfArray(x, _dataLenth, out mx);
            mx /= _dataLenth;
            double my = 0.0;
            FittingCommon.SumOfArray(y, _dataLenth, out my);
            my /= _dataLenth;

            // 第二步求斜率和截距
            double covariance = 0.0;
            double variance = 0.0;
            for (int i = 0; i < _dataLenth; i++)
            {
                double diff = x[i] - mx;
                covariance += diff * (y[i] - my);
                variance += diff * diff;
            }
            var b = covariance / variance;
            intercept = my - b * mx;
            slope = b;
            #endregion

            #region 计算拟合后的数据
            for (int i = 0; i < interestX.Length; i++)
            {
                fittedY[i] = slope * interestX[i] + intercept;
            }
            #endregion
        }

        /// <summary>
        /// Calculate the exponential fit of a pair (intersetX, fittedY) based on known data sequence (x,y) using the Least Square method.
        /// </summary>
        /// <param name="x"> x sequence of the known data set (x,y).</param>
        /// <param name="y"> y sequence of the known data set (x,y).</param>
        /// <param name="interestX">x of interest.</param>
        /// <param name="fittedY">Fitted y value. </param>
        public static void ExponentialFit(double[] x, double[] y, double interestX, out double fittedY)
        {
            #region 验证输入数据与参数是否合法，非法则抛出异常

            //检查输入数据是否为空
            if (x.Length == 0 || y.Length == 0)
            {
                throw new JXIParamException("数组为空");
            }

            //检查输入数据长度是否至少为2个点
            if (x.Length <= 1)
            {
                throw new JXIParamException("输入数据的长度至少为2");
            }

            //检查y数组是否有负值
            double min = y.Min();
            if (min < 0)
            {
                throw new JXIParamException("y数组不能有负值");
            }
            FittingCommon.Equilong(ref x, ref y);//确保x、y的长度一致
            #endregion

            #region 定义变量
            int _dataLenth = x.Length;//数据长度
            double[] _data = new double[_dataLenth];//线性拟合后数组
            double[] lny = new double[_dataLenth];//预处理，将y求ln后的数组
            double amplitude = 0.0;//幅度
            double damping = 0.0;//指数
            double offset = 0.0;//偏移
            #endregion

            #region 最小二乘法求解
            y = FittingCommon.ZeroValidate(y);//数据预处理，数据y中不能有等于0的点，有则用接近0的非0数替代            
            VMLNative.vdLn(_dataLenth, y, lny);//对y求ln

            //调用线性拟合方法
            double intercept;
            double slope;
            LinearFit(x, lny, x, ref _data, out slope, out intercept);

            //求指数拟合参数
            amplitude = System.Math.Exp(intercept);//求指数
            damping = slope;
            offset = 0;
            #endregion

            #region 计算拟合后的数据
            fittedY = amplitude * System.Math.Exp(damping * interestX) + offset;
            #endregion
        }

        /// <summary>
        /// Calculate the exponential fit of a pair (intersetX, fittedY) based on known data sequence (x,y) using the Least Square method.
        /// </summary>
        /// <param name="x"> x sequence of the known data set (x,y).</param>
        /// <param name="y"> y sequence of the known data set (x,y).</param>
        /// <param name="interestX">x of interest.</param>
        /// <param name="fittedY">Fitted y value. </param>
        /// <param name="amplitude"> Amplitude of the fitted model.</param>
        /// <param name="damping"> Damping of the fitted model.</param>
        /// <param name="offset">> Offset of the fitted model.</param>
        public static void ExponentialFit(double[] x, double[] y, double interestX, out double fittedY, out double amplitude, out double damping, out double offset)
        {
            #region 验证输入数据与参数是否合法，非法则抛出异常

            //检查输入数据是否为空
            if (x.Length == 0 || y.Length == 0)
            {
                throw new JXIParamException("数组为空");
            }

            //检查输入数据长度是否至少为2个点
            if (x.Length <= 1)
            {
                throw new JXIParamException("输入数据的长度至少为2");
            }

            //检查y数组是否有负值
            double min = y.Min();
            if (min < 0)
            {
                throw new JXIParamException("y数组不能有负值");
            }
            FittingCommon.Equilong(ref x, ref y);//确保x、y的长度一致
            #endregion

            #region 定义变量
            int _dataLenth = x.Length;//数据长度
            double[] _data = new double[_dataLenth];//线性拟合后数组
            double[] lny = new double[_dataLenth];//预处理，将y求ln后的数组
            #endregion

            #region 最小二乘法求解
            y = FittingCommon.ZeroValidate(y);//数据预处理，数据y中不能有等于0的点，有则用接近0的非0数替代            
            VMLNative.vdLn(_dataLenth, y, lny);//对y求ln

            //调用线性拟合方法
            double intercept;
            double slope;
            LinearFit(x, lny, x, ref _data, out slope, out intercept);

            //求指数拟合参数
            amplitude = System.Math.Exp(intercept);//求指数
            damping = slope;
            offset = 0;
            #endregion

            #region 计算拟合后的数据
            fittedY = amplitude * System.Math.Exp(damping * interestX) + offset;
            #endregion
        }

        /// <summary>
        /// Calculate the exponential fit of a data set (intersetX, fittedY) based on known data sequence (x,y) using the Least Square method.
        /// </summary>
        /// <param name="x"> x sequence of the known data set (x,y).</param>
        /// <param name="y"> y sequence of the known data set (x,y).</param>
        /// <param name="interestX">x sequence of interest.</param>
        /// <param name="fittedY">Fitted y sequence corresponding to the intersetX seqeuence. </param>
        public static void ExponentialFit(double[] x, double[] y, double[] interestX, ref double[] fittedY)
        {
            #region 验证输入数据与参数是否合法，非法则抛出异常

            //检查输入数据是否为空
            if (x.Length == 0 || y.Length == 0)
            {
                throw new JXIParamException("数组为空");
            }

            //检查输入数据长度是否至少为2个点
            if (x.Length <= 1)
            {
                throw new JXIParamException("输入数据的长度至少为2");
            }

            //检查y数组是否有负值
            double min = y.Min();
            if (min < 0)
            {
                throw new JXIParamException("y数组不能有负值");
            }
            FittingCommon.Equilong(ref x, ref y);//确保x、y的长度一致
            FittingCommon.Equilong(ref interestX, ref fittedY);//确保interestX、fittedY的长度一致
            #endregion

            #region 定义变量
            int _dataLenth = x.Length;//数据长度
            double[] _data = new double[_dataLenth];//线性拟合后数组
            double[] lny = new double[_dataLenth];//预处理，将y求ln后的数组
            double amplitude = 0.0;//幅度
            double damping = 0.0;//指数
            double offset = 0.0;//偏移
            #endregion

            #region 最小二乘法求解
            y = FittingCommon.ZeroValidate(y);//数据预处理，数据y中不能有等于0的点，有则用接近0的非0数替代            
            VMLNative.vdLn(_dataLenth, y, lny);//对y求ln

            //调用线性拟合方法
            double intercept;
            double slope;
            LinearFit(x, lny, x, ref _data, out slope, out intercept);

            //求指数拟合参数
            amplitude = System.Math.Exp(intercept);//求指数
            damping = slope;
            offset = 0;
            #endregion

            #region 计算拟合后的数据
            for (int i = 0; i < interestX.Length; i++)
            {
                fittedY[i] = amplitude * System.Math.Exp(damping * interestX[i]) + offset;
            }           
            #endregion
        }

        /// <summary>
        /// Calculate the exponential fit of a data set (intersetX, fittedY) based on known data sequence (x,y) using the Least Square method.
        /// </summary>
        /// <param name="x"> x sequence of the known data set (x,y).</param>
        /// <param name="y"> y sequence of the known data set (x,y).</param>
        /// <param name="interestX">x sequence of interest.</param>
        /// <param name="fittedY">Fitted y sequence corresponding to the intersetX sequence. </param>
        /// <param name="amplitude"> Amplitude of the fitted model.</param>
        /// <param name="damping"> Damping of the fitted model.</param>
        /// <param name="offset">> Offset of the fitted model.</param>
        public static void ExponentialFit(double[] x, double[] y, double[] interestX, ref double[] fittedY, out double amplitude, out double damping, out double offset)
        {
            #region 验证输入数据与参数是否合法，非法则抛出异常

            //检查输入数据是否为空
            if (x.Length == 0 || y.Length == 0)
            {
                throw new JXIParamException("数组为空");
            }

            //检查输入数据长度是否至少为2个点
            if (x.Length <= 1)
            {
                throw new JXIParamException("输入数据的长度至少为2");
            }

            //检查y数组是否有负值
            double min = y.Min();
            if (min < 0)
            {
                throw new JXIParamException("y数组不能有负值");
            }
            FittingCommon.Equilong(ref x, ref y);//确保x、y的长度一致
            FittingCommon.Equilong(ref interestX, ref fittedY);//确保interestX、fittedY的长度一致
            #endregion

            #region 定义变量
            int _dataLenth = x.Length;//数据长度
            double[] _data = new double[_dataLenth];//线性拟合后数组
            double[] lny = new double[_dataLenth];//预处理，将y求ln后的数组
            #endregion

            #region 最小二乘法求解
            y = FittingCommon.ZeroValidate(y);//数据预处理，数据y中不能有等于0的点，有则用接近0的非0数替代            
            VMLNative.vdLn(_dataLenth, y, lny);//对y求ln

            //调用线性拟合方法
            double intercept;
            double slope;
            LinearFit(x, lny, x, ref _data, out slope, out intercept);

            //求指数拟合参数
            amplitude = System.Math.Exp(intercept);//求指数
            damping = slope;
            offset = 0;
            #endregion

            #region 计算拟合后的数据
            for (int i = 0; i < interestX.Length; i++)
            {
                fittedY[i] = amplitude * System.Math.Exp(damping * interestX[i]) + offset;
            }
            #endregion
        }

        /// <summary>
        /// Calculate the polynomial fit of a pair (intersetX, fittedY) based on known data sequence (x,y),
        /// using the Least Square method and Singular Value Decomposition (SVD) algorithm.
        /// </summary>
        /// <param name="x"> x sequence of the known data set (x,y).</param>
        /// <param name="y"> y sequence of the known data set (x,y).</param>
        /// <param name="order">Order of the polynomial that fits to the data sequence.</param>
        /// <param name="interestX">x of interest.</param>
        /// <param name="fittedY">Fitted y value. </param>
        public static void PolynomialFit(double[] x, double[] y, int order, double interestX, out double fittedY)
        {
            #region 验证输入数据与参数是否合法，非法则抛出异常
            if (x.Length == 0 || y.Length == 0)//输入空数组
            {
                throw new JXIParamException("数组为空");
            }

            //有效数据长度必须大于阶数
            if ((x.Length < y.Length ? x.Length : y.Length) <= order)
            {
                throw new JXIParamException("有效数据长度必须大于阶数");
            }
             FittingCommon.Equilong(ref x, ref y);
            #endregion

            #region 定义变量
            double [] a = new double[(order + 1) * (order + 1)];
            double [] b = new double[order + 1];
            double [] coefficients = new double[order + 1];
            #endregion

            #region 最小二乘法求解
            FittingCommon.GeneratedMatrix(x, y, order, a, b);//构造线性方程组的矩阵
            int info = LAPACK.LAPACKE_dgels(LAPACK.LAPACK_ROW_MAJOR, 'N', order + 1, order + 1, 1, a, order + 1, b, 1);//解线性方程组
            coefficients = b;
            #endregion

            #region 计算拟合后的数据
            fittedY = FittingCommon.CalculateY(interestX, coefficients);
            #endregion
        }

        /// <summary>
        /// Calculate the polynomial fit of a pair (intersetX, fittedY) based on known data sequence (x,y),
        /// using the Least Square method and Singular Value Decomposition (SVD) algorithm.
        /// </summary>
        /// <param name="x"> x sequence of the known data set (x,y).</param>
        /// <param name="y"> y sequence of the known data set (x,y).</param>
        /// <param name="order">Order of the polynomial that fits to the data sequence.</param>
        /// <param name="interestX">x of interest.</param>
        /// <param name="fittedY">Fitted y value. </param>
        /// <param name="coefficients">Coefficients of the fitted model in ascending order of power, its length must be "order + 1".</param>
        public static void PolynomialFit(double[] x, double[] y, int order, double interestX, out double fittedY, ref double[] coefficients)
        {
            #region 验证输入数据与参数是否合法，非法则抛出异常
            if (x.Length == 0 || y.Length == 0)//输入空数组
            {
                throw new JXIParamException("数组为空");
            }

            //有效数据长度必须大于阶数
            if ((x.Length < y.Length ? x.Length : y.Length) <= order)
            {
                throw new JXIParamException("有效数据长度必须大于阶数");
            }

            if (coefficients.Length< order + 1)//如果coefficients的长度小于order + 1
            {
                Array.Resize(ref coefficients, order + 1);
            }

            FittingCommon.Equilong(ref x, ref y);//确保x、y的长度一致
            #endregion

            #region 定义变量
            double[] a = new double[(order + 1) * (order + 1)];
            double[] b = new double[order + 1];
            #endregion

            #region 最小二乘法求解
            FittingCommon.GeneratedMatrix(x, y, order, a, b);//构造线性方程组的矩阵
            int info = LAPACK.LAPACKE_dgels(LAPACK.LAPACK_ROW_MAJOR, 'N', order + 1, order + 1, 1, a, order + 1, b, 1);//解线性方程组
            coefficients = b;
            #endregion

            #region 计算拟合后的数据
            fittedY = FittingCommon.CalculateY(interestX, coefficients);
            #endregion
        }

        /// <summary>
        /// Calculate the polynomial fit of a data set (intersetX, fittedY) based on known data sequence (x,y),
        /// using the Least Square method and Singular Value Decomposition (SVD) algorithm.
        /// </summary>
        /// <param name="x"> x sequence of the known data set (x,y).</param>
        /// <param name="y"> y sequence of the known data set (x,y).</param>
        /// <param name="order">Order of the polynomial that fits to the data sequence.</param>
        /// <param name="interestX">x sequence of interest.</param>
        /// <param name="fittedY">Fitted y sequence corresponding to the intersetX sequence. </param>
        public static void PolynomialFit(double[] x, double[] y, int order, double[] interestX, ref double[] fittedY)
        {
            #region 验证输入数据与参数是否合法，非法则抛出异常
            if (x.Length == 0 || y.Length == 0)//输入空数组
            {
                throw new JXIParamException("数组为空");
            }
            //有效数据长度必须大于阶数
            if ((x.Length < y.Length ? x.Length : y.Length) <= order)
            {
                throw new JXIParamException("有效数据长度必须大于阶数");
            }
            FittingCommon.Equilong(ref x, ref y);
            FittingCommon.Equilong(ref interestX, ref fittedY);
            #endregion

            #region 定义变量
            int _dataLenth = x.Length;
            double[] a = new double[(order + 1) * (order + 1)];
            double[] b = new double[order + 1];
            double[] coefficients = new double[order + 1];
            #endregion

            #region 最小二乘法求解
            FittingCommon.GeneratedMatrix(x, y, order, a, b);//构造线性方程组的矩阵
            int info = LAPACK.LAPACKE_dgels(LAPACK.LAPACK_ROW_MAJOR, 'N', order + 1, order + 1, 1, a, order + 1, b, 1);//解线性方程组
            coefficients = b;
            #endregion

            #region 计算拟合后的数据
            for (int i = 0; i < interestX.Length; i++)
            {
                fittedY[i] = FittingCommon.CalculateY(interestX[i], coefficients);
            }
            #endregion
        }

        /// <summary>
        /// Calculate the polynomial fit of a data set (intersetX, fittedY) based on known data sequence (x,y),
        /// using the Least Square method and Singular Value Decomposition (SVD) algorithm.
        /// </summary>
        /// <param name="x"> x sequence of the known data set (x,y).</param>
        /// <param name="y"> y sequence of the known data set (x,y).</param>
        /// <param name="order">Order of the polynomial that fits to the data sequence.</param>
        /// <param name="interestX">x sequence of interest.</param>
        /// <param name="fittedY">Fitted y sequence corresponding to the intersetX sequence. </param>
        /// <param name="coefficients">Coefficients of the fitted model in ascending order of power, its length must be "order + 1".</param>
        public static void PolynomialFit(double[] x, double[] y, int order, double[] interestX, ref double[] fittedY, ref double[] coefficients)
        {
            #region 验证输入数据与参数是否合法，非法则抛出异常
            if (x.Length == 0 || y.Length == 0)//输入空数组
            {
                throw new JXIParamException("数组为空");
            }
            //有效数据长度必须大于阶数
            if ((x.Length < y.Length ? x.Length : y.Length) <= order)
            {
                throw new JXIParamException("有效数据长度必须大于阶数");
            }
            if (coefficients.Length < order + 1)//如果coefficients的长度小于order + 1
            {
                Array.Resize(ref coefficients, order + 1);
            }
            FittingCommon.Equilong(ref x, ref y);
            FittingCommon.Equilong(ref interestX, ref fittedY);
            #endregion

            #region 定义变量
            int _dataLenth = x.Length;
            double[] a = new double[(order + 1) * (order + 1)];
            double[] b = new double[order + 1];
            #endregion

            #region 最小二乘法求解
            FittingCommon.GeneratedMatrix(x, y, order, a, b);//构造线性方程组的矩阵
            int info = LAPACK.LAPACKE_dgels(LAPACK.LAPACK_ROW_MAJOR, 'N', order + 1, order + 1, 1, a, order + 1, b, 1);//解线性方程组
            coefficients = b;
            #endregion

            #region 计算拟合后的数据
            for (int i = 0; i < interestX.Length; i++)
            {
                fittedY[i] = FittingCommon.CalculateY(interestX[i], coefficients);
            }
            #endregion
        }

    }
}
