# Numerics: Vector Math Operations Examples (2)

## 数组操作范例核心代码

控制台无法正常输出中文，将一些中文输出换成了英文：

* 参数输出 --> Argument Output
* 数组返回 --> Return Output
* 原位计算 --> In-Place Calculation

### ABS

求数组中函数的绝对值：

```csharp
Console.WriteLine("*** Vector.Abs ***");
//define data
short[] dataI16 = new short[] { -2, -1, 0, 1, 2 };
int[] dataInt = new int[] { -234567, -123456, 0, 123456, 234567 };
float[] dataFloat = new float[] { (float)-2.1234567890123456, (float)-1.1234567890123456, 0, (float)1.1234567890123456, (float)2.1234567890123456 };
double[] dataDouble = new double[] { -2.1234567890123456, -1.1234567890123456, 0, 1.1234567890123456, 2.1234567890123456 };

//A入B出
double[] absDouble = new double[dataDouble.Length];
Vector.ArrayAbs(dataDouble, absDouble);
Console.Write("Argument output: ");
Console.WriteLine(String.Join(", ", absDouble));

//A入返回数组
double[] absDoubleReturn = Vector.GetArrayAbs(dataDouble);
Console.Write("Return output: ");
Console.WriteLine(String.Join(", ", absDoubleReturn));

//** A入A出，原位计算 **
Console.WriteLine("* In-Place Calculation *");
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
```

输出：

```csharp
/* 
*** Vector.ABS ***
Argument Output: 2.12345678901235, 1.12345678901235, 0, 1.12345678901235, 2.12345678901235
Return output: 2.12345678901235, 1.12345678901235, 0, 1.12345678901235, 2.12345678901235
* In-Place Calculation *
short[]:  2, 1, 0, 1, 2
int[]:    234567, 123456, 0, 123456, 234567
float[]:   2.123457, 1.123457, 0, 1.123457, 2.123457
double[]: 2.12345678901235, 1.12345678901235, 0, 1.12345678901235, 2.12345678901235
 */
```



### Sum

求数组函数之和：

```csharp
Console.WriteLine();
Console.WriteLine("*** Vector.Sum ***");

// define data
double[] dataADoubleSum = new double[] { -25.12, -12.12, 0, 12.10, 28.10 };

//A入, output出
double sumOutput = Vector.ArraySum(dataADoubleSum);
Console.Write("Argument output: ");
Console.WriteLine(String.Join(", ", sumOutput));
```

输出：

```csharp
/*
*** Vector.Sum ***
Argument output: 2.96
*/
```



### Square

求数组中函数的平方：

```csharp
Console.WriteLine();
Console.WriteLine("*** Vector.Square ***");

// define data
double[] dataADoubleSquare = new double[] { -2.12, -1.12, 0, 1.12, 2.12 };

//A入, output出
double[] squareOutput = new double[dataADoubleSquare.Length];
Vector.ArraySquare(dataADoubleSquare, squareOutput);
Console.Write("Argument output: ");
Console.WriteLine(String.Join(", ", squareOutput));

//A入, 返回数组
squareOutput = Vector.GetArraySquare(dataADoubleSquare);
Console.Write("Return Output: ");
Console.WriteLine(String.Join(", ", squareOutput));

//** A入A出，原位计算 **
Console.WriteLine("* In-Place Calculation *");
//double原位数组平方
Vector.ArraySquare(dataADoubleSquare);
Console.Write("dataADoubleSquare:  ");
Console.WriteLine(String.Join(", ", dataADoubleSquare));
```

输出：

```csharp
/*
*** Vector.Square ***
Argument output: 4.4944, 1.2544, 0, 1.2544, 4.4944
Return Output: 4.4944, 1.2544, 0, 1.2544, 4.4944
* In-Place Calculation *
dataADoubleSquare:  4.4944, 1.2544, 0, 1.2544, 4.4944
*/
```



### Square Root

求数组中函数的平方根：

```csharp
Console.WriteLine();
Console.WriteLine("*** Vector.Square Root ***");

// define data
double[] dataADoubleSqrt = new double[] { -2.12, -1.12, 0, 1.12, 2.12 };

//A入, output出
double[] sqrtOutput = new double[dataADoubleSqrt.Length];
Vector.ArrayRoot(dataADoubleSqrt, sqrtOutput);
Console.Write("Argument output: ");
Console.WriteLine(String.Join(", ", sqrtOutput));

//A入, 返回数组
squareOutput = Vector.GetArrayRoot(dataADoubleSqrt);
Console.Write("Return Output: ");
Console.WriteLine(String.Join(", ", sqrtOutput));

//** A入A出，原位计算 **
Console.WriteLine("* In-Place Calculation *");
//double原位数组平方
Vector.ArrayRoot(dataADoubleSqrt);
Console.Write("dataADoubleSqrt:  ");
Console.WriteLine(String.Join(", ", dataADoubleSqrt));
```

输出：

检测到输入值为负数，无法得出虚数值，返回 “NaN”。

```csharp
/*
*** Vector.Square Root ***
Argument output: NaN, NaN, 0, 1.05830052442584, 1.4560219778561
Return Output: NaN, NaN, 0, 1.05830052442584, 1.4560219778561
* In-Place Calculation *
dataADoubleSqrt:  NaN, NaN, 0, 1.05830052442584, 1.4560219778561
*/
```



### Scale

将数组中函数值扩大n倍：

```csharp
Console.WriteLine();
Console.WriteLine("*** Vector.Scale ***");

// define data
double[] dataADoubleScale = new double[] { -2.12, -1.12, 0, 1.12, 2.12 };
double[] dataCDoubleScale = new double[dataADoubleScale.Length];
double scaleValue = 3.5;

//A,B入C出
Vector.ArrayScale<double>(dataADoubleScale, scaleValue, dataCDoubleScale);
Console.WriteLine("Argument output:");
Console.WriteLine(String.Join(", ", dataCDoubleScale));

//A,B入返回数组
double[] dataDoubleScaleReturn = Vector.GetArrayScale<double>(dataADoubleScale, scaleValue);
Console.Write("Return Output: ");
Console.WriteLine(String.Join(", ", dataDoubleScaleReturn));

//** A,B入A出，原位计算 **
Console.WriteLine("* In-Place Calculation *");
//A,B入A出
Vector.ArrayScale<double>(dataADoubleScale, scaleValue);
Console.Write("dataADoubleScale: ");
Console.WriteLine(String.Join(", ", dataADoubleScale));
```

输出：

```csharp
/*
*** Vector.Scale ***
Argument output:
-7.42, -3.92, 0, 3.92, 7.42
Return Output: -7.42, -3.92, 0, 3.92, 7.42
* In-Place Calculation *
dataADoubleScale: -7.42, -3.92, 0, 3.92, 7.42
*/
```



### Dot Product

求两个数组的点积：

```csharp
Console.WriteLine();
Console.WriteLine("*** Vector.DotProduct ***");

// define data
double[] dataADoubleDotP = new double[] { -2.12, -1.12, 0, 1.12, 2.12 };
double[] dataBDoubleDotP = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 };

//A,B入C出
double doubleDotP = Vector.ArrayDotProduct(dataADoubleDotP, dataBDoubleDotP);
Console.Write("Argument output: ");
Console.WriteLine(String.Join(", ", doubleDotP));
```

输出：

```csharp
/*
*** Vector.DotProduct ***
Argument output: 10.72
*/
```



