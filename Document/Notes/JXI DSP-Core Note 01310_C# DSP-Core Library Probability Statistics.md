# C# DSP-Core Library Probability Statistics

Feb-21-2022

# 功能

* 位置：Mathematics\ProbabilityStatistics

* 分类

  * Winform Histogram
  * Winform Mean
  * Winform MeanSquaredError
  * Winform Median
  * Windorm Mode
  * Winform MultiFunction
  * Winform RootMeanSquare
  * Winform StandardDeviation
  * Winform Variance

* 类库公开方法

  * ProbabilityStatistics

    * 公开方法

    ```c#
    /// <summary>
    /// 计算算术平均值,（x1+x2+...+xn）/n
    /// </summary>
    /// <param name="nums">待计算数组</param>
    /// <returns>算术平均值</returns>
    public static double Mean()
    
    /// <summary>
    /// 计算几何平均数：(x1*x2*...*xn)^(1/n)
    /// </summary>
    /// <param name="nums">待计算数组</param>
    /// <returns>几何平均值</returns>
    public static double GeometricMean()
    
    /// <summary>
    /// 计算调和平均数：n/((1/x1)+(1/x2)+...+(1/xn))
    /// </summary>
    /// <param name="nums">待计算数组</param>
    /// <returns>调和平均数</returns>
    public static double HarmonicMean()
    
    /// <summary>
    /// 计算切尾平均数
    /// </summary>
    /// <param name="nums">待计算数组</param>
    /// <param name="trimmedPercent">切尾百分比（0-100）</param>
    /// <returns>切尾平均数</returns>
    public static double TrimmedMean()
    
    /// <summary>
    /// 计算数组的众数
    /// </summary>
    /// <param name="nums">数组</param>        
    /// <returns></returns>
    public static double[] Mode()
    
    /// <summary>
    /// 对一维数组求中值
    /// </summary>
    /// <param name="nums">输入一维数组</param>
    /// <returns>返回输入序列的中值</returns>
    public static double Median()
    
    /// <summary>
    /// 计算均方差
    /// </summary>
    /// <param name="x">序列X值</param>
    /// <param name="y">序列Y值</param>
    /// <returns>均方差值</returns>
    public static double MeanSquaredError()
    
    /// <summary>
    /// 计算均方根
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static double RootMeanSquare()
    
    /// <summary>
    /// 计算方差
    /// </summary>
    /// <param name="nums">计算方差的数组</param>
    /// <returns></returns>
    public static double Variance()
    
    /// <summary>
    /// 计算标准差
    /// </summary>
    /// <param name="nums">计算标准差的数组</param>
    /// <returns></returns>
    public static double StandardDeviation()
    
    /// <summary>
    /// 直方图统计分析(统计输入数据在指定区间中出现次数)
    /// </summary>
    /// <param name="nums">输入数据(输入数组长度至少包含一个元素)</param>
    /// <param name="histogram">nums的离散直方图</param>
    /// <param name="intervals">区间中间值,区间数有数组长度决定</param>
    /// <param name="intervalType">指定区间开闭方式</param>
    public static void Histogram()
    ```

    * 公开函数

    ```c#
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
    ```

    

  * PointByPointStatistics

  ```c#
  /// <summary>
  /// 将新的数值添加到历史队列中，若队列已满，则移除最早的数据点。
  /// </summary>
  /// <param name="newValue"></param>
  public void Enqueue()
  
  /// <summary>
  /// 复制对象的数据（深复制）。
  /// </summary>
  /// <param name="source"></param>
  public void CopyFrom()
  
  /// <summary>
  /// 清空历史数据。
  /// </summary>
  public void Reset()
  ```




# Winform Histogram

### 界面

![image-20220221235348302](Resources\Note 01310\histogram_example.png)

### 核心代码



# Winform Mean



# Winform MeanSquaredError



# Winform Median



# Windorm Mode



# Winform MultiFunction



# Winform RootMeanSquare



# Winform StandardDeviation



# Winform Variance

