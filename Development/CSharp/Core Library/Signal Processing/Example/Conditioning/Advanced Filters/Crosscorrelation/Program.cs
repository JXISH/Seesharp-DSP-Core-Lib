using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using SeeSharpTools.JXI.Numerics;
using SeeSharpTools.JXI.SignalProcessing.Conditioning.AdvancedFilters;

namespace CrossCorrelation_Example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region  --- Cross Correlation ---
            //定义输入和输出参数
            double[] src1Double = new double[7] { 0, 0, 1, 1, 1, 0, 0 };
            double[] src2Double = new double[7] { 0, 0, 1, 1, 1, 0, 0 };
            double[] outputDouble = new double[13];

            double[] src1Float = new double[7] { 0, 0, 1, 1, 1, 0, 0 };
            double[] src2Float = new double[7] { 0, 0, 1, 1, 1, 0, 0 };
            double[] outputFloat = new double[13];

            Complex[] src1Complex = new Complex[7] { 0, 0, 1, 1, 1, 0, 0 };
            Complex[] src2Complex = new Complex[7] { 0, 0, 1, 1, 1, 0, 0 };
            Complex[] outputComplex = new Complex[13];

            Complex32[] src1Complex32 = new Complex32[7] { 0, 0, 1, 1, 1, 0, 0 };
            Complex32[] src2Complex32 = new Complex32[7] { 0, 0, 1, 1, 1, 0, 0 };
            Complex32[] outputComplex32 = new Complex32[13];

            //计算互相关
            CrossCorrelation.ComputeCrossCorrelation(src1Double, src2Double, out outputDouble);
            CrossCorrelation.ComputeCrossCorrelation(src1Float, src2Float, out outputFloat);
            CrossCorrelation.ComputeCrossCorrelation(src1Complex, src2Complex, out outputComplex);
            CrossCorrelation.ComputeCrossCorrelation(src1Complex32, src2Complex32, out outputComplex32);

            Console.WriteLine("*** Cross Correlation ***");

            Console.WriteLine("\nDouble input:");
            Console.Write("src1: ");
            foreach (var item in src1Double) { Console.Write("{0} ", item); }
            Console.Write("\nsrc2: ");
            foreach (var item in src2Double) { Console.Write("{0} ", item); }
            Console.WriteLine("\nDouble output:");
            foreach (var item in outputDouble) { Console.Write("{0} ", item); }
            Console.WriteLine();

            Console.WriteLine("\nFloat input:");
            Console.Write("src1: ");
            foreach (var item in src1Float) { Console.Write("{0} ", item); }
            Console.Write("\nsrc2: ");
            foreach (var item in src2Float) { Console.Write("{0} ", item); }
            Console.WriteLine("\nFloat output:");
            foreach (var item in outputFloat) { Console.Write("{0} ", item); }
            Console.WriteLine();

            Console.WriteLine("\nComplex input:");
            Console.Write("src1: ");
            foreach (var item in src1Complex) { Console.Write("{0} ", item); }
            Console.Write("\nsrc2: ");
            foreach (var item in src2Complex) { Console.Write("{0} ", item); }
            Console.WriteLine("\nComplex output:");
            foreach (var item in outputComplex) { Console.Write("{0} ", item); }
            Console.WriteLine();

            Console.WriteLine("\nComplex32 input:");
            Console.Write("src1: ");
            foreach (var item in src1Complex32) { Console.Write("{0}; ", item); }
            Console.Write("\nsrc2: ");
            foreach (var item in src2Complex32) { Console.Write("{0}; ", item); }
            Console.WriteLine("\nComplex32 output:");
            foreach (var item in outputComplex32) { Console.Write("{0}; ", item); }
            Console.WriteLine();
            Console.ReadKey();

            /*
            *** Cross Correlation ***

            Double input:
            src1: 0 0 1 1 1 0 0
            src2: 0 0 1 1 1 0 0
            Double output:
            0 0 0 0 1 2 3 2 1 0 0 0 0

            Float input:
            src1: 0 0 1 1 1 0 0
            src2: 0 0 1 1 1 0 0
            Float output:
            0 0 0 0 1 2 3 2 1 0 0 0 0

            Complex input:
            src1: (0, 0) (0, 0) (1, 0) (1, 0) (1, 0) (0, 0) (0, 0)
            src2: (0, 0) (0, 0) (1, 0) (1, 0) (1, 0) (0, 0) (0, 0)
            Complex output:
            (0, 0) (0, 0) (0, 0) (0, 0) (1, 0) (2, 0) (3, 0) (2, 0) (1, 0) (0, 0) (0, 0) (0, 0) (0, 0)

            Complex32 input:
            src1: 0, 0 i; 0, 0 i; 1, 0 i; 1, 0 i; 1, 0 i; 0, 0 i; 0, 0 i;
            src2: 0, 0 i; 0, 0 i; 1, 0 i; 1, 0 i; 1, 0 i; 0, 0 i; 0, 0 i;
            Complex32 output:
            0, 0 i; 0, 0 i; 0, 0 i; 0, 0 i; 1, 0 i; 2, 0 i; 3, 0 i; 2, 0 i; 1, 0 i; 0, 0 i; 0, 0 i; 0, 0 i; 0, 0 i;
            */
            #endregion
        }
    }
}
