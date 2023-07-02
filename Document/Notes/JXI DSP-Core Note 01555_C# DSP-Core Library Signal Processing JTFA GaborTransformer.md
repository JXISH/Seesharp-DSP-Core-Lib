# JXI DSP-Core Note 01555_C# DSP-Core Library

# JTFA.GaborTransformer

**Author:** zliao

Github Username: A1020A

**Date:** Jun-30-2023

## GaborTransformer类在Solution Explorer中的显示

该类的定义路径为Signal Processing/Source/JTFA/GaborTransformer

![image-20230629220555217](.\Resources\Note 01555\DSP-Core Gabor Transformer and Example Places.PNG)

## GaborTransformer类及方法说明

### 1. 类的声明

```c#
/// <summary>
/// Gabor 变换
/// </summary>
public class GaborTransformer
```

该类可对信号进行Gabor变换(时域-时频域)与展开(时频域-时域)，根据指定的成对窗函数将信号在时域波形与时频域复数分布间转换



### 2. 枚举

```C#
/// <summary>
/// Gabor填充类型
/// </summary>
public enum PaddingType { noPadding, zeros, history, wrap }
```

Gabor变换时域头部填充的类型，包括无填充，零填充，历史填充，回绕填充



### 3. 私有字段

```c#
/// <summary>
/// Gabor实数填充数据
/// </summary>
private double[] _padding;

/// <summary>
/// Gabor复数填充数据
/// </summary>
private Complex[] _paddingC;

/// <summary>
/// 实数Gabor，遗留数据
/// </summary>
private double[] _unprocessedWaveformReal;

/// <summary>
/// 复数Gabor，遗留数据
/// </summary>
private Complex[] _unprocessedWaveformComplex;

/// <summary>
/// Gabor解析窗, 内部参数
/// </summary>
private double[] _analysisWindow;

/// <summary>
/// Gabor还原窗， 内部参数
/// </summary>
private double[] _synthesisWindow;
```





### 4. 公有属性

#### （1）FrequencyBins

```c#
/// <summary>
/// Gabor频域点数
/// </summary>
public int FrequencyBins { get; set; }
```

含义：Gabor变换时的频域采样点数



#### （2）TimeStep

```C#
/// <summary>
/// Gabor时域步进
/// </summary>
public int TimeStep { get; set; }
```

含义：Gabor变换的时域步进



#### （3）WindowType

```C#
/// <summary>
/// Gabor窗函数种类
/// </summary>
public WindowType WindowType { get; set; }
```

含义：用于Gabor变换的窗函数类型



#### （4）WindowLength

```c#
/// <summary>
/// Gabor窗函数长度
/// </summary>
public int WindowLength { get; set; }
```

含义：用于Gabor变换的窗函数长度



#### （5）Padding

```c#
/// <summary>
/// Gabor填充
/// </summary>
public PaddingType Padding { get; set; }
```

含义：Gabor变换的填充类型设置



#### （6）AnalysisWindow

```c#
/// <summary>
/// Gabor解析窗
/// </summary>
public double[] AnalysisWindow { get => _analysisWindow;}
```

含义：Gabor变换使用的解析窗时域数据



#### （7）SynthesisWindow

```c#
/// <summary>
/// Gabor还原窗
/// </summary>
public double[] SynthesisWindow { get => _synthesisWindow;}
```

含义：Gabor变换使用的还原窗时域数据



### 5. 方法说明

#### （1）GaborTransformer()

**方法申明：**

```C#
/// <summary>
/// Gabor Transformer 默认参数
/// </summary>
public GaborTransformer()
```

**功能描述：**GaborTransformer类的constructor，设置类的默认值



#### （2）GenerateDualWindow()

**方法申明：**

```c#
/// <summary>
/// 根据窗类型生成解析与还原双窗函数
/// </summary>
/// <param name="windowType">窗类型</param>
public void GenerateDualWindow(WindowType windowType) 
```

**功能描述：**按照输入的窗函数类型生成与当前GaborTransformer参数对应的双窗函数

**输入参数：**

- windowType：窗类型，数据类型：窗函数类型枚举

  

#### （3）GetGaborTransform方法重载一

**方法申明：**

```c#
/// <summary>
/// 复数Gabor变换
/// </summary>
/// <param name="waveform">输入波形</param>
/// <param name="Coeff">输出时频域系数矩阵</param>
public void GetGaborTransform(Complex[] waveform, Complex[][] Coeff)
```

**功能描述：**根据GaborTransformer类中的参数，计算复数Gabor 变换，从时域转换到时频域

**输入参数：**

- waveform：复数输入信号，数据类型：Complex类型数组

- Coeff：输出时频域谱，数据类型：Complex类型二维数组

  

#### （3）GetGaborTransform方法重载二

**方法申明：**

```c#
/// <summary>
/// 实数Gabor变换
/// </summary>
/// <param name="waveform">输入波形</param>
/// <param name="Coeff">输出时频域系数矩阵</param>
/// <param name="conjugateRemovedCoeff">全/半频谱选项</param>
public void GetGaborTransform(double[] waveform, Complex[][] Coeff, bool conjugateRemovedCoeff = false)
```

**功能描述：**根据GaborTransformer类中的参数，计算实数Gabor 变换，从时域转换到时频域，可区分全/半频谱输出。鉴于实数波形频域共轭对称的特性，只需要一半频谱长度保留其全部频谱信息。

**输入参数：**

- waveform: 输入信号，数据类型：double类型数组
- Coeff：输出时频域谱，数据类型：Complex类型二维数组
- conjugateRemovedCoeff：使用全/半频谱输出，数据类型：bool



#### （3）GetGaborTransform方法重载三

**方法申明：**

```c#
/// <summary>
/// 复数Gabor Transform静态实现，将一个时域信号通过解析窗和时域采样后映射到时频域里
/// </summary>
/// <param name="waveform">输入波形</param>
/// <param name="analysisWindow">时域解析窗</param>
/// <param name="dM">时域采样步进</param>
/// <param name="N">频域采样点数</param>
/// <param name="padding">时域信号填充</param>
/// <param name="Coeff">输出: 时频域谱</param>
public static void GetGaborTransform(Complex[] waveform, double[] analysisWindow, int dM, int N, double[] padding, ref Complex[][] Coeff)
```

**功能描述：**根据输入参数计算复数Gabor 变换，从时域转换到时频域，静态方法

**输入参数：**

- waveform: 输入信号，数据类型：Complex类型数组
- analysisWindow：时域解析窗，数据类型：double类型数组
- dM：时域采样步进，数据类型：int
- N：频域采样点数，数据类型：int
- padding：时域信号填充，数据类型：double类数组
- Coeff：输出时频域谱，数据类型：Complex类型二维数组



#### （4）GetGaborTransform方法重载四

**方法申明：**

```C#
/// <summary>
/// 实数Gabor Transform静态实现，将一个时域信号通过解析窗和时域采样后映射到时频域里
/// </summary>
/// <param name="waveform">输入波形</param>
/// <param name="analysisWindow">时域解析窗</param>
/// <param name="dM">时域采样步进</param>
/// <param name="N">频域采样点数</param>
/// <param name="padding">时域信号填充</param>
/// <param name="spectrum">输出: 时频域谱</param>
public static void GetGaborTransform(double[] waveform, double[] analysisWindow, int dM, int N, double[] padding, ref Complex[][] spectrum, bool conjugateRemovedCoeff = false)
```

**功能描述：**根据输入参数计算实数Gabor 变换，从时域转换到时频域，静态方法

**输入参数：**

- waveform: 输入信号，数据类型：double类型数组
- analysisWindow：时域解析窗，数据类型：double类型数组
- dM：时域采样步进，数据类型：int
- N：频域采样点数，数据类型：int
- padding：时域信号填充，数据类型：double类数组
- Coeff：输出时频域谱，数据类型：Complex类型二维数组
- conjugateRemovedCoeff：使用全/半频谱输出，数据类型：bool

#### （4）GetGaborExpansion方法重载一

**方法申明：**

```c#
/// <summary>
/// 复数Gabor展开
/// </summary>
/// <param name="Coeff"></param>
/// <param name="waveformOut"></param>
public void GetGaborExpansion(Complex[][] Coeff, Complex[] waveformOut)
```

**功能描述：**根据GaborTransformer类中的参数，计算复数Gabor 展开，从时频域转换回时域

**输入参数：**

- Coeff：输入时频域谱，数据类型：Complex类型二维数组
- waveform: 输出时域信号，数据类型：Complex类型数组



#### （4）GetGaborExpansion方法重载二

**方法申明：**

```C#
/// <summary>
/// 实数Gabor展开
/// </summary>
/// <param name="Coeff">时频域系数矩阵</param>
/// <param name="waveformOut">输出还原信号</param>
/// <param name="conjugateRemovedCoeff"></param>
public void GetGaborExpansion(Complex[][] Coeff, double[] waveformOut, bool conjugateRemovedCoeff = false)
```

**功能描述：**根据GaborTransformer类中的参数，计算实数Gabor 展开，从时频域转换回时域

**输入参数：**

- Coeff：输入时频域谱，数据类型：Complex类型二维数组
- waveform: 输出时域信号，数据类型：Complex类型数组
- conjugateRemovedCoeff：接受全/半频谱输入，数据类型：bool



#### （4）GetGaborExpansion方法重载三

**方法申明：**

```C#
/// <summary>
/// 将一个通过Gabor Transform生成的时频域谱通过对应的还原窗还原成时域信号
/// </summary>
/// <param name="spectrum">输入时频域谱</param>
/// <param name="synthesisWindow">时域还原窗</param>
/// <param name="dM">时域采样步进</param>
/// <param name="N">频域采样点数</param>
/// <param name="recoverdSignal">输出：还原时域信号</param>
public static void GetGaborExpasion(Complex[][] spectrum, double[] synthesisWindow, int dM, int N, Complex[] recoverdSignal)
```

**功能描述：**根据输入参数计算复数Gabor 展开，从时频域转换回时域，静态方法

**输入参数：**

- spectrum: 输入信号，数据类型：Complex类型二维数组
- synthesisWindow：时域还原窗，数据类型：double类型数组
- dM：时域采样步进，数据类型：int
- N：频域采样点数，数据类型：int
- recoveredSignal：输出时域还原信号，数据类型：Complex类型数组

#### （4）GetGaborExpansion方法重载四

**方法申明：**

```c#
/// <summary>
/// 将一个通过Gabor Transform生成的时频域谱通过对应的还原窗还原成时域信号
/// </summary>
/// <param name="spectrum">输入时频域谱</param>
/// <param name="synthesisWindow">时域还原窗</param>
/// <param name="dM">时域采样步进</param>
/// <param name="N">频域采样点数</param>
/// <param name="recoverdSignal">输出：还原时域信号</param>
public static void GetGaborExpasion(Complex[][] spectrum, double[] synthesisWindow, int dM, int N, double[] recoverdSignal, bool conjugateRemovedCoeff = false)
```

**功能描述：**根据输入参数计算实数Gabor 展开，从时频域转换回时域，静态方法

**输入参数：**

- spectrum: 输入信号，数据类型：Complex类型二维数组
- synthesisWindow：时域还原窗，数据类型：double类型数组
- dM：时域采样步进，数据类型：int
- N：频域采样点数，数据类型：int
- recoveredSignal：输出时域还原信号，数据类型：double类型数组
- conjugateRemovedCoeff：接受全/半频谱输入，数据类型：bool

## Example

### Solution Explorer位置

Signal Processing\Example\JTFA\Gabor Transform\Gabor Transform Example

### 界面

![DSP-Core Gabor Transform Example Screenshot](.\Resources\Note 01555\DSP-Core Gabor Transform Example Screenshot.PNG)