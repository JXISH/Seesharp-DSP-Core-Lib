# JXI DSP-Core Note 01130_C# DSP-Core Library 

# Numerics: Vector Math Operations

## Vector类在Solution Explorer中的显示

由于我们采用了Partial Class （部分定义） 的方法，在多个*.cs文件里面定义Vector Class，所以当你在Solution Explorer里面点击打开Numerics Library\Vector 下面任何一个cs文件都可以看到一长串属性和方法。你可能感到困惑，但是当你了解任何一个cs文件打开的列表是相同的，整个Vector类的列表，就不那么焦虑了。

![image-20220210102948250](.\Resources\Note 01130\Vector obj list in sln explorer.png)

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

比较输入Array，返回bool类型 “是/否”

```csharp
/// <summary>
/// 比较 Array 是否相同
/// </summary>
public static bool ArrayEqual<T>(T[] a, T[] b)
```



### RealImageToComplex

1. 实部，虚部 -> 复数 (double -> Complex, float->Complex32)

   

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

幅度，相位 -> 复数 (double -> Complex, float->Complex32)

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



### CrossProduct

```csharp
```



## 范例

### ABS 核心代码

```c#
Console.WriteLine("*** Vector.ABS ***");
//define data
short[] dataI16 = new short[] { -2, -1, 0, 1, 2 };
int[] dataInt = new int[] { -234567, -123456, 0, 123456, 234567 };
float[] dataFloat = new float[] { (float)-2.1234567890123456, (float)-1.1234567890123456, 0, (float)1.1234567890123456, (float)2.1234567890123456 };
double[] dataDouble = new double[] { -2.1234567890123456, -1.1234567890123456, 0, 1.1234567890123456, 2.1234567890123456 };

//A入B出
double[] absDouble = new double[dataDouble.Length];
Vector.ArrayAbs(dataDouble, absDouble);
Console.Write("参数输出: ");
Console.WriteLine(String.Join(", ", absDouble));

//A入返回数组
double[] absDoubleReturn = Vector.GetArrayAbs(dataDouble);
Console.Write("数组返回: ");
Console.WriteLine(String.Join(", ", absDoubleReturn));

//** A入A出，原位计算 **
Console.WriteLine("* 原位计算 *");
//short原位ABS
Vector.ArrayAbs(dataI16);
Console.Write("short[]:  ");
Console.WriteLine(String.Join(", ", dataI16));

//int原位ABS
Vector.ArrayAbs(dataInt);
Console.Write("int[]:    ");
Console.WriteLine(String.Join(", ", dataInt));

//float原位ABS
Vector.ArrayAbs(dataFloat);
Console.Write("float[]:   ");
Console.WriteLine(String.Join(", ", dataFloat));

//double 原位ABS
Vector.ArrayAbs(dataDouble);
Console.Write("double[]: ");
Console.WriteLine(String.Join(", ", dataDouble));

/* output:
参数输出: 2.12345678901235, 1.12345678901235, 0, 1.12345678901235, 2.12345678901235
数组返回: 2.12345678901235, 1.12345678901235, 0, 1.12345678901235, 2.12345678901235
* 原位计算 *
short[]:  2, 1, 0, 1, 2
int[]:    234567, 123456, 0, 123456, 234567
float[]:   2.123457, 1.123457, 0, 1.123457, 2.123457
double[]: 2.12345678901235, 1.12345678901235, 0, 1.12345678901235, 2.12345678901235
*/
```



### Add (Array+Array) 核心代码

```csharp

```



### Add (Array+Value) 核心代码

```csharp

```



### ArrayEqual 核心代码

```csharp

```



### RealToComplex 核心代码

```csharp
/// <summary>
/// 获取 Complex 数组实部
/// </summary>
public static void GetComplexReal(Complex[] a,double []real)
{
    GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
    IntPtr a_address = a_GC.AddrOfPinnedObject();

    ippsReal_64fc(a_address, real, a.Length);

    a_GC.Free();
}

/// <summary>
/// 获取 Complex 数组实部
/// </summary>
public static double[] GetComplexReal(Complex[] a)
{
    double[] real = new double[a.Length];
    GetComplexReal(a, real);
    return real;
}

/// <summary>
/// 获取 Complex32 数组实部
/// </summary>
public static void GetComplexReal(Complex32[] a, float[] real)
{
    GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
    IntPtr a_address = a_GC.AddrOfPinnedObject();

    ippsReal_32fc(a_address, real, a.Length);

    a_GC.Free();
}

/// <summary>
/// 获取 Complex32 数组实部
/// </summary>
public static float[] GetComplexReal(Complex32[] a)
{
    float[] real = new float[a.Length];
    GetComplexReal(a, real);
    return real;
}
```

### ComplexToRealImage 核心代码

```csharp
/// <summary>
/// 获取 Complex 数组实部虚部
/// </summary>
public static void ComplexToRealImage(Complex[] a, double[] real, double[] image)
{
    GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
    IntPtr a_address = a_GC.AddrOfPinnedObject();

    ippsCplxToReal_64fc(a_address, real, image, a.Length);

    a_GC.Free();
}

/// <summary>
/// 获取 Complex32 数组实部虚部
/// </summary>
public static void ComplexToRealImage(Complex32[] a, float[] real, float[] image)
{
    GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
    IntPtr a_address = a_GC.AddrOfPinnedObject();

    ippsCplxToReal_32fc(a_address, real, image, a.Length);

    a_GC.Free();
}

/// <summary>
/// 获取 Complex 数组实部虚部
/// </summary>
public static void ComplexToRealImage(double[] magitude, double[] phase, double[] real, double[] image)
{
    ippsPolarToCart_64f(magitude, phase, real, image, magitude.Length);
}

/// <summary>
/// 获取 Complex32 数组实部虚部
/// </summary>
public static void ComplexToRealImage(float[] magitude, float[] phase, float[] real, float[] image)
{
    ippsPolarToCart_32f(magitude, phase, real, image, magitude.Length);
}
```



### PolarToComplex 核心代码



### Sub 核心代码

```csharp
/// <summary>
/// I16 数组减法
/// inout = inout - b
/// </summary>
internal static void ArraySub(short[] inout, short[] b)
{
    ippsSub_16s_I(b, inout, inout.Length);
}

/// <summary>
/// I16 数组减法
/// output = a - b
/// </summary>
internal static void ArraySub(short[] a, short[] b, short[] output)
{
    ippsSub_16s(b, a, output, a.Length);
}

/// <summary>
/// double 数组减法
/// inout = inout - b
/// </summary>
internal static void ArraySub(double[] inout, double[] b)
{
    ippsSub_64f_I(b, inout, inout.Length);
}

/// <summary>
/// double 数组减法
/// output = a - b
/// </summary>
internal static void ArraySub(double[] a, double[] b, double[] output)
{
    ippsSub_64f(b, a, output, a.Length);
}

/// <summary>
/// float 数组减法
/// inout = inout - b
/// </summary>
internal static void ArraySub(float[] inout, float[] b)
{
    ippsSub_32f_I(b, inout, inout.Length);
}

/// <summary>
/// float 数组减法
/// output = a - b
/// </summary>
internal static void ArraySub(float[] a, float[] b, float[] output)
{
    ippsSub_32f(b, a, output, a.Length);
}

/// <summary>
/// Complex 数组减法
/// inout = inout - b
/// </summary>
internal static void ArraySub(Complex[] inout, Complex[] b)
{
    GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
    IntPtr inout_address = inout_GC.AddrOfPinnedObject();
    GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
    IntPtr b_address = b_GC.AddrOfPinnedObject();

    ippsSub_64fc_I(b_address, inout_address, inout.Length);

    inout_GC.Free();
    b_GC.Free();
}

/// <summary>
/// Complex 数组减法
/// output = a - b
/// </summary>
internal static void ArraySub(Complex[] a, Complex[] b, Complex[] output)
{
    GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
    IntPtr a_address = a_GC.AddrOfPinnedObject();
    GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
    IntPtr b_address = b_GC.AddrOfPinnedObject();
    GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
    IntPtr output_address = output_GC.AddrOfPinnedObject();

    ippsSub_64fc(b_address, a_address, output_address, a.Length);

    a_GC.Free();
    b_GC.Free();
    output_GC.Free();
}

/// <summary>
/// Complex32 数组减法
/// inout = inout - b
/// </summary>
internal static void ArraySub(Complex32[] inout, Complex32[] b)
{
    GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
    IntPtr inout_address = inout_GC.AddrOfPinnedObject();
    GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
    IntPtr b_address = b_GC.AddrOfPinnedObject();

    ippsSub_32fc_I(b_address, inout_address, inout.Length);

    inout_GC.Free();
    b_GC.Free();
}

/// <summary>
/// Complex32 数组减法
/// output = a - b
/// </summary>
internal static void ArraySub(Complex32[] a, Complex32[] b, Complex32[] output)
{
    GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
    IntPtr a_address = a_GC.AddrOfPinnedObject();
    GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
    IntPtr b_address = b_GC.AddrOfPinnedObject();
    GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
    IntPtr output_address = output_GC.AddrOfPinnedObject();

    ippsSub_32fc(b_address, a_address, output_address, a.Length);

    a_GC.Free();
    b_GC.Free();
    output_GC.Free();
}
```



### Devision 核心代码

```csharp
/// <summary>
/// double 数组除法
/// inout = inout / b
/// </summary>
internal static void ArrayDivision(double[] inout, double[] b)
{
    ippsDiv_64f_I(b, inout, inout.Length);
}

/// <summary>
/// double 数组除法
/// output = a / b
/// </summary>
internal static void ArrayDivision(double[] a, double[] b, double[] output)
{
    ippsDiv_64f(b, a, output, a.Length);
}

/// <summary>
/// float 数组除法
/// inout = inout / b
/// </summary>
internal static void ArrayDivision(float[] inout, float[] b)
{
    ippsDiv_32f_I(b, inout, inout.Length);
}

/// <summary>
/// float 数组除法
/// output = a / b
/// </summary>
internal static void ArrayDivision(float[] a, float[] b, float[] output)
{
    ippsDiv_32f(b, a, output, a.Length);
}

/// <summary>
/// Complex 数组除法
/// inout = inout / b
/// </summary>
internal static void ArrayDivision(Complex[] inout, Complex[] b)
{
    GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
    IntPtr inout_address = inout_GC.AddrOfPinnedObject();
    GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
    IntPtr b_address = b_GC.AddrOfPinnedObject();

    ippsDiv_64fc_I(b_address, inout_address, inout.Length);

    inout_GC.Free();
    b_GC.Free();
}

/// <summary>
/// Complex 数组除法
/// output = a / b
/// </summary>
internal static void ArrayDivision(Complex[] a, Complex[] b, Complex[] output)
{
    GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
    IntPtr a_address = a_GC.AddrOfPinnedObject();
    GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
    IntPtr b_address = b_GC.AddrOfPinnedObject();
    GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
    IntPtr output_address = output_GC.AddrOfPinnedObject();

    ippsDiv_64fc(b_address,a_address, output_address, a.Length);

    a_GC.Free();
    b_GC.Free();
    output_GC.Free();
}

/// <summary>
/// Complex32 数组除法
/// inout = inout / b
/// </summary>
internal static void ArrayDivision(Complex32[] inout, Complex32[] b)
{
    GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
    IntPtr inout_address = inout_GC.AddrOfPinnedObject();
    GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
    IntPtr b_address = b_GC.AddrOfPinnedObject();

    ippsDiv_32fc_I(b_address, inout_address, inout.Length);

    inout_GC.Free();
    b_GC.Free();
}

/// <summary>
/// Complex32 数组除法
/// output = a / b
/// </summary>
internal static void ArrayDivision(Complex32[] a, Complex32[] b, Complex32[] output)
{
    GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
    IntPtr a_address = a_GC.AddrOfPinnedObject();
    GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
    IntPtr b_address = b_GC.AddrOfPinnedObject();
    GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
    IntPtr output_address = output_GC.AddrOfPinnedObject();

    ippsDiv_32fc(b_address, a_address, output_address, a.Length);

    a_GC.Free();
    b_GC.Free();
    output_GC.Free();
}
```



### Scale 核心代码

```csharp
/// <summary>
/// I16 数组乘以常数
/// inout = inout * b
/// </summary>
internal static void ArrayScale(short[] inout, short b)
{
    ippsMulC_16s_I(b, inout, inout.Length);
}

/// <summary>
/// I16 数组乘以常数
/// output = a * b
/// </summary>
internal static void ArrayScale(short[] a, short b, short[] output)
{
    Vector.ArrayCopy(output, a);
    ippsMulC_16s_I(b, output, output.Length);
}

/// <summary>
/// double数组乘以常数
/// inout = inout * b
/// </summary>
internal static void ArrayScale(double[] inout, double b)
{
    ippsMulC_64f_I(b, inout, inout.Length);
}

/// <summary>
/// double数组乘以常数
/// output = a * b
/// </summary>
internal static void ArrayScale(double[] a, double b, double[] output)
{
    ippsMulC_64f(a, b, output, a.Length);
}

/// <summary>
/// float数组乘以常数
/// inout = inout * b
/// </summary>
internal static void ArrayScale(float[] inout, float b)
{
    ippsMulC_32f_I(b, inout, inout.Length);
}

/// <summary>
/// float数组乘以常数
/// output = a * b
/// </summary>
internal static void ArrayScale(float[] a, float b, float[] output)
{
    ippsMulC_32f(a, b, output, a.Length);
}

/// <summary>
/// Complex数组乘以常数
/// inout = inout * b
/// </summary>
internal static void ArrayScale(Complex[] inout, Complex b)
{
    GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
    IntPtr inout_address = inout_GC.AddrOfPinnedObject();

    ippsMulC_64fc_I(b, inout_address, inout.Length);

    inout_GC.Free();
}

/// <summary>
/// Complex数组乘以常数
/// output = a * b
/// </summary>
internal static void ArrayScale(Complex[] a, Complex b, Complex[] output)
{
    GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
    IntPtr a_address = a_GC.AddrOfPinnedObject();
    GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
    IntPtr output_address = output_GC.AddrOfPinnedObject();

    ippsMulC_64fc(a_address, b, output_address, a.Length);

    a_GC.Free();
    output_GC.Free();
}

/// <summary>
/// Comple32数组乘以常数
/// inout = inout * b
/// </summary>
internal static void ArrayScale(Complex32[] inout, Complex32 b)
{
    GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
    IntPtr inout_address = inout_GC.AddrOfPinnedObject();

    ippsMulC_32fc_I(b, inout_address, inout.Length);

    inout_GC.Free();
}

/// <summary>
/// Comple32数组乘以常数
/// output = a * b
/// </summary>
internal static void ArrayScale(Complex32[] a, Complex32 b, Complex32[] output)
{
    GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
    IntPtr a_address = a_GC.AddrOfPinnedObject();
    GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
    IntPtr output_address = output_GC.AddrOfPinnedObject();

    ippsMulC_32fc(a_address, b, output_address, a.Length);

    a_GC.Free();
    output_GC.Free();
}
```



### Multiplication 核心代码

```csharp
/// <summary>
/// I16 数组乘法
/// inout = inout * b
/// </summary>
internal static void ArrayMulti(short[] inout, short[] b)
{
    ippsMul_16s_I(b, inout, inout.Length);
}

/// <summary>
/// I16 数组乘法
/// output = a * b
/// </summary>
internal static void ArrayMulti(short[] a, short[] b, short[] output)
{
    ippsMul_16s(a, b, output, a.Length);
}

/// <summary>
/// double 数组乘法
/// inout = inout * b
/// </summary>
internal static void ArrayMulti(double[] inout, double[] b)
{
    ippsMul_64f_I(b, inout, inout.Length);
}

/// <summary>
/// double 数组乘法
/// output = a * b
/// </summary>
internal static void ArrayMulti(double[] a, double[] b, double[] output)
{
    ippsMul_64f(a, b, output, a.Length);
}

/// <summary>
/// float 数组乘法
/// inout = inout * b
/// </summary>
internal static void ArrayMulti(float[] inout, float[] b)
{
    ippsMul_32f_I(b, inout, inout.Length);
}

/// <summary>
/// float 数组乘法
/// output = a * b
/// </summary>
internal static void ArrayMulti(float[] a, float[] b, float[] output)
{
    ippsMul_32f(a, b, output, a.Length);
}

/// <summary>
/// Complex 数组乘法
/// inout = inout * b
/// </summary>
internal static void ArrayMulti(Complex[] inout, Complex[] b)
{
    GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
    IntPtr inout_address = inout_GC.AddrOfPinnedObject();
    GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
    IntPtr b_address = b_GC.AddrOfPinnedObject();

    ippsMul_64fc_I(b_address, inout_address, inout.Length);

    inout_GC.Free();
    b_GC.Free();
}

/// <summary>
/// Complex 数组乘法
/// output = a * b
/// </summary>
internal static void ArrayMulti(Complex[] a, Complex[] b, Complex[] output)
{
    GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
    IntPtr a_address = a_GC.AddrOfPinnedObject();
    GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
    IntPtr b_address = b_GC.AddrOfPinnedObject();
    GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
    IntPtr output_address = output_GC.AddrOfPinnedObject();

    ippsMul_64fc(a_address, b_address, output_address, a.Length);

    a_GC.Free();
    b_GC.Free();
    output_GC.Free();
}

/// <summary>
/// Complex32 数组乘法
/// inout = inout * b
/// </summary>
internal static void ArrayMulti(Complex32[] inout, Complex32[] b)
{
    GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
    IntPtr inout_address = inout_GC.AddrOfPinnedObject();
    GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
    IntPtr b_address = b_GC.AddrOfPinnedObject();

    ippsMul_32fc_I(b_address, inout_address, inout.Length);

    inout_GC.Free();
    b_GC.Free();
}

/// <summary>
/// Complex32 数组乘法
/// output = a * b
/// </summary>
internal static void ArrayMulti(Complex32[] a, Complex32[] b, Complex32[] output)
{
    GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
    IntPtr a_address = a_GC.AddrOfPinnedObject();
    GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
    IntPtr b_address = b_GC.AddrOfPinnedObject();
    GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
    IntPtr output_address = output_GC.AddrOfPinnedObject();

    ippsMul_32fc(a_address, b_address, output_address, a.Length);

    a_GC.Free();
    b_GC.Free();
    output_GC.Free();
}
```



### DotProduct 核心代码

```csharp
/// <summary>
/// 对 float 数组求点积
/// </summary>
public static float ArrayDotProduct(float[] a, float[] b)
{
    float dotProduct;
    ippsDotProd_32f(a, b, a.Length, out dotProduct);
    return dotProduct;
}

/// <summary>
/// 对 double 数组求点积
/// </summary>
public static double ArrayDotProduct(double[] a, double[] b)
{
    double dotProduct;
    ippsDotProd_64f(a, b, a.Length, out dotProduct);
    return dotProduct;
}

/// <summary>
/// 对 Complex32 数组求点积
/// </summary>
public static Complex32 ArrayDotProduct(Complex32[] a, Complex32[] b)
{
    Complex32 dotProduct;

    GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
    IntPtr a_address = a_GC.AddrOfPinnedObject();

    GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
    IntPtr b_address = b_GC.AddrOfPinnedObject();

    ippsDotProd_32fc(a_address, b_address, a.Length, out dotProduct);

    a_GC.Free();
    b_GC.Free();

    return dotProduct;
}

/// <summary>
/// 对 Complex 数组求点积
/// </summary>
public static Complex ArrayDotProduct(Complex[] a, Complex[] b)
{
    Complex dotProduct;

    GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
    IntPtr a_address = a_GC.AddrOfPinnedObject();

    GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
    IntPtr b_address = b_GC.AddrOfPinnedObject();

    ippsDotProd_64fc(a_address, b_address, a.Length, out dotProduct);

    a_GC.Free();
    b_GC.Free();

    return dotProduct;
}

/// <summary>
/// 对 Complex32 数组求点积
/// return =  a dotProduct b*
/// </summary>
public static Complex32 ArrayDotProductConj(Complex32[] a, Complex32[] b)
{
    Complex32 dotProduct;

    GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
    IntPtr a_address = a_GC.AddrOfPinnedObject();

    Complex32[] bConj = Vector.GetComplexConjugation(b);
    GCHandle bConj_GC = GCHandle.Alloc(bConj, GCHandleType.Pinned);
    IntPtr bConj_address = bConj_GC.AddrOfPinnedObject();

    ippsDotProd_32fc(a_address, bConj_address, a.Length, out dotProduct);

    a_GC.Free();
    bConj_GC.Free();

    return dotProduct;
}

/// <summary>
/// 对 Complex 数组求点积
/// return =  a dotProduct b*
/// </summary>
public static Complex ArrayDotProductConj(Complex[] a, Complex[] b)
{
    Complex dotProduct;

    GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
    IntPtr a_address = a_GC.AddrOfPinnedObject();

    Complex[] bConj = Vector.GetComplexConjugation(b);
    GCHandle bConj_GC = GCHandle.Alloc(bConj, GCHandleType.Pinned);
    IntPtr bConj_address = bConj_GC.AddrOfPinnedObject();

    ippsDotProd_64fc(a_address, bConj_address, a.Length, out dotProduct);

    a_GC.Free();
    bConj_GC.Free();

    return dotProduct;
}
```



### Transpose 核心代码

```csharp
/// <summary>
/// 二维数组转置
/// </summary>
public static void ArrayTranspose<T>(T[][] input, T[][] output)
{
    int row = input.Length;
    int colum = input[0].Length;

    for (int i = 0; i < row; i++)
    {
        for (int j = 0; j < colum; j++)
        {
            output[j][i] = input[i][j];
        }
    }
}
```

