# Numerics: Vector Converter

### 介绍

可转换数组的类型，

如使用 ConvertToDouble(int[] input) 将int类型的数组转换为double类型的数组。

这批范例采用.NET framework，Windows console（控制台）的模式。运行通过Console.Write/WriteLine显示在弹出控制台窗口，通过Console.ReadKey()等待一个按键结束并关闭控制台。

## 实数类型之间转换

```csharp
/// <summary>
/// 将 short 数组转换为 int 数组
/// </summary>
internal static void ArrayConvert(short[] input, int[] output)

/// <summary>
/// 将 short 数组转换为 float 数组
/// </summary>
internal static void ArrayConvert(short[] input, float[] output)

/// <summary>
/// 将 int 数组转换为 float 数组
/// </summary>
internal static void ArrayConvert(int[] input, float[] output)

/// <summary>
/// 将 int 数组转换为 double 数组
/// </summary>
internal static void ArrayConvert(int[] input, double[] output)

/// <summary>
/// 将 float 数组转换为 double 数组
/// </summary>
internal static void ArrayConvert(float[] input, double[] output)

/// <summary>
/// 将 double 数组转换为 float 数组
/// </summary>
internal static void ArrayConvert(double[] input, float[] output)

/// <summary>
/// 将 double 数组转换为 int 数组
/// </summary>
internal static void ArrayConvert(double[] input, int[] output)

/// <summary>
/// 将 float 数组转换为 int 数组
/// </summary>
internal static void ArrayConvert(float[] input, int[] output)

/// <summary>
/// 将 float 数组转换为 short 数组
/// </summary>
internal static void ArrayConvert(float[] input, short[] output)

/// <summary>
/// 将 int 数组转换为 short 数组
/// </summary>
internal static void ArrayConvert(int[] input, short[] output)
```



## 各种类型转化为复数

```csharp
/// 将 short 数组转换为 Complex32 数组
public static void ArrayConvert(short[] input, Complex32[] output)

/// 将 Complex32 数组转换为 Complex 数组
public static void ArrayConvert(Complex32[] input, Complex[] output)

/// 将 Complex 数组转换为 Complex32 数组
public static void ArrayConvert(Complex[] input, Complex32[] output)

/// 将 Complex32 数组转换为short数组
public static void ArrayConvert(Complex32[] input, short[] output)
```



## 各种类型转化为 Short (I16)

```csharp
/// 将 I32 数组转换为 I16 数组
public static short[] ConvertToShort(int[] input)

/// 将 I32 数组转换为 I16 数组
public static void ConvertToShort(int[] input, short[] output)

/// 将 float 数组转换为 I16 数组
public static short[] ConvertToShort(float[] input)

/// 将 float 数组转换为 I16 数组
public static void ConvertToShort(float[] input, short[] output)

/// 将 Complex32 数组转换为 I16 数组
public static short[] ConvertToShort(Complex32[] input)

/// 将 Complex32 数组转换为 I16 数组
public static void ConvertToShort(Complex32[] input, short[] output)
```



## 各种类型转化为 I32

```csharp
/// 将 double 数组转换为 I32 数组
public static int[] ConvertToInt(double[] input)

/// 将 double 数组转换为 I32 数组
public static void ConvertToInt(double[] input, int[] output)

/// 将 float 数组转换为 int 数组
public static int[] ConvertToInt(float[] input)

/// 将 float 数组转换为 int 数组
public static void ConvertToInt(float[] input, int[] output)

/// 将 I16 数组转换为 I32 数组
public static int[] ConvertToInt(short[] input)

/// 将 I16 数组转换为 float 数组
public static void ConvertToInt(short[] input, int[] output)
```



## 各种类型转化为 Float

```csharp
/// 将 I16 数组转换为 float 数组
public static float[] ConvertToFloat(short[] input)

/// 将 I16 数组转换为 float 数组
public static void ConvertToFloat(short[] input, float[] output)

/// 将 I32 数组转换为 float 数组
public static float[] ConvertToFloat(int[] input)

/// 将 I32 数组转换为 float 数组
public static void ConvertToFloat(int[] input, float[] output)

/// 将 double 数组转换为 float 数组
public static float[] ConvertToFloat(double[] input)

/// 将 double 数组转换为 float 数组
public static void ConvertToFloat(double[] input, float[] output)
```



## 各种类型转化为 Double

```csharp
/// 将 I32 数组转换为 double 数组
public static double[] ConvertToDouble(int[] input)

/// 将 I32 数组转换为 double 数组
public static void ConvertToDouble(int[] input, double[] output)

/// 将 float 数组转换为 double 数组
public static double[] ConvertToDouble(float[] input)

/// 将 float 数组转换为 double 数组
public static void ConvertToDouble(float[] input, double[] output)
```



## 不同复数类型之间转换

### Complex32 => Complex

```csharp
/// 将 Complex32 数组转换为 Complex 数组
public static Complex[] ConvertToComplex(Complex32[] input)

/// 将 Complex32 数组转换为 Complex 数组
public static void ConvertToComplex(Complex32[] input, Complex[] output)
```

### Complex => Complex32

```csharp
/// 将 Complex 数组转换为 Complex32 数组
public static Complex32[] ConvertToComplex32 (Complex[] input)

/// 将 Complex 数组转换为 Complex32 数组
public static void ConvertToComplex32(Complex[] input, Complex32[] output)

/// 将 short 数组转换为 Complex32 数组
public static Complex32[] ConvertToComplex32 (short[] input)

/// 将 short 数组转换为 Complex32 数组
public static void ConvertToComplex32(short[] input, Complex32[] output)
```

