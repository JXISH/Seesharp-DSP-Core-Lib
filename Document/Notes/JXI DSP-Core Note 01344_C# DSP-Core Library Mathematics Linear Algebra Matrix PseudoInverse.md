# JXI DSP-Core Note 01344_C# DSP-Core Library

## Matrix.PseudoInverse

**Author:** zliao

**Github Username:** A1020A

**Date:** Jun-30-2023

## PseudoInverse 在Solution Explore 中的显示

![DSP-Core Math Matrix PseudoInverse Place](.\Resources\Note 01344\DSP-Core Math Matrix PseudoInverse Place.PNG)

## PseudoInverse方法说明

#### （1）PseudoInverse方法重载一

**方法申明：**

```c#
/// <summary>
/// 方阵伪逆
/// </summary>
public void PseudoInverse()
```

**功能描述：**对当前矩阵进行伪逆

**输入参数：**无



#### （1）PseudoInverse方法重载二

**方法申明：**

```c#
/// <summary>
/// 方阵伪逆
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="input">伪逆运算的目标矩阵</param>
/// <returns>伪逆矩阵</returns>
public static Matrix<T> PseudoInverse(Matrix<T> input)
```

**功能描述：**对输入的矩阵进行转置，静态方法

**输入参数：**

- input：输入矩阵，数据类型：多态矩阵