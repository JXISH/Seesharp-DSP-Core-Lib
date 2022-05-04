# C# DSP-Core Library 功能树

May-4-2022

## 关于

DSP-Core是聚星仪器基于C#的核心数字信号处理库，及其开发项目的名称。该库包含测试测量常用的C#基础功能。

你拿到这个库之后，可以编译生成一些类库dll。然后在你其他工作中引用这些dll就可以了。

下面这个功能树是整个项目function grouping的概览，也是多年信号处理编程经验的呈现。

![DSP-Core Overview_2022-0504](.\Resources\Note 00100\DSP-Core Overview_2022-0504.PNG)

## 功能树

DSP-Core

1. Numerics
   1. Data Strcture
      1. Const
      2. Circular Buffer
      3. Complex32  //float 组成的复数比Complex少用一半内存
      4. NumericUtility  //交织数组和复数的转换
      5. NumericRange //上下限截断
      6. MathExtension //增加一个sinc函数
   2. Vector
      1. Init
      2. Math: Abs, Add, Sub, Sum, Multi, Division, DotProduct, SquareRoot, 平方, log2/e/10, Exp2/e/10
      3. Operation: Copy, Sort, Reverse, 升降采样，极坐标-复数互换， 复数-实部虚部互换
      4. Stats: 统计数组Max, Min, Mean, 相位平均
      5. Logic: Compare, 两数组对应元素取大、取小
2. File IO
   1. Wav
   2. Vector File
   3. IQ File
3. Multimedia
   1. MP3 Encoder
4. Mathematics
   1. Curve Fitting
   2. Probability Statistics (with point by point)
   3. Interpolation
   4. Geometry
   5. Linear Algebra
5. Signal Processing
   1. Conditioning
      1. Easy Filters
      2. Advanced Filters
      3. Synchronization
      4. Easy Resample
   2. Generation
      1. Generation
      2. Basic Function Generators
      3. Noise Generator
   3. Measurements
      1. Frequency Response Function
      2. Harmonic Analysis
      3. Square-wave Measurement
      4. Third Octave Analysis
   4. Spectrum Analysis
      1. General Spectrum
      2. RFSA Spectrum
   5. Transforms
      1. DFT
   6. Windows
   7. JTFA
      1. General JTFA
      2. RFSAJTFA - Future release
      3. Spectrogram: STFT
6. RF Communications
   1. Modem
      1. Analog Modulation
      2. Analog Demodulation
   2. Modulator
      1. Modulator
      2. DigitalConverter



## JXI DSP和Math Toolset

这个库Math和Signal Processing主要来自原来的收费软件包JXI Math 和 DSP Tooset。我们在Math里面增加了Geometry, Point-by-point, Linear Algebra；在Signal Processing里面增加了Advanced Filter (含升降采样的FIR), Spectrum Analysis的RFSA Spectrum, Transform的DFT。



## 命名解释和思考

* Numerics

  查了Merrian-Webster 字典，Numeric是形容词，也是名词。 名词表示数，可以有复数，也查到了范句：

  // in addition to the standard alphabetical letters and *numerics*, the keyboard features rows of special characters

* Transforms & Windows

  变换可以有傅里叶和其他变换(如希尔伯特变换)。以目前我们的应用和工作基础，仅放离散傅里叶变换，其中调用intel工具库，自动选择快速傅里叶变换。提供实数、复数、shifted、non-shifted等选项。

  Windows和变换分开，是因为有时候窗函数单独使用，不需要傅里叶变换。比如，指数窗可以和FRF联合使用，单独用窗函数可以做非周期采样的平均。

* 频谱分析分类

  信号处理常用的频谱是已知采样长度，比如2048， 4096进行计算，通常采样长度=变换长度=窗函数长度。

  RFSA频谱会指定频谱范围、分辨率带宽和频谱点数，由此库函数计算采样长度、窗函数长度和变换长度。 有时候需要补零调节使得结果符合频谱特性要求。

* JTFA(联合时频分析)分类

  通常的JTFA类似常规频谱，将其累积得到时频图谱。

  RFSA JTFA常用复数输入，会切除两边各20%过渡带。

  STFT纯粹短时傅里叶变换，保留原始复数数据图谱，适合高级数字信号处理。

* 与简仪SeeSharp DSP工具关系

  本库独立，很多范例调用了简仪SeeSharp GUI库。但是基本上本库功能是SeeSharp DSP库没有的，两者可以互补。