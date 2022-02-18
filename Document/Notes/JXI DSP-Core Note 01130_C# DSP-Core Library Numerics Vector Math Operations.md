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

## ABS

#### 数学操作的输出模式

1. 双参数，A入B出

   要求B定义和A类型相同，大小相同。

2. 单参数，A输入，方法返回输出数组

   减少一次B数组定义。

3. 单输入，原位计算

   当你不需要保留原来的数据时，使用这个模式，可以节约内存

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



### 核心代码

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

## Add (Array+Array)

## Add (Array + Value)

## Sub

## Devision

## Scale

## Multiplication

## DotProduct

## CrossProduct



