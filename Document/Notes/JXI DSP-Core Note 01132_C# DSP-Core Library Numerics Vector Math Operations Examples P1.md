# Numerics: Vector Operations Examples (1)

## 数组操作范例核心代码

反馈过控制台无法正常输出中文，将一些中文输出换成了英文：

* 参数输出 --> Argument Output
* 数组返回 --> Return Output
* 原位计算 --> In-Place Calculation

### Addition

#### 数组相加：

```csharp
//创建数组变量
double[] dataADoubleAdd = new double[] { -2.12, -1.12, 0, 1.12, 2.12 };
double[] dataBDoubleAdd = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 };

//A,B入C出
double[] dataCDoubleAdd = new double[dataADoubleAdd.Length];
Vector.ArrayAdd(dataADoubleAdd, dataBDoubleAdd, dataCDoubleAdd);
Console.Write("Argument Output: ");
Console.WriteLine(String.Join(", ", dataCDoubleAdd));

//A,B入返回数组
double[] dataDoubleAddReturn = Vector.GetArrayAdd(dataADoubleAdd, dataBDoubleAdd);
Console.Write("Return Output: ");
Console.WriteLine(String.Join(", ", dataDoubleAddReturn));

//** A,B入A出，原位计算 **
Console.WriteLine("* In-Place Calculation *");


//double原位数组相加
Vector.ArrayAdd(dataADoubleAdd, dataBDoubleAdd);
Console.Write("dataADoubleAdd:  ");
Console.WriteLine(String.Join(", ", dataADoubleAdd));

```

输出：

```csharp
/*
* *** Vector.Add ***
Argument Output: -1.12, 0.88, 3, 5.12, 7.12
Return Output: -1.12, 0.88, 3, 5.12, 7.12
* In-Place Calculation *
dataADoubleAdd:  -1.12, 0.88, 3, 5.12, 7.12
*/
```



#### 数组加常数：

```csharp
//** A,B入A出，原位计算 **
Console.WriteLine("* In-Place Calculation *");

//double原位数组加常数
Vector.ArrayAdd(dataADoubleAdd, addingValue);
Console.Write("dataADoubleAdd:  ");
Console.WriteLine(String.Join(", ", dataADoubleAdd));

```

输出：

```csharp
/*
* *** Vector.Add 1 value ***
* In-Place Calculation *
dataADouble:  7.88, 8.88, 10, 11.12, 12.12
*/
```



### Subtraction

```csharp
//创建数组变量
double[] dataADoubleSub = new double[] { -2.12, -1.12, 0, 1.12, 2.12 };
double[] dataBDoubleSub = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 };

//A,B入C出
double[] dataCDoubleSub = new double[dataADoubleSub.Length];
Vector.ArraySub(dataADoubleSub, dataBDoubleSub, dataCDoubleSub);
Console.Write("Argument Output: ");
Console.WriteLine(String.Join(", ", dataCDoubleSub));

//A,B入返回数组
double[] dataDoubleSubReturn = Vector.GetArraySub(dataADoubleSub, dataBDoubleSub);
Console.Write("Return Output: ");
Console.WriteLine(String.Join(", ", dataDoubleSubReturn));

//** A,B入A出，原位计算 **
Console.WriteLine("* In-Place Calculation *");
//double原位数组相减
Vector.ArraySub(dataADoubleSub, dataBDoubleSub);
Console.Write("dataADoubleSub:  ");
Console.WriteLine(String.Join(", ", dataADoubleSub));

```

输出：

```csharp
/*
* *** Vector.Sub ***
Argument Output: -3.12, -3.12, -3, -2.88, -2.88
Return Output: -3.12, -3.12, -3, -2.88, -2.88
* In-Place Calculation *
dataADoubleSub:  -3.12, -3.12, -3, -2.88, -2.88
*/
```



### Multiplication

```csharp
//创建数组变量
double[] dataADoubleMulti = new double[] { -2.12, -1.12, 0, 1.12, 2.12 };
double[] dataBDoubleMulti = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 };

//A,B入C出
double[] dataCDoubleMulti = new double[dataADoubleMulti.Length];
Vector.ArrayMulti<double>(dataADoubleMulti, dataBDoubleMulti, dataCDoubleMulti);
Console.Write("Argument output: ");
Console.WriteLine(String.Join(", ", dataCDoubleMulti));

//A,B入返回数组
double[] dataDoubleMultiReturn = Vector.GetArrayMulti<double>(dataADoubleMulti, dataBDoubleMulti);
Console.Write("Return Output: ");
Console.WriteLine(String.Join(", ", dataDoubleMultiReturn));

//** A,B入A出，原位计算 **
Console.WriteLine("* In-Place Calculation *");
//double原位数组相乘
Vector.ArrayMulti<double>(dataADoubleMulti, dataBDoubleMulti);
Console.Write("dataADouble:  ");
Console.WriteLine(String.Join(", ", dataADoubleMulti));
```

输出：

```csharp
/*
*** Vector.Multiplication ***
Argument output: -2.12, -2.24, 0, 4.48, 10.6
Return Output: -2.12, -2.24, 0, 4.48, 10.6
* In-Place Calculation *
dataADouble:  -2.12, -2.24, 0, 4.48, 10.6
*/
```



### Division

```csharp
//创建数组变量
double[] dataADoubleDiv = new double[] { -2.12, -1.12, 0, 1.12, 2.12 };
double[] dataBDoubleDiv = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 };

//A,B入C出
double[] dataCDoubleDiv = new double[dataADoubleDiv.Length];
Vector.ArrayDivision(dataADoubleDiv, dataBDoubleDiv, dataCDoubleDiv);
Console.Write("Argument output: ");
Console.WriteLine(String.Join(", ", dataCDoubleDiv));

//A,B入返回数组
double[] dataDoubleDivReturn = Vector.GetArrayDivision(dataADoubleDiv, dataBDoubleDiv);
Console.Write("Return Output: ");
Console.WriteLine(String.Join(", ", dataDoubleDivReturn));

//** A,B入A出，原位计算 **
Console.WriteLine("* In-Place Calculation *");
//double原位数组相除
Vector.ArrayDivision(dataADoubleDiv, dataBDoubleDiv);
Console.Write("dataADouble:  ");
Console.WriteLine(String.Join(", ", dataADoubleDiv));
```

输出：

```csharp
/*
*** Vector.Division ***
Argument output: -2.12, -0.56, 0, 0.28, 0.424
Return Output: -2.12, -0.56, 0, 0.28, 0.424
* In-Place Calculation *
dataADouble:  -2.12, -0.56, 0, 0.28, 0.424
*/
```



### Compare

```csharp
Console.WriteLine();
Console.WriteLine("*** Vector.Compare ***");

// define data
double[] dataADoubleComp = new double[] { -2.12, -1.12, 0, 1.12, 2.12 };
double[] dataBDoubleComp = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 };

//compare array
bool compared = Vector.ArrayEqual(dataADoubleComp, dataBDoubleComp);
Console.Write("Compared Result: ");
Console.WriteLine(compared);
```

输出：

```csharp
/*
*** Vector.Equal ***
Compared Result: False
*/
```

