using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SeeSharpTools.JXI.Numerics.NumericOperationsExample
{
    internal class NumericOperationsExample
    {
        static void Main(string[] args)
        {
            // Maths:
            #region ABS Example
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

            /* output:
             *  *** Vector.ABS ***
            Argument Output: 2.12345678901235, 1.12345678901235, 0, 1.12345678901235, 2.12345678901235
            Return output: 2.12345678901235, 1.12345678901235, 0, 1.12345678901235, 2.12345678901235
            * In-Place Calculation *
            short[]:  2, 1, 0, 1, 2
            int[]:    234567, 123456, 0, 123456, 234567
            float[]:   2.123457, 1.123457, 0, 1.123457, 2.123457
            double[]: 2.12345678901235, 1.12345678901235, 0, 1.12345678901235, 2.12345678901235
             */
            #endregion

            #region Add Example

            Console.WriteLine();
            Console.WriteLine("*** Vector.Add ***");

            //define data
            double[] dataADoubleAdd = new double[] { -2.12, -1.12, 0, 1.12, 2.12 };
            double[] dataBDoubleAdd = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 };

            //A,B入C出
            double[] dataCDoubleAdd = new double[dataADoubleAdd.Length];
            Vector.ArrayAdd(dataADoubleAdd, dataBDoubleAdd, dataCDoubleAdd);
            Console.Write("Argument output: ");
            Console.WriteLine(String.Join(", ", dataCDoubleAdd));

            //A,B入返回数组
            double[] dataDoubleAddReturn = Vector.GetArrayAdd(dataADoubleAdd, dataBDoubleAdd);
            Console.Write("Return Output: ");
            Console.WriteLine(String.Join(", ", dataDoubleAddReturn));

            //** A,B入A出，原位计算 **
            Console.WriteLine("* In-Place Calculation *");
            //double原位数组相加
            Vector.ArrayAdd(dataADoubleAdd, dataBDoubleAdd);
            Console.Write("dataADouble:  ");
            Console.WriteLine(String.Join(", ", dataADoubleAdd));

            /* output:
             * *** Vector.Add ***
            Argument Output: -1.12, 0.88, 3, 5.12, 7.12
            Return Output: -1.12, 0.88, 3, 5.12, 7.12
            * In-Place Calculation *
            dataADoubleSub:  -1.12, 0.88, 3, 5.12, 7.12
            */
            #endregion

            #region Add1Value
            Console.WriteLine();
            Console.WriteLine("*** Vector.Add 1 value ***");
            dataADoubleAdd = new double[] { -2.12, -1.12, 0, 1.12, 2.12 }; //恢复初始数值
            double addingValue = 10;
            //** A,B入A出，原位计算 **
            Console.WriteLine("* In-Place Calculation *");
            //double原位数组加常数
            Vector.ArrayAdd(dataADoubleAdd, addingValue);
            Console.Write("dataADouble:  ");
            Console.WriteLine(String.Join(", ", dataADoubleAdd));
            /*
             * *** Vector.Add 1 value ***
            * In-Place Calculation *
            dataADouble:  7.88, 8.88, 10, 11.12, 12.12
            */
            #endregion

            #region Sub Example
            Console.WriteLine();
            Console.WriteLine("*** Vector.Sub ***");

            //define data
            double[] dataADoubleSub = new double[] { -2.12, -1.12, 0, 1.12, 2.12 };
            double[] dataBDoubleSub = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 };

            //A,B入C出
            double[] dataCDoubleSub = new double[dataADoubleSub.Length];
            Vector.ArraySub(dataADoubleSub, dataBDoubleSub, dataCDoubleSub);
            Console.Write("Argument output: ");
            Console.WriteLine(String.Join(", ", dataCDoubleSub));

            //A,B入返回数组
            double[] dataDoubleSubReturn = Vector.GetArraySub(dataADoubleSub, dataBDoubleSub);
            Console.Write("Return Output: ");
            Console.WriteLine(String.Join(", ", dataDoubleSubReturn));

            //** A,B入A出，原位计算 **
            Console.WriteLine("* In-Place Calculation *");
            //double原位数组相减
            Vector.ArraySub(dataADoubleSub, dataBDoubleSub);
            Console.Write("dataADouble:  ");
            Console.WriteLine(String.Join(", ", dataADoubleSub));

            /* output:
            *** Vector.Sub ***
            Argument output: -3.12, -3.12, -3, -2.88, -2.88
            Return Output: -3.12, -3.12, -3, -2.88, -2.88
            * In-Place Calculation *
            dataADouble:  -3.12, -3.12, -3, -2.88, -2.88
            */
            #endregion

            #region Multiplication
            Console.WriteLine();
            Console.WriteLine("*** Vector.Multiplication ***");

            //define data
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

            /* output:
            *** Vector.Multiplication ***
            Argument output: -2.12, -2.24, 0, 4.48, 10.6
            Return Output: -2.12, -2.24, 0, 4.48, 10.6
            * In-Place Calculation *
            dataADouble:  -2.12, -2.24, 0, 4.48, 10.6
            */
            #endregion

            #region Division
            Console.WriteLine();
            Console.WriteLine("*** Vector.Division ***");

            //define data
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

            /* output:
            *** Vector.Division ***
            Argument output: -2.12, -0.56, 0, 0.28, 0.424
            Return Output: -2.12, -0.56, 0, 0.28, 0.424
            * In-Place Calculation *
            dataADouble:  -2.12, -0.56, 0, 0.28, 0.424
            */
            #endregion

            #region Compare
            Console.WriteLine();
            Console.WriteLine("*** Vector.Equal ***");

            // define data
            double[] dataADoubleCompare = new double[] { -2.12, -1.12, 0, 1.12, 2.12 };
            double[] dataBDoubleCompare = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 };

            //compare array
            bool compared = Vector.ArrayEqual(dataADoubleCompare, dataBDoubleCompare);
            Console.Write("Compared Result: ");
            Console.WriteLine(compared);

            /* output:
            *** Vector.Equal ***
            Compared Result: False
            */

            #endregion

            #region Square
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

            //** A,B入A出，原位计算 **
            Console.WriteLine("* In-Place Calculation *");
            //double原位数组平方
            Vector.ArraySquare(dataADoubleSquare);
            Console.Write("dataADoubleSquare:  ");
            Console.WriteLine(String.Join(", ", dataADoubleSquare));

            /* output:
            *** Vector.Square ***
            Argument output: 4.4944, 1.2544, 0, 1.2544, 4.4944
            Return Output: 4.4944, 1.2544, 0, 1.2544, 4.4944
            * In-Place Calculation *
            dataADoubleSquare:  4.4944, 1.2544, 0, 1.2544, 4.4944
            */

            #endregion

            #region Square Root
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

            //** A,B入A出，原位计算 **
            Console.WriteLine("* In-Place Calculation *");
            //double原位数组平方
            Vector.ArrayRoot(dataADoubleSqrt);
            Console.Write("dataADoubleSqrt:  ");
            Console.WriteLine(String.Join(", ", dataADoubleSqrt));

            /* output:
            *** Vector.Square Root ***
            Argument output: NaN, NaN, 0, 1.05830052442584, 1.4560219778561
            Return Output: NaN, NaN, 0, 1.05830052442584, 1.4560219778561
            * In-Place Calculation *
            dataADoubleSqrt:  NaN, NaN, 0, 1.05830052442584, 1.4560219778561
            */

            #endregion

            #region Sum
            Console.WriteLine();
            Console.WriteLine("*** Vector.Sum ***");

            // define data
            double[] dataADoubleSum = new double[] { -25.12, -12.12, 0, 12.10, 28.10 };

            //A入, output出
            double sumOutput = Vector.ArraySum(dataADoubleSum);
            Console.Write("Argument output: ");
            Console.WriteLine(String.Join(", ", sumOutput));

            /* output:
            *** Vector.Sum ***
            Argument output: 2.96
            */

            #endregion

            #region Scale Example
            Console.WriteLine();
            Console.WriteLine("*** Vector.Scale ***");

            // define data
            double[] dataADoubleScale = new double[] { -2.12, -1.12, 0, 1.12, 2.12 };
            double[] dataCDoubleScale = new double[dataADoubleScale.Length];
            double b = 3.5;

            //A,b入C出
            Vector.ArrayScale<double>(dataADoubleScale, b, dataCDoubleScale);
            Console.WriteLine("Argument output:");
            Console.WriteLine(String.Join(", ", dataCDoubleScale));

            //A,b入返回数组
            double[] dataDoubleScaleReturn = Vector.GetArrayScale<double>(dataADoubleScale, b);
            Console.Write("Return Output: ");
            Console.WriteLine(String.Join(", ", dataDoubleScaleReturn));

            //** A,b入A出，原位计算 **
            Console.WriteLine("* In-Place Calculation *");
            //A,b入A出
            Vector.ArrayScale<double>(dataADoubleScale, b);
            Console.Write("dataADoubleScale: ");
            Console.WriteLine(String.Join(", ", dataADoubleScale));

            /* output:
            *** Vector.Scale ***
            Argument output:
            -7.42, -3.92, 0, 3.92, 7.42
            Return Output: -7.42, -3.92, 0, 3.92, 7.42
            * In-Place Calculation *
            dataADoubleScale: -7.42, -3.92, 0, 3.92, 7.42
            */

            #endregion

            #region Dot Product
            Console.WriteLine();
            Console.WriteLine("*** Vector.DotProduct ***");
            
            // define data
            double[] dataADoubleDotP = new double[] { -2.12, -1.12, 0, 1.12, 2.12 };
            double[] dataBDoubleDotP = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 };

            //A,B入C出
            double doubleDotP = Vector.ArrayDotProduct(dataADoubleDotP, dataBDoubleDotP);
            Console.Write("Argument output: ");
            Console.WriteLine(String.Join(", ", doubleDotP));

            /* output:
            *** Vector.DotProduct ***
            Argument output: 10.72
            */

            #endregion

            // Operate:
            #region Copy
            Console.WriteLine();
            Console.WriteLine("*** Vector.Copy ***");

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

            /* output:
            *** Vector.Copy ***
            dataBDoubleCopy: 1, 0, 1.12, 4, 5
            dataBDoubleCopy: -2.12, -1.12, 0, 1.12, 2.12
            */

            #endregion

            #region Sort
            Console.WriteLine();
            Console.WriteLine("*** Vector.Sort ***");

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

            #endregion

            #region Transpose
            Console.WriteLine();
            Console.WriteLine("*** Vector.Transpose ***");

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

            /* output:
            *** Vector.Transpose ***
            Transposed Argument Output:
            -2.12, 1.12
            -1.12, 2.12
            -0.12, 0.12
            */

            #endregion

            #region Reverse
            Console.WriteLine();
            Console.WriteLine("*** Vector.Reverse ***");

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

            /* output:
            *** Vector.Reverse ***
            Argument output: 1.12, 2.12, 0, -4.12, -5.12
            * In-Place Calculation *
            dataADoubleRev 1.12, 2.12, 0, -4.12, -5.12
            */

            #endregion

            #region RealImagetoComplex
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

            /* output:
            *** Vector.RealImageToComplex ***
            Argument output: (-2.12, 1), (-1.12, 2), (0, 3), (1.12, 4), (2.12, 5)
            Return Output: (-2.12, 1), (-1.12, 2), (0, 3), (1.12, 4), (2.12, 5)
            */

            #endregion

            #region ComplexToRealImage
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

            #endregion

            #region PolarToComplex
            Console.WriteLine();
            Console.WriteLine("*** Vector.PolarToComplex ***");

            // define data
            double[] magnitudePtoC = new double[] { 1, 2 };
            double[] phasePtoC = new double[] { 0, 1.57 };
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
            phasePtoC = new double[] { 1, 2 };
            // magnitude, phase入, 返回complex data
            complexDataPtoC = Vector.PolarToComplex(magnitudePtoC, phasePtoC);
            Console.Write("Return output: ");
            Console.WriteLine(String.Join(", ", complexDataPtoC));

            // phase入, 返回complex data
            complexDataPtoC = Vector.PolarToComplex(phasePtoC);
            Console.Write("Return output: ");
            Console.WriteLine(String.Join(", ", complexDataPtoC));

            /* output:
             * *** Vector.PolarToComplex ***
            Complex Argument output: (1, 0), (0.00159265342146653, 1.99999936586367)
            Complex Argument output: (1, 0), (0.000796326710733263, 0.999999682931835)
            Return Output: (1.08060461173628, 1.68294196961579), (-1.24844050964143, 2.72789228047704)
            Return Output: (0.54030230586814, 0.841470984807897), (-0.416146836547142, 0.909297426825682)
            */

            #endregion

            //wait until keypress
            Console.ReadKey();
        }
    }
}
