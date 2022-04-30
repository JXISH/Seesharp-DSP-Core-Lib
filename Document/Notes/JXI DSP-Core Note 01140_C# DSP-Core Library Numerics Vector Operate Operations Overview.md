# Numerics: Vector Operate Operations



## Vector类在Solution Explorer中的显示

由于采用了Partial Class （部分定义） 的方法，在多个*.cs文件里面定义Vector Class，所以当在Solution Explorer里面点击打开Numerics Library\Vector 下面任何一个cs文件都可以看到一长串属性和方法。你可能会感到困惑，但是当了解任何一个cs文件打开的列表是相同的，整个Vector类的列表，就不那么焦虑了。

而且，当你在这个列表里面点击某一个感兴趣的对象时，Visual Studio会在代码编辑窗里面打开改对象的实现代码。



## 范例说明

范例位于 Numerics\Examples

本数学操作范例为：Numeric Operations Example

这批范例采用.NET framework，Windows console（控制台）的模式。运行通过Console.Write/WriteLine显示在弹出控制台窗口，通过Console.ReadKey()等待一个按键结束并关闭控制台。



## 数组操作功能列表

### Copy

拷贝数组：

```csharp
/// 数组拷贝模板
public static void ArrayCopy<T>(T[] source, T[] destination)

/// 数组拷贝模板
public static void ArrayCopy<T>(T[] source, int sourceStart, T[] destination, int destinationStat, int length)

/// 二维数组拷贝模板
public static void ArrayCopy<T>(T[,] source, T[,] destination)

/// 数组拷贝模板
public static void ArrayCopy<T>(T[] source, IntPtr destination, int length = 0)

/// 数组拷贝模板
public static void ArrayCopy<T>(T[] source, byte[] destination, int length = 0)

/// 数组拷贝模板
public static void ArrayCopy<T>(IntPtr source, T[] destination, int length = 0)

/// 数组拷贝模板
public static void ArrayCopy<T>(byte[] source, T[] destination, int length = 0)
```



### Sort

以升序排列数组。

```csharp
///  数组升序
public static void ArrayAscend(short[] inout)

///  数组升序
public static void ArrayAscend(short[] a, short[] output)

///  数组升序，并返回索引位置
public static void ArrayAscendIndex(short[] inout, int[] index)

///  数组升序，并返回索引位置
public static void ArrayAscendIndex(short[] a, short[] output,int[] index)

///  数组升序
public static void ArrayAscend(int[] inout)

///  数组升序
public static void ArrayAscend(int[] a, int[] output)

///  数组升序，并返回索引位置
public static void ArrayAscendIndex(int[] inout, int[] index)

///  数组升序，并返回索引位置
public static void ArrayAscendIndex(int[] a, int[] output, int[] index)

///  数组升序
public static void ArrayAscend(float[] inout)

///  数组升序
public static void ArrayAscend(float[] a, float[] output)

///  数组升序，并返回索引位置
public static void ArrayAscendIndex(float[] inout, int[] index)

///  数组升序，并返回索引位置
public static void ArrayAscendIndex(float[] a, float[] output, int[] index)

///  数组升序
public static void ArrayAscend(double[] inout)

///  数组升序
public static void ArrayAscend(double[] a, double[] output)

///  数组升序，并返回索引位置
public static void ArrayAscendIndex(double[] inout, int[] index)

///  数组升序，并返回索引位置
public static void ArrayAscendIndex(double[] a, double[] output, int[] index)
```



以降序排列数组。

```csharp
///  数组降序
public static void ArrayDescend(short[] inout)

///  数组降序
public static void ArrayDescend(short[] a, short[] output)

///  数组降序，并返回索引位置
public static void ArrayDescendIndex(short[] inout, int[] index)

///  数组降序，并返回索引位置
public static void ArrayDescendIndex(short[] a, short[] output, int[] index)

///  数组降序
public static void ArrayDescend(int[] inout)

///  数组降序
public static void ArrayDescend(int[] a, int[] output)

///  数组降序，并返回索引位置
public static void ArrayDescendIndex(int[] inout, int[] index)

///  数组降序，并返回索引位置
public static void ArrayDescendIndex(int[] a, int[] output, int[] index)

///  数组降序
public static void ArrayDescend(float[] inout)

///  数组降序
public static void ArrayDescend(float[] a, float[] output)

///  数组降序，并返回索引位置
public static void ArrayDescendIndex(float[] inout, int[] index)

///  数组降序，并返回索引位置
public static void ArrayDescendIndex(float[] a, float[] output, int[] index)

///  数组降序
public static void ArrayDescend(double[] inout)

///  数组降序
public static void ArrayDescend(double[] a, double[] output)

///  数组降序，并返回索引位置
public static void ArrayDescendIndex(double[] inout, int[] index)

///  数组降序，并返回索引位置
public static void ArrayDescendIndex(double[] a, double[] output, int[] index)
```



### Transpose

将数组转置，一种转置方式：

A进output出。

```csharp
/// 二维数组转置
public static void ArrayTranspose<T>(T[][] input, T[][] output)
```



### Reverse

将数组反转：

{1, 2, 3, 4, 5} => {5, 4, 3, 2, 1}

两种反转方式：

原位计算，A进A出；A进output出。

```csharp
///  数组反转
public static void ArrayReverse(short[] inout)

///  数组反转
public static void ArrayReverse(short[] a, short[] output)

///  数组反转
public static void ArrayReverse(float[] inout)

///  数组反转
public static void ArrayReverse(float[] a, float[] output)

///  数组反转
public static void ArrayReverse(double[] inout)

///  数组反转
public static void ArrayReverse(double[] a, double[] output)

///  数组反转
public static void ArrayReverse(Complex32[] inout)

///  数组反转
public static void ArrayReverse(Complex32[] a, Complex32[] output)

///  数组反转
public static void ArrayReverse(Complex[] inout)

///  数组反转
public static void ArrayReverse(Complex[] a, Complex[] output)
```



### RealImageToComplex

1. 实部，虚部 -> 复数 

   提供了两种数据类型的方法：(double -> Complex, float->Complex32)

   

   注解：Complex32是为了节约内存，本类库定义的实部、虚部都是32位字长的复数，而系统的Complex是64位字长的。

```csharp
/// 由实部虚部生成 Complex 数组
public static void RealImageToComplex(double[] real, double[] image, Complex[] complexData)

/// 由实部虚部生成 Complex 数组
public static Complex[] RealImageToComplex(double[] real, double[] image)

/// 由实部虚部生成 Complex32 数组
public static void RealImageToComplex(float[] real, float[] image, Complex32[] complexData)

/// 由实部虚部生成 Complex32 数组
public static Complex32[] RealImageToComplex(float[] real, float[] image)
```



### PolarToComplex

幅度，相位 -> 复数

提供了两种数据类型的方法：(double -> Complex, float->Complex32)

```csharp
/// 由幅度相位生成 Complex 数组
public static void PolarToComplex(double[] magitude, double[] phase, Complex[] complexData)

/// 由幅度相位生成 Complex 数组
public static void PolarToComplex( double[] phase, Complex[] complexData)

/// 由幅度相位生成 Complex 数组
public static Complex[] PolarToComplex(double[] magitude, double[] phase)

/// 由幅度相位生成 Complex 数组
public static Complex[] PolarToComplex( double[] phase)

/// 由幅度相位生成 Complex32 数组
public static void PolarToComplex(float[] magitude, float[] phase, Complex32[] complexData)

/// 由幅度相位生成 Complex32 数组
public static void PolarToComplex(float[] phase, Complex32[] complexData)

/// 由幅度相位生成 Complex32 数组
public static Complex32[] PolarToComplex(float[] magitude, float[] phase)

/// 由幅度相位生成 Complex32 数组
public static Complex32[] PolarToComplex(float[] phase)
```



### ComplexToRealImage

复数 ->实部，虚部 (float -> Complex32, double -> Complex)

复数 ->实部，虚部 ( Complex32 -> float , Complex -> double)

```csharp
/// 获取 Complex 数组实部虚部
public static void ComplexToRealImage(Complex[] a, double[] real, double[] image)

/// 获取 Complex32 数组实部虚部
public static void ComplexToRealImage(Complex32[] a, float[] real, float[] image)

/// 获取 Complex 数组实部虚部
public static void ComplexToRealImage(double[] magitude, double[] phase, double[] real, double[] image)

/// 获取 Complex32 数组实部虚部
public static void ComplexToRealImage(float[] magitude, float[] phase, float[] real, float[] image)
```

