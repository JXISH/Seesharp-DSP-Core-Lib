# Numerics: Vector Operate Examples Part 1



## 范例说明

范例位于 Numerics\Examples

本数学操作范例为：Numeric Operations Example

这批范例采用.NET framework，Windows console（控制台）的模式。运行通过Console.Write/WriteLine显示在弹出控制台窗口，通过Console.ReadKey()等待一个按键结束并关闭控制台。



## 数组操作功能范例

### Copy

拷贝数组：

```csharp
// define data
double[] dataADoubleCopy = new double[] { -2.12, -1.12, 0, 1.12, 2.12 };
double[] dataBDoubleCopy = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 };

// A入B出
Vector.ArrayCopy(dataADoubleCopy, 2, dataBDoubleCopy, 1, 2);
Console.Write("dataBDoubleCopy: ");
Console.WriteLine(String.Join(", ", dataBDoubleCopy));

// A入B出
Vector.ArrayCopy(dataADoubleCopy, dataBDoubleCopy);
Console.Write("dataBDoubleCopy: ");
Console.WriteLine(String.Join(", ", dataBDoubleCopy));

```

输出：

```csharp
/* output:
*** Vector.Copy ***
dataBDoubleCopy: 1, 0, 1.12, 4, 5
dataBDoubleCopy: -2.12, -1.12, 0, 1.12, 2.12
*/
```



### Sort

#### 以升序排列数组。

```csharp

```

输出：

```csharp

```



#### 以降序排列数组。

```csharp

```

输出：

```csharp

```



### Transpose

将数组转置，一种转置方式：

A进output出。

```csharp

```

输出：

```csharp

```



### Reverse

将数组反转：

{1, 2, 3, 4, 5} => {5, 4, 3, 2, 1}

两种反转方式：

原位计算，A进A出；A进output出。

```csharp
// define data
double[] dataADoubleRev = new double[] { -5.12, -4.12, 0, 2.12, 1.12 };
double[] dataBDoubleRev = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 };

//A入B出
Vector.ArrayReverse(dataADoubleRev, dataBDoubleRev);
Console.Write("Argument output: ");
Console.WriteLine(String.Join(", ", dataBDoubleRev));

//** 原位计算 **
Console.WriteLine("* In-Place Calculation *");
//A入A出
Vector.ArrayReverse(dataADoubleRev);
Console.Write("dataADoubleRev ");
Console.WriteLine(String.Join(", ", dataADoubleRev));
```

输出：

```csharp
```

