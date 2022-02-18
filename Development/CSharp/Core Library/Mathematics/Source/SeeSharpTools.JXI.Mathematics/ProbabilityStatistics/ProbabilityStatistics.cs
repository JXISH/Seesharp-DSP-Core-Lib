using System;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// JXI.Mathematics
/// Author：JX Instrumentation.Inc
/// Release Time ：2017.09.19
/// Version：JXI.Mathematics 1.0.0
/// 
/// Chinese Simplified:
/// JXI.Mathematics
/// 作者：上海聚星仪器有限公司 
/// 修改日期：2017.09.04
/// 版本：JXI.Mathematics 1.0.0
/// </summary>
namespace SeeSharpTools.JXI.Mathematics.ProbabilityStatistics
{
    /// <summary>
    /// <para>Calculate the Mean, Median, Mode, StandardDeviation, Variance, MeanSquareError, RootMeanSquare and Creat the Histgram of the 1D array</para>
    /// <para>计算一维数组均值、中值、众数、标准差、方差、均方差、均方根,一维数组直方图统计</para>
    /// </summary>
    public static class ProbabilityStatistics
    {
        static ProbabilityStatistics()
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

        #region Public Methods

        /// <summary>
        /// 计算算术平均值,（x1+x2+...+xn）/n
        /// </summary>
        /// <param name="nums">待计算数组</param>
        /// <returns>算术平均值</returns>
        public static double Mean(double[] nums)
        {
            #region 参数列表条件判断
            //输入数组不能为空
            if (nums == null || nums.Length == 0)
            {
                throw new ArgumentException("Parameter Input Illegal");
            }
            #endregion
            double sum = 0;
            double avg = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
            }
            avg = sum / nums.Length;
            return avg;
        }

        /// <summary>
        /// 计算几何平均数：(x1*x2*...*xn)^(1/n)
        /// </summary>
        /// <param name="nums">待计算数组</param>
        /// <returns>几何平均值</returns>
        public static double GeometricMean(double[] nums)
        {
            #region 参数列表条件判断
            //输入数组不能为空
            if (nums == null || nums.Length == 0)
            {
                throw new ArgumentException("Parameter Input Illegal");
            }
            #endregion
            double product = 1;
            double avg = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                product *= nums[i];
            }
            avg = Math.Pow(product, 1.0 / nums.Length);
            return avg;
        }

        /// <summary>
        /// 计算调和平均数：n/((1/x1)+(1/x2)+...+(1/xn))
        /// </summary>
        /// <param name="nums">待计算数组</param>
        /// <returns>调和平均数</returns>
        public static double HarmonicMean(double[] nums)
        {
            #region 参数列表条件判断
            //输入数组不能为空
            if (nums == null || nums.Length == 0)
            {
                throw new ArgumentException("Parameter Input Illegal");
            }
            #endregion
            double sum = 0;
            double avg = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                sum += (1.0 / nums[i]);
            }
            avg = nums.Length / sum;
            return avg;
        }

        /// <summary>
        /// 计算切尾平均数
        /// </summary>
        /// <param name="nums">待计算数组</param>
        /// <param name="trimmedPercent">切尾百分比（0-100）</param>
        /// <returns>切尾平均数</returns>
        public static double TrimmedMean(double[] nums, double trimmedPercent)
        {
            #region 参数列表条件判断
            //输入数组不能为空，切尾百分比为0~100之间的数,另外切尾百分比过大吧数组切完了也会报错
            if (nums == null || nums.Length == 0 || trimmedPercent < 0 || trimmedPercent > 100)
            {
                throw new ArgumentException("Parameter Input Illegal");
            }
            #endregion

            //为了不修改nums值，对数组的计算和修改在tempArr数组中进行
            double[] tempArr = new double[nums.Length];
            nums.CopyTo(tempArr, 0);
            Array.Sort(tempArr);

            double sum = 0;
            double avg = 0;
            int trimNum = (int)(trimmedPercent * tempArr.Length / (2 * 100));

            for (int i = trimNum; i < tempArr.Length - trimNum; i++)
            {
                sum += tempArr[i];
            }
            avg = sum / (tempArr.Length - trimNum * 2);
            return avg;
        }
                 
        /// <summary>
        /// 计算数组的众数
        /// </summary>
        /// <param name="nums">数组</param>        
        /// <returns></returns>
        public static double[] Mode(double[] nums)
        {
            #region 预处理

            //输入合法性检验
            if (nums == null || nums.Length == 0)
            {
                throw new ArgumentException("Parameter Input Illegal");
            }

            // 为避免函数改变调用方nums，不直接在nums上进行处理 
            double[] data = new double[nums.Length];
            nums.CopyTo(data, 0);            
            #endregion
            
            //数组排序
            Array.Sort(data);

            List<double> valueList = new List<double>(); //直方图统计横坐标
            List<int> countList = new List<int>();       //直方图计数统计

            //添加第一个数进来
            valueList.Add(data[0]);
            countList.Add(1);

            //用for循环进行直方图统计
            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] == data[i - 1])//当前数字与上一项相等则Count++
                {
                    countList[countList.Count - 1]++;
                }
                else//当前数字与上一项不登则新建一个直方图项
                {
                    valueList.Add(data[i]);
                    countList.Add(1);
                }
            }

            List<double> mode = new List<double>();//众数

            //先假设第一个数为众数
            mode.Add(valueList[0]);
            int Count = countList[0];

            for (int i = 1; i < valueList.Count; i++)
            {
                if (countList[i] == Count)
                {
                    mode.Add(valueList[i]);//新增加一个众数
                }
                else if (countList[i] > Count)
                {
                    Count = countList[i];   //更新Count                    
                    mode.Clear();           //清除原来所有众数                    
                    mode.Add(valueList[i]); //当前数为众数
                }
            }

            if (mode.Count == valueList.Count)
            {
                return new double[] { };//所有数出现次数都是一样的，则没有众数，因此返回空数组
            }
            else
            {
                return mode.ToArray();//返回众数数组
            }
        }

        /// <summary>
        /// 对一维数组求中值
        /// </summary>
        /// <param name="nums">输入一维数组</param>
        /// <returns>返回输入序列的中值</returns>
        public static double Median(double[] nums)
        {
            #region 预处理

            //输入合法性检验
            if (nums == null || nums.Length == 0)
            {
                throw new ArgumentException("Parameter Input Illegal");
            }

            // 为避免函数改变调用方nums，不直接在nums上进行处理，新建一个数组将原来数组的内容copy过去 
            double[] data = new double[nums.Length];
            nums.CopyTo(data, 0);            
            #endregion

          
            double median;
            Array.Sort(data);//对输入数组进行排序
            if ((data.Length & 1) == 1)//数组长度为奇数

            {
                median = data[(data.Length - 1) / 2];
            }
            else//数组长度为偶数
            {
                median = (data[data.Length / 2] + data[(data.Length / 2 - 1)]) / 2;
            }
            return median;
        }
        
        /// <summary>
        /// 计算均方差
        /// </summary>
        /// <param name="x">序列X值</param>
        /// <param name="y">序列Y值</param>
        /// <returns>均方差值</returns>
        public static double MeanSquaredError(double[] x, double[] y)
        {
            if (x == null || y == null || x.Length != y.Length || x.Length == 0 || y.Length == 0) 
            {
                throw new ArgumentException("Parameter Input Illegal");
            }

            double MSE;//Mean Square Error(均方差)

            double sum = 0;
            for (int i = 0; i < x.Length; i++)
            {
                sum += Math.Pow(x[i] - y[i], 2);
            }
            MSE = sum / x.Length;

            return MSE;
        }

        /// <summary>
        /// 计算均方根
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static double RootMeanSquare(double[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                throw new ArgumentException("Parameter Input Illegal");
            }
            double RMS;//Root Mean Square(均方根)
            double sum = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                sum += Math.Pow(nums[i], 2);
            }
            RMS = Math.Pow((double)sum / nums.Length, 1 / 2.0);
            return RMS;
        }

        /// <summary>
        /// 计算方差
        /// </summary>
        /// <param name="nums">计算方差的数组</param>
        /// <returns></returns>
        public static double Variance(double[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                throw new ArgumentException("Parameter Input Illegal");
            }
            double _mean = Mean(nums);
            double sum = 0;
            int length = nums.Length;
            for (int i = 0; i < length; i++)
            {
                sum += Math.Pow((nums[i] - _mean), 2);
            }
            double _variance = sum / length; // 方差
            return _variance;
        }

        /// <summary>
        /// 计算标准差
        /// </summary>
        /// <param name="nums">计算标准差的数组</param>
        /// <returns></returns>
        public static double StandardDeviation(double[] nums)
        {
            double _variance = Variance(nums); // 方差
            double _standardDeviation = Math.Pow(_variance, 1 / 2.0);//标准差
            return _standardDeviation;
        }

        /// <summary>
        /// 直方图统计分析(统计输入数据在指定区间中出现次数)
        /// </summary>
        /// <param name="nums">输入数据(输入数组长度至少包含一个元素)</param>
        /// <param name="histogram">nums的离散直方图</param>
        /// <param name="intervals">区间中间值,区间数有数组长度决定</param>
        /// <param name="intervalType">指定区间开闭方式</param>
        public static void Histogram(double[] nums, ref int[] histogram, ref double[] intervals, IntervalType intervalType = IntervalType.LeftClosed)
        {
                                      
            if (nums == null || nums.Length == 0 || histogram == null || histogram.Length == 0 || intervals == null || intervals.Length == 0 || histogram.Length != intervals.Length) 
            {
                //nums、higtogram、intervals至少有一个元素
                //higtogram、intervals长度必须等长                
                throw new ArgumentException() { Source = "Unexpected Input Parameter for Hstogram Creating" };
            }
            int numberOfIntervals = histogram.Length;
            // 为避免函数改变调用方nums，不直接在nums上进行处理      
            double[] data = new double[nums.Length];
            nums.CopyTo(data,0);
            
            Array.Sort(data);                           //对数句进行排序
            intervals = new double[numberOfIntervals];  //直方图及区间数组初始化 
            histogram = new int[numberOfIntervals];      //直方图及区间数组初始化
            int dataLength = data.Length;               // 输入数据长度
            double _dataMin = data[0];                  // 输入数据的最小值
            double _dataMax = data[dataLength - 1];     // 输入数据的最大值
            double _delta = (_dataMax - _dataMin) / numberOfIntervals;// 区间宽度
            double _deltaHalf = _delta / 2;             // 区间半宽
            
            //==============================分割区间==============================
            for (int i = 0; i < numberOfIntervals; i++)
            {
                intervals[i] = _dataMin + _deltaHalf + _delta * i;
            }
            //==============================用for循环遍历数组进行直方图统计==============================
            int DataPosi = 0;               //输入序列遍历位置
            int IntervalPosi = 0;           //当前分析区间位置
            double left = _dataMin;         //当前统计区间左边界
            double right = left + _delta;   //当前统计区间右边界
            int count = 0;                  //计数                                       
            if (intervalType == IntervalType.LeftClosed)
            {
                #region ------------------------左闭右开区间方式统计----------------------------------
                
                while (DataPosi < dataLength)
                {
                    if (left <= data[DataPosi]  && data[DataPosi] < right)
                    {                       
                        count++;                        //当前遍历数字落在当前统计区间上
                        DataPosi++;                     //数据遍历位置自加，下一次循环分析下一个数据
                        if (DataPosi == dataLength) { histogram[IntervalPosi] = count; break; }//最后一个区间计数值写入直方图相应位置                        
                    }                    
                    else                                //当前数字不在当前统计区间
                    {
                        histogram[IntervalPosi] = count; //区间计数值写入直方图相应位置，结束当前区间统计完成第i个区间计数统计
                        count = 0;                      //count清零  
                        IntervalPosi++;                        
                        if (IntervalPosi == numberOfIntervals){  break; }      //判断是否还存在下一个区间          
                        left = right;                                           //将统计区间向右移动一个
                        right = intervals[IntervalPosi] + _deltaHalf;           //将统计区间向右移动一个
                    }
                }               
                //剩下在后面的全部认为与最后一个区间右边界相等
                histogram[numberOfIntervals - 1] = histogram[numberOfIntervals - 1] + dataLength - DataPosi;
                #endregion
            }
            else
            {
                #region ------------------------左闭右开区间方式统计----------------------------------
                while (DataPosi <dataLength && data[DataPosi] == left) { count++;   DataPosi++; } //第一个区间左边界数据统计
                while (DataPosi < dataLength)
                {
                    if (left < data[DataPosi] && data[DataPosi] <= right) 
                    {
                        count++;                        //当前遍历数字落在当前统计区间上
                        DataPosi++;                     //数据遍历位置自加，下一次循环分析下一个数据
                        if (DataPosi == dataLength) { histogram[IntervalPosi] = count; break; }//最后一个区间计数值写入直方图相应位置  
                    }
                    else                                //当前数字不在当前统计区间
                    {
                        histogram[IntervalPosi] = count; //区间计数值写入直方图相应位置，结束当前区间统计完成第i个区间计数统计
                        count = 0;                      //count清零   
                        IntervalPosi++;
                        if (IntervalPosi >= numberOfIntervals)  { break; }   //判断是否还存在下一个区间                       
                        left = right;                                           //将统计区间向右移动一个
                        right = intervals[IntervalPosi] + _deltaHalf;           //将统计区间向右移动一个                                      
                    }                    
                }
                //由于double数精度问题可能会导致数组中数大于最大值的情况，用下面一条语句对该情况进行处
                histogram[numberOfIntervals - 1] = histogram[numberOfIntervals - 1] + dataLength - DataPosi;
                #endregion               
            }   
        }
        #endregion
    }
    #region enum Definition

    /// <summary>
    /// <para>直方图区间开闭形式</para>
    /// <para>选择左闭区间则除最右一个区间为闭区间，其他所有区间为左闭右开形式；</para>
    /// <para>选择右闭区间则除最左一个区间为闭区间，其他所有区间为左开右闭区间；</para>
    /// </summary>
    public enum IntervalType
    {
        /// <summary>
        /// 除最右一个区间为闭区间，其他所有区间为左闭右开形式
        /// </summary>
        LeftClosed,

        /// <summary>
        /// 除最左一个区间为闭区间，其他所有区间为左开右闭区间
        /// </summary>
        RightClosed,
    }

    /// <summary>
    /// 均值计算类型枚举
    /// </summary>
    public enum MeanType
    {
        /// <summary>
        /// 算数平均值
        /// </summary>
        ArithmeticMean,

        /// <summary>
        /// 几何平均值
        /// </summary>
        GeometricMean,

        /// <summary>
        /// 调和平均数
        /// </summary>
        HarmonicMean,

        /// <summary>
        /// 切尾平均数
        /// </summary>
        TrimmedMean,
    }
    #endregion
}
