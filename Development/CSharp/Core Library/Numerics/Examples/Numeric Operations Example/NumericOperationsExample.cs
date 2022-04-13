using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeSharpTools.JXI.Numerics.NumericOperationsExample
{
    internal class NumericOperationsExample
    {
        static void Main(string[] args)
        {
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
            double[] dataADoubleComp = new double[] { -2.12, -1.12, 0, 1.12, 2.12 };
            double[] dataBDoubleComp = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 };

            //compare array
            bool compared = Vector.ArrayEqual(dataADoubleComp, dataBDoubleComp);
            Console.Write("Compared Result: ");
            Console.WriteLine(compared);

            /* output:
            *** Vector.Equal ***
            Compared Result: False
            */

            #endregion


            //wait until keypress
            Console.ReadKey();
        }
    }
}
