# JXI DSP-Core Note 01343_C# DSP-Core Library

# Mathematics: Linear Algebra Matrix

**Author:** Peter Park

**Date:** Jul-26-2022

## 1. Matrix类在Solution Explorer中的显示

<img src="Resources/Note 01343/path_of_Matrix.jpg" alt="path_of_Matrix" style="zoom:80%;" />

Matrix主体类的定义位置的路径为：

*Core Library\Mathematics\Source\SeeSharpTools.JXI.Mathematics\Linear Algebra\Matrix*

## 2. Matrix类的构造函数及重载

### 构造函数列表及说明

```c#
public Matrix(int row, int colum)
/// <summary>
/// 功能描述：根据输入的行列信息构造一个对应大小的零矩阵
/// 参数说明：row：构建矩阵的行数，colum：矩阵的列数
/// </summary>
    
public Matrix(T[,] data, bool symmetric = false)
/// <summary>
/// 功能描述：根据输入的二维数组的数据构建一个矩阵
/// 参数说明：data: 泛型二维数组，bool symmetric:是否为对称阵（布尔类型，默认为false）
/// </summary>
    
public Matrix(Matrix<T> source)
/// <summary>
/// 功能描述：根据输入的Matrix<T>类的引用变量source对应的实例，构建一个相同的实例
/// 参数说明：source：Matrix<T>类的引用变量
/// </summary>

public Matrix(T[][] data, bool symmetric = false)
/// <summary>
/// 功能描述：根据输入的二维数组的数据构建一个矩阵
/// 参数说明：data: 以数组为元素的数组，bool symmetric:是否为对称阵（布尔类型，默认为false）
/// </summary>
    
public Matrix(T[] diagonal)
/// <summary>
/// 功能描述：根据一维数组diagonal的元素，构造相应的对角矩阵
/// 参数说明：diagnal:一维泛型数组，也是对角元素
/// </summary>

```

### 构造函数相关代码示例：

```C#
static void Main(string[] args)
        {
    		//创建二维数组，并利用构造函数对Matrix类的mat进行初始化
            double[,] data = new double[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            //double[][] data2 = new double[3][] { new double[3], new double[3],                                                            new double[3]};
            Matrix<double> mat = new Matrix<double>(data);
    
    		//利用构造函数，完成mat2对mat的拷贝
            Matrix<double> mat2 = new Matrix<double>(mat);
   
    		//打印mat和mat2的数据以及哈希码
            Console.WriteLine("mat:");
            PrintMatrix(mat);
            Console.WriteLine("mat's HashCode:{0}", mat.GetHashCode());

            Console.WriteLine("mat2:");
            PrintMatrix(mat2);
            Console.WriteLine("mat2's HashCode:{0}",mat2.GetHashCode());
		}

//矩阵类（double型）的打印函数
static void PrintMatrix(Matrix<double> matrix)
 		{
                int i = 0;
                int j = 0;
                for (i = 0; i < matrix.Row; i++)
                {
                    for (j = 0; j < matrix.Colum; j++)
                    {
                        Console.Write("{0} ", matrix.MatrixArray[i, j]);
                    }
                    Console.WriteLine();
                }
            }
        }

/* output:
mat:
1 2 3
4 5 6
7 8 9
mat's HashCode:46104728

mat2:
1 2 3
4 5 6
7 8 9
mat2's HashCode:12289376
*/
//由哈希码的值不同可知所创建的两个矩阵mat和mat2对应的数据存放在不同的位置
```



## 3. Matrix类的属性

### 属性列表及说明

```c#
public int Row 
/// <summary>
/// 行数
/// </summary>
    
public int Colum 
/// <summary>
/// 列数
/// </summary>
    
public T[,] MatrixArray 
/// <summary>
/// 矩阵对应的二维数组数据信息
/// </summary>    
        
public bool IsSymmetric 
/// <summary>
/// 是否是(共轭)对称阵
/// </summary>

public bool IsSquare
/// <summary>
/// 是否是方阵
/// </summary>
    
public bool IsValid 
/// <summary>
/// 是否非空
/// </summary>
```

### 属性相关的代码示例：

```C#
 static void Main(string[] args)
        {
     		//创建二维数组，并利用构造函数对Matrix类的mat进行初始化
            double[,] data = new double[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            Matrix<double> mat = new Matrix<double>(data);
			
     		//打印对象mat的相关属性值
     		//行列信息
            Console.WriteLine("size of mat: row:{0}, colum:{1}",mat.Row,mat.Colum);
     		//是否为方阵
			Console.WriteLine("mat is square: {0}", mat.IsSquare);
     		//是否非空
     		Console.WriteLine("mat is valid: {0}", mat.IsValid );
        }
/* output:
size of mat: row:3, colum:3
mat is square: True
mat is valid: True
*/
```

## 4. Matrix的方法介绍

### (1)GetColum&GetRow

**方法申明：**

```c#
 public T[] GetColum(int colum)
 public T[] GetRow(int row)
```

**功能描述**：获取矩阵某一行或某一列的数据值，并以一维数组的形式返回

**输入参数**：行序或列序（范围：0~N-1, N为矩阵总行数或列数）

**返回值**：某一行或某一列的数据值，数据类型：一维泛型数组

**方法调用举例**：

```C#
static void Main(string[] args)
        {
    		//创建二维数组和矩阵类对象mat，并利用构造函数对Matrix类的mat进行初始化
			double[,] data = new double[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            Matrix<double> mat = new Matrix<double>(data);

    		//打印mat第一行的元素，即mat.GetRow(0)的元素
            Console.Write("the elements of the first row: ");
            for (int i = 0; i < ，即mat.GetRow(0)的元素.Length; i++)
            {
                Console.Write( mat.GetRow(0).GetValue(i));
                Console.Write(" ");
            }

            Console.WriteLine();
			
    		Console.Write("the elements of the first colum: ");
    		//打印第一列元素，即mat.GetColum(1)的元素
            foreach (var item in mat.GetColum(1))
            {
                Console.Write("{0} ",item);
            }
        }
/* output:
the elements of the first row: 1 2 3
the elements of the first colum: 2 5 8
*/
```



### (2)SetColum&SetRow

**方法申明**：

```C#
public void SetColum(int colum, T[] columdata)
public void SetRow(int row,T[] rowdata)
```

**功能描述**：为矩阵的某一行或者某一列赋值

**输入参数**：

* 行序或列序（范围：0~N-1, N为矩阵总行数或列数）
* 用于赋值的一维数组

**方法调用举例**：

```C#
static void Main(string[] args)
        {
            //创建二维数组和矩阵类对象mat，并利用构造函数对Matrix类的mat进行初始化
            double[,] data = new double[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            Matrix<double> mat = new Matrix<double>(data);
    		//利用矩阵mat的行列属性，构造一个与之等大的空矩阵mat2
            Matrix<double> mat2 = new Matrix<double>(mat.Row,mat.Colum);
			
    		//打印两个矩阵数据
            Console.WriteLine("before assignment:");
            Console.WriteLine("mat:");
            PrintMatrix(mat);
            Console.WriteLine("mat2:");
            PrintMatrix(mat2);
			
    		//使用GetRow和SetRow方法为mat2赋值
            for (int i = 0; i < mat.Row; i++)
            {
                mat2.SetRow(i, mat.GetRow(i));
            }
    
            Console.WriteLine();
            //再次打印两个矩阵数据
            Console.WriteLine("after assignment:");
            Console.WriteLine("mat:");
            PrintMatrix(mat);
            Console.WriteLine("mat2:");
            PrintMatrix(mat2);
        }
/* output:
before assignment:
mat:     mat2:
1 2 3    0 0 0
4 5 6    0 0 0
7 8 9    0 0 0
after assignment:
mat:     mat2:
1 2 3    1 2 3
4 5 6    4 5 6
7 8 9    7 8 9
*/
```



### (3)CopyFrom

**方法申明**：

```c#
public void CopyFrom(Matrix<T> source)
```

**功能描述**：使某一矩阵完成对目标矩阵的元素值的拷贝

**输入参数**：目标矩阵类的引用变量

**方法调用举例：**

```C#
static void Main(string[] args)
        {
    		//创建二维数组data和data2作为矩阵的初始化数据    		
            double[,] data = new double[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            double[][] data2 = new double[3][] { new double[3], new double[3],                                                            new double[3] };
    		//利用构造函数并输入data和data2对Matrix类的mat和mat2进行初始化
            Matrix<double> mat = new Matrix<double>(data);
            Matrix<double> mat2 = new Matrix<double>(data2);

    		//打印复制前两矩阵元素
            Console.WriteLine("before copy:");
            Console.WriteLine("mat:");
            PrintMatrix(mat);
            Console.WriteLine("mat2:");
            PrintMatrix(mat2);
    
			//调用拷贝方法将mat拷贝到mat2
            mat2.CopyFrom(mat);
            Console.WriteLine();
			
    		//打印复制后两矩阵的元素
            Console.WriteLine("after copy:");
            Console.WriteLine("mat:");
            PrintMatrix(mat);
            Console.WriteLine("mat2:");
            PrintMatrix(mat2);
            Console.WriteLine();
        }
/* output:
before copy:
mat:     mat2:
1 2 3    0 0 0
4 5 6    0 0 0
7 8 9    0 0 0
after copy:
mat:     mat2:
1 2 3    1 2 3
4 5 6    4 5 6
7 8 9    7 8 9
*/
```



### (4)Identity

**方法申明**：

```c#
public static Matrix<T> Identity(int N)
```

**功能描述**：创建一个大小为N*N的单位矩阵

**输入参数**：创建的目标单位矩阵的行列值N

**返回值**：Matrix<T>类的单位矩阵

**方法调用举例**：

```C#
Matrix<double> mat = Matrix<double>.Identity(3);
Console.WriteLine("mat:");
PrintMatrix(mat);
/* output:
理论上的输出（运行中遇到了问题，具体请看问题汇总文档）：
mat：
1 0 0
0 1 0
0 0 1
*/
```

