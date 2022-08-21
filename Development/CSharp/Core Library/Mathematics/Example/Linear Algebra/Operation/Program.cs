using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using SeeSharpTools.JXI.Mathematics.LinearAlgebra.Matrix;
using SeeSharpTools.JXI.Numerics;

namespace Eigenvalue_Example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region ---Inverse Example---

            Console.WriteLine();
            Console.WriteLine("*** Inverse ***");

            //define data
            Complex32[,] A = new Complex32[4, 3] { { 3, 2, 4 }, { 2, 0, 2 }, { 4, 2, 3 }, { 4, 2, 3 } };
            Matrix<Complex32> matA = new Matrix<Complex32>(A);
            Matrix<Complex32> invA;
            Console.WriteLine();
            Console.WriteLine("* Original Matrix *");
            Console.WriteLine("3   2   4");
            Console.WriteLine("2   0   2");
            Console.WriteLine("4   2   3");

            //求逆
            //invA = Matrix<Complex32>.Inverse(matA);
            Console.WriteLine();
            Console.WriteLine("* Inverse Matrix *");
            //PrintMatrix(invA);
            Console.WriteLine();
            #endregion

            #region ---Transpose Example---

            Console.WriteLine();
            Console.WriteLine("*** Transpose ***");

            //define data
            Complex32[,] B = new Complex32[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            Matrix<Complex32> matB = new Matrix<Complex32>(B);
            Matrix<Complex32> tranB;
            Console.WriteLine();
            Console.WriteLine("* Original Matrix *");
            Console.WriteLine("1   2   3");
            Console.WriteLine("4   5   6");
            Console.WriteLine("7   8   9");

            //求转置
            tranB = Matrix<Complex32>.GetTranspose(matB);
            Console.WriteLine();
            Console.WriteLine("* Transpose Matrix *");
            //PrintMatrix(invA);
            Console.WriteLine();
            #endregion

            #region ---Multiply Example---

            Console.WriteLine();
            Console.WriteLine("*** Transpose ***");

            //define data
            Complex32[,] Left = new Complex32[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            Matrix<Complex32> matLeft = new Matrix<Complex32>(Left);
            Complex32[,] Right = new Complex32[3, 3] { { 1, 1, 1 }, { 2, 2, 2 }, { 3, 3, 3 } };
            Matrix<Complex32> matRight = new Matrix<Complex32>(Right);
            Complex32[,] Result = new Complex32[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            Matrix<Complex32> matResult = new Matrix<Complex32>(Result);
            Console.WriteLine();
            Console.WriteLine("* Original Left Matrix *");
            Console.WriteLine("1   2   3");
            Console.WriteLine("4   5   6");
            Console.WriteLine("7   8   9");
            Console.WriteLine();
            Console.WriteLine("* Original Right Matrix *");
            Console.WriteLine("1   1   1");
            Console.WriteLine("2   2   2");
            Console.WriteLine("3   3   3");


            //求矩阵相乘
            //Matrix<Complex32>.Multiply(matLeft, matRight, matResult);
            Console.WriteLine();
            Console.WriteLine("* Transpose Matrix *");
            //PrintMatrix(invA);
            Console.WriteLine();
            #endregion

        }
        static void PrintMatrix(Matrix<Complex32> matrix)
        {
            int i;
            int j;
            //for (i = 0; i < matrix.MatrixArray; i++)
            //{
            //    for (j = 0; j < matrix[0].Length; j++)
            //    {
            //        Console.Write("{0} ", matrix[i][j]);
            //    }
            //    Console.WriteLine();
            //}
        }
    }
}
