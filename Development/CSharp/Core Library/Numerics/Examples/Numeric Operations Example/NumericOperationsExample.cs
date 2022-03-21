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
            #region ABS example
            Console.WriteLine("*** Vector.Abs ***");
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
             *  *** Vector.ABS ***
            参数输出: 2.12345678901235, 1.12345678901235, 0, 1.12345678901235, 2.12345678901235
            数组返回: 2.12345678901235, 1.12345678901235, 0, 1.12345678901235, 2.12345678901235
            * 原位计算 *
            short[]:  2, 1, 0, 1, 2
            int[]:    234567, 123456, 0, 123456, 234567
            float[]:   2.123457, 1.123457, 0, 1.123457, 2.123457
            double[]: 2.12345678901235, 1.12345678901235, 0, 1.12345678901235, 2.12345678901235
             */
            #endregion
            #region Add example
            Console.WriteLine();
            Console.WriteLine("*** Vector.Add ***");
            //define data
            double[] dataADouble = new double[] { -2.12, -1.12, 0, 1.12, 2.12 };
            double[] dataBDouble = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            //A,B入C出
            double[] dataCDouble = new double[dataADouble.Length];
            Vector.ArrayAdd(dataADouble, dataBDouble, dataCDouble);
            Console.Write("参数输出: ");
            Console.WriteLine(String.Join(", ", dataCDouble));

            //A,B入返回数组
            double[] dataDoubleReturn = Vector.GetArrayAdd(dataADouble, dataBDouble);
            Console.Write("数组返回: ");
            Console.WriteLine(String.Join(", ", dataDoubleReturn));

            //** A,B入A出，原位计算 **
            Console.WriteLine("* 原位计算 *");
            //double原位数组相加
            Vector.ArrayAdd(dataADouble, dataBDouble);
            Console.Write("dataADouble:  ");
            Console.WriteLine(String.Join(", ", dataADouble));

            /* output:
             * *** Vector.Add ***
            参数输出: -1.12, 0.88, 3, 5.12, 7.12
            数组返回: -1.12, 0.88, 3, 5.12, 7.12
            * 原位计算 *
            dataADouble:  -1.12, 0.88, 3, 5.12, 7.12
            */
            #endregion

            #region Add1Value
            Console.WriteLine();
            Console.WriteLine("*** Vector.Add 1 value ***");
            dataADouble = new double[] { -2.12, -1.12, 0, 1.12, 2.12 }; //恢复初始数值
            double addingValue = 10;
            //** A,B入A出，原位计算 **
            Console.WriteLine("* 原位计算 *");
            //double原位数组加常数
            Vector.ArrayAdd(dataADouble, addingValue);
            Console.Write("dataADouble:  ");
            Console.WriteLine(String.Join(", ", dataADouble));
            /*
             * *** Vector.Add 1 value ***
            * 原位计算 *
            dataADouble:  7.88, 8.88, 10, 11.12, 12.12
            */
            #endregion


            //wait until keypress
            Console.ReadKey();
        }
    }
}
