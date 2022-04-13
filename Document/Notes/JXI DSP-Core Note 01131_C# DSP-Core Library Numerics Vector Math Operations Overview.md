# Numerics: Vector Math Operations



## Vector类在Solution Explorer中的显示

由于我们采用了Partial Class （部分定义） 的方法，在多个*.cs文件里面定义Vector Class，所以当你在Solution Explorer里面点击打开Numerics Library\Vector 下面任何一个cs文件都可以看到一长串属性和方法。你可能感到困惑，但是当你了解任何一个cs文件打开的列表是相同的，整个Vector类的列表，就不那么焦虑了。

而且，当你在这个列表里面点击某一个感兴趣的对象时，Visual Studio会在代码编辑窗里面打开改对象的实现代码。

## 范例说明

范例位于 Numerics\Examples

本数学操作范例为：Numeric Operations Example

这批范例采用.NET framework，Windows console（控制台）的模式。运行通过Console.Write/WriteLine显示在弹出控制台窗口，通过Console.ReadKey()等待一个按键结束并关闭控制台。

## 数组数学操作功能列表

### ABS

数学操作的输出模式：

1. 双参数，A入B出

   要求B定义和A类型相同，大小相同。

2. 单参数，A输入，方法返回输出数组

   减少一次B数组定义。

3. 单输入，原位计算

   当你不需要保留原来的数据时，使用这个模式，可以节约内存。

```c#
//对应方法定义代码
/// <summary>
/// 数组取模
/// output = Abs (a)
/// </summary>
public static void ArrayAbs<T>(T[] a, T[] output)
/// <summary>
/// 数组取模
/// return = Abs (a)
/// </summary>
public static T[] GetArrayAbs<T>(T[] a)
    /// <summary>
/// 数组取模
/// inout = Abs (inout)
/// </summary>
public static void ArrayAbs<T>(T [] inout)
```



### Add (Array+Array)

输入输出沿用ABS的策略，分为a[]+b[] -> output[]，a[]+b[] -> 返回值，a[]+b[] -> a[] (inout)，且称为第三参数返回，方法返回，和原位计算。

```csharp
/// <summary>
/// 数组加法
/// output = a + b
/// </summary>
public static void ArrayAdd<T>(T[] a, T[] b, T[] output)

/// <summary>
/// 数组加法
/// return = a + b
/// </summary>
public static T[] GetArrayAdd<T>(T[] a, T[] b)

/// <summary>
/// 数组加法
/// inout = inout + b
/// </summary>
public static void ArrayAdd<T>(T[] inout, T[] b)
```



### Add (Array + Value)

还是三种实现：第三参数返回，方法返回，和原位计算

```csharp
/// <summary>
/// 数组加常数
/// output = a + b
/// </summary>
public static void ArrayAdd<T>(T[] a, T b, T[] output)

/// <summary>
/// 数组加常数
/// return = a + b
/// </summary>
public static T[] GetArrayAdd<T>(T[] a, T b)     

/// <summary>
/// 数组加常数
/// inout = inout + b
/// </summary>
public static void ArrayAdd<T>(T[] inout, T b)

```



### Sub

同Add，三种实现：第三参数返回，方法返回，和原位计算

```csharp
/// <summary>
/// 数组减法
/// inout = inout - b
/// </summary>
public static void ArraySub<T>(T[] inout, T[] b)

/// <summary>
/// 数组减法
/// return = a - b
/// </summary>
public static T[] GetArraySub<T>(T[] a, T[] b)

/// <summary>
/// 数组减法
/// output = a - b
/// </summary>
public static void ArraySub<T>(T[] a, T[] b, T[] output)

```



### Devision

三种实现：第三参数返回，方法返回，和原位计算

```csharp
/// <summary>
/// 数组除法
/// inout = inout / b
/// </summary>
public static void ArrayDivision<T>(T[] inout, T[] b)

/// <summary>
/// 数组除法
/// return = a / b
/// </summary>
public static T[] GetArrayDivision<T>(T[] a, T[] b)

/// <summary>
/// 数组除法
/// output = a / b
/// </summary>
public static void ArrayDivision<T>(T[] a, T[] b, T[] output)
```



### Scale

三种实现：第三参数返回，方法返回，和原位计算

```csharp
/// <summary>
/// 数组乘以常数
/// inout = inout * b
/// </summary>
public static void ArrayScale<T>(T[] inout, T b)

/// <summary>
/// 数组乘以常数
/// output = a * b
/// </summary>
public static T[] GetArrayScale<T>(T[] a, T b)

/// <summary>
/// 数组乘以常数
/// output = a * b
/// </summary>
public static void ArrayScale<T>(T[] a, T b, T[] output)
```

三种实现：第三参数返回，方法返回，和原位计算

以下方法不同的是输入数组b为double类型

```csharp
/// <summary>
/// 数组乘以常数
/// inout = inout * b
/// </summary>
public static void ArrayScale<T>(T[] inout, double b)

/// <summary>
/// 数组乘以常数
/// output = a * b
/// </summary>
public static T[] GetArrayScale<T>(T[] a, double b)

/// <summary>
/// 数组乘以常数
/// output = a * b
/// </summary>
public static void ArrayScale<T>(T[] a, double b, T[] output)
```



### Multiplication

三种实现：第三参数返回，方法返回，和原位计算

```csharp
/// <summary>
/// 数组乘法
/// inout = inout * b
/// </summary>
public static void ArrayMulti<T>(T[] inout, T[] b)

/// <summary>
/// 数组乘法
/// return = a * b
/// </summary>
public static T[] GetArrayMulti<T>(T[] a, T[] b)

/// <summary>
/// 数组乘法
/// output = a * b
/// </summary>
public static void ArrayMulti<T>(T[] a, T[] b, T[] output)
```



### DotProduct

1. float数组 · float数组 -> float点积输出
2. double数组 · double数组 -> double点积输出

```csharp
/// <summary>
/// 对 float 数组求点积
/// </summary>
public static float ArrayDotProduct(float[] a, float[] b)

/// <summary>
/// 对 double 数组求点积
/// </summary>
public static double ArrayDotProduct(double[] a, double[] b)

/// <summary>
/// 对 Complex32 数组求点积
/// </summary>
public static Complex32 ArrayDotProduct(Complex32[] a, Complex32[] b)

/// <summary>
/// 对 Complex 数组求点积
/// </summary>
public static Complex ArrayDotProduct(Complex[] a, Complex[] b)

/// <summary>
/// 对 Complex32 数组求点积
/// return =  a dotProduct b*
/// </summary>
public static Complex32 ArrayDotProductConj(Complex32[] a, Complex32[] b)

/// <summary>
/// 对 Complex 数组求点积
/// return =  a dotProduct b*
/// </summary>
public static Complex ArrayDotProductConj(Complex[] a, Complex[] b)

```



### ArrayEqual

比较输入的两个Array是否相等。

```csharp
/// <summary>
/// 比较 Array 是否相同
/// </summary>
public static bool ArrayEqual<T>(T[] a, T[] b)
```



### RealImageToComplex

1. 实部，虚部 -> 复数 

   提供了两种数据类型的方法：(double -> Complex, float->Complex32)

   
   
   注解：Complex32是为了节约内存，本类库定义的实部、虚部都是32位字长的复数，而系统的Complex是64位字长的。

```csharp
/// <summary>
/// 由实部虚部生成 Complex 数组
/// </summary>
public static void RealImageToComplex(double[] real, double[] image, Complex[] complexData)

/// <summary>
/// 由实部虚部生成 Complex 数组
/// </summary>
public static Complex[] RealImageToComplex(double[] real, double[] image)

/// <summary>
/// 由实部虚部生成 Complex32 数组
/// </summary>
public static void RealImageToComplex(float[] real, float[] image, Complex32[] complexData)

/// <summary>
/// 由实部虚部生成 Complex32 数组
/// </summary>
public static Complex32[] RealImageToComplex(float[] real, float[] image)
```



### PolarToComplex

幅度，相位 -> 复数

提供了两种数据类型的方法：(double -> Complex, float->Complex32)

```csharp
/// <summary>
/// 由幅度相位生成 Complex 数组
/// </summary>
public static void PolarToComplex(double[] magitude, double[] phase, Complex[] complexData)

/// <summary>
/// 由幅度相位生成 Complex 数组
/// </summary>
public static void PolarToComplex( double[] phase, Complex[] complexData)

/// <summary>
/// 由幅度相位生成 Complex 数组
/// </summary>
public static Complex[] PolarToComplex(double[] magitude, double[] phase)

/// <summary>
/// 由幅度相位生成 Complex 数组
/// </summary>
public static Complex[] PolarToComplex( double[] phase)

/// <summary>
/// 由幅度相位生成 Complex32 数组
/// </summary>
public static void PolarToComplex(float[] magitude, float[] phase, Complex32[] complexData)

/// <summary>
/// 由幅度相位生成 Complex32 数组
/// </summary>
public static void PolarToComplex(float[] phase, Complex32[] complexData)

/// <summary>
/// 由幅度相位生成 Complex32 数组
/// </summary>
public static Complex32[] PolarToComplex(float[] magitude, float[] phase)

/// <summary>
/// 由幅度相位生成 Complex32 数组
/// </summary>
public static Complex32[] PolarToComplex(float[] phase)
```



### ComplexToRealImage

复数 ->实部，虚部 (float -> Complex32, double -> Complex)

复数 ->实部，虚部 ( Complex32 -> float , Complex -> double)

```csharp
/// <summary>
/// 获取 Complex 数组实部虚部
/// </summary>
public static void ComplexToRealImage(Complex[] a, double[] real, double[] image)

/// <summary>
/// 获取 Complex32 数组实部虚部
/// </summary>
public static void ComplexToRealImage(Complex32[] a, float[] real, float[] image)

/// <summary>
/// 获取 Complex 数组实部虚部
/// </summary>
public static void ComplexToRealImage(double[] magitude, double[] phase, double[] real, double[] image)

/// <summary>
/// 获取 Complex32 数组实部虚部
/// </summary>
public static void ComplexToRealImage(float[] magitude, float[] phase, float[] real, float[] image)

```