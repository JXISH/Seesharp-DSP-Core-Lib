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
            double[,] A = new double[3, 3] { { 3, 2, 4 }, { 2, 0, 2 }, { 4, 2, 3 } };
            Matrix<double> matA = new Matrix<double>(A);
            Matrix<double> invA;
            Console.WriteLine();
            Console.WriteLine("* Original Matrix *");
            PrintMatrix(matA);

            //求逆
            invA = Matrix<double>.Inverse(matA);
            Console.WriteLine();
            Console.WriteLine("* Inversed Matrix *");
            PrintMatrix(invA);
            Console.WriteLine();
            #endregion

            #region ---Transpose Example---

            Console.WriteLine();
            Console.WriteLine("*** Transpose ***");

            //define data
            double[,] B = new double[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            Matrix<double> matB = new Matrix<double>(B);
            Matrix<double> tranB;
            Console.WriteLine();
            Console.WriteLine("* Original Matrix *");
            PrintMatrix(matB);

            //求转置
            tranB = Matrix<double>.GetTranspose(matB);
            Console.WriteLine();
            Console.WriteLine("* Transposed Matrix *");
            PrintMatrix(tranB);
            Console.WriteLine();
            #endregion

            #region ---Multiply Example---

            Console.WriteLine();
            Console.WriteLine("*** Multiply ***");

            //define data
            double[,] Left = new double[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            Matrix<double> matLeft = new Matrix<double>(Left);
            double[,] Right = new double[3, 3] { { 1, 1, 1 }, { 2, 2, 2 }, { 3, 3, 3 } };
            Matrix<double> matRight = new Matrix<double>(Right);
            double[,] Result = new double[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            Matrix<double> matResult = new Matrix<double>(Result);
            Console.WriteLine();
            Console.WriteLine("* Original Left Matrix *");
            PrintMatrix(matLeft);

            Console.WriteLine();
            Console.WriteLine("* Original Right Matrix *");
            PrintMatrix(matRight);

            //求矩阵相乘
            Matrix<double>.Multiply(matLeft, matRight, matResult);
            Console.WriteLine();
            Console.WriteLine("* Multiplied Matrix *");
            PrintMatrix(matResult);
            Console.WriteLine();
            #endregion

            //Exit after key pressed
            Console.ReadKey();
        }
        static void PrintMatrix(Matrix<double> matrix)
        {
            int i;
            int j;
            for (i = 0; i < matrix.MatrixArray.GetLength(0); i++)
            {
                for (j = 0; j < matrix.MatrixArray.GetLength(1); j++)
                {
                    Console.Write("{0} ", matrix.MatrixArray[i,j]);
                }
                Console.WriteLine();
            }
        }
    }
}
