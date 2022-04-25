# Numerics: Vector Operate Operations



## Vector类在Solution Explorer中的显示



## 范例说明

范例位于 Numerics\Examples

本数学操作范例为：Numeric Operations Example

这批范例采用.NET framework，Windows console（控制台）的模式。运行通过Console.Write/WriteLine显示在弹出控制台窗口，通过Console.ReadKey()等待一个按键结束并关闭控制台。



## 数组操作功能列表

### Copy

拷贝数组：

```csharp
/// <summary>
/// 数组拷贝模板
/// </summary>
public static void ArrayCopy<T>(T[] source, T[] destination)

/// <summary>
/// 数组拷贝模板
/// </summary>
public static void ArrayCopy<T>(T[] source, int sourceStart, T[] destination, int destinationStat, int length)

/// <summary>
/// 二维数组拷贝模板
/// </summary>
public static void ArrayCopy<T>(T[,] source, T[,] destination)

/// <summary>
/// 数组拷贝模板
/// </summary>
public static void ArrayCopy<T>(T[] source, IntPtr destination, int length = 0)

/// <summary>
/// 数组拷贝模板
/// </summary>
public static void ArrayCopy<T>(T[] source, byte[] destination, int length = 0)

/// <summary>
/// 数组拷贝模板
/// </summary>
public static void ArrayCopy<T>(IntPtr source, T[] destination, int length = 0)

/// <summary>
/// 数组拷贝模板
/// </summary>
public static void ArrayCopy<T>(byte[] source, T[] destination, int length = 0)
```



### Sort

以升序排列数组。

```csharp
/// <summary>
///  数组升序
/// </summary>
public static void ArrayAscend(short[] inout)

/// <summary>
///  数组升序
/// </summary>
public static void ArrayAscend(short[] a, short[] output)

/// <summary>
///  数组升序，并返回索引位置
/// </summary>
public static void ArrayAscendIndex(short[] inout, int[] index)

/// <summary>
///  数组升序，并返回索引位置
/// </summary>
public static void ArrayAscendIndex(short[] a, short[] output,int[] index)

/// <summary>
///  数组升序
/// </summary>
public static void ArrayAscend(int[] inout)

/// <summary>
///  数组升序
/// </summary>
public static void ArrayAscend(int[] a, int[] output)

/// <summary>
///  数组升序，并返回索引位置
/// </summary>
public static void ArrayAscendIndex(int[] inout, int[] index)

/// <summary>
///  数组升序，并返回索引位置
/// </summary>
public static void ArrayAscendIndex(int[] a, int[] output, int[] index)

/// <summary>
///  数组升序
/// </summary>
public static void ArrayAscend(float[] inout)

/// <summary>
///  数组升序
/// </summary>
public static void ArrayAscend(float[] a, float[] output)

/// <summary>
///  数组升序，并返回索引位置
/// </summary>
public static void ArrayAscendIndex(float[] inout, int[] index)

/// <summary>
///  数组升序，并返回索引位置
/// </summary>
public static void ArrayAscendIndex(float[] a, float[] output, int[] index)

/// <summary>
///  数组升序
/// </summary>
public static void ArrayAscend(double[] inout)

/// <summary>
///  数组升序
/// </summary>
public static void ArrayAscend(double[] a, double[] output)

/// <summary>
///  数组升序，并返回索引位置
/// </summary>
public static void ArrayAscendIndex(double[] inout, int[] index)

/// <summary>
///  数组升序，并返回索引位置
/// </summary>
public static void ArrayAscendIndex(double[] a, double[] output, int[] index)
```



以降序排列数组。

```csharp
/// <summary>
///  数组降序
/// </summary>
public static void ArrayDescend(short[] inout)

/// <summary>
///  数组降序
/// </summary>
public static void ArrayDescend(short[] a, short[] output)

/// <summary>
///  数组降序，并返回索引位置
/// </summary>
public static void ArrayDescendIndex(short[] inout, int[] index)

/// <summary>
///  数组降序，并返回索引位置
/// </summary>
public static void ArrayDescendIndex(short[] a, short[] output, int[] index)

/// <summary>
///  数组降序
/// </summary>
public static void ArrayDescend(int[] inout)

/// <summary>
///  数组降序
/// </summary>
public static void ArrayDescend(int[] a, int[] output)

/// <summary>
///  数组降序，并返回索引位置
/// </summary>
public static void ArrayDescendIndex(int[] inout, int[] index)

/// <summary>
///  数组降序，并返回索引位置
/// </summary>
public static void ArrayDescendIndex(int[] a, int[] output, int[] index)

/// <summary>
///  数组降序
/// </summary>
public static void ArrayDescend(float[] inout)

/// <summary>
///  数组降序
/// </summary>
public static void ArrayDescend(float[] a, float[] output)

/// <summary>
///  数组降序，并返回索引位置
/// </summary>
public static void ArrayDescendIndex(float[] inout, int[] index)

/// <summary>
///  数组降序，并返回索引位置
/// </summary>
public static void ArrayDescendIndex(float[] a, float[] output, int[] index)

/// <summary>
///  数组降序
/// </summary>
public static void ArrayDescend(double[] inout)

/// <summary>
///  数组降序
/// </summary>
public static void ArrayDescend(double[] a, double[] output)

/// <summary>
///  数组降序，并返回索引位置
/// </summary>
public static void ArrayDescendIndex(double[] inout, int[] index)

/// <summary>
///  数组降序，并返回索引位置
/// </summary>
public static void ArrayDescendIndex(double[] a, double[] output, int[] index)
```



### Transpose

将数组转置，一种转置方式：

A进output出。

```csharp
/// <summary>
/// 二维数组转置
/// </summary>
public static void ArrayTranspose<T>(T[][] input, T[][] output)
```



### Reverse

将数组反转：

{1, 2, 3, 4, 5} => {5, 4, 3, 2, 1}

两种反转方式：

原位计算，A进A出；A进output出。

```csharp
/// <summary>
///  数组反转
/// </summary>
public static void ArrayReverse(short[] inout)

/// <summary>
///  数组反转
/// </summary>
public static void ArrayReverse(short[] a, short[] output)

/// <summary>
///  数组反转
/// </summary>
public static void ArrayReverse(float[] inout)

/// <summary>
///  数组反转
/// </summary>
public static void ArrayReverse(float[] a, float[] output)

/// <summary>
///  数组反转
/// </summary>
public static void ArrayReverse(double[] inout)

/// <summary>
///  数组反转
/// </summary>
public static void ArrayReverse(double[] a, double[] output)

/// <summary>
///  数组反转
/// </summary>
public static void ArrayReverse(Complex32[] inout)

/// <summary>
///  数组反转
/// </summary>
public static void ArrayReverse(Complex32[] a, Complex32[] output)

/// <summary>
///  数组反转
/// </summary>
public static void ArrayReverse(Complex[] inout)

/// <summary>
///  数组反转
/// </summary>
public static void ArrayReverse(Complex[] a, Complex[] output)
```

