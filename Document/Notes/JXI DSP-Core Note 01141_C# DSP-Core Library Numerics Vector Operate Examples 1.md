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
// define data
double[] dataADoubleSort = new double[] { -2.12, 1.12, 7, -1.12, 2.12 };
double[] dataBDoubleSort = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
int[] sortIndex = new int[dataADoubleSort.Length];
// print origin
Console.Write("Original data A: ");
Console.WriteLine(String.Join(", ", dataADoubleSort));

// A入B出
Vector.ArrayAscend(dataADoubleSort, dataBDoubleSort);
Console.Write("Argument output: ");
Console.WriteLine(String.Join(", ", dataBDoubleSort));

// 返回排序结果及索引位置
Vector.ArrayAscendIndex(dataADoubleSort, dataBDoubleSort, sortIndex);
Console.Write("Sorted Argument outout: ");
Console.WriteLine(String.Join(", ", dataBDoubleSort));
Console.Write("sortedIndex Argument outout: ");
Console.WriteLine(String.Join(", ", sortIndex));

// ** 原位计算 **
Console.WriteLine("* In-Place Calculation *");
// A入A出
Vector.ArrayAscend(dataADoubleSort);
Console.Write("dataADoubleSort: ");
Console.WriteLine(String.Join(", ", dataADoubleSort));

// 原位排序并返回索引位置
dataADoubleSort = new double[] { -2.12, 1.12, 7, -1.12, 2.12 };
Vector.ArrayAscendIndex(dataADoubleSort, sortIndex);
Console.Write("Sorted Argument outout: ");
Console.WriteLine(String.Join(", ", dataADoubleSort));
Console.Write("sortedIndex Argument output: ");
Console.WriteLine(String.Join(", ", sortIndex));
```

输出：

```csharp
/* output:
*** Vector.Sort ***
Original data A: -2.12, 1.12, 7, -1.12, 2.12
Argument output: -2.12, -1.12, 1.12, 2.12, 7
Sorted Argument outout: -2.12, -1.12, 1.12, 2.12, 7
sortedIndex Argument outout: 0, 3, 1, 4, 2
* In-Place Calculation *
dataADoubleSort: -2.12, -1.12, 1.12, 2.12, 7
Sorted Argument outout: -2.12, -1.12, 1.12, 2.12, 7
sortedIndex Argument output: 0, 3, 1, 4, 2
*/
```



#### 以降序排列数组。

要降序排列数组的话，根据上述升序排序用Descend替换Ascend即可。这里就进行一次A入B出的演示。

```csharp
// A入B出
Vector.ArrayDescend(dataADoubleSort, dataBDoubleSort);
Console.Write("Ascend Argument output: ");
Console.WriteLine(String.Join(", ", dataBDoubleSort));
```

输出：

```csharp
/* output:
Decend Sort:
Ascend Argument output: 7, 2.12, 1.12, -1.12, -2.12
*/
```



### Transpose

将数组转置，一种转置方式：

A进output出。

```csharp
// define data
double[][] dataADoubleTranspose = new double[][] { new double[] { -2.12, -1.12, -0.12 },
                                                    new double[] { 1.12, 2.12, 0.12 } };
double[][] dataBDoubleTranspose = new double[3][];
dataBDoubleTranspose[0] = new double[2];
dataBDoubleTranspose[1] = new double[2];
dataBDoubleTranspose[2] = new double[2];

// A进 output出
Vector.ArrayTranspose(dataADoubleTranspose, dataBDoubleTranspose);
Console.WriteLine("Transposed Argument Output: ");
for (int i = 0; i < dataBDoubleTranspose.Length; i++)
{
    Console.WriteLine(String.Join(", ", dataBDoubleTranspose[i]));
}
```

输出：

```csharp
/* output:
*** Vector.Transpose ***
Transposed Argument Output:
-2.12, 1.12
-1.12, 2.12
-0.12, 0.12
*/
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
/* output:
*** Vector.Reverse ***
Argument output: 1.12, 2.12, 0, -4.12, -5.12
* In-Place Calculation *
dataADoubleRev 1.12, 2.12, 0, -4.12, -5.12
*/
```

