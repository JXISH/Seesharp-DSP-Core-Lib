# Numerics: Vector Operate Examples Part 2



## 范例说明

范例位于 Numerics\Examples

本数学操作范例为：Numeric Operations Example

这批范例采用.NET framework，Windows console（控制台）的模式。运行通过Console.Write/WriteLine显示在弹出控制台窗口，通过Console.ReadKey()等待一个按键结束并关闭控制台。



## 数组操作功能范例

### Real, Image to Complex



```csharp
Console.WriteLine();
Console.WriteLine("*** Vector.RealImageToComplex ***");

// define data
double[] dataADoubleReal = new double[] { -2.12, -1.12, 0, 1.12, 2.12 };
double[] dataBDoubleReal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 };

//A,B入，C出
Complex[] complexDataRItoC1 = new Complex[dataADoubleReal.Length];
Vector.RealImageToComplex(dataADoubleReal, dataBDoubleReal, complexDataRItoC1);
Console.Write("Argument output: ");
Console.WriteLine(String.Join(", ", complexDataRItoC1));

//A,B入，返回数组
Complex[] complexDataRItoC2 = Vector.RealImageToComplex(dataADoubleReal, dataBDoubleReal);
Console.Write("Return Output: ");
Console.WriteLine(String.Join(", ", complexDataRItoC2));
```

输出：

```csharp
/* output:
*** Vector.RealImageToComplex ***
Argument output: (-2.12, 1), (-1.12, 2), (0, 3), (1.12, 4), (2.12, 5)
Return Output: (-2.12, 1), (-1.12, 2), (0, 3), (1.12, 4), (2.12, 5)
*/
```



### Complex to Real, Image



```csharp
Console.WriteLine();
Console.WriteLine("*** Vector.ComplexToRealImage ***");

// define data
Complex[] complexDataCtoRI = new Complex[] { new Complex(-2.12, -1), new Complex(1.12, 2)};
Complex32[] complex32DataCtoRI = new Complex32[] { new Complex32((float)-2.12, -1),
                                                    new Complex32((float)1.12, 2) };

//complex入，real, image出
double[] Real1A = new double[complexDataCtoRI.Length];
double[] Image1A = new double[complexDataCtoRI.Length];
Vector.ComplexToRealImage(complexDataCtoRI, Real1A, Image1A);
Console.WriteLine("Complex in; Real, Image out: ");
Console.Write("Real Argument output: ");
Console.WriteLine(String.Join(", ", Real1A));
Console.Write("Image Argument output: ");
Console.WriteLine(String.Join(", ", Image1A));

//complex32入，real, image出
float[] Real32 = new float[complex32DataCtoRI.Length];
float[] Image32 = new float[complex32DataCtoRI.Length];
Vector.ComplexToRealImage(complex32DataCtoRI, Real32, Image32);
Console.WriteLine("Complex32 in; Real, Image out: ");
Console.Write("Real Argument output: ");
Console.WriteLine(String.Join(", ", Real32));
Console.Write("Image Argument output: ");
Console.WriteLine(String.Join(", ", Image32));

//magnitude, double入，real, image出
double[] magnitudeCtoRI = new double[] {1, 2};
double[] phaseCtoRI = new double[] {0, 1.57};
double[] Real2A = new double[2];
double[] Image2A = new double[2];
Vector.ComplexToRealImage(magnitudeCtoRI, phaseCtoRI, Real2A, Image2A);
Console.WriteLine("Magnitude, Phase in; Real, Image out: ");
Console.Write("Real Argument output: ");
Console.WriteLine(String.Join(", ", Real2A));
Console.Write("Image Argument output: ");
Console.WriteLine(String.Join(", ", Image2A));
```

输出：

```csharp
/* output:
 *** Vector.ComplexToRealImage ***
Complex in; Real, Image out:
Real Argument output: -2.12, 1.12
Image Argument output: -1, 2
Complex32 in; Real, Image out:
Real Argument output: -2.12, 1.12
Image Argument output: -1, 2
Magnitude, Phase in; Real, Image out:
Real Argument output: 1, 0.00159265342146653
Image Argument output: 0, 1.99999936586367
*/
```



### Polar to Complex



```csharp
Console.WriteLine();
Console.WriteLine("*** Vector.PolarToComplex ***");

// define data
double[] magnitudePtoC = new double[] { 1, 2 };
double[] phasePtoC = new double[] { 0, 1.57 }; //0 and pi/2
Complex[] complexDataPtoC = new Complex[] { new Complex(), new Complex() };

// magnitude, phase入, complex出
Vector.PolarToComplex(magnitudePtoC, phasePtoC, complexDataPtoC);
Console.Write("Complex Argument output: ");
Console.WriteLine(String.Join(", ", complexDataPtoC));

// phase入, complex出
Vector.PolarToComplex(phasePtoC, complexDataPtoC);
Console.Write("Complex Argument output: ");
Console.WriteLine(String.Join(", ", complexDataPtoC));

// 返回complex数组:
magnitudePtoC = new double[] { 2, 3 };
phasePtoC = new double[] { 0.785, 3.141 }; //pi/4, pi
// magnitude, phase入, 返回complex data
complexDataPtoC = Vector.PolarToComplex(magnitudePtoC, phasePtoC);
Console.Write("Return output: ");
Console.WriteLine(String.Join(", ", complexDataPtoC));

// phase入, 幅度恒等于1， 返回complex data
complexDataPtoC = Vector.PolarToComplex(phasePtoC);
Console.Write("Return output: ");
Console.WriteLine(String.Join(", ", complexDataPtoC));
```

输出：

```csharp
/* output:
 * *** Vector.PolarToComplex ***
Complex Argument output: (1, 0), (0.00159265342146653, 1.99999936586367)
Complex Argument output: (1, 0), (0.000796326710733263, 0.999999682931835)
Return output: (1.4147765383344, 1.41365036221073), (-2.9999994731426, 0.00177796066529836)
Return output: (0.7073882691672, 0.706825181105366), (-0.999999824380866, 0.000592653555099454)
*/
```

